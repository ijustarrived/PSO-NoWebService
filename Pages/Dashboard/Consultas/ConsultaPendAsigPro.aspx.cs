using PSO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PSO.Pages.Dashboard.Consultas
{
    public partial class ConsultaPendAsigPro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Verify role permission

            Usuario user = Session["UserObj"] == null ? new Usuario() : (Usuario)Session["UserObj"];

            if (!user.Role.ViewConsuPendAsig)
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
        }

        protected void solicitudesGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(solicitudesGV, "Select$" + e.Row.RowIndex);

                e.Row.ToolTip = "Seleccionar para revisar";

                //e.Row.Cells[3].Text = Pueblo.GetPueblo(Convert.ToInt32(e.Row.Cells[3].Text));

                // -1 cause pueblo ddl, in solicitud, starts in 0 and db starts in 1
                e.Row.Cells[3].Text = Pueblo.GetPueblo(Convert.ToInt32(e.Row.Cells[3].Text) - 1);

                e.Row.Cells[4].Text = e.Row.Cells[4].Text.Split(' ')[0];
            }
        }

        protected void solicitudesGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/Pages/Dashboard/Solicitud.aspx?NumSolicitud={0}",
                solicitudesGV.SelectedRow.Cells[0].Text), true);
        }
    }
}