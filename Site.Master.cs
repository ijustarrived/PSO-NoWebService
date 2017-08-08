using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using PSO.Entities;
using PSO.Repositorios;
using System.Threading.Tasks;

namespace PSO
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario user = Session["UserObj"] == null ? new Usuario() : (Usuario)Session["UserObj"];

            Cosmetic cosmetic = new Cosmetic();

            /*
             * Since the value on the session doesn't change unless custom page is saved,
             *  this way it shows the cosmetic changes without having to save and without having to change the original session
             */
            if (Page.AppRelativeVirtualPath.Contains("CustomizeAllPages"))
                cosmetic = Session["DemoCosmetic"] == null ? CosmeticsRepo.GetPageCosmetics()
                : (Cosmetic)Session["DemoCosmetic"];

            else
                cosmetic = Session["Cosmetic"] == null ? CosmeticsRepo.GetPageCosmetics()
                : (Cosmetic)Session["Cosmetic"];

            logoutBtn.ForeColor = System.Drawing.ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            footerLblDiv.Style.Add("color", cosmetic.LabelForeColor);

            userLink.ForeColor = System.Drawing.ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            pageTitleDiv.Style.Add("background-color", cosmetic.TitleBackColor);

            logoImg.ImageUrl = cosmetic.LogoPath;

            //Avoid Unauthenticated login
            if (string.IsNullOrEmpty(user.Email))
                Response.Redirect("~/Pages/Login.aspx", true);

            userLink.Text = user.Email;

            if (Session["Cosmetic"] == null)
                Session["Cosmetic"] = cosmetic;

            #region Email Thread Excpetion alert

            if (Session["emailError"] != null && string.IsNullOrEmpty((string)Session["emailError"]))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "emailAlert",
                            string.Format("alert('Hubo problemas enviando el correo. Error: {0}');",
                            ((string)Session["emailError"]).Replace("'", string.Empty).Replace("\r\n", " ")), true);

                Session.Remove("emailError");
            }

            else if (Session["emailThreadError"] != null && string.IsNullOrEmpty((string)Session["emailThreadError"]))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "emailThreadAlert",
                            string.Format("alert('Hubo problemas con el thread que envia correos Error: {0}');",
                            ((string)Session["emailThreadError"]).Replace("'", string.Empty).Replace("\r\n", " ")), true);

                Session.Remove("emailThreadError");
            }

            #endregion
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        protected void logoutBtn_Click(object sender, EventArgs e)
        {
            Usuario user = Session["UserObj"] == null ? new Usuario() : (Usuario)Session["UserObj"];

            #region Release all locked solicitudes in a new thread

            if (user.Role.RoleType == Rol.TiposRole.COORDINADOR)
            {
                try
                {
                    Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            Exception excep = SolicitudRepo.ReleaseAllLockedSolicitudes(user.ID);

                            if (excep != null)
                            {
                                throw new Exception(string.Format(
                                "No se pudo actualizar la solicitudes. Error Liberar Solicitudes: {0}",
                                    excep.Message.Replace("'", string.Empty)));
                            }
                        }

                        catch (Exception ex)
                        {
                            Session["emailError"] = ex.Message;
                        }
                    });
                }

                catch (Exception ex)
                {
                    Session["emailThreadError"] = ex.Message;
                }
            }

            #endregion

            UserRepo.UpdateUserLoggedLock(user.ID, false);

            #region Save log

            UsuarioLog userLog = new UsuarioLog(user);

            userLog.LogOutDate = DateTime.Now;

            UserLogRepo.Create(userLog);

            #endregion

            Globals.SetSessionIsAlive(false);

            if (user != null)
            {
                Session.Remove("UserObj");

                Session.Remove("UserId");
            }

            //Session.Remove("UserObj");

            Response.Redirect("~/Pages/Login.aspx", true);
        }
    }

}