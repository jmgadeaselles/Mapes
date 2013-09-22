<%@ Page Title="" Language="VB" MasterPageFile="~/admin/MasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="admin_llocs_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head2" Runat="Server">

    <script type="text/javascript">
        function EsborrarLloc(idLloc) {
            if (confirm('¿Esborrar lloc ' + idLloc + '?')) {
                //window.location = 'Esborrar.aspx?id=' + idLloc;
                MostrarDetall('Esborrar.aspx', idLloc);
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:Label ID="Titol" runat="server" Text="Llocs"></asp:Label>
    <br />
    <asp:SqlDataSource ID="Dades" runat="server"></asp:SqlDataSource>
    <asp:Label ID="Label1" runat="server" Text="Nom:"></asp:Label>
    <asp:TextBox ID="Nom" runat="server"></asp:TextBox>
    <asp:Label ID="Label2" runat="server" Text="Habilitat:"></asp:Label>
    <asp:DropDownList ID="Habilitat" runat="server">
    </asp:DropDownList>
    <asp:Label ID="Label3" runat="server" Text="Editable:"></asp:Label>
    <asp:DropDownList ID="Editable" runat="server">
    </asp:DropDownList>
    <asp:Button ID="Botobuscar" runat="server" Text="Buscar" />
    <asp:GridView ID="Taula" runat="server" AllowPaging="True" 
        EnableModelValidation="True" PageSize="25">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkAdmin" runat="server" CommandArgument='<%# eval("ID") %>'>Admin</asp:LinkButton>
                    &nbsp;-
                    <asp:LinkButton ID="LinkObrir" runat="server" CommandArgument='<%# eval("ID") %>'>Obrir</asp:LinkButton>
                    &nbsp;-
                    <asp:LinkButton ID="LinkEsborrar" runat="server" CommandArgument='<%# eval("ID") %>'>Esborrar</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>

