<%@ Page Language="C#" Title="Comparaci&oacute;n entre Solicitudes Recibidas y Procesadas" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="RecibidosVsProcReport.aspx.cs" Inherits="PSO.Pages.Dashboard.Reports.RecibidosVsProcReport" %>

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

    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.2/jquery-ui.js"></script>

    <link rel="stylesheet" href="https://code.jquery.com/ui/1.10.2/themes/smoothness/jquery-ui.css" />

    <div style="text-align: center; margin-top: 20px; margin-bottom: 20px">

        <asp:Label runat="server" ID ="periodoFechaLbl" Text="Periodo de Fechas - Solicitudes Recibidas" ForeColor="#79256E"></asp:Label>

    </div>

    <div style="text-align: center; margin-top: 20px">

        <script>

            $(
                function () {
                    $("#<% = desdeTxtBx.ClientID%>").datepicker();
                }
                    );

        </script>

        <asp:TextBox MaxLength ="10" runat="server" Style="width: initial; margin-right: 20px;" ID="desdeTxtBx"
             placeholder="Desde"></asp:TextBox>

        

        <script>

            $(
                function () {
                    $("#<% = hastaTxtBx.ClientID%>").datepicker();
                }
                    );

        </script>

        <asp:TextBox MaxLength ="10" runat="server" Style="width: initial" ID="hastaTxtBx" placeholder="Hasta"></asp:TextBox>

        <div style="margin-bottom: 10px;">

            <asp:RegularExpressionValidator runat="server" ControlToValidate="desdeTxtBx" Display="Dynamic" SetFocusOnError="true"
                ErrorMessage ="Fecha desde es inv&aacute;lida. Formato v&aacute;lido es MM/DD/YYYY."
                             ValidationExpression ="^((0?[13578]|10|12)(\/)(([1-9])|(0[1-9])|([12])([0-9]?)|(3[01]?))(\/)((19)([0-9])(\d{1})|(20)([01])(\d{1})|([8901])(\d{1}))|(0?[2469]|11)(\/)(([1-9])|(0[1-9])|([12])([0-9]?)|(3[0]?))(\/)((19)([0-9])(\d{1})|(20)([01])(\d{1})|([8901])(\d{1})))$"
                ForeColor="#CC0000">

            </asp:RegularExpressionValidator>

        </div>

        <div>

            <asp:RegularExpressionValidator runat="server" ControlToValidate="hastaTxtBx" Display="Dynamic" SetFocusOnError="true"
                ErrorMessage ="Fecha hasta es inv&aacute;lida. Formato v&aacute;lido es MM/DD/YYYY."
                             ValidationExpression ="^((0?[13578]|10|12)(\/)(([1-9])|(0[1-9])|([12])([0-9]?)|(3[01]?))(\/)((19)([0-9])(\d{1})|(20)([01])(\d{1})|([8901])(\d{1}))|(0?[2469]|11)(\/)(([1-9])|(0[1-9])|([12])([0-9]?)|(3[0]?))(\/)((19)([0-9])(\d{1})|(20)([01])(\d{1})|([8901])(\d{1})))$"
                ForeColor="#CC0000">

            </asp:RegularExpressionValidator>

        </div>

    </div>

    <div style="margin-top: 20px; text-align: center; margin-bottom: 20px">

        <asp:Button Style="padding: 10px 15px;" ID="searchBtn" runat="server" OnClick="searchBtn_Click"
            Text="Buscar" />

    </div>

    <div style="text-align: center; margin-bottom: 20px; overflow: auto" runat="server" id="chartDiv">

        <asp:Chart ID="Chart1" runat="server">

            <Series>
                <asp:Series Name="testSeries">

                    <Points>

                        <asp:DataPoint Color="0, 192, 0" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black" AxisLabel="Recibidas" />

                        <asp:DataPoint Color="#888800" Font="Microsoft Sans Serif, 15pt" LabelForeColor="Black"
                            AxisLabel="Procesadas" />

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

                        <LabelStyle Font="Microsoft Sans Serif, 30pt" />

                    </AxisX>
                </asp:ChartArea>
            </ChartAreas>

        </asp:Chart>

        <div runat ="server" id ="detallesTitleDiv" style="background-color: #616161; margin-top: 40px">

            <asp:Label ForeColor="#E5E5E5" Font-Size="X-Large" Font-Bold="true" runat="server">Detalle del Comparativo
                 entre Solicitudes Recibidas y Procesadas</asp:Label>

        </div>

    </div>

    <div style="margin-top: 20px; text-align: center">

        <div style="text-align: center; margin-top: 20px; margin-bottom: 20px">

            <asp:Label runat="server" ID ="periodoFechasDetalleLbl" 
                Text="Periodo de Fechas - Solicitudes Recibidas" ForeColor="#79256E"></asp:Label>

        </div>

        <div style="text-align: center; margin-top: 20px">

            <script>

                $(
                    function () {
                        $("#<% = desdeDetailTxtBx.ClientID%>").datepicker();
                }
                    );

            </script>

            <asp:TextBox MaxLength ="10" runat="server" Style="width: initial; margin-right: 20px;" ID="desdeDetailTxtBx"
                 placeholder="Desde"></asp:TextBox>

            

            <script>

                $(
                    function () {
                        $("#<% = hastaDetailTxtBx.ClientID%>").datepicker();
                }
                    );

            </script>

            <asp:TextBox MaxLength ="10" runat="server" Style="width: initial" ID="hastaDetailTxtBx" placeholder="Hasta"></asp:TextBox>

            <div style="margin-bottom: 10px;">

                <asp:RegularExpressionValidator runat="server" ControlToValidate="desdeDetailTxtBx" Display="Dynamic" SetFocusOnError="true"
                    ErrorMessage ="Fecha desde es inv&aacute;lida. Formato v&aacute;lido es MM/DD/YYYY."
                             ValidationExpression ="^((0?[13578]|10|12)(\/)(([1-9])|(0[1-9])|([12])([0-9]?)|(3[01]?))(\/)((19)([0-9])(\d{1})|(20)([01])(\d{1})|([8901])(\d{1}))|(0?[2469]|11)(\/)(([1-9])|(0[1-9])|([12])([0-9]?)|(3[0]?))(\/)((19)([0-9])(\d{1})|(20)([01])(\d{1})|([8901])(\d{1})))$"
                    ForeColor="#CC0000">

                </asp:RegularExpressionValidator>

            </div>

            <div>

            <asp:RegularExpressionValidator runat="server" ControlToValidate="hastaDetailTxtBx" Display="Dynamic" SetFocusOnError="true"
               ErrorMessage ="Fecha hasta es inv&aacute;lida. Formato v&aacute;lido es MM/DD/YYYY."
                             ValidationExpression ="^((0?[13578]|10|12)(\/)(([1-9])|(0[1-9])|([12])([0-9]?)|(3[01]?))(\/)((19)([0-9])(\d{1})|(20)([01])(\d{1})|([8901])(\d{1}))|(0?[2469]|11)(\/)(([1-9])|(0[1-9])|([12])([0-9]?)|(3[0]?))(\/)((19)([0-9])(\d{1})|(20)([01])(\d{1})|([8901])(\d{1})))$"
                ForeColor="#CC0000">

            </asp:RegularExpressionValidator>

        </div>

        </div>

        <div style="text-align: center; margin-top: 20px; margin-bottom: 20px">

            <asp:Label runat="server" ID ="empleadoLbl" Text="Empleados por Rol" ForeColor="#79256E"></asp:Label>

        </div>

        <div style="text-align: center; margin-top: 10px">

            <asp:DropDownList runat="server" ID="searchDDL" Visible="false">

                <asp:ListItem>Seleccionar</asp:ListItem>

            </asp:DropDownList>

            <asp:DropDownList runat="server" ID="filterDDL" AutoPostBack="true"
                OnSelectedIndexChanged="filterDDL_SelectedIndexChanged">

                <asp:ListItem> Sin Filtro </asp:ListItem>

                <asp:ListItem>Coordinadores</asp:ListItem>

                <asp:ListItem>Procesadores</asp:ListItem>

            </asp:DropDownList>

        </div>

        <div style="text-align: center; margin-top: 20px; margin-bottom: 20px" runat ="server" id ="statusDiv" visible ="false">

            <asp:Label runat="server" ID ="statusLbl" Text="Status por Rol" ForeColor="#79256E"></asp:Label>

            <div style="text-align: center; margin-top: 10px">

            <asp:DropDownList runat="server" ID="statusSearchDDL">

                <asp:ListItem> Sin Filtro </asp:ListItem>

                <asp:ListItem>Pendientes</asp:ListItem>

                <asp:ListItem>Completados</asp:ListItem> 
            </asp:DropDownList>

        </div>

        </div>        

        <div style="text-align: center; margin-top: 20px">

            <asp:Button Style="padding: 10px 15px;" runat="server" ID="searchRolBtn" Text="Buscar"
                OnClick="searchRolBtn_Click" />

        </div>

    </div>

    <div style="overflow: auto">

        <asp:GridView runat="server" AutoGenerateColumns="false" AllowPaging="True" ForeColor="#79256E" BackColor="#F3F0F7"
            CellPadding="2" GridLines="None" CssClass="table" ShowHeaderWhenEmpty="true" ID="recievedGV"
            Style="margin-top: 40px; width: 95%; margin-left: auto; margin-right: auto"
            DataSourceID="solicitudesSQLDS" OnRowDataBound="recievedGV_RowDataBound" OnPageIndexChanging ="recievedGV_PageIndexChanging">

            <AlternatingRowStyle BackColor="#E5E5E5" />

            <EmptyDataTemplate>

                <div style="text-align: center">

                    <asp:Label ForeColor="#79256E" ID="emptyLbl" runat="server">No hay data disponible</asp:Label>

                </div>

            </EmptyDataTemplate>

            <Columns>

                <asp:BoundField DataField="NumeroSolicitud" HeaderText="N&uacute;mero de Solicitud">
                    <HeaderStyle Font-Size="13pt" />
                    <ItemStyle Font-Size="12pt" />
                </asp:BoundField>

                <asp:BoundField DataField="FechaTramitada" HeaderText="Fecha Recibida">
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

                <asp:BoundField DataField="CoordinadorID" HeaderText="Coordinador">
                    <HeaderStyle Font-Size="13pt" />
                    <ItemStyle Font-Size="12pt" />
                </asp:BoundField>

                <asp:BoundField DataField="ProcesadorID" HeaderText="Procesador">
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

    <%--<asp:SqlDataSource runat="server" ID="solicitudesSQLDS" SelectCommand="SELECT CoordinadorID, ProcesadorID, NumeroSolicitud, FechaTramitada,
         FechaTrabajado, DATEDIFF(day, FechaTramitada, FechaTrabajado) AS Duration FROM Solicitudes @WHERE"
        ConnectionString="<%$ ConnectionStrings:local %>" OnSelecting="solicitudesSQLDS_Selecting"></asp:SqlDataSource>--%>

    <%--<asp:SqlDataSource runat="server" ID="solicitudesSQLDS" SelectCommand="SELECT CoordinadorID, ProcesadorID, NumeroSolicitud, FechaTramitada,
         FechaTrabajado, (select DATEDIFF(dd, FechaTramitada, FechaTrabajado) + 
case when DATEPART(dw, FechaTramitada) = 7 then 1 else 0 end -
(DATEDIFF(wk, FechaTramitada, FechaTrabajado) * 2) -
case when DATEPART(dw, FechaTramitada) = 1 then 1 else 0 end +
case when DATEPART(dw, FechaTrabajado) = 1 then 1 else 0 end) AS Duration FROM Solicitudes @WHERE"
        ConnectionString="<%$ ConnectionStrings:local %>" OnSelecting="solicitudesSQLDS_Selecting"></asp:SqlDataSource>--%>

    <hr style=" width:30%; margin-bottom: 20px; border-color:#616161" />

    <div style="text-align: center; margin-bottom: 20px; margin-top:20px">

        <asp:Label ForeColor="#79256E" runat="server" ID="totalAvisosLbl"></asp:Label>

    </div>

    <div style="text-align: center; margin-top:20px">

        <asp:Label ForeColor="#79256E" runat="server" ID="totalPagesLbl"></asp:Label>

    </div>

    <asp:SqlDataSource runat="server" ID="solicitudesSQLDS" SelectCommand="SELECT CoordinadorID, ProcesadorID, NumeroSolicitud, FechaTramitada,
         FechaTrabajado, (ProcesadorID) AS Duration FROM Solicitudes @WHERE"
        ConnectionString="<%$ ConnectionStrings:local %>" OnSelecting="solicitudesSQLDS_Selecting"></asp:SqlDataSource>

    <div style="text-align: center; margin-bottom: 40px; margin-top: 70px; padding-bottom: 100px">

        <input type="button" onclick="javascript: window.print();"
            style="border-style: solid; border-color: #616161; border-width: 3px; padding: 10px 15px;"
            value="Imprimir" id ="printBtn" />

    </div>

</asp:Content>

