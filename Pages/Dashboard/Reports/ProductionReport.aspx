<%@ Page Language="C#" Title ="Resultados de Producción por Rol" AutoEventWireup="true" 
    CodeBehind="ProductionReport.aspx.cs" Inherits="PSO.Pages.Dashboard.Reports.ProductionReport" MasterPageFile="~/Site.Master" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
     Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

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

    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.10.2/jquery-ui.js"></script>

    <link rel="stylesheet" href="https://code.jquery.com/ui/1.10.2/themes/smoothness/jquery-ui.css" />

    <div style="text-align: center; margin-top: 20px; margin-bottom: 20px">

        <asp:Label runat="server" Text="Periodo de Fechas" ID ="periodoFechaLbl" ForeColor="#79256E"></asp:Label>

    </div>

    <div style ="text-align:center; margin-top:20px; margin-bottom:20px">

        <script>

            $(
                function () {
                    $("#<% = desdeTxtBx.ClientID%>").datepicker();
                                }
                    );

        </script>

        <asp:TextBox MaxLength ="10" runat="server" Style="width: initial; margin-right: 20px;" ID="desdeTxtBx" placeholder="Desde"></asp:TextBox>

        

        <script>

            $(
                function () {
                    $("#<% = hastaTxtBx.ClientID%>").datepicker();
                                }
                    );

        </script>

          <asp:TextBox MaxLength ="10" runat="server" Style="width: initial" ID="hastaTxtBx" placeholder="Hasta"></asp:TextBox>

        

        <asp:DropDownList runat ="server" ID ="rolDDL" AutoPostBack ="true" OnSelectedIndexChanged ="rolDDL_SelectedIndexChanged">

            <asp:ListItem>Coordinadores</asp:ListItem>

            <asp:ListItem>Procesadores</asp:ListItem>

        </asp:DropDownList>       

    </div>

    <div style="text-align:center; margin-bottom:10px">

        <asp:RegularExpressionValidator runat="server" ControlToValidate="desdeTxtBx" Display="Dynamic" SetFocusOnError="true"
            ErrorMessage ="Inv&aacute;lida. Formato v&aacute;lido es MM/DD/YYYY."
                             ValidationExpression ="^((0?[13578]|10|12)(\/)(([1-9])|(0[1-9])|([12])([0-9]?)|(3[01]?))(\/)((19)([0-9])(\d{1})|(20)([01])(\d{1})|([8901])(\d{1}))|(0?[2469]|11)(\/)(([1-9])|(0[1-9])|([12])([0-9]?)|(3[0]?))(\/)((19)([0-9])(\d{1})|(20)([01])(\d{1})|([8901])(\d{1})))$"
            ForeColor="#CC0000">

        </asp:RegularExpressionValidator>

    </div>

    <div style="text-align:center; margin-bottom:10px">

        <asp:RegularExpressionValidator runat="server" ControlToValidate="hastaTxtBx" Display="Dynamic" SetFocusOnError="true"
            ErrorMessage ="Inv&aacute;lida. Formato v&aacute;lido es MM/DD/YYYY."
                             ValidationExpression ="^((0?[13578]|10|12)(\/)(([1-9])|(0[1-9])|([12])([0-9]?)|(3[01]?))(\/)((19)([0-9])(\d{1})|(20)([01])(\d{1})|([8901])(\d{1}))|(0?[2469]|11)(\/)(([1-9])|(0[1-9])|([12])([0-9]?)|(3[0]?))(\/)((19)([0-9])(\d{1})|(20)([01])(\d{1})|([8901])(\d{1})))$"
            ForeColor="#CC0000">

        </asp:RegularExpressionValidator>

    </div>

    <div style="margin-top: 20px; text-align: center; margin-bottom: 20px">

        <asp:Button Style="padding: 10px 15px;" ID="searchBtn" runat="server" OnClick ="searchBtn_Click" Text="Buscar" />

    </div>

    <div style="text-align: center; overflow:auto">

        <asp:Chart ID="productionChrt" runat="server">

            <Series>

                <asp:Series Name="Employees">

                    <Points></Points>

                </asp:Series>

            </Series>

            <ChartAreas>

                <asp:ChartArea Name="testArea">

                    <AxisY>

                        <MajorGrid LineDashStyle ="NotSet" />

                        <LabelStyle Enabled ="false" />

                    </AxisY>

                    <AxisX>

                        

                        <MajorGrid Enabled="false" />

                        <LabelStyle Font="Microsoft Sans Serif, 30pt" Interval ="1" />

                    </AxisX>

                </asp:ChartArea>

            </ChartAreas>

        </asp:Chart>

    </div>

    <hr style=" width:30%; margin-bottom: 20px; border-color:#616161" />

    <div style="text-align: center; margin-bottom: 20px; margin-top:20px">

        <asp:Label ForeColor="#79256E" runat="server" ID="totalAvisosLbl"></asp:Label>

    </div>

    <div runat ="server" id ="detallesTitleDiv" 
        style="background-color: #616161; margin-top: 40px; text-align:center">

            <asp:Label ForeColor="#E5E5E5" ID ="detailsLbl" Font-Size="X-Large" Font-Bold="true" runat="server">
                Detalle de Producci&oacute;n por Coordinador

            </asp:Label>

        </div>

    <div style="overflow: auto">

        <asp:GridView runat="server" AutoGenerateColumns="false" AllowPaging="True" ForeColor="#79256E" BackColor="#F3F0F7"
            CellPadding="2" GridLines="None" CssClass="table" ShowHeaderWhenEmpty="true" ID="coorGV"
            Style="margin-top: 40px; width: 95%; margin-left: auto; margin-right: auto"
            OnPageIndexChanging="coorGV_PageIndexChanging" OnRowDataBound="coorGV_RowDataBound">

            <AlternatingRowStyle BackColor="#E5E5E5" />

            <EmptyDataTemplate>

                <div style="text-align: center">

                    <asp:Label ForeColor="#79256E" ID="emptyLbl" runat="server">No hay data disponible</asp:Label>

                </div>

            </EmptyDataTemplate>

            <Columns>

                <asp:BoundField DataField="CoordinadorID" HeaderText="Coordinador">
                    <HeaderStyle Font-Size="13pt" />
                    <ItemStyle Font-Size="12pt" />
                </asp:BoundField>

                <asp:BoundField DataField="NumeroSolicitud" HeaderText="N&uacute;mero de Solicitud">
                    <HeaderStyle Font-Size="13pt" />
                    <ItemStyle Font-Size="12pt" />
                </asp:BoundField>

                <asp:BoundField DataField="FechaTramitada" HeaderText="Fecha Recibida">
                    <HeaderStyle Font-Size="13pt" />
                    <ItemStyle Font-Size="12pt" />
                </asp:BoundField>

                <asp:BoundField DataField="FechaRevision" HeaderText="Fecha Revisada">
                    <HeaderStyle Font-Size="13pt" />
                    <ItemStyle Font-Size="12pt" />
                </asp:BoundField>

                <asp:BoundField DataField="Duration" HeaderText="D&iacute;as Transcuridos">
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

        <asp:GridView runat="server" AutoGenerateColumns="false" AllowPaging="True" ForeColor="#79256E" BackColor="#F3F0F7"
            CellPadding="2" GridLines="None" CssClass="table" ShowHeaderWhenEmpty="true" ID="procGV"
            Style="margin-top: 40px; width: 95%; margin-left: auto; margin-right: auto"
            OnRowDataBound="procGV_RowDataBound"
            OnPageIndexChanging="procGV_PageIndexChanging" Visible="false">

            <AlternatingRowStyle BackColor="#E5E5E5" />

            <EmptyDataTemplate>

                <div style="text-align: center">

                    <asp:Label ForeColor="#79256E" ID="emptyLbl" runat="server">No hay data disponible</asp:Label>

                </div>

            </EmptyDataTemplate>

            <Columns>

                <asp:BoundField DataField="ProcesadorID" HeaderText="Procesador">
                    <HeaderStyle Font-Size="13pt" />
                    <ItemStyle Font-Size="12pt" />
                </asp:BoundField>

                <asp:BoundField DataField="NumeroSolicitud" HeaderText="N&uacute;mero de Solicitud">
                    <HeaderStyle Font-Size="13pt" />
                    <ItemStyle Font-Size="12pt" />
                </asp:BoundField>

                <asp:BoundField DataField="FechaAsigProcesador" HeaderText="Fecha Asignada">
                    <HeaderStyle Font-Size="13pt" />
                    <ItemStyle Font-Size="12pt" />
                </asp:BoundField>

                <asp:BoundField DataField="FechaTrabajado" HeaderText="Fecha Completada">
                    <HeaderStyle Font-Size="13pt" />
                    <ItemStyle Font-Size="12pt" />
                </asp:BoundField>

                <asp:BoundField DataField="Duration" HeaderText="D&iacute;as Transcuridos">
                    <HeaderStyle Font-Size="13pt" />
                    <ItemStyle Font-Size="12pt" />
                </asp:BoundField>



            </Columns>

            <FooterStyle BackColor="#616161" />

            <RowStyle HorizontalAlign ="Center" />

            <HeaderStyle BackColor="#616161" ForeColor="#E5E5E5" Font-Bold="True" HorizontalAlign ="Center" />

            <PagerStyle BackColor="#616161" Font-Size="Large" ForeColor="#E5E5E5" Font-Bold="true" />

            <SelectedRowStyle BackColor="#79256E" ForeColor="#F3F0F7" />

        </asp:GridView>

    </div>

    <hr style=" width:30%; margin-bottom: 20px; border-color:#616161" />

    <div style="text-align: center; margin-top:20px">

        <asp:Label ForeColor="#79256E" runat="server" ID="totalPagesLbl"></asp:Label>

    </div>

    <div style="text-align: center; margin-bottom: 40px; margin-top: 70px; padding-bottom: 100px">

        <input type="button" onclick="javascript: window.print();"
            style="border-style: solid; border-color: #616161; border-width: 3px; padding: 10px 15px;"
            value="Imprimir" id ="printBtn" />

    </div>

    </asp:Content>
