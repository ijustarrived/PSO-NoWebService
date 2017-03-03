<%@ Page Language="C#" Title="Consultas" MasterPageFile ="~/Pages/Main.Master" AutoEventWireup="true" 
    CodeBehind="ConsultasMain.aspx.cs" Inherits="PSO.Pages.Dashboard.Consultas.ConsultasMain" %>

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

            <asp:Button runat="server" CssClass ="dashBtns" ID="CoordinadorBtn" OnClick ="CoordinadorBtn_Click" 
                Text="Solicitudes Pendientes de Revisarse por Coordinador" />

        </div>

        <div style ="display:inline-block">

            <asp:Button runat="server" CssClass ="dashBtns" ID="pendAsigProBtn" OnClick ="pendAsigProBtn_Click"
                Text="Solicitudes Pendientes de Asignarse a un Procesador" />

        </div>

        <div style ="display:inline-block">

            <asp:Button runat="server" CssClass ="dashBtns" ID="processBtn" OnClick ="processBtn_Click" 
                Text="Solicitudes Pendientes de Trabajarse por un Procesador" />

        </div>

        <div style ="display:inline-block; margin-bottom: 40px;">

            <asp:Button runat="server" CssClass ="dashBtns" ID="servSSNomStatusBtn" OnClick ="servSSNomStatusBtn_Click" 
                Text="Solicitudes &#010; por Nombre, Seguro Social o Status" />

        </div>   

    </div>

</asp:Content>


