<%@ Page Title="" Language="VB" MasterPageFile="~/usr/Sessio.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="usr_perfil_Default" EnableViewStateMac="false" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head2" Runat="Server">
    <link href="../Accedir.css" rel="stylesheet" type="text/css" />
    <link href="../../estils/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7/jquery.js"></script>    
    <script src="../../js/JJ.js" type="text/javascript"></script>
    <script src="../../js/jquery.form.js" type="text/javascript"></script>
    <script src="../../js/Mapa.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

    <center>
    <div id="CapaPerfil" class="DivUsuari">


        <div style="bottom:0px; left: 5px; height:20px; position:absolute;">
            <asp:LinkButton ID="LinkButton1" runat="server" 
                PostBackUrl="~/usr/perfil/baixa/Default.aspx" 
                meta:resourcekey="LinkButton1Resource1">Donar-se de baixa</asp:LinkButton>
        </div>                    


        <table style="width: 100%; vertical-align:top;">
            <tr>
                <td style="vertical-align:top;">
                    <asp:Image ID="Foto" runat="server" meta:resourcekey="FotoResource1" />
                    <br />
                    <asp:LinkButton ID="LinkCamviarImatge" runat="server" 
                        meta:resourcekey="LinkCamviarImatgeResource1">Camviar imatge</asp:LinkButton>
                    <div id="FonsCamviarImatge" class="FonsFoto"></div>                                
                    <div id="CapaCamviarImatge" class="CapaCamviarImatge"></div>                                                                    

                                    
                </td>
                <td>
        <table style="width: 100%;">
            <tr>
                <td style="text-align:right; vertical-align:top;">
                    </td>
                <td style="vertical-align:top; text-align:left;">
                    <asp:Label ID="Missatge" runat="server" Text="Label" Visible="False" 
                        meta:resourcekey="MissatgeResource1"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align:right; vertical-align:top;">
                    <asp:Label ID="Label1" runat="server" Text="Alies:" 
                        meta:resourcekey="Label1Resource1"></asp:Label>
                </td>
                <td style="vertical-align:top; text-align:left;">
                    <asp:Label ID="Alies" runat="server" Text="Label" 
                        meta:resourcekey="AliesResource1"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align:right; vertical-align:top;">
                    <asp:Label ID="Label2" runat="server" Text="Nom:" 
                        meta:resourcekey="Label2Resource1"></asp:Label>
                </td>
                <td style="vertical-align:top; text-align:left;">
                    <asp:TextBox ID="Nom" runat="server" ValidationGroup="DADES" MaxLength="50" 
                        meta:resourcekey="NomResource1"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="Nom" Display="Dynamic" ErrorMessage="*" 
                        ValidationGroup="DADES" 
                        meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align:right; vertical-align:top;">
                    <asp:Label ID="Label3" runat="server" Text="Cognoms:" 
                        meta:resourcekey="Label3Resource1"></asp:Label>
                </td>
                <td style="vertical-align:top; text-align:left;">
                    <asp:TextBox ID="Cognoms" runat="server" MaxLength="50" 
                        meta:resourcekey="CognomsResource1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align:right; vertical-align:top;">
                    <asp:Label ID="Label4" runat="server" Text="DataNaiximent" 
                        meta:resourcekey="Label4Resource1"></asp:Label>
                </td>
                <td style="vertical-align:top; text-align:left;">
                    <asp:TextBox ID="DataNaiximent" runat="server" ValidationGroup="DADES" 
                        MaxLength="10" meta:resourcekey="DataNaiximentResource1"></asp:TextBox>
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="DataNaiximent" Display="Dynamic" ErrorMessage="*" 
                        ValidationGroup="DADES" 
                        meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
&nbsp;<asp:Label ID="Label7" runat="server" Text="dd/mm/aaaa" meta:resourcekey="Label7Resource1"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align:right; vertical-align:top;">
                    <asp:Label ID="Label5" runat="server" Text="Pais:" 
                        meta:resourcekey="Label5Resource1"></asp:Label>
                </td>
                <td style="vertical-align:top; text-align:left;">
                    <asp:DropDownList ID="Pais" runat="server" ValidationGroup="DADES" 
                        meta:resourcekey="PaisResource1">
                    </asp:DropDownList>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ControlToValidate="Pais" Display="Dynamic" ErrorMessage="*" 
                        ValidationGroup="DADES" 
                        meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align:right; vertical-align:top;">
                    Regio:
                </td>
                <td style="vertical-align:top; text-align:left;">
                    <asp:TextBox ID="Regio" runat="server" MaxLength="50" 
                        meta:resourcekey="RegioResource1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align:right; vertical-align:top;">
                    CP:
                </td>
                <td style="vertical-align:top; text-align:left;">
                    <asp:TextBox ID="CP" runat="server" ValidationGroup="DADES" MaxLength="10" 
                        meta:resourcekey="CPResource1"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="CP" Display="Dynamic" ErrorMessage="*" 
                        ValidationGroup="DADES" 
                        meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align:right; vertical-align:top;">
                    <asp:Label ID="Label8" runat="server" meta:resourcekey="Label8Resource1" 
                        Text="Localitat:"></asp:Label>
&nbsp;</td>
                <td style="vertical-align:top; text-align:left;">
                    <asp:TextBox ID="Localitat" runat="server" ValidationGroup="DADES" 
                        MaxLength="50" meta:resourcekey="LocalitatResource1"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                        ControlToValidate="Localitat" Display="Dynamic" ErrorMessage="*" 
                        ValidationGroup="DADES" 
                        meta:resourcekey="RequiredFieldValidator6Resource1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align:right; vertical-align:top;">
                    <asp:Label ID="Label9" runat="server" meta:resourcekey="Label9Resource1" 
                        Text="Tel·lefon"></asp:Label>
                </td>
                <td style="vertical-align:top; text-align:left;">
                    <asp:TextBox ID="Telefon" runat="server" MaxLength="30" 
                        meta:resourcekey="TelefonResource1"></asp:TextBox>                    
                </td>
            </tr>
            <tr>
                <td style="text-align:right; vertical-align:top;">
                    <asp:Label ID="Label6" runat="server" Text="Mail:" 
                        meta:resourcekey="Label6Resource1"></asp:Label>                    
                </td>
                <td style="vertical-align:top; text-align:left;">
                    <asp:Label ID="Mail" runat="server" Text="Label" 
                        meta:resourcekey="MailResource1"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align:right; vertical-align:top;">
                    <asp:Button ID="Button1" runat="server" Text="Guardar" CssClass="Boton" 
                        ValidationGroup="DADES" meta:resourcekey="Button1Resource2" />                    
                </td>
                <td style="vertical-align:top; text-align:left;">
                    <input id="Reset1" type="reset" value='<%=GetLocalResourceObject("BotoCancelar.Text").ToString %>' />
                </td>
            </tr>

        </table>                    
                </td>
            </tr>
        </table>



        
    </div>
    </center>

</asp:Content>

