<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="usr_perfil_baixa_Default" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../Accedir.css" rel="stylesheet" type="text/css" />
    <link href="../../../estils/StyleSheet.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <center>
    <div id="CapaPerfil" class="DivUsuari">
    
        <asp:MultiView ID="Multivista" runat="server" ActiveViewIndex="0">
            <asp:View ID="VistaConfirmacio" runat="server">
        <table style="width:100%;">
            <tr>
                <td colspan="2">
                <asp:Label ID="Label1" runat="server" 
                    Text="Per a donar-se de baixa es requereix que introduiu la contrasenya." 
                        meta:resourcekey="Label1Resource1"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Errada" runat="server" CssClass="MissatgeError" 
                        Text="Usuari i/o contrasenya no válits" Visible="False" 
                        meta:resourcekey="ErradaResource1"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align:right">
                    <asp:Label ID="Label3" runat="server" Text="Email:" 
                        meta:resourcekey="Label3Resource1"></asp:Label>
                </td>
                <td style="text-align:left">
                    <asp:TextBox ID="Email" runat="server" ValidationGroup="VALIDACIO_BAIXA" 
                        meta:resourcekey="EmailResource1"></asp:TextBox>
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="Email" Display="Dynamic" ErrorMessage="*" 
                        ValidationGroup="VALIDACIO_BAIXA" 
                        meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="Email" ErrorMessage="*" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                        ValidationGroup="VALIDACIO_BAIXA" 
                        meta:resourcekey="RegularExpressionValidator1Resource1"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align:right">
                    <asp:Label ID="Label2" runat="server" Text="Contrasenya:" 
                        meta:resourcekey="Label2Resource1"></asp:Label>
                </td>
                <td style="text-align:left">
                    <asp:TextBox ID="Contrasenya" runat="server" TextMode="Password" 
                        ValidationGroup="VALIDACIO_BAIXA" meta:resourcekey="ContrasenyaResource1"></asp:TextBox>
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="Contrasenya" ErrorMessage="*" 
                        ValidationGroup="VALIDACIO_BAIXA" 
                        meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align:right">
                    <asp:Button ID="Button1" runat="server" CssClass="Boton" 
                        Text="Donar-se de baixa" ValidationGroup="VALIDACIO_BAIXA" 
                        meta:resourcekey="Button1Resource1" />
                </td>
                <td style="text-align:left">
                    <asp:Button ID="Button2" runat="server" Text="Cancel·lar" 
                        PostBackUrl="~/usr/perfil/Default.aspx" 
                        meta:resourcekey="Button2Resource1" />
                </td>
            </tr>
        </table>        
            </asp:View>
            <asp:View ID="VistaBaixa" runat="server">
                <br />
                <asp:Label ID="Label4" runat="server" Text="Has sigut donat de baixa" 
                    meta:resourcekey="Label4Resource1"></asp:Label>
                <br />
                <br />
                <asp:Label ID="Label5" runat="server" Text="Esperem tornar-te a voret." 
                    meta:resourcekey="Label5Resource1"></asp:Label>
                <br />
                <br />
                <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/Default.aspx" 
                    meta:resourcekey="HyperLink5Resource1">Anar a la pagina principal</asp:HyperLink>
                <br />
            </asp:View>
        </asp:MultiView>

    
    </div>
    </center>


</asp:Content>

