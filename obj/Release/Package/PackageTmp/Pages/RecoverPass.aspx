<%@ Page Language="C#" Title ="Recuperar Contraseña" MasterPageFile ="~/Pages/Main.Master" AutoEventWireup="true" 
    CodeBehind="RecoverPass.aspx.cs" Inherits="PSO.Pages.RecoverPass" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent2" runat="server">

    <div style="width: 310px; margin-left: auto; margin-right: auto; margin-top: 70px; margin-bottom: 20px" class="container">

        <div style="float: left;">

            <asp:Label runat="server" Text="Correo Electr&oacute;nico" ForeColor="#79256E"></asp:Label>

        </div>

        <div>

            <asp:TextBox runat="server" ID="emailTxtBx"></asp:TextBox>

        </div>

    </div>

    <div style="text-align: center; margin-bottom: 40px;">

        <asp:Button ID="bdayBtn" BorderColor="#616161" CssClass="btn" BorderWidth="3" Text="Continuar"
            runat="server" OnClick ="bdayBtn_Click" ForeColor="#79256E" Style="padding: 10px 15px;" />

    </div>

    <div class="container" style="width: 310px; margin-left: auto; margin-right: auto; margin-bottom: 20px"
         runat ="server" id ="ssDiv" visible ="false">

        <div style="float: left;">

            <asp:Label runat="server" Text="Seguro Social" ForeColor="#79256E"></asp:Label>

        </div>

        <div>

            <asp:TextBox runat="server" ID="ssTxtBx"></asp:TextBox>

        </div>

    </div>

    <div style="text-align: center; padding-bottom: 100px">

        <asp:Button ID="recoverBtn" BorderColor="#616161" CssClass="btn" BorderWidth="3" Text="Recuperar Contraseña"
            runat="server" OnClick ="recoverBtn_Click" ForeColor="#79256E" Style="padding: 10px 15px;" Visible ="false" />

    </div>

</asp:Content>
