﻿using PSO.Entities;
using PSO.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PSO.Pages.Dashboard.Consultas
{
    public partial class ConsultaProcesador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Verify role permission

            Usuario user = Session["UserObj"] == null ? new Usuario() : (Usuario)Session["UserObj"];

            if (!user.Role.ViewConsuProc)
            {
                if (string.IsNullOrEmpty(user.Email))
                    Response.Redirect("~/Pages/Login.aspx", true);

                Response.Redirect("~/Pages/Dashboard/Main.aspx", true);
            }

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

            dashboardPnl.Controls.Add(mainDashLink);
            #endregion

            #region 2nd link
            secondDashlbl.Text = " > ";

            dashboardPnl.Controls.Add(secondDashlbl);

            secondDashLink.NavigateUrl = "~/Pages/Dashboard/Consultas/ConsultasMain.aspx";

            secondDashLink.Text = "Consultas";

            dashboardPnl.Controls.Add(secondDashLink);
            #endregion

            #endregion

            if(!IsPostBack)
            {
                LinkedList<Usuario> procesadores = UserRepo.GetUsersByRole((int)Rol.TiposRole.PROCESADOR);

                for (int i = 0; i < procesadores.Count; i++)
                {
                    filterDDL.Items.Add(procesadores.ElementAt(i).GetNombreCompleto());

                    if (procesadores.ElementAt(i).GetNombreCompleto() == user.GetNombreCompleto())
                    {
                        filterDDL.SelectedValue = user.GetNombreCompleto();

                        filterDDL.Enabled = false;
                    }
                }
            }
        }

        protected void solicitudesGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(solicitudesGV, "Select$" + e.Row.RowIndex);

                e.Row.ToolTip = "Seleccionar para revisar";

                e.Row.Cells[3].Text = Pueblo.GetPueblo(Convert.ToInt32(e.Row.Cells[3].Text));

                e.Row.Cells[4].Text = e.Row.Cells[4].Text.Split(' ')[0];
            }
        }

        protected void solicitudesGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/Pages/Dashboard/Solicitud.aspx?NumSolicitud={0}",
                solicitudesGV.SelectedRow.Cells[0].Text), true);
        }

        protected void filterDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            solicitudesGV.DataBind();
        }

        protected void solicitudesSQLDS_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            string and = string.Empty;

            if (filterDDL.SelectedIndex != 0)  // -1 cause 0 here is default but 0 on solicitud is a procesador
                and = string.Format("AND ProcesadorID = {0}", (filterDDL.SelectedIndex - 1));

            e.Command.CommandText = e.Command.CommandText.Replace("@AND", and);

        }

        protected void solicitudesGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            solicitudesGV.PageIndex = e.NewPageIndex;

            solicitudesGV.DataBind();
        }
    }
}