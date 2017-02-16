<%@ Page Language="C#" AutoEventWireup="true" Title ="Reportes" MasterPageFile ="~/Pages/Main.Master" 
    CodeBehind="ReportsMain.aspx.cs" Inherits="PSO.Pages.Dashboard.Reports.ReportsMain" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent2" runat="server">

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
