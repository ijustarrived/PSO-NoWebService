<%@ Page Language="C#" AutoEventWireup="true" Title="Configuraci&oacute;n de Documentos Requeridos" MasterPageFile="~/Site.Master"
    CodeBehind="DocsReqConfig.aspx.cs" Inherits="PSO.Pages.Dashboard.Configs.DocsReqConfig" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <script>
        
        var countDown = 1020;  /*equals 17 minutes in seconds not exact 15 cause it might die before if it was exact due to threa priorities.
        //It's converted to seconds cause the interval loop every second not every millisecond*/

        var timer = setInterval("KeepAlive()", 1000); //Set to run every 1 sec

        function KeepAlive()
        {
            var aliveHF = document.getElementById("<%= aliveHF.ClientID%>");

            //The purpose of KeepAlive is to run a single line of programmming on the server 
            //just so it can keep the session alive.
            aliveHF.value = PageMethods.KeepAlive();

             var userId = <%= Session["UserId"] %>;

            //When the countdown hits 0 it'll do a fake timeout
                if (countDown === 0)
                {
                    clearInterval(timer);

                    UpdateUserLoggedLock(userId, false);

                    var lockedId = <%= Session["lockedId"]%>;

                    if(lockedId != 0)
                        ReleaseAllLkdSolicitudes(lockedId);

                    window.location.href = '../../Login.aspx';
                }

                else
                {
                    countDown = countDown - 1;

                    UpdateUserLoggedLock(userId, true);
                }
        }

        //Releases all solicitudes under that user
        function ReleaseAllLkdSolicitudes(lockedId) 
        {     
            $.ajax
                ({
                    type: "POST",

                    url: 'Solicitud.aspx/ReleaseAllLkdSolicitudes',

                    data: "{'lockedId':'" + lockedId +"'}",

                    contentType: "application/json; charset=utf-8",

                    dataType: "json",

                    success: function (msg) 
                    {
                    },

                    error: function (e) 
                    {
                       
                    }
                });
        }

        //Updates current user logged lock
        function UpdateUserLoggedLock(userId, shouldBeLoggedLock)
        {
            $.ajax({
                type: "POST",
                url: "/WebServices/LockingService.asmx/UpdateUserLoginLock",
                data: "{'id':'"+ userId +"', 'shouldBeLoggedLocked': '"+ shouldBeLoggedLock +"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success:
                    function sucess(ok) {
                        
                    },
                error:
                    function error(e) {
                        
                    }
            });
        }

    </script>

    <asp:HiddenField ID ="aliveHF" runat ="server" />

    <script>

        function ChangeClinetSideColors(lblColor, titleColor)
        {
            

            $('#printBtn').css({ 'color': lblColor });
        }

    </script>

    <div style ="text-align:center; margin-top:20px">

            <asp:Label runat="server" ID="agregraDocLbl" Font-Size ="X-Large"
                 Text="Agregar un Documento Requerido" ForeColor="#79256E"></asp:Label>

        </div>

    <div class="container" 
        style="width: 310px; margin-left: auto; margin-right: auto; margin-bottom: 20px; margin-top:20px">

        <div style="float: left;">

            <asp:Label runat="server" ID="nombreDocLbl" Text="Nombre del Documento" ForeColor="#79256E"></asp:Label>

        </div>

        <div>

            <asp:TextBox MaxLength ="100" runat="server" ID="docNameTxtBx"></asp:TextBox>

            <div>

                <asp:RequiredFieldValidator ControlToValidate ="docNameTxtBx" 
                    ErrorMessage ="Documento debe de tener un nombre" Display ="Dynamic" 
                    SetFocusOnError ="true" runat ="server" ForeColor ="#CC0000"></asp:RequiredFieldValidator>

            </div>

        </div>

        <div style ="text-align:center; margin-top:20px">

            <asp:Button ID ="createDocReqBtn" OnClick ="createDocReqBtn_Click" BorderColor="#616161" CssClass="btn"
                 BorderWidth="3" Text="Crear" UseSubmitBehavior ="false" runat="server" 
                ForeColor="#79256E" Style="padding: 10px 15px;" />

        </div>

    </div>

    <div style ="text-align:center; margin-top:20px">

            <hr style="background-color: #616161; width: 80%; height: 3px" />

        </div>

    <div style="overflow: auto">

    <asp:GridView runat ="server" ID ="docsGV" DataSourceID ="docsSQLDS" 
        style ="margin-top:40px; width:95%; margin-left:auto; margin-right:auto" 
        AllowPaging="True" ForeColor="#79256E" BackColor="#F3F0F7" CellPadding="2" GridLines="None" CssClass="table" 
        ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" OnRowDataBound ="docsGV_RowDataBound"
          DataKeyNames="ID">

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

            <asp:ButtonField CommandName ="Edit" Text ="Renombrar" />

            <asp:ButtonField CommandName ="Update"  Text ="Actualizar" />

            <asp:ButtonField CommandName ="Delete" Text ="Eliminar" />        

        </Columns>

        <FooterStyle BackColor="#616161" />

        <HeaderStyle BackColor="#616161" ForeColor="#E5E5E5" Font-Bold="True" />

        <PagerStyle BackColor="#616161" Font-Size="Large" CssClass ="gvPager" ForeColor="#E5E5E5" Font-Bold="true" />

        <SelectedRowStyle BackColor="#79256E" ForeColor="#F3F0F7" />

    </asp:GridView>

        </div>

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
            value="Imprimir" id ="printBtn" />
    </div>

    </asp:Content>
