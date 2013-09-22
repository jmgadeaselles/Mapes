<%@ Page Title="" Language="VB" MasterPageFile="~/usr/MasterPage.master" AutoEventWireup="false" CodeFile="Alta.aspx.vb" Inherits="usr_Alta" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head2" Runat="Server">
   
    <script type="text/javascript">
        function BotoNext() {
            var c = document.getElementById('<%=Me.CheckAcceptar.ClientID %>');
            var b = document.getElementById('<%=Me.BotoNext.ClientID %>');
            b.disabled = !c.checked;
            return false;
        }        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:Label ID="Label1" runat="server" Text="Registrarse" 
        meta:resourcekey="Label1Resource1" Font-Bold="False" CssClass="TitolBloc"></asp:Label>
    <asp:MultiView ID="Pasos" runat="server" ActiveViewIndex="0">
        <asp:View ID="Paso1" runat="server">
            <table style="width:100%;">
                <tr>
                    <td style="text-align: center" colspan="2">
                        <asp:Label ID="Missatge" runat="server" Text="Label" CssClass="MissatgeError" 
                            Visible="False" meta:resourcekey="MissatgeResource1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="Label5" runat="server" Text="Mail:" 
                            meta:resourcekey="Label5Resource1"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Mail" runat="server" ValidationGroup="ALTA1" MaxLength="320" 
                            meta:resourcekey="MailResource1"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                            ControlToValidate="Mail" Display="Dynamic" ErrorMessage="*" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                            ValidationGroup="ALTA1" 
                            meta:resourcekey="RegularExpressionValidator1Resource1"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="Mail" ErrorMessage="*" ValidationGroup="ALTA1" 
                            meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="Label6" runat="server" Text="Contrasenya:" 
                            meta:resourcekey="Label6Resource1"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Contrasenya" runat="server" TextMode="Password" 
                            ValidationGroup="ALTA1" MaxLength="20" 
                            meta:resourcekey="ContrasenyaResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="Contrasenya" ErrorMessage="*" ValidationGroup="ALTA1" 
                            meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="Label7" runat="server" Text="Repetir contrasenya:" 
                            meta:resourcekey="Label7Resource1"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="RepetirContrasenya" runat="server" TextMode="Password" 
                            ValidationGroup="ALTA1" MaxLength="20" 
                            meta:resourcekey="RepetirContrasenyaResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                            ControlToValidate="RepetirContrasenya" Display="Dynamic" ErrorMessage="*" 
                            ValidationGroup="ALTA1" 
                            meta:resourcekey="RequiredFieldValidator10Resource1"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" 
                            ControlToCompare="Contrasenya" ControlToValidate="RepetirContrasenya" 
                            ErrorMessage="*" ValidationGroup="ALTA1" 
                            meta:resourcekey="CompareValidator1Resource1"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="Label8" runat="server" Text="Alies:" 
                            meta:resourcekey="Label8Resource1"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Alies" runat="server" ValidationGroup="ALTA1" MaxLength="50" 
                            meta:resourcekey="AliesResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ControlToValidate="Alies" ErrorMessage="*" ValidationGroup="ALTA1" 
                            meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="Label9" runat="server" Text="Nom:" 
                            meta:resourcekey="Label9Resource1"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Nom" runat="server" ValidationGroup="ALTA1" MaxLength="50" 
                            meta:resourcekey="NomResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ControlToValidate="Nom" ErrorMessage="*" ValidationGroup="ALTA1" 
                            meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="Label10" runat="server" Text="Cognoms:" 
                            meta:resourcekey="Label10Resource1"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Cognoms" runat="server" ValidationGroup="ALTA1" MaxLength="50" 
                            meta:resourcekey="CognomsResource1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="Label11" runat="server" Text="Data naiximent:" 
                            meta:resourcekey="Label11Resource1"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="DataNaiximent" runat="server" ValidationGroup="ALTA1" 
                            MaxLength="10" meta:resourcekey="DataNaiximentResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                            ControlToValidate="DataNaiximent" ErrorMessage="*" ValidationGroup="ALTA1" 
                            Display="Dynamic" meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>
                        <asp:Label ID="Label20" runat="server" Text="dd/mm/aaaa" 
                            meta:resourcekey="Label20Resource1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="Label12" runat="server" Text="Pais:" 
                            meta:resourcekey="Label12Resource1"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="Pais" runat="server" ValidationGroup="ALTA1" 
                            meta:resourcekey="PaisResource1">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                            ControlToValidate="Pais" ErrorMessage="*" ValidationGroup="ALTA1" 
                            Display="None" meta:resourcekey="RequiredFieldValidator6Resource1">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="Label13" runat="server" Text="Regio:" 
                            meta:resourcekey="Label13Resource1"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Regio" runat="server" ValidationGroup="ALTA1" MaxLength="50" 
                            meta:resourcekey="RegioResource1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="Label15" runat="server" Text="CP:" 
                            meta:resourcekey="Label15Resource1"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="CP" runat="server" ValidationGroup="ALTA1" MaxLength="10" 
                            meta:resourcekey="CPResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                            ControlToValidate="CP" ErrorMessage="*" ValidationGroup="ALTA1" 
                            meta:resourcekey="RequiredFieldValidator8Resource1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="Label14" runat="server" Text="Localitat:" 
                            meta:resourcekey="Label14Resource1"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Localitat" runat="server" ValidationGroup="ALTA1" 
                            MaxLength="50" meta:resourcekey="LocalitatResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                            ControlToValidate="Localitat" ErrorMessage="*" ValidationGroup="ALTA1" 
                            meta:resourcekey="RequiredFieldValidator9Resource1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="Label16" runat="server" Text="Tel·lefon:" 
                            meta:resourcekey="Label16Resource1"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Telefon" runat="server" ValidationGroup="ALTA1" MaxLength="30" 
                            meta:resourcekey="TelefonResource1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        &nbsp;</td>
                    <td>
                        <asp:CheckBox ID="CheckAcceptar" runat="server" onClick="BotoNext();"
                            Text="He llegit i accepte les" meta:resourcekey="CheckAcceptar" />
                        <asp:LinkButton ID="LinkButton1" runat="server"  meta:resourcekey="LinkButton1" onclientclick="window.open('/legal/condicions.aspx','_blank');return false;" PostBackUrl="#" ValidationGroup="x">condicions d&#39;us</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        
                    </td>
                    <td>
                        <br />
                        <asp:Button ID="BotoNext" runat="server" Text="Següent &gt;&gt;&gt;" 
                            ValidationGroup="ALTA1" CssClass="Boton" 
                            meta:resourcekey="Button1Resource1" Enabled="False" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="Paso2" runat="server">
            <table style="width:100%;">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="Label17" runat="server" 
                            
                            Text="Li hem enviat un mail a l'adreça de correu electrònic que has especificat on trobaras la clau d'activació del teu compter. Posa-la açi:" 
                            meta:resourcekey="Label17Resource1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <asp:Label ID="ErrorClau" runat="server" CssClass="MissatgeError" 
                            Text="La clau d'activació es incorrecta" Visible="False" 
                            meta:resourcekey="ErrorClauResource1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <asp:Label ID="Label18" runat="server" Text="Clau d'activació:" 
                            meta:resourcekey="Label18Resource1"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="ClauActivacio" runat="server" ValidationGroup="ALTA2" 
                            meta:resourcekey="ClauActivacioResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                            ControlToValidate="ClauActivacio" ErrorMessage="*" 
                            meta:resourcekey="RequiredFieldValidator11Resource1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <br />
                        <asp:Button ID="Button2" runat="server" Text="Següent &gt;&gt;&gt;" 
                            ValidationGroup="ALTA2" CssClass="Boton" 
                            meta:resourcekey="Button2Resource1" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="Paso3" runat="server">
            <asp:Label ID="Label19" runat="server" 
                
                Text="Enhorabona, el seu compter ha sigut creat correctament. Ara podras accedir i participar a la Web." 
                meta:resourcekey="Label19Resource1"></asp:Label>
        </asp:View>
    </asp:MultiView>
</asp:Content>

