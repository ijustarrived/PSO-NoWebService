﻿using System;
using PSO.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PSO.Pages.Dashboard.Configs
{
    public partial class RolesConfig : System.Web.UI.Page
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

            Usuario user = Session["UserObj"] == null ? new Usuario() : (Usuario)Session["UserObj"];

            #region Verify role permission

            if (!user.Role.ViewConfigRole)
            {
                if (string.IsNullOrEmpty(user.Email))
                    Response.Redirect("~/Pages/Login.aspx", true);

                Response.Redirect("~/Pages/Dashboard/Main.aspx", true);
            } 

            #endregion

            #region Intantiate roles list

            LinkedList<Rol> roles = new LinkedList<Rol>();

            roles.AddLast(new Rol(Rol.TiposRole.ADMINISTRADOR));

            roles.AddLast(new Rol(Rol.TiposRole.COORDINADOR));

            roles.AddLast(new Rol(Rol.TiposRole.EXTERNO));

            roles.AddLast(new Rol(Rol.TiposRole.PROCESADOR));

            roles.AddLast(new Rol(Rol.TiposRole.SUPERVISOR)); 

            #endregion

            roleGV.DataSource = roles;

            roleGV.DataBind();
        }

        protected void roleGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("RolePermisos.aspx?RoleID={0}",
               roleGV.SelectedRow.Cells[0].Text), true);
        }

        protected void roleGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(roleGV, "Select$" + e.Row.RowIndex);

                e.Row.ToolTip = "Seleccionar para revisar";
            }
        }
    }
}