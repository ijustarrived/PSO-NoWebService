<%@ Page Language="C#" Title ="Recuperar Contraseña" MasterPageFile ="~/Pages/Main.Master" AutoEventWireup="true" 
    CodeBehind="RecoverPass.aspx.cs" Inherits="PSO.Pages.RecoverPass" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent2" runat="server">

    <script src="../Scripts/jquery.mask.js" type="text/javascript"></script>

    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.2/jquery-ui.js"></script>

    <link rel="stylesheet" href="https://code.jquery.com/ui/1.10.2/themes/smoothness/jquery-ui.css" />

    <div style="width: 310px; margin-left: auto; margin-right: auto; margin-top: 70px; margin-bottom: 20px" class="container">

        <div style="float: left;">

            <asp:Label runat="server" ID ="emailLbl" Text="Correo Electr&oacute;nico*" ForeColor="#79256E"></asp:Label>

        </div>

        <div>

            <asp:TextBox runat="server" ID="emailTxtBx"></asp:TextBox>

        </div>

        <div>

            <asp:RequiredFieldValidator runat="server" ControlToValidate="emailTxtBx" Display="Dynamic"
                ErrorMessage="Requerido" ForeColor="#CC0000"
                SetFocusOnError="true" ID ="asigCommentRFV"></asp:RequiredFieldValidator>

        </div>

    </div>

    <div style="text-align: center; margin-bottom: 40px;">

        <asp:Button ID="bdayBtn" BorderColor="#616161" CssClass="btn" BorderWidth="3" Text="Continuar"
            runat="server" OnClick ="bdayBtn_Click" ForeColor="#79256E" Style="padding: 10px 15px;" />

    </div>

    <div class="container" style="width: 310px; margin-left: auto; margin-right: auto; margin-bottom: 20px"
         runat ="server" id ="ssDiv" visible ="false">

        <div style="float: left;">

            <asp:Label runat="server" ID ="bdayLbl" Text="Fecha de Nacimiento*" ForeColor="#79256E"></asp:Label>

        </div>

        <div>

            <script>

                            $(function ()
                            {
                                $("#<% = ssTxtBx.ClientID%>").datepicker();
                            });

                        </script>

            <asp:TextBox runat="server" ID="ssTxtBx"></asp:TextBox>

        </div>

        <div>

            <asp:RequiredFieldValidator runat="server" ControlToValidate="ssTxtBx" Display="Dynamic"
                ErrorMessage="Requerido" ForeColor="#CC0000" 
                SetFocusOnError="true" ID ="RequiredFieldValidator1"></asp:RequiredFieldValidator>

        </div>

        <div>

            <asp:RegularExpressionValidator runat="server" ControlToValidate="ssTxtBx" Display="Dynamic" SetFocusOnError="true"
                ErrorMessage ="Inv&aacute;lida. Formato v&aacute;lido es MM/DD/YYYY."
                             ValidationExpression ="^((0?[13578]|10|12)(\/)(([1-9])|(0[1-9])|([12])([0-9]?)|(3[01]?))(\/)((19)([0-9])(\d{1})|(20)([01])(\d{1})|([8901])(\d{1}))|(0?[2469]|11)(\/)(([1-9])|(0[1-9])|([12])([0-9]?)|(3[0]?))(\/)((19)([0-9])(\d{1})|(20)([01])(\d{1})|([8901])(\d{1})))$"
                ForeColor="#CC0000">

            </asp:RegularExpressionValidator>

        </div>

    </div>

    <div style="text-align: center; padding-bottom: 100px">

        <asp:Button ID="recoverBtn" BorderColor="#616161" CssClass="btn" BorderWidth="3" Text="Recuperar Contraseña"
            runat="server" OnClick ="recoverBtn_Click" ForeColor="#79256E" Style="padding: 10px 15px;" Visible ="false" />

    </div>

</asp:Content>
