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

            //Avoid Unauthenticated login
            if (string.IsNullOrEmpty(user.Email))
                Response.Redirect("~/Pages/Login.aspx", true);

            userLink.Text = user.Email;

            #region Email Thread Excpetion alert

            if (Session["emailError"] != null && string.IsNullOrEmpty((string)Session["emailError"]))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "emailAlert",
                            string.Format("alert('Hubo problemas enviado el correo. Error: {0}');",
                            ((string)Session["emailError"]).Replace("'", string.Empty)), true);

                Session.Remove("emailError");
            }

            else if (Session["emailThreadError"] != null && string.IsNullOrEmpty((string)Session["emailThreadError"]))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "emailThreadAlert",
                            string.Format("alert('Hubo problemas con el thread que envia correos Error: {0}');",
                            ((string)Session["emailThreadError"]).Replace("'", string.Empty)), true);

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
            Session.Remove("UserObj");

            Response.Redirect("~/Pages/Login.aspx", true);
        }
    }

}