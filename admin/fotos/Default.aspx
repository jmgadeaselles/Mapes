<%@ Page Title="" Language="VB" MasterPageFile="~/admin/MasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="admin_fotos_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head2" Runat="Server">

    <script type="text/javascript">
        function EsborrarFoto(idFoto) {
            if (confirm('¿Esborrar foto ' + idFoto + '?')) {
                MostrarDetall('Esborrar.aspx', idFoto);
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:Label ID="Label1" runat="server" Text="Fotos"></asp:Label>
    <asp:SqlDataSource ID="Dades" runat="server"></asp:SqlDataSource>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Text:"></asp:Label>&nbsp;<asp:TextBox ID="Text"
        runat="server"></asp:TextBox>
    &nbsp;<asp:Label ID="Label3" runat="server" Text="Habilitada:"></asp:Label>
&nbsp;<asp:DropDownList ID="CampHabilitat" runat="server">
</asp:DropDownList>
&nbsp;<asp:Button ID="BotoBuscar" runat="server" Text="Buscar" />
    <asp:GridView ID="Taula" runat="server" AllowPaging="True" 
        EnableModelValidation="True">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Image ID="Foto" runat="server" />
                    <asp:HiddenField ID="CampID" runat="server" Value='<%# eval("id") %>' />
                    <asp:HiddenField ID="CampArxiu" runat="server" Value='<%# eval("arxiu") %>' />
                    <asp:HiddenField ID="CampExtensio" runat="server" Value='<%# eval("extensio") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkObrir" runat="server">Obrir</asp:LinkButton>
                    <br />
                    <asp:LinkButton ID="LinkAdmin" runat="server">Admin</asp:LinkButton>
                    <br />
                    <asp:LinkButton ID="LinkEsborrar" runat="server">Esborrar</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>

