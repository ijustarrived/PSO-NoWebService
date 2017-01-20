<%@ Page Language="C#" AutoEventWireup="true" Title="Solicitudes Recibidas por Status" MasterPageFile="~/Site.Master"
    CodeBehind="AvisosStatusReport.aspx.cs" Inherits="PSO.Pages.Dashboard.Reports.AvisosStatusReport" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div style="text-align: center; margin-bottom: 20px; margin-top:20px">

        <asp:Label ForeColor="#79256E" runat="server" ID="totalAvisosLbl"></asp:Label>

    </div>

    <hr style=" width:30%; margin-bottom: 20px; border-color:#616161" />

    <div style="text-align: center; overflow:auto">

        <asp:Chart ID="Chart1" runat="server" Width="1000">

            <Legends>

                <asp:Legend Alignment ="Center" Docking ="Bottom" IsTextAutoFit ="false" Name ="LgenedChrt"></asp:Legend>

            </Legends>

            <Series>
                <asp:Series ChartType ="Pie" Name="testSeries">

                    <Points>

                        <asp:DataPoint Color="#888800" Font="Microsoft Sans Serif, 15pt" LabelForeColor="White" 
                             LegendText ="Pendientes de Revisarse por un Coordinador"/>

                        <asp:DataPoint Color="#0000a6" Font="Microsoft Sans Serif, 15pt" LabelForeColor="White"
                              LegendText ="Pendientes por Documentos Incompletos" />

                        <asp:DataPoint Color="#d5d500" Font="Microsoft Sans Serif, 15pt" LabelForeColor="White"
                              LegendText ="Pendientes de Asignarse un Procesador"/>

                        <asp:DataPoint Color="#13dafd" Font="Microsoft Sans Serif, 15pt" LabelForeColor="White"
                             LegendText ="Pendientes de Trabajarse por un Procesador"/>

                        <asp:DataPoint Color="0, 192, 0" Font="Microsoft Sans Serif, 15pt" LabelForeColor="White"
                             LegendText ="Aprobadas"/>

                        <asp:DataPoint Color="192, 0, 0" Font="Microsoft Sans Serif, 15pt" LabelForeColor="White"
                             LegendText ="Denegadas" />

                        <asp:DataPoint Color="#616161" Font="Microsoft Sans Serif, 15pt" LabelForeColor="White"
                             LegendText ="Inactivas" />

                    </Points>

                </asp:Series>
            </Series>

            <ChartAreas>
                <asp:ChartArea Name="testArea">
                    <AxisY>
                        <MajorGrid LineDashStyle="Dash" />
                        <LabelStyle Interval="Auto" />
                    </AxisY>

                    <AxisX>

                        <MajorGrid Enabled="false" />

                        <LabelStyle Font="Microsoft Sans Serif, 30pt" />

                    </AxisX>
                </asp:ChartArea>
            </ChartAreas>

        </asp:Chart>

    </div>

    <div style="text-align: center; margin-bottom: 40px; margin-top: 70px; padding-bottom: 100px">

        <input type="button" onclick="javascript: window.print();"
            style="border-style: solid; border-color: #616161; border-width: 3px; padding: 10px 15px;"
            value="Imprimir" />

    </div>

</asp:Content>

