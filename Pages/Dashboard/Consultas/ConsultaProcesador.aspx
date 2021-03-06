﻿<%@ Page Language="C#" Title ="Solicitudes Pendientes de Trabajarse por un Procesador" MasterPageFile ="~/Site.Master" AutoEventWireup="true"
     CodeBehind="ConsultaProcesador.aspx.cs" Inherits="PSO.Pages.Dashboard.Consultas.ConsultaProcesador" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <script>

        function ChangeClinetSideColors(lblColor, titleColor)
        {           
            $('#printBtn').css({ 'color': lblColor });
        }

    </script>

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

    <div style ="text-align:center; margin-top:40px">

        <asp:DropDownList ID ="filterDDL" runat ="server" AutoPostBack ="true" 
            OnSelectedIndexChanged ="filterDDL_SelectedIndexChanged">

            <asp:ListItem>Sin filtro</asp:ListItem>

        </asp:DropDownList>

    </div>

    <div style="overflow: auto">

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

        <RowStyle HorizontalAlign ="Center" />

            <HeaderStyle BackColor="#616161" ForeColor="#E5E5E5" Font-Bold="True" HorizontalAlign ="Center" />

        <PagerStyle BackColor="#616161" Font-Size="Large" CssClass ="gvPager" ForeColor="#E5E5E5" Font-Bold="true" />

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

    <asp:SqlDataSource runat ="server" ID ="solicitudesSQLDS" SelectCommand ="SELECT ProcesadorID, NumeroSolicitud, Pueblo,
         (Nombre + ' '+ ApellidoPaterno + ' ' + ApellidoMaterno) AS Nombre, Celular, FechaAsigProcesador
        FROM Solicitudes WHERE Status = 3 @AND" 
        ConnectionString ="<%$ ConnectionStrings:local %>" OnSelecting ="solicitudesSQLDS_Selecting"></asp:SqlDataSource>

    <div style="text-align: center; margin-bottom: 40px; margin-top:70px; padding-bottom: 100px">

        <input type="button" onclick="javascript:window.print();" 
            style="border-style: solid; border-color: #616161; border-width: 3px; padding: 10px 15px;"
            value="Imprimir" id ="printBtn" />

    </div>

</asp:Content>

