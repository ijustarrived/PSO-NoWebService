using System;
using PSO.Entities;
using PSO.Repositorios;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PSO.Pages.Dashboard.Configs
{
    public partial class RoleEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario user = Session["UserObj"] == null ? new Usuario() : (Usuario)Session["UserObj"];

            #region Verify role permission

            if (!user.Role.ViewConfigRole)
            {
                if (string.IsNullOrEmpty(user.Email))
                    Response.Redirect("~/Pages/Login.aspx", true);

                Response.Redirect("~/Pages/Dashboard/Main.aspx", true);
            }

            #endregion

            #region Breadcrumb config

            var dashboardPnl = (Panel)Master.FindControl("dashboardLinkPnl");

            //Clear so that it can hold a new instance of links
            dashboardPnl.Controls.Clear();

            HyperLink mainDashLink = new HyperLink(),
                secondDashLink = new HyperLink(),
                thirdDashLink = new HyperLink();

            Label secondDashlbl = new Label(),
                thirdDashlbl = new Label();

            #region 1st link
            mainDashLink.NavigateUrl = "~/Pages/Dashboard/Main.aspx";

            mainDashLink.Text = "Inicio";

            dashboardPnl.Controls.Add(mainDashLink);
            #endregion

            #region 2nd link
            secondDashlbl.Text = " > ";

            dashboardPnl.Controls.Add(secondDashlbl);

            secondDashLink.NavigateUrl = "~/Pages/Dashboard/Configs/ConfigsMain.aspx";

            secondDashLink.Text = "Configuraci&oacute;n";

            dashboardPnl.Controls.Add(secondDashLink);
            #endregion

            #region 3rd link
            thirdDashlbl.Text = " > ";

            dashboardPnl.Controls.Add(thirdDashlbl);

            thirdDashLink.NavigateUrl = "~/Pages/Dashboard/Configs/RolesConfig.aspx";

            thirdDashLink.Text = "Roles";

            dashboardPnl.Controls.Add(thirdDashLink);
            #endregion

            #endregion                      

            if (!IsPostBack)
            {
                if (Request.QueryString["RoleID"] != null)
                {
                    roleLbl.Text = Rol.GetRoleType(Convert.ToInt32(Request.QueryString["RoleID"])).ToString();

                    #region Set role

                    Rol role = RoleRepo.GetRoleByType(Convert.ToInt32(Request.QueryString["RoleID"]));

                    viewConfigDocReqChkBx.Checked = role.ViewConfigDocReq;

                    viewConfigRoleChkBx.Checked = role.ViewConfigRole;

                    viewConfigUserChkBx.Checked = role.ViewConfigUser;

                    viewConsuCoorChkBx.Checked = role.ViewConsuCoor;

                    viewConsuPenAsigChkBx.Checked = role.ViewConsuPendAsig;

                    viewConsuProcChkBx.Checked = role.ViewConsuProc;

                    viewConsuSolicitudesChkBx.Checked = role.ViewConsuSolicitud;

                    viewReportAvisoStatusChkBx.Checked = role.ViewRepAvisosStatus;

                    viewReportReciVsProcChkBx.Checked = role.ViewRepRecVsPen;

                    viewSolicitudChkBx.Checked = role.ViewSolicitud;

                    editDocReqChkBx.Checked = role.EditDocReq;

                    editRoleChkBx.Checked = role.EditRoles;

                    editUsersChkBx.Checked = role.EditUsers;

                    viewReportProdu.Checked = role.ViewRepProduc;

                    #endregion
                }

                else
                {
                    Response.Redirect("~/Pages/Dashboard/Main.aspx", true);
                }
            }
        }

        protected void saveBtn_Click(object sender, EventArgs e)
        {
            #region Init Role

            Rol role = new Rol()
            {
                RoleType = Rol.GetRoleType(Convert.ToInt32(Request.QueryString["RoleID"])),

                EditDocReq = editDocReqChkBx.Checked,

                EditRoles = editRoleChkBx.Checked,

                EditUsers = editUsersChkBx.Checked,

                ViewConfigDocReq = viewConfigDocReqChkBx.Checked,

                ViewConfigRole = viewConfigRoleChkBx.Checked,

                ViewConfigUser = viewConfigUserChkBx.Checked,

                ViewConsuCoor = viewConsuCoorChkBx.Checked,

                ViewConsuPendAsig = viewConsuPenAsigChkBx.Checked,

                ViewConsuProc = viewConsuProcChkBx.Checked,

                ViewConsuSolicitud = viewConsuSolicitudesChkBx.Checked,

                ViewRepAvisosStatus = viewReportAvisoStatusChkBx.Checked,

                ViewRepRecVsPen = viewReportReciVsProcChkBx.Checked,

                ViewSolicitud = viewSolicitudChkBx.Checked,

                ViewRepProduc = viewReportProdu.Checked
            };

            #endregion

            #region Update

            try
            {
                Exception excep = RoleRepo.Update(role);

                if (excep != null)
                {
                    //ClientScript.RegisterStartupScript(this.GetType(), "roleEditAlert",
                    //string.Format("alert('No se pudo actualizar el rol. Error Rol: {0}');", excep.Message), true);

                    throw new Exception(string.Format("No se pudo actualizar el rol. Error Rol: {0}", excep.Message));
                }

                #region Update current user

                Usuario user = Session["UserObj"] == null ? new Usuario() : (Usuario)Session["UserObj"];

                if (role.RoleType == user.Role.RoleType)
                    user.Role = role; 

                #endregion
            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "roleEditAlert",
                        string.Format("alert('{0}');", ex.Message.Replace("\r\n", " ")), true);
            } 

            #endregion

            Response.Redirect("RolesConfig.aspx", true);            
        }
    }
}