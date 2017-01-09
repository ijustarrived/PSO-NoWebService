﻿using PSO.Entities;
using PSO.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PSO.Pages.Dashboard.Configs
{
    public partial class DocsReqConfig : System.Web.UI.Page
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
                #region Verify role permission

                Usuario user = Session["UserObj"] == null ? new Usuario() : (Usuario)Session["UserObj"];

                #region Verify role permission

                if (!user.Role.ViewConfigDocReq)
                {
                    if (string.IsNullOrEmpty(user.Email))
                        Response.Redirect("~/Pages/Login.aspx", true);

                    Response.Redirect("~/Pages/Dashboard/Main.aspx", true);
                } 

                #endregion

                #endregion

                if (!Request.Browser.Browser.Equals("InternetExplorer") && !Request.Browser.Browser.Equals("Safari"))
                {
                    ViewState["deleteCell"] = 2;

                    ViewState["editCell"] = 3;

                    ViewState["updateCell"] = 4;
                }

                //Add one to cells cause select link is added, on the 1st cell, when gv.AutoGenerateSelectButton = true on IE and safari
                else
                {
                    docsGV.AutoGenerateSelectButton = true;

                    ViewState["deleteCell"] = 3;

                    ViewState["editCell"] = 4;

                    ViewState["updateCell"] = 5;
                }
            }
        }

        protected void createDocReqBtn_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = DB.GetLocalConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "INSERT INTO TitulosDocumentos (Nombre) VALUES (@Nombre)";                    

                    cmd.Parameters.AddWithValue("@Nombre", docNameTxtBx.Text.Trim());

                    try
                    {
                        cmd.Connection = conn;

                        conn.Open();

                        cmd.ExecuteNonQuery();
                    }

                    catch (Exception ex)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "DocCreateTitleAlert",
                       string.Format("alert('No se pudo añadir, el documento, a la lista de requeridos. Error: {0}');", ex.Message), true);
                    }

                    docsGV.DataBind();
                }
            }
        }

        protected void docsGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {     
                int deleteCell = Convert.ToInt32(ViewState["deleteCell"]),
                    updateCell = Convert.ToInt32(ViewState["updateCell"]),
                    editCell = Convert.ToInt32(ViewState["editCell"]);

                #region Config delete btn depending on browser.

                if (e.Row.Cells.Count > 0)
                {
                    ButtonField delBtn,
                        updateBtn,
                        editBtn;

                    if (!Request.Browser.Browser.Equals("InternetExplorer") && !Request.Browser.Browser.Equals("Safari"))
                    {
                        delBtn = (ButtonField)docsGV.Columns[deleteCell];

                        editBtn = (ButtonField)docsGV.Columns[editCell];

                        updateBtn = (ButtonField)docsGV.Columns[updateCell];
                    }

                    //Minus 1 cause going through e.Row.Cells includes select cell and going through gv.Columns doesn't include select link
                    else
                    {
                        delBtn = (ButtonField)docsGV.Columns[Math.Abs(deleteCell - 1)];

                        editBtn = (ButtonField)docsGV.Columns[Math.Abs(editCell - 1)];

                        updateBtn = (ButtonField)docsGV.Columns[Math.Abs(updateCell - 1)];
                    }

                    if (!Request.Browser.Browser.Equals("Chrome"))
                    {
                        delBtn.ButtonType = ButtonType.Button;

                        editBtn.ButtonType = ButtonType.Button;

                        updateBtn.ButtonType = ButtonType.Button;
                    }

                    e.Row.Cells[editCell].Visible = docsGV.EditIndex == -1;

                    e.Row.Cells[updateCell].Visible = docsGV.EditIndex != -1;
                }

                #endregion
            }

            else if(e.Row.RowType == DataControlRowType.Header)
            {
                int updateCell = Convert.ToInt32(ViewState["updateCell"]),
                    editCell = Convert.ToInt32(ViewState["editCell"]);

                if (docsGV.EditIndex == -1)
                {
                    e.Row.Cells[updateCell].Attributes.Add("style", "display:none");

                    e.Row.Cells[editCell].Attributes.Remove("style");
                }

                else
                {
                    e.Row.Cells[editCell].Attributes.Add("style", "display:none");

                    e.Row.Cells[updateCell].Attributes.Remove("style");
                }
            }
        }
    }
}