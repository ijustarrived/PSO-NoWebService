using PSO.Entities;
using PSO.Repositorios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace PSO.Pages.Dashboard.DashBrds
{
    public partial class DashMain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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

            #region Set Cosmetics

            Cosmetic cosmetic = (Cosmetic)Session["Cosmetic"];

            #region Set titles

            #region Set section title color

            historialCompletadasTitleDiv.Style.Add("background-color", cosmetic.TitleBackColor);

            historialRecibidasTitleDiv.Style.Add("background-color", cosmetic.TitleBackColor);

            randoDiasTitleDiv.Style.Add("background-color", cosmetic.TitleBackColor);

            solicitudesStatusTitleDiv.Style.Add("background-color", cosmetic.TitleBackColor);

            #endregion

            #region Set text

            rangoDiasTitleLbl.Text = cosmetic.CompletadasMesDiasTitle;

            historialCompletadasTitleLbl.Text = cosmetic.HistoryCompletadasTitle;

            historialRecibidasTitleLbl.Text = cosmetic.HistoryRecibidasTitle;

            SolicitudStatusTitleLbl.Text = cosmetic.SolicitudesStatusTitle;

            #endregion

            #endregion

            #region Set lbls

            totalAvisosLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            ScriptManager.RegisterStartupScript(this, GetType(), "InvokeChangeClinetSideColors",
                                    string.Format("ChangeClinetSideColors('{0}', '{1}');",
                                    cosmetic.LabelForeColor, cosmetic.TitleBackColor), true);

            #endregion

            #endregion

            #region Breadcrumb setup

            var dashboardPnl = (Panel)Master.FindControl("dashboardLinkPnl");

            dashboardPnl.Controls.Clear();

            HyperLink mainDashLink = new HyperLink();

            mainDashLink.ID = "mainDashLink";

            mainDashLink.NavigateUrl = "~/Pages/Dashboard/Main.aspx";

            mainDashLink.Text = "Inicio";

            dashboardPnl.Controls.Add(mainDashLink);

            mainDashLink.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            #endregion

            LinkedList<_Solicitud> solicitudesByYear = SolicitudRepo.GetSolicitudesByYear(DateTime.Now.Year),
                solicitudesByYearNRol = SolicitudRepo.GetSolicitudesCompletadasByYearNRol(DateTime.Now.Year, Rol.TiposRole.PROCESADOR),
                solicitudesByYearMonthNRol = SolicitudRepo.GetSolicitudesCompletadasByMonthYearNRol(DateTime.Now.Year,
                monthDDL.SelectedIndex + 1, Rol.TiposRole.PROCESADOR);

            SetByYearChartData(solicitudesByYear, historialRecibidasChrt);

            SetByYearChartData(solicitudesByYearNRol, historialCompletadasChrt);

            SetDayChartData(solicitudesByYearMonthNRol);

            #region Define and Init counters

            int aprovadosCount = SolicitudRepo.GetSolicitudesByStatus((int)_Solicitud.Statuses.APROBADA).Count,
                        denegadasCount = SolicitudRepo.GetSolicitudesByStatus((int)_Solicitud.Statuses.DENEGADA).Count,
                        pendCoorCount = SolicitudRepo.GetSolicitudesByStatus((int)_Solicitud.Statuses.PEND_REVISAR).Count,
                        pendAsigCount = SolicitudRepo.GetSolicitudesByStatus((int)_Solicitud.Statuses.PEND_ASIGNAR).Count,
                        pendTrabajarCount = SolicitudRepo.GetSolicitudesByStatus((int)_Solicitud.Statuses.PEND_TRABAJAR).Count,
                        docIncompleteCount = SolicitudRepo.GetSolicitudesByStatus((int)_Solicitud.Statuses.PEND_DOCS).Count,
                        inactivasCount = SolicitudRepo.GetSolicitudesByStatus((int)_Solicitud.Statuses.INACTIVO).Count,
                        totalSolicitudes = aprovadosCount + denegadasCount + docIncompleteCount + pendAsigCount +
                        pendCoorCount + pendTrabajarCount + inactivasCount;

            #endregion

            totalAvisosLbl.Text = string.Format("Total de Solicitudes Recibidas: {0}", totalSolicitudes);

            SetPieChartData(aprovadosCount, pendCoorCount, pendAsigCount, pendTrabajarCount
                , docIncompleteCount, denegadasCount, inactivasCount);
        }

        private void SetPieChartData(int aprobadasCount, int pendientesCount, int asigInspectorCount,
            int procesosInpeccionCount, int docsIncompletosCount, int denegadasCount, int inactivasCount)
        {
            //Chart1.Series[0]["BarLabelStyle"] = "Right";

            #region pend revisar

            if (pendientesCount > 0)
            {
                Chart1.Series[0].Points[0].SetValueY(pendientesCount);

                Chart1.Series[0].Points[0].Label = pendientesCount.ToString();

                Chart1.Series[0].Points[0].ToolTip = string.Format("{0} : {1}", Chart1.Series[0].Points[0].LegendText,
                    pendientesCount);
            }

            #endregion

            #region docs incomplete

            if (docsIncompletosCount > 0)
            {
                Chart1.Series[0].Points[1].SetValueY(docsIncompletosCount);

                Chart1.Series[0].Points[1].Label = docsIncompletosCount.ToString();

                Chart1.Series[0].Points[1].ToolTip = string.Format("{0} : {1}", Chart1.Series[0].Points[1].LegendText,
                    docsIncompletosCount);
            }

            #endregion

            #region Asig proc

            if (asigInspectorCount > 0)
            {
                Chart1.Series[0].Points[2].Label = asigInspectorCount.ToString();

                Chart1.Series[0].Points[2].SetValueY(asigInspectorCount);

                Chart1.Series[0].Points[2].ToolTip = string.Format("{0} : {1}", Chart1.Series[0].Points[2].LegendText,
                    asigInspectorCount);
            }

            #endregion

            #region pend trabajar

            if (procesosInpeccionCount > 0)
            {
                Chart1.Series[0].Points[3].Label = procesosInpeccionCount.ToString();

                Chart1.Series[0].Points[3].SetValueY(procesosInpeccionCount);

                Chart1.Series[0].Points[3].ToolTip = string.Format("{0} : {1}", Chart1.Series[0].Points[3].LegendText,
                    procesosInpeccionCount);
            }

            #endregion

            #region Aprobada

            if (aprobadasCount > 0)
            {
                Chart1.Series[0].Points[4].SetValueY(aprobadasCount);

                Chart1.Series[0].Points[4].Label = aprobadasCount.ToString();

                Chart1.Series[0].Points[4].ToolTip = string.Format("{0} : {1}", Chart1.Series[0].Points[4].LegendText,
                    aprobadasCount);
            }

            #endregion

            #region Denegada

            if (denegadasCount > 0)
            {
                Chart1.Series[0].Points[5].SetValueY(denegadasCount);

                Chart1.Series[0].Points[5].Label = denegadasCount.ToString();

                Chart1.Series[0].Points[5].ToolTip = string.Format("{0} : {1}", Chart1.Series[0].Points[5].LegendText,
                    denegadasCount);
            }

            #endregion

            #region Inactivas

            if (inactivasCount > 0)
            {
                Chart1.Series[0].Points[6].SetValueY(inactivasCount);

                Chart1.Series[0].Points[6].Label = inactivasCount.ToString();

                Chart1.Series[0].Points[6].ToolTip = string.Format("{0} : {1}", Chart1.Series[0].Points[6].LegendText,
                    inactivasCount);
            }

            #endregion
        }

        private void SetByYearChartData(LinkedList<_Solicitud> solicitudesByYear, Chart chart)
        {
            #region Define counters

            int eneCount = 0,
                    febCount = 0,
                    marCount = 0,
                    abrCount = 0,
                    mayCount = 0,
                    junCount = 0,
                    julCount = 0,
                    agoCount = 0,
                    sepCount = 0,
                    octCount = 0,
                    novCount = 0,
                    dicCount = 0;

            #endregion

            for (int i = 0; i < solicitudesByYear.Count; i++)
            {
                switch (solicitudesByYear.ElementAt(i).FechaTramitada.Month)
                {
                    case 1:

                        eneCount++;

                        break;

                    case 2:

                        febCount++;

                        break;

                    case 3:

                        marCount++;

                        break;

                    case 4:

                        abrCount++;

                        break;

                    case 5:

                        mayCount++;

                        break;

                    case 6:

                        junCount++;

                        break;

                    case 7:

                        julCount++;

                        break;

                    case 8:

                        agoCount++;

                        break;

                    case 9:

                        sepCount++;

                        break;

                    case 10:

                        octCount++;

                        break;

                    case 11:

                        novCount++;

                        break;

                    case 12:

                        dicCount++;

                        break;
                }
            }

            #region Ene

            if (eneCount > 0)
            {
                chart.Series[0].Points[0].SetValueY(eneCount);

                chart.Series[0].Points[0].Label = eneCount.ToString();
            }

            #endregion

            #region Feb

            if (febCount > 0)
            {
                chart.Series[0].Points[1].SetValueY(febCount);

                chart.Series[0].Points[1].Label = febCount.ToString();
            }

            #endregion

            #region Mar

            if (marCount > 0)
            {
                chart.Series[0].Points[2].SetValueY(marCount);

                chart.Series[0].Points[2].Label = marCount.ToString();
            }

            #endregion

            #region Abr

            if (abrCount > 0)
            {
                chart.Series[0].Points[3].SetValueY(abrCount);

                chart.Series[0].Points[3].Label = abrCount.ToString();
            }

            #endregion

            #region May

            if (mayCount > 0)
            {
                chart.Series[0].Points[4].SetValueY(mayCount);

                chart.Series[0].Points[4].Label = mayCount.ToString();
            }

            #endregion

            #region Jun

            if (junCount > 0)
            {
                chart.Series[0].Points[5].SetValueY(junCount);

                chart.Series[0].Points[5].Label = junCount.ToString();
            }

            #endregion

            #region Jul

            if (julCount > 0)
            {
                chart.Series[0].Points[6].SetValueY(julCount);

                chart.Series[0].Points[6].Label = julCount.ToString();
            }

            #endregion

            #region Ago

            if (agoCount > 0)
            {
                chart.Series[0].Points[7].SetValueY(agoCount);

                chart.Series[0].Points[7].Label = agoCount.ToString();
            }

            #endregion

            #region Sep

            if (sepCount > 0)
            {
                chart.Series[0].Points[8].SetValueY(sepCount);

                chart.Series[0].Points[8].Label = sepCount.ToString();
            }

            #endregion

            #region Oct

            if (octCount > 0)
            {
                chart.Series[0].Points[9].SetValueY(octCount);

                chart.Series[0].Points[9].Label = octCount.ToString();
            }

            #endregion

            #region Nov

            if (novCount > 0)
            {
                chart.Series[0].Points[10].SetValueY(novCount);

                chart.Series[0].Points[10].Label = novCount.ToString();
            }

            #endregion

            #region Dic

            if (dicCount > 0)
            {
                chart.Series[0].Points[11].SetValueY(dicCount);

                chart.Series[0].Points[11].Label = dicCount.ToString();
            }

            #endregion
        }

        private void SetDayChartData(LinkedList<_Solicitud> solicitudes)
        {
            //-1 cause I can't tell between 0 as in the difference is 0 and the initial defined value
            int zeroToTwoCount = -1,
                threeToFiveCount = 0,
                sixToEightCount = 0,
                nineAndAboveCount = 0;

            for (int i = 0; i < solicitudes.Count; i++)
            {
                int dateDiff = (solicitudes.ElementAt(i).FechaTrabajado - solicitudes.ElementAt(i).FechaTramitada).Days;

                if (dateDiff > 8)
                    nineAndAboveCount++;

                else if (dateDiff > 5)
                    sixToEightCount++;

                else if (dateDiff > 2)
                    threeToFiveCount++;

                else
                    zeroToTwoCount++;

                //if (dateDiff < 3)
                //    zeroToTwoCount++;

                //else if (dateDiff < 6)
                //    threeToFiveCount++;

                //else if (dateDiff < 9)
                //    sixToEightCount++;

                //else if (dateDiff >= 9)
                //    nineAndAboveCount++;
            }

            #region 0-2

            if (zeroToTwoCount < 0)
                zeroToTwoCount = 0;

            /*
             * Si se le sumo una vez, va a terminar en 0 por que empieza desde -1.
             * +1 para que caiga en el numero que es
             */
            else
                zeroToTwoCount++;

            dateRangeChrt.Series[0].Points[3].SetValueY(zeroToTwoCount);

            dateRangeChrt.Series[0].Points[3].Label = zeroToTwoCount.ToString();

            #endregion

            #region 3-5

            dateRangeChrt.Series[0].Points[2].SetValueY(threeToFiveCount);

            dateRangeChrt.Series[0].Points[2].Label = threeToFiveCount.ToString();

            #endregion

            #region 6-8

            dateRangeChrt.Series[0].Points[1].SetValueY(sixToEightCount);

            dateRangeChrt.Series[0].Points[1].Label = sixToEightCount.ToString();

            #endregion

            #region 9 and above

            dateRangeChrt.Series[0].Points[0].SetValueY(nineAndAboveCount);

            dateRangeChrt.Series[0].Points[0].Label = nineAndAboveCount.ToString();

            #endregion
        }
    }
}