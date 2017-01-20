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
    public partial class RecibidosVsProcReport : System.Web.UI.Page
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

            if (!IsPostBack)
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
            }
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
            LinkedList<_Solicitud> solicitudes = new LinkedList<_Solicitud>(),
                solicitudesProcesadas = new LinkedList<_Solicitud>();

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

            for (int i = 0; i < solicitudes.Count; i++)
            {
                if (solicitudes.ElementAt(i).Status == _Solicitud.Statuses.APROBADA 
                    || solicitudes.ElementAt(i).Status == _Solicitud.Statuses.DENEGADA)
                {
                    solicitudesProcesadas.AddLast(solicitudes.ElementAt(i));
                }
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

                    where = string.Format("{0} @AND CoordinadorID = {1} AND FechaRevision <> CONVERT(datetime, '12/31/9999 23:59:59.997')",
                        where, searchDDL.SelectedIndex);

                    break;

                //Proc
                case 2:

                    //where = string.Format(@"{0} @AND (ProcesadorID = {1}) 
                    //                            AND (FechaAsigProcesador <> CONVERT(datetime, '12/31/9999 23:59:59.997'))", 
                    //                            where, searchDDL.SelectedIndex);

                    // + 1 cause search ddl starts in 1 and procesador ddl, in solicitud, starts in 0
                    where = string.Format(@"{0} @AND (ProcesadorID = {1}) 
                                                AND (FechaAsigProcesador <> CONVERT(datetime, '12/31/9999 23:59:59.997'))",
                                                where, searchDDL.SelectedIndex + 1);

                    break;
            }

            #endregion

            #region Status DLL

            switch(statusSearchDDL.SelectedIndex)
            {
                //Pending
                case 1:

                    int statusId = 0;

                    if(filterDDL.SelectedIndex > 1)
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

            ViewState["SolicitudCount"] = solicitudes.Count;

            ViewState["ProcesadasCount"] = solicitudesProcesadas.Count;

            #region Change this to a real getter

            SetChartData(solicitudes.Count, solicitudesProcesadas.Count);

            //SetChartData(solicitudes.Count, fakeProcesadasSolicitudes.Count); 

            #endregion

            e.Command.CommandText = e.Command.CommandText.Replace("@WHERE", where);
        }

        protected void recievedGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {     
                _Solicitud solicitud = SolicitudRepo.GetSolicitudByNumSolicitud(e.Row.Cells[0].Text);

                if (solicitud.FechaRevision.Date.Year == 9999)
                    e.Row.Cells[4].Text = string.Empty;

                else
                {
                    LinkedList<Usuario> coordinadores = UserRepo.GetUsersByRole((int)Rol.TiposRole.COORDINADOR);

                    e.Row.Cells[4].Text = coordinadores.ElementAt(Convert.ToInt32(e.Row.Cells[4].Text)).GetNombreCompleto();
                }

                DateTime fechaTrabajo = Convert.ToDateTime(e.Row.Cells[2].Text),
                fechaTramite = Convert.ToDateTime(e.Row.Cells[1].Text);

                //don't display default date and substract current date and tramite
                if (fechaTrabajo.Date.Date.Year == 9999)
                {
                    e.Row.Cells[2].Text = string.Empty;

                    TimeSpan span = DateTime.Now.Subtract(fechaTramite);

                    e.Row.Cells[3].Text = ((int)span.TotalDays).ToString();

                    //Esta va pero para el demo no
                    e.Row.Cells[5].Text = string.Empty;
                }

                else
                {
                    LinkedList<Usuario> procesadores = UserRepo.GetUsersByRole((int)Rol.TiposRole.PROCESADOR);

                    //e.Row.Cells[5].Text = procesadores.ElementAt(Convert.ToInt32(e.Row.Cells[5].Text)).GetNombreCompleto();

                    e.Row.Cells[5].Text = procesadores.ElementAt(Convert.ToInt32(e.Row.Cells[5].Text) - 1).GetNombreCompleto();
                }

                #region Esto es solo para el demo

                //if (solicitud.FechaAsigProcesador.Date.Year != 9999)
                //{
                //    LinkedList<Usuario> procesadores = UserRepo.GetUsersByRole((int)Rol.TiposRole.PROCESADOR);

                //    e.Row.Cells[5].Text = procesadores.ElementAt(Convert.ToInt32(e.Row.Cells[5].Text)).GetNombreCompleto();

                //    e.Row.Cells[2].Text = "19/12/2016";
                //} 

                #endregion
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
        }

        protected void filterDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList _filterDDL = (DropDownList)sender;

            searchDDL.Visible = _filterDDL.SelectedIndex > 0;

            statusDiv.Visible = filterDDL.SelectedIndex == 2;

            switch (filterDDL.SelectedIndex)
            {
                //Coor
                case 1:

                    #region Instantiate searchDDL

                    LinkedList<Usuario> coordinadores = UserRepo.GetUsersByRole((int)Rol.TiposRole.COORDINADOR);

                    searchDDL.Items.Clear();

                    for (int i = 0; i < coordinadores.Count; i++)
                    {
                        searchDDL.Items.Add(coordinadores.ElementAt(i).GetNombreCompleto());
                    }

                    #endregion

                    break;

                //Proc
                case 2:
                    #region Instantiate searchDDL

                    LinkedList<Usuario> procesadores = UserRepo.GetUsersByRole((int)Rol.TiposRole.PROCESADOR);

                    searchDDL.Items.Clear();

                    for (int i = 0; i < procesadores.Count; i++)
                    {
                        searchDDL.Items.Add(procesadores.ElementAt(i).GetNombreCompleto());
                    }

                    #endregion

                    break;                   
            }

            SetChartData((int)ViewState["SolicitudCount"], (int)ViewState["ProcesadasCount"]);
        }
    }
}