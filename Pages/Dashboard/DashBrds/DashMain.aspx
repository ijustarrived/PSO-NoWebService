<%@ Page Language="C#" Title ="Dashboard" MasterPageFile="~/Site.Master" AutoEventWireup="true"
     CodeBehind="DashMain.aspx.cs" Inherits="PSO.Pages.Dashboard.DashBrds.DashMain" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script>

        function ChangeClinetSideColors(lblColor, titleColor)
        {
            $('#printBtn').css({ 'color': lblColor });
        }

    </script>

    <script>

        var countDown = 1020; /*equals 17 minutes in seconds not exact 15 cause it might die before if it was exact due to threa priorities.
        It's converted to seconds cause the interval loop every second not every millisecond*/

        var timer = setInterval("KeepAlive()", 1000);

        function KeepAlive()
        {
            var aliveHF = document.getElementById("<%= aliveHF.ClientID%>");

            //The purpose of KeepAlive is to run a single line of programmming on the server 
            //just so it can keep the session alive.
                aliveHF.value = PageMethods.KeepAlive();

            //When the countdown hits 0 it'll do a fake timeout
                if (countDown === 0)
                {
                    clearInterval(timer);

                    var lockedId = <%= Session["lockedId"] %>;

                    if(lockedId != 0)
                        PageMethods.ReleaseAllLkdSolicitudes(lockedId);

                    window.location.href = '../../Login.aspx';
                }

                else
                    countDown = countDown - 1;
            }

    </script>

    <asp:HiddenField ID ="aliveHF" runat ="server" />

    <div style="text-align: center; margin-top:20px; overflow:auto">

        <%-- Historial recibidas --%>
        <div style="display: inline-block; margin-right:3%; border-style: solid;
                    border-color: #616161; border-width: 1px; padding-left: 5px;
                    padding-right: 5px; padding-bottom: 15px; border-radius: 5px; margin-bottom:10px">

            <div runat="server" id="historialRecibidasTitleDiv" style="background-color: #616161; text-align: center; margin-top: 20px">

                <asp:Label ID="historialRecibidasTitleLbl" ForeColor="#E5E5E5" Font-Size="X-Large" Font-Bold="true" runat="server">
            
            Historial de Solicitudes Recibidas

                </asp:Label>

            </div>

            <div style="text-align: center; margin-top: 20px; overflow: auto">

                <asp:Chart ID="historialRecibidasChrt" runat="server" Height="200" Width="500">

                    <Series>
                        <asp:Series ChartType="Line" Name="Año Corriente" YValueType ="Int32" MarkerStyle ="Circle">

                            <Points>

                                <asp:DataPoint Color="Aqua" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                                    AxisLabel="Ene" />

                                <asp:DataPoint Color="Blue" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                                    AxisLabel="Feb" />

                                <asp:DataPoint Color="MediumAquamarine" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                                    AxisLabel="Mar" />

                                <asp:DataPoint Color="BlueViolet" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                                    AxisLabel="Abr" />

                                <asp:DataPoint Color="Brown" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                                    AxisLabel="May" />

                                <asp:DataPoint Color="BurlyWood" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                                    AxisLabel="Jun" />

                                <asp:DataPoint Color="Orange" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                                    AxisLabel="Jul" />

                                <asp:DataPoint Color="LightBlue" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                                    AxisLabel="Ago" />

                                <asp:DataPoint Color="RosyBrown" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                                    AxisLabel="Sep" />

                                <asp:DataPoint Color="SandyBrown" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                                    AxisLabel="Oct" />

                                <asp:DataPoint Color="Yellow" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                                    AxisLabel="Nov" />

                                <asp:DataPoint Color="YellowGreen" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                                    AxisLabel="Dic" />

                            </Points>

                        </asp:Series>
                    </Series>

                    <ChartAreas>
                        <asp:ChartArea Name="testArea">
                            <AxisY>
                                <MajorGrid LineDashStyle="NotSet" />

                                <LabelStyle Format ="0" />
                            </AxisY>

                            <AxisX>

                                <MajorGrid Enabled="false" />

                                <LabelStyle Font="Microsoft Sans Serif, 30pt" Interval="1" />

                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>

                </asp:Chart>

            </div>

        </div>

        <%-- Historial completadas --%>

        <div style="display: inline-block; margin-right:3%; border-style: solid;
                    border-color: #616161; border-width: 1px; padding-left: 5px;
                    padding-right: 5px; padding-bottom: 15px; border-radius: 5px; margin-bottom:10px">

            <div runat="server" id="historialCompletadasTitleDiv"
                style="background-color: #616161; text-align: center; margin-top: 20px">

                <asp:Label ID="historialCompletadasTitleLbl" ForeColor="#E5E5E5" Font-Size="X-Large" Font-Bold="true" runat="server">
            
            Historial de Solicitudes Completadas

                </asp:Label>

            </div>

            <div style="text-align: center; margin-top: 20px; overflow: auto">

                <asp:Chart ID="historialCompletadasChrt" runat="server" Height="200" Width="500">

                    <Series>
                        <asp:Series ChartType="Line" Name="Año Corriente" YValueType ="Int32" MarkerStyle ="Circle">

                            <Points>

                                <asp:DataPoint Color="Aqua" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                                    AxisLabel="Ene" />

                                <asp:DataPoint Color="Blue" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                                    AxisLabel="Feb" />

                                <asp:DataPoint Color="MediumAquamarine" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                                    AxisLabel="Mar" />

                                <asp:DataPoint Color="BlueViolet" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                                    AxisLabel="Abr" />

                                <asp:DataPoint Color="Brown" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                                    AxisLabel="May" />

                                <asp:DataPoint Color="BurlyWood" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                                    AxisLabel="Jun" />

                                <asp:DataPoint Color="Orange" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                                    AxisLabel="Jul" />

                                <asp:DataPoint Color="LightBlue" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                                    AxisLabel="Ago" />

                                <asp:DataPoint Color="RosyBrown" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                                    AxisLabel="Sep" />

                                <asp:DataPoint Color="SandyBrown" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                                    AxisLabel="Oct" />

                                <asp:DataPoint Color="Yellow" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                                    AxisLabel="Nov" />

                                <asp:DataPoint Color="YellowGreen" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                                    AxisLabel="Dic" />

                            </Points>

                        </asp:Series>
                    </Series>

                    <ChartAreas>
                        <asp:ChartArea Name="testArea">
                            <AxisY>
                                <MajorGrid LineDashStyle="NotSet" />

                                <LabelStyle />

                            </AxisY>

                            <AxisX>

                                <MajorGrid Enabled="false" />

                                <LabelStyle Font="Microsoft Sans Serif, 30pt" Interval="1" />

                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>

                </asp:Chart>

            </div>

        </div>

        <%-- Solicitudes status --%>

        <div style="display: inline-block; margin-right:3%; border-style: solid;
                    border-color: #616161; border-width: 1px; padding-left: 5px;
                    padding-right: 5px; padding-bottom: 15px; border-radius: 5px; margin-bottom:10px">

            <div runat="server" id="solicitudesStatusTitleDiv"
                style="background-color: #616161; text-align: center; margin-top: 20px">

                <asp:Label ID="SolicitudStatusTitleLbl" ForeColor="#E5E5E5" Font-Size="X-Large" Font-Bold="true" runat="server">
            
            Solicitudes Recibidas por Status

                </asp:Label>

            </div>

            <div style="text-align: center; margin-bottom: 20px; margin-top: 20px">

                <asp:Label ForeColor="#79256E" runat="server" ID="totalAvisosLbl"></asp:Label>

            </div>

            <%--<hr style="width: 30%; margin-bottom: 20px; border-color: #616161" />--%>

            <div style="text-align: center; overflow: auto">

                <asp:Chart ID="Chart1" runat="server" Width="500" Height="200px">

                    <Legends>

                        <asp:Legend Alignment="Center" Docking="Right"
                            IsTextAutoFit="false" Name="LgenedChrt">
                        </asp:Legend>

                    </Legends>

                    <Series>

                        <asp:Series ChartType="Pie" Name="testSeries"
                             CustomProperties="PieLineColor=Black, PieLabelStyle=Outside">

                            <Points>

                                <asp:DataPoint Color="Aqua" Font="Microsoft Sans Serif, 10pt" LabelForeColor ="Aqua"
                                    LegendText="Pendientes de Revisarse por un Coordinador" />

                                <asp:DataPoint Color="#0000a6" Font="Microsoft Sans Serif, 10pt" LabelForeColor="#0000a6"
                                    LegendText="Pendientes por Documentos Incompletos" />

                                <asp:DataPoint Color="#d5d500" Font="Microsoft Sans Serif, 10pt" LabelForeColor="#d5d500"
                                    LegendText="Pendientes de Asignarse a un Procesador" />

                                <asp:DataPoint Color="Teal" Font="Microsoft Sans Serif, 10pt" LabelForeColor ="Teal"
                                    LegendText="Pendientes de Trabajarse por un Procesador" />

                                <asp:DataPoint Color="0, 192, 0" Font="Microsoft Sans Serif, 10pt" LabelForeColor="0, 192, 0"
                                    LegendText="Aprobadas" />

                                <asp:DataPoint Color="192, 0, 0" Font="Microsoft Sans Serif, 10pt" LabelForeColor="192, 0, 0"
                                    LegendText="Denegadas" />

                                <asp:DataPoint Color="#616161" Font="Microsoft Sans Serif, 10pt" LabelForeColor="#616161"
                                    LegendText="Inactivas" />

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

        </div>

        <%-- Rango de dias completadas --%>

        <div style="display: inline-block; margin-right:3%; border-style: solid;
                    border-color: #616161; border-width: 1px; padding-left: 5px;
                    padding-right: 5px; padding-bottom: 15px; border-radius: 5px; margin-bottom:10px">

            <div runat="server" id="randoDiasTitleDiv" style="background-color: #616161; text-align: center; margin-top: 20px">

                <asp:Label ID="rangoDiasTitleLbl" ForeColor="#E5E5E5" Font-Size="X-Large" Font-Bold="true" runat="server">

            Rango de Días de Solicitudes Completadas por Mes

                </asp:Label>

            </div>

            <div style="text-align: center; margin-top: 20px">

                <asp:DropDownList ID="monthDDL" AutoPostBack="true" runat="server">

                    <asp:ListItem>Enero</asp:ListItem>

                    <asp:ListItem>Febrero</asp:ListItem>

                    <asp:ListItem>Marzo</asp:ListItem>

                    <asp:ListItem>Abril</asp:ListItem>

                    <asp:ListItem>Mayo</asp:ListItem>

                    <asp:ListItem>Junio</asp:ListItem>

                    <asp:ListItem>Julio</asp:ListItem>

                    <asp:ListItem>Agosto</asp:ListItem>

                    <asp:ListItem>Septiembre</asp:ListItem>

                    <asp:ListItem>Octubre</asp:ListItem>

                    <asp:ListItem>Noviembre</asp:ListItem>

                    <asp:ListItem>Diciembre</asp:ListItem>

                </asp:DropDownList>

            </div>

            <div style="text-align: center; margin-top: 20px; overflow: auto">

                <asp:Chart ID="dateRangeChrt" runat="server" Height="200" Width="500">

                    <Series>
                        <asp:Series ChartType="Bar" Name="Año Corriente">

                            <Points>

                                <asp:DataPoint Color="BlueViolet" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                                    AxisLabel="9 Días - En Adelante" />

                                <asp:DataPoint Color="MediumAquamarine" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                                    AxisLabel="6 - 8 Días" />

                                <asp:DataPoint Color="Blue" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                                    AxisLabel="3 - 5 Días" />

                                <asp:DataPoint Color="Aqua" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                                    AxisLabel="1 - 2 Días " />

                            </Points>

                        </asp:Series>
                    </Series>

                    <ChartAreas>
                        <asp:ChartArea Name="testArea">
                            <AxisY>
                                <MajorGrid LineDashStyle="NotSet" />

                                <LabelStyle Enabled="false" />

                            </AxisY>

                            <AxisX>

                                <MajorGrid Enabled="false" />

                                <LabelStyle Font="Microsoft Sans Serif, 30pt" Interval="1" />

                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>

                </asp:Chart>

            </div>

        </div>

    </div>

    <div style="text-align: center; margin-bottom: 40px; margin-top:70px; padding-bottom: 100px">

        <input type="button" onclick="javascript:window.print();" 
            style="border-style: solid; border-color: #616161; border-width: 3px; padding: 10px 15px;"
            value="Imprimir" id ="printBtn" />

    </div>

</asp:Content>
