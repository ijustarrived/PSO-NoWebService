using PSO.Entities;
using PSO.Repositorios;
using System;
using System.Collections.Generic;
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

            #region Breadcrumb setup

            var dashboardPnl = (Panel)Master.FindControl("dashboardLinkPnl");

            dashboardPnl.Controls.Clear();

            HyperLink mainDashLink = new HyperLink();

            mainDashLink.ID = "mainDashLink";

            mainDashLink.NavigateUrl = "~/Pages/Dashboard/Main.aspx";

            mainDashLink.Text = "Inicio";

            dashboardPnl.Controls.Add(mainDashLink);

            #endregion

            LinkedList<_Solicitud> solicitudesByYear = SolicitudRepo.GetSolicitudesByYear(DateTime.Now.Year),
                solicitudesByYearNRol = SolicitudRepo.GetSolicitudesCompletadasByYearNRol(DateTime.Now.Year, Rol.TiposRole.PROCESADOR),
                solicitudesCoor = SolicitudRepo.GetSolicitudesCompletadasByYearNRol(DateTime.Now.Year, Rol.TiposRole.COORDINADOR),
                solicitudesSup = SolicitudRepo.GetSolicitudesCompletadasByYearNRol(DateTime.Now.Year, Rol.TiposRole.SUPERVISOR),
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
                        pendCoorCount + pendTrabajarCount;

            #endregion

            totalAvisosLbl.Text = string.Format("Total de Solicitudes Recibidas: {0}", totalSolicitudes);

            SetPieChartData(aprovadosCount, pendCoorCount, pendAsigCount, pendTrabajarCount
                , docIncompleteCount, denegadasCount, inactivasCount);

            SetTableData(solicitudesByYearNRol, Rol.TiposRole.PROCESADOR);

            SetTableData(solicitudesCoor, Rol.TiposRole.COORDINADOR);

            SetTableData(solicitudesSup, Rol.TiposRole.SUPERVISOR);
        }

        private void SetPieChartData(int aprobadasCount, int pendientesCount, int asigInspectorCount, int procesosInpeccionCount
           , int docsIncompletosCount, int denegadasCount, int inactivasCount)
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

            #region Inactivas

            if (denegadasCount > 0)
            {
                Chart1.Series[0].Points[6].SetValueY(inactivasCount);

                Chart1.Series[0].Points[6].Label = inactivasCount.ToString();
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
            int zeroToTwoCount = 0,
                threeToFiveCount = 0,
                sixToEightCount = 0,
                nineAndAboveCount = 0;

            for (int i = 0; i < solicitudes.Count; i++)
            {
                int dateDiff = (solicitudes.ElementAt(i).FechaTrabajado - solicitudes.ElementAt(i).FechaTramitada).Days;

                if (dateDiff < 3)
                    zeroToTwoCount++;

                else if (dateDiff < 6)
                    threeToFiveCount++;

                else if (dateDiff < 9)
                    sixToEightCount++;

                else if (dateDiff >= 9)
                    nineAndAboveCount++;
            }

            #region 0-2

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

        private void SetTableData(LinkedList<_Solicitud> solicitudes, Rol.TiposRole rol)
        {
            #region Define counters

            int totalTrabajoEneCount = 0,
                    totalTrabajoFebCount = 0,
                    totalTrabajoMarCount = 0,
                    totalTrabajoAbrCount = 0,
                    totalTrabajoMayCount = 0,
                    totalTrabajoJunCount = 0,
                    totalTrabajoJulCount = 0,
                    totalTrabajoAgoCount = 0,
                    totalTrabajoSepCount = 0,
                    totalTrabajoOctCount = 0,
                    totalTrabajoNovCount = 0,
                    totalTrabajoDicCount = 0,

                    solicitudesEneCount = 0,
                    solicitudesFebCount = 0,
                    solicitudesMarCount = 0,
                    solicitudesAbrCount = 0,
                    solicitudesMayCount = 0,
                    solicitudesJunCount = 0,
                    solicitudesJulCount = 0,
                    solicitudesAgoCount = 0,
                    solicitudesSepCount = 0,
                    solicitudesOctCount = 0,
                    solicitudesNovCount = 0,
                    solicitudesDicCount = 0;

            #endregion

            #region Iterate through solicitudes and initiate counters according to rol

            for (int i = 0; i < solicitudes.Count; i++)
            {
                switch (rol)
                {
                    case Rol.TiposRole.COORDINADOR:

                        int dateDif = (solicitudes.ElementAt(i).FechaRevision - solicitudes.ElementAt(i).FechaTramitada).Days;

                        #region Intantiating counters

                        switch (solicitudes.ElementAt(i).FechaRevision.Month)
                        {
                            case 1:

                                totalTrabajoEneCount += dateDif;

                                solicitudesEneCount++;

                                break;

                            case 2:

                                totalTrabajoFebCount += dateDif;

                                solicitudesFebCount++;

                                break;

                            case 3:

                                totalTrabajoMarCount += dateDif;

                                solicitudesMarCount++;

                                break;

                            case 4:

                                totalTrabajoAbrCount += dateDif;

                                solicitudesAbrCount++;

                                break;

                            case 5:

                                totalTrabajoMayCount += dateDif;

                                solicitudesMayCount++;

                                break;

                            case 6:

                                totalTrabajoJunCount += dateDif;

                                solicitudesJunCount++;

                                break;

                            case 7:

                                totalTrabajoJulCount += dateDif;

                                solicitudesJulCount++;

                                break;

                            case 8:

                                totalTrabajoAgoCount += dateDif;

                                solicitudesAgoCount++;

                                break;

                            case 9:

                                totalTrabajoSepCount += dateDif;

                                solicitudesSepCount++;

                                break;

                            case 10:

                                totalTrabajoOctCount += dateDif;

                                solicitudesOctCount++;

                                break;

                            case 11:

                                totalTrabajoNovCount += dateDif;

                                solicitudesNovCount++;

                                break;

                            default:

                                totalTrabajoDicCount += dateDif;

                                solicitudesDicCount++;

                                break;
                        }

                        #endregion

                        break;

                    case Rol.TiposRole.PROCESADOR:

                        dateDif = (solicitudes.ElementAt(i).FechaTrabajado - solicitudes.ElementAt(i).FechaAsigProcesador).Days;

                        #region Intantiating counters

                        switch (solicitudes.ElementAt(i).FechaTrabajado.Month)
                        {
                            case 1:

                                totalTrabajoEneCount += dateDif;

                                solicitudesEneCount++;

                                break;

                            case 2:

                                totalTrabajoFebCount += dateDif;

                                solicitudesFebCount++;

                                break;

                            case 3:

                                totalTrabajoMarCount += dateDif;

                                solicitudesMarCount++;

                                break;

                            case 4:

                                totalTrabajoAbrCount += dateDif;

                                solicitudesAbrCount++;

                                break;

                            case 5:

                                totalTrabajoMayCount += dateDif;

                                solicitudesMayCount++;

                                break;

                            case 6:

                                totalTrabajoJunCount += dateDif;

                                solicitudesJunCount++;

                                break;

                            case 7:

                                totalTrabajoJulCount += dateDif;

                                solicitudesJulCount++;

                                break;

                            case 8:

                                totalTrabajoAgoCount += dateDif;

                                solicitudesAgoCount++;

                                break;

                            case 9:

                                totalTrabajoSepCount += dateDif;

                                solicitudesSepCount++;

                                break;

                            case 10:

                                totalTrabajoOctCount += dateDif;

                                solicitudesOctCount++;

                                break;

                            case 11:

                                totalTrabajoNovCount += dateDif;

                                solicitudesNovCount++;

                                break;

                            default:

                                totalTrabajoDicCount += dateDif;

                                solicitudesDicCount++;

                                break;
                        }

                        #endregion

                        break;

                    default:

                        dateDif = (solicitudes.ElementAt(i).FechaAsigProcesador - solicitudes.ElementAt(i).FechaRevision).Days;

                        #region Intantiating counters

                        switch (solicitudes.ElementAt(i).FechaAsigProcesador.Month)
                        {
                            case 1:

                                totalTrabajoEneCount += dateDif;

                                solicitudesEneCount++;

                                break;

                            case 2:

                                totalTrabajoFebCount += dateDif;

                                solicitudesFebCount++;

                                break;

                            case 3:

                                totalTrabajoMarCount += dateDif;

                                solicitudesMarCount++;

                                break;

                            case 4:

                                totalTrabajoAbrCount += dateDif;

                                solicitudesAbrCount++;

                                break;

                            case 5:

                                totalTrabajoMayCount += dateDif;

                                solicitudesMayCount++;

                                break;

                            case 6:

                                totalTrabajoJunCount += dateDif;

                                solicitudesJunCount++;

                                break;

                            case 7:

                                totalTrabajoJulCount += dateDif;

                                solicitudesJulCount++;

                                break;

                            case 8:

                                totalTrabajoAgoCount += dateDif;

                                solicitudesAgoCount++;

                                break;

                            case 9:

                                totalTrabajoSepCount += dateDif;

                                solicitudesSepCount++;

                                break;

                            case 10:

                                totalTrabajoOctCount += dateDif;

                                solicitudesOctCount++;

                                break;

                            case 11:

                                totalTrabajoNovCount += dateDif;

                                solicitudesNovCount++;

                                break;

                            default:

                                totalTrabajoDicCount += dateDif;

                                solicitudesDicCount++;

                                break;
                        }

                        #endregion

                        break;
                }
            }

            #endregion

            #region Set lbl txt according to rol

            switch (rol)
            {
                case Rol.TiposRole.COORDINADOR:

                    #region Setting coor lbls

                    abrCoorLbl.Text = totalTrabajoAbrCount != 0 ? ((int)(totalTrabajoAbrCount / solicitudesAbrCount)).ToString()
                        : "0";

                    agoCoorLbl.Text = totalTrabajoAgoCount != 0 ? ((int)(totalTrabajoAgoCount / solicitudesAgoCount)).ToString()
                        : "0";

                    dicCoorLbl.Text = totalTrabajoDicCount != 0 ? ((int)(totalTrabajoDicCount / solicitudesDicCount)).ToString()
                        : "0";

                    eneCoorLbl.Text = totalTrabajoEneCount != 0 ? ((int)(totalTrabajoEneCount / solicitudesEneCount)).ToString()
                        : "0";

                    febCoorLbl.Text = totalTrabajoFebCount != 0 ? ((int)(totalTrabajoFebCount / solicitudesFebCount)).ToString()
                        : "0";

                    julCoorLbl.Text = totalTrabajoJulCount != 0 ? ((int)(totalTrabajoJulCount / solicitudesJulCount)).ToString()
                        : "0";

                    junCoorLbl.Text = totalTrabajoJunCount != 0 ? ((int)(totalTrabajoJunCount / solicitudesJunCount)).ToString()
                        : "0";

                    marCoorLbl.Text = totalTrabajoMarCount != 0 ? ((int)(totalTrabajoMarCount / solicitudesMarCount)).ToString()
                        : "0";

                    mayCoorLbl.Text = totalTrabajoMayCount != 0 ? ((int)(totalTrabajoMayCount / solicitudesMayCount)).ToString()
                        : "0";

                    novCoorLbl.Text = totalTrabajoNovCount != 0 ? ((int)(totalTrabajoNovCount / solicitudesNovCount)).ToString()
                        : "0";

                    octCoorLbl.Text = totalTrabajoOctCount != 0 ? ((int)(totalTrabajoOctCount / solicitudesOctCount)).ToString()
                        : "0";

                    sepCoorLbl.Text = totalTrabajoSepCount != 0 ? ((int)(totalTrabajoSepCount / solicitudesSepCount)).ToString()
                        : "0";

                    #endregion

                    break;

                case Rol.TiposRole.PROCESADOR:

                    #region Setting proc lbls

                    abrProcLbl.Text = totalTrabajoAbrCount != 0 ? ((int)(totalTrabajoAbrCount / solicitudesAbrCount)).ToString()
                        : "0";

                    agoProcLbl.Text = totalTrabajoAgoCount != 0 ? ((int)(totalTrabajoAgoCount / solicitudesAgoCount)).ToString()
                        : "0";

                    dicProcLbl.Text = totalTrabajoDicCount != 0 ? ((int)(totalTrabajoDicCount / solicitudesDicCount)).ToString()
                        : "0";

                    eneProcLbl.Text = totalTrabajoEneCount != 0 ? ((int)(totalTrabajoEneCount / solicitudesEneCount)).ToString()
                        : "0";

                    febProcLbl.Text = totalTrabajoFebCount != 0 ? ((int)(totalTrabajoFebCount / solicitudesFebCount)).ToString()
                        : "0";

                    julProcLbl.Text = totalTrabajoJulCount != 0 ? ((int)(totalTrabajoJulCount / solicitudesJulCount)).ToString()
                        : "0";

                    junProcLbl.Text = totalTrabajoJunCount != 0 ? ((int)(totalTrabajoJunCount / solicitudesJunCount)).ToString()
                        : "0";

                    marProcLbl.Text = totalTrabajoMarCount != 0 ? ((int)(totalTrabajoMarCount / solicitudesMarCount)).ToString()
                        : "0";

                    mayProcLbl.Text = totalTrabajoMayCount != 0 ? ((int)(totalTrabajoMayCount / solicitudesMayCount)).ToString()
                        : "0";

                    novProcLbl.Text = totalTrabajoNovCount != 0 ? ((int)(totalTrabajoNovCount / solicitudesNovCount)).ToString()
                        : "0";

                    octProcLbl.Text = totalTrabajoOctCount != 0 ? ((int)(totalTrabajoOctCount / solicitudesOctCount)).ToString()
                        : "0";

                    sepProcLbl.Text = totalTrabajoSepCount != 0 ? ((int)(totalTrabajoSepCount / solicitudesSepCount)).ToString()
                        : "0";

                    #endregion

                    break;

                default:

                    #region Setting sup lbls

                    abrSupLbl.Text = totalTrabajoAbrCount != 0 ? ((int)(totalTrabajoAbrCount / solicitudesAbrCount)).ToString()
                        : "0";

                    agoSupLbl.Text = totalTrabajoAgoCount != 0 ? ((int)(totalTrabajoAgoCount / solicitudesAgoCount)).ToString()
                        : "0";

                    dicSupLbl.Text = totalTrabajoDicCount != 0 ? ((int)(totalTrabajoDicCount / solicitudesDicCount)).ToString()
                        : "0";

                    eneSupLbl.Text = totalTrabajoEneCount != 0 ? ((int)(totalTrabajoEneCount / solicitudesEneCount)).ToString()
                        : "0";

                    febSupLbl.Text = totalTrabajoFebCount != 0 ? ((int)(totalTrabajoFebCount / solicitudesFebCount)).ToString()
                        : "0";

                    julSupLbl.Text = totalTrabajoJulCount != 0 ? ((int)(totalTrabajoJulCount / solicitudesJulCount)).ToString()
                        : "0";

                    junSupLbl.Text = totalTrabajoJunCount != 0 ? ((int)(totalTrabajoJunCount / solicitudesJunCount)).ToString()
                        : "0";

                    marSupLbl.Text = totalTrabajoMarCount != 0 ? ((int)(totalTrabajoMarCount / solicitudesMarCount)).ToString()
                        : "0";

                    maySupLbl.Text = totalTrabajoMayCount != 0 ? ((int)(totalTrabajoMayCount / solicitudesMayCount)).ToString()
                        : "0";

                    novSupLbl.Text = totalTrabajoNovCount != 0 ? ((int)(totalTrabajoNovCount / solicitudesNovCount)).ToString()
                        : "0";

                    octSupLbl.Text = totalTrabajoOctCount != 0 ? ((int)(totalTrabajoOctCount / solicitudesOctCount)).ToString()
                        : "0";

                    sepSupLbl.Text = totalTrabajoSepCount != 0 ? ((int)(totalTrabajoSepCount / solicitudesSepCount)).ToString()
                        : "0";

                    #endregion

                    break;
            }

            #endregion
        }
    }
}