<%@ Page Language="C#" Title ="Personalizar Sistema" AutoEventWireup="true"
     MasterPageFile ="~/Site.Master"  CodeBehind="CustomizeAllPages.aspx.cs" Inherits="PSO.Pages.Dashboard.Configs.CustomizeAllPages" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <script>

        function ChangeClinetSideColors(lblColor, titleColor)
        {
            

            $('#printBtn').css({ 'color': lblColor });
        }

    </script>

    <div style=" margin-top: 20px; margin-bottom: 20px; text-align:center">

        <div>

            <asp:Label runat="server" ID ="versionesLbl" Text="Versiones" ForeColor="#79256E"></asp:Label>

        </div>

        <div>

            <asp:DropDownList ID="colorVersionsDDL" runat="server" AutoPostBack="true">

                <asp:ListItem>Original</asp:ListItem>

                <asp:ListItem>Rojo</asp:ListItem>

                <asp:ListItem>Verde</asp:ListItem>

                <asp:ListItem>Azul</asp:ListItem>

            </asp:DropDownList>

        </div>

    </div>

    <div style="background-color: #616161; text-align:center; margin-top: 20px" runat ="server" id ="logoTitleDiv">

        <asp:Label ForeColor="#E5E5E5" Font-Size="X-Large" Font-Bold="true" runat="server">Logo</asp:Label>

    </div>

    <%-- Logo --%>

    <div style="text-align: center; margin-bottom:20px; margin-top: 20px">

        <asp:Image runat="server" CssClass="img-responsive" ID ="logoImg" ImageUrl="~/PageImages/PSO_horizontal.png" />

    </div>

    <div style ="text-align:center">

            <div style ="padding-right:2%">

                <asp:Label Text="Logo del Sistema" ID ="logoLbl" runat="server" ForeColor="#79256E"></asp:Label>

            </div>

            <asp:FileUpload ID ="logoFileUp" runat ="server" style ="margin-left:auto; margin-right:auto" />

        </div> 
    
    <div style="background-color: #616161; text-align:center; margin-top: 20px" runat ="server" id ="colorTitleDiv">

        <asp:Label ForeColor="#E5E5E5" Font-Size="X-Large" Font-Bold="true" runat="server">Colores</asp:Label>

    </div>   

    <%-- Colors --%>

    <table class="table" style ="width: 90%; margin-left: auto; margin-right: auto;">

        <tr>
            <%-- Text --%>

            <td style ="border-top-width:0px; text-align:center">

                <div style="border-bottom-style: solid; padding-left: 7%; margin-top: 40px">

                    <asp:Label ForeColor="#79256E" ID ="textoLbl" Font-Size="X-Large" Font-Bold="true" 
                        runat="server">Texto</asp:Label>

                </div>

                <div>

                    <div style="margin-top: 20px;">

                        <asp:Label runat="server" ID ="tituloLbl" Text="Color de los Titulos de los Campos" ForeColor="#79256E"></asp:Label>

                        <asp:Button runat ="server" Enabled ="false" ID ="labelForeColorBtn"
                             BackColor ="#79256E"  Width ="50px" Height ="50px" />

                    </div>

                    <div style="margin-top: 20px;">

                        <asp:Label runat="server" ID ="linkLbl" Text="Color de los Enlaces" ForeColor="#79256E"></asp:Label>

                        <asp:Button runat ="server" Enabled ="false" ID ="linkForeColorBtn" 
                            BackColor ="#79256E" Width ="50px" Height ="50px" />

                    </div>            

                </div>

            </td>

            <%-- Background --%>

            <td style ="border-top-width:0px; text-align:center">

                <div style="border-bottom-style: solid; padding-left: 7%; margin-top: 40px">

                    <asp:Label ForeColor="#79256E" ID ="fondoLbl" Font-Size="X-Large" Font-Bold="true" 
                        runat="server">Fondo</asp:Label>

                </div>

                <div>

                    <div style="margin-top: 20px;">

                        <asp:Label runat="server" ID ="titleColorLbl" Text="Color de los Titulos de las Paginas" 
                            ForeColor="#79256E"></asp:Label>

                        <asp:Button runat ="server" ID ="titleBackColorBtn" Enabled ="false"
                             BackColor ="#616161" Width ="50px" Height ="50px" />

                    </div>

                </div>

            </td>

        </tr>

    </table>

    <%-- Titulos --%>

    <div style="background-color: #616161; text-align:center; margin-top: 20px" runat ="server" id ="TituloTitleDiv">

        <asp:Label ForeColor="#E5E5E5" Font-Size="X-Large" Font-Bold="true" runat="server">Titulos</asp:Label>

    </div>

    <table class="table" style ="width: 90%; margin-left: auto; margin-right: auto;">

        <tr>
            <%-- Consultas --%>

            <td style ="border-top-width:0px; text-align:center">

                <div style="border-bottom-style: solid; padding-left: 7%; margin-top: 40px">

                    <asp:Label ForeColor="#79256E" ID ="consultaLbl" Font-Size="X-Large" Font-Bold="true" 
                        runat="server">Consultas</asp:Label>

                </div>

                <div>

                    <div style="margin-top: 20px;">

                        <asp:TextBox ID ="consultaCoorTitleTxtBx" runat ="server" 
                            Text ="Solicitudes Pendientes de Revisarse por Coordinador"></asp:TextBox>

                    </div>

                    <div style="margin-top: 20px;">

                        <asp:TextBox ID ="consultaProcTitleTxtBx" runat ="server" 
                            Text ="Solicitudes Pendientes de Trabajarse por un Procesador"></asp:TextBox>

                    </div> 
                    
                     <div style="margin-top: 20px;">

                        <asp:TextBox ID ="consultaSolicitudTitleTxtBx" runat="server"
                             Text="Solicitudes &#010; por Nombre, Seguro Social o Status"></asp:TextBox>

                    </div>

                    <div style="margin-top: 20px;">

                        <asp:TextBox ID ="consultaSuperTitleTxtBx" runat="server"
                             Text="Solicitudes Pendientes de Asignarse a un Procesador"></asp:TextBox>

                    </div>            

                </div>

            </td>

            <%-- Reportes --%>

            <td style ="border-top-width:0px; text-align:center">

                <div style="border-bottom-style: solid; padding-left: 7%; margin-top: 40px">

                    <asp:Label ForeColor="#79256E" ID ="reportLbl" Font-Size="X-Large" Font-Bold="true" 
                        runat="server">Reportes</asp:Label>

                </div>

                <div>

                    <div style="margin-top: 20px;">

                        <asp:TextBox ID ="reportComparacionTitleTxtBx" runat="server"

                             Text="Comparación entre Solicitudes Recibidas y Procesadas"></asp:TextBox>
                    </div>

                    <div style="margin-top: 20px;">

                        <asp:TextBox ID ="indicadoresProductividadTitleTxtBx" runat="server"
                             Text="Indicadores de Productividad en Proceso de Manejo de Solicitudes"></asp:TextBox>

                    </div>

                    <div style="margin-top: 20px;">

                        <asp:TextBox ID ="reportProduccionTitleTxtBx" runat="server"
                             Text="Resultados de Producción por Rol"></asp:TextBox>

                    </div>

                </div>

            </td>

        </tr>

        <tr>
            <%-- Dashboard --%>

            <td style="border-top-width: 0px; text-align: center">

                <div style="border-bottom-style: solid; padding-left: 7%; margin-top: 40px">

                    <asp:Label ForeColor="#79256E" ID ="dashboardLbl" Font-Size="X-Large" Font-Bold="true"
                         runat="server">Dashboard</asp:Label>

                </div>

                <div>

                    <div style="margin-top: 20px;">

                        <asp:TextBox ID ="historyRecibidasTitleTxtBx" runat="server" 
                            Text="Historial de Solicitudes Recibidas"></asp:TextBox>

                    </div>

                    <div style="margin-top: 20px;">

                        <asp:TextBox ID ="historyCompletadasTitleTxtBx" runat="server"
                             Text="Historial de Solicitudes Completadas"></asp:TextBox>

                    </div>

                    <div style="margin-top: 20px;">

                        <asp:TextBox ID ="solicitudesStatusTitleTxtBx" runat="server"
                             Text="Solicitudes Recibidas por Status"></asp:TextBox>

                    </div>

                    <%--<div style="margin-top: 20px;">

                        <asp:TextBox ID ="indicadoresProductividadTitleTxtBx" runat="server"
                             Text="Indicadores de Productividad en Proceso de Manejo de Solicitudes"></asp:TextBox>

                    </div>--%>

                    <div style="margin-top: 20px;">

                        <asp:TextBox ID ="completadasMesDiasTitleTxtBx" runat="server"
                             Text="Rango de Días de Solicitudes Completadas por Mes"></asp:TextBox>

                    </div>

                </div>

            </td>

            <%-- Misc --%>

            <td style="border-top-width: 0px; text-align: center">

                <div style="border-bottom-style: solid; padding-left: 7%; margin-top: 40px">

                    <asp:Label ForeColor="#79256E" ID ="miscLbl" Font-Size="X-Large" Font-Bold="true" 
                        runat="server">Miscel&aacute;neo</asp:Label>

                </div>

                <div>

                    <div style="margin-top: 20px;">

                        <asp:TextBox ID ="solicitudTitleTxtBx" runat="server" Text="Solicitud de Servicio"></asp:TextBox>

                    </div>

                </div>

            </td>

        </tr>

    </table>

    <div style="text-align: center; margin-bottom: 40px; margin-top:70px; padding-bottom: 100px">

        <asp:Button ID ="saveBtn" runat ="server" OnClick ="saveBtn_Click" Text ="Guardar" 
            style ="padding: 10px 15px; margin-right: 20px" />

        <input type="button" onclick="javascript:window.print();" 
            style="border-style: solid; border-color: #616161; border-width: 3px; padding: 10px 15px;"
            value="Imprimir" id ="printBtn"  />

    </div>

</asp:Content>
