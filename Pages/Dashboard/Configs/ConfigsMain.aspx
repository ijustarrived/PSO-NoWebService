<%@ Page Language="C#" Title ="Configuraciones" MasterPageFile ="~/Pages/Main.Master" AutoEventWireup="true"
     CodeBehind="ConfigsMain.aspx.cs" Inherits="PSO.Pages.Dashboard.Configs.ConfigsMain" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent2" runat="server">

    <div style ="text-align:center; margin-top:40px">

        <div style ="display:inline-block">

            <asp:Button runat="server" CssClass ="dashBtns" ID="usersBtn" OnClick ="usersBtn_Click" Text="Usuarios" />

        </div>

        <div style ="display:inline-block">

            <asp:Button runat="server" CssClass ="dashBtns" ID="docsReqBtn" OnClick ="docsReqBtn_Click" Text="Documentos Requeridos" />

        </div>

        <div style ="display:inline-block">

            <asp:Button runat="server" CssClass ="dashBtns" ID="rolesBtn" OnClick ="rolesBtn_Click"
                Text="Roles" />

        </div> 

        <div style ="display:inline-block">

            <asp:Button runat="server" CssClass ="dashBtns" ID="customizePagesBtn" OnClick ="customizePagesBtn_Click"
                Text="Personalizar Sistema" />

        </div> 

    </div>

</asp:Content>
