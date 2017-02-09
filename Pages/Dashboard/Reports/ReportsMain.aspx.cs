using PSO.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
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
                Usuario user = Session["UserObj"] == null ? new Usuario() : (Usuario)Session["UserObj"];

                #region Verify role access

                if (!user.Role.ViewRepRecVsPen && !user.Role.ViewRepProduc && !user.Role.ViewReportIndicadores)
                {
                    Response.Redirect("~/Pages/Dashboard/Main.aspx", true);
                }

                RecibidosVsProcessBtn.Visible = user.Role.ViewRepRecVsPen;

                productionBtn.Visible = user.Role.ViewRepProduc;

                indicadorBtn.Visible = user.Role.ViewReportIndicadores;

                #endregion

                #region Set Cosmetics

                Cosmetic cosmetic = (Cosmetic)Session["Cosmetic"];

                productionBtn.Text = cosmetic.ReportProduccionTitle;

                productionBtn.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

                RecibidosVsProcessBtn.Text = cosmetic.ReportComparacionTitle;

                RecibidosVsProcessBtn.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

                indicadorBtn.Text = cosmetic.IndicadoresProductividadTitle;

                indicadorBtn.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

                productionBtn.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

                #endregion

                #region Breadcrumb setup

                var dashboardPnl = (Panel)Master.FindControl("dashboardLinkPnl");

                dashboardPnl.Controls.Clear();

                HyperLink mainDashLink = new HyperLink(),
                    testLink = new HyperLink();

                mainDashLink.ID = "mainDashLink";

                mainDashLink.NavigateUrl = "~/Pages/Dashboard/Main.aspx";

                mainDashLink.Text = "Inicio";

                dashboardPnl.Controls.Add(mainDashLink);

                mainDashLink.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

                #endregion
            }
        }

        protected void RecibidosVsProcessBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Dashboard/Reports/RecibidosVsProcReport.aspx", true);
        }

        protected void productionBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Dashboard/Reports/ProductionReport.aspx", true);
        }

        protected void indicadorBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Dashboard/Reports/TrabajoReport.aspx", true);
        }
    }
}