using PSO.Entities;
using PSO.Repositorios;
using System;
using System.Collections.Generic;
using System.Drawing;
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

            #region Set cosmetics

            Cosmetic cosmetic = (Cosmetic)Session["Cosmetic"];

            Page.Title = cosmetic.ConsultaSuperTitle;

            totalAvisosLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            totalPagesLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            solicitudesGV.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            solicitudesGV.FooterStyle.BackColor = ColorTranslator.FromHtml(cosmetic.TitleBackColor);

            solicitudesGV.PagerStyle.BackColor = ColorTranslator.FromHtml(cosmetic.TitleBackColor);

            solicitudesGV.HeaderStyle.BackColor = ColorTranslator.FromHtml(cosmetic.TitleBackColor);

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

            secondDashLink.NavigateUrl = "~/Pages/Dashboard/Consultas/ConsultasMain.aspx";

            secondDashLink.Text = "Consultas";

            secondDashLink.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            dashboardPnl.Controls.Add(secondDashLink);

            #endregion

            #endregion

            #region Set total solicitudes and pages

            if (solicitudesGV.Rows.Count != 0 && string.IsNullOrEmpty(totalAvisosLbl.Text))
            {
                totalPagesLbl.Text = string.Format("Total de Paginas: {0}",
                    solicitudesGV.PageCount);

                solicitudesGV.AllowPaging = false;

                solicitudesGV.AllowSorting = false;

                solicitudesGV.DataBind();

                totalAvisosLbl.Text = string.Format("Total de Solicitudes: {0}",
                    solicitudesGV.Rows.Count);

                solicitudesGV.AllowPaging = true;

                solicitudesGV.AllowSorting = true;

                solicitudesGV.DataBind();
            }

            #endregion
        }

        /// <summary>
        /// Runs only on the fake timeout interval
        /// </summary>
        /// <param name="lockedId"></param>
        [System.Web.Services.WebMethod]
        public static void ReleaseAllLkdSolicitudes(int lockedId)
        {
            SolicitudRepo.ReleaseAllLockedSolicitudes(lockedId);
        }

        /// <summary>
        /// Runs only on the fake timeout interval
        /// </summary>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static string KeepAlive()
        {
            return DateTime.Now.ToShortDateString();
        }

        protected void solicitudesGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(solicitudesGV,
                    "Select$" + e.Row.RowIndex);

                e.Row.ToolTip = "Seleccionar para revisar";

                // -1 cause pueblo ddl, in solicitud, starts in 0 and db starts in 1
                e.Row.Cells[3].Text = Pueblo.GetPueblo(Convert.ToInt32(e.Row.Cells[3].Text) - 1);

                e.Row.Cells[4].Text = e.Row.Cells[4].Text.Split(' ')[0];
            }

            else if (e.Row.RowType == DataControlRowType.EmptyDataRow)
            {
                Cosmetic cosmetic = (Cosmetic)Session["Cosmetic"];

                ((Label)e.Row.Controls[0].Controls[0].FindControl("emptyLbl")).ForeColor
                    = ColorTranslator.FromHtml(cosmetic.LabelForeColor);
            }
        }

        protected void solicitudesGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/Pages/Dashboard/Solicitud.aspx?NumSolicitud={0}",
                solicitudesGV.SelectedRow.Cells[0].Text), true);
        }
    }
}