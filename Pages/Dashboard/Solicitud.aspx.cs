﻿using PSO.Entities;
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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;

namespace PSO.Pages.Dashboard
{
    public partial class Solicitud : Page
    {
        protected void Page_Load(object sender, EventArgs e)
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

            Usuario user = Session["UserObj"] == null ? new Usuario() : (Usuario)Session["UserObj"];

            if (!user.Role.ViewSolicitud)
            {
                if (string.IsNullOrEmpty(user.Email))
                    Response.Redirect("~/Pages/Login.aspx", true);

                Response.Redirect("~/Pages/Dashboard/Main.aspx", true);
            }

            string numSolicitud = Request.QueryString["numSolicitud"] == null ? string.Empty
                    : (string)Request.QueryString["numSolicitud"];

            CreateDocReqCtrls(numSolicitud);

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(numSolicitud))
                {
                    #region Edit

                    _Solicitud solicitud = SolicitudRepo.GetSolicitudByNumSolicitud(numSolicitud);

                    #region Set coordinadores ddl

                    LinkedList<Usuario> coordinadores = UserRepo.GetUsersByRole((int)Usuario.TiposUsuarios.COORDINADOR);

                    for (int i = 0; i < coordinadores.Count; i++)
                    {
                        coorDDL.Items.Add(coordinadores.ElementAt(i).GetNombreCompleto());
                    }

                    #endregion

                    #region Set status

                    statusDiv.Visible = true;

                    Dictionary<string, Color> statusMsgColor = _Solicitud.GetStatusMsgAndColor(solicitud.Status);

                    statusTxtLbl.ForeColor = statusMsgColor.ElementAt(0).Value;

                    statusTxtLbl.Text = statusMsgColor.ElementAt(0).Key;

                    #endregion

                    #region Set employee header and other components according to solicitud status

                    #region Set header with solicitud info if existing

                    #region User

                    userRow.Visible = true;

                    string[] split = numSolicitudLbl.Text.Split(':');

                    numSolicitudLbl.Text = string.Format("{0} : {1}", split[0], solicitud.NumeroSolicitud);

                    split = fechaSolicitudLbl.Text.Split(':');

                    fechaSolicitudLbl.Text = string.Format("{0} : {1}", split[0], solicitud.FechaTramitada.ToShortDateString());

                    #endregion

                    #region Coordinador

                    if (!solicitud.FechaRevision.Date.Equals(DateTime.MaxValue.Date))
                    {
                        coorRow.Visible = true;

                        coorDDL.SelectedIndex = solicitud.CoordinadorID;

                        coorDDL.Enabled = false;

                        split = fechaRevisadoLbl.Text.Split(':');

                        fechaRevisadoLbl.Text = string.Format("{0} : {1}", split[0], solicitud.FechaRevision.ToShortDateString());

                        asigCommentDiv.Visible = true;

                        asigComment.Text = solicitud.ComentarioProcesador;

                        if (solicitud.Status != _Solicitud.Statuses.PEND_REVISAR)
                            asigComment.Enabled = false;
                    }

                    #endregion

                    #region Asignacion

                    if (!solicitud.FechaAsigProcesador.Date.Equals(DateTime.MaxValue.Date))
                    {
                        asigRow.Visible = true;

                        #region Set procesadores ddl

                        LinkedList<Usuario> procesadores = UserRepo.GetUsersByRole((int)Usuario.TiposUsuarios.PROCESADOR);

                        for (int i = 0; i < procesadores.Count; i++)
                        {
                            procesadoresDDL.Items.Add(procesadores.ElementAt(i).GetNombreCompleto());
                        }

                        #endregion

                        procesadoresDDL.SelectedIndex = solicitud.ProcesadorId;

                        procesadoresDDL.Enabled = false;

                        split = fechaAsignadoLbl.Text.Split(':');

                        fechaAsignadoLbl.Text = string.Format("{0} : {1}", split[0], solicitud.FechaAsigProcesador.ToShortDateString());
                    }

                    #endregion

                    #region Trabajo

                    if (!solicitud.FechaTrabajado.Date.Equals(DateTime.MaxValue.Date))
                    {
                        //split = trabajoSistemaLbl.Text.Split(':');

                        //trabajoSistemaLbl.Text = string.Format("{0} : {1}", split[0], "Sí");

                        statusDDL.SelectedValue = solicitud.Status.ToString();

                        statusDDL.Enabled = false;

                        split = fechaTrabajoLbl.Text.Split(':');

                        fechaTrabajoLbl.Text = string.Format("{0} : {1}", split[0], solicitud.FechaTrabajado.ToShortDateString());

                        trabajoCommentDiv.Visible = true;

                        trabajoComment.Text = solicitud.ComentarioTrabajo;

                        trabajoComment.Enabled = false;
                    }
                    #endregion

                    #endregion

                    #region Set header and some components according to solicitud status for the 1st time

                    switch (solicitud.Status)
                    {
                        case _Solicitud.Statuses.PEND_REVISAR:

                            if (user.Role.RoleType != Rol.TiposRole.COORDINADOR)
                                ScriptManager.RegisterStartupScript(this, GetType(), "userMustWaitAlert",
                                    string.Format("WaitingAnswerAlert('{0}');", "Esperar revision de coordinador"), true);

                            coorRow.Visible = true;

                            coorDDL.SelectedValue = user.GetNombreCompleto();

                            coorDDL.Enabled = false;

                            asigCommentRFV.Enabled = true;

                            string[] _split = fechaRevisadoLbl.Text.Split(':');

                            fechaRevisadoLbl.Text = string.Format("{0} : {1}", _split[0], DateTime.Now.ToShortDateString());

                            asigCommentDiv.Visible = true;

                            break;

                        case _Solicitud.Statuses.PEND_ASIGNAR:

                            if (user.Role.RoleType != Rol.TiposRole.SUPERVISOR)
                                ScriptManager.RegisterStartupScript(this, GetType(), "userMustWaitAlert",
                                    string.Format("WaitingAnswerAlert('{0}');", "Esperar a que se asigne un procesador"), true);

                            asigRow.Visible = true;

                            #region Set procesadores ddl

                            LinkedList<Usuario> procesadores = UserRepo.GetUsersByRole((int)Usuario.TiposUsuarios.PROCESADOR);

                            for (int i = 0; i < procesadores.Count; i++)
                            {
                                procesadoresDDL.Items.Add(procesadores.ElementAt(i).GetNombreCompleto());
                            }

                            #endregion

                            _split = fechaAsignadoLbl.Text.Split(':');

                            fechaAsignadoLbl.Text = string.Format("{0} : {1}", _split[0], DateTime.Now.ToShortDateString());

                            break;

                        case _Solicitud.Statuses.PEND_TRABAJAR:

                            if (user.Role.RoleType != Rol.TiposRole.PROCESADOR)
                                ScriptManager.RegisterStartupScript(this, GetType(), "userMustWaitAlert",
                                    string.Format("WaitingAnswerAlert('{0}');", "Esperar a que sea trabajado por procesador"), true);

                            trabajadoRow.Visible = true;

                            trabajoCommentDiv.Visible = trabajadoRow.Visible;

                            _split = fechaTrabajoLbl.Text.Split(':');

                            fechaTrabajoLbl.Text = string.Format("{0} : {1}", _split[0], DateTime.Now.ToShortDateString());

                            break;

                        case _Solicitud.Statuses.PEND_DOCS:

                            if (user.Role.RoleType != Rol.TiposRole.EXTERNO)
                                ScriptManager.RegisterStartupScript(this, GetType(), "ProcMustWaitAlert",
                                    string.Format("WaitingAnswerAlert('{0}');", "Esperar por documentos del solicitante"), true);

                            ScriptManager.RegisterStartupScript(this, GetType(), "uploadAlert",
                                    @"alert('Debe subir todos los documentos, nuevamente, para
asegurar que se encuentren actualizados')".Replace("\r\n", " "), true);

                            coorDDL.Enabled = false;

                            asigComment.Enabled = false;

                            procesadoresDDL.Enabled = false;

                            trabajoComment.Enabled = false;

                            ViewState["Status"] = _Solicitud.Statuses.PEND_DOCS;

                            break;

                        case _Solicitud.Statuses.INACTIVO:

                            saveBtn.Enabled = false;

                            coorDDL.Enabled = false;

                            asigComment.Enabled = false;

                            procesadoresDDL.Enabled = false;

                            trabajoComment.Enabled = false;

                            break;

                        default:

                            saveBtn.Enabled = false;

                            trabajadoRow.Visible = true;

                            trabajoCommentDiv.Visible = true;

                            trabajoComment.Enabled = false;

                            break;
                    }

                    #endregion

                    #endregion

                    #region Set info Solicitante

                    nameTxtBx.Text = solicitud.Nombre;

                    paternoTxtBx.Text = solicitud.ApellidoPaterno;

                    maternoTxtBx.Text = solicitud.ApellidoMaterno;

                    solicitud.SeguroSocial = Usuario.DecryptWord(solicitud.SeguroSocial);

                    string ssnLastSet = string.Empty;

                    for (int i = 0; i < solicitud.SeguroSocial.Length; i++)
                    {
                        if (i < 5)
                        {
                            encryptedSSNPartHF.Value += solicitud.SeguroSocial[i];
                        }

                        else
                            ssnLastSet += solicitud.SeguroSocial[i];
                    }

                    ssTxtBx.Text = string.Format("***-**-{0}", ssnLastSet);

                    emailTxtBx.Text = solicitud.Email;

                    bdayTxtBx.Text = solicitud.FechaNacimiento.ToShortDateString();

                    solicitud.LicenciaConducir = Usuario.DecryptWord(solicitud.LicenciaConducir);

                    decryptedDriversHF.Value = solicitud.LicenciaConducir;

                    for (int i = 0; i < solicitud.LicenciaConducir.Length; i++)
                    {
                        driversTxtBx.Text += "*";
                    }

                    puebloDDL.SelectedIndex = solicitud.Pueblo;

                    codigoTxtBx.Text = solicitud.CodigoPostal;

                    dirPostalTxtBx.Text = solicitud.DirrecionPostal;

                    puebloPostalDDL.SelectedIndex = solicitud.PuebloPostal;

                    codigoPostalPosTxtBx.Text = solicitud.CodigoPostalPostal;

                    celTxtBx.Text = solicitud.Celular;

                    telResiTxtBx.Text = solicitud.Telefono;

                    dirTxtBx.Text = solicitud.Dirrecion;

                    #endregion

                    #region Set referencias

                    LinkedList<ReferenciaDisponible> referencias = ReferenciasDisponiblesRepo.GetReferenciasByNumSolicitud(solicitud.ID.ToString());

                    #region Ref 1

                    cercanoTxt.Text = referencias.ElementAt(0).Nombre;

                    codigoRefTxtBx.Text = referencias.ElementAt(0).CodigoPostal;

                    parentescoRefDDL.SelectedValue = referencias.ElementAt(0).Parentesco;

                    telFamTxtBx.Text = referencias.ElementAt(0).Telefono;

                    dirRefTxtBx.Text = referencias.ElementAt(0).Direccion;

                    puebloRefDDL.SelectedIndex = referencias.ElementAt(0).Pueblo;

                    #endregion

                    #region Ref 2

                    cercano2TxtBx.Text = referencias.ElementAt(1).Nombre;

                    codigoRef2TxtBx.Text = referencias.ElementAt(1).CodigoPostal;

                    parentescoDDL.SelectedValue = referencias.ElementAt(1).Parentesco;

                    telFam2TxtBx.Text = referencias.ElementAt(1).Telefono;

                    dirRef2TxtBx.Text = referencias.ElementAt(1).Direccion;

                    puebloRef2DDL.SelectedIndex = referencias.ElementAt(1).Pueblo;

                    #endregion

                    #region Ref 3

                    cercanoRef3TxtBx.Text = referencias.ElementAt(2).Nombre;

                    codigoRef3TxtBx.Text = referencias.ElementAt(2).CodigoPostal;

                    parentescoRef3DDL.SelectedValue = referencias.ElementAt(2).Parentesco;

                    telFamRef3TxtBx.Text = referencias.ElementAt(2).Telefono;

                    dirRef3TxtBx.Text = referencias.ElementAt(2).Direccion;

                    puebloRef3DDL.SelectedIndex = referencias.ElementAt(2).Pueblo;

                    #endregion

                    #endregion

                    #region Set info Co-Solicitante

                    nombreCoTxtBx.Text = solicitud.NombreCo;

                    paternoCoTxtBx.Text = solicitud.ApellidoPaternoCo;

                    maternoCoTxtBx.Text = solicitud.ApellidoMaternoCo;

                    solicitud.SeguroSocialCo = string.IsNullOrEmpty(solicitud.SeguroSocialCo) ? string.Empty
                        : Usuario.DecryptWord(solicitud.SeguroSocialCo);

                    ssnLastSet = string.Empty;

                    for (int i = 0; i < solicitud.SeguroSocialCo.Length; i++)
                    {
                        if (i < 5)
                        {
                            ssEncryptedPartCoHF.Value += solicitud.SeguroSocialCo[i];
                        }

                        else
                            ssnLastSet += solicitud.SeguroSocialCo[i];
                    }

                    ssCoTxtBx.Text = string.IsNullOrEmpty(solicitud.SeguroSocialCo) ? string.Empty
                        : string.Format("***-**-{0}", ssnLastSet);

                    emailCoTxtBx.Text = solicitud.EmailCo;

                    bdayCoTxtBx.Text = solicitud.FechaNacimientoCo.Date.Equals(DateTime.MaxValue.Date)
                        ? string.Empty : solicitud.FechaNacimientoCo.ToShortDateString();

                    if (string.IsNullOrEmpty(solicitud.LicenciaConducirCo))
                    {
                        driversCoTxtBx.Text = string.Empty;
                    }

                    else
                    {
                        solicitud.LicenciaConducirCo = Usuario.DecryptWord(solicitud.LicenciaConducirCo);

                        decryptedDriversCoHF.Value = solicitud.LicenciaConducirCo;

                        for (int i = 0; i < solicitud.LicenciaConducirCo.Length; i++)
                        {
                            driversCoTxtBx.Text += "*";
                        }
                    }

                    puebloCoDDL.SelectedIndex = solicitud.PuebloCo;

                    codigoCoTxtBx.Text = solicitud.CodigoPostalCo;

                    dirCoPostalTxtbx.Text = solicitud.DirrecionPostalCo;

                    puebloCoPostalDDL.SelectedIndex = solicitud.PuebloPostalCo;

                    codigoCoPostalTxtBx.Text = solicitud.CodigoPostalPostalCo;

                    celCoTextBx.Text = solicitud.CelularCo;

                    telCoTxtBx.Text = solicitud.TelefonoCo;

                    dirCoTxtBx.Text = solicitud.DirrecionCo;

                    #endregion

                    #region Create and set doc recieved controls

                    docRecievedParentDiv.Visible = true;

                    docsAsociadosTableTag.Visible = true;

                    LinkedList<DocReq> docs = DocsReqRepo.GetDocsByNumSolicitud(numSolicitud);

                    Session["docsRequeridos"] = docs;

                    CreateDocRecievedCtrl(docs);

                    //Disable statuses if user updating
                    if (solicitud.Status != _Solicitud.Statuses.PEND_REVISAR)
                    {
                        LinkedList<DropDownList> docAsociadosStatuses = (LinkedList<DropDownList>)Session["docAsociadosStatuses"];

                        for (int i = 0; i < docAsociadosStatuses.Count; i++)
                        {
                            docAsociadosStatuses.ElementAt(i).Enabled = false;
                        }
                    }

                    #endregion 

                    #endregion
                }

                else
                {
                    #region Create

                    #region Set info Solicitante from User

                    nameTxtBx.Text = user.Nombre;

                    paternoTxtBx.Text = user.ApellidoPaterno;

                    maternoTxtBx.Text = user.ApellidoMaterno;

                    //ssTxtBx.Text = user.SeguroSocial;

                    emailTxtBx.Text = user.Email;

                    bdayTxtBx.Text = user.FechaNacimiento.ToShortDateString();

                    //driversTxtBx.Text = user.LicenciaConducir;

                    puebloDDL.SelectedIndex = user.Pueblo;

                    codigoTxtBx.Text = user.CodigoPostal;

                    dirPostalTxtBx.Text = user.DireccionPost;

                    puebloPostalDDL.SelectedIndex = user.PuebloPost;

                    codigoPostalPosTxtBx.Text = user.CodigoPostalPost;

                    celTxtBx.Text = user.Celular;

                    telResiTxtBx.Text = user.Tel;

                    dirTxtBx.Text = user.Direccion;

                    #endregion

                    ViewState["Status"] = _Solicitud.Statuses.NO_TRAMITADO;

                    #endregion
                }
            }

            else
            {
                #region Set doc controls on postback
                LinkedList<DocReq> docsRequeridos = (LinkedList<DocReq>)Session["docsRequeridos"];

                if (docsRequeridos != null)
                {
                    CreateDocRecievedCtrl(docsRequeridos);
                }
                #endregion
            }
        }

        protected void saveBtn_Click(object sender, EventArgs e)
        {
            Usuario user = Session["UserObj"] == null ? new Usuario() : (Usuario)Session["UserObj"];

            #region Instantiate solicitud

            _Solicitud solicitud = new _Solicitud()
            {
                FechaTramitada = DateTime.Now,

                Nombre = nameTxtBx.Text,

                //Join both encrypted sets and decrypted sets and encrypt for db
                SeguroSocial = Usuario.EncryptWord(string.Format("{0}{1}", encryptedSSNPartHF.Value,
                 ssTxtBx.Text.Replace("*", string.Empty).Replace("-", string.Empty))),

                Email = emailTxtBx.Text,

                ApellidoMaterno = maternoTxtBx.Text,

                ApellidoPaterno = paternoTxtBx.Text,

                FechaNacimiento = Convert.ToDateTime(bdayTxtBx.Text),

                Telefono = telResiTxtBx.Text,

                LicenciaConducir = Usuario.EncryptWord(decryptedDriversHF.Value),

                Celular = celTxtBx.Text,

                Dirrecion = dirTxtBx.Text,

                Pueblo = puebloDDL.SelectedIndex,

                CodigoPostal = codigoTxtBx.Text,

                DirrecionPostal = dirPostalTxtBx.Text,

                PuebloPostal = puebloPostalDDL.SelectedIndex,

                CodigoPostalPostal = codigoPostalPosTxtBx.Text,

                NombreCo = nombreCoTxtBx.Text,

                //Encrypt only if not empty
                SeguroSocialCo = string.IsNullOrEmpty(ssCoTxtBx.Text) ? string.Empty
                : Usuario.EncryptWord(string.Format("{0}{1}", ssEncryptedPartCoHF.Value,
                ssCoTxtBx.Text.Replace("*", string.Empty).Replace("-", string.Empty))),

                EmailCo = emailCoTxtBx.Text,

                ApellidoMaternoCo = maternoCoTxtBx.Text,

                ApellidoPaternoCo = paternoCoTxtBx.Text,

                FechaNacimientoCo = string.IsNullOrEmpty(bdayCoTxtBx.Text) ? DateTime.MaxValue
                : Convert.ToDateTime(bdayCoTxtBx.Text),

                TelefonoCo = telCoTxtBx.Text,

                //Encrypt only if not empty
                LicenciaConducirCo = string.IsNullOrEmpty(driversCoTxtBx.Text) ? string.Empty
                : Usuario.EncryptWord(decryptedDriversCoHF.Value),

                CelularCo = celCoTextBx.Text,

                DirrecionCo = dirCoTxtBx.Text,

                PuebloCo = puebloCoDDL.SelectedIndex,

                CodigoPostalCo = codigoCoTxtBx.Text,

                DirrecionPostalCo = dirCoPostalTxtbx.Text,

                PuebloPostalCo = puebloCoPostalDDL.SelectedIndex,

                CodigoPostalPostalCo = codigoCoPostalTxtBx.Text,
            };

            #endregion

            #region Instantiate referencias

            LinkedList<ReferenciaDisponible> referencias = new LinkedList<ReferenciaDisponible>();

            referencias.AddLast(new ReferenciaDisponible()
            {
                CodigoPostal = codigoRefTxtBx.Text,

                Direccion = dirRefTxtBx.Text,

                Nombre = cercanoTxt.Text,

                Parentesco = parentescoRefDDL.SelectedValue,

                Pueblo = puebloRefDDL.SelectedIndex,

                Telefono = telFamTxtBx.Text
            });

            referencias.AddLast(new ReferenciaDisponible()
            {
                CodigoPostal = codigoRef2TxtBx.Text,

                Direccion = dirRef2TxtBx.Text,

                Nombre = cercano2TxtBx.Text,

                Parentesco = parentescoDDL.SelectedValue,

                Pueblo = puebloRef2DDL.SelectedIndex,

                Telefono = telFam2TxtBx.Text
            });

            referencias.AddLast(new ReferenciaDisponible()
            {
                CodigoPostal = codigoRef3TxtBx.Text,

                Direccion = dirRef3TxtBx.Text,

                Nombre = cercanoRef3TxtBx.Text,

                Parentesco = parentescoRef3DDL.SelectedValue,

                Pueblo = puebloRef3DDL.SelectedIndex,

                Telefono = telFamRef3TxtBx.Text
            });

            #endregion

            string numSolicitud = Request.QueryString["numSolicitud"] == null ? string.Empty
                    : (string)Request.QueryString["numSolicitud"];

            if (string.IsNullOrEmpty(numSolicitud))
            {
                #region Create

                solicitud.Status = _Solicitud.Statuses.PEND_REVISAR;

                try
                {
                    #region Solicitud


                    Exception excep = SolicitudRepo.Create(solicitud);

                    if (excep != null)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, GetType(), "solicitudCreateAlert",
                        //        string.Format("alert('No se pudo someter la solicitud. Error Solicitud: {0}');",
                        //        excep.Message.Replace("'", string.Empty)), true);

                        throw new Exception(string.Format("No se pudo someter la solicitud. Error Solicitud: {0}"
                            , excep.Message.Replace("'", string.Empty)));
                    }

                    #endregion

                    #region Referencias

                    //Since numSolicitud is the same one as the id I'm assigning it after the id is created
                    solicitud.NumeroSolicitud = SolicitudRepo.GetLastNumSolicitudByEmail(solicitud.Email);

                    for (int i = 0; i < referencias.Count; i++)
                    {
                        referencias.ElementAt(i).NumeroSolicitud = solicitud.NumeroSolicitud;
                    }

                    #region Build num solicitud and update

                    //Update needs the id set
                    solicitud.ID = Convert.ToInt32(solicitud.NumeroSolicitud);

                    solicitud.NumeroSolicitud = string.Format("{0}{1}{2}-{3}", DateTime.Now.Date.Year,
                                DateTime.Now.Date.Month, DateTime.Now.Date.Day, solicitud.NumeroSolicitud.PadLeft(3, '0'));

                    excep = SolicitudRepo.Update(solicitud);

                    if (excep != null)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "solicitudUpdateReferenciaAlert",
                                string.Format("No se pudo actualizar la solicitud. Error Solicitud Update: {0}",
                                excep.Message.Replace("'", string.Empty)), true);

                        throw new Exception(excep.Message);
                    }

                    #endregion

                    string error = ReferenciasDisponiblesRepo.Create(referencias);

                    if (!string.IsNullOrEmpty(error))
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, GetType(), "referenciasCreateAlert",
                        //        string.Format("alert('No se pudo someter la solicitud. Error Referencias: {0}');",
                        //        error.Replace("'", string.Empty)), true);

                        throw new Exception(string.Format("No se pudo someter la solicitud. Error Referencias: {0}",
                                error.Replace("'", string.Empty)));
                    }

                    #endregion

                    #region Docs

                    #region Instantiate docs

                    LinkedList<DocReq> docs = new LinkedList<DocReq>();

                    LinkedList<FileUpload> docRequeridosFileUploads = (LinkedList<FileUpload>)Session["docRequeridosFileUpload"];

                    for (int i = 0; i < docRequeridosFileUploads.Count; i++)
                    {
                        docs.AddLast(new DocReq()
                        {
                            Nombre = docRequeridosFileUploads.ElementAt(i).ID.Replace("fileUp", string.Empty),

                            NumeroSolicitud = solicitud.NumeroSolicitud,

                            PathDoc = string.Format("{0}/{1}/{2}_{3}", DocReq.GetPathDocs(), user.Email
                                        , solicitud.NumeroSolicitud,
                                        Path.GetFileName(docRequeridosFileUploads.ElementAt(i).PostedFile.FileName.Replace(
                                        "#", string.Empty).Replace("&", string.Empty))),

                            EmailUser = user.Email,

                        });
                    }

                    #endregion

                    error = DocsReqRepo.Create(docs);

                    if (!string.IsNullOrEmpty(error))
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, GetType(), "docCreateAlert",
                        //        string.Format("alert('No se pudo someter la solicitud. Error Documentos: {0}');",
                        //        error.Replace("'", string.Empty)), true);

                        throw new Exception(string.Format("No se pudo someter la solicitud. Error Documentos: {0}",
                                error.Replace("'", string.Empty)));
                    }

                    SaveDocsTo(docRequeridosFileUploads, docs);

                    #endregion

                    #region Send email

                    LinkedList<Usuario> to = new LinkedList<Usuario>(),
                        coordinadores = UserRepo.GetUsersByRole((int)Rol.TiposRole.COORDINADOR),
                        supervisores = UserRepo.GetUsersByRole((int)Rol.TiposRole.SUPERVISOR);

                    #region User

                    to.AddLast(UserRepo.GetUserByEmail(solicitud.Email));

                    SendThreadedEmail(to, Correo.MailType.PEND_REVISAR_USER, solicitud);

                    #endregion

                    #region Employees

                    to = new LinkedList<Usuario>();

                    for (int i = 0; i < coordinadores.Count; i++)
                    {
                        to.AddLast(coordinadores.ElementAt(i));
                    }

                    //for (int i = 0; i < procesadores.Count; i++)
                    //{
                    //    to.AddLast(procesadores.ElementAt(i));
                    //}

                    for (int i = 0; i < supervisores.Count; i++)
                    {
                        to.AddLast(supervisores.ElementAt(i));
                    }

                    SendThreadedEmail(to, Correo.MailType.PEND_REVISAR, solicitud);

                    #endregion

                    #endregion

                    //Show num solicitud and redirect to main dashboard
                    ScriptManager.RegisterStartupScript(this, GetType(), "userMustWaitAlert",
                                           string.Format("WaitingAnswerAlert('Número Solicitud: {0}');", solicitud.NumeroSolicitud), true);
                }

                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "solicitudCreateAlert",
                        string.Format("alert('{0}');", ex.Message.Replace("\r\n", " ")), true);
                }

                #endregion
            }

            else
            {
                #region Edit                

                #region Set header info on solicitud obj from previous solicitud obj

                _Solicitud solicitudExistente = SolicitudRepo.GetSolicitudByNumSolicitud(numSolicitud);

                solicitud.Status = solicitudExistente.Status;

                solicitud.CoordinadorID = solicitudExistente.CoordinadorID;

                solicitud.FechaRevision = solicitudExistente.FechaRevision;

                solicitud.ComentarioProcesador = solicitudExistente.ComentarioProcesador;

                solicitud.ProcesadorId = solicitudExistente.ProcesadorId;

                solicitud.FechaAsigProcesador = solicitudExistente.FechaAsigProcesador;

                solicitud.FechaTrabajado = solicitudExistente.FechaTrabajado;

                #endregion

                #region Update solicitud and doc obj according to status, also setup email

                Correo.MailType mailType = Correo.MailType.PEND_REVISAR;

                LinkedList<Usuario> to = new LinkedList<Usuario>();

                switch (solicitud.Status)
                {
                    case _Solicitud.Statuses.PEND_REVISAR:

                        #region PEND_REVISAR

                        solicitud.CoordinadorID = coorDDL.SelectedIndex;

                        solicitud.FechaRevision = Convert.ToDateTime(fechaRevisadoLbl.Text.Split(':')[1]);

                        solicitud.ComentarioProcesador = asigComment.Text;

                        #region Set status and send email according to a doc status

                        LinkedList<DropDownList> docAsociadosStatuses = (LinkedList<DropDownList>)Session["docAsociadosStatuses"];

                        #region Update docs

                        #region Set statuses

                        LinkedList<DocReq> docsExistentes = DocsReqRepo.GetDocsByNumSolicitud(numSolicitud);

                        for (int i = 0; i < docsExistentes.Count; i++)
                        {
                            docsExistentes.ElementAt(i).Status = docAsociadosStatuses.ElementAt(i).SelectedIndex;
                        }

                        #endregion

                        string error = DocsReqRepo.Update(docsExistentes);

                        if (!string.IsNullOrEmpty(error))
                        {
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "docUpdateAlert",
                                    string.Format("alert('No se pudo someter la solicitud. Error Actualizar Documentos: {0}');",
                                    error.Replace("'", string.Empty)), true);

                            throw new Exception(error);
                        }

                        #endregion

                        bool IsIncomplete = false;

                        //Check doc status
                        for (int i = 0; i < docAsociadosStatuses.Count; i++)
                        {
                            if (docAsociadosStatuses.ElementAt(i).SelectedIndex == 0)
                            {
                                IsIncomplete = true;

                                break;
                            }
                        }

                        if (!IsIncomplete)
                        {
                            solicitud.Status = _Solicitud.Statuses.PEND_ASIGNAR;

                            mailType = Correo.MailType.PEND_ASIG_USER;

                            //to = UserRepo.GetUsersByRole((int)Usuario.TiposUsuarios.SUPERVISOR);
                        }

                        else
                        {
                            solicitud.Status = _Solicitud.Statuses.PEND_DOCS;

                            solicitud.FechaDocIncompleto = DateTime.Now;

                            mailType = Correo.MailType.DOC_INCOMPLETOS_USER;

                            //to = new LinkedList<Usuario>();

                            //to.AddLast(UserRepo.GetUserByEmail(solicitud.Email));
                        }
                        #endregion

                        #endregion

                        break;

                    case _Solicitud.Statuses.PEND_ASIGNAR:

                        #region PEND_ASIGNAR

                        solicitud.ProcesadorId = procesadoresDDL.SelectedIndex;

                        solicitud.FechaAsigProcesador = Convert.ToDateTime(fechaAsignadoLbl.Text.Split(':')[1]);

                        solicitud.Status = _Solicitud.Statuses.PEND_TRABAJAR;

                        mailType = Correo.MailType.PEND_TRABAJAR_USER;

                        //to = UserRepo.GetUsersByRole((int)Usuario.TiposUsuarios.PROCESADOR); 

                        #endregion

                        //Send notification that solicitud service is ready to be consumed

                        break;

                    case _Solicitud.Statuses.PEND_TRABAJAR:

                        switch(statusDDL.SelectedIndex)
                        {
                            //Denegada
                            case 0:

                                solicitud.Status = _Solicitud.Statuses.DENEGADA;

                                mailType = Correo.MailType.DENEGADA;

                                break;

                                //Aprovada
                            default:

                                solicitud.Status = _Solicitud.Statuses.APROBADA;

                                mailType = Correo.MailType.APROBADA;

                                break;
                        }

                        solicitud.FechaTrabajado = Convert.ToDateTime(fechaTrabajoLbl.Text.Split(':')[1]);

                        solicitud.ComentarioTrabajo = trabajoComment.Text;

                        //Send notification that solicitud service is ready to be consumed

                        break;

                    case _Solicitud.Statuses.PEND_DOCS:

                        #region PEND_DOCS

                        mailType = Correo.MailType.DOC_INCOMPLETOS_RESENT;

                        //to = new LinkedList<Usuario>();

                        //to.AddLast(UserRepo.GetUserByEmail(solicitud.Email));
                        //Save docs in file, update doc obj and doc db

                        #region Update docs obj

                        docAsociadosStatuses = (LinkedList<DropDownList>)Session["docAsociadosStatuses"];

                        docsExistentes = DocsReqRepo.GetDocsByNumSolicitud(numSolicitud);

                        LinkedList<FileUpload> docRequeridosFileUploads = (LinkedList<FileUpload>)Session["docRequeridosFileUpload"];

                        //Set docs obj
                        for (int i = 0; i < docRequeridosFileUploads.Count; i++)
                        {
                            docsExistentes.ElementAt(i).Nombre = docRequeridosFileUploads.ElementAt(i).ID.Replace("fileUp", string.Empty);

                            docsExistentes.ElementAt(i).PathDoc = string.Format("{0}/{1}/{2}_{3}", DocReq.GetPathDocs(), user.Email
                                            , solicitud.NumeroSolicitud,
                                            Path.GetFileName(docRequeridosFileUploads.ElementAt(i).PostedFile.FileName.Replace(
                                            "#", string.Empty).Replace("&", string.Empty)));

                            docsExistentes.ElementAt(i).EmailUser = user.Email;

                            docsExistentes.ElementAt(i).Status = docAsociadosStatuses.ElementAt(i).SelectedIndex;
                        }

                        #endregion

                        solicitud.Status = _Solicitud.Statuses.PEND_REVISAR;

                        solicitud.FechaDocIncompleto = DateTime.MaxValue;

                        error = DocsReqRepo.Update(docsExistentes);

                        if (!string.IsNullOrEmpty(error))
                        {
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "docUpdateAlert",
                                    string.Format("alert('No se pudo someter la solicitud. Error Actualizar Documentos: {0}');",
                                    error.Replace("'", string.Empty)), true);

                            throw new Exception(error);
                        }

                        SaveDocsTo(docRequeridosFileUploads, docsExistentes);

                        #endregion

                        break;
                }

                #endregion

                try
                {
                    #region Solicitud

                    solicitud.NumeroSolicitud = numSolicitud;

                    solicitud.ID = Convert.ToInt32(numSolicitud.Split('-')[1]);

                    Exception excep = SolicitudRepo.Update(solicitud);

                    if (excep != null)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, GetType(), "solicitudUpdateAlert",
                        //        string.Format("alert('No se pudo actualizar la solicitud. Error Actualizar Solicitud: {0}');",
                        //        excep.Message.Replace("'", string.Empty)), true);

                        throw new Exception(string.Format("No se pudo actualizar la solicitud. Error Actualizar Solicitud: {0}",
                                excep.Message.Replace("'", string.Empty)));
                    }

                    #region Send email

                    //It's here cause they didn't give me emails for internal users
                    to.AddLast(UserRepo.GetUserByEmail(solicitud.Email));

                    SendThreadedEmail(to, mailType, solicitud);

                    #region Also send email to employees

                    to = new LinkedList<Usuario>();

                    switch (mailType)
                    {
                        case Correo.MailType.DOC_INCOMPLETOS_RESENT:

                            LinkedList<Usuario> coordinadores = UserRepo.GetUsersByRole((int)Rol.TiposRole.COORDINADOR),
                                //procesadores = UserRepo.GetUsersByRole((int)Rol.TiposRole.PROCESADOR),
                                supervisores = UserRepo.GetUsersByRole((int)Rol.TiposRole.SUPERVISOR);

                            for (int i = 0; i < coordinadores.Count; i++)
                            {
                                to.AddLast(coordinadores.ElementAt(i));
                            }

                            //for (int i = 0; i < procesadores.Count; i++)
                            //{
                            //    to.AddLast(procesadores.ElementAt(i));
                            //}

                            for (int i = 0; i < supervisores.Count; i++)
                            {
                                to.AddLast(supervisores.ElementAt(i));
                            }

                            SendThreadedEmail(to, Correo.MailType.DOCS_INCOMPLETOS, solicitud);

                            break;

                        case Correo.MailType.PEND_ASIG_USER:

                            supervisores = UserRepo.GetUsersByRole((int)Rol.TiposRole.SUPERVISOR);

                            for (int i = 0; i < supervisores.Count; i++)
                            {
                                to.AddLast(supervisores.ElementAt(i));
                            }

                            SendThreadedEmail(to, Correo.MailType.PEND_ASIG, solicitud);

                            break;

                        case Correo.MailType.PEND_TRABAJAR_USER:

                            LinkedList<Usuario> procesadores = UserRepo.GetUsersByRole((int)Rol.TiposRole.PROCESADOR);

                            for (int i = 0; i < procesadores.Count; i++)
                            {
                                if (procesadores.ElementAt(i).GetNombreCompleto().Equals(procesadoresDDL.SelectedValue))
                                {
                                    to.AddLast(procesadores.ElementAt(i));

                                    break;
                                }
                            }

                            SendThreadedEmail(to, Correo.MailType.PEND_TRABAJAR, solicitud);

                            break;
                    }

                    #endregion

                    #endregion

                    #endregion

                    #region Referencias

                    LinkedList<ReferenciaDisponible> referenciasExistentes =
                        ReferenciasDisponiblesRepo.GetReferenciasByNumSolicitud(solicitud.ID.ToString());

                    for (int i = 0; i < referencias.Count; i++)
                    {
                        referencias.ElementAt(i).ID = referenciasExistentes.ElementAt(i).ID;
                    }

                    string _error = ReferenciasDisponiblesRepo.Update(referencias);

                    if (!string.IsNullOrEmpty(_error))
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, GetType(), "referenciasUpdateAlert",
                        //        string.Format("alert('No se pudo actualizar la solicitud. Error Actualizar Referencias: {0}');",
                        //        _error.Replace("'", string.Empty)), true);

                        throw new Exception(string.Format("No se pudo actualizar la solicitud. Error Actualizar Referencias: {0}",
                                _error.Replace("'", string.Empty)));
                    }

                    #endregion
                }

                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "solicitudCreateAlert",
                        string.Format("alert('{0}');", ex.Message.Replace("\r\n", " ")), true);
                }

                Response.Redirect("Main.aspx", true);

                #endregion
            }
        }

        /// <summary>
        /// Save docs on a specific location
        /// </summary>
        /// <param name="fileUploads"></param>
        /// <param name="docs"></param>
        private void SaveDocsTo(LinkedList<FileUpload> fileUploads, LinkedList<DocReq> docs)
        {
            for (int i = 0; i < docs.Count; i++)
            {
                fileUploads.ElementAt(i).SaveAs(Server.MapPath(docs.ElementAt(i).PathDoc));
            }
        }

        /// <summary>
        /// Injects controls to doc req section according to what's on the db
        /// </summary>
        /// <param name="numSolicitud"></param>
        private void CreateDocReqCtrls(string numSolicitud)
        {
            LinkedList<FileUpload> docRequeridosFileUploads = new LinkedList<FileUpload>();

            if (docReqDiv.Controls.Count > 0)
            {
                docReqDiv.Controls.Clear();
            }

            LinkedList<DocReq> docsRequeridos = DocsReqRepo.GetDocsNames();

            _Solicitud solicitud = SolicitudRepo.GetSolicitudByNumSolicitud(numSolicitud);

            //LinkedList<RequiredFieldValidator> _validators = new LinkedList<RequiredFieldValidator>();

            for (int i = 0; i < docsRequeridos.Count; i++)
            {
                HtmlInputFile upload = new HtmlInputFile();

                #region Create controls

                #region Lbl

                Label lbl = new Label();

                lbl.ID = docsRequeridos.ElementAt(i).Nombre + "Lbl";

                lbl.Text = docsRequeridos.ElementAt(i).Nombre;

                lbl.ForeColor = ColorTranslator.FromHtml("#79256E");

                #endregion

                #region FileUp

                FileUpload fileUp = new FileUpload();

                //fileUp.ID = docsRequeridos.ElementAt(i).Nombre.Replace(" ", string.Empty) + "fileUp";

                fileUp.ID = docsRequeridos.ElementAt(i).Nombre.Replace(" ", "_") + "fileUp";

                fileUp.ToolTip = "Presionar para anejar documento";

                #endregion

                #region Add controls to doc req div

                HtmlGenericControl div = new HtmlGenericControl("div"),
                    lblDiv = new HtmlGenericControl("div"),
                    rfvDiv = new HtmlGenericControl("div");

                lblDiv.Controls.Add(lbl);

                div.Attributes["style"] = "margin-bottom:20px; margin-left:auto; margin-right:auto; width:20%";

                div.Controls.Add(lblDiv);

                //div.Controls.Add(lbl);

                docRequeridosFileUploads.AddLast(fileUp);

                div.Controls.Add(fileUp);

                #region RFV

                RequiredFieldValidator docValidator = new RequiredFieldValidator();

                docValidator.ErrorMessage = "Requerido";

                docValidator.SetFocusOnError = true;

                docValidator.ForeColor = ColorTranslator.FromHtml("#CC0000");

                docValidator.Display = ValidatorDisplay.Dynamic;

                docValidator.ControlToValidate = fileUp.ClientID;

                if (solicitud.Status != _Solicitud.Statuses.PEND_DOCS
                    && solicitud.Status != _Solicitud.Statuses.NO_TRAMITADO)
                {
                    docValidator.Enabled = false;
                }

                #endregion

                rfvDiv.Controls.Add(docValidator);

                div.Controls.Add(rfvDiv);

                docReqDiv.Controls.Add(div);

                #endregion

                #endregion
            }

            Session["docRequeridosFileUpload"] = docRequeridosFileUploads;
        }

        /// <summary>
        /// Injects controls to doc recieved section according to docs req
        /// </summary>
        /// <param name="docs"></param>
        private void CreateDocRecievedCtrl(LinkedList<DocReq> docs)
        {
            LinkedList<DropDownList> docAsociadosStatuses = new LinkedList<DropDownList>();

            LinkedList<RequiredFieldValidator> docAsociadosValidators = new LinkedList<RequiredFieldValidator>();

            LinkedList<ImageButton> ImageButtons = new LinkedList<ImageButton>();

            LinkedList<LiteralControl> literals = new LinkedList<LiteralControl>(); //Is used to inject html into table cells

            HtmlTableRow htmlRow = new HtmlTableRow();

            HtmlTableCell htmlCell = new HtmlTableCell();

            LiteralControl uploadLit1 = new LiteralControl(); //Holds opening and closing divs for title, img and ddl

            for (int i = 0; i < docs.Count; i++)
            {
                #region Title

                htmlCell = new HtmlTableCell();

                htmlCell.Attributes["style"] = "padding-bottom:20px";

                literals.AddLast(new LiteralControl());

                //-1 cause count is always one number ahead
                literals.ElementAt(literals.Count - 1).Text = "<div style = 'text-align:center;'>";

                htmlCell.Controls.Add(literals.ElementAt(literals.Count - 1));

                uploadLit1.Text += "<div>";

                Label lbl = new Label();

                lbl.Text = docs.ElementAt(i).Nombre.Replace("_", " ");

                lbl.ForeColor = ColorTranslator.FromHtml("#79256E");

                htmlCell.Controls.Add(lbl);

                literals.AddLast(new LiteralControl());

                literals.ElementAt(literals.Count - 1).Text = "</div>";

                htmlCell.Controls.Add(literals.ElementAt(literals.Count - 1));

                uploadLit1.Text += "</div>";

                #endregion

                #region Image

                literals.AddLast(new LiteralControl());

                literals.ElementAt(literals.Count - 1).Text = "<div style = 'text-align:center;'>";

                htmlCell.Controls.Add(literals.ElementAt(literals.Count - 1));

                UpdatePanel updatePnl = new UpdatePanel();

                ImageButton imgBtn = new ImageButton();

                imgBtn.ID = docs.ElementAt(i).Nombre.Replace("(", string.Empty).Replace(")", string.Empty) + "ImgBtn";

                imgBtn.Width = new Unit("80%");

                imgBtn.Height = new Unit("80%");

                imgBtn.ImageUrl = docs.ElementAt(i).PathDoc;

                imgBtn.AlternateText = "Presionar esta area para revisar documento";

                imgBtn.OnClientClick = docs.ElementAt(i).Nombre.Replace("(", string.Empty).Replace(")", string.Empty).Replace(" ",
                    string.Empty) + "ImgClick();";



                imgBtn.CausesValidation = false;

                #region function used when imgbtn is clicked

                string script = @"<script>
                                          function " + docs.ElementAt(i).Nombre.Replace("(", string.Empty).Replace(")", string.Empty).Replace(" ",
                            string.Empty) + @"ImgClick() 
                                          {
                                            var img = document.getElementById('MainContent_" + imgBtn.ClientID + @"');

                                            var url = img.getAttribute('src');

                                            window.open(url, '_blank');
                                         }

                                </script>";

                #endregion

                ClientScript.RegisterClientScriptBlock(GetType(), docs.ElementAt(i).Nombre.Replace("(", string.Empty).Replace(")", string.Empty)
                    + "AsociadoScript", script);

                updatePnl.ContentTemplateContainer.Controls.Add(imgBtn);

                htmlCell.Controls.Add(updatePnl);

                literals.AddLast(new LiteralControl());

                literals.ElementAt(literals.Count - 1).Text = "</div>";

                htmlCell.Controls.Add(literals.ElementAt(literals.Count - 1));

                ImageButtons.AddLast(imgBtn);

                #endregion

                #region Status

                literals.AddLast(new LiteralControl());

                literals.ElementAt(literals.Count - 1).Text = "<div style = 'text-align:center;'>";

                htmlCell.Controls.Add(literals.ElementAt(literals.Count - 1));

                //docsAsociadosTableTag.Controls.Add(literals.ElementAt(literals.Count - 1));

                uploadLit1.Text += "<div>";

                Label statusLbl = new Label();

                statusLbl.Text = "Status*";

                statusLbl.ForeColor = ColorTranslator.FromHtml("#79256E");

                DropDownList statusDDL = new DropDownList();

                statusDDL.Items.Add("Incompleto");

                statusDDL.Items.Add("Completo");

                statusDDL.ID = docs.ElementAt(i).Nombre + "DDL";

                statusDDL.SelectedIndex = docs.ElementAt(i).Status;

                statusDDL.SelectedIndexChanged += new EventHandler(OnStatusDDLChanged);

                statusDDL.AutoPostBack = true;

                docAsociadosStatuses.AddLast(statusDDL);

                //RequiredFieldValidator statusRFV = new RequiredFieldValidator();

                //statusRFV.InitialValue = "Seleccionar";

                //statusRFV.ForeColor = Color.Tomato;

                //statusRFV.ControlToValidate = statusDDL.ID;

                //statusRFV.ErrorMessage = "Requerido";

                //statusRFV.Display = ValidatorDisplay.Dynamic;

                //docAsociadosValidators.AddLast(statusRFV);

                htmlCell.Controls.Add(statusLbl);

                htmlCell.Controls.Add(statusDDL);

                //htmlCell.Controls.Add(statusRFV);

                literals.AddLast(new LiteralControl());

                literals.ElementAt(literals.Count - 1).Text = "</div>";

                //Add literal to html cell
                htmlCell.Controls.Add(literals.ElementAt(literals.Count - 1));

                //Add html cell to html row
                htmlRow.Controls.Add(htmlCell);

                #endregion        

                if (i == docs.Count - 1)
                {
                    docsAsociadosTableTag.Controls.Add(htmlRow);
                }

                else if (htmlRow.Cells.Count > 2)
                {
                    docsAsociadosTableTag.Controls.Add(htmlRow);

                    htmlRow = new HtmlTableRow();
                }
            }

            Session["docAsociadosValidators"] = docAsociadosValidators;

            Session["docAsociadosStatuses"] = docAsociadosStatuses;

            Session["ImageButtons"] = ImageButtons;

            //ViewState["docAsociadosCommentTxtBxsIds"] = docAsociadosCommentTxtBxsIDs;

            //ViewState["docAsociadosCommentLblsIDs"] = docAsociadosCommentLblsIDs;
        }

        private void SendThreadedEmail(LinkedList<Usuario> _to, Correo.MailType mailType, _Solicitud solicitud)
        {
            #region Send email

            LinkedList<string> to = new LinkedList<string>();

            for (int i = 0; i < _to.Count; i++)
            {
                to.AddLast(_to.ElementAt(i).Email);
            }

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
                        exMail = mail.SendEmail(mail.ComposeEmail(to, Correo.GetSubject(mailType),
                            Correo.GetBody(mailType).Replace("@NUMSOLICITUD", solicitud.NumeroSolicitud).Replace(
                                "@COMMENTS", solicitud.ComentarioProcesador)));

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

        protected void OnStatusDDLChanged(object statusDDL, EventArgs e)
        {
            LinkedList<DropDownList> docAsociadosStatuses = (LinkedList<DropDownList>)Session["docAsociadosStatuses"];

            asigLbl.Text = asigLbl.Text.Replace("*", string.Empty);

            for (int i = 0; i < docAsociadosStatuses.Count; i++)
            {
                if (docAsociadosStatuses.ElementAt(i).SelectedIndex == 0)
                {
                    asigLbl.Text = string.Format("{0}*", asigLbl.Text);

                    asigCommentRFV.Enabled = true;

                    break;
                }

                else
                {
                    asigCommentRFV.Enabled = false;
                }
            }
        }
    }
}