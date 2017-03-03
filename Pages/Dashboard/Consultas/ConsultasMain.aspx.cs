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

namespace PSO.Pages.Dashboard.Consultas
{
    public partial class ConsultasMain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Verify role access

            Usuario user = Session["UserObj"] == null ? new Usuario() : (Usuario)Session["UserObj"];

            if (!user.Role.ViewConsuCoor && !user.Role.ViewConsuProc
                && !user.Role.ViewConsuPendAsig && !user.Role.ViewConsuSolicitud)
            {
                Response.Redirect("~/Pages/Dashboard/Main.aspx", true);
            }

            CoordinadorBtn.Visible = user.Role.ViewConsuCoor;

            pendAsigProBtn.Visible = user.Role.ViewConsuPendAsig;

            processBtn.Visible = user.Role.ViewConsuProc;

            servSSNomStatusBtn.Visible = user.Role.ViewConsuSolicitud;

            #endregion

            Cosmetic cosmetic = (Cosmetic)Session["Cosmetic"];

            CoordinadorBtn.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            CoordinadorBtn.Text = cosmetic.ConsultaCoorTitle;

            pendAsigProBtn.Text = cosmetic.ConsultaSuperTitle;

            processBtn.Text = cosmetic.ConsultaProcTitle;

            servSSNomStatusBtn.Text = cosmetic.ConsultaSolicitudTitle;

            pendAsigProBtn.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            processBtn.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            servSSNomStatusBtn.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            #region Breadcrumb setup

            var dashboardPnl = (Panel)Master.FindControl("dashboardLinkPnl");

            dashboardPnl.Controls.Clear();

            HyperLink mainDashLink = new HyperLink();

            mainDashLink.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            mainDashLink.ID = "mainDashLink";

            mainDashLink.NavigateUrl = "~/Pages/Dashboard/Main.aspx";

            mainDashLink.Text = "Inicio";

            dashboardPnl.Controls.Add(mainDashLink);

            #endregion            
        }

        /// <summary>
        /// Runs only on the fake timeout interval
        /// </summary>
        /// <param name="lockedId"></param>
        [System.Web.Services.WebMethod]
        public static void ReleaseAllLkdSolicitudes(int lockedId)
        {
            SolicitudRepo.ReleaseAllLockedSolicitudes(lockedId);
        }

        /// <summary>
        /// Runs only on the fake timeout interval
        /// </summary>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static string KeepAlive()
        {
            return DateTime.Now.ToShortDateString();
        }

        protected void CoordinadorBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Dashboard/Consultas/ConsultaCoordinador.aspx", true);
        }

        protected void pendAsigProBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Dashboard/Consultas/ConsultaPendAsigPro.aspx", true);
        }

        protected void processBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Dashboard/Consultas/ConsultaProcesador.aspx", true);
        }

        protected void servSSNomStatusBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Dashboard/Consultas/ConsultaServSSNomStatus.aspx", true);
        }
    }
}