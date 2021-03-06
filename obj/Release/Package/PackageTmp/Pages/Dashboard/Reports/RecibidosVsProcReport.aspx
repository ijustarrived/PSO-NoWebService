﻿<%@ Page Language="C#" Title="Comparaci&oacute;n entre Solicitudes Recibidas y Procesadas" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="RecibidosVsProcReport.aspx.cs" Inherits="PSO.Pages.Dashboard.Reports.RecibidosVsProcReport" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.2/jquery-ui.js"></script>

    <link rel="stylesheet" href="https://code.jquery.com/ui/1.10.2/themes/smoothness/jquery-ui.css" />

    <div style="text-align: center; margin-top: 20px; margin-bottom: 20px">

        <asp:Label runat="server" Text="Fecha Tramitada" ForeColor="#79256E"></asp:Label>

    </div>

    <div style="text-align: center; margin-top: 20px">

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

    </div>

    <div style="margin-top: 20px; text-align: center; margin-bottom: 20px">

        <asp:Button Style="padding: 10px 15px;" ID="searchBtn" runat="server" OnClick="searchBtn_Click" Text="Buscar" />

    </div>

    <div style="text-align: center; margin-bottom: 20px" runat="server" id="chartDiv">

        <asp:Chart ID="Chart1" runat="server" Width="1000">

            <Series>
                <asp:Series Name="testSeries">

                    <Points>

                        <asp:DataPoint Color="0, 192, 0" Font="Microsoft Sans Serif, 15pt" LabelForeColor="MediumOrchid" AxisLabel="Recibidos" />

                        <asp:DataPoint Color="#888800" Font="Microsoft Sans Serif, 15pt" LabelForeColor="MediumOrchid"
                            AxisLabel="Procesados" />

                    </Points>

                </asp:Series>
            </Series>

            <ChartAreas>
                <asp:ChartArea Name="testArea">
                    <AxisY>

                        <MajorGrid LineDashStyle = "NotSet" />

                        <LabelStyle Enabled ="false" />

                    </AxisY>

                    <AxisX>

                        <MajorGrid Enabled="false" />

                        <LabelStyle Font="Microsoft Sans Serif, 30pt" />

                    </AxisX>
                </asp:ChartArea>
            </ChartAreas>

        </asp:Chart>

        <div style="background-color: #616161; padding-left: 7%; margin-top: 40px">

            <asp:Label ForeColor="#E5E5E5" Font-Size="X-Large" Font-Bold="true" runat="server">Listado de Solicitudes</asp:Label>

        </div>

    </div>    

    <asp:GridView runat="server" AutoGenerateColumns="false" AllowPaging="True" ForeColor="#79256E" BackColor="#F3F0F7"
        CellPadding="2" GridLines="None" CssClass="table" ShowHeaderWhenEmpty="true" ID="recievedGV"
        Style="margin-top: 40px; width: 95%; margin-left: auto; margin-right: auto"
        DataSourceID="solicitudesSQLDS" OnRowDataBound ="recievedGV_RowDataBound">

        <AlternatingRowStyle BackColor="#E5E5E5" />

        <EmptyDataTemplate>

            <div style="text-align: center">

                <asp:Label ForeColor="#79256E" runat="server">No hay data disponible</asp:Label>

            </div>

        </EmptyDataTemplate>

        <Columns>

            <asp:BoundField DataField="NumeroSolicitud" HeaderText="N&uacute;mero Solicitud">
                <HeaderStyle Font-Size="13pt" />
                <ItemStyle Font-Size="12pt" />
            </asp:BoundField>

            <asp:BoundField DataField="FechaTramitada" HeaderText="Fecha Recibido">
                <HeaderStyle Font-Size="13pt" />
                <ItemStyle Font-Size="12pt" />
            </asp:BoundField>

              <asp:BoundField DataField="FechaTrabajado" HeaderText="Fecha Completado">
                <HeaderStyle Font-Size="13pt" />
                <ItemStyle Font-Size="12pt"/>
            </asp:BoundField>

             <asp:BoundField DataField="Duration" HeaderText="Dias Transcuridos">
                <HeaderStyle Font-Size="13pt" />
                <ItemStyle Font-Size="12pt" />
            </asp:BoundField>

            <asp:BoundField DataField="CoordinadorID" HeaderText="Coordinador">
                <HeaderStyle Font-Size="13pt" />
                <ItemStyle Font-Size="12pt" />
            </asp:BoundField>

            <asp:BoundField DataField="ProcesadorID" HeaderText="Procesador">
                <HeaderStyle Font-Size="13pt" />
                <ItemStyle Font-Size="12pt" />
            </asp:BoundField>

        </Columns>

        <FooterStyle BackColor="#616161" />

        <HeaderStyle BackColor="#616161" ForeColor="#E5E5E5" Font-Bold="True" />

        <PagerStyle BackColor="#616161" Font-Size="Large" ForeColor="#E5E5E5" Font-Bold="true" />

        <SelectedRowStyle BackColor="#79256E" ForeColor="#F3F0F7" />

    </asp:GridView>

    <asp:SqlDataSource runat ="server" ID ="solicitudesSQLDS" SelectCommand ="SELECT CoordinadorID, ProcesadorID, NumeroSolicitud, FechaTramitada,
         FechaTrabajado, DATEDIFF(day, FechaTramitada, FechaTrabajado) AS Duration FROM Solicitudes @WHERE" 
        ConnectionString ="<%$ ConnectionStrings:local %>" OnSelecting ="solicitudesSQLDS_Selecting"></asp:SqlDataSource>

    <%--<table class="table" runat ="server" id ="gvTbl" visible ="false">

        <tr>

            <td style ="border-top-width:0px;">

                <div>

                    <div style="text-align: center">

                        <asp:Label ForeColor="#79256E" runat="server" Text="Solicitudes Recibidas"></asp:Label>

                    </div>

                    <hr style="margin-left: 30%; margin-right: 30%; margin-bottom: 20px; border-color: #616161" />

                    

                </div>

            </td>

            <td style ="border-top-width:0px;">

                <div>

                    <div style="text-align: center">

                        <asp:Label ForeColor="#79256E" runat="server" Text="Solicitudes Procesadas"></asp:Label>

                    </div>

                    <hr style="margin-left: 30%; margin-right: 30%; margin-bottom: 20px; border-color: #616161" />

                    <asp:GridView runat="server" AutoGenerateColumns="false" AllowPaging="True" ForeColor="#79256E" BackColor="#F3F0F7"
                        CellPadding="2" GridLines="None" CssClass="table" ShowHeaderWhenEmpty="true" ID="procGV"
                        Style="margin-top: 40px; width: 95%; margin-left: auto; margin-right: auto">

                        <AlternatingRowStyle BackColor="#E5E5E5" />

                        <EmptyDataTemplate>

                            <div style="text-align: center">

                                <asp:Label ForeColor="#79256E" runat="server">No hay data disponible</asp:Label>

                            </div>

                        </EmptyDataTemplate>

                        <Columns>

                            <asp:BoundField DataField="NumeroSolicitud" HeaderText="N&uacute;mero Solicitud">
                                <HeaderStyle Font-Size="13pt" HorizontalAlign ="Center"/>
                                <ItemStyle Font-Size="12pt" HorizontalAlign ="Center" />
                            </asp:BoundField>

                        </Columns>

                        <FooterStyle BackColor="#616161" />

                        <HeaderStyle BackColor="#616161" ForeColor="#E5E5E5" Font-Bold="True" />

                        <PagerStyle BackColor="#616161" Font-Size="Large" ForeColor="#E5E5E5" Font-Bold="true" />

                        <SelectedRowStyle BackColor="#79256E" ForeColor="#F3F0F7" />

                        <RowStyle HorizontalAlign ="Center" />

                    </asp:GridView>

                </div>

            </td>

        </tr>

    </table>--%>

    <div style="text-align: center; margin-bottom: 40px; margin-top: 70px; padding-bottom: 100px">

        <input type="button" onclick="javascript: window.print();"
            style="border-style: solid; border-color: #616161; border-width: 3px; padding: 10px 15px;"
            value="Imprimir" />

    </div>

</asp:Content>

