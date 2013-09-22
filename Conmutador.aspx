<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Conmutador.aspx.vb" Inherits="Conmutador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="1">
        <asp:View ID="VistaErrada" runat="server">
            <p>
                <br />
            </p>
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                NOT FOUND</p>
            <p>
            </p>
        </asp:View>
        <asp:View ID="VistaCercar" runat="server">
            <div style="position:relative;top:0px;left:0px;right:0px;height:100px;background-color:#CCCCFF;">
                <div style="position: absolute;top:50%;left:0px;right:0px;bottom:50%;vertical-align:middle; text-align:center;"><asp:Label ID="Label1" runat="server" Text="Text"></asp:Label>:
                    <asp:TextBox ID="TextBox1" runat="server" Width="346px"></asp:TextBox><asp:Button ID="Button1" runat="server" Text="Cercar" CssClass="Boton" />
                </div>
            </div>
            <div>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>

