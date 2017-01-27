<%@ Page Language="C#" Title ="Dashboard" MasterPageFile="~/Site.Master" AutoEventWireup="true"
     CodeBehind="DashMain.aspx.cs" Inherits="PSO.Pages.Dashboard.DashBrds.DashMain" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div style="background-color: #616161; text-align:center; margin-top: 20px">

        <asp:Label ForeColor="#E5E5E5" Font-Size="X-Large" Font-Bold="true" runat="server">Historial de Solicitudes Recibidas</asp:Label>

    </div>

    <div style="text-align: center; margin-top:20px; overflow: auto">

        <asp:Chart ID="historialRecibidasChrt" runat="server" Width="1000">

            <Series>
                <asp:Series ChartType="Line" Name="Año Corriente">

                    <Points>

                        <asp:DataPoint Color ="Aqua"  Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black" 
                             AxisLabel="Ene" />

                        <asp:DataPoint Color="Blue" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black" 
                             AxisLabel="Feb" />

                        <asp:DataPoint Color ="MediumAquamarine" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                            AxisLabel="Mar" />

                        <asp:DataPoint Color ="BlueViolet" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                            AxisLabel="Abr" />

                        <asp:DataPoint Color ="Brown" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                            AxisLabel="May" />

                        <asp:DataPoint Color ="BurlyWood" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                            AxisLabel="Jun" />

                        <asp:DataPoint Color ="Orange" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                            AxisLabel="Jul" />

                        <asp:DataPoint Color ="LightBlue" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                            AxisLabel="Ago" />

                        <asp:DataPoint Color ="RosyBrown" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                            AxisLabel="Sep" />

                        <asp:DataPoint Color ="SandyBrown" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                            AxisLabel="Oct" />

                        <asp:DataPoint Color="Yellow" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                            AxisLabel="Nov" />

                        <asp:DataPoint Color ="YellowGreen" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                            AxisLabel="Dic" />

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

                        <LabelStyle Font="Microsoft Sans Serif, 30pt" Interval ="1" />

                    </AxisX>
                </asp:ChartArea>
            </ChartAreas>

        </asp:Chart>

    </div>

    <div style="background-color: #616161; text-align:center; margin-top: 20px">

        <asp:Label ForeColor="#E5E5E5" Font-Size="X-Large" Font-Bold="true" runat="server">Historial de Solicitudes Completadas</asp:Label>

    </div>

    <div style="text-align: center; margin-top:20px; overflow: auto">

        <asp:Chart ID="historialCompletadasChrt" runat="server" Width="1000">

            <Series>
                <asp:Series ChartType="Line" Name="Año Corriente">

                    <Points>

                        <asp:DataPoint Color ="Aqua"  Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black" 
                             AxisLabel="Ene" />

                        <asp:DataPoint Color="Blue" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black" 
                             AxisLabel="Feb" />

                        <asp:DataPoint Color ="MediumAquamarine" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                            AxisLabel="Mar" />

                        <asp:DataPoint Color ="BlueViolet" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                            AxisLabel="Abr" />

                        <asp:DataPoint Color ="Brown" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                            AxisLabel="May" />

                        <asp:DataPoint Color ="BurlyWood" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                            AxisLabel="Jun" />

                        <asp:DataPoint Color ="Orange" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                            AxisLabel="Jul" />

                        <asp:DataPoint Color ="LightBlue" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                            AxisLabel="Ago" />

                        <asp:DataPoint Color ="RosyBrown" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                            AxisLabel="Sep" />

                        <asp:DataPoint Color ="SandyBrown" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                            AxisLabel="Oct" />

                        <asp:DataPoint Color="Yellow" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                            AxisLabel="Nov" />

                        <asp:DataPoint Color ="YellowGreen" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                            AxisLabel="Dic" />

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

                        <LabelStyle Font="Microsoft Sans Serif, 30pt" Interval ="1" />

                    </AxisX>
                </asp:ChartArea>
            </ChartAreas>

        </asp:Chart>

    </div>

    <div style="background-color: #616161; text-align:center; margin-top: 20px">

        <asp:Label ForeColor="#E5E5E5" Font-Size="X-Large" Font-Bold="true" runat="server">Solicitudes Recibidas por Status</asp:Label>

    </div>

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

    <div style="background-color: #616161; text-align:center; margin-top: 20px">

        <asp:Label ForeColor="#E5E5E5" Font-Size="X-Large" Font-Bold="true" runat="server">Indicadores de Productividad en Proceso
             de Manejo de Solicitudes</asp:Label>

    </div>

    <div style=" margin-top:20px; overflow:auto">

        <table class ="table" style ="margin-left:auto; margin-right:auto; width:95%">

            <%-- Header --%>
            <tr>

                <th scope ="col" style ="color:#E5E5E5 ;background-color:#616161; ">
                    </th>

                <th scope ="col" style ="color:#E5E5E5 ;background-color:#616161; ">

                    <asp:Label Font-Size ="13" runat ="server">Meta</asp:Label>

                </th>

                <th scope ="col" style ="color:#E5E5E5 ;background-color:#616161;">

                    <asp:Label Font-Size ="13" runat ="server">Ene</asp:Label>

                </th>

                <th scope ="col" style ="color:#E5E5E5 ;background-color:#616161">

                    <asp:Label Font-Size ="13" runat ="server">Feb</asp:Label>

                </th>

                <th scope ="col" style ="color:#E5E5E5 ;background-color:#616161;">

                    <asp:Label Font-Size ="13" runat ="server">Mar</asp:Label>

                </th>

                <th scope ="col" style ="color:#E5E5E5 ;background-color:#616161 ">

                    <asp:Label Font-Size ="13" runat ="server">Abr</asp:Label>

                </th>

                <th scope ="col" style ="color:#E5E5E5 ;background-color:#616161;">

                    <asp:Label Font-Size ="13" runat ="server">May</asp:Label>

                </th>

                <th scope ="col" style ="color:#E5E5E5 ;background-color:#616161;">

                    <asp:Label Font-Size ="13" runat ="server">Jun</asp:Label>

                </th>

                <th scope ="col" style ="color:#E5E5E5 ;background-color:#616161;">

                    <asp:Label Font-Size ="13" runat ="server">Jul</asp:Label>

                </th>

                <th scope ="col" style ="color:#E5E5E5 ;background-color:#616161;">

                    <asp:Label Font-Size ="13" runat ="server">Ago</asp:Label>

                </th>

                <th scope ="col" style ="color:#E5E5E5 ;background-color:#616161 ">

                    <asp:Label Font-Size ="13" runat ="server">Sep</asp:Label>

                </th>

                <th scope ="col" style ="color:#E5E5E5 ;background-color:#616161 ">

                    <asp:Label Font-Size ="13" runat ="server">Oct</asp:Label>

                </th>

                <th scope ="col" style ="color:#E5E5E5 ;background-color:#616161 ">

                    <asp:Label Font-Size ="13" runat ="server">Nov</asp:Label>

                </th>

                <th scope ="col" style ="color:#E5E5E5 ;background-color:#616161 ">

                    <asp:Label Font-Size ="13" runat ="server">Dic</asp:Label>

                </th>

            </tr>

            <%-- Coor --%>
            <tr>

                <td style ="color:#79256E ;background-color:#F3F0F7; ">

                    <asp:Label Font-Size ="13" runat ="server">Procesos Coordinador</asp:Label>

                    </td>

                <td style ="color:#79256E ;background-color:#F3F0F7; ">

                    <asp:Label Font-Size ="13" runat ="server">1 Día</asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#F3F0F7; ">

                    <asp:Label ID ="eneCoorLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#F3F0F7; ">

                    <asp:Label ID ="febCoorLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#F3F0F7; ">

                    <asp:Label ID ="marCoorLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#F3F0F7; ">

                    <asp:Label ID ="abrCoorLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#F3F0F7; ">

                    <asp:Label ID ="mayCoorLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#F3F0F7; ">

                    <asp:Label ID ="junCoorLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#F3F0F7; ">

                    <asp:Label ID ="julCoorLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#F3F0F7; ">

                    <asp:Label ID ="agoCoorLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#F3F0F7; ">

                    <asp:Label ID ="sepCoorLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#F3F0F7; ">

                    <asp:Label ID ="octCoorLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#F3F0F7; ">

                    <asp:Label ID ="novCoorLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#F3F0F7; ">

                    <asp:Label ID ="dicCoorLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

            </tr>

            <%-- Super --%>
            <tr>

                <td style ="color:#79256E ;background-color:#E5E5E5; ">

                    <asp:Label Font-Size ="13" runat ="server">Procesos Supervisor</asp:Label>

                    </td>

                <td style ="color:#79256E ;background-color:#E5E5E5; ">

                    <asp:Label Font-Size ="13" runat ="server">3 Días</asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#E5E5E5; ">

                    <asp:Label ID ="eneSupLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#E5E5E5; ">

                    <asp:Label ID ="febSupLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#E5E5E5; ">

                    <asp:Label ID ="marSupLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#E5E5E5; ">

                    <asp:Label ID ="abrSupLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#E5E5E5; ">

                    <asp:Label ID ="maySupLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#E5E5E5; ">

                    <asp:Label ID ="junSupLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#E5E5E5; ">

                    <asp:Label ID ="julSupLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#E5E5E5; ">

                    <asp:Label ID ="agoSupLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#E5E5E5; ">

                    <asp:Label ID ="sepSupLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#E5E5E5; ">

                    <asp:Label ID ="octSupLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#E5E5E5; ">

                    <asp:Label ID ="novSupLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#E5E5E5; ">

                    <asp:Label ID ="dicSupLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

            </tr>

            <%-- Proc --%>
             <tr>

                <td style ="color:#79256E ;background-color:#F3F0F7; ">

                    <asp:Label Font-Size ="13" runat ="server">Procesos Procesador</asp:Label>

                    </td>

                <td style ="color:#79256E ;background-color:#F3F0F7; ">

                    <asp:Label Font-Size ="13" runat ="server">3 Días</asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#F3F0F7; ">

                    <asp:Label ID ="eneProcLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#F3F0F7; ">

                    <asp:Label ID ="febProcLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#F3F0F7; ">

                    <asp:Label ID ="marProcLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#F3F0F7; ">

                    <asp:Label ID ="abrProcLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#F3F0F7; ">

                    <asp:Label ID ="mayProcLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#F3F0F7; ">

                    <asp:Label ID ="junProcLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#F3F0F7; ">

                    <asp:Label ID ="julProcLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#F3F0F7; ">

                    <asp:Label ID ="agoProcLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#F3F0F7; ">

                    <asp:Label ID ="sepProcLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#F3F0F7; ">

                    <asp:Label ID ="octProcLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#F3F0F7; ">

                    <asp:Label ID ="novProcLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

                <td style ="color:#79256E ;background-color:#F3F0F7; ">

                    <asp:Label ID ="dicProcLbl" Font-Size ="13" runat ="server"></asp:Label>

                </td>

            </tr>

        </table>

    </div>

    <div style="background-color: #616161; text-align:center; margin-top: 20px">

        <asp:Label ForeColor="#E5E5E5" Font-Size="X-Large" Font-Bold="true" runat="server">

            Rango de Días de Solicitudes Completadas por Mes

        </asp:Label>

    </div>

    <div style ="text-align:center; margin-top:20px">

        <asp:DropDownList ID ="monthDDL" AutoPostBack ="true" runat ="server">

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

    <div style="text-align: center; margin-top:20px; overflow: auto">

        <asp:Chart ID="dateRangeChrt" runat="server" Width="1000">

            <Series>
                <asp:Series ChartType ="Bar" Name="Año Corriente">

                    <Points>
                        
                        <asp:DataPoint Color ="BlueViolet" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                            AxisLabel="9 - En Adelante" />

                         <asp:DataPoint Color ="MediumAquamarine" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                            AxisLabel="6 - 8" />

                        <asp:DataPoint Color="Blue" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black" 
                             AxisLabel="3 - 5" />

                        <asp:DataPoint Color ="Aqua"  Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black" 
                             AxisLabel="1 - 2" /> 

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

                        <LabelStyle Font="Microsoft Sans Serif, 30pt" Interval ="1" />

                    </AxisX>
                </asp:ChartArea>
            </ChartAreas>

        </asp:Chart>

    </div>

    <div style="text-align: center; margin-bottom: 40px; margin-top:70px; padding-bottom: 100px">

        <input type="button" onclick="javascript:window.print();" 
            style="border-style: solid; border-color: #616161; border-width: 3px; padding: 10px 15px;"
            value="Imprimir"  />

    </div>

</asp:Content>
