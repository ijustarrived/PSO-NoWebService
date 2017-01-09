using PSO.Entities;
using PSO.Repositorios;
using PSO.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PSO.Pages
{
    public partial class RecoverPass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void bdayBtn_Click(object sender, EventArgs e)
        {
            Usuario user = UserRepo.GetUserByEmail(emailTxtBx.Text);

            if (string.IsNullOrEmpty(user.Email))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "wrongEmailAlert",
                   "alert('Correo Electrónico no existe');", true);
            }

            else
            {
                ssDiv.Visible = true;

                recoverBtn.Visible = ssDiv.Visible;

                ViewState["user"] = user;
            }
        }

        protected void recoverBtn_Click(object sender, EventArgs e)
        {
            Usuario user = (Usuario)ViewState["user"];

            try
            {
                if ((Convert.ToDateTime(ssTxtBx.Text)).Date.Equals(user.FechaNacimiento.Date))
                {
                    SendThreadedEmail(user, Correo.GetSubject(Correo.MailType.PASS_RECOVER),
                        Correo.GetBody(Correo.MailType.PASS_RECOVER).Replace("@PASS", Usuario.DecryptWord(user.Password)));

                    ClientScript.RegisterStartupScript(this.GetType(), "passSentAlert",
                       "alert('Contraseña fue enviada por Correo Electrónico'); window.location = 'Login.aspx';", true);
                }

                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "wrongBdayAlert",
                       "alert('Fecha nacimiento no coincide');", true);
                }
            }

            catch(Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "registerCreateAlert",
                                    string.Format("alert('No se pudo recuperar contraseña. Error: {0}');",
                                    ex.Message.Replace("'", string.Empty)), true);
            }
        }

        private void SendThreadedEmail(Usuario _to, string subj, string body)
        {
            #region Send email

            LinkedList<string> to = new LinkedList<string>();

            to.AddLast(_to.Email);

            //for (int i = 0; i < _to.Count; i++)
            //{
            //    to.AddLast(_to.ElementAt(i).Email);
            //}

            //to.AddLast(user.Email);

            Mailing mail = new Mailing(25, Mailing.GetPSOSMTPServer());

            //Send email in a new thread

            Exception exMail = null;

            try
            {
                Task.Factory.StartNew(() =>
                {
                try
                {
                    exMail = mail.SendEmail(mail.ComposeEmail(to, subj, body));

                        if (exMail != null)
                        {
                            throw new Exception(exMail.Message);
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

                //exMail = ex;

                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "emailThreadAlert",
                //                string.Format("alert('Hubieron problemas con el thread que envia correos. Error: {0}');",
                //                ex.Message.Replace("'", string.Empty)), true);
            }

            #endregion
        }
    }
}