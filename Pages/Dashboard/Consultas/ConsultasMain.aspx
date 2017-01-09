<%@ Page Language="C#" Title="Consultas" MasterPageFile ="~/Pages/Main.Master" AutoEventWireup="true" 
    CodeBehind="ConsultasMain.aspx.cs" Inherits="PSO.Pages.Dashboard.Consultas.ConsultasMain" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent2" runat="server">

    <div style ="text-align:center; margin-top:40px">

        <div style ="display:inline-block">

            <asp:Button runat="server" CssClass ="dashBtns" ID="CoordinadorBtn" OnClick ="CoordinadorBtn_Click" 
                Text="Solicitudes Pendientes a Revisar por Coordinador" />

        </div>

        <div style ="display:inline-block">

            <asp:Button runat="server" CssClass ="dashBtns" ID="pendAsigProBtn" OnClick ="pendAsigProBtn_Click"
                Text="Servicios Pendientes de Asignar a un Procesador" />

        </div>

        <div style ="display:inline-block">

            <asp:Button runat="server" CssClass ="dashBtns" ID="processBtn" OnClick ="processBtn_Click" 
                Text="Solicitudes Pendiente a Trabajarse por un Procesador" />

        </div>

        <div style ="display:inline-block">

            <asp:Button runat="server" CssClass ="dashBtns" ID="servSSNomStatusBtn" OnClick ="servSSNomStatusBtn_Click" 
                Text="Servicios Solicitados &#010; por Nombre, Seguro Social o Status" />

        </div>   

    </div>

</asp:Content>


