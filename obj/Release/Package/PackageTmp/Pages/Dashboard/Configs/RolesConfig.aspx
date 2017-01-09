<%@ Page Language="C#" Title ="Configuraci&oacute;n de Roles" MasterPageFile ="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="RolesConfig.aspx.cs" Inherits="PSO.Pages.Dashboard.Configs.RolesConfig" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <style>

        th
        {
            text-align:center;
        }

    </style>

    <asp:GridView runat ="server" ID ="roleGV" AllowPaging="True" ForeColor="#79256E" BackColor="#F3F0F7"
         CellPadding="2" GridLines="None" CssClass="table" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false"
         style ="margin-top:40px; width:35%; margin-left:auto; margin-right:auto" 
        OnSelectedIndexChanged ="roleGV_SelectedIndexChanged" OnRowDataBound ="roleGV_RowDataBound" >

        <AlternatingRowStyle BackColor ="#E5E5E5" />

        <EmptyDataTemplate>

            <div style="text-align: center">

                <asp:Label ForeColor="#79256E" runat="server">No hay data disponible</asp:Label>

            </div>

        </EmptyDataTemplate>

        <Columns>

            <asp:BoundField DataField="ID" HeaderText="ID">
                <HeaderStyle Font-Size="13pt" />
                <ItemStyle Font-Size="12pt" />
            </asp:BoundField>

            <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                <HeaderStyle Font-Size="13pt" />
                <ItemStyle Font-Size="12pt" />
            </asp:BoundField>

        </Columns>

        <FooterStyle BackColor="#616161" />

        <HeaderStyle BackColor="#616161" ForeColor="#E5E5E5" Font-Bold="True" />

        <PagerStyle BackColor="#616161" Font-Size="Large" ForeColor="#E5E5E5" Font-Bold="true" />

        <SelectedRowStyle BackColor="#79256E" ForeColor="#F3F0F7" />

        <RowStyle HorizontalAlign ="Center" />

    </asp:GridView>

    <div style="text-align: center; margin-bottom: 40px; margin-top:70px; padding-bottom: 100px">

        <input type="button" onclick="javascript:window.print();" 
            style="border-style: solid; border-color: #616161; border-width: 3px; padding: 10px 15px;"
            value="Imprimir"  />

        <%--<asp:Button BorderColor="#616161" CssClass="btn" BorderWidth="3" Text="Imprimir"
            runat="server" OnClientClick="javascript:window.print();" ForeColor="#79256E" Style="padding: 10px 15px;" />--%>

    </div>

    </asp:Content>

