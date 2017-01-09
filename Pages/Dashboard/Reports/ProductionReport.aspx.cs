﻿using PSO.Entities;
using PSO.Repositorios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace PSO.Pages.Dashboard.Reports
{
    public partial class ProductionReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario user = Session["UserObj"] == null ? new Usuario() : (Usuario)Session["UserObj"];

            if (!user.Role.ViewRepProduc)
            {
                if (string.IsNullOrEmpty(user.Email))
                    Response.Redirect("~/Pages/Login.aspx", true);

                Response.Redirect("~/Pages/Dashboard/Main.aspx", true);
            }

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
                ExecuteSearch();
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
                        if (solicitudes.ElementAt(i2).ProcesadorId == i)
                        {
                            employeeSolicitudesCount++;
                        }
                    }

                    else
                    {
                        if (solicitudes.ElementAt(i2).CoordinadorID == i)
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
            Array colors = Enum.GetValues(typeof(KnownColor));

            Random colorIndex = new Random();

            for (int i = 0; i < EmployeeNResults.Count; i++)
            {
                DataPoint point = new DataPoint();

                point.AxisLabel = EmployeeNResults.Keys.ElementAt(i);

                point.SetValueY(EmployeeNResults.Values.ElementAt(i));

                point.Label = EmployeeNResults.Values.ElementAt(i).ToString();

                point.Color = Color.FromKnownColor((KnownColor)colors.GetValue(colorIndex.Next(0, colors.Length - 1)));

                productionChrt.Series[0].Points.Add(point);                
            }
        }

        protected void searchBtn_Click(object sender, EventArgs e)
        {
            ExecuteSearch();
        }

        private LinkedList<_Solicitud> GetSolicitudesByStatusNDateRange(Rol.TiposRole role)
        {
            LinkedList<_Solicitud> solicitudes = new LinkedList<_Solicitud>();

            if (string.IsNullOrEmpty(desdeTxtBx.Text))
                solicitudes = SolicitudRepo.GetSolicitudesByRole(role);

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

                    empleados = UserRepo.GetUsersByRole((int)Rol.TiposRole.PROCESADOR);

                    solicitudes = GetSolicitudesByStatusNDateRange(Rol.TiposRole.PROCESADOR);

                    break;

                default:

                    empleados = UserRepo.GetUsersByRole((int)Rol.TiposRole.COORDINADOR);

                    solicitudes = GetSolicitudesByStatusNDateRange(Rol.TiposRole.COORDINADOR);

                    break;
            }
        }

        protected void rolDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExecuteSearch();
        }
    }
}