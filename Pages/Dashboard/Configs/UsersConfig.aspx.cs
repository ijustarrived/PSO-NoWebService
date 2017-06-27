using System;
using PSO.Entities;
using PSO.Repositorios;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace PSO.Pages.Dashboard.Configs
{
    public partial class UsersConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Verify role permission

            Usuario user = Session["UserObj"] == null ? new Usuario() : (Usuario)Session["UserObj"];

            if (!user.Role.ViewConfigUser)
            {
                if (string.IsNullOrEmpty(user.Email))
                    Response.Redirect("~/Pages/Login.aspx", true);

                Response.Redirect("~/Pages/Dashboard/Main.aspx", true);
            }

            #endregion

            #region Set cosmetics

            Cosmetic cosmetic = (Cosmetic)Session["Cosmetic"];

            tipoUserLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            userGV.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            userGV.FooterStyle.BackColor = ColorTranslator.FromHtml(cosmetic.TitleBackColor);

            userGV.PagerStyle.BackColor = ColorTranslator.FromHtml(cosmetic.TitleBackColor);

            userGV.HeaderStyle.BackColor = ColorTranslator.FromHtml(cosmetic.TitleBackColor);

            ScriptManager.RegisterStartupScript(this, GetType(), "InvokeChangeClinetSideColors",
                                   string.Format("ChangeClinetSideColors('{0}', '{1}');",
                                   cosmetic.LabelForeColor, cosmetic.TitleBackColor), true);

            ViewState["_Cosmetic"] = cosmetic;

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

            secondDashLink.NavigateUrl = "~/Pages/Dashboard/Configs/ConfigsMain.aspx";

            secondDashLink.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            secondDashLink.Text = "Configuraci&oacute;n";

            dashboardPnl.Controls.Add(secondDashLink);

            #endregion

            #endregion

            tipoUserDDL_SelectedIndexChanged(tipoUserDDL, EventArgs.Empty);
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

        protected void tipoUserDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (((DropDownList)sender).SelectedIndex)
            {
                case 0:

                    userGV.DataSource = UserRepo.GetUsersByRole((int)Usuario.TiposUsuarios.EXTERNO, true);

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
                #region Set row select

                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(userGV, "Select$" + e.Row.RowIndex);

                e.Row.ToolTip = "Seleccionar para revisar";

                #endregion

                LinkedList<Usuario> users = (LinkedList<Usuario>)userGV.DataSource;

                /*
                 * Si cada page tiene 10 items max. Si al page num * 10 Voy a tener los mimos items que etan en el page,
                pero la lista
                */
                int index = userGV.PageIndex * 10 + e.Row.RowIndex;

                e.Row.Cells[0].Text = users.ElementAt(index).GetNombreCompleto();

                // -1 cause pueblo ddl, in solicitud, starts in 0 and db starts in 1
                e.Row.Cells[2].Text = Pueblo.GetPueblo(Convert.ToInt32(e.Row.Cells[2].Text) - 1);

                e.Row.Cells[4].Text = users.ElementAt(index).Role.RoleType.ToString();

                #region Set activeBtn properties

                if (userGV.Rows.Count > 0)
                {
                    int activeCell = 5;

                    Button activeBtn = (Button)userGV.Rows[e.Row.RowIndex - 1].Cells[activeCell].FindControl("activateBtn");

                    activeBtn.ForeColor = ColorTranslator.FromHtml(((Cosmetic)ViewState["_Cosmetic"]).LabelForeColor);

                    if (users.ElementAt(index - 1).Activo)
                        activeBtn.Text = "Desactivar";

                    else
                        activeBtn.Text = "Activar";
                }

                #endregion
            }


            if (e.Row.RowType == DataControlRowType.Pager)
            {
                LinkedList<Usuario> users = (LinkedList<Usuario>)userGV.DataSource;

                int index = userGV.PageIndex * 10 + (userGV.Rows.Count - 1);

                int activeCell = 5;

                Button activeBtn = (Button)userGV.Rows[userGV.Rows.Count - 1].Cells[activeCell].FindControl("activateBtn");

                if (users.ElementAt(index).Activo)
                    activeBtn.Text = "Desactivar";

                else
                    activeBtn.Text = "Activar";
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

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            GridViewRow selectedRow = (GridViewRow)((Button)sender).NamingContainer;

            int listIndex = userGV.PageIndex * 10 + selectedRow.RowIndex;

            //Usando el ds del gv para llenar la lista
            LinkedList<Usuario> users = (LinkedList<Usuario>)userGV.DataSource;

            users.ElementAt(listIndex).Activo = !users.ElementAt(listIndex).Activo;

            UserRepo.Update(users.ElementAt(listIndex), users.ElementAt(listIndex).Email);

            userGV.DataBind();
        }

        protected void activateLink_Click(object sender, EventArgs e)
        {

        }

        protected void activateBtn_Command(object sender, CommandEventArgs e)
        {
            Unnamed_Click(sender, EventArgs.Empty);
        }
    }
}