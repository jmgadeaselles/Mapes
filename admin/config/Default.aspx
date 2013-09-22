<%@ Page Title="" Language="VB" MasterPageFile="~/admin/MasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="admin_config_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head2" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:Label ID="Label1" runat="server" Text="Configuració"></asp:Label>
    <br />
    <br />
    <asp:MultiView ID="Multivistes" runat="server" ActiveViewIndex="0">
        <asp:View ID="VistaDades" runat="server">
            <asp:CheckBox ID="CheckCrear" runat="server" Text="Permetre crear llocs" />
            <br />
            <asp:CheckBox ID="CheckEditar" runat="server" Text="Permetre editar llocs" />
            <br />
            <br />
            <asp:CheckBox ID="CheckAltes" runat="server" Text="Permetre crear usuaris" />
            <br />
            <asp:CheckBox ID="CheckAltaEmail" runat="server" 
                
                Text="Al donar-se un usuari d'alta, enviar-li un email per a verificarlo" />
            <br />
            <br />
            <asp:CheckBox ID="RegistrarValidacionsCorrectes" runat="server" 
                Text="Registrar els inicis de sessio valits" />
            <br />
            <asp:CheckBox ID="RegistrarValidacioInvalides" runat="server" 
                Text="Registrar els inicis de sessio no valits" />
            <br />
            <br />
            <asp:Button ID="BotoGuardar" runat="server" Text="Guardar" />
        </asp:View>
        <asp:View ID="VistaOK" runat="server">
            <asp:Label ID="Label2" runat="server" Text="OK"></asp:Label>
        </asp:View>
    </asp:MultiView>
</asp:Content>

