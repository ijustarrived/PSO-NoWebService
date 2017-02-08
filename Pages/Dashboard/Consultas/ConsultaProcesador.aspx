<%@ Page Language="C#" Title ="Solicitudes Pendientes de Trabajarse por un Procesador" MasterPageFile ="~/Site.Master" AutoEventWireup="true"
     CodeBehind="ConsultaProcesador.aspx.cs" Inherits="PSO.Pages.Dashboard.Consultas.ConsultaProcesador" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <script>

        function ChangeClinetSideColors(lblColor, titleColor)
        {           
            $('#printBtn').css({ 'color': lblColor });
        }

    </script>

    <div style ="text-align:center; margin-top:40px">

        <asp:DropDownList ID ="filterDDL" runat ="server" AutoPostBack ="true" 
            OnSelectedIndexChanged ="filterDDL_SelectedIndexChanged">

            <asp:ListItem>Sin filtro</asp:ListItem>

        </asp:DropDownList>

    </div>

    <asp:GridView runat="server" AutoGenerateColumns="false" AllowPaging="True" ForeColor="#79256E" BackColor="#F3F0F7"
        CellPadding="2" GridLines="None" CssClass="table" ShowHeaderWhenEmpty="true" ID="solicitudesGV" 
         DataSourceID ="solicitudesSQLDS" style ="margin-top:40px;  width:95%; margin-left:auto; margin-right:auto"
         OnRowDataBound ="solicitudesGV_RowDataBound" OnPageIndexChanging ="solicitudesGV_PageIndexChanging"
         OnSelectedIndexChanged ="solicitudesGV_SelectedIndexChanged">

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

            <asp:BoundField DataField="ProcesadorID" HeaderText="Procesador">
                <HeaderStyle Font-Size="13pt" />
                <ItemStyle Font-Size="12pt" />
            </asp:BoundField>

             <asp:BoundField DataField="FechaAsigProcesador" HeaderText="Fecha Asignada">
                <HeaderStyle Font-Size="13pt" />
                <ItemStyle Font-Size="12pt" />
            </asp:BoundField>

        </Columns>

        <FooterStyle BackColor="#616161" />

        <HeaderStyle BackColor="#616161" ForeColor="#E5E5E5" Font-Bold="True" />

        <PagerStyle BackColor="#616161" Font-Size="Large" ForeColor="#E5E5E5" Font-Bold="true" />

        <SelectedRowStyle BackColor="#79256E" ForeColor="#F3F0F7" />

    </asp:GridView>

    <asp:SqlDataSource runat ="server" ID ="solicitudesSQLDS" SelectCommand ="SELECT ProcesadorID, NumeroSolicitud, Pueblo,
         (Nombre + ' '+ ApellidoMaterno + ' ' + ApellidoPaterno) AS Nombre, Celular, FechaAsigProcesador
        FROM Solicitudes WHERE Status = 3 @AND" 
        ConnectionString ="<%$ ConnectionStrings:local %>" OnSelecting ="solicitudesSQLDS_Selecting"></asp:SqlDataSource>

    <div style="text-align: center; margin-bottom: 40px; margin-top:70px; padding-bottom: 100px">

        <input type="button" onclick="javascript:window.print();" 
            style="border-style: solid; border-color: #616161; border-width: 3px; padding: 10px 15px;"
            value="Imprimir" id ="printBtn" />

    </div>

</asp:Content>

