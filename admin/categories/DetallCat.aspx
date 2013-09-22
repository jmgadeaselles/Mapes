<%@ Page Title="" Language="VB" MasterPageFile="~/admin/Detall.master" AutoEventWireup="false" CodeFile="DetallCat.aspx.vb" Inherits="admin_categories_DetallCat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="IdCate" runat="server" />
    <asp:HiddenField ID="LlocNou" runat="server" />


    <asp:MultiView ID="MultiVistes" runat="server">
        <asp:View ID="VistaEdicio" runat="server">
            <asp:Table ID="Taula" runat="server" Width="100%">
            </asp:Table>
        </asp:View>
        <asp:View ID="VistaOK" runat="server">
            <asp:Label ID="Resultat" runat="server" Text="OK"></asp:Label>
        </asp:View>
    </asp:MultiView>


</asp:Content>

