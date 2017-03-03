<%@ Page Language="C#" Title ="" MasterPageFile ="~/Pages/Main.Master" AutoEventWireup="true" 
    CodeBehind="Login.aspx.cs" Inherits="PSO.Pages.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent2" runat="server">

    <div style="width: 310px; margin-left: auto; margin-right: auto; margin-top: 70px; margin-bottom: 60px" class="container">

        <div style="float: left;">

            <asp:Label runat="server" ID="emailLbl" Text="Correo Electr&oacute;nico" ForeColor="#79256E"></asp:Label>

        </div>

        <div>

            <asp:TextBox runat="server" TabIndex="1" ID="emailTxtBx"></asp:TextBox>

        </div>

        <div>

            <asp:RequiredFieldValidator runat="server" ControlToValidate="emailTxtBx" Display="Dynamic"
                ErrorMessage="Requerido" ForeColor="#CC0000" SetFocusOnError="true"></asp:RequiredFieldValidator>

        </div>

    </div>

    <div class="container" style="width: 310px; margin-left: auto; margin-right: auto; margin-bottom: 20px">

        <div style="float: left;">

            <asp:Label runat="server" ID="passLbl" Text="Contraseña" ForeColor="#79256E"></asp:Label>

        </div>

        <div>

            <asp:TextBox runat="server" TabIndex="2" TextMode="Password" ID="passwordTxtBx"></asp:TextBox>

        </div>

        <div>

            <asp:RequiredFieldValidator runat="server" ControlToValidate="passwordTxtBx" Display="Dynamic"
                ErrorMessage="Requerido" ForeColor="#CC0000" SetFocusOnError="true"></asp:RequiredFieldValidator>

        </div>

    </div>

    <div class="container" style="text-align: center; margin-bottom: 70px">

        <asp:HyperLink runat="server" ID="recoverPassLink" Text="Recuperar Contraseña" NavigateUrl="~/Pages/RecoverPass.aspx"
            ForeColor="#79256E"></asp:HyperLink>

    </div>

    <div style="text-align: center; margin-bottom: 40px; padding-bottom: 100px">

        <asp:Button ID="loginBtn" BorderColor="#616161" BorderWidth="3" CssClass="btn" Text="Ingresar"
            Style="margin-right: 20px; padding: 10px 15px;" runat="server" OnClick="loginBtn_Click" ForeColor="#79256E" />

        <asp:Button ID="registerBtn" BorderColor="#616161" CssClass="btn" BorderWidth="3" Text="Registrar"
            runat="server" OnClick="registerBtn_Click" CausesValidation ="false" ForeColor="#79256E" Style="padding: 10px 15px;" />

    </div>

</asp:Content>
