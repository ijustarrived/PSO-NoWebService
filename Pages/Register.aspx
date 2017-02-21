﻿<%@ Page Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true"
    CodeBehind="Register.aspx.cs" Inherits="PSO.Pages.Register" Title="Registrar" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent2" runat="server">

    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.2/jquery-ui.js"></script>

    <link rel="stylesheet" href="https://code.jquery.com/ui/1.10.2/themes/smoothness/jquery-ui.css" />

    <script src="../Scripts/jquery.mask.js" type="text/javascript"></script>

    <script>

        function CopyResToPost() {
            var dirRes = document.getElementById("<%= dirResTxtBx.ClientID%>");

            var dirPost = document.getElementById("<%= dirPostalTxtBx.ClientID%>");

            dirPost.value = dirRes.value;

            var codigoRes = document.getElementById("<%= codigoResiTxtBx.ClientID%>");

            var codigoPost = document.getElementById("<%= codigoPostalTxtBx.ClientID%>");

            codigoPost.value = codigoRes.value;

            var puebloRes = document.getElementById("<%= puebloResiDDL.ClientID%>");

            var puebloPost = document.getElementById("<%= puebloPostalDDL.ClientID%>");

            puebloPost.value = puebloRes.value;
        }

        function ChangeClinetSideColors(lblColor, titleColor)
        {
            $('#printBtn').css({ 'color': lblColor });

            $('#printBtn2').css({ 'color': lblColor });
        }

    </script>

    <%-- General section --%>

    <table style="width: 100%; margin-top: 70px" class="table" cellspacing="0px">

        <tr>

            <td style="border-top-style: none; vertical-align: bottom">

                <div style="width: 60%; margin-left: auto; margin-right: auto; margin-bottom: 20px">

                    <div style="float: left;">

                        <asp:Label runat="server" ID ="nombreLbl" Text="Nombre*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server" ID="nameTxtBx"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="nameTxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

            <td style="border-top-style: none; vertical-align: bottom">

                <div style="width: 60%; margin-right: auto; margin-left: auto; margin-bottom: 20px">

                    <div style="float: left;">

                        <asp:Label runat="server" ID ="emaiLlbl" Text="Correo Electr&oacute;nico*"
                             ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server" ID="emailTxtBx"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RegularExpressionValidator runat ="server" ControlToValidate ="emailTxtBx"
                             Display ="Dynamic" SetFocusOnError ="true" 
                            ErrorMessage ="Incorrecto"
                             ValidationExpression ="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor ="#CC0000">

                        </asp:RegularExpressionValidator>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="emailTxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

        </tr>

        <tr>

            <td style="border-top-style: none; vertical-align: bottom">

                <div style="width: 60%; margin-left: auto; margin-right: auto; margin-bottom: 20px">

                    <div style="float: left;">

                        <asp:Label runat="server" ID ="apePatLbl" Text="Apellido Paterno*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server" AutoCompleteType ="Disabled" ID="apellidoPatTxtBx"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="apellidoPatTxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>



            </td>

            <td style="border-top-style: none; vertical-align: bottom">

                <div style="width: 60%; margin-right: auto; margin-left: auto; margin-bottom: 20px">

                    <div style="float: left;">

                        <asp:Label runat="server" ID ="passLbl" Text="Contraseña*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server" TextMode="Password" AutoCompleteType ="Disabled" 
                            ID="passwordTxtBx"></asp:TextBox>

                        <div>

                            <asp:RegularExpressionValidator runat="server" ID="passwordREV" 
                                SetFocusOnError="true" ForeColor="#CC0000"
                                ErrorMessage="Minimo son tres caracters (a - z, 0 - 9)." Display="Dynamic"
                                ValidationExpression="[a-zA-Z0-9]{3,}"
                                ControlToValidate="passwordTxtBx"></asp:RegularExpressionValidator>

                        </div>

                    </div>

                </div>

            </td>

        </tr>

        <tr>

            <td style="border-top-style: none; vertical-align: bottom">

                <div style="width: 60%; margin-left: auto; margin-right: auto; margin-bottom: 20px">

                    <div style="float: left;">

                        <asp:Label runat="server" ID ="apeMatLbl" Text="Apellido Materno*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server" ID="apellidoMatTxtBx"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="apellidoMatTxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>                

            </td>

            <td style="border-top-style: none; vertical-align: bottom">

                <div style="width: 60%; margin-right: auto; margin-left: auto; margin-bottom: 20px">

                    <div style="float: left;">

                        <asp:Label runat="server" ID ="celLbl" Text="Tel&eacute;fono Celular*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <script>

                            $(function ()
                            {
                                $("#<% = celTxtBx.ClientID%>").mask("999-999-9999");
                            });

                        </script>

                        <asp:TextBox runat="server" ID="celTxtBx"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="celTxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

                

            </td>

        </tr>

        <tr>

            <td style="border-top-style: none; vertical-align: bottom">

                <div style="width: 60%; margin-left: auto; margin-right: auto; margin-bottom: 20px">

                    <div style="float: left;">

                        <asp:Label runat="server" ID ="bdayLbl" Text="Fecha de Nacimiento*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <script>

                            $(function ()
                            {
                                $("#<% = bdayTxtBx.ClientID%>").datepicker();
                            });

                        </script>

                        <asp:TextBox runat="server" placeholder ="MM/DD/YYYY" ID="bdayTxtBx"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="bdayTxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

            <td style="border-top-style: none; vertical-align: bottom">

                <div runat ="server" id ="tipoUserDiv" visible ="false" style="width: 60%; margin-left: auto; 
                margin-right: auto; margin-bottom: 20px">

                    <div>

                        <asp:Label runat="server" ID ="tipoLbl" Text="Tipo de Usuario" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:DropDownList runat="server" ID="tipoUsuarioDDL">

                            <asp:ListItem>Externo</asp:ListItem>

                            <asp:ListItem>Procesador</asp:ListItem>

                            <asp:ListItem>Coordinador</asp:ListItem>

                            <asp:ListItem>Administrador</asp:ListItem>

                            <asp:ListItem>Supervisor</asp:ListItem>

                        </asp:DropDownList>

                    </div>

                </div>

            </td>

        </tr>

        <tr>

            <td style="vertical-align: bottom; border-top-style: none; border-bottom-color: #616161; 
            border-bottom-style: solid; border-bottom-width: 3px; padding-bottom: 40px;">

            </td>

            <td style="vertical-align: bottom; border-top-style: none; border-bottom-color: #616161;
             border-bottom-style: solid; border-bottom-width: 3px; padding-bottom: 40px;">

                

            </td>

        </tr>

        <tr>

            <td style ="text-align:center">

                <asp:Label ForeColor ="#79256E" ID ="campoReqLbl" Font-Bold ="true" runat ="server" 
                    Text ="Los campos con ' * ' son requeridos."></asp:Label>

            </td>

        </tr>

    </table>

    <%-- Address section --%>

    <table style="width: 100%; color: #79256E; margin-top: 70px" class="table">

        <tr>

            <td style="border-top-style: none; vertical-align: bottom;">

                <div style="width:60%; margin-left: auto; margin-right: auto; margin-bottom: 20px">

                    <div style="float: left;">

                        <div style="text-align: center; margin-bottom:20px">

                            <input type="button" onclick="CopyResToPost();" style="border-style: solid; 
                                border-color: #616161; border-width: 3px; padding: 10px 15px;"
                                value="Copiar Informaci&oacute;n Residencial a Postal" id ="printBtn" />
                        </div>

                        <div style="margin-bottom: 20px">

                            <asp:Label runat="server" ID ="residencialLbl" Font-Bold="true" 
                                Font-Size="X-Large" Text="Residencial"></asp:Label>

                        </div>

                        <asp:Label runat="server" ID ="dirLbl" Text="Direcci&oacute;n*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox TextMode="MultiLine" runat="server" ID="dirResTxtBx"></asp:TextBox>

                    </div>
                    
                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="dirResTxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>



                </div>

            </td>

            <td style="vertical-align: bottom; border-top-style: none;">

                <div style="width:60%; margin-right: auto; margin-left: auto; margin-bottom: 20px">

                    <div style="float: left;">

                        <div style="margin-bottom: 20px">

                            <asp:Label runat="server" ID ="PosLbl" Font-Bold="true" 
                                Font-Size="X-Large" Text="Postal"></asp:Label>

                        </div>

                        <asp:Label runat="server" ID ="dirPosLbl" Text="Direcci&oacute;n*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server" TextMode="MultiLine" ID="dirPostalTxtBx"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="dirPostalTxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

        </tr>

        <tr>

            <td style="border-top-style: none; vertical-align: bottom;">

                <div style="width:60%; margin-left: auto; margin-right: auto; margin-bottom: 20px">

                    <div style="float: left;">

                        <asp:Label runat="server" ID ="telLbl" Text="Tel&eacute;fono*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <script>

                            $(function ()
                            {
                                $("#<% = telResTxtBx.ClientID%>").mask("999-999-9999");
                            });

                        </script>

                        <asp:TextBox runat="server" ID="telResTxtBx"></asp:TextBox>

                    </div>

                     <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="telResTxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

            <td style="border-top-style: none; vertical-align: bottom;">

                <div style="margin-right:auto; margin-bottom: 20px; width: 60%; margin-left: auto;">

                    <div>

                        <asp:Label runat="server" ID ="puebloLbl" Text="Pueblo*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:DropDownList runat="server" ID="puebloPostalDDL">

                            <asp:ListItem>Seleccionar</asp:ListItem>

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

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="puebloPostalDDL" Display ="Dynamic" 
                            ErrorMessage ="Requerido" InitialValue ="Seleccionar" ForeColor ="#CC0000" 
                            SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

        </tr>

        <tr>

            <td style="border-top-style: none; vertical-align: bottom;">

                <div style="margin-left:auto; margin-right: auto; margin-bottom: 20px; width: 60%;">

                    <div>

                        <asp:Label runat="server" ID ="puebloPosLbl" Text="Pueblo*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:DropDownList runat="server" ID="puebloResiDDL">

                            <asp:ListItem> Seleccionar </asp:ListItem>

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

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="puebloResiDDL" Display ="Dynamic" 
                            ErrorMessage ="Requerido" InitialValue ="Seleccionar" ForeColor ="#CC0000" 
                            SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

            <td style="border-top-style: none; vertical-align: bottom;">

                <div style="width:60%; margin-right: auto; margin-left: auto; margin-bottom: 20px">

                    <div style="float: left;">

                        <asp:Label runat="server" ID ="codigoLbl" Text="C&oacute;digo Postal*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server" ID="codigoPostalTxtBx"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="codigoPostalTxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>

        </tr>

        <tr>

            <td style="border-top-style: none; vertical-align: bottom;">

                <div style="width:60%; margin-left: auto; margin-right: auto; margin-bottom: 20px">

                    <div style="float: left;">

                        <asp:Label runat="server" ID ="codigoPosLbl" Text="C&oacute;digo Postal*" ForeColor="#79256E"></asp:Label>

                    </div>

                    <div>

                        <asp:TextBox runat="server" ID="codigoResiTxtBx"></asp:TextBox>

                    </div>

                    <div>

                        <asp:RequiredFieldValidator runat ="server" ControlToValidate ="codigoResiTxtBx" Display ="Dynamic" 
                            ErrorMessage ="Requerido" ForeColor ="#CC0000" SetFocusOnError ="true"></asp:RequiredFieldValidator>

                    </div>

                </div>

            </td>
        </tr>

    </table>

    <div style="text-align: center; margin-bottom: 40px; margin-top: 40px; padding-bottom: 100px">

        <asp:Button ID="saveBtn" BorderColor="#616161" BorderWidth="3" Text="Guardar"
            Style="margin-right: 20px; padding: 10px 15px;" runat="server" OnClick="saveBtn_Click" ForeColor="#79256E" />

        <input type="button" onclick="javascript: window.print();"
            style="border-style: solid; vertical-align: middle; border-color: #616161; 
                   border-width: 3px; padding: 10px 15px;"
            value="Imprimir" id ="printBtn2" />

    </div>

</asp:Content>
