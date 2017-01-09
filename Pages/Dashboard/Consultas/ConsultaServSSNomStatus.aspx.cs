﻿using PSO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PSO.Pages.Dashboard.Consultas
{
    public partial class ConsultaServSSNomStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Verify role permission

            Usuario user = Session["UserObj"] == null ? new Usuario() : (Usuario)Session["UserObj"];

            if (!user.Role.ViewConsuSolicitud)
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

            #region Set filter css

            switch (filterDDL.SelectedIndex)
            {
                case 1: // nombre

                    searchTxtBx.Attributes["style"] = "display:initial;";

                    statusDDL.Attributes["style"] = "display:none;";

                    break;

                case 2: //ss

                    searchTxtBx.Attributes["style"] = "display:initial;";

                    statusDDL.Attributes["style"] = "display:none;";

                    break;

                case 3: // status

                    searchTxtBx.Attributes["style"] = "display:none;";

                    statusDDL.Attributes["style"] = "display:initial;";

                    break;

                default:

                    searchTxtBx.Attributes["style"] = "display:none;";

                    statusDDL.Attributes["style"] = "display:none;";

                    break;
            } 

            #endregion
        }

        protected void searchBtn_Click(object sender, EventArgs e)
        {
            solicitudesGV.DataBind();
        }

        protected void solicitudesGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(solicitudesGV, "Select$" + e.Row.RowIndex);

                e.Row.ToolTip = "Seleccionar para revisar";

                e.Row.Cells[4].Text = Pueblo.GetPueblo(Convert.ToInt32(e.Row.Cells[4].Text));

                if (!e.Row.Cells[1].Text.Contains("-"))
                {
                    string ssn = Usuario.DecryptWord(e.Row.Cells[1].Text),
                                ssnWithDashes = string.Empty;

                    for (int i = 0; i < ssn.Length; i++)
                    {
                        ssnWithDashes += ssn[i];

                        if (i == 2 || i == 4)
                            ssnWithDashes += "-";
                    }

                    e.Row.Cells[1].Text = ssnWithDashes; 
                }
            }
        }

        protected void solicitudesGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/Pages/Dashboard/Solicitud.aspx?NumSolicitud={0}",
               solicitudesGV.SelectedRow.Cells[0].Text), true);
        }

        protected void solicitudesSQLDS_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            string where = string.Empty;

            Usuario user = Session["UserObj"] == null ? new Usuario() : (Usuario)Session["UserObj"];

            switch (filterDDL.SelectedIndex)
            {
                case 1: // nombre

                    where = string.Format(@"WHERE Nombre LIKE '%{0}%' OR ApellidoPaterno LIKE '%{0}%' 
                        OR ApellidoMaterno LIKE '%{0}%'", searchTxtBx.Text);

                    if (Rol.TiposRole.EXTERNO == user.Role.RoleType)
                        where = string.Format(@"{0} AND Email = '{1}'", where, user.Email);

                    break;

                case 2: //ss

                    where = string.Format("WHERE SeguroSocial = '{0}'", string.IsNullOrEmpty(searchTxtBx.Text) ? string.Empty 
                        : Usuario.EncryptWord(searchTxtBx.Text.Replace("-", string.Empty)));

                    if (Rol.TiposRole.EXTERNO == user.Role.RoleType)
                        where = string.Format(@"{0} AND Nombre = '{1}' AND ApellidoMaterno = '{2}' 
                                        AND ApellidoPaterno = '{3}' AND Email = '{4}'", where, user.Nombre, user.ApellidoMaterno,
                                                user.ApellidoPaterno, user.Email);

                    break;

                case 3: // status

                    //+ 1 cause status enum includes no tramitado as 0 and 0 here = pend revisar
                    where = string.Format("WHERE Status = {0}", (statusDDL.SelectedIndex + 1));

                    if (Rol.TiposRole.EXTERNO == user.Role.RoleType)
                        where = string.Format(@"{0} AND Nombre = '{1}' AND ApellidoMaterno = '{2}' 
                                        AND ApellidoPaterno = '{3}' AND Email = '{4}'", where,
                                        user.Nombre, user.ApellidoMaterno, user.ApellidoPaterno, user.Email);

                    break;
            }

            if (string.IsNullOrEmpty(where) && Rol.TiposRole.EXTERNO == user.Role.RoleType)
            {
                //External can only view his
                where = string.Format(@" WHERE Nombre = '{0}' AND ApellidoMaterno = '{1}' 
                                        AND ApellidoPaterno = '{2}' AND Email = '{3}'",
                                        user.Nombre, user.ApellidoMaterno, user.ApellidoPaterno, user.Email);
            }

            e.Command.CommandText = e.Command.CommandText.Replace("@WHERE", where);
        }

        protected void solicitudesGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            solicitudesGV.PageIndex = e.NewPageIndex;

            searchBtn_Click(sender, EventArgs.Empty);
        }
    }
}