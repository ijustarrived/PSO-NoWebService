<%@ Page Language="C#" AutoEventWireup="true" Title ="Inicio" MasterPageFile ="~/Pages/Main.Master"
     CodeBehind="Main.aspx.cs" Inherits="PSO.Pages.Dashboard.Main" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent2" runat="server">

    <script>

        var countDown = 1020;  /*equals 17 minutes in seconds not exact 15 cause it might die before if it was exact due to threa priorities.
        It's converted to seconds cause the interval loop every second not every millisecond*/

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

                    var lockedId = <%= Session["lockedId"] %>;

                    if(lockedId != 0)
                        PageMethods.ReleaseAllLkdSolicitudes(lockedId);

                    window.location.href = '../Login.aspx';
                }

                else
                    countDown = countDown - 1;
            }

    </script>

    <asp:HiddenField ID ="aliveHF" runat ="server" />

    <div style ="text-align:center; margin-top:40px; margin-bottom:40px">

        <div style ="display:inline-block">

            <%--<asp:Button runat="server" CssClass ="dashBtns" ID="solicitudBtn" OnClick ="solicitudBtn_Click" Text="Solicitud de Servicio" />--%>

            <asp:ImageButton runat ="server" style ="padding:0px" CssClass ="dashBtns" ID ="solicitudImgBtn" OnClick ="solicitudBtn_Click"
                ImageUrl ="~/PageImages/WithText/Solicitud_wtext.png" AlternateText ="Solicitud de Servicios" />

        </div>

        <div style ="display:inline-block">

            <%--<asp:Button runat="server" CssClass ="dashBtns" ID="consultaBtn" OnClick ="consultaBtn_Click" Text="Consultas" />--%>

            <asp:ImageButton runat ="server" style ="padding:0px" CssClass ="dashBtns" ID ="consultaImgBtn" OnClick ="consultaBtn_Click"
                ImageUrl = "~/PageImages/WithText/Consultas_wtext.png" AlternateText ="Consultas" />

        </div>

        <div style ="display:inline-block">

            <%--<asp:Button runat="server" CssClass ="dashBtns" ID="reportBtn" OnClick ="reportBtn_Click" Text="Reportes" />--%>

            <asp:ImageButton runat ="server" style ="padding:0px" CssClass ="dashBtns" ID ="reportImgBtn" OnClick ="reportBtn_Click" 
                ImageUrl ="~/PageImages/WithText/Reportes_wtext.png" AlternateText ="Reportes" />

        </div>

        <div style ="display:inline-block">

            <%--<asp:Button runat="server" CssClass ="dashBtns" ID="dashBtn" OnClick ="dashBtn_Click" Text="Dashboard" />--%>

            <asp:ImageButton runat ="server" style ="padding:0px" CssClass ="dashBtns" ID ="dashImgBtn" OnClick ="dashBtn_Click"
                ImageUrl ="~/PageImages/WithText/dashboard.png" AlternateText ="Dashboard" />

        </div> 

        <div style ="display:inline-block">

            <%--<asp:Button runat="server" CssClass ="dashBtns" ID="configBtn" OnClick ="configBtn_Click" Text="Configuraciones" />--%>

            <asp:ImageButton runat ="server" style ="padding:0px" CssClass ="dashBtns" ID ="configImgBtn" OnClick ="configBtn_Click" 
                ImageUrl ="~/PageImages/WithText/configuracion_wtext.png" AlternateText ="Configuración" />

        </div>  
        
        <div style ="display:inline-block">

            <%--<asp:Button runat="server" CssClass ="dashBtns" ID="accountBtn" OnClick ="accountBtn_Click" Text="Perfil" />--%>

            <asp:ImageButton runat ="server" style ="padding:0px" CssClass ="dashBtns" ID ="accountImgBtn" OnClick ="accountBtn_Click"
                ImageUrl ="~/PageImages/WithText/Perfil_wtext.png" AlternateText ="Perfil" />

        </div>  

        <div style ="display:inline-block">

            <%--<asp:Button runat="server" CssClass ="dashBtns" ID="logoutBtn" OnClick ="logoutBtn_Click" Text="Salir" />--%>

            <asp:ImageButton runat ="server" style ="padding:0px" CssClass ="dashBtns" ID ="logoutImgBtn" OnClick ="logoutBtn_Click"
                ImageUrl ="~/PageImages/WithText/Salir_wtext.png" />

        </div> 

        

    </div>

</asp:Content>
