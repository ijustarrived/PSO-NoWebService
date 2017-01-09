﻿using PSO.Entities;
using PSO.Repositorios;
using PSO.Utilities;
using System;
using System.Collections.Generic;
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
            if (!IsPostBack)
            {
                Usuario userProfile = new Usuario(), // Selected profile
                    loggedUser = Session["UserObj"] == null ? new Usuario() 
                    : (Usuario)Session["UserObj"];

                if (Request.QueryString["Email"] != null)
                {
                    userProfile = UserRepo.GetUserByEmail((string)Request.QueryString["Email"]);

                    //Redirect if he isn't admin and if this profile isn't his
                    if (!loggedUser.Email.Equals(userProfile.Email) && loggedUser.Role.RoleType != Rol.TiposRole.ADMINISTRADOR)
                    {
                        if(string.IsNullOrEmpty(loggedUser.Email))
                            Response.Redirect("Register.aspx", true);

                        else
                            Response.Redirect("Main.aspx", true);
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

                    mainDashLink.NavigateUrl = "~/Pages/Dashboard/Main.aspx";

                    mainDashLink.Text = "Inicio";

                    dashboardPnl.Controls.Add(mainDashLink);
                    #endregion

                    if (loggedUser.Role.RoleType == Rol.TiposRole.ADMINISTRADOR)
                        tipoUserDiv.Visible = true;

                    #region Edit

                    Page.Title = "Perfil";

                    //To avoid validation when editting
                    passwordREV.Enabled = false;

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

                    licenseTxtBx.Text = userProfile.LicenciaConducir;

                    nameTxtBx.Text = userProfile.Nombre;

                    puebloPostalDDL.SelectedIndex = userProfile.PuebloPost;

                    puebloResiDDL.SelectedIndex = userProfile.Pueblo;

                    sSTxtBx.Text = userProfile.SeguroSocial;

                    telResTxtBx.Text = userProfile.Tel;

                    tipoUsuarioDDL.SelectedIndex = (int)userProfile.Role.RoleType;  

                    #endregion

                    #endregion
                }
            }

            else
            {
                if(Session["registerError"] != null && !string.IsNullOrEmpty((string)Session["registerError"]))
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), string.Format( "registerError-{0}{1}{2}", 
                        DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                                    string.Format( "alert('{0}');", ((string)Session["registerError"]).Replace("\r\n", " ")), true);

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

                LicenciaConducir = licenseTxtBx.Text,

                Nombre = nameTxtBx.Text,

                Password = string.IsNullOrEmpty(passwordTxtBx.Text) ? string.Empty
                : Usuario.EncryptWord(passwordTxtBx.Text),

                Pueblo = puebloResiDDL.SelectedIndex,

                PuebloPost = puebloPostalDDL.SelectedIndex,

                SeguroSocial = sSTxtBx.Text,

                Tel = telResTxtBx.Text,

                Role = RoleRepo.GetRoleByType(tipoUsuarioDDL.SelectedIndex)
            };

            //switch (tipoUsuarioDDL.SelectedIndex)
            //{
            //    case (int)Rol.TiposRole.EXTERNO:

            //        userProfile.Role.RoleType = Rol.TiposRole.EXTERNO;

            //        break;

            //    case (int)Rol.TiposRole.ADMINISTRADOR:

            //        userProfile.Role.RoleType = Rol.TiposRole.ADMINISTRADOR;

            //        break;

            //    case (int)Rol.TiposRole.COORDINADOR:

            //        userProfile.Role.RoleType = Rol.TiposRole.COORDINADOR;

            //        break;

            //    default:

            //        userProfile.Role.RoleType = Rol.TiposRole.PROCESADOR;

            //        break;
            //}

            #endregion

            Usuario loggedUser = Session["UserObj"] == null ? new Usuario() : (Usuario)Session["UserObj"];

            {
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

                    //Means that user didn't change password
                    if (string.IsNullOrEmpty(passwordTxtBx.Text))
                        userProfile.Password = (string)ViewState["Password"];

                    try
                    {
                        //Viewstate has old email
                        Exception excep = UserRepo.Update(userProfile, (string)ViewState["Email"]);

                        if (excep != null)
                        {
                            //ClientScript.RegisterStartupScript(this.GetType(), "registerEditAlert",
                            //string.Format("alert('No se pudo actualizar el perfil. Error: {0}');", excep.Message), true);

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

                    #endregion
                }
                #endregion
            }
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