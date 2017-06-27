<%@ Page Language="C#" Title ="Permisos" AutoEventWireup="true" MasterPageFile ="~/Site.Master"
     CodeBehind="RolePermisos.aspx.cs" Inherits="PSO.Pages.Dashboard.Configs.RoleEdit" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <script>
        
        var countDown = 1020;  /*equals 17 minutes in seconds not exact 15 cause it might die before if it was exact due to threa priorities.
        //It's converted to seconds cause the interval loop every second not every millisecond*/

        var timer = setInterval("KeepAlive()", 1000); //Set to run every 1 sec

        function KeepAlive()
        {
            var aliveHF = document.getElementById("<%= aliveHF.ClientID%>");

            //The purpose of KeepAlive is to run a single line of programmming on the server 
            //just so it can keep the session alive.
            aliveHF.value = PageMethods.KeepAlive();

             var userId = <%= Session["UserId"] %>;

            //When the countdown hits 0 it'll do a fake timeout
                if (countDown === 0)
                {
                    clearInterval(timer);

                    UpdateUserLoggedLock(userId, false);

                    var lockedId = <%= Session["lockedId"]%>;

                    if(lockedId != 0)
                        ReleaseAllLkdSolicitudes(lockedId);

                    window.location.href = '../../Login.aspx';
                }

                else
                {
                    countDown = countDown - 1;

                    UpdateUserLoggedLock(userId, true);
                }
        }

        //Releases all solicitudes under that user
        function ReleaseAllLkdSolicitudes(lockedId) 
        {     
            $.ajax
                ({
                    type: "POST",

                    url: '../Solicitud.aspx/ReleaseAllLkdSolicitudes',

                    data: "{'lockedId':'" + lockedId +"'}",

                    contentType: "application/json; charset=utf-8",

                    dataType: "json",

                    success: function (msg) 
                    {
                    },

                    error: function (e) 
                    {
                       
                    }
                });
        }

        //Updates current user logged lock
        function UpdateUserLoggedLock(userId, shouldBeLoggedLock)
        {
            $.ajax({
                type: "POST",
                url: "../Main.aspx/UpdateUserLoginLock",
                data: "{'id':'"+ userId +"', 'shouldBeLoggedLocked': '"+ shouldBeLoggedLock +"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success:
                    function sucess(ok) {
                        
                    },
                error:
                    function error(e) {
                        
                    }
            });
        }

    </script>

    <asp:HiddenField ID ="aliveHF" runat ="server" />

    <style>

        input[type = checkbox]
        {
            vertical-align:bottom
        }

    </style>

    <script>

        function ChangeClinetSideColors(lblColor, titleColor)
        {    
            $('#printBtn').css({ 'color': lblColor });
        }

    </script>

    <div style="text-align:center; margin-top:20px">

        <asp:Label ForeColor="#79256E" Font-Size="X-Large" Font-Bold="true" ID ="roleLbl" runat="server"></asp:Label>

    </div>

    <table class="table" style ="width: 70%; margin-left: auto; margin-right: auto;">

        <tr>
            <%-- Revisar --%>

            <td style ="border-top-width:0px; text-align:center">

                <div runat ="server" id ="consultaTitleDiv" style="background-color: #616161; padding-left: 7%; margin-top: 40px">

                    <asp:Label ForeColor="#E5E5E5" Font-Size="X-Large"
                        Font-Bold="true" runat="server">Consultar</asp:Label>

                </div>

                <div>

                    <div style="margin-top: 20px;">

                        <asp:Label runat="server" ID ="consultaCoorLbl" Text="Consulta de Solicitudes Pendientes 
                            a Revisar por Coordinador" ForeColor="#79256E"></asp:Label>

                        <asp:CheckBox runat="server" ID="viewConsuCoorChkBx" />

                    </div>

                    <div style="margin-top: 20px;">

                        <asp:Label runat="server" ID ="consultaProLbl" Text="Consulta de Solicitudes Pendientes
                             a Trabajarse por un Procesador" ForeColor="#79256E"></asp:Label>

                        <asp:CheckBox runat="server" ID="viewConsuProcChkBx" />

                    </div>

                    <div style="margin-top: 20px;">

                        <asp:Label runat="server" ID ="consultaSolicitudLbl" Text="Consulta de Solicitudes 
                            &#010; por Nombre, Seguro Social o Status" ForeColor="#79256E"></asp:Label>

                        <asp:CheckBox runat="server" ID="viewConsuSolicitudesChkBx" />

                    </div>

                    <div style="margin-top: 20px;">

                        <asp:Label runat="server" ID ="consultaSupLbl" Text="Consulta de Solicitudes
                             Pendientes de Asignar a un Procesador" ForeColor="#79256E"></asp:Label>

                        <asp:CheckBox runat="server" ID="viewConsuPenAsigChkBx" />

                    </div>

                    <div style="margin-top: 20px;">

                        <asp:Label runat="server" ID ="configRoleLbl" Text="Configuraci&oacute;n de Roles"
                             ForeColor="#79256E"></asp:Label>

                        <asp:CheckBox runat="server" ID="viewConfigRoleChkBx" />

                    </div>

                    <div style="margin-top: 20px;">

                        <asp:Label runat="server" ID ="configUserLbl" Text="Configuraci&oacute;n de Usuarios"
                             ForeColor="#79256E"></asp:Label>

                        <asp:CheckBox runat="server" ID="viewConfigUserChkBx" />

                    </div>

                    <div style="margin-top: 20px;">

                        <asp:Label runat="server" ID ="consultaDocLbl" 
                            Text="Configuraci&oacute;n de Documentos Requeridos" ForeColor="#79256E"></asp:Label>

                        <asp:CheckBox runat="server" ID="viewConfigDocReqChkBx" />

                    </div>

                    <div style="margin-top: 20px;">

                        <asp:Label runat="server" Text="Indicadores de Productividad en Proceso de Manejo de Solicitudes"
                             ForeColor="#79256E" ID ="indicadoresLbl"></asp:Label>

                        <asp:CheckBox runat="server" ID="viewReportIndicadoresChkBx" />

                    </div>

                    <div style="margin-top: 20px;">

                        <asp:Label runat="server" ID ="reportComparacionLbl" Text="Reporte de Comparaci&oacute;n entre
                             Solicitudes Recibidas y Procesadas" ForeColor="#79256E"></asp:Label>

                        <asp:CheckBox runat="server" ID="viewReportReciVsProcChkBx" />

                    </div>

                    <div style="margin-top: 20px;">

                        <asp:Label runat="server" ID ="reportProductionLbl" Text="Reporte de Producción por Rol"
                             ForeColor="#79256E"></asp:Label>

                        <asp:CheckBox runat="server" ID="viewReportProdu" />

                    </div>

                    <%--<div style="margin-top: 20px;">

                        <asp:Label ID ="solicitudLbl" runat="server" Text="Solicitud Servicios" ForeColor="#79256E"></asp:Label>

                        <asp:CheckBox runat="server" ID="viewSolicitudChkBx" />

                    </div>--%>  
                    
                    <div style="margin-top: 20px;">

                        <asp:Label runat="server" ID ="dashBrLbl" Text="Dashboard" ForeColor="#79256E"></asp:Label>

                        <asp:CheckBox runat="server" ID="viewReportAvisoStatusChkBx" />

                    </div>                  

                </div>

            </td>

            <%-- Modificar --%>

            <td style ="border-top-width:0px; text-align:center">

                <div runat ="server" id ="modificarTitleDiv" style="background-color: #616161; padding-left: 7%; margin-top: 40px">

                    <asp:Label ForeColor="#E5E5E5" Font-Size="X-Large"
                         Font-Bold="true" runat="server">Modificar</asp:Label>

                </div>

                <div>

                    <%--<div style="margin-top: 20px;">

                        <asp:Label runat="server" Text="Roles" ForeColor="#79256E"></asp:Label>

                        <asp:CheckBox runat="server" ID="editRoleChkBx" />

                    </div>

                    <div style="margin-top: 20px;">

                        <asp:Label runat="server" Text="Usuarios" ForeColor="#79256E"></asp:Label>

                        <asp:CheckBox runat="server" ID="editUsersChkBx" />

                    </div>

                    <div style="margin-top: 20px;">

                        <asp:Label runat="server" Text="Documentos Requeridos" ForeColor="#79256E"></asp:Label>

                        <asp:CheckBox runat="server" ID="editDocReqChkBx" />

                    </div>--%>

                    <div style="margin-top: 20px;">

                        <asp:Label runat="server" ID ="CustomizeLbl" Text="Personalizar Sistema" ForeColor="#79256E"></asp:Label>

                        <asp:CheckBox runat="server" ID="customizePagesChkBx" />

                    </div>

                    <div style="margin-top: 20px;">

                        <asp:Label ID ="solicitudLbl" runat="server" Text="Solicitud Servicios" ForeColor="#79256E"></asp:Label>

                        <asp:CheckBox runat="server" ID="viewSolicitudChkBx" />

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
            value="Imprimir" id ="printBtn" />

    </div>

    </asp:Content>

