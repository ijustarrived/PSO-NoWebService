<%@ Page Language="C#" Title ="Configuraci&oacute;n de Usuarios" AutoEventWireup="true" MasterPageFile ="~/Site.Master"
     CodeBehind="UsersConfig.aspx.cs" Inherits="PSO.Pages.Dashboard.Configs.UsersConfig" %>

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

            //When the countdown hits 0 it'll do a fake timeout
                if (countDown === 0)
                {
                    clearInterval(timer);

                    var userId = <%= Session["UserId"] %>;

                    UpdateUserLoggedLock(userId, false);

                    var lockedId = <%= Session["lockedId"]%>;

                    if(lockedId != 0)
                        ReleaseAllLkdSolicitudes(lockedId);

                    window.location.href = '../../Login.aspx';
                }

                else
                    countDown = countDown - 1;
        }

        //Releases all solicitudes under that user
        function ReleaseAllLkdSolicitudes(lockedId) 
        {     
            $.ajax
                ({
                    type: "POST",

                    url: 'Solicitud.aspx/ReleaseAllLkdSolicitudes',

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
                url: "/WebServices/LockingService.asmx/UpdateUserLoginLock",
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

    <script>

        function ChangeClinetSideColors(lblColor, titleColor)
        {           
            $('#printBtn').css({ 'color': lblColor });
        }

    </script>

    <div style ="margin-top:20px; text-align:center">

        <asp:Label ID ="tipoUserLbl" ForeColor ="#79256E" runat ="server">Tipo de Usuario</asp:Label>

        <asp:DropDownList runat ="server" ID ="tipoUserDDL" AutoPostBack ="true" OnSelectedIndexChanged ="tipoUserDDL_SelectedIndexChanged">

            <asp:ListItem>Externo</asp:ListItem>

            <asp:ListItem>Interno</asp:ListItem>

        </asp:DropDownList>

    </div>

    <div style="overflow: auto">

    <asp:GridView runat ="server" ID ="userGV" AllowPaging="True" ForeColor="#79256E" BackColor="#F3F0F7"
         CellPadding="2" GridLines="None" CssClass="table" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
         style ="margin-top:40px; width:95%; margin-left:auto; margin-right:auto" 
        OnRowDataBound ="userGV_RowDataBound" OnPageIndexChanging ="userGV_PageIndexChanging" 
        OnSelectedIndexChanged ="userGV_SelectedIndexChanged" >

        <AlternatingRowStyle BackColor ="#E5E5E5" />

        <EmptyDataTemplate>

            <div style="text-align: center">

                <asp:Label ForeColor="#79256E" runat="server">No hay data disponible</asp:Label>

            </div>

        </EmptyDataTemplate>

        <Columns>

            <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                <HeaderStyle Font-Size="13pt" />
                <ItemStyle Font-Size="12pt" />
            </asp:BoundField>

            <asp:BoundField DataField="Email" HeaderText="Correo Electr&oacute;nico">
                <HeaderStyle Font-Size="13pt" />
                <ItemStyle Font-Size="12pt" />
            </asp:BoundField>

            <asp:BoundField DataField="Pueblo" HeaderText="Pueblo">
                <HeaderStyle Font-Size="13pt" />
                <ItemStyle Font-Size="12pt" />
            </asp:BoundField>

            <asp:BoundField DataField="Celular" HeaderText="Tel&eacute;fono Celular">
                <HeaderStyle Font-Size="13pt" />
                <ItemStyle Font-Size="12pt" />
            </asp:BoundField>

            <asp:BoundField DataField="Celular" HeaderText="Tipo de Usuario">
                <HeaderStyle Font-Size="13pt" />
                <ItemStyle Font-Size="12pt" />
            </asp:BoundField>

            <%--<asp:ButtonField CommandName ="activate" ButtonType ="Button" Text ="Desactivar" />--%>

            <asp:TemplateField>

                <ItemTemplate>

                    <asp:Button CausesValidation ="false" Text ="Desactivar" 
                        ID ="activateBtn" runat ="server" OnClick ="Unnamed_Click"   />

                    <%--<asp:Button CommandName ="activate" CausesValidation ="false" Text ="Desactivar" 
                        ID ="activateBtn" runat ="server"  
                        OnCommand ="activateBtn_Command" CommandArgument ="<%# ((GridViewRow) Container).RowIndex %>" />--%>

                </ItemTemplate>

            </asp:TemplateField>

        </Columns>

        <FooterStyle BackColor="#616161" />

        <HeaderStyle BackColor="#616161" ForeColor="#E5E5E5" Font-Bold="True" />

        <PagerStyle BackColor="#616161" Font-Size="Large" CssClass ="gvPager" ForeColor="#E5E5E5" Font-Bold="true" />

        <SelectedRowStyle BackColor="#79256E" ForeColor="#F3F0F7" /></asp:GridView>

        </div>

    <div style="text-align: center; margin-bottom: 40px; margin-top:70px; padding-bottom: 100px">

        <input type="button" onclick="javascript:window.print();" 
            style="border-style: solid; border-color: #616161; border-width: 3px; padding: 10px 15px;"
            value="Imprimir" id ="printBtn" />

    </div>

    </asp:Content>
