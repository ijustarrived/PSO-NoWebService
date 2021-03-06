﻿<%@ Page Language="C#" AutoEventWireup="true" Title="Configuraci&oacute;n de Documentos Requeridos" MasterPageFile="~/Site.Master"
    CodeBehind="DocsReqConfig.aspx.cs" Inherits="PSO.Pages.Dashboard.Configs.DocsReqConfig" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div style ="text-align:center; margin-top:20px">

            <asp:Label runat="server" Font-Size ="X-Large" Text="Agregar un Documento Requerido" ForeColor="#79256E"></asp:Label>

        </div>

    <div class="container" style="width: 310px; margin-left: auto; margin-right: auto; margin-bottom: 20px; margin-top:20px">

        <div style="float: left;">

            <asp:Label runat="server" Text="Nombre del Documento" ForeColor="#79256E"></asp:Label>

        </div>

        <div>

            <asp:TextBox runat="server" ID="docNameTxtBx"></asp:TextBox>

        </div>

        <div style ="text-align:center; margin-top:20px">

            <asp:Button ID ="createDocReqBtn" OnClick ="createDocReqBtn_Click" BorderColor="#616161" CssClass="btn"
                 BorderWidth="3" Text="Crear" runat="server" ForeColor="#79256E" Style="padding: 10px 15px;" />

        </div>

    </div>

    <div style ="text-align:center; margin-top:20px">

            <hr style="background-color: #616161; width: 80%; height: 3px" />

        </div>


    <asp:GridView runat ="server" ID ="docsGV" DataSourceID ="docsSQLDS" style ="margin-top:40px; width:95%; margin-left:auto; margin-right:auto" 
        AllowPaging="True" ForeColor="#79256E" BackColor="#F3F0F7" CellPadding="2" GridLines="None" CssClass="table" 
        ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" OnRowDataBound ="docsGV_RowDataBound"
          DataKeyNames="ID" >

        <AlternatingRowStyle BackColor ="#E5E5E5" />

        <EmptyDataTemplate>

            <div style="text-align: center">

                <asp:Label ForeColor="#79256E" runat="server">No hay data disponible</asp:Label>

            </div>

        </EmptyDataTemplate>

        <Columns>

            <asp:BoundField DataField="ID" ReadOnly ="true" >
                <HeaderStyle Font-Size="13pt" />
                <ItemStyle Font-Size="12pt" />
            </asp:BoundField>

            <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                <HeaderStyle Font-Size="13pt" />
                <ItemStyle Font-Size="12pt" />
            </asp:BoundField>

            <asp:ButtonField CommandName ="Delete" Text ="Eliminar" />

            <asp:ButtonField CommandName ="Edit" Text ="Renombrar" />

            <asp:ButtonField CommandName ="Update"  Text ="Actualizar" />

        </Columns>

        <FooterStyle BackColor="#616161" />

        <HeaderStyle BackColor="#616161" ForeColor="#E5E5E5" Font-Bold="True" />

        <PagerStyle BackColor="#616161" Font-Size="Large" ForeColor="#E5E5E5" Font-Bold="true" />

        <SelectedRowStyle BackColor="#79256E" ForeColor="#F3F0F7" />

    </asp:GridView>

    <asp:SqlDataSource runat ="server" ID ="docsSQLDS" ConnectionString="<%$ ConnectionStrings:local %>"
                SelectCommand="SELECT ID, Nombre FROM TitulosDocumentos ORDER BY Nombre ASC"
         DeleteCommand ="DELETE FROM TitulosDocumentos WHERE ID = @ID" 
        UpdateCommand ="UPDATE TitulosDocumentos SET Nombre = @Nombre WHERE ID = @ID">

        <DeleteParameters>

            <asp:Parameter Name ="ID" Type ="Int32" />

        </DeleteParameters>

        <UpdateParameters>

            <asp:Parameter Name ="Nombre" Type ="String" />

        </UpdateParameters>

    </asp:SqlDataSource>

    <div style="text-align: center; margin-bottom: 40px; margin-top: 70px; padding-bottom: 100px">

        <input type="button" onclick="javascript:window.print();" 
            style="border-style: solid; border-color: #616161; border-width: 3px; padding: 10px 15px;"
            value="Imprimir"  />

        <%--<asp:Button BorderColor="#616161" CssClass="btn" BorderWidth="3" Text="Imprimir"
            runat="server" OnClientClick="javascript:window.print();" ForeColor="#79256E" Style="padding: 10px 15px;" />--%>
    </div>

    </asp:Content>
