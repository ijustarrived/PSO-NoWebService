<%@ Page Language="C#" Title ="Solicitudes por Nombre, Seguro Social o Status" MasterPageFile ="~/Site.Master" AutoEventWireup="true"
     CodeBehind="ConsultaServSSNomStatus.aspx.cs" Inherits="PSO.Pages.Dashboard.Consultas.ConsultaServSSNomStatus" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

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

    <script>

        function filterDDLChanged(filterDDL)
        {
            var statusDDL = document.getElementById("<%= statusDDL.ClientID%>");

            var searchTxtBx = document.getElementById("<%= searchTxtBx.ClientID%>");

            searchTxtBx.value = "";

            if (filterDDL.selectedIndex == 3)
            {
                statusDDL.style.display = "initial";

                searchTxtBx.style.display = "none";
            }

            else if (filterDDL.selectedIndex == 0)
            {
                statusDDL.style.display = "none";

                searchTxtBx.style.display = "none";
            }

            else
            {
                statusDDL.style.display = "none";

                searchTxtBx.style.display = "initial";

                searchTxtBx.focus();
            }
        }

        function ChangeClinetSideColors(lblColor, titleColor)
        {           
            $('#printBtn').css({ 'color': lblColor });
        }

    </script>

    <div class="container" style="text-align:center; margin-left: auto; margin-right: auto; margin-bottom: 20px; margin-top: 20px">

        <asp:TextBox MaxLength ="100" ID ="searchTxtBx" Width="20%" style ="display:none" runat ="server"></asp:TextBox>

        <asp:DropDownList ID ="statusDDL" runat ="server" style ="display:none">

            <asp:ListItem>Pendiente de Revisarse por un Coordinador</asp:ListItem>
            <asp:ListItem>Pendiente de Asignarse a un Procesador</asp:ListItem>
            <asp:ListItem>Pendiente de Trabajarse por un Procesador</asp:ListItem>
            <asp:ListItem>Aprobada</asp:ListItem>
            <asp:ListItem>Denegada</asp:ListItem>
            <asp:ListItem>Pendiente por Documentos Incompletos</asp:ListItem>
            <asp:ListItem>Inactiva</asp:ListItem> 

        </asp:DropDownList>

        <asp:DropDownList ID ="filterDDL" runat="server" onchange ="filterDDLChanged(this);">
            
                <asp:ListItem>Sin Filtro</asp:ListItem>

                <asp:ListItem>Nombre</asp:ListItem>

                <asp:ListItem>Seguro Social</asp:ListItem>

                <asp:ListItem>Status</asp:ListItem>

            </asp:DropDownList>

        <div style ="margin-top:20px">

            <asp:Button style ="padding: 10px 15px;" ID ="searchBtn" runat ="server" OnClick ="searchBtn_Click" Text ="Buscar" />

        </div>

    </div>

    <div style="overflow: auto">

    <asp:GridView runat="server" AutoGenerateColumns="false" AllowPaging="True" ForeColor="#79256E" BackColor="#F3F0F7"
        CellPadding="2" GridLines="None" CssClass="table" ShowHeaderWhenEmpty="true" ID="solicitudesGV" 
         DataSourceID ="solicitudesSQLDS" style ="margin-top:40px;  width:95%; margin-left:auto; margin-right:auto" 
        OnRowDataBound ="solicitudesGV_RowDataBound" OnSelectedIndexChanged ="solicitudesGV_SelectedIndexChanged"
         OnPageIndexChanging ="solicitudesGV_PageIndexChanging">

        <AlternatingRowStyle BackColor ="#E5E5E5" />

        <EmptyDataTemplate>

            <div style="text-align: center">

                <asp:Label ForeColor="#79256E" ID ="emptyLbl" runat="server">No hay data disponible</asp:Label>

            </div>

        </EmptyDataTemplate>

        <Columns>

            <asp:BoundField DataField="NumeroSolicitud" HeaderText="N&uacute;mero de Solicitud">
                <HeaderStyle Font-Size="13pt" />
                <ItemStyle Font-Size="12pt" />
            </asp:BoundField>

            <asp:BoundField DataField="SeguroSocial" HeaderText="Seguro Social">
                <HeaderStyle Font-Size="13pt" />
                <ItemStyle Font-Size="12pt" />
            </asp:BoundField>

            <asp:BoundField DataField="Nombre" HeaderText="Nombre del Solicitante">
                <HeaderStyle Font-Size="13pt" />
                <ItemStyle Font-Size="12pt" />
            </asp:BoundField>

            <asp:BoundField DataField="Celular" HeaderText="Tel&eacute;fono Celular">
                <HeaderStyle Font-Size="13pt" />
                <ItemStyle Font-Size="12pt" />
            </asp:BoundField>

            <asp:BoundField DataField="Pueblo" HeaderText="Pueblo">
                <HeaderStyle Font-Size="13pt" />
                <ItemStyle Font-Size="12pt" />
            </asp:BoundField>


        </Columns>

        <FooterStyle BackColor="#616161" />

        <HeaderStyle BackColor="#616161" ForeColor="#E5E5E5" Font-Bold="True" />

        <PagerStyle BackColor="#616161" Font-Size="Large" ForeColor="#E5E5E5" Font-Bold="true" />

        <SelectedRowStyle BackColor="#79256E" ForeColor="#F3F0F7" />

    </asp:GridView>

        </div>

     <hr style=" width:30%; margin-bottom: 20px; border-color:#616161" />

    <div style="text-align: center; margin-bottom: 20px; margin-top:20px">

        <asp:Label ForeColor="#79256E" runat="server" ID="totalAvisosLbl"></asp:Label>

    </div>

    <div style="text-align: center; margin-top:20px">

        <asp:Label ForeColor="#79256E" runat="server" ID="totalPagesLbl"></asp:Label>

    </div>

    <asp:SqlDataSource runat ="server" ID ="solicitudesSQLDS" SelectCommand ="SELECT NumeroSolicitud, Pueblo,
         (Nombre + ' '+ ApellidoPaterno + ' ' + ApellidoMaterno) AS Nombre, Celular, SeguroSocial FROM Solicitudes @WHERE" 
        ConnectionString ="<%$ ConnectionStrings:local %>" OnSelecting ="solicitudesSQLDS_Selecting"></asp:SqlDataSource>

    <div style="text-align: center; margin-bottom: 40px; margin-top:70px; padding-bottom: 100px">

        <input type="button" onclick="javascript:window.print();" 
            style="border-style: solid; border-color: #616161; border-width: 3px; padding: 10px 15px;"
            value="Imprimir" id ="printBtn" />

    </div>

</asp:Content>

