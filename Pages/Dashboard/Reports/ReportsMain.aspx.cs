using PSO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PSO.Pages.Dashboard.Reports
{
    public partial class ReportsMain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region Breadcrumb setup

                var dashboardPnl = (Panel)Master.FindControl("dashboardLinkPnl");

                dashboardPnl.Controls.Clear();

                HyperLink mainDashLink = new HyperLink(),
                    testLink = new HyperLink();

                mainDashLink.ID = "mainDashLink";

                mainDashLink.NavigateUrl = "~/Pages/Dashboard/Main.aspx";

                mainDashLink.Text = "Inicio";

                dashboardPnl.Controls.Add(mainDashLink);

                #endregion

                Usuario user = Session["UserObj"] == null ? new Usuario() : (Usuario)Session["UserObj"];

                #region Verify role access

                //if (!user.Role.ViewRepAvisosStatus && !user.Role.ViewRepRecVsPen && !user.Role.ViewRepProduc)
                //{
                //    Response.Redirect("~/Pages/Dashboard/Main.aspx", true);
                //}

                if (!user.Role.ViewRepRecVsPen && !user.Role.ViewRepProduc)
                {
                    Response.Redirect("~/Pages/Dashboard/Main.aspx", true);
                }

                //AvisosRecibidosStatusBtn.Visible = user.Role.ViewRepAvisosStatus;

                RecibidosVsProcessBtn.Visible = user.Role.ViewRepRecVsPen;

                productionBtn.Visible = user.Role.ViewRepProduc;

                #endregion
            }
        }

        protected void AvisosRecibidosStatusBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Dashboard/Reports/AvisosStatusReport.aspx", true);
        }

        protected void RecibidosVsProcessBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Dashboard/Reports/RecibidosVsProcReport.aspx", true);
        }

        protected void productionBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Dashboard/Reports/ProductionReport.aspx", true);
        }
    }
}