<%@ Page Title="" Language="VB" MasterPageFile="~/admin/MasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="admin_categories_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head2" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:Label ID="Label1" runat="server" Text="Categories"></asp:Label>
<asp:SqlDataSource ID="Dades" runat="server"></asp:SqlDataSource>
<br />
    <asp:LinkButton ID="LinkButton1" runat="server" 
        onclientclick="MostrarDetall('DetallCat.aspx','');return false;" 
        PostBackUrl="#">Nova categoria</asp:LinkButton>
<asp:GridView ID="Taula" runat="server" EnableModelValidation="True">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="LinkEditar" runat="server" CommandArgument='<%# eval("ID") %>'>Editar</asp:LinkButton>
                &nbsp;-
                <asp:LinkButton ID="LinkEsborrar" runat="server" CommandArgument='<%# eval("ID") %>'>Esborrar</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
    <br />
    </asp:Content>

