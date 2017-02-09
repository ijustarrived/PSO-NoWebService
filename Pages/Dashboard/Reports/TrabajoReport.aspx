<%@ Page Language="C#" Title ="Indicadores de Productividad en Proceso de Manejo de Solicitudes" AutoEventWireup="true" MasterPageFile ="~/Site.Master"
     CodeBehind="TrabajoReport.aspx.cs" Inherits="PSO.Pages.Dashboard.Reports.TrabajoReport" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <script>

        function ChangeClinetSideColors(lblColor, titleColor)
        {           
            $('#printBtn').css({ 'color': lblColor });
        }

    </script>

    <div style="margin-top: 20px; overflow: auto">

        <table class="table" style="margin-left: auto; margin-right: auto; width: 95%">

            <%-- Header --%>
            <tr>

                <th runat="server" id="fillerTitleTh"
                    scope="col" style="color: #E5E5E5; background-color: #616161;"></th>

                <th runat="server" id="metaTitleTh" scope="col"
                    style="color: #E5E5E5; background-color: #616161;">

                    <asp:Label Font-Size="13" runat="server">Meta</asp:Label>

                </th>

                <th runat="server" id="eneTitleTh" scope="col"
                    style="color: #E5E5E5; background-color: #616161;">

                    <asp:Label Font-Size="13" runat="server">Ene</asp:Label>

                </th>

                <th runat="server" id="febTitleTh" scope="col"
                    style="color: #E5E5E5; background-color: #616161">

                    <asp:Label Font-Size="13" runat="server">Feb</asp:Label>

                </th>

                <th runat="server" id="marTitleTh" scope="col"
                    style="color: #E5E5E5; background-color: #616161;">

                    <asp:Label Font-Size="13" runat="server">Mar</asp:Label>

                </th>

                <th runat="server" id="abrTitleTh"
                    scope="col" style="color: #E5E5E5; background-color: #616161">

                    <asp:Label Font-Size="13" runat="server">Abr</asp:Label>

                </th>

                <th runat="server" id="mayTitleTh"
                    scope="col" style="color: #E5E5E5; background-color: #616161;">

                    <asp:Label Font-Size="13" runat="server">May</asp:Label>

                </th>

                <th runat="server" id="junTitleTh"
                    scope="col" style="color: #E5E5E5; background-color: #616161;">

                    <asp:Label Font-Size="13" runat="server">Jun</asp:Label>

                </th>

                <th scope="col" runat="server" id="julTitleTh"
                    style="color: #E5E5E5; background-color: #616161;">

                    <asp:Label Font-Size="13" runat="server">Jul</asp:Label>

                </th>

                <th scope="col" runat="server" id="agoTitleTh"
                    style="color: #E5E5E5; background-color: #616161;">

                    <asp:Label Font-Size="13" runat="server">Ago</asp:Label>

                </th>

                <th scope="col" runat="server" id="sepTitleTh"
                    style="color: #E5E5E5; background-color: #616161">

                    <asp:Label Font-Size="13" runat="server">Sep</asp:Label>

                </th>

                <th scope="col" runat="server" id="octTitleTh"
                    style="color: #E5E5E5; background-color: #616161">

                    <asp:Label Font-Size="13" runat="server">Oct</asp:Label>

                </th>

                <th scope="col" runat="server" id="novTitleTh"
                    style="color: #E5E5E5; background-color: #616161">

                    <asp:Label Font-Size="13" runat="server">Nov</asp:Label>

                </th>

                <th scope="col" runat="server" id="dicTitleTh"
                    style="color: #E5E5E5; background-color: #616161">

                    <asp:Label Font-Size="13" runat="server">Dic</asp:Label>

                </th>

            </tr>

            <%-- Coor --%>
            <tr>

                <td runat="server" id="coorLblTh" style="color: #79256E; background-color: #F3F0F7;">

                    <asp:Label Font-Size="13" runat="server">Procesos Coordinador</asp:Label>

                </td>

                <td runat="server" id="coorMetaLblTh" style="color: #79256E; background-color: #F3F0F7;">

                    <asp:Label Font-Size="13" runat="server">1 Día</asp:Label>

                </td>

                <td runat="server" id="coorEneLblTh" style="color: #79256E; background-color: #F3F0F7;">

                    <asp:Label ID="eneCoorLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="coorFebLblTh" style="color: #79256E; background-color: #F3F0F7;">

                    <asp:Label ID="febCoorLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="coorMarLblTh" style="color: #79256E; background-color: #F3F0F7;">

                    <asp:Label ID="marCoorLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="coorAbrLblTh" style="color: #79256E; background-color: #F3F0F7;">

                    <asp:Label ID="abrCoorLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="coorMayLblTh" style="color: #79256E; background-color: #F3F0F7;">

                    <asp:Label ID="mayCoorLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="coorJunLblTh" style="color: #79256E; background-color: #F3F0F7;">

                    <asp:Label ID="junCoorLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="coorJulLblTh" style="color: #79256E; background-color: #F3F0F7;">

                    <asp:Label ID="julCoorLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="coorAgoLblTh" style="color: #79256E; background-color: #F3F0F7;">

                    <asp:Label ID="agoCoorLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="coorSepLblTh" style="color: #79256E; background-color: #F3F0F7;">

                    <asp:Label ID="sepCoorLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="coorOctLblTh" style="color: #79256E; background-color: #F3F0F7;">

                    <asp:Label ID="octCoorLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="coorNovLblTh" style="color: #79256E; background-color: #F3F0F7;">

                    <asp:Label ID="novCoorLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="coorDicLblTh" style="color: #79256E; background-color: #F3F0F7;">

                    <asp:Label ID="dicCoorLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

            </tr>

            <%-- Super --%>
            <tr>

                <td runat="server" id="supLblTh" style="color: #79256E; background-color: #E5E5E5;">

                    <asp:Label Font-Size="13" runat="server">Procesos Supervisor</asp:Label>

                </td>

                <td runat="server" id="supMetaLblTh" style="color: #79256E; background-color: #E5E5E5;">

                    <asp:Label Font-Size="13" runat="server">3 Días</asp:Label>

                </td>

                <td runat="server" id="supEneLblTh" style="color: #79256E; background-color: #E5E5E5;">

                    <asp:Label ID="eneSupLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="supFebLblTh" style="color: #79256E; background-color: #E5E5E5;">

                    <asp:Label ID="febSupLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="supMarLblTh" style="color: #79256E; background-color: #E5E5E5;">

                    <asp:Label ID="marSupLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="supAbrLblTh" style="color: #79256E; background-color: #E5E5E5;">

                    <asp:Label ID="abrSupLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="supMayLblTh" style="color: #79256E; background-color: #E5E5E5;">

                    <asp:Label ID="maySupLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="supJunLblTh" style="color: #79256E; background-color: #E5E5E5;">

                    <asp:Label ID="junSupLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="supJulLblTh" style="color: #79256E; background-color: #E5E5E5;">

                    <asp:Label ID="julSupLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="supAgoLblTh" style="color: #79256E; background-color: #E5E5E5;">

                    <asp:Label ID="agoSupLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="supSepLblTh" style="color: #79256E; background-color: #E5E5E5;">

                    <asp:Label ID="sepSupLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="supOctLblTh" style="color: #79256E; background-color: #E5E5E5;">

                    <asp:Label ID="octSupLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="supNovLblTh" style="color: #79256E; background-color: #E5E5E5;">

                    <asp:Label ID="novSupLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="supDicLblTh" style="color: #79256E; background-color: #E5E5E5;">

                    <asp:Label ID="dicSupLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

            </tr>

            <%-- Proc --%>
            <tr>

                <td runat="server" id="proLblTh" style="color: #79256E; background-color: #F3F0F7;">

                    <asp:Label Font-Size="13" runat="server">Procesos Procesador</asp:Label>

                </td>

                <td runat="server" id="proMetaLblTh" style="color: #79256E; background-color: #F3F0F7;">

                    <asp:Label Font-Size="13" runat="server">3 Días</asp:Label>

                </td>

                <td runat="server" id="proEneLblTh" style="color: #79256E; background-color: #F3F0F7;">

                    <asp:Label ID="eneProcLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="proFebLblTh" style="color: #79256E; background-color: #F3F0F7;">

                    <asp:Label ID="febProcLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="proMarLblTh" style="color: #79256E; background-color: #F3F0F7;">

                    <asp:Label ID="marProcLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="proAbrLblTh" style="color: #79256E; background-color: #F3F0F7;">

                    <asp:Label ID="abrProcLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="proMayLblTh" style="color: #79256E; background-color: #F3F0F7;">

                    <asp:Label ID="mayProcLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="proJunLblTh" style="color: #79256E; background-color: #F3F0F7;">

                    <asp:Label ID="junProcLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="proJulLblTh" style="color: #79256E; background-color: #F3F0F7;">

                    <asp:Label ID="julProcLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="proAgoLblTh" style="color: #79256E; background-color: #F3F0F7;">

                    <asp:Label ID="agoProcLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="proSepLblTh" style="color: #79256E; background-color: #F3F0F7;">

                    <asp:Label ID="sepProcLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="proOctLblTh" style="color: #79256E; background-color: #F3F0F7;">

                    <asp:Label ID="octProcLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="proNovLblTh" style="color: #79256E; background-color: #F3F0F7;">

                    <asp:Label ID="novProcLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

                <td runat="server" id="proDicLblTh" style="color: #79256E; background-color: #F3F0F7;">

                    <asp:Label ID="dicProcLbl" Font-Size="13" runat="server"></asp:Label>

                </td>

            </tr>

        </table>

    </div>

    <div style="text-align: center; margin-bottom: 40px; margin-top:70px; padding-bottom: 100px">

        <input type="button" onclick="javascript:window.print();" 
            style="border-style: solid; border-color: #616161; border-width: 3px; padding: 10px 15px;"
            value="Imprimir" id ="printBtn" />

    </div>

</asp:Content>