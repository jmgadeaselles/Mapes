<%@ Page Title="" Language="VB" MasterPageFile="~/items/Detall.master" AutoEventWireup="false" CodeFile="Accedir.aspx.vb" Inherits="usr_Accedir" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../estils/StyleSheet.css" rel="stylesheet" type="text/css" />   
    <link href="../estils/Menu.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="border-style: hidden; border-width: 10px; width:100%;">
        <tr>
            <td colspan="2">
                <asp:Label ID="Errada" runat="server" CssClass="MissatgeError" 
                    Text="Usuari i/o contrasenya no correctes" Visible="False" 
                    meta:resourcekey="ErradaResource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label1" runat="server" Text="Mail:" 
                    meta:resourcekey="Label1Resource1"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Mail" runat="server" CssClass="CampsValidacio" 
                    ValidationGroup="VALIDACIO" meta:resourcekey="MailResource1"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="Mail" ErrorMessage="*" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                    ValidationGroup="VALIDACIO" 
                    meta:resourcekey="RegularExpressionValidator1Resource1"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="Mail" ErrorMessage="*" ValidationGroup="VALIDACIO" 
                    meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="Label2" runat="server" Text="Contrasenya:" 
                    meta:resourcekey="Label2Resource1"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Contrasenya" runat="server" CssClass="CampsValidacio" 
                    TextMode="Password" ValidationGroup="VALIDACIO" 
                    meta:resourcekey="ContrasenyaResource1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="Contrasenya" ErrorMessage="*" 
                    ValidationGroup="VALIDACIO" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                &nbsp;</td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Iniciar sesió" 
                    ValidationGroup="VALIDACIO" CssClass="Boton" 
                    meta:resourcekey="Button1Resource1" />
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                &nbsp;</td>
            <td>
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/usr/Alta.aspx" 
                    Target="_parent" meta:resourcekey="HyperLink2Resource1">Registrarse</asp:HyperLink>
            </td>
        </tr>
    </table>

</asp:Content>

