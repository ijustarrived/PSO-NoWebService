﻿<%@ Page Language="C#" Title="Solicitud de Servicio" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Solicitud.aspx.cs" Inherits="PSO.Pages.Dashboard.Solicitud" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.2/jquery-ui.js"></script>

    <link rel="stylesheet" href="https://code.jquery.com/ui/1.10.2/themes/smoothness/jquery-ui.css" />

    <script src="../../Scripts/jquery.mask.js" type="text/javascript"></script> 

    <script>

        function DisplayError(msg)
        {
            alert(msg);
        }

        function WaitingAnswerAlert(alertMsg)
        {
            alert(alertMsg);

            window.location = "Main.aspx";
        }

        function EncryptDrivers(word, event)
        {
            var wordSplit = word.value.split('');

            var hf = document.getElementById("<%= decryptedDriversHF.ClientID%>");

            //Clear txtbx if backspace or delete is pressed cause I have no way which key is being deleted
            if (event.keyCode === 8 || event.keyCode === 46)
            {
                word.value = "";

                hf.value = "";
            }

            else
            {
                for (var i = 0; i < wordSplit.length; i++)
                {
                    //it's a way to check if it's a numeric value
                    var result = wordSplit[i] * 0;

                    if (wordSplit[i] != '*')
                    {
                        //Do not accept alphanum values
                        if (isNaN(result))
                            wordSplit[i] = '';

                        else
                        {
                            //Safe encrypted keys in hf for later use
                            hf.value = hf.value + wordSplit[i];

                            wordSplit[i] = '*';
                        }
                    }
                }

                word.value = ""

                //Rebuild txtbx value
                for (var i = 0; i < wordSplit.length; i++)
                {
                    word.value = word.value + wordSplit[i];
                }
            }
        }

        //Check the none Co
        function EncryptDriversCo(word, event)
        {
            var wordSplit = word.value.split('');

            var hf = document.getElementById("<%= decryptedDriversCoHF.ClientID%>");

            if (event.keyCode === 8 || event.keyCode === 46)
            {
                word.value = "";

                hf.value = "";
            }

            else
            {
                for (var i = 0; i < wordSplit.length; i++) {
                    var result = wordSplit[i] * 0;

                    if (wordSplit[i] != '*') {
                        if (isNaN(result))
                            wordSplit[i] = '';

                        else {
                            hf.value = hf.value + wordSplit[i];

                            wordSplit[i] = '*';
                        }
                    }
                }

                word.value = ""

                for (var i = 0; i < wordSplit.length; i++) {
                    word.value = word.value + wordSplit[i];
                }
            }
        }        

        function EncryptSSN(word, event)
        {
            var hf = document.getElementById("<%= encryptedSSNPartHF.ClientID%>");

            //Delete txtbx value if delete or backspace key is pressed
            if (event.keyCode === 8 || event.keyCode === 46)
            {
                word.value = "";

                hf.value = "";
            }

            else 
            {
                var maxEncryptedCount = 0;

                //Set how long the encrypted part should be
                if (word.value.includes("-"))
                    maxEncryptedCount = 8;

                else
                    maxEncryptedCount = 6; 

                var wordSplit = word.value.split('');

                //Encrypt the 1st  and 2nd set of digits
                if (wordSplit.length < maxEncryptedCount)
                {
                    for (var i = 0; i < wordSplit.length; i++)
                    {                        
                        var result = wordSplit[i] * 0;

                        //Check if input is numeric
                        if (result === 0)
                        {
                            if (wordSplit[i] != '*')
                            {
                                //Store encypted side for later use
                                hf.value = hf.value + wordSplit[i];

                                wordSplit[i] = '*';
                            }
                        }

                         //Erase any alphanum values
                        else if (wordSplit[i] != '*' && wordSplit[i] != '-')
                        {
                            wordSplit[i] = ''
                        }
                    }
                }

                //Get rid of any alphanum on the last set of digits
                else if (wordSplit.length >= maxEncryptedCount)
                {
                    for (var i = (maxEncryptedCount - 1) ; i < wordSplit.length ; i++)
                    {
                        var result = wordSplit[i] * 0;

                        //Check if value is numeric
                        if (isNaN(result))
                            wordSplit[i] = '';
                    }
                }

                word.value = ""

                //Rebuild SSN using all new sets
                for (var i = 0; i < wordSplit.length; i++)
                {
                    var result = wordSplit[i] * 0;

                    //Add dashes
                    if ((i === 3 || i === 6) && (wordSplit[i] != '-') && (wordSplit[i] === '*'))
                        wordSplit[i] = "-" + wordSplit[i];

                    //Double check if 1st and 2nd sets are numeric cause doesn't check when working on the last set
                    if (!isNaN(result) || wordSplit[i] === '-' || wordSplit[i] === '*'
                        || wordSplit[i].includes("-"))
                    {
                        word.value = word.value + wordSplit[i];
                    }                    
                }
            }
        }

        //Same as none Co version
        function EncryptSSNCo(word, event)
        {
            var hf = document.getElementById("<%= ssEncryptedPartCoHF.ClientID%>");

            if (event.keyCode === 8 || event.keyCode === 46)
            {
                word.value = "";

                hf.value = "";
            }

            else 
            {
                var maxEncryptedCount = 0;

                if (word.value.includes("-"))
                    maxEncryptedCount = 8;

                else
                    maxEncryptedCount = 6; 

                if (word.value.length < maxEncryptedCount && word.value.length < hf.value.length) {
                    hf.value = "";
                }

                var wordSplit = word.value.split('');

                if (wordSplit.length < maxEncryptedCount) {
                    for (var i = 0; i < wordSplit.length; i++) {
                        var result = wordSplit[i] * 0;

                        if (result === 0) {
                            if (wordSplit[i] != '*') {
                                hf.value = hf.value + wordSplit[i];

                                wordSplit[i] = '*';
                            }
                        }

                        else if (wordSplit[i] != '*' && wordSplit[i] != '-') {
                            wordSplit[i] = ''
                        }
                    }
                }

                else if (wordSplit.length >= maxEncryptedCount) {
                    for (var i = (maxEncryptedCount - 1) ; i < wordSplit.length ; i++) {
                        var result = wordSplit[i] * 0;

                        if (isNaN(result))
                            wordSplit[i] = '';
                    }
                }

                word.value = ""

                for (var i = 0; i < wordSplit.length; i++)
                {
                    var result = wordSplit[i] * 0;

                    if ((i === 3 || i === 6) && (wordSplit[i] != '-') && (wordSplit[i] === '*'))
                        wordSplit[i] = "-" + wordSplit[i];

                    if (!isNaN(result) || wordSplit[i] === '-' || wordSplit[i] === '*'
                        || wordSplit[i].includes("-"))
                    {
                        word.value = word.value + wordSplit[i];
                    }
                }
            }
        }

    </script>

    <%-- Header --%>

    <div style="text-align: center; margin-top: 20px" runat ="server" id ="statusDiv" visible ="false">

        <asp:Label runat="server" Text="Status" Font-Size="X-Large" ForeColor="#79256E"></asp:Label>

        <asp:Label runat="server" Font-Size="X-Large" ID="statusTxtLbl" Text="Tipo"></asp:Label>

    </div>

    <div style="margin-top: 20px">

        <table class="table" style="color: #79256E; width: 70%; margin-left: auto; margin-right: auto">

            <tr runat ="server" id ="userRow" visible ="false">

                <td style="text-align: right; border-top-style: none; padding-right:20% ">

                    <asp:Label runat="server" Text="Número Solicitud:" ID="numSolicitudLbl"></asp:Label>

                </td>

                <td style="padding-left:10%; border-top-style: none">

                    <asp:Label runat="server" Text="Fecha Solicitud:" ID="fechaSolicitudLbl"></asp:Label>

                </td>

            </tr>

            <tr runat ="server" id ="coorRow" visible ="false">

                <td style="text-align: right; border-top-style: none; padding-right:20%">

                    <asp:Label runat="server" Text="Coordinador" ID="coordinadorLbl"></asp:Label>

                    <asp:DropDownList runat ="server" ID ="coorDDL"></asp:DropDownList>

                </td>

                <td style="padding-left:10%; border-top-style: none">

                    <asp:Label runat="server" Text="Fecha Revisado:" ID="fechaRevisadoLbl"></asp:Label>

                </td>

            </tr>

        </table>

    </div>

    <div style="width: 70%; margin-left: auto; margin-right: auto; text-align: center" runat="server" visible="false" id="asigCommentDiv">

        <asp:Label ForeColor="#79256E" ID ="asigLbl" Text="Comentarios*" runat="server"></asp:Label>

        <asp:TextBox ID="asigComment" runat="server" TextMode="MultiLine"></asp:TextBox>

        <div>

            <asp:RequiredFieldValidator runat="server" ControlToValidate="asigComment" Display="Dynamic"
                ErrorMessage="Requerido" ForeColor="#CC0000" Enabled="false" 
                SetFocusOnError="true" ID ="asigCommentRFV"></asp:RequiredFieldValidator>

        </div>

    </div>

    <table class="table" style="color: #79256E; margin-top: 20px; width: 70%; margin-left: auto; margin-right: auto"
        >

        <tr runat ="server" id ="asigRow" visible ="false">

                <td style="text-align: right; border-top-style: none; padding-right:20%">

                    <asp:Label runat="server" Text="Asignado a" ID="Label3"></asp:Label>

                    <asp:DropDownList runat="server" ID="procesadoresDDL">

                    </asp:DropDownList>

                </td>

                <td style="border-top-style: none; padding-left: 10%;">

                    <asp:Label runat="server" Text="Fecha Asignado:" ID="fechaAsignadoLbl"></asp:Label>

                </td>

            </tr>

        <tr runat ="server" id ="trabajadoRow" visible ="false">

            <td style="text-align: right; padding-right:10%; border-top-style: none">


                <asp:Label runat="server" Text="Trabajado en Sistema de Préstamos: Sí" ID="trabajoSistemaLbl"></asp:Label>
            </td>

            <td style="border-top-style: none">

                <asp:Label runat="server" Text="Fecha Trabajado:" ID="fechaTrabajoLbl"></asp:Label>

            </td>

        </tr>

    </table>

    <div style="width: 70%; margin-left: auto; margin-right: auto; text-align: center" 
        runat ="server" id ="trabajoCommentDiv" visible ="false">

        <asp:Label ForeColor="#79256E" Text="Comentarios" runat="server"></asp:Label>

        <asp:TextBox ID="trabajoComment" runat="server" TextMode="MultiLine"></asp:TextBox>

    </div>

    <div style ="text-align:center; margin-top:40px">

        <asp:Label ForeColor ="#79256E" Font-Bold ="true" runat ="server" Text ="Los campos con ' * ', son requeridos"></asp:Label>

    </div>

    <%-- Info Solicitante --%>

    <div style="background-color: #616161; padding-left: 7%; margin-top:40px">

        <asp:Label ForeColor="#E5E5E5" Font-Size="X-Large" Font-Bold="true" runat="server">Informaci&oacute;n del Solicitante</asp:Label>

    </div>

    <table class ="table" style="color: #79256E; margin-top: 20px;">

        <tr>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div style=" ">

                        <asp:Label runat="server" Text="Nombre*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server"   ID="nameTxtBx"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="nameTxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div style=" ">

                        <asp:Label runat="server" Text="Apellido Paterno*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server"   ID="paternoTxtBx"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="paternoTxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div style=" ">

                        <asp:Label runat="server" Text="Apellido Materno*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server"   ID="maternoTxtBx"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="maternoTxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

        </tr>

        <tr>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div style=" ">

                        <asp:Label runat="server" Text="Seguro Social*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <%--<script>

                            $(function ()
                            {
                                $("#<% = ssTxtBx.ClientID%>").mask("999-99-9999");
                            });

                        </script>--%>

                        <asp:HiddenField ID ="encryptedSSNPartHF" runat ="server" />

                        <asp:TextBox runat="server" onkeyup="EncryptSSN(this, event);"
                            MaxLength ="11" ID="ssTxtBx" AutoCompleteType ="Disabled"></asp:TextBox>

                    </div>

                     <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="ssTxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div style=" ">

                        <asp:Label runat="server" Text="Correo Electr&oacute;nico*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server"   ID="emailTxtBx"></asp:TextBox>

                    </div>

                    <div>
                        
                        <asp:RegularExpressionValidator runat ="server" ControlToValidate ="emailTxtBx" Display ="Dynamic" SetFocusOnError ="true" 
                            ErrorMessage ="Incorrecto"
                             ValidationExpression ="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor ="#CC0000">

                        </asp:RegularExpressionValidator>


                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="emailTxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div style=" ">

                        <script>

                            $(
                                function () {
                                    $("#<% = bdayTxtBx.ClientID%>").datepicker();
                                }
                    );

                        </script>

                        <asp:Label runat="server" Text="Fecha Nacimiento*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server"   ID="bdayTxtBx"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RegularExpressionValidator runat ="server" ControlToValidate ="bdayTxtBx" Display ="Dynamic" SetFocusOnError ="true" 
                            ErrorMessage ="Incorrecto. Formato valido es MM/DD/YYYY"
                             ValidationExpression ="^((0?[13578]|10|12)(-|\/)(([1-9])|(0[1-9])|([12])([0-9]?)|(3[01]?))(-|\/)((19)([2-9])(\d{1})|(20)([01])(\d{1})|
                            ([8901])(\d{1}))|(0?[2469]|11)(-|\/)(([1-9])|(0[1-9])|([12])([0-9]?)|(3[0]?))(-|\/)((19)([2-9])(\d{1})
                            |(20)([01])(\d{1})|([8901])(\d{1})))$" ForeColor ="#CC0000">

                        </asp:RegularExpressionValidator>

                    </div>

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="bdayTxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

        </tr>

        <tr>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div style=" ">

                        <asp:Label runat="server" Text="Licencia Conducir*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:HiddenField ID ="decryptedDriversHF" runat ="server" />

                        <asp:TextBox runat="server" MaxLength ="7" onkeyup ="EncryptDrivers(this, event);" 
                            ID="driversTxtBx" AutoCompleteType ="Disabled"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="driversTxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div style=" ">

                        <asp:Label runat="server" Text="Tel&eacute;fono Celular*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <script>

                            $(function ()
                            {
                                $("#<% = celTxtBx.ClientID%>").mask("999-999-9999");
                            });

                        </script>

                        <asp:TextBox runat="server"   ID="celTxtBx"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="celTxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div style=" ">

                        <asp:Label runat="server" Text="Tel&eacute;fono Residencial*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <script>

                            $(function ()
                            {
                                $("#<% = telResiTxtBx.ClientID%>").mask("999-999-9999");
                            });

                        </script>

                        <asp:TextBox runat="server"   ID="telResiTxtBx"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="telResiTxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

        </tr>

        <tr>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div style=" ">

                        <asp:Label runat="server" Text="Direcci&oacute;n Residencial*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server" TextMode ="MultiLine" ID="dirTxtBx"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="dirTxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div style=" ">

                        <asp:Label runat="server" Text="Pueblo*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:DropDownList ID="puebloDDL" runat="server">

                            <asp:ListItem>Adjuntas</asp:ListItem>
                            <asp:ListItem>Aguada</asp:ListItem>
                            <asp:ListItem>Aguadilla</asp:ListItem>
                            <asp:ListItem>Aguas Buenas</asp:ListItem>
                            <asp:ListItem>Aibonito</asp:ListItem>
                            <asp:ListItem>Añasco</asp:ListItem>
                            <asp:ListItem>Arecibo</asp:ListItem>
                            <asp:ListItem>Arroyo</asp:ListItem>
                            <asp:ListItem>Barceloneta</asp:ListItem>
                            <asp:ListItem>Barranquitas</asp:ListItem>
                            <asp:ListItem>Bayamón</asp:ListItem>
                            <asp:ListItem>Cabo Rojo</asp:ListItem>
                            <asp:ListItem>Caguas</asp:ListItem>
                            <asp:ListItem>Camuy</asp:ListItem>
                            <asp:ListItem>Canóvanas</asp:ListItem>
                            <asp:ListItem>Carolina</asp:ListItem>
                            <asp:ListItem>Cataño</asp:ListItem>
                            <asp:ListItem>Cayey</asp:ListItem>
                            <asp:ListItem>Ceiba</asp:ListItem>
                            <asp:ListItem>Ciales</asp:ListItem>
                            <asp:ListItem>Cidra</asp:ListItem>
                            <asp:ListItem>Coamo</asp:ListItem>
                            <asp:ListItem>Comerío</asp:ListItem>
                            <asp:ListItem>Corozal</asp:ListItem>
                            <asp:ListItem>Culebra (Isla municipio)</asp:ListItem>
                            <asp:ListItem>Dorado</asp:ListItem>
                            <asp:ListItem>Fajardo</asp:ListItem>
                            <asp:ListItem>Florida</asp:ListItem>
                            <asp:ListItem>Guánica</asp:ListItem>
                            <asp:ListItem>Guayama</asp:ListItem>
                            <asp:ListItem>Guayanilla</asp:ListItem>
                            <asp:ListItem>Guaynabo</asp:ListItem>
                            <asp:ListItem>Gurabo</asp:ListItem>
                            <asp:ListItem>Hatillo</asp:ListItem>
                            <asp:ListItem>Hormigueros</asp:ListItem>
                            <asp:ListItem>Humacao</asp:ListItem>
                            <asp:ListItem>Isabela</asp:ListItem>
                            <asp:ListItem>Jayuya</asp:ListItem>
                            <asp:ListItem>Juana Díaz</asp:ListItem>
                            <asp:ListItem>Juncos</asp:ListItem>
                            <asp:ListItem>Lajas</asp:ListItem>
                            <asp:ListItem>Lares</asp:ListItem>
                            <asp:ListItem>Las Marías</asp:ListItem>
                            <asp:ListItem>Las Piedras</asp:ListItem>
                            <asp:ListItem>Loíza</asp:ListItem>
                            <asp:ListItem>Luquillo</asp:ListItem>
                            <asp:ListItem>Manatí</asp:ListItem>
                            <asp:ListItem>Maricao</asp:ListItem>
                            <asp:ListItem>Maunabo</asp:ListItem>
                            <asp:ListItem>Mayagüez</asp:ListItem>
                            <asp:ListItem>Moca</asp:ListItem>
                            <asp:ListItem>Morovis</asp:ListItem>
                            <asp:ListItem>Naguabo</asp:ListItem>
                            <asp:ListItem>Naranjito</asp:ListItem>
                            <asp:ListItem>Orocovis</asp:ListItem>
                            <asp:ListItem>Patillas</asp:ListItem>
                            <asp:ListItem>Peñuelas</asp:ListItem>
                            <asp:ListItem>Ponce</asp:ListItem>
                            <asp:ListItem>Quebradillas</asp:ListItem>
                            <asp:ListItem>Rincón</asp:ListItem>
                            <asp:ListItem>Río Grande</asp:ListItem>
                            <asp:ListItem>Sabana Grande</asp:ListItem>
                            <asp:ListItem>Salinas</asp:ListItem>
                            <asp:ListItem>San Germán</asp:ListItem>
                            <asp:ListItem>San Juan</asp:ListItem>
                            <asp:ListItem>San Lorenzo</asp:ListItem>
                            <asp:ListItem>San Sebastián</asp:ListItem>
                            <asp:ListItem>Santa Isabel</asp:ListItem>
                            <asp:ListItem>Toa Alta</asp:ListItem>
                            <asp:ListItem>Toa Baja</asp:ListItem>
                            <asp:ListItem>Trujillo Alto</asp:ListItem>
                            <asp:ListItem>Utuado</asp:ListItem>
                            <asp:ListItem>Vega Alta</asp:ListItem>
                            <asp:ListItem>Vega Baja</asp:ListItem>
                            <asp:ListItem>Vieques (Isla municipio)</asp:ListItem>
                            <asp:ListItem>Villalba</asp:ListItem>
                            <asp:ListItem>Yabucoa</asp:ListItem>
                            <asp:ListItem>Yauco</asp:ListItem>

                        </asp:DropDownList>

                    </div>

                </div>

            </td>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div style=" ">

                        <asp:Label runat="server" Text="C&oacute;digo Postal*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server"   ID="codigoTxtBx"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="codigoTxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

        </tr>

        <tr>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div style=" ">

                        <asp:Label runat="server" Text="Direcci&oacute;n Postal*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server" TextMode ="MultiLine" ID="dirPostalTxtBx"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="dirPostalTxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div style=" ">

                        <asp:Label runat="server" Text="Pueblo*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:DropDownList ID ="puebloPostalDDL" runat ="server">

                            <asp:ListItem>Adjuntas</asp:ListItem>
                            <asp:ListItem>Aguada</asp:ListItem>
                            <asp:ListItem>Aguadilla</asp:ListItem>
                            <asp:ListItem>Aguas Buenas</asp:ListItem>
                            <asp:ListItem>Aibonito</asp:ListItem>
                            <asp:ListItem>Añasco</asp:ListItem>
                            <asp:ListItem>Arecibo</asp:ListItem>
                            <asp:ListItem>Arroyo</asp:ListItem>
                            <asp:ListItem>Barceloneta</asp:ListItem>
                            <asp:ListItem>Barranquitas</asp:ListItem>
                            <asp:ListItem>Bayamón</asp:ListItem>
                            <asp:ListItem>Cabo Rojo</asp:ListItem>
                            <asp:ListItem>Caguas</asp:ListItem>
                            <asp:ListItem>Camuy</asp:ListItem>
                            <asp:ListItem>Canóvanas</asp:ListItem>
                            <asp:ListItem>Carolina</asp:ListItem>
                            <asp:ListItem>Cataño</asp:ListItem>
                            <asp:ListItem>Cayey</asp:ListItem>
                            <asp:ListItem>Ceiba</asp:ListItem>
                            <asp:ListItem>Ciales</asp:ListItem>
                            <asp:ListItem>Cidra</asp:ListItem>
                            <asp:ListItem>Coamo</asp:ListItem>
                            <asp:ListItem>Comerío</asp:ListItem>
                            <asp:ListItem>Corozal</asp:ListItem>
                            <asp:ListItem>Culebra (Isla municipio)</asp:ListItem>
                            <asp:ListItem>Dorado</asp:ListItem>
                            <asp:ListItem>Fajardo</asp:ListItem>
                            <asp:ListItem>Florida</asp:ListItem>
                            <asp:ListItem>Guánica</asp:ListItem>
                            <asp:ListItem>Guayama</asp:ListItem>
                            <asp:ListItem>Guayanilla</asp:ListItem>
                            <asp:ListItem>Guaynabo</asp:ListItem>
                            <asp:ListItem>Gurabo</asp:ListItem>
                            <asp:ListItem>Hatillo</asp:ListItem>
                            <asp:ListItem>Hormigueros</asp:ListItem>
                            <asp:ListItem>Humacao</asp:ListItem>
                            <asp:ListItem>Isabela</asp:ListItem>
                            <asp:ListItem>Jayuya</asp:ListItem>
                            <asp:ListItem>Juana Díaz</asp:ListItem>
                            <asp:ListItem>Juncos</asp:ListItem>
                            <asp:ListItem>Lajas</asp:ListItem>
                            <asp:ListItem>Lares</asp:ListItem>
                            <asp:ListItem>Las Marías</asp:ListItem>
                            <asp:ListItem>Las Piedras</asp:ListItem>
                            <asp:ListItem>Loíza</asp:ListItem>
                            <asp:ListItem>Luquillo</asp:ListItem>
                            <asp:ListItem>Manatí</asp:ListItem>
                            <asp:ListItem>Maricao</asp:ListItem>
                            <asp:ListItem>Maunabo</asp:ListItem>
                            <asp:ListItem>Mayagüez</asp:ListItem>
                            <asp:ListItem>Moca</asp:ListItem>
                            <asp:ListItem>Morovis</asp:ListItem>
                            <asp:ListItem>Naguabo</asp:ListItem>
                            <asp:ListItem>Naranjito</asp:ListItem>
                            <asp:ListItem>Orocovis</asp:ListItem>
                            <asp:ListItem>Patillas</asp:ListItem>
                            <asp:ListItem>Peñuelas</asp:ListItem>
                            <asp:ListItem>Ponce</asp:ListItem>
                            <asp:ListItem>Quebradillas</asp:ListItem>
                            <asp:ListItem>Rincón</asp:ListItem>
                            <asp:ListItem>Río Grande</asp:ListItem>
                            <asp:ListItem>Sabana Grande</asp:ListItem>
                            <asp:ListItem>Salinas</asp:ListItem>
                            <asp:ListItem>San Germán</asp:ListItem>
                            <asp:ListItem>San Juan</asp:ListItem>
                            <asp:ListItem>San Lorenzo</asp:ListItem>
                            <asp:ListItem>San Sebastián</asp:ListItem>
                            <asp:ListItem>Santa Isabel</asp:ListItem>
                            <asp:ListItem>Toa Alta</asp:ListItem>
                            <asp:ListItem>Toa Baja</asp:ListItem>
                            <asp:ListItem>Trujillo Alto</asp:ListItem>
                            <asp:ListItem>Utuado</asp:ListItem>
                            <asp:ListItem>Vega Alta</asp:ListItem>
                            <asp:ListItem>Vega Baja</asp:ListItem>
                            <asp:ListItem>Vieques (Isla municipio)</asp:ListItem>
                            <asp:ListItem>Villalba</asp:ListItem>
                            <asp:ListItem>Yabucoa</asp:ListItem>
                            <asp:ListItem>Yauco</asp:ListItem>

                        </asp:DropDownList>

                    </div>

                </div>

            </td>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div style=" ">

                        <asp:Label runat="server" Text="C&oacute;digo Postal*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server"   ID="codigoPostalPosTxtBx"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="codigoPostalPosTxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

        </tr>

    </table>

     <%-- Ref --%>

    <div style="background-color: #616161; padding-left: 7%; margin-top:40px">

        <asp:Label ForeColor="#E5E5E5" Font-Size="X-Large" Font-Bold="true" runat="server">Referencias</asp:Label>

    </div>

    <table class ="table" style="color: #79256E; margin-top: 20px;">

        <%-- Ref --%>

        <tr>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div>

                        <asp:Label runat="server" Text="Nombre del Pariente m&aacute;s" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:Label runat="server" Text="cercano que no viva con usted*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server"   ID="cercanoTxt"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="cercanoTxt" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div>

                        <asp:Label runat="server" Text="Parentesco*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:DropDownList runat ="server" ID ="parentescoRefDDL">

                            <asp:ListItem>Padre</asp:ListItem>

                            <asp:ListItem>Madre</asp:ListItem>

                            <asp:ListItem>Hijo(a)</asp:ListItem>

                            <asp:ListItem>Abuelo(a)</asp:ListItem>

                            <asp:ListItem>Tío(a)</asp:ListItem>

                            <asp:ListItem>Vecino(a)</asp:ListItem>

                            <asp:ListItem>Amigo(a)</asp:ListItem>

                            <asp:ListItem>Supervisor(a)</asp:ListItem>

                        </asp:DropDownList>

                        <%--<asp:TextBox runat="server"   ID="parentescoRefTxtBx"></asp:TextBox>--%>

                    </div>

                    <%--<div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="parentescoRefTxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>--%>

                </div>

            </td>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div>

                        <asp:Label runat="server" Text="Tel&eacute;fono Familiar*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <script>

                            $(function ()
                            {
                                $("#<% = telFamTxtBx.ClientID%>").mask("999-999-9999");
                            });

                        </script>

                        <asp:TextBox runat="server"   ID="telFamTxtBx"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="telFamTxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

        </tr>

        <tr>

            <td style ="border-top-style: none; vertical-align:bottom">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div>

                        <asp:Label runat="server" Text="Direcci&oacute;n Residencial*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server" TextMode ="MultiLine"  ID="dirRefTxtBx"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="dirRefTxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

            <td style ="border-top-style: none; vertical-align:bottom">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div>

                        <asp:Label runat="server" Text="Pueblo*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:DropDownList runat ="server" ID ="puebloRefDDL">

                            <asp:ListItem>Adjuntas</asp:ListItem>
                            <asp:ListItem>Aguada</asp:ListItem>
                            <asp:ListItem>Aguadilla</asp:ListItem>
                            <asp:ListItem>Aguas Buenas</asp:ListItem>
                            <asp:ListItem>Aibonito</asp:ListItem>
                            <asp:ListItem>Añasco</asp:ListItem>
                            <asp:ListItem>Arecibo</asp:ListItem>
                            <asp:ListItem>Arroyo</asp:ListItem>
                            <asp:ListItem>Barceloneta</asp:ListItem>
                            <asp:ListItem>Barranquitas</asp:ListItem>
                            <asp:ListItem>Bayamón</asp:ListItem>
                            <asp:ListItem>Cabo Rojo</asp:ListItem>
                            <asp:ListItem>Caguas</asp:ListItem>
                            <asp:ListItem>Camuy</asp:ListItem>
                            <asp:ListItem>Canóvanas</asp:ListItem>
                            <asp:ListItem>Carolina</asp:ListItem>
                            <asp:ListItem>Cataño</asp:ListItem>
                            <asp:ListItem>Cayey</asp:ListItem>
                            <asp:ListItem>Ceiba</asp:ListItem>
                            <asp:ListItem>Ciales</asp:ListItem>
                            <asp:ListItem>Cidra</asp:ListItem>
                            <asp:ListItem>Coamo</asp:ListItem>
                            <asp:ListItem>Comerío</asp:ListItem>
                            <asp:ListItem>Corozal</asp:ListItem>
                            <asp:ListItem>Culebra (Isla municipio)</asp:ListItem>
                            <asp:ListItem>Dorado</asp:ListItem>
                            <asp:ListItem>Fajardo</asp:ListItem>
                            <asp:ListItem>Florida</asp:ListItem>
                            <asp:ListItem>Guánica</asp:ListItem>
                            <asp:ListItem>Guayama</asp:ListItem>
                            <asp:ListItem>Guayanilla</asp:ListItem>
                            <asp:ListItem>Guaynabo</asp:ListItem>
                            <asp:ListItem>Gurabo</asp:ListItem>
                            <asp:ListItem>Hatillo</asp:ListItem>
                            <asp:ListItem>Hormigueros</asp:ListItem>
                            <asp:ListItem>Humacao</asp:ListItem>
                            <asp:ListItem>Isabela</asp:ListItem>
                            <asp:ListItem>Jayuya</asp:ListItem>
                            <asp:ListItem>Juana Díaz</asp:ListItem>
                            <asp:ListItem>Juncos</asp:ListItem>
                            <asp:ListItem>Lajas</asp:ListItem>
                            <asp:ListItem>Lares</asp:ListItem>
                            <asp:ListItem>Las Marías</asp:ListItem>
                            <asp:ListItem>Las Piedras</asp:ListItem>
                            <asp:ListItem>Loíza</asp:ListItem>
                            <asp:ListItem>Luquillo</asp:ListItem>
                            <asp:ListItem>Manatí</asp:ListItem>
                            <asp:ListItem>Maricao</asp:ListItem>
                            <asp:ListItem>Maunabo</asp:ListItem>
                            <asp:ListItem>Mayagüez</asp:ListItem>
                            <asp:ListItem>Moca</asp:ListItem>
                            <asp:ListItem>Morovis</asp:ListItem>
                            <asp:ListItem>Naguabo</asp:ListItem>
                            <asp:ListItem>Naranjito</asp:ListItem>
                            <asp:ListItem>Orocovis</asp:ListItem>
                            <asp:ListItem>Patillas</asp:ListItem>
                            <asp:ListItem>Peñuelas</asp:ListItem>
                            <asp:ListItem>Ponce</asp:ListItem>
                            <asp:ListItem>Quebradillas</asp:ListItem>
                            <asp:ListItem>Rincón</asp:ListItem>
                            <asp:ListItem>Río Grande</asp:ListItem>
                            <asp:ListItem>Sabana Grande</asp:ListItem>
                            <asp:ListItem>Salinas</asp:ListItem>
                            <asp:ListItem>San Germán</asp:ListItem>
                            <asp:ListItem>San Juan</asp:ListItem>
                            <asp:ListItem>San Lorenzo</asp:ListItem>
                            <asp:ListItem>San Sebastián</asp:ListItem>
                            <asp:ListItem>Santa Isabel</asp:ListItem>
                            <asp:ListItem>Toa Alta</asp:ListItem>
                            <asp:ListItem>Toa Baja</asp:ListItem>
                            <asp:ListItem>Trujillo Alto</asp:ListItem>
                            <asp:ListItem>Utuado</asp:ListItem>
                            <asp:ListItem>Vega Alta</asp:ListItem>
                            <asp:ListItem>Vega Baja</asp:ListItem>
                            <asp:ListItem>Vieques (Isla municipio)</asp:ListItem>
                            <asp:ListItem>Villalba</asp:ListItem>
                            <asp:ListItem>Yabucoa</asp:ListItem>
                            <asp:ListItem>Yauco</asp:ListItem>

                        </asp:DropDownList>

                    </div>

                </div>

            </td>

            <td style ="border-top-style: none; vertical-align:bottom">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div>

                        <asp:Label runat="server" Text="C&oacute;digo Postal*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server"   ID="codigoRefTxtBx"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="codigoRefTxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

        </tr>

       <%-- Ref 2 --%>

        <tr>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div>

                        <asp:Label runat="server" Text="Nombre del Pariente m&aacute;s" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:Label runat="server" Text="cercano que no viva con usted*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server"   ID="cercano2TxtBx"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="cercano2TxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div>

                        <asp:Label runat="server" Text="Parentesco*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:DropDownList runat ="server" ID ="parentescoDDL">

                            <asp:ListItem>Padre</asp:ListItem>

                            <asp:ListItem>Madre</asp:ListItem>

                            <asp:ListItem>Hijo(a)</asp:ListItem>

                            <asp:ListItem>Abuelo(a)</asp:ListItem>

                            <asp:ListItem>Tío(a)</asp:ListItem>

                            <asp:ListItem>Vecino(a)</asp:ListItem>

                            <asp:ListItem>Amigo(a)</asp:ListItem>

                            <asp:ListItem>Supervisor(a)</asp:ListItem>

                        </asp:DropDownList>

                        <%--<asp:TextBox runat="server"   ID="parentescoTxtBx"></asp:TextBox>--%>

                    </div>

                    <%--<div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="parentescoTxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>--%>

                </div>

            </td>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div>

                        <asp:Label runat="server" Text="Tel&eacute;fono Familiar*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <script>

                            $(function ()
                            {
                                $("#<% = telFam2TxtBx.ClientID%>").mask("999-999-9999");
                            });

                        </script>

                        <asp:TextBox runat="server"   ID="telFam2TxtBx"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="telFam2TxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

        </tr>

        <tr>

            <td style ="border-top-style: none; vertical-align:bottom">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div>

                        <asp:Label runat="server" Text="Direcci&oacute;n Residencial*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server" TextMode ="MultiLine" ID="dirRef2TxtBx"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="dirRef2TxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

            <td style ="border-top-style: none; vertical-align:bottom">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div>

                        <asp:Label runat="server" Text="Pueblo*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:DropDownList runat ="server" ID ="puebloRef2DDL">

                            <asp:ListItem>Adjuntas</asp:ListItem>
                            <asp:ListItem>Aguada</asp:ListItem>
                            <asp:ListItem>Aguadilla</asp:ListItem>
                            <asp:ListItem>Aguas Buenas</asp:ListItem>
                            <asp:ListItem>Aibonito</asp:ListItem>
                            <asp:ListItem>Añasco</asp:ListItem>
                            <asp:ListItem>Arecibo</asp:ListItem>
                            <asp:ListItem>Arroyo</asp:ListItem>
                            <asp:ListItem>Barceloneta</asp:ListItem>
                            <asp:ListItem>Barranquitas</asp:ListItem>
                            <asp:ListItem>Bayamón</asp:ListItem>
                            <asp:ListItem>Cabo Rojo</asp:ListItem>
                            <asp:ListItem>Caguas</asp:ListItem>
                            <asp:ListItem>Camuy</asp:ListItem>
                            <asp:ListItem>Canóvanas</asp:ListItem>
                            <asp:ListItem>Carolina</asp:ListItem>
                            <asp:ListItem>Cataño</asp:ListItem>
                            <asp:ListItem>Cayey</asp:ListItem>
                            <asp:ListItem>Ceiba</asp:ListItem>
                            <asp:ListItem>Ciales</asp:ListItem>
                            <asp:ListItem>Cidra</asp:ListItem>
                            <asp:ListItem>Coamo</asp:ListItem>
                            <asp:ListItem>Comerío</asp:ListItem>
                            <asp:ListItem>Corozal</asp:ListItem>
                            <asp:ListItem>Culebra (Isla municipio)</asp:ListItem>
                            <asp:ListItem>Dorado</asp:ListItem>
                            <asp:ListItem>Fajardo</asp:ListItem>
                            <asp:ListItem>Florida</asp:ListItem>
                            <asp:ListItem>Guánica</asp:ListItem>
                            <asp:ListItem>Guayama</asp:ListItem>
                            <asp:ListItem>Guayanilla</asp:ListItem>
                            <asp:ListItem>Guaynabo</asp:ListItem>
                            <asp:ListItem>Gurabo</asp:ListItem>
                            <asp:ListItem>Hatillo</asp:ListItem>
                            <asp:ListItem>Hormigueros</asp:ListItem>
                            <asp:ListItem>Humacao</asp:ListItem>
                            <asp:ListItem>Isabela</asp:ListItem>
                            <asp:ListItem>Jayuya</asp:ListItem>
                            <asp:ListItem>Juana Díaz</asp:ListItem>
                            <asp:ListItem>Juncos</asp:ListItem>
                            <asp:ListItem>Lajas</asp:ListItem>
                            <asp:ListItem>Lares</asp:ListItem>
                            <asp:ListItem>Las Marías</asp:ListItem>
                            <asp:ListItem>Las Piedras</asp:ListItem>
                            <asp:ListItem>Loíza</asp:ListItem>
                            <asp:ListItem>Luquillo</asp:ListItem>
                            <asp:ListItem>Manatí</asp:ListItem>
                            <asp:ListItem>Maricao</asp:ListItem>
                            <asp:ListItem>Maunabo</asp:ListItem>
                            <asp:ListItem>Mayagüez</asp:ListItem>
                            <asp:ListItem>Moca</asp:ListItem>
                            <asp:ListItem>Morovis</asp:ListItem>
                            <asp:ListItem>Naguabo</asp:ListItem>
                            <asp:ListItem>Naranjito</asp:ListItem>
                            <asp:ListItem>Orocovis</asp:ListItem>
                            <asp:ListItem>Patillas</asp:ListItem>
                            <asp:ListItem>Peñuelas</asp:ListItem>
                            <asp:ListItem>Ponce</asp:ListItem>
                            <asp:ListItem>Quebradillas</asp:ListItem>
                            <asp:ListItem>Rincón</asp:ListItem>
                            <asp:ListItem>Río Grande</asp:ListItem>
                            <asp:ListItem>Sabana Grande</asp:ListItem>
                            <asp:ListItem>Salinas</asp:ListItem>
                            <asp:ListItem>San Germán</asp:ListItem>
                            <asp:ListItem>San Juan</asp:ListItem>
                            <asp:ListItem>San Lorenzo</asp:ListItem>
                            <asp:ListItem>San Sebastián</asp:ListItem>
                            <asp:ListItem>Santa Isabel</asp:ListItem>
                            <asp:ListItem>Toa Alta</asp:ListItem>
                            <asp:ListItem>Toa Baja</asp:ListItem>
                            <asp:ListItem>Trujillo Alto</asp:ListItem>
                            <asp:ListItem>Utuado</asp:ListItem>
                            <asp:ListItem>Vega Alta</asp:ListItem>
                            <asp:ListItem>Vega Baja</asp:ListItem>
                            <asp:ListItem>Vieques (Isla municipio)</asp:ListItem>
                            <asp:ListItem>Villalba</asp:ListItem>
                            <asp:ListItem>Yabucoa</asp:ListItem>
                            <asp:ListItem>Yauco</asp:ListItem>

                        </asp:DropDownList>

                    </div>

                </div>

            </td>

            <td style ="border-top-style: none; vertical-align:bottom">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div>

                        <asp:Label runat="server" Text="C&oacute;digo Postal*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server"   ID="codigoRef2TxtBx"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="codigoRef2TxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

        </tr>

        <%-- Ref 3 --%>

        <tr>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div>

                        <asp:Label runat="server" Text="Nombre del Pariente m&aacute;s" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:Label runat="server" Text="cercano que no viva con usted*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server"   ID="cercanoRef3TxtBx"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="cercanoRef3TxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div>

                        <asp:Label runat="server" Text="Parentesco*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:DropDownList runat ="server" ID ="parentescoRef3DDL">

                            <asp:ListItem>Padre</asp:ListItem>

                            <asp:ListItem>Madre</asp:ListItem>

                            <asp:ListItem>Hijo(a)</asp:ListItem>

                            <asp:ListItem>Abuelo(a)</asp:ListItem>

                            <asp:ListItem>Tío(a)</asp:ListItem>

                            <asp:ListItem>Vecino(a)</asp:ListItem>

                            <asp:ListItem>Amigo(a)</asp:ListItem>

                            <asp:ListItem>Supervisor(a)</asp:ListItem>

                        </asp:DropDownList>

                        <%--<asp:TextBox runat="server"   ID="parentescoRef3TxtBx"></asp:TextBox>--%>

                    </div>

                    <%--<div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="parentescoRef3TxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>--%>

                </div>

            </td>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div>

                        <asp:Label runat="server" Text="Tel&eacute;fono Familiar*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <script>

                            $(function ()
                            {
                                $("#<% = telFamRef3TxtBx.ClientID%>").mask("999-999-9999");
                            });

                        </script>

                        <asp:TextBox runat="server"   ID="telFamRef3TxtBx"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="telFamRef3TxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

        </tr>

        <tr>

            <td style ="border-top-style: none; vertical-align:bottom">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div>

                        <asp:Label runat="server" Text="Direcci&oacute;n Residencial*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server" TextMode ="MultiLine" ID="dirRef3TxtBx"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="dirRef3TxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

            <td style ="border-top-style: none; vertical-align:bottom">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div>

                        <asp:Label runat="server" Text="Pueblo*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:DropDownList runat ="server" ID ="puebloRef3DDL">

                            <asp:ListItem>Adjuntas</asp:ListItem>
                            <asp:ListItem>Aguada</asp:ListItem>
                            <asp:ListItem>Aguadilla</asp:ListItem>
                            <asp:ListItem>Aguas Buenas</asp:ListItem>
                            <asp:ListItem>Aibonito</asp:ListItem>
                            <asp:ListItem>Añasco</asp:ListItem>
                            <asp:ListItem>Arecibo</asp:ListItem>
                            <asp:ListItem>Arroyo</asp:ListItem>
                            <asp:ListItem>Barceloneta</asp:ListItem>
                            <asp:ListItem>Barranquitas</asp:ListItem>
                            <asp:ListItem>Bayamón</asp:ListItem>
                            <asp:ListItem>Cabo Rojo</asp:ListItem>
                            <asp:ListItem>Caguas</asp:ListItem>
                            <asp:ListItem>Camuy</asp:ListItem>
                            <asp:ListItem>Canóvanas</asp:ListItem>
                            <asp:ListItem>Carolina</asp:ListItem>
                            <asp:ListItem>Cataño</asp:ListItem>
                            <asp:ListItem>Cayey</asp:ListItem>
                            <asp:ListItem>Ceiba</asp:ListItem>
                            <asp:ListItem>Ciales</asp:ListItem>
                            <asp:ListItem>Cidra</asp:ListItem>
                            <asp:ListItem>Coamo</asp:ListItem>
                            <asp:ListItem>Comerío</asp:ListItem>
                            <asp:ListItem>Corozal</asp:ListItem>
                            <asp:ListItem>Culebra (Isla municipio)</asp:ListItem>
                            <asp:ListItem>Dorado</asp:ListItem>
                            <asp:ListItem>Fajardo</asp:ListItem>
                            <asp:ListItem>Florida</asp:ListItem>
                            <asp:ListItem>Guánica</asp:ListItem>
                            <asp:ListItem>Guayama</asp:ListItem>
                            <asp:ListItem>Guayanilla</asp:ListItem>
                            <asp:ListItem>Guaynabo</asp:ListItem>
                            <asp:ListItem>Gurabo</asp:ListItem>
                            <asp:ListItem>Hatillo</asp:ListItem>
                            <asp:ListItem>Hormigueros</asp:ListItem>
                            <asp:ListItem>Humacao</asp:ListItem>
                            <asp:ListItem>Isabela</asp:ListItem>
                            <asp:ListItem>Jayuya</asp:ListItem>
                            <asp:ListItem>Juana Díaz</asp:ListItem>
                            <asp:ListItem>Juncos</asp:ListItem>
                            <asp:ListItem>Lajas</asp:ListItem>
                            <asp:ListItem>Lares</asp:ListItem>
                            <asp:ListItem>Las Marías</asp:ListItem>
                            <asp:ListItem>Las Piedras</asp:ListItem>
                            <asp:ListItem>Loíza</asp:ListItem>
                            <asp:ListItem>Luquillo</asp:ListItem>
                            <asp:ListItem>Manatí</asp:ListItem>
                            <asp:ListItem>Maricao</asp:ListItem>
                            <asp:ListItem>Maunabo</asp:ListItem>
                            <asp:ListItem>Mayagüez</asp:ListItem>
                            <asp:ListItem>Moca</asp:ListItem>
                            <asp:ListItem>Morovis</asp:ListItem>
                            <asp:ListItem>Naguabo</asp:ListItem>
                            <asp:ListItem>Naranjito</asp:ListItem>
                            <asp:ListItem>Orocovis</asp:ListItem>
                            <asp:ListItem>Patillas</asp:ListItem>
                            <asp:ListItem>Peñuelas</asp:ListItem>
                            <asp:ListItem>Ponce</asp:ListItem>
                            <asp:ListItem>Quebradillas</asp:ListItem>
                            <asp:ListItem>Rincón</asp:ListItem>
                            <asp:ListItem>Río Grande</asp:ListItem>
                            <asp:ListItem>Sabana Grande</asp:ListItem>
                            <asp:ListItem>Salinas</asp:ListItem>
                            <asp:ListItem>San Germán</asp:ListItem>
                            <asp:ListItem>San Juan</asp:ListItem>
                            <asp:ListItem>San Lorenzo</asp:ListItem>
                            <asp:ListItem>San Sebastián</asp:ListItem>
                            <asp:ListItem>Santa Isabel</asp:ListItem>
                            <asp:ListItem>Toa Alta</asp:ListItem>
                            <asp:ListItem>Toa Baja</asp:ListItem>
                            <asp:ListItem>Trujillo Alto</asp:ListItem>
                            <asp:ListItem>Utuado</asp:ListItem>
                            <asp:ListItem>Vega Alta</asp:ListItem>
                            <asp:ListItem>Vega Baja</asp:ListItem>
                            <asp:ListItem>Vieques (Isla municipio)</asp:ListItem>
                            <asp:ListItem>Villalba</asp:ListItem>
                            <asp:ListItem>Yabucoa</asp:ListItem>
                            <asp:ListItem>Yauco</asp:ListItem>

                        </asp:DropDownList>

                    </div>

                </div>

            </td>

            <td style ="border-top-style: none; vertical-align:bottom">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div>

                        <asp:Label runat="server" Text="C&oacute;digo Postal*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server"   ID="codigoRef3TxtBx"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="codigoRef3TxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

        </tr>

    </table>

    <%-- Info Co Solicitante --%>

    <div style="background-color: #616161; padding-left: 7%; margin-top:40px">

        <asp:Label ForeColor="#E5E5E5" Font-Size="X-Large" Font-Bold="true" runat="server">Informaci&oacute;n del Co-Solicitante (Opcional)</asp:Label>

    </div>

    <table class ="table" style="color: #79256E; margin-top: 20px;">

        <tr>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div style=" ">

                        <asp:Label runat="server" Text="Nombre" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server"   ID="nombreCoTxtBx"></asp:TextBox>

                    </div>

                </div>

            </td>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div style=" ">

                        <asp:Label runat="server" Text="Apellido Paterno" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server"   ID="paternoCoTxtBx"></asp:TextBox>

                    </div>

                </div>

            </td>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div style=" ">

                        <asp:Label runat="server" Text="Apellido Materno" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server"   ID="maternoCoTxtBx"></asp:TextBox>

                    </div>

                </div>

            </td>

        </tr>

        <tr>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div style=" ">

                        <asp:Label runat="server" Text="Seguro Social" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <%--<script>

                            $(function ()
                            {
                                $("#<% = ssCoTxtBx.ClientID%>").mask("999-99-9999");
                            });

                        </script>--%>

                        <asp:HiddenField ID ="ssEncryptedPartCoHF" runat ="server" />

                        <asp:TextBox runat="server" onkeyup ="EncryptSSNCo(this, event);"
                             MaxLength ="11" ID="ssCoTxtBx" AutoCompleteType ="Disabled"></asp:TextBox>

                    </div>

                </div>

            </td>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div style=" ">

                        <asp:Label runat="server" Text="Correo Electr&oacute;nico" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server"   ID="emailCoTxtBx"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RegularExpressionValidator runat ="server" ControlToValidate ="emailCoTxtBx" Display ="Dynamic" SetFocusOnError ="true" 
                            ErrorMessage ="Incorrecto"
                             ValidationExpression ="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor ="#CC0000">

                        </asp:RegularExpressionValidator>

                    </div>

                </div>

            </td>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div style=" ">

                        <script>

                            $(
                                function () {
                                    $("#<% = bdayCoTxtBx.ClientID%>").datepicker();
                                }
                    );

                        </script>

                        <asp:Label runat="server" Text="Fecha Nacimiento" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server"   ID="bdayCoTxtBx"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RegularExpressionValidator runat ="server" ControlToValidate ="bdayCoTxtBx" Display ="Dynamic" SetFocusOnError ="true" 
                            ErrorMessage ="Incorrecto. Formato valido es MM/DD/YYYY"
                             ValidationExpression ="^((0?[13578]|10|12)(-|\/)(([1-9])|(0[1-9])|([12])([0-9]?)
                            |(3[01]?))(-|\/)((19)([2-9])(\d{1})|(20)([01])(\d{1})|
                            ([8901])(\d{1}))|(0?[2469]|11)(-|\/)(([1-9])|(0[1-9])|([12])([0-9]?)|(3[0]?))(-|\/)((19)([2-9])(\d{1})
                            |(20)([01])(\d{1})|([8901])(\d{1})))$" ForeColor ="#CC0000">

                        </asp:RegularExpressionValidator>

                    </div>

                </div>

            </td>

        </tr>

        <tr>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div style=" ">

                        <asp:Label runat="server" Text="Licencia Conducir" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>
                        <asp:HiddenField ID ="decryptedDriversCoHF" runat ="server" />

                        <asp:TextBox runat="server" MaxLength ="7" onkeyup ="EncryptDriversCo(this, event);"
                             ID="driversCoTxtBx" AutoCompleteType ="Disabled"></asp:TextBox>

                    </div>

                </div>

            </td>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div style=" ">

                        <asp:Label runat="server" Text="Tel&eacute;fono Celular" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <script>

                            $(function ()
                            {
                                $("#<% = celCoTextBx.ClientID%>").mask("999-999-9999");
                            });

                        </script>

                        <asp:TextBox runat="server"   ID="celCoTextBx"></asp:TextBox>

                    </div>

                </div>

            </td>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div style=" ">

                        <asp:Label runat="server" Text="Tel&eacute;fono Residencial" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <script>

                            $(function ()
                            {
                                $("#<% = telCoTxtBx.ClientID%>").mask("999-999-9999");
                            });

                        </script>

                        <asp:TextBox runat="server"   ID="telCoTxtBx"></asp:TextBox>

                    </div>

                </div>

            </td>

        </tr>

        <tr>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div style=" ">

                        <asp:Label runat="server" Text="Direcci&oacute;n Residencial" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server" TextMode ="MultiLine" ID="dirCoTxtBx"></asp:TextBox>

                    </div>

                </div>

            </td>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div style=" ">

                        <asp:Label runat="server" Text="Pueblo" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:DropDownList ID ="puebloCoDDL" runat ="server">

                            <asp:ListItem>Adjuntas</asp:ListItem>
                            <asp:ListItem>Aguada</asp:ListItem>
                            <asp:ListItem>Aguadilla</asp:ListItem>
                            <asp:ListItem>Aguas Buenas</asp:ListItem>
                            <asp:ListItem>Aibonito</asp:ListItem>
                            <asp:ListItem>Añasco</asp:ListItem>
                            <asp:ListItem>Arecibo</asp:ListItem>
                            <asp:ListItem>Arroyo</asp:ListItem>
                            <asp:ListItem>Barceloneta</asp:ListItem>
                            <asp:ListItem>Barranquitas</asp:ListItem>
                            <asp:ListItem>Bayamón</asp:ListItem>
                            <asp:ListItem>Cabo Rojo</asp:ListItem>
                            <asp:ListItem>Caguas</asp:ListItem>
                            <asp:ListItem>Camuy</asp:ListItem>
                            <asp:ListItem>Canóvanas</asp:ListItem>
                            <asp:ListItem>Carolina</asp:ListItem>
                            <asp:ListItem>Cataño</asp:ListItem>
                            <asp:ListItem>Cayey</asp:ListItem>
                            <asp:ListItem>Ceiba</asp:ListItem>
                            <asp:ListItem>Ciales</asp:ListItem>
                            <asp:ListItem>Cidra</asp:ListItem>
                            <asp:ListItem>Coamo</asp:ListItem>
                            <asp:ListItem>Comerío</asp:ListItem>
                            <asp:ListItem>Corozal</asp:ListItem>
                            <asp:ListItem>Culebra (Isla municipio)</asp:ListItem>
                            <asp:ListItem>Dorado</asp:ListItem>
                            <asp:ListItem>Fajardo</asp:ListItem>
                            <asp:ListItem>Florida</asp:ListItem>
                            <asp:ListItem>Guánica</asp:ListItem>
                            <asp:ListItem>Guayama</asp:ListItem>
                            <asp:ListItem>Guayanilla</asp:ListItem>
                            <asp:ListItem>Guaynabo</asp:ListItem>
                            <asp:ListItem>Gurabo</asp:ListItem>
                            <asp:ListItem>Hatillo</asp:ListItem>
                            <asp:ListItem>Hormigueros</asp:ListItem>
                            <asp:ListItem>Humacao</asp:ListItem>
                            <asp:ListItem>Isabela</asp:ListItem>
                            <asp:ListItem>Jayuya</asp:ListItem>
                            <asp:ListItem>Juana Díaz</asp:ListItem>
                            <asp:ListItem>Juncos</asp:ListItem>
                            <asp:ListItem>Lajas</asp:ListItem>
                            <asp:ListItem>Lares</asp:ListItem>
                            <asp:ListItem>Las Marías</asp:ListItem>
                            <asp:ListItem>Las Piedras</asp:ListItem>
                            <asp:ListItem>Loíza</asp:ListItem>
                            <asp:ListItem>Luquillo</asp:ListItem>
                            <asp:ListItem>Manatí</asp:ListItem>
                            <asp:ListItem>Maricao</asp:ListItem>
                            <asp:ListItem>Maunabo</asp:ListItem>
                            <asp:ListItem>Mayagüez</asp:ListItem>
                            <asp:ListItem>Moca</asp:ListItem>
                            <asp:ListItem>Morovis</asp:ListItem>
                            <asp:ListItem>Naguabo</asp:ListItem>
                            <asp:ListItem>Naranjito</asp:ListItem>
                            <asp:ListItem>Orocovis</asp:ListItem>
                            <asp:ListItem>Patillas</asp:ListItem>
                            <asp:ListItem>Peñuelas</asp:ListItem>
                            <asp:ListItem>Ponce</asp:ListItem>
                            <asp:ListItem>Quebradillas</asp:ListItem>
                            <asp:ListItem>Rincón</asp:ListItem>
                            <asp:ListItem>Río Grande</asp:ListItem>
                            <asp:ListItem>Sabana Grande</asp:ListItem>
                            <asp:ListItem>Salinas</asp:ListItem>
                            <asp:ListItem>San Germán</asp:ListItem>
                            <asp:ListItem>San Juan</asp:ListItem>
                            <asp:ListItem>San Lorenzo</asp:ListItem>
                            <asp:ListItem>San Sebastián</asp:ListItem>
                            <asp:ListItem>Santa Isabel</asp:ListItem>
                            <asp:ListItem>Toa Alta</asp:ListItem>
                            <asp:ListItem>Toa Baja</asp:ListItem>
                            <asp:ListItem>Trujillo Alto</asp:ListItem>
                            <asp:ListItem>Utuado</asp:ListItem>
                            <asp:ListItem>Vega Alta</asp:ListItem>
                            <asp:ListItem>Vega Baja</asp:ListItem>
                            <asp:ListItem>Vieques (Isla municipio)</asp:ListItem>
                            <asp:ListItem>Villalba</asp:ListItem>
                            <asp:ListItem>Yabucoa</asp:ListItem>
                            <asp:ListItem>Yauco</asp:ListItem>

                        </asp:DropDownList>

                    </div>

                </div>

            </td>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div style=" ">

                        <asp:Label runat="server" Text="C&oacute;digo Postal" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server"   ID="codigoCoTxtBx"></asp:TextBox>

                    </div>

                </div>

            </td>

        </tr>

        <tr>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div style=" ">

                        <asp:Label runat="server" Text="Direcci&oacute;n Postal" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server" TextMode ="MultiLine" ID="dirCoPostalTxtbx"></asp:TextBox>

                    </div>

                </div>

            </td>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div style=" ">

                        <asp:Label runat="server" Text="Pueblo" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:DropDownList ID ="puebloCoPostalDDL" runat ="server">

                            <asp:ListItem>Adjuntas</asp:ListItem>
                            <asp:ListItem>Aguada</asp:ListItem>
                            <asp:ListItem>Aguadilla</asp:ListItem>
                            <asp:ListItem>Aguas Buenas</asp:ListItem>
                            <asp:ListItem>Aibonito</asp:ListItem>
                            <asp:ListItem>Añasco</asp:ListItem>
                            <asp:ListItem>Arecibo</asp:ListItem>
                            <asp:ListItem>Arroyo</asp:ListItem>
                            <asp:ListItem>Barceloneta</asp:ListItem>
                            <asp:ListItem>Barranquitas</asp:ListItem>
                            <asp:ListItem>Bayamón</asp:ListItem>
                            <asp:ListItem>Cabo Rojo</asp:ListItem>
                            <asp:ListItem>Caguas</asp:ListItem>
                            <asp:ListItem>Camuy</asp:ListItem>
                            <asp:ListItem>Canóvanas</asp:ListItem>
                            <asp:ListItem>Carolina</asp:ListItem>
                            <asp:ListItem>Cataño</asp:ListItem>
                            <asp:ListItem>Cayey</asp:ListItem>
                            <asp:ListItem>Ceiba</asp:ListItem>
                            <asp:ListItem>Ciales</asp:ListItem>
                            <asp:ListItem>Cidra</asp:ListItem>
                            <asp:ListItem>Coamo</asp:ListItem>
                            <asp:ListItem>Comerío</asp:ListItem>
                            <asp:ListItem>Corozal</asp:ListItem>
                            <asp:ListItem>Culebra (Isla municipio)</asp:ListItem>
                            <asp:ListItem>Dorado</asp:ListItem>
                            <asp:ListItem>Fajardo</asp:ListItem>
                            <asp:ListItem>Florida</asp:ListItem>
                            <asp:ListItem>Guánica</asp:ListItem>
                            <asp:ListItem>Guayama</asp:ListItem>
                            <asp:ListItem>Guayanilla</asp:ListItem>
                            <asp:ListItem>Guaynabo</asp:ListItem>
                            <asp:ListItem>Gurabo</asp:ListItem>
                            <asp:ListItem>Hatillo</asp:ListItem>
                            <asp:ListItem>Hormigueros</asp:ListItem>
                            <asp:ListItem>Humacao</asp:ListItem>
                            <asp:ListItem>Isabela</asp:ListItem>
                            <asp:ListItem>Jayuya</asp:ListItem>
                            <asp:ListItem>Juana Díaz</asp:ListItem>
                            <asp:ListItem>Juncos</asp:ListItem>
                            <asp:ListItem>Lajas</asp:ListItem>
                            <asp:ListItem>Lares</asp:ListItem>
                            <asp:ListItem>Las Marías</asp:ListItem>
                            <asp:ListItem>Las Piedras</asp:ListItem>
                            <asp:ListItem>Loíza</asp:ListItem>
                            <asp:ListItem>Luquillo</asp:ListItem>
                            <asp:ListItem>Manatí</asp:ListItem>
                            <asp:ListItem>Maricao</asp:ListItem>
                            <asp:ListItem>Maunabo</asp:ListItem>
                            <asp:ListItem>Mayagüez</asp:ListItem>
                            <asp:ListItem>Moca</asp:ListItem>
                            <asp:ListItem>Morovis</asp:ListItem>
                            <asp:ListItem>Naguabo</asp:ListItem>
                            <asp:ListItem>Naranjito</asp:ListItem>
                            <asp:ListItem>Orocovis</asp:ListItem>
                            <asp:ListItem>Patillas</asp:ListItem>
                            <asp:ListItem>Peñuelas</asp:ListItem>
                            <asp:ListItem>Ponce</asp:ListItem>
                            <asp:ListItem>Quebradillas</asp:ListItem>
                            <asp:ListItem>Rincón</asp:ListItem>
                            <asp:ListItem>Río Grande</asp:ListItem>
                            <asp:ListItem>Sabana Grande</asp:ListItem>
                            <asp:ListItem>Salinas</asp:ListItem>
                            <asp:ListItem>San Germán</asp:ListItem>
                            <asp:ListItem>San Juan</asp:ListItem>
                            <asp:ListItem>San Lorenzo</asp:ListItem>
                            <asp:ListItem>San Sebastián</asp:ListItem>
                            <asp:ListItem>Santa Isabel</asp:ListItem>
                            <asp:ListItem>Toa Alta</asp:ListItem>
                            <asp:ListItem>Toa Baja</asp:ListItem>
                            <asp:ListItem>Trujillo Alto</asp:ListItem>
                            <asp:ListItem>Utuado</asp:ListItem>
                            <asp:ListItem>Vega Alta</asp:ListItem>
                            <asp:ListItem>Vega Baja</asp:ListItem>
                            <asp:ListItem>Vieques (Isla municipio)</asp:ListItem>
                            <asp:ListItem>Villalba</asp:ListItem>
                            <asp:ListItem>Yabucoa</asp:ListItem>
                            <asp:ListItem>Yauco</asp:ListItem>

                        </asp:DropDownList>

                    </div>

                </div>

            </td>

            <td style ="border-top-style: none; vertical-align: bottom;">

                <div style="width: 60%; margin-bottom: 20px; margin-left: auto; margin-right: auto;">

                    <div style=" ">

                        <asp:Label runat="server" Text="C&oacute;digo Postal" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server"   ID="codigoCoPostalTxtBx"></asp:TextBox>

                    </div>

                </div>

            </td>

        </tr>

    </table>

    <%-- Docs req --%>

    <div style="background-color: #616161; padding-left: 7%; margin-top: 40px; margin-bottom:20px">

        <asp:Label ForeColor="#E5E5E5" Font-Size="X-Large" Font-Bold="true" runat="server">Documentos Requeridos</asp:Label>

    </div>

    <%-- This is where literals are inserted --%>

    <div runat ="server" id ="docReqDiv">

    </div>

    <%-- Docs recividos --%>

    <div id ="docRecievedParentDiv" runat ="server" visible ="false">

        <div style="background-color: #616161; padding-left: 7%; margin-top: 40px; margin-bottom: 20px">

            <asp:Label ForeColor="#E5E5E5" Font-Size="X-Large" Font-Bold="true" runat="server">Documentos Recibidos</asp:Label>

        </div>

        <%-- This is where literals are inserted  --%>

        <table style="width: 90%;" cellspacing ="0" runat="server" id="docsAsociadosTableTag">

        </table>

    </div>

    <div style="text-align: center; margin-bottom: 40px; margin-top: 40px; padding-bottom: 100px">

        <div style ="margin-bottom:20px">

            <asp:Label runat="server" style ="padding:10px" BorderStyle ="Dashed"
                 BorderWidth ="3px" Font-Bold ="true" 
                ForeColor="#79256E">Favor asegurarse su solicitud esta completa y correcta,
                             antes de guardar la misma.<br />       Una vez la guarde, todo cambio ser&aacute;
                 a trav&eacute;s del coordinador, que se asigne a trabajar la misma     </asp:Label>

        </div>

        <asp:Button ID="saveBtn" BorderColor="#616161" BorderWidth="3" CssClass="btn" Text="Guardar"
            Style="margin-right: 20px; padding: 10px 15px;" runat="server" OnClick="saveBtn_Click" ForeColor="#79256E" />

        <input type="button" onclick="javascript: window.print();"
            style="border-style: solid; vertical-align: middle; border-color: #616161; 
                   border-width: 3px; padding: 10px 15px;"
            value="Imprimir" />

    </div>

</asp:Content>

