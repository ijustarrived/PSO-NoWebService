using PSO.Entities;
using PSO.Repositorios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PSO.Pages.Dashboard.Configs
{
    public partial class CustomizeAllPages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Cosmetic cosmetic = (Cosmetic)Session["Cosmetic"];

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

            #region Verify role permission

            Usuario user = Session["UserObj"] == null ? new Usuario() : (Usuario)Session["UserObj"];

            if (!user.Role.EditCustomizationPage)
            {
                if (string.IsNullOrEmpty(user.Email))
                    Response.Redirect("~/Pages/Login.aspx", true);

                Response.Redirect("~/Pages/Dashboard/Main.aspx", true);
            }

            #endregion

            if (!IsPostBack)
            {
                cosmetic = (Cosmetic)Session["Cosmetic"];

                colorVersionsDDL.SelectedIndex = (int)cosmetic.ColorVersion;

                #region Set lbl colors

                //consultaLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

                //dashboardLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

                //fondoLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

                //linkLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

                //logoLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

                //miscLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

                //reportLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

                //textoLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

                //titleColorLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

                //versionesLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

                //labelForeColorBtn.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

                //linkForeColorBtn.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

                //saveBtn.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

                #endregion

                #region Set title div's backcolor

                //colorTitleDiv.Style.Add("background-color", cosmetic.TitleBackColor);

                //logoTitleDiv.Style.Add("background-color", cosmetic.TitleBackColor);

                //TituloTitleDiv.Style.Add("background-color", cosmetic.TitleBackColor);

                #endregion

                SetPageColors(cosmetic);

                logoImg.ImageUrl = cosmetic.LogoPath;

                #region Set titles

                consultaCoorTitleTxtBx.Text = cosmetic.ConsultaCoorTitle;

                completadasMesDiasTitleTxtBx.Text = cosmetic.CompletadasMesDiasTitle;

                consultaProcTitleTxtBx.Text = cosmetic.ConsultaProcTitle;

                consultaSolicitudTitleTxtBx.Text = cosmetic.ConsultaSolicitudTitle;

                consultaSuperTitleTxtBx.Text = cosmetic.ConsultaSuperTitle;

                historyCompletadasTitleTxtBx.Text = cosmetic.HistoryCompletadasTitle;

                historyRecibidasTitleTxtBx.Text = cosmetic.HistoryRecibidasTitle;

                indicadoresProductividadTitleTxtBx.Text = cosmetic.IndicadoresProductividadTitle;

                reportComparacionTitleTxtBx.Text = cosmetic.ReportComparacionTitle;

                reportProduccionTitleTxtBx.Text = cosmetic.ReportProduccionTitle;

                solicitudesStatusTitleTxtBx.Text = cosmetic.SolicitudesStatusTitle;

                solicitudTitleTxtBx.Text = cosmetic.SolicitudTitle;

                #endregion
            }

            else
            {
                #region If postback

                /*
                        * Do the ddl post back programming here cause this always
                        * runs before any other event. Si lo hubiera hecho en otro evento, there would've been null values
                        */

                Cosmetic defaultCosmetic = new Cosmetic();

                switch (colorVersionsDDL.SelectedIndex)
                {
                    case 0:

                        cosmetic.LabelForeColor = "#79256E";

                        cosmetic.LinkForeColor = "#79256E";

                        cosmetic.TitleBackColor = "#616161";

                        #region Set title to default values

                        completadasMesDiasTitleTxtBx.Text = defaultCosmetic.CompletadasMesDiasTitle;

                        consultaCoorTitleTxtBx.Text = defaultCosmetic.ConsultaCoorTitle;

                        consultaProcTitleTxtBx.Text = defaultCosmetic.ConsultaProcTitle;

                        consultaSolicitudTitleTxtBx.Text = defaultCosmetic.ConsultaSolicitudTitle;

                        consultaSuperTitleTxtBx.Text = defaultCosmetic.ConsultaSuperTitle;

                        historyCompletadasTitleTxtBx.Text = defaultCosmetic.HistoryCompletadasTitle;

                        historyRecibidasTitleTxtBx.Text = defaultCosmetic.HistoryRecibidasTitle;

                        indicadoresProductividadTitleTxtBx.Text = defaultCosmetic.IndicadoresProductividadTitle;

                        reportComparacionTitleTxtBx.Text = defaultCosmetic.ReportComparacionTitle;

                        reportProduccionTitleTxtBx.Text = defaultCosmetic.ReportProduccionTitle;

                        solicitudesStatusTitleTxtBx.Text = defaultCosmetic.SolicitudesStatusTitle;

                        solicitudTitleTxtBx.Text = defaultCosmetic.SolicitudTitle;

                        #endregion

                        logoImg.ImageUrl = defaultCosmetic.LogoPath;

                        cosmetic.LogoPath = defaultCosmetic.LogoPath;

                        break;

                    //red
                    case 1:

                        cosmetic.LabelForeColor = "#BA0000";

                        cosmetic.LinkForeColor = cosmetic.LabelForeColor;

                        cosmetic.TitleBackColor = cosmetic.LabelForeColor;

                        break;

                    //green
                    case 2:

                        cosmetic.LabelForeColor = "#008C4F";

                        cosmetic.LinkForeColor = cosmetic.LabelForeColor;

                        cosmetic.TitleBackColor = cosmetic.LabelForeColor;

                        break;

                    //blue
                    default:

                        cosmetic.LabelForeColor = "#006699";

                        cosmetic.LinkForeColor = cosmetic.LabelForeColor;

                        cosmetic.TitleBackColor = cosmetic.LabelForeColor;

                        break;
                }

                SetPageColors(cosmetic);

                ViewState["DemoCosmetic"] = cosmetic;

                #endregion
            }

            #region Set breadcrumb colors

            secondDashlbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            secondDashLink.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            mainDashLink.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            #endregion
        }

        protected void saveBtn_Click(object sender, EventArgs e)
        {
            #region Instantiate cosmetic

            Cosmetic cosmetic = (Cosmetic)ViewState["DemoCosmetic"];

            cosmetic.LogoPath = logoImg.ImageUrl;

            if (!string.IsNullOrEmpty(logoFileUp.PostedFile.FileName))
                cosmetic.LogoPath = string.Format("{0}/{1}",
                Cosmetic.GetLogoDir(),
                Path.GetFileName(logoFileUp.PostedFile.FileName.Replace("#", string.Empty).Replace("&", string.Empty)));

            cosmetic.ColorVersion = Cosmetic.GetVersion(colorVersionsDDL.SelectedIndex);

            cosmetic.ConsultaCoorTitle = consultaCoorTitleTxtBx.Text;

            cosmetic.CompletadasMesDiasTitle = completadasMesDiasTitleTxtBx.Text;

            cosmetic.ConsultaProcTitle = consultaProcTitleTxtBx.Text;

            cosmetic.ConsultaSolicitudTitle = consultaSolicitudTitleTxtBx.Text;

            cosmetic.ConsultaSuperTitle = consultaSuperTitleTxtBx.Text;

            cosmetic.HistoryCompletadasTitle = historyCompletadasTitleTxtBx.Text;

            cosmetic.HistoryRecibidasTitle = historyRecibidasTitleTxtBx.Text;

            cosmetic.IndicadoresProductividadTitle = indicadoresProductividadTitleTxtBx.Text;

            cosmetic.ReportComparacionTitle = reportComparacionTitleTxtBx.Text;

            cosmetic.ReportProduccionTitle = reportProduccionTitleTxtBx.Text;

            cosmetic.SolicitudesStatusTitle = solicitudesStatusTitleTxtBx.Text;

            cosmetic.SolicitudTitle = solicitudTitleTxtBx.Text;

            #endregion

            try
            {
                Exception excep = CosmeticsRepo.Update(cosmetic);

                if (excep != null)
                {
                    throw new Exception(string.Format("No se pudo personalizar el sistema. Error Personalizar Sistema: {0}",
                            excep.Message.Replace("'", string.Empty)));
                }

                if (logoFileUp.HasFile)
                    logoFileUp.SaveAs(Server.MapPath(cosmetic.LogoPath));

                Session["Cosmetic"] = cosmetic;

                Response.Redirect("~/Pages/Dashboard/Configs/CustomizeAllPages.aspx", true);
            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomizeSystemAlert",
                        string.Format("alert('{0}');", exp.Message.Replace("\r\n", " ")), true);
            }
        }

        private void SetPageColors(Cosmetic cosmetic)
        {
            #region Set lbl colors

            consultaLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            dashboardLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            fondoLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            linkLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            logoLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            miscLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            reportLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            textoLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            tituloLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            titleColorLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            versionesLbl.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            labelForeColorBtn.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            linkForeColorBtn.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            saveBtn.ForeColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            labelForeColorBtn.BackColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            linkForeColorBtn.BackColor = ColorTranslator.FromHtml(cosmetic.LabelForeColor);

            ScriptManager.RegisterStartupScript(this, GetType(), "InvokeChangeClinetSideColors",
                                    string.Format("ChangeClinetSideColors('{0}', '{1}');",
                                    cosmetic.LabelForeColor, cosmetic.TitleBackColor), true);

            #endregion

            #region Set title div's backcolor

            colorTitleDiv.Style.Add("background-color", cosmetic.TitleBackColor);

            logoTitleDiv.Style.Add("background-color", cosmetic.TitleBackColor);

            TituloTitleDiv.Style.Add("background-color", cosmetic.TitleBackColor);

            titleBackColorBtn.BackColor = ColorTranslator.FromHtml(cosmetic.TitleBackColor);

            #endregion
        }
    }
}