<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="directoris_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="Label1" runat="server" Text="Directoris:"></asp:Label>
    <br />
    <asp:HyperLink ID="HyperLink1" runat="server" 
        NavigateUrl="http://www.directori.cat/">Directori català</asp:HyperLink>
    <br />
    <a href="http://anunciofrezco.com/">Publicar Anuncios Gratis</a>
    <br />
    <a href="http://www.directorioxd.com">DirectorioXD</a>
</asp:Content>

