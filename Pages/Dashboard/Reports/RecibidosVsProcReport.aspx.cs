using System;
using PSO.Entities;
using PSO.Repositorios;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace PSO.Pages.Dashboard.Reports
{
    public partial class RecibidosVsProcReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Verify role permission

            Usuario user = Session["UserObj"] == null ? new Usuario() : (Usuario)Session["UserObj"];

            if (!user.Role.ViewRepRecVsPen)
            {
                if (string.IsNullOrEmpty(user.Email))
                    Response.Redirect("~/Pages/Login.aspx", true);

                Response.Redirect("~/Pages/Dashboard/Main.aspx", true);
            }

            #endregion

            #region Set cosmetics

            Cosmetic cosmetic = (Cosmetic)Session["Cosmetic"];

            Page.Title = cosmetic.ReportComparacionTitle;

            detallesTitleDiv.Style.Add("background-color", cosmetic.TitleBackColor);

            #region Set lbl colors

            searchBtn.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            searchRolBtn.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            empleadoLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            periodoFechaLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            periodoFechasDetalleLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            statusLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            recievedGV.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            #endregion

            #region Set gv colors

            recievedGV.EmptyDataRowStyle.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            recievedGV.FooterStyle.BackColor = ColorTranslator.FromHtml(cosmetic.TitleBackColor);

            recievedGV.PagerStyle.BackColor = ColorTranslator.FromHtml(cosmetic.TitleBackColor);

            recievedGV.HeaderStyle.BackColor = ColorTranslator.FromHtml(cosmetic.TitleBackColor);

            #endregion

            ScriptManager.RegisterStartupScript(this, GetType(), "InvokeChangeClinetSideColors",
                                   string.Format("ChangeClinetSideColors('{0}', '{1}');",
                                   cosmetic.LabelForeColor, cosmetic.TitleBackColor), true);

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

            if (!IsPostBack)
            {
                ViewState["Coords"] = UserRepo.GetUsersByRole((int)Rol.TiposRole.COORDINADOR, false);

                ViewState["Procs"] = UserRepo.GetUsersByRole((int)Rol.TiposRole.PROCESADOR, false);
            }

            CountRowsNPages();

            //if (recievedGV.Rows.Count != 0)
            //{                
            //    recievedGV.AllowPaging = false;

            //    recievedGV.AllowSorting = false;

            //    recievedGV.DataBind();

            //    totalAvisosLbl.Text = string.Format("Total de Solicitudes: {0}",
            //        recievedGV.Rows.Count);

            //    recievedGV.AllowPaging = true;

            //    recievedGV.AllowSorting = true;

            //    recievedGV.DataBind();

            //    totalPagesLbl.Text = string.Format("Total de Paginas: {0}",
            //        recievedGV.PageCount);
            //}
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

        protected void searchBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(desdeTxtBx.Text) && !string.IsNullOrEmpty(hastaTxtBx.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "bothDatesEmptyAlert",
                                    "alert('Ambas fechas son requeridas')", true);

                hastaTxtBx.Text = string.Empty;
            }

            else if (!string.IsNullOrEmpty(desdeTxtBx.Text) && string.IsNullOrEmpty(hastaTxtBx.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "bothDatesEmptyAlert",
                                    "alert('Ambas fechas son requeridas')", true);

                desdeTxtBx.Text = string.Empty;
            }

            recievedGV.DataBind();
        }

        private void SetChartData(int aprobadasCount, int pendientesCount)
        {
            if (aprobadasCount > 0)
            {
                Chart1.Series[0].Points[0].SetValueY(aprobadasCount);

                Chart1.Series[0].Points[0].Label = aprobadasCount.ToString();
            }

            if (pendientesCount > 0)
            {
                Chart1.Series[0].Points[1].SetValueY(pendientesCount);

                Chart1.Series[0].Points[1].Label = pendientesCount.ToString();
            }
        }

        protected void solicitudesSQLDS_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            LinkedList<_Solicitud> solicitudes = new LinkedList<_Solicitud>();

            string where = string.Empty;

            #region Query for Chart and gv

            if (!string.IsNullOrEmpty(desdeTxtBx.Text) && !string.IsNullOrEmpty(hastaTxtBx.Text))
            {
                desdeDetailTxtBx.Text = desdeTxtBx.Text;

                hastaDetailTxtBx.Text = hastaTxtBx.Text;

                where = string.Format(@"WHERE FechaTramitada >= CONVERT(datetime, '{0} 00:00:00.000') 
                                                AND FechaTramitada < CONVERT(datetime, '{1} 23:59:59.997')",
                                                desdeTxtBx.Text, hastaTxtBx.Text);

                solicitudes = SolicitudRepo.GetSolicitudesByDateRange(
                    Convert.ToDateTime(desdeTxtBx.Text), Convert.ToDateTime(hastaTxtBx.Text));
            }

            else
            {
                solicitudes = SolicitudRepo.GetSolicitudes();
            }

            #endregion

            #region Query for GV only

            #region Query according to Rol DLL

            if (!string.IsNullOrEmpty(desdeDetailTxtBx.Text) && !string.IsNullOrEmpty(hastaDetailTxtBx.Text))
            {
                where = string.Format(@"WHERE FechaTramitada >= CONVERT(datetime, '{0} 00:00:00.000')
                                                AND FechaTramitada < CONVERT(datetime, '{1} 23:59:59.997')",
                                                desdeDetailTxtBx.Text, hastaDetailTxtBx.Text);
            }

            switch (filterDDL.SelectedIndex)
            {
                //Coor
                case 1:

                    //where = string.Format("{0} @AND CoordinadorID = {1} AND FechaRevision <> CONVERT(datetime, '12/31/9999 23:59:59.997')",
                    //    where, searchDDL.SelectedIndex - 1); // - 1 cause el ddl del coor empieza del 1 y el del db empieza en el 0

                    where = string.Format(@"{0} @AND REPLACE(CoordinadorID, ' ', '') = REPLACE('{1}', ' ', '') 
                        AND FechaRevision <> CONVERT(datetime, '12/31/9999 23:59:59.997')",
                       where, searchDDL.SelectedValue);

                    break;

                //Proc
                case 2:

                    where = string.Format(@"{0} @AND (REPLACE(ProcesadorID, ' ', '') = REPLACE('{1}', ' ', '')) 
                                                AND (FechaAsigProcesador <> CONVERT(datetime, '12/31/9999 23:59:59.997'))",
                                                where, searchDDL.SelectedValue);

                    //where = string.Format(@"{0} @AND (ProcesadorID = {1}) 
                    //                            AND (FechaAsigProcesador <> CONVERT(datetime, '12/31/9999 23:59:59.997'))",
                    //                            where, searchDDL.SelectedIndex);

                    //where = string.Format(@"{0} @AND (ProcesadorID = {1}) 
                    //                            AND (FechaAsigProcesador <> CONVERT(datetime, '12/31/9999 23:59:59.997'))",
                    //                           where, (searchDDL.SelectedIndex - 1));


                    break;
            }

            #endregion

            #region Status DLL

            switch (statusSearchDDL.SelectedIndex)
            {
                //Pending
                case 1:

                    int statusId = 0;

                    if (filterDDL.SelectedIndex > 1)
                        statusId = (int)_Solicitud.Statuses.PEND_TRABAJAR;

                    where = string.Format("{0} AND Status = {1} ", where, statusId);

                    break;

                //Worked
                case 2:

                    if (filterDDL.SelectedIndex > 1)
                        where = string.Format(@"{0} AND (Status = {1} OR Status = {2}) 
                                                ", where,
                            (int)_Solicitud.Statuses.APROBADA, (int)_Solicitud.Statuses.DENEGADA);

                    break;
            }

            #endregion

            if (!where.Contains("WHERE"))
                where = where.Replace("@AND", "WHERE");

            else
                where = where.Replace("@AND", "AND");

            #endregion

            int completadasCount = 0;

            for (int i = 0; i < solicitudes.Count; i++)
            {
                if (solicitudes.ElementAt(i).FechaTrabajado.Year != 9999)
                    completadasCount++;
            }

            ViewState["SolicitudCount"] = solicitudes.Count;

            ViewState["completadasCount"] = completadasCount;

            SetChartData(solicitudes.Count, completadasCount);

            e.Command.CommandText = e.Command.CommandText.Replace("@WHERE", where);
        }

        protected void recievedGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                _Solicitud solicitud = SolicitudRepo.GetSolicitudByNumSolicitud(e.Row.Cells[0].Text);

                e.Row.Cells[1].Text = e.Row.Cells[1].Text.Split(' ')[0];

                if (solicitud.FechaRevision.Date.Year == 9999)
                    e.Row.Cells[4].Text = string.Empty;

                else
                {
                    LinkedList<Usuario> coordinadores = (LinkedList<Usuario>)ViewState["Coords"];

                    //e.Row.Cells[4].Text = coordinadores.ElementAt(Convert.ToInt32(e.Row.Cells[4].Text)).GetNombreCompleto();
                }

                DateTime fechaTrabajo = Convert.ToDateTime(e.Row.Cells[2].Text),
                fechaTramite = Convert.ToDateTime(e.Row.Cells[1].Text);

                //don't display default date and substract current date and tramite
                if (fechaTrabajo.Date.Date.Year == 9999)
                {
                    e.Row.Cells[2].Text = string.Empty;

                    e.Row.Cells[3].Text = CalcDateDiff(fechaTramite.Date, DateTime.Now.Date, true).ToString();

                    //TimeSpan span = DateTime.Now.Subtract(fechaTramite);

                    //e.Row.Cells[3].Text = ((int)span.TotalDays).ToString();
                }

                else
                {
                    e.Row.Cells[2].Text = e.Row.Cells[2].Text.Split(' ')[0];

                    e.Row.Cells[3].Text = CalcDateDiff(fechaTramite.Date, fechaTrabajo.Date, true).ToString();
                }

                //Means has no proc assigned
                if (!e.Row.Cells[5].Text.Equals("0"))
                {
                    LinkedList<Usuario> procesadores = (LinkedList<Usuario>)ViewState["Procs"];

                    //e.Row.Cells[5].Text = procesadores.ElementAt(Convert.ToInt32(e.Row.Cells[5].Text) - 1).GetNombreCompleto();
                }

                else
                {
                    e.Row.Cells[5].Text = string.Empty;
                }
            }

            else if (e.Row.RowType == DataControlRowType.EmptyDataRow)
            {
                Cosmetic cosmetic = (Cosmetic)Session["Cosmetic"];

                ((Label)e.Row.Controls[0].Controls[0].FindControl("emptyLbl")).ForeColor
                    = ColorTranslator.FromHtml(cosmetic.LabelForeColor);
            }
        }

        protected void searchRolBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(desdeDetailTxtBx.Text) && !string.IsNullOrEmpty(hastaDetailTxtBx.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "bothDatesEmptyAlert",
                                    "alert('Ambas fechas son requeridas')", true);

                desdeDetailTxtBx.Text = string.Empty;
            }

            else if (!string.IsNullOrEmpty(desdeDetailTxtBx.Text) && string.IsNullOrEmpty(hastaDetailTxtBx.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "bothDatesEmptyAlert",
                                    "alert('Ambas fechas son requeridas')", true);

                desdeDetailTxtBx.Text = string.Empty;
            }
            recievedGV.DataBind();

            CountRowsNPages();
        }

        protected void filterDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList _filterDDL = (DropDownList)sender;

            searchDDL.Visible = _filterDDL.SelectedIndex > 0;

            statusDiv.Visible = filterDDL.SelectedIndex == 2;

            if (!searchDDL.Visible)
                statusSearchDDL.SelectedIndex = 0;

            switch (filterDDL.SelectedIndex)
            {
                //Coor
                case 1:

                    #region Instantiate searchDDL

                    LinkedList<Usuario> coordinadores = (LinkedList<Usuario>)ViewState["Coords"];

                    searchDDL.Items.Clear();

                    searchDDL.Items.Add("Seleccionar");

                    for (int i = 0; i < coordinadores.Count; i++)
                    {
                        searchDDL.Items.Add(coordinadores.ElementAt(i).GetNombreCompleto());
                    }

                    #endregion

                    break;

                //Proc
                case 2:
                    #region Instantiate searchDDL

                    LinkedList<Usuario> procesadores = (LinkedList<Usuario>)ViewState["Procs"];

                    searchDDL.Items.Clear();

                    searchDDL.Items.Add("Seleccionar");

                    for (int i = 0; i < procesadores.Count; i++)
                    {
                        searchDDL.Items.Add(procesadores.ElementAt(i).GetNombreCompleto());
                    }

                    #endregion

                    break;
            }

            SetChartData((int)ViewState["SolicitudCount"], (int)ViewState["completadasCount"]);
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

        protected void recievedGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            recievedGV.PageIndex = e.NewPageIndex;

            recievedGV.DataBind();
        }

        private void CountRowsNPages()
        {
            if (recievedGV.Rows.Count != 0)
            {
                recievedGV.AllowPaging = false;

                recievedGV.AllowSorting = false;

                recievedGV.DataBind();

                totalAvisosLbl.Text = string.Format("Total de Solicitudes: {0}",
                    recievedGV.Rows.Count);

                recievedGV.AllowPaging = true;

                recievedGV.AllowSorting = true;

                recievedGV.DataBind();

                totalPagesLbl.Text = string.Format("Total de Paginas: {0}",
                    recievedGV.PageCount);
            }
        }
    }
}