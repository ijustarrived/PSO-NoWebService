using PSO.Entities;
using PSO.Repositorios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace PSO.Pages.Dashboard.Reports
{
    public partial class ProductionReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Verify role permission

            Usuario user = Session["UserObj"] == null ? new Usuario() : (Usuario)Session["UserObj"];

            if (!user.Role.ViewRepProduc)
            {
                if (string.IsNullOrEmpty(user.Email))
                    Response.Redirect("~/Pages/Login.aspx", true);

                Response.Redirect("~/Pages/Dashboard/Main.aspx", true);
            }

            #endregion

            #region Set cosmetics

            Cosmetic cosmetic = (Cosmetic)Session["Cosmetic"];

            Page.Title = cosmetic.ReportProduccionTitle;

            detallesTitleDiv.Style.Add("background-color", cosmetic.TitleBackColor);

            searchBtn.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            periodoFechaLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            totalAvisosLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            #region Set coor gv colors

            coorGV.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            coorGV.FooterStyle.BackColor = ColorTranslator.FromHtml(cosmetic.TitleBackColor);

            coorGV.PagerStyle.BackColor = ColorTranslator.FromHtml(cosmetic.TitleBackColor);

            coorGV.HeaderStyle.BackColor = ColorTranslator.FromHtml(cosmetic.TitleBackColor);

            #endregion

            #region Set proc gv colors

            procGV.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            procGV.FooterStyle.BackColor = ColorTranslator.FromHtml(cosmetic.TitleBackColor);

            procGV.PagerStyle.BackColor = ColorTranslator.FromHtml(cosmetic.TitleBackColor);

            procGV.HeaderStyle.BackColor = ColorTranslator.FromHtml(cosmetic.TitleBackColor);

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

            ExecuteSearch();

            if (!IsPostBack)
            {
                //ViewState["users"] = UserRepo.GetUsersByRole((int)Rol.TiposRole.COORDINADOR, false);                
            }

            rolDDL_SelectedIndexChanged(sender, EventArgs.Empty);

            #region Set total pages 

            GridView visibleGv = coorGV.Visible ? coorGV : procGV;

            if (visibleGv.Rows.Count != 0)
                totalPagesLbl.Text = string.Format("Total de Paginas: {0}",
                visibleGv.PageCount);

            #endregion
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

        private void ExecuteSearch()
        {
            LinkedList<_Solicitud> solicitudes = new LinkedList<_Solicitud>();

            LinkedList<Usuario> empleados = new LinkedList<Usuario>();

            SetEmpleadoNSolicitud(ref solicitudes, ref empleados);

            totalAvisosLbl.Text = string.Format("Total de Producción: {0}", solicitudes.Count);

            Dictionary<string, int> EmployeeNResults = CountSolicitudesByEmployee(rolDDL.SelectedIndex, empleados, solicitudes);

            SetChartPoints(EmployeeNResults);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rolIndex">ddl Selected index</param>
        /// <param name="empleados"></param>
        /// <param name="solicitudes"></param>
        /// <returns>Dictionary filled with employee names as keys and they're quantfied production as values</returns>
        private Dictionary<string, int> CountSolicitudesByEmployee(int rolIndex,
            LinkedList<Usuario> empleados, LinkedList<_Solicitud> solicitudes)
        {
            Dictionary<string, int> EmployeeNResults = new Dictionary<string, int>();

            //Cycle between employee
            for (int i = 0; i < empleados.Count; i++)
            {
                int employeeSolicitudesCount = 0;

                //Count production of that employee
                for (int i2 = 0; i2 < solicitudes.Count; i2++)
                {
                    if (rolIndex == 1)
                    {
                        if ((solicitudes.ElementAt(i2).ProcesadorId.Replace(" ", string.Empty)).Equals(
                            empleados.ElementAt(i).GetNombreCompleto().Replace(" ", string.Empty)))
                        {
                            employeeSolicitudesCount++;
                        }
                    }

                    else
                    {
                        string nom = empleados.ElementAt(i).GetNombreCompleto().Trim(),
                            nom2 = solicitudes.ElementAt(i2).CoordinadorID.Trim();

                        if (solicitudes.ElementAt(i2).CoordinadorID.Replace(" ", string.Empty).Equals(
                            empleados.ElementAt(i).GetNombreCompleto().Replace(" ", string.Empty)))
                        {
                            employeeSolicitudesCount++;
                        }
                    }
                }

                EmployeeNResults.Add(empleados.ElementAt(i).GetNombreCompleto(), employeeSolicitudesCount);
            }

            return EmployeeNResults;
        }

        private void SetChartPoints(Dictionary<string, int> EmployeeNResults)
        {
            #region Define colors for graph bar

            LinkedList<Color> colors = new LinkedList<Color>();

            colors.AddLast(Color.Aqua);

            colors.AddLast(Color.Blue);

            colors.AddLast(Color.BlueViolet);

            colors.AddLast(Color.Brown);

            colors.AddLast(Color.BurlyWood);

            colors.AddLast(Color.Chartreuse);

            colors.AddLast(Color.Coral);

            colors.AddLast(Color.CornflowerBlue);

            colors.AddLast(Color.Crimson);

            colors.AddLast(Color.DarkBlue);

            colors.AddLast(Color.DarkCyan);

            colors.AddLast(Color.DarkGreen);

            colors.AddLast(Color.DarkKhaki);

            colors.AddLast(Color.DarkOrange);

            colors.AddLast(Color.DarkSalmon);

            colors.AddLast(Color.DarkSlateBlue);

            colors.AddLast(Color.DodgerBlue);

            colors.AddLast(Color.Firebrick);

            colors.AddLast(Color.Fuchsia);

            colors.AddLast(Color.Gold);

            colors.AddLast(Color.Goldenrod);

            colors.AddLast(Color.Gray);

            colors.AddLast(Color.GreenYellow);

            colors.AddLast(Color.Indigo);

            colors.AddLast(Color.LightCoral);

            colors.AddLast(Color.LightSalmon);

            colors.AddLast(Color.LightSeaGreen);

            colors.AddLast(Color.Lime);

            colors.AddLast(Color.MediumAquamarine);

            colors.AddLast(Color.MediumOrchid);

            colors.AddLast(Color.MediumSeaGreen);

            colors.AddLast(Color.MediumSlateBlue);

            colors.AddLast(Color.MediumVioletRed);

            colors.AddLast(Color.MidnightBlue);

            colors.AddLast(Color.Olive);

            colors.AddLast(Color.OliveDrab);

            colors.AddLast(Color.Orange);

            colors.AddLast(Color.OrangeRed);

            colors.AddLast(Color.Plum);

            colors.AddLast(Color.Red);

            colors.AddLast(Color.RosyBrown);

            colors.AddLast(Color.RoyalBlue);

            colors.AddLast(Color.Salmon);

            colors.AddLast(Color.SandyBrown);

            colors.AddLast(Color.SeaGreen);

            colors.AddLast(Color.SlateBlue);

            colors.AddLast(Color.SpringGreen);

            colors.AddLast(Color.Teal);

            colors.AddLast(Color.Tomato);

            colors.AddLast(Color.Yellow);

            #endregion

            //Array colors = Enum.GetValues(typeof(KnownColor));

            Random colorIndex = new Random();

            for (int i = 0; i < EmployeeNResults.Count; i++)
            {
                DataPoint point = new DataPoint();

                point.AxisLabel = EmployeeNResults.Keys.ElementAt(i);

                point.SetValueY(EmployeeNResults.Values.ElementAt(i));

                point.Label = EmployeeNResults.Values.ElementAt(i).ToString();

                point.Color = colors.ElementAt(colorIndex.Next(0, colors.Count - 1));

                //point.Color = Color.FromKnownColor((KnownColor)colors.GetValue(colorIndex.Next(0, colors.Length - 1)));

                productionChrt.Series[0].Points.Add(point);
            }
        }

        protected void searchBtn_Click(object sender, EventArgs e)
        {
            //ExecuteSearch();
        }

        private LinkedList<_Solicitud> GetSolicitudesByStatusNDateRange(Rol.TiposRole role)
        {
            LinkedList<_Solicitud> solicitudes = new LinkedList<_Solicitud>();

            if (string.IsNullOrEmpty(desdeTxtBx.Text))
                solicitudes = SolicitudRepo.GetSolicitudesCompletadasByRole(role);

            else if (!string.IsNullOrEmpty(desdeTxtBx.Text) && !string.IsNullOrEmpty(hastaTxtBx.Text))
                solicitudes = SolicitudRepo.GetSolicitudesByRoleNDateRange(role, Convert.ToDateTime(desdeTxtBx.Text),
                    Convert.ToDateTime(hastaTxtBx.Text));

            else if (!string.IsNullOrEmpty(desdeTxtBx.Text) && string.IsNullOrEmpty(hastaTxtBx.Text))
                solicitudes = SolicitudRepo.GetSolicitudesByRoleNDateRange(role, Convert.ToDateTime(desdeTxtBx.Text),
                    Convert.ToDateTime(DateTime.Now));

            return solicitudes;
        }

        private void SetEmpleadoNSolicitud(ref LinkedList<_Solicitud> solicitudes, ref LinkedList<Usuario> empleados)
        {
            switch (rolDDL.SelectedIndex)
            {
                case 1:

                    empleados = UserRepo.GetUsersByRole((int)Rol.TiposRole.PROCESADOR, true);

                    solicitudes = GetSolicitudesByStatusNDateRange(Rol.TiposRole.PROCESADOR);

                    break;

                default:

                    empleados = UserRepo.GetUsersByRole((int)Rol.TiposRole.COORDINADOR, true);

                    solicitudes = GetSolicitudesByStatusNDateRange(Rol.TiposRole.COORDINADOR);

                    break;
            }
        }

        protected void rolDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ExecuteSearch();

            if (rolDDL.SelectedIndex == 0)
            {
                detailsLbl.Text = "Detalle de Producción por Coordinador";

                coorGV.Visible = true;

                procGV.Visible = !coorGV.Visible;

                //ViewState["users"] = UserRepo.GetUsersByRole((int)Rol.TiposRole.COORDINADOR, false);

                if (string.IsNullOrEmpty(desdeTxtBx.Text))
                    coorGV.DataSource = SolicitudRepo.GetSolicitudesCompletadasByRole(Rol.TiposRole.COORDINADOR);

                else if (!string.IsNullOrEmpty(desdeTxtBx.Text) && !string.IsNullOrEmpty(hastaTxtBx.Text))
                    coorGV.DataSource = SolicitudRepo.GetSolicitudesByRoleNDateRange(Rol.TiposRole.COORDINADOR,
                        Convert.ToDateTime(desdeTxtBx.Text), Convert.ToDateTime(hastaTxtBx.Text));

                else if (!string.IsNullOrEmpty(desdeTxtBx.Text) && string.IsNullOrEmpty(hastaTxtBx.Text))
                    coorGV.DataSource = SolicitudRepo.GetSolicitudesByRoleNDateRange(Rol.TiposRole.COORDINADOR,
                        Convert.ToDateTime(desdeTxtBx.Text), Convert.ToDateTime(DateTime.Now));

                coorGV.DataBind();
            }

            else
            {
                detailsLbl.Text = "Detalle de Producción por Procesador";

                coorGV.Visible = false;

                procGV.Visible = !coorGV.Visible;

                //ViewState["users"] = UserRepo.GetUsersByRole((int)Rol.TiposRole.PROCESADOR, false);

                if (string.IsNullOrEmpty(desdeTxtBx.Text))
                    procGV.DataSource = SolicitudRepo.GetSolicitudesCompletadasByRole(Rol.TiposRole.PROCESADOR);

                else if (!string.IsNullOrEmpty(desdeTxtBx.Text) && !string.IsNullOrEmpty(hastaTxtBx.Text))
                    procGV.DataSource = SolicitudRepo.GetSolicitudesByRoleNDateRange(Rol.TiposRole.PROCESADOR,
                        Convert.ToDateTime(desdeTxtBx.Text), Convert.ToDateTime(hastaTxtBx.Text));

                else if (!string.IsNullOrEmpty(desdeTxtBx.Text) && string.IsNullOrEmpty(hastaTxtBx.Text))
                    procGV.DataSource = SolicitudRepo.GetSolicitudesByRoleNDateRange(Rol.TiposRole.PROCESADOR,
                        Convert.ToDateTime(desdeTxtBx.Text), Convert.ToDateTime(DateTime.Now));

                procGV.DataBind();
            }


        }

        protected void coorGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //LinkedList<Usuario> users = (LinkedList<Usuario>)ViewState["users"];

                //e.Row.Cells[0].Text = users.ElementAt(Convert.ToInt32(e.Row.Cells[0].Text)).GetNombreCompleto();

                if (!string.IsNullOrEmpty(e.Row.Cells[2].Text))
                    e.Row.Cells[2].Text = e.Row.Cells[2].Text.Split(' ')[0];

                if (!string.IsNullOrEmpty(e.Row.Cells[3].Text))
                    e.Row.Cells[3].Text = e.Row.Cells[3].Text.Split(' ')[0];

                DateTime fechaIni = Convert.ToDateTime(e.Row.Cells[2].Text),
                    fechaFinal = Convert.ToDateTime(e.Row.Cells[3].Text);

                e.Row.Cells[4].Text = CalcDateDiff(fechaIni.Date, fechaFinal.Date, true).ToString();
            }

            else if (e.Row.RowType == DataControlRowType.EmptyDataRow)
            {
                Cosmetic cosmetic = (Cosmetic)Session["Cosmetic"];

                ((Label)e.Row.Controls[0].Controls[0].FindControl("emptyLbl")).ForeColor
                    = ColorTranslator.FromHtml(cosmetic.LabelForeColor);
            }
        }

        protected void procGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //LinkedList<Usuario> users = (LinkedList<Usuario>)ViewState["users"];

                //e.Row.Cells[0].Text = users.ElementAt(Convert.ToInt32(e.Row.Cells[0].Text) - 1).GetNombreCompleto();

                if (!string.IsNullOrEmpty(e.Row.Cells[2].Text))
                    e.Row.Cells[2].Text = e.Row.Cells[2].Text.Split(' ')[0];

                if (!string.IsNullOrEmpty(e.Row.Cells[3].Text))
                    e.Row.Cells[3].Text = e.Row.Cells[3].Text.Split(' ')[0];

                DateTime fechaTrabajo = Convert.ToDateTime(e.Row.Cells[3].Text),
                fechaTramite = Convert.ToDateTime(e.Row.Cells[2].Text);

                e.Row.Cells[4].Text = CalcDateDiff(fechaTramite.Date, fechaTrabajo.Date, true).ToString();
            }

            else if (e.Row.RowType == DataControlRowType.EmptyDataRow)
            {
                Cosmetic cosmetic = (Cosmetic)Session["Cosmetic"];

                ((Label)e.Row.Controls[0].Controls[0].FindControl("emptyLbl")).ForeColor
                    = ColorTranslator.FromHtml(cosmetic.LabelForeColor);
            }
        }

        protected void procGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            procGV.PageIndex = e.NewPageIndex;

            procGV.DataBind();
        }

        protected void coorGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            coorGV.PageIndex = e.NewPageIndex;

            coorGV.DataBind();
        }

        /// <summary>
        /// Calculates difference between two dates excluding weekends
        /// </summary>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <param name="semiExclusive">If the verification for weekends should skip the 1st day</param>
        /// <returns>Calculated difference excluding weekends</returns>
        private int CalcDateDiff(DateTime fechaInicial, DateTime fechaFinal, bool semiExclusive)
        {
            int noLaborablesCount = 0;

            DateTime fechaInicialOriginal = fechaInicial;

            if (fechaFinal != fechaInicialOriginal)
            {
                if (semiExclusive)
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

    }
}