using System;
using PSO.Entities;
using PSO.Repositorios;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PSO.Pages.Dashboard.Configs
{
    public partial class UsersConfig : System.Web.UI.Page
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

            secondDashLink.NavigateUrl = "~/Pages/Dashboard/Configs/ConfigsMain.aspx";

            secondDashLink.Text = "Configuraci&oacute;n";

            dashboardPnl.Controls.Add(secondDashLink);
            #endregion

            #endregion

            if (!IsPostBack)
            {
                Usuario user = Session["UserObj"] == null ? new Usuario() : (Usuario)Session["UserObj"];

                if (!user.Role.ViewConfigUser)
                {
                    if (string.IsNullOrEmpty(user.Email))
                        Response.Redirect("~/Pages/Login.aspx", true);

                    Response.Redirect("~/Pages/Dashboard/Main.aspx", true);
                }
            }

            tipoUserDDL_SelectedIndexChanged(tipoUserDDL, EventArgs.Empty);
        }

        protected void tipoUserDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (((DropDownList)sender).SelectedIndex)
            {
                case 0:

                    userGV.DataSource = UserRepo.GetUsersByRole((int)Usuario.TiposUsuarios.EXTERNO);

                    break;

                default:

                    userGV.DataSource = UserRepo.GetAllEmployeeUsers();

                    break;
            }

            userGV.DataBind();
        }

        protected void userGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(userGV, "Select$" + e.Row.RowIndex);

                e.Row.ToolTip = "Seleccionar para revisar";

                LinkedList<Usuario> users = (LinkedList<Usuario>)userGV.DataSource;

                /*
                 * Si cada page tiene 10 items max. Si al page num * 10 Voy a tener los mimos items que etan en el page,
                pero la lista
                */
                int index = userGV.PageIndex * 10 + e.Row.RowIndex;

                e.Row.Cells[0].Text = users.ElementAt(index).GetNombreCompleto();

                //e.Row.Cells[2].Text = Pueblo.GetPueblo(Convert.ToInt32(e.Row.Cells[2].Text));

                // -1 cause pueblo ddl, in solicitud, starts in 0 and db starts in 1
                e.Row.Cells[2].Text = Pueblo.GetPueblo(Convert.ToInt32(e.Row.Cells[2].Text) - 1);

                e.Row.Cells[4].Text = users.ElementAt(e.Row.RowIndex).Role.RoleType.ToString();
            }
        }

        protected void userGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/Pages/Register?Email={0}",
               userGV.SelectedRow.Cells[1].Text), true);
        }

        protected void userGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            userGV.PageIndex = e.NewPageIndex;

            userGV.DataBind();
        }
    }
}