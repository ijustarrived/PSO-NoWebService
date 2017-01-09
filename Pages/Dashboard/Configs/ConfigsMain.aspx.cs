using PSO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PSO.Pages.Dashboard.Configs
{
    public partial class ConfigsMain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                #region Breadcrumb setup

                var dashboardPnl = (Panel)Master.FindControl("dashboardLinkPnl");

                dashboardPnl.Controls.Clear();

                HyperLink mainDashLink = new HyperLink();

                mainDashLink.ID = "mainDashLink";

                mainDashLink.NavigateUrl = "~/Pages/Dashboard/Main.aspx";

                mainDashLink.Text = "Inicio";

                dashboardPnl.Controls.Add(mainDashLink);

                #endregion

                #region Verify role access

                Usuario user = Session["UserObj"] == null ? new Usuario() : (Usuario)Session["UserObj"];

                if (!user.Role.ViewConfigDocReq && !user.Role.ViewConfigUser
                    && !user.Role.ViewConfigRole)
                {
                    if (string.IsNullOrEmpty(user.Email))
                        Response.Redirect("~/Pages/Login.aspx", true);

                    Response.Redirect("~/Pages/Dashboard/Main.aspx", true);
                }

                docsReqBtn.Visible = user.Role.ViewConfigDocReq;

                usersBtn.Visible = user.Role.ViewConfigUser;

                rolesBtn.Visible = user.Role.ViewConfigRole;

                #endregion
            }
        }

        protected void docsReqBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Dashboard/Configs/DocsReqConfig.aspx", true);
        }

        protected void rolesBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Dashboard/Configs/RolesConfig.aspx", true);
        }

        protected void usersBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Dashboard/Configs/UsersConfig.aspx", true);
        }
    }
}