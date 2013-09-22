<%@ Page Title="" Language="VB" MasterPageFile="~/admin/MasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="admin_usr_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head2" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:Label ID="Label1" runat="server" Text="Usuaris"></asp:Label>
<br />
<asp:SqlDataSource ID="Dades" runat="server"></asp:SqlDataSource>
    <asp:Label ID="Label2" runat="server" Text="Usuari:"></asp:Label>
&nbsp;<asp:TextBox ID="Usuari" runat="server"></asp:TextBox>
&nbsp;<asp:Button ID="Button1" runat="server" 
        PostBackUrl="~/admin/usr/Default.aspx" Text="Buscar" />
    <br />
    <asp:GridView ID="Taula" runat="server" EnableModelValidation="True">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkEditar" runat="server" CommandArgument='<%# eval("ID") %>'>Editar</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
<br />
<br />
</asp:Content>

