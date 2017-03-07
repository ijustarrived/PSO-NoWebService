using PSO.Entities;
using PSO.Repositorios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PSO.Pages.Dashboard.Reports
{
    public partial class TrabajoReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario user = Session["UserObj"] == null ? new Usuario() : (Usuario)Session["UserObj"];

            #region Verify role permission

            if (!user.Role.ViewReportIndicadores)
            {
                if (string.IsNullOrEmpty(user.Email))
                    Response.Redirect("~/Pages/Login.aspx", true);

                Response.Redirect("~/Pages/Dashboard/Main.aspx", true);
            }

            #endregion

            #region Set Cosmetics

            Cosmetic cosmetic = (Cosmetic)Session["Cosmetic"];

            Page.Title = cosmetic.IndicadoresProductividadTitle;

            #region Set titles

            #region Table headers

            abrTitleTh.Style.Add("background-color", cosmetic.TitleBackColor);

            agoTitleTh.Style.Add("background-color", cosmetic.TitleBackColor);

            dicTitleTh.Style.Add("background-color", cosmetic.TitleBackColor);

            eneTitleTh.Style.Add("background-color", cosmetic.TitleBackColor);

            febTitleTh.Style.Add("background-color", cosmetic.TitleBackColor);

            fillerTitleTh.Style.Add("background-color", cosmetic.TitleBackColor);

            julTitleTh.Style.Add("background-color", cosmetic.TitleBackColor);

            junTitleTh.Style.Add("background-color", cosmetic.TitleBackColor);

            marTitleTh.Style.Add("background-color", cosmetic.TitleBackColor);

            mayTitleTh.Style.Add("background-color", cosmetic.TitleBackColor);

            novTitleTh.Style.Add("background-color", cosmetic.TitleBackColor);

            octTitleTh.Style.Add("background-color", cosmetic.TitleBackColor);

            sepTitleTh.Style.Add("background-color", cosmetic.TitleBackColor);

            metaTitleTh.Style.Add("background-color", cosmetic.TitleBackColor);

            #endregion

            #endregion

            #region Set lbls

            ScriptManager.RegisterStartupScript(this, GetType(), "InvokeChangeClinetSideColors",
                                    string.Format("ChangeClinetSideColors('{0}', '{1}');",
                                    cosmetic.LabelForeColor, cosmetic.TitleBackColor), true);

            #region Meta

            coorMetaLblTh.Style.Add("color", cosmetic.LabelForeColor);

            proMetaLblTh.Style.Add("color", cosmetic.LabelForeColor);

            supMetaLblTh.Style.Add("color", cosmetic.LabelForeColor);

            #endregion

            #region Coor th 

            coorMarLblTh.Style.Add("color", cosmetic.LabelForeColor);

            coorMayLblTh.Style.Add("color", cosmetic.LabelForeColor);

            coorAbrLblTh.Style.Add("color", cosmetic.LabelForeColor);

            coorAgoLblTh.Style.Add("color", cosmetic.LabelForeColor);

            coorDicLblTh.Style.Add("color", cosmetic.LabelForeColor);

            coorEneLblTh.Style.Add("color", cosmetic.LabelForeColor);

            coorFebLblTh.Style.Add("color", cosmetic.LabelForeColor);

            coorJulLblTh.Style.Add("color", cosmetic.LabelForeColor);

            coorJunLblTh.Style.Add("color", cosmetic.LabelForeColor);

            coorLblTh.Style.Add("color", cosmetic.LabelForeColor);

            coorNovLblTh.Style.Add("color", cosmetic.LabelForeColor);

            coorOctLblTh.Style.Add("color", cosmetic.LabelForeColor);

            coorSepLblTh.Style.Add("color", cosmetic.LabelForeColor);

            #endregion

            #region Proc th

            proAbrLblTh.Style.Add("color", cosmetic.LabelForeColor);

            proAgoLblTh.Style.Add("color", cosmetic.LabelForeColor);

            proDicLblTh.Style.Add("color", cosmetic.LabelForeColor);

            proEneLblTh.Style.Add("color", cosmetic.LabelForeColor);

            proFebLblTh.Style.Add("color", cosmetic.LabelForeColor);

            proJulLblTh.Style.Add("color", cosmetic.LabelForeColor);

            proJunLblTh.Style.Add("color", cosmetic.LabelForeColor);

            proLblTh.Style.Add("color", cosmetic.LabelForeColor);

            proMarLblTh.Style.Add("color", cosmetic.LabelForeColor);

            proMayLblTh.Style.Add("color", cosmetic.LabelForeColor);

            proNovLblTh.Style.Add("color", cosmetic.LabelForeColor);

            proOctLblTh.Style.Add("color", cosmetic.LabelForeColor);

            proSepLblTh.Style.Add("color", cosmetic.LabelForeColor);

            #endregion

            #region Sup th

            supAbrLblTh.Style.Add("color", cosmetic.LabelForeColor);

            supAgoLblTh.Style.Add("color", cosmetic.LabelForeColor);

            supDicLblTh.Style.Add("color", cosmetic.LabelForeColor);

            supEneLblTh.Style.Add("color", cosmetic.LabelForeColor);

            supFebLblTh.Style.Add("color", cosmetic.LabelForeColor);

            supJulLblTh.Style.Add("color", cosmetic.LabelForeColor);

            supJunLblTh.Style.Add("color", cosmetic.LabelForeColor);

            supLblTh.Style.Add("color", cosmetic.LabelForeColor);

            supMarLblTh.Style.Add("color", cosmetic.LabelForeColor);

            supMayLblTh.Style.Add("color", cosmetic.LabelForeColor);

            supNovLblTh.Style.Add("color", cosmetic.LabelForeColor);

            supOctLblTh.Style.Add("color", cosmetic.LabelForeColor);

            supSepLblTh.Style.Add("color", cosmetic.LabelForeColor);

            #endregion

            #endregion

            #endregion

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

            mainDashLink.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            dashboardPnl.Controls.Add(mainDashLink);

            #endregion

            #region 2nd link

            secondDashlbl.Text = " > ";

            secondDashlbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            dashboardPnl.Controls.Add(secondDashlbl);

            secondDashLink.NavigateUrl = "~/Pages/Dashboard/Reports/ReportsMain.aspx";

            secondDashLink.Text = "Reportes";

            secondDashLink.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            dashboardPnl.Controls.Add(secondDashLink);

            #endregion

            #endregion

            LinkedList<_Solicitud> solicitudesByYearNRol =
                SolicitudRepo.GetSolicitudesCompletadasByYearNRol(DateTime.Now.Year, Rol.TiposRole.PROCESADOR),
                solicitudesCoor = SolicitudRepo.GetSolicitudesCompletadasByYearNRol(DateTime.Now.Year, Rol.TiposRole.COORDINADOR),
                solicitudesSup = SolicitudRepo.GetSolicitudesCompletadasByYearNRol(DateTime.Now.Year, Rol.TiposRole.SUPERVISOR);

            SetTableData(solicitudesByYearNRol, Rol.TiposRole.PROCESADOR);

            SetTableData(solicitudesCoor, Rol.TiposRole.COORDINADOR);

            SetTableData(solicitudesSup, Rol.TiposRole.SUPERVISOR);
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

                        //int dateDif = (solicitudes.ElementAt(i).FechaRevision - solicitudes.ElementAt(i).FechaTramitada).Days;

                        int dateDif = CalcDateDiff(solicitudes.ElementAt(i).FechaTramitada.Date,
                            solicitudes.ElementAt(i).FechaRevision.Date, true);

                        //Even though it oly took hours, in days it's 1
                        //if (dateDif == 0)
                        //    dateDif = 1;

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

                        //dateDif = (solicitudes.ElementAt(i).FechaTrabajado - solicitudes.ElementAt(i).FechaAsigProcesador).Days;

                        //if (dateDif == 0)
                        //    dateDif = 1;

                        dateDif = CalcDateDiff(solicitudes.ElementAt(i).FechaAsigProcesador.Date,
                            solicitudes.ElementAt(i).FechaTrabajado.Date, true);

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

                        //dateDif = (solicitudes.ElementAt(i).FechaAsigProcesador - solicitudes.ElementAt(i).FechaRevision).Days;

                        //if (dateDif == 0)
                        //    dateDif = 1;

                        dateDif = CalcDateDiff(solicitudes.ElementAt(i).FechaRevision.Date,
                            solicitudes.ElementAt(i).FechaAsigProcesador.Date, true);

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

                    abrCoorLbl.Text = totalTrabajoAbrCount != 0
                        ? (Math.Round((double)totalTrabajoAbrCount / solicitudesAbrCount)).ToString()
                        : "0";

                    agoCoorLbl.Text = totalTrabajoAgoCount != 0
                        ? (Math.Round((double)totalTrabajoAgoCount / solicitudesAgoCount)).ToString()
                        : "0";
                    dicCoorLbl.Text = totalTrabajoDicCount != 0
                        ? (Math.Round((double)totalTrabajoDicCount / solicitudesDicCount)).ToString()
                        : "0";

                    eneCoorLbl.Text = totalTrabajoEneCount != 0
                        ? (Math.Round((double)totalTrabajoEneCount / solicitudesEneCount)).ToString()
                        : "0";

                    febCoorLbl.Text = totalTrabajoFebCount != 0
                        ? (Math.Round((double)totalTrabajoFebCount / solicitudesFebCount)).ToString()
                        : "0";

                    julCoorLbl.Text = totalTrabajoJulCount != 0
                        ? (Math.Round((double)totalTrabajoJulCount / solicitudesJulCount)).ToString()
                        : "0";

                    junCoorLbl.Text = totalTrabajoJunCount != 0
                        ? (Math.Round((double)totalTrabajoJunCount / solicitudesJunCount)).ToString()
                        : "0";

                    marCoorLbl.Text = totalTrabajoMarCount != 0
                        ? (Math.Round((double)totalTrabajoMarCount / solicitudesMarCount)).ToString()
                        : "0";

                    mayCoorLbl.Text = totalTrabajoMayCount != 0
                        ? (Math.Round((double)totalTrabajoMayCount / solicitudesMayCount)).ToString()
                        : "0";

                    novCoorLbl.Text = totalTrabajoNovCount != 0
                        ? (Math.Round((double)totalTrabajoNovCount / solicitudesNovCount)).ToString()
                        : "0";

                    octCoorLbl.Text = totalTrabajoOctCount != 0
                        ? (Math.Round((double)totalTrabajoOctCount / solicitudesOctCount)).ToString()
                        : "0";

                    sepCoorLbl.Text = totalTrabajoSepCount != 0
                        ? (Math.Round((double)totalTrabajoSepCount / solicitudesSepCount)).ToString()
                        : "0";

                    #endregion

                    break;

                case Rol.TiposRole.PROCESADOR:

                    #region Setting proc lbls

                    abrProcLbl.Text = totalTrabajoAbrCount != 0
                        ? (Math.Round((double)totalTrabajoAbrCount / solicitudesAbrCount)).ToString()
                        : "0";

                    agoProcLbl.Text = totalTrabajoAgoCount != 0
                        ? (Math.Round((double)totalTrabajoAgoCount / solicitudesAgoCount)).ToString()
                        : "0";
                    dicProcLbl.Text = totalTrabajoDicCount != 0
                        ? (Math.Round((double)totalTrabajoDicCount / solicitudesDicCount)).ToString()
                        : "0";

                    eneProcLbl.Text = totalTrabajoEneCount != 0
                        ? (Math.Round((double)totalTrabajoEneCount / solicitudesEneCount)).ToString()
                        : "0";

                    febProcLbl.Text = totalTrabajoFebCount != 0
                        ? (Math.Round((double)totalTrabajoFebCount / solicitudesFebCount)).ToString()
                        : "0";

                    julProcLbl.Text = totalTrabajoJulCount != 0
                        ? (Math.Round((double)totalTrabajoJulCount / solicitudesJulCount)).ToString()
                        : "0";

                    junProcLbl.Text = totalTrabajoJunCount != 0
                        ? (Math.Round((double)totalTrabajoJunCount / solicitudesJunCount)).ToString()
                        : "0";

                    marProcLbl.Text = totalTrabajoMarCount != 0
                        ? (Math.Round((double)totalTrabajoMarCount / solicitudesMarCount)).ToString()
                        : "0";

                    mayProcLbl.Text = totalTrabajoMayCount != 0
                        ? (Math.Round((double)totalTrabajoMayCount / solicitudesMayCount)).ToString()
                        : "0";

                    novProcLbl.Text = totalTrabajoNovCount != 0
                        ? (Math.Round((double)totalTrabajoNovCount / solicitudesNovCount)).ToString()
                        : "0";

                    octProcLbl.Text = totalTrabajoOctCount != 0
                        ? (Math.Round((double)totalTrabajoOctCount / solicitudesOctCount)).ToString()
                        : "0";

                    sepProcLbl.Text = totalTrabajoSepCount != 0
                        ? (Math.Round((double)totalTrabajoSepCount / solicitudesSepCount)).ToString()
                        : "0";

                    #endregion

                    break;

                default:

                    #region Setting sup lbls

                    abrSupLbl.Text = totalTrabajoAbrCount != 0
                        ? (Math.Round((double)totalTrabajoAbrCount / solicitudesAbrCount)).ToString()
                        : "0";

                    agoSupLbl.Text = totalTrabajoAgoCount != 0
                        ? (Math.Round((double)totalTrabajoAgoCount / solicitudesAgoCount)).ToString()
                        : "0";
                    dicSupLbl.Text = totalTrabajoDicCount != 0
                        ? (Math.Round((double)totalTrabajoDicCount / solicitudesDicCount)).ToString()
                        : "0";

                    eneSupLbl.Text = totalTrabajoEneCount != 0
                        ? (Math.Round((double)totalTrabajoEneCount / solicitudesEneCount)).ToString()
                        : "0";

                    febSupLbl.Text = totalTrabajoFebCount != 0
                        ? (Math.Round((double)totalTrabajoFebCount / solicitudesFebCount)).ToString()
                        : "0";

                    julSupLbl.Text = totalTrabajoJulCount != 0
                        ? (Math.Round((double)totalTrabajoJulCount / solicitudesJulCount)).ToString()
                        : "0";

                    junSupLbl.Text = totalTrabajoJunCount != 0
                        ? (Math.Round((double)totalTrabajoJunCount / solicitudesJunCount)).ToString()
                        : "0";

                    marSupLbl.Text = totalTrabajoMarCount != 0
                        ? (Math.Round((double)totalTrabajoMarCount / solicitudesMarCount)).ToString()
                        : "0";

                    maySupLbl.Text = totalTrabajoMayCount != 0
                        ? (Math.Round((double)totalTrabajoMayCount / solicitudesMayCount)).ToString()
                        : "0";

                    novSupLbl.Text = totalTrabajoNovCount != 0
                        ? (Math.Round((double)totalTrabajoNovCount / solicitudesNovCount)).ToString()
                        : "0";

                    octSupLbl.Text = totalTrabajoOctCount != 0
                        ? (Math.Round((double)totalTrabajoOctCount / solicitudesOctCount)).ToString()
                        : "0";

                    sepSupLbl.Text = totalTrabajoSepCount != 0
                        ? (Math.Round((double)totalTrabajoSepCount / solicitudesSepCount)).ToString()
                        : "0";

                    #endregion

                    break;
            }

            #endregion
        }

        /// <summary>
        /// Calculates difference between two dates excluding weekends
        /// </summary>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <param name="addOneToInitial">If the verification for weekends should start the next day</param>
        /// <returns>Calculated difference excluding weekends</returns>
        private int CalcDateDiff(DateTime fechaInicial, DateTime fechaFinal, bool addOneToInitial)
        {
            int noLaborablesCount = 0;

            DateTime fechaInicialOriginal = fechaInicial;

            if (fechaFinal != fechaInicialOriginal)
            {
                if (addOneToInitial)
                    fechaInicial = fechaInicial.AddDays(1);

                //Va sumandole a fecha tramitada hasta que llega a la de trabajo
                while (fechaFinal != fechaInicial)
                {
                    if (fechaInicial.DayOfWeek == DayOfWeek.Saturday || fechaInicial.DayOfWeek == DayOfWeek.Sunday)
                    {
                        noLaborablesCount++;
                    }
                    fechaInicial = fechaInicial.AddDays(1);

                    //Check last day before breaking out of loop.
                    if (fechaFinal == fechaInicial)
                    {
                        if (fechaInicial.DayOfWeek == DayOfWeek.Saturday || fechaInicial.DayOfWeek == DayOfWeek.Sunday)
                        {
                            noLaborablesCount++;
                        }
                    }
                }
            }

            int dateDiff = (fechaFinal - fechaInicialOriginal).Days;

            dateDiff = dateDiff - noLaborablesCount;

            //La diferencia por lo menos debe de ser uno por que se tarda un dia minimo, en procesar
            //if (dateDiff < 1)
            //    dateDiff = 1;

            return dateDiff;
        }

        [System.Web.Services.WebMethod]
        public static string KeepAlive()
        {
            return DateTime.Now.ToShortDateString();
        }

        [System.Web.Services.WebMethod]
        public static void ReleaseAllLkdSolicitudes(int lockedId)
        {
            SolicitudRepo.ReleaseAllLockedSolicitudes(lockedId);
        }
    }
}