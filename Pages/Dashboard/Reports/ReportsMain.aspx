<%@ Page Language="C#" AutoEventWireup="true" Title ="Reportes" MasterPageFile ="~/Pages/Main.Master" 
    CodeBehind="ReportsMain.aspx.cs" Inherits="PSO.Pages.Dashboard.Reports.ReportsMain" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent2" runat="server">

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

    <div style ="text-align:center; margin-top:40px">

        <div style ="display:inline-block">

            <asp:Button runat="server" CssClass ="dashBtns" ID="RecibidosVsProcessBtn" OnClick ="RecibidosVsProcessBtn_Click"
                Text="Comparaci&oacute;n entre Solicitudes Recibidas y Procesadas" />

        </div>

        <div style ="display:inline-block">

            <asp:Button runat="server" CssClass ="dashBtns" ID="indicadorBtn" OnClick ="indicadorBtn_Click"
                Text="Resultados de Producción por Rol" />

        </div> 

        <div style ="display:inline-block; margin-bottom: 40px;">

            <asp:Button runat="server" CssClass ="dashBtns" ID="productionBtn" OnClick ="productionBtn_Click"
                Text="Resultados de Producción por Rol" />

        </div> 

    </div>

</asp:Content>
