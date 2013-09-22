<%@ Page Title="" Language="VB" MasterPageFile="~/admin/MasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="admin_registre_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head2" Runat="Server">
    <script type="text/javascript">

        function Esborrar() {
            if (confirm('¿Esborrar registre?')) {
                window.location = 'Esborrar.aspx';
            }
        }
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

    <asp:Label ID="Label1" runat="server" Text="Registre"></asp:Label>
        <asp:SqlDataSource ID="Dades" runat="server"></asp:SqlDataSource>

    <asp:HyperLink ID="HyperLink12" runat="server" 
        NavigateUrl="javascript:Esborrar();">Esborrar registre</asp:HyperLink>

    <table style="width: 100%;">
        <tr>
            <td>
                <asp:CheckBox ID="VoreErradades" runat="server" AutoPostBack="True" 
                    Checked="True" Text="Erradades" /><br />
                <asp:CheckBox ID="VoreAdvertencies" runat="server" AutoPostBack="True" 
                    Checked="True" Text="Advertencies" /><br />
                <asp:CheckBox ID="VoreEvents" runat="server" AutoPostBack="True" Checked="True" 
                    Text="Events" />
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Usuari:"></asp:Label>
                <asp:DropDownList ID="Usuari" runat="server" AutoPostBack="True">
                </asp:DropDownList>
                <br />
                <asp:Label ID="Label3" runat="server" Text="IP:"></asp:Label>
                <asp:DropDownList ID="IP" runat="server" AutoPostBack="True">
                </asp:DropDownList>
                <br />
                <asp:Label ID="Label4" runat="server" Text="User Agent:"></asp:Label>
                <asp:DropDownList ID="UserAgent" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
    </table>

    <p>
        <asp:GridView ID="Taula" runat="server" AllowPaging="True" DataSourceID="Dades">
        </asp:GridView>
</p>
    <p>
    <br />
</p>
</asp:Content>

