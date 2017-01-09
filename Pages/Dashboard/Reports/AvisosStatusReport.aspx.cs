using System;
using PSO.Entities;
using PSO.Repositorios;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PSO.Pages.Dashboard.Reports
{
    public partial class AvisosStatusReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Breadcrumb config

            var dashboardPnl = (Panel)Master.FindControl("dashboardLinkPnl");

            //Clear so that it can hold a new instance of links
            dashboardPnl.Controls.Clear();

            HyperLink mainDashLink = new HyperLink(),
                secondDashLink = new HyperLink();

            Label secondDashlbl = new Label();

            #region 1st link
            mainDashLink.NavigateUrl = "~/Pages/Dashboard/Main.aspx";

            mainDashLink.Text = "Inicio";

            dashboardPnl.Controls.Add(mainDashLink);
            #endregion

            #region 2nd link
            secondDashlbl.Text = " > ";

            dashboardPnl.Controls.Add(secondDashlbl);

            secondDashLink.NavigateUrl = "~/Pages/Dashboard/Reports/ReportsMain.aspx";

            secondDashLink.Text = "Reportes";

            dashboardPnl.Controls.Add(secondDashLink);
            #endregion

            #endregion

            if(!IsPostBack)
            {
                #region Verify role permission

                Usuario user = Session["UserObj"] == null ? new Usuario() : (Usuario)Session["UserObj"];

                if (!user.Role.ViewRepAvisosStatus)
                {
                    if (string.IsNullOrEmpty(user.Email))
                        Response.Redirect("~/Pages/Login.aspx", true);

                    Response.Redirect("~/Pages/Dashboard/Main.aspx", true);
                }

                #endregion

                #region Define and Init counters

                int aprovadosCount = SolicitudRepo.GetSolicitudesByStatus((int)_Solicitud.Statuses.APROBADA).Count,
                            denegadasCount = SolicitudRepo.GetSolicitudesByStatus((int)_Solicitud.Statuses.DENEGADA).Count,
                            pendCoorCount = SolicitudRepo.GetSolicitudesByStatus((int)_Solicitud.Statuses.PEND_REVISAR).Count,
                            pendAsigCount = SolicitudRepo.GetSolicitudesByStatus((int)_Solicitud.Statuses.PEND_ASIGNAR).Count,
                            pendTrabajarCount = SolicitudRepo.GetSolicitudesByStatus((int)_Solicitud.Statuses.PEND_TRABAJAR).Count,
                            docIncompleteCount = SolicitudRepo.GetSolicitudesByStatus((int)_Solicitud.Statuses.PEND_DOCS).Count,
                            totalSolicitudes = aprovadosCount + denegadasCount + docIncompleteCount + pendAsigCount + 
                            pendCoorCount + pendTrabajarCount;

                #endregion

                totalAvisosLbl.Text = string.Format("Total de Solicitudes Recibidas: {0}", totalSolicitudes);

                SetChartData(aprovadosCount, pendCoorCount, pendAsigCount, pendTrabajarCount, docIncompleteCount, denegadasCount);
            }

        }

        private void SetChartData(int aprobadasCount, int pendientesCount, int asigInspectorCount, int procesosInpeccionCount
            , int docsIncompletosCount, int denegadasCount)
        {
            #region pend revisar

            if (pendientesCount > 0)
            {
                Chart1.Series[0].Points[0].SetValueY(pendientesCount);

                Chart1.Series[0].Points[0].Label = pendientesCount.ToString();
            }

            #endregion

            #region docs incomplete

            if (docsIncompletosCount > 0)
            {
                Chart1.Series[0].Points[1].SetValueY(docsIncompletosCount);

                Chart1.Series[0].Points[1].Label = docsIncompletosCount.ToString();
            }

            #endregion

            #region Asig proc

            if (asigInspectorCount > 0)
            {
                Chart1.Series[0].Points[2].Label = asigInspectorCount.ToString();

                Chart1.Series[0].Points[2].SetValueY(asigInspectorCount);
            }

            #endregion

            #region pend trabajar

            if (procesosInpeccionCount > 0)
            {
                Chart1.Series[0].Points[3].Label = procesosInpeccionCount.ToString();

                Chart1.Series[0].Points[3].SetValueY(procesosInpeccionCount);
            }

            #endregion

            #region Aprobada

            if (aprobadasCount > 0)
            {
                Chart1.Series[0].Points[4].SetValueY(aprobadasCount);

                Chart1.Series[0].Points[4].Label = aprobadasCount.ToString();
            }

            #endregion

            #region Denegada

            if (denegadasCount > 0)
            {
                Chart1.Series[0].Points[5].SetValueY(denegadasCount);

                Chart1.Series[0].Points[5].Label = denegadasCount.ToString();
            }

            #endregion
        }
    }
}