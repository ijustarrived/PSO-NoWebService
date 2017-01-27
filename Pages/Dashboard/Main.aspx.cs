using PSO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PSO.Pages.Dashboard
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var dashboardPnl = (Panel)Master.FindControl("dashboardLinkPnl");

                dashboardPnl.Controls.Clear();

                Usuario user = Session["UserObj"] == null ? new Usuario() : (Usuario)Session["UserObj"];

                #region Check if user is logged

                if (string.IsNullOrEmpty(user.Email))
                    Response.Redirect("~/Pages/Login.aspx", true);

                #endregion

                #region Set btns visibity according to role permission

                if (!user.Role.ViewSolicitud)
                {
                    solicitudImgBtn.Visible = false;
                }

                if (!user.Role.ViewRepAvisosStatus)
                {
                    dashBtn.Visible = false;
                }

                if (!user.Role.ViewConsuCoor && !user.Role.ViewConsuPendAsig
                            && !user.Role.ViewConsuProc && !user.Role.ViewConsuSolicitud)
                {
                    consultaImgBtn.Visible = false;
                }

                if (!user.Role.ViewConfigDocReq && !user.Role.ViewConfigRole && !user.Role.ViewConfigUser)
                {
                    configImgBtn.Visible = false;
                }

                if (!user.Role.ViewRepProduc && !user.Role.ViewRepRecVsPen)
                {
                    reportImgBtn.Visible = false;
                }

                //if (!user.Role.ViewRepAvisosStatus && !user.Role.ViewRepRecVsPen)
                //{
                //    reportImgBtn.Visible = false;
                //} 

                #endregion
            }
        }

        protected void solicitudBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Dashboard/Solicitud.aspx", true);
        }

        protected void consultaBtn_Click(object sender, EventArgs e)
        {
            Usuario user = Session["UserObj"] == null ? new Usuario() : (Usuario)Session["UserObj"];

            if (user.Role.RoleType == Rol.TiposRole.EXTERNO)
                Response.Redirect("~/Pages/Dashboard/Consultas/ConsultaServSSNomStatus.aspx", true);

            Response.Redirect("~/Pages/Dashboard/Consultas/ConsultasMain.aspx", true);
        }

        protected void reportBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Dashboard/Reports/ReportsMain.aspx", true);
        }

        protected void configBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Dashboard/Configs/ConfigsMain.aspx", true);
        }

        protected void accountBtn_Click(object sender, EventArgs e)
        {
            Usuario user = Session["UserObj"] == null ? new Usuario() : (Usuario)Session["UserObj"];

            Response.Redirect(string.Format("~/Pages/Register.aspx?Email={0}", user.Email), true);
        }

        protected void logoutBtn_Click(object sender, EventArgs e)
        {
            if (Session["UserObj"] != null)
                Session.Remove("UserObj");

            Response.Redirect("~/Pages/Login.aspx", true);
        }

        protected void dashBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Dashboard/DashBrds/DashMain.aspx", true);
        }
    }
}