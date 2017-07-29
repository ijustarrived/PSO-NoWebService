using PSO.Entities;
using PSO.Repositorios;
using PSO.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PSO.Pages
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Set Cosmetics

            Cosmetic cosmetic = (Cosmetic)Session["Cosmetic"];

            PosLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            dirLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            apeMatLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            emaiLlbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            apePatLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            bdayLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            campoReqLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            celLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            codigoLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            codigoPosLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            dirPosLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            nombreLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            passLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            puebloLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            puebloPosLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            residencialLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            telLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            tipoLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            saveBtn.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            ScriptManager.RegisterStartupScript(this, GetType(), "InvokeChangeClinetSideColors",
                                    string.Format("ChangeClinetSideColors('{0}', '{1}');",
                                    cosmetic.LabelForeColor, cosmetic.TitleBackColor), true);

            #endregion

            //Multiline txt length
            dirResTxtBx.Attributes["maxlength"] = Convert.ToString(400);

            dirPostalTxtBx.Attributes["maxlength"] = Convert.ToString(400);

            if (!IsPostBack)
            {
                Usuario userProfile = new Usuario(), // Selected profile
                    loggedUser = Session["UserObj"] == null ? new Usuario()
                    : (Usuario)Session["UserObj"]; //Session user

                if (Request.QueryString["Email"] != null)
                {
                    userProfile = UserRepo.GetUserByEmail((string)Request.QueryString["Email"]);

                    //Redirect if he isn't admin and if this profile isn't his
                    if (!loggedUser.Email.Equals(userProfile.Email) && loggedUser.Role.RoleType != Rol.TiposRole.ADMINISTRADOR)
                    {
                        //if(string.IsNullOrEmpty(loggedUser.Email))
                        //    Response.Redirect("Register.aspx", true);

                        if (string.IsNullOrEmpty(loggedUser.Email))
                            Response.Redirect("Login.aspx", true);

                        else
                            Response.Redirect("~/Pages/Dashboard/Main.aspx", true);
                    }
                }

                else
                    userProfile = loggedUser;

                if (!string.IsNullOrEmpty(userProfile.Email))
                {
                    #region Breadcrumb Config

                    var dashboardPnl = (Panel)Master.FindControl("dashboardLinkPnl");

                    dashboardPnl.Controls.Clear();

                    HyperLink mainDashLink = new HyperLink();

                    mainDashLink.ID = "mainDashLink";

                    mainDashLink.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

                    mainDashLink.NavigateUrl = "~/Pages/Dashboard/Main.aspx";

                    mainDashLink.Text = "Inicio";

                    dashboardPnl.Controls.Add(mainDashLink);

                    #endregion

                    if (loggedUser.Role.RoleType == Rol.TiposRole.ADMINISTRADOR)
                        tipoUserDiv.Visible = true;

                    #region Edit

                    Page.Title = "Perfil";

                    emailTxtBx.Enabled = false;

                    //To avoid validation when editting
                    passRFV.Enabled = false;

                    passLbl.Text = passLbl.Text.Replace("*", string.Empty);
                    //passwordREV.Enabled = false;

                    #region Set txtBxs & DDLs

                    apellidoMatTxtBx.Text = userProfile.ApellidoMaterno;

                    apellidoPatTxtBx.Text = userProfile.ApellidoPaterno;

                    celTxtBx.Text = userProfile.Celular;

                    codigoPostalTxtBx.Text = userProfile.CodigoPostalPost;

                    codigoResiTxtBx.Text = userProfile.CodigoPostal;

                    dirPostalTxtBx.Text = userProfile.DireccionPost;

                    dirResTxtBx.Text = userProfile.Direccion;

                    emailTxtBx.Text = userProfile.Email;

                    ViewState["Email"] = userProfile.Email;

                    ViewState["Password"] = userProfile.Password;

                    bdayTxtBx.Text = userProfile.FechaNacimiento.ToShortDateString();

                    //licenseTxtBx.Text = userProfile.LicenciaConducir;

                    nameTxtBx.Text = userProfile.Nombre;

                    puebloPostalDDL.SelectedIndex = userProfile.PuebloPost;

                    puebloResiDDL.SelectedIndex = userProfile.Pueblo;

                    //sSTxtBx.Text = userProfile.SeguroSocial;

                    telResTxtBx.Text = userProfile.Tel;

                    tipoUsuarioDDL.SelectedIndex = (int)userProfile.Role.RoleType;

                    #endregion

                    #endregion
                }
            }

            else
            {
                if (Session["registerError"] != null && !string.IsNullOrEmpty((string)Session["registerError"]))
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), string.Format("registerError-{0}{1}{2}",
                        DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                                    string.Format("alert('{0}');", ((string)Session["registerError"]).Replace("\r\n", " ")), true);

                    Session["registerError"] = string.Empty;
                }
            }
        }

        protected void saveBtn_Click(object sender, EventArgs e)
        {
            #region Define and instanciate User obj

            Usuario userProfile = new Usuario()
            {
                Activo = true,

                ApellidoMaterno = apellidoMatTxtBx.Text,

                ApellidoPaterno = apellidoPatTxtBx.Text,

                Celular = celTxtBx.Text,

                CodigoPostal = codigoResiTxtBx.Text,

                CodigoPostalPost = codigoPostalTxtBx.Text,

                Direccion = dirResTxtBx.Text,

                DireccionPost = dirPostalTxtBx.Text,

                Email = emailTxtBx.Text,

                FechaNacimiento = Convert.ToDateTime(bdayTxtBx.Text),

                //LicenciaConducir = licenseTxtBx.Text,

                Nombre = nameTxtBx.Text,

                Password = string.IsNullOrEmpty(passwordTxtBx.Text) ? string.Empty
                : Usuario.EncryptWord(passwordTxtBx.Text),

                Pueblo = puebloResiDDL.SelectedIndex,

                PuebloPost = puebloPostalDDL.SelectedIndex,

                //SeguroSocial = sSTxtBx.Text,

                Tel = telResTxtBx.Text,

                Role = RoleRepo.GetRoleByType(tipoUsuarioDDL.SelectedIndex)
            };

            #endregion

            Usuario loggedUser = Session["UserObj"] == null ? new Usuario() : (Usuario)Session["UserObj"];

            #region Create/Edit
            if (string.IsNullOrEmpty(loggedUser.Email))
            {
                #region Create

                Usuario existingUser = UserRepo.GetUserByEmail(userProfile.Email);

                if (!existingUser.Email.Equals(userProfile.Email))
                {
                    Exception excep = UserRepo.Create(userProfile);

                    try
                    {
                        if (excep != null)
                        {
                            //ScriptManager.RegisterClientScriptBlock(this, GetType(), "registerCreateAlert",
                            //    string.Format("alert('No se pudo crear el usuario. Error: {0}');",
                            //    excep.Message.Replace("'", string.Empty)), true);

                            throw new Exception(string.Format("No se pudo crear el usuario. Error: {0}",
                                excep.Message.Replace("'", string.Empty)));
                        }

                        CreateUserDir(userProfile.Email);

                        SendThreadedEmail(userProfile, Correo.GetSubject(Correo.MailType.REGISTER),
                Correo.GetBody(Correo.MailType.REGISTER));

                        Response.Redirect("~/Pages/Login.aspx", true);
                    }

                    catch (Exception ex)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "registerError",
                                string.Format("alert('{0}');", ex.Message.Replace("\r\n", " ")), true);
                    }
                }

                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "emailAleardyExistAlert",
                                "alert('Ya existe un usuario con ese Correo.');", true);
                }

                #endregion
            }

            else
            {
                #region Edit

                #region Verify if adding this user doesn't break license restriction
                Usuario existingUser = UserRepo.GetUserByEmail((string)ViewState["Email"]);

                bool canAddUserToInternal = true;

                if (existingUser.Role.RoleType == Rol.TiposRole.EXTERNO && tipoUsuarioDDL.SelectedIndex != ((int)Rol.TiposRole.EXTERNO))
                {
                    int internalUserCount = UserRepo.GetInternalUserCount();

                    canAddUserToInternal = Usuario.GetUserMaxAmount() > internalUserCount;
                }
                #endregion

                if (canAddUserToInternal)
                {
                    //Means that user didn't change password
                    if (string.IsNullOrEmpty(passwordTxtBx.Text))
                        userProfile.Password = (string)ViewState["Password"];

                    try
                    {
                        //Viewstate has old email
                        Exception excep = UserRepo.Update(userProfile, (string)ViewState["Email"]);

                        if (excep != null)
                        {
                            throw new Exception(string.Format("No se pudo actualizar el perfil. Error: {0}"
                                , excep.Message));
                        }

                        #region Update user dir

                        string oldEmail = (string)ViewState["Email"];

                        if (!oldEmail.Equals(emailTxtBx.Text))
                        {
                            Directory.Move(Server.MapPath(string.Format("{0}/{1}", DocReq.GetPathDocs(), oldEmail)),
                                Server.MapPath(string.Format("{0}/{1}", DocReq.GetPathDocs(), emailTxtBx.Text)));

                            //Update solicitud and docs email
                        }

                        #endregion

                        //Update current logged User if same profile
                        if (oldEmail.Equals(loggedUser.Email))
                            Session["UserObj"] = userProfile;

                        Response.Redirect("~/Pages/Dashboard/Main.aspx", true);
                    }

                    catch (Exception ex)
                    {
                        Session["registerError"] = ex.Message;
                    }
                }

                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "exceedsLicenseAlert",
                        "alert('No se puede añadir usuario, ya que excedió el límite establecido para su licencia.');", true);
                }


                #endregion
            }
            #endregion
        }

        private void CreateUserDir(string email)
        {
            Directory.CreateDirectory(Server.MapPath(string.Format("{0}/{1}",
                DocReq.GetPathDocs(), email)));
        }

        private void SendThreadedEmail(Usuario _to, string subj, string body)
        {
            #region Send email

            LinkedList<string> to = new LinkedList<string>();

            to.AddLast(_to.Email);

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
            }

            #endregion
        }
    }
}