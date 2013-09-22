<%@ Page Title="" Language="VB" MasterPageFile="~/admin/MasterPage.master" AutoEventWireup="false" CodeFile="Esborrar.aspx.vb" Inherits="admin_registre_Esborrar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head2" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:Label ID="Label1" runat="server" Text="Registre esborrat"></asp:Label>
    <br />
    <br />
    <asp:HyperLink ID="HyperLink12" runat="server" 
        NavigateUrl="~/admin/registre/Default.aspx">Tornar al registre</asp:HyperLink>
</asp:Content>

