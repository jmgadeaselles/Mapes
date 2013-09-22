<%@ Page Title="" Language="VB" MasterPageFile="~/admin/MasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="admin_suggeriments_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head2" Runat="Server">

    <script type="text/javascript">
        function EsborrarSugg(idSugg) {
            if (confirm('¿Esborrar suggeriment ' + idSugg + '?')) {
                MostrarDetall('Esborrar.aspx', idSugg);
            }
        }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:SqlDataSource ID="Dades" runat="server"></asp:SqlDataSource>
    <asp:GridView ID="Taula" runat="server" AllowPaging="True" 
        EnableModelValidation="True">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkEsborrar" runat="server" CommandArgument='<%# eval("ID") %>'>Esborrar</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>

