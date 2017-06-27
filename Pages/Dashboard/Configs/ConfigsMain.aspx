<%@ Page Language="C#" Title ="Configuraciones" MasterPageFile ="~/Pages/Main.Master" AutoEventWireup="true"
     CodeBehind="ConfigsMain.aspx.cs" Inherits="PSO.Pages.Dashboard.Configs.ConfigsMain" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent2" runat="server">

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

                    url: '../Solicitud.aspx/ReleaseAllLkdSolicitudes',

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
                url: "../Main.aspx/UpdateUserLoginLock",
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

        <div style ="display:inline-block; margin-bottom: 40px;">

            <asp:Button runat="server" CssClass ="dashBtns" ID="customizePagesBtn" OnClick ="customizePagesBtn_Click"
                Text="Personalizar Sistema" />

        </div> 

    </div>

</asp:Content>
