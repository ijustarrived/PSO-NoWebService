<%@ Page Language="C#" Title ="Permisos" AutoEventWireup="true" MasterPageFile ="~/Site.Master"
     CodeBehind="RolePermisos.aspx.cs" Inherits="PSO.Pages.Dashboard.Configs.RoleEdit" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <style>

        input[type = checkbox]
        {
            vertical-align:bottom
        }

    </style>

    <div style="text-align:center; margin-top:20px">

        <asp:Label ForeColor="#79256E" Font-Size="X-Large" Font-Bold="true" ID ="roleLbl" runat="server"></asp:Label>

    </div>

    <table class="table" style ="width: 70%; margin-left: auto; margin-right: auto;">

        <tr>
            <%-- Revisar --%>

            <td style ="border-top-width:0px; text-align:center">

                <div style="background-color: #616161; padding-left: 7%; margin-top: 40px">

                    <asp:Label ForeColor="#E5E5E5" Font-Size="X-Large" Font-Bold="true" runat="server">Revisar</asp:Label>

                </div>

                <div>

                    <div style="margin-top: 20px;">

                        <asp:Label runat="server" Text="Consulta de Solicitudes Pendientes 
                            a Revisar por Coordinador" ForeColor="#79256E"></asp:Label>

                        <asp:CheckBox runat="server" ID="viewConsuCoorChkBx" />

                    </div>

                    <div style="margin-top: 20px;">

                        <asp:Label runat="server" Text="Consulta de Solicitudes Pendientes
                             a Trabajarse por un Procesador" ForeColor="#79256E"></asp:Label>

                        <asp:CheckBox runat="server" ID="viewConsuProcChkBx" />

                    </div>

                    <div style="margin-top: 20px;">

                        <asp:Label runat="server" Text="Consulta de Solicitudes 
                            &#010; por Nombre, Seguro Social o Status" ForeColor="#79256E"></asp:Label>

                        <asp:CheckBox runat="server" ID="viewConsuSolicitudesChkBx" />

                    </div>

                    <div style="margin-top: 20px;">

                        <asp:Label runat="server" Text="Consulta de Solicitudes
                             Pendientes de Asignar a un Procesador" ForeColor="#79256E"></asp:Label>

                        <asp:CheckBox runat="server" ID="viewConsuPenAsigChkBx" />

                    </div>

                    <div style="margin-top: 20px;">

                        <asp:Label runat="server" Text="Configuraci&oacute;n de Roles" ForeColor="#79256E"></asp:Label>

                        <asp:CheckBox runat="server" ID="viewConfigRoleChkBx" />

                    </div>

                    <div style="margin-top: 20px;">

                        <asp:Label runat="server" Text="Configuraci&oacute;n de Usuarios" ForeColor="#79256E"></asp:Label>

                        <asp:CheckBox runat="server" ID="viewConfigUserChkBx" />

                    </div>

                    <div style="margin-top: 20px;">

                        <asp:Label runat="server" Text="Configuraci&oacute;n de Documentos Requeridos" ForeColor="#79256E"></asp:Label>

                        <asp:CheckBox runat="server" ID="viewConfigDocReqChkBx" />

                    </div>

                    <div style="margin-top: 20px;">

                        <asp:Label runat="server" Text="Reporte de Solicitudes por Status" ForeColor="#79256E"></asp:Label>

                        <asp:CheckBox runat="server" ID="viewReportAvisoStatusChkBx" />

                    </div>

                    <div style="margin-top: 20px;">

                        <asp:Label runat="server" Text="Reporte de Comparaci&oacute;n entre
                             Solicitudes Recibidas y Procesadas" ForeColor="#79256E"></asp:Label>

                        <asp:CheckBox runat="server" ID="viewReportReciVsProcChkBx" />

                    </div>

                    <div style="margin-top: 20px;">

                        <asp:Label runat="server" Text="Reporte de Producción por Rol" ForeColor="#79256E"></asp:Label>

                        <asp:CheckBox runat="server" ID="viewReportProdu" />

                    </div>

                    <div style="margin-top: 20px;">

                        <asp:Label runat="server" Text="Solicitud Servicios" ForeColor="#79256E"></asp:Label>

                        <asp:CheckBox runat="server" ID="viewSolicitudChkBx" />

                    </div>                    

                </div>

            </td>

            <%-- Modificar --%>

            <td style ="border-top-width:0px; text-align:center">

                <div style="background-color: #616161; padding-left: 7%; margin-top: 40px">

                    <asp:Label ForeColor="#E5E5E5" Font-Size="X-Large" Font-Bold="true" runat="server">Modificar</asp:Label>

                </div>

                <div>

                    <div style="margin-top: 20px;">

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
            value="Imprimir"  />

    </div>

    </asp:Content>

