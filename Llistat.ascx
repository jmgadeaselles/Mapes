<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Llistat.ascx.vb" Inherits="Llistat" %>

<link href="estils/StyleSheet.css" rel="stylesheet" type="text/css" />

<asp:SqlDataSource ID="Dades" runat="server"></asp:SqlDataSource>
<asp:GridView ID="Taula" runat="server" AllowPaging="True" 
    AutoGenerateColumns="False" DataSourceID="Dades" 
    EnableModelValidation="True" CellPadding="4" ForeColor="#333333" 
    GridLines="None" Width="100%">
    <AlternatingRowStyle BackColor="White" BorderStyle="None" />
    <Columns>
        <asp:TemplateField>
            <HeaderTemplate>
                <asp:Label ID="Titol" runat="server" Text="Label"></asp:Label>
            </HeaderTemplate>
            <ItemTemplate>
                <table style="width:100%;" border="0">
                    <tr>
                        <td style="width:100px;vertical-align:top;">                            
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# Iif(eval("foto") isnot System.DbNull.Value,JJ.Geo.Llocs.Fotos.FotoClass.GetURLImatge(eval("foto")),"/img/transparent.png") %>' />                            
                        </td>
                        <td style="text-align:left;vertical-align:top;">
                            <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# eval("nom") %>' Font-Bold="True" NavigateUrl='<%# eval("id") %>'>HyperLink</asp:HyperLink>
                            <br />
                            <asp:Label ID="Label2" runat="server" Text='<%# eval("descripcio_breu") %>'></asp:Label>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <EditRowStyle BackColor="#2461BF" />
    <FooterStyle BackColor="#5B5BFF" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#5B5BFF" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#EFF3FB" BorderStyle="None" />
    <SelectedRowStyle BackColor="#D1DDF1" BorderStyle="None" Font-Bold="True" 
        ForeColor="#333333" />
</asp:GridView>


