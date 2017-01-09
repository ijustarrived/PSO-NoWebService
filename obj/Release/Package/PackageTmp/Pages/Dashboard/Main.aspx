<%@ Page Language="C#" AutoEventWireup="true" Title ="Inicio" MasterPageFile ="~/Pages/Main.Master"
     CodeBehind="Main.aspx.cs" Inherits="PSO.Pages.Dashboard.Main" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent2" runat="server">

    <div style ="text-align:center; margin-top:40px; margin-bottom:40px">

        <div style ="display:inline-block">

            <%--<asp:Button runat="server" CssClass ="dashBtns" ID="solicitudBtn" OnClick ="solicitudBtn_Click" Text="Solicitud de Servicio" />--%>

            <asp:ImageButton runat ="server" style ="padding:0px" CssClass ="dashBtns" ID ="solicitudImgBtn" OnClick ="solicitudBtn_Click"
                ImageUrl ="~/PageImages/WithText/Solicitud_wtext.png" />

        </div>

        <div style ="display:inline-block">

            <%--<asp:Button runat="server" CssClass ="dashBtns" ID="consultaBtn" OnClick ="consultaBtn_Click" Text="Consultas" />--%>

            <asp:ImageButton runat ="server" style ="padding:0px" CssClass ="dashBtns" ID ="consultaImgBtn" OnClick ="consultaBtn_Click"
                ImageUrl = "~/PageImages/WithText/Consultas_wtext.png" />

        </div>

        <div style ="display:inline-block">

            <%--<asp:Button runat="server" CssClass ="dashBtns" ID="reportBtn" OnClick ="reportBtn_Click" Text="Reportes" />--%>

            <asp:ImageButton runat ="server" style ="padding:0px" CssClass ="dashBtns" ID ="reportImgBtn" OnClick ="reportBtn_Click" 
                ImageUrl ="~/PageImages/WithText/Reportes_wtext.png" />

        </div>

        <div style ="display:inline-block">

            <%--<asp:Button runat="server" CssClass ="dashBtns" ID="configBtn" OnClick ="configBtn_Click" Text="Configuraciones" />--%>

            <asp:ImageButton runat ="server" style ="padding:0px" CssClass ="dashBtns" ID ="configImgBtn" OnClick ="configBtn_Click" 
                ImageUrl ="~/PageImages/WithText/configuracion_wtext.png" />

        </div>  
        
        <div style ="display:inline-block">

            <%--<asp:Button runat="server" CssClass ="dashBtns" ID="accountBtn" OnClick ="accountBtn_Click" Text="Perfil" />--%>

            <asp:ImageButton runat ="server" style ="padding:0px" CssClass ="dashBtns" ID ="accountImgBtn" OnClick ="accountBtn_Click"
                ImageUrl ="~/PageImages/WithText/Perfil_wtext.png" />

        </div>  

        <div style ="display:inline-block">

            <%--<asp:Button runat="server" CssClass ="dashBtns" ID="logoutBtn" OnClick ="logoutBtn_Click" Text="Salir" />--%>

            <asp:ImageButton runat ="server" style ="padding:0px" CssClass ="dashBtns" ID ="logoutImgBtn" OnClick ="logoutBtn_Click"
                ImageUrl ="~/PageImages/WithText/Salir_wtext.png" />

        </div> 

    </div>

</asp:Content>
