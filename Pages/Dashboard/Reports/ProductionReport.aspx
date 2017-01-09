<%@ Page Language="C#" Title ="Resultados de Producción por Rol" AutoEventWireup="true" 
    CodeBehind="ProductionReport.aspx.cs" Inherits="PSO.Pages.Dashboard.Reports.ProductionReport" MasterPageFile="~/Site.Master" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
     Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.2/jquery-ui.js"></script>

    <link rel="stylesheet" href="https://code.jquery.com/ui/1.10.2/themes/smoothness/jquery-ui.css" />

    <div style="text-align: center; margin-top: 20px; margin-bottom: 20px">

        <asp:Label runat="server" Text="Periodo de Fechas" ForeColor="#79256E"></asp:Label>

    </div>

    <div style ="text-align:center; margin-top:20px; margin-bottom:20px">

        <script>

            $(
                function () {
                    $("#<% = desdeTxtBx.ClientID%>").datepicker();
                                }
                    );

        </script>

        <asp:TextBox runat="server" Style="width: initial; margin-right: 20px;" ID="desdeTxtBx" placeholder="Desde"></asp:TextBox>

        <script>

            $(
                function () {
                    $("#<% = hastaTxtBx.ClientID%>").datepicker();
                                }
                    );

        </script>

          <asp:TextBox runat="server" Style="width: initial" ID="hastaTxtBx" placeholder="Hasta"></asp:TextBox>

        <asp:DropDownList runat ="server" ID ="rolDDL" AutoPostBack ="true" OnSelectedIndexChanged ="rolDDL_SelectedIndexChanged">

            <asp:ListItem>Coordinadores</asp:ListItem>

            <asp:ListItem>Procesadores</asp:ListItem>

        </asp:DropDownList>

    </div>

    <div style="margin-top: 20px; text-align: center; margin-bottom: 20px">

        <asp:Button Style="padding: 10px 15px;" ID="searchBtn" runat="server" OnClick ="searchBtn_Click" Text="Buscar" />

    </div>

    <div style="text-align: center; overflow:auto">

        <asp:Chart ID="productionChrt" runat="server">

            <Series>

                <asp:Series Name="Employees">

                    <Points></Points>

                </asp:Series>

            </Series>

            <ChartAreas>

                <asp:ChartArea Name="testArea">

                    <AxisY>

                        <MajorGrid LineDashStyle ="NotSet" />

                        <LabelStyle Enabled ="false" />

                    </AxisY>

                    <AxisX>

                        

                        <MajorGrid Enabled="false" />

                        <LabelStyle Font="Microsoft Sans Serif, 30pt" />

                    </AxisX>

                </asp:ChartArea>

            </ChartAreas>

        </asp:Chart>

    </div>

    <hr style=" width:30%; margin-bottom: 20px; border-color:#616161" />

    <div style="text-align: center; margin-bottom: 20px; margin-top:20px">

        <asp:Label ForeColor="#79256E" runat="server" ID="totalAvisosLbl"></asp:Label>

    </div>

    <div style="text-align: center; margin-bottom: 40px; margin-top: 70px; padding-bottom: 100px">

        <input type="button" onclick="javascript: window.print();"
            style="border-style: solid; border-color: #616161; border-width: 3px; padding: 10px 15px;"
            value="Imprimir" />

    </div>

    </asp:Content>
