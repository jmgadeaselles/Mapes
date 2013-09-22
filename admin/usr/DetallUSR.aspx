<%@ Page Title="" Language="VB" MasterPageFile="~/admin/Detall.master" AutoEventWireup="false" CodeFile="DetallUSR.aspx.vb" Inherits="admin_usr_DetallUSR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%;">
        <tr style="vertical-align: top;">
            <td style="text-align:right;">
                <asp:Image ID="Foto" runat="server" />
            </td>
            <td>
                <table style="width:100%;">
                    <tr>
                        <td style="text-align:right;font-weight:bold;">
                            &nbsp;</td>
                        <td style="text-align:left;">
                            <asp:Label ID="Resultat" runat="server" Font-Bold="True" Text="Label" 
                                Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;font-weight:bold;">
                            <asp:HiddenField ID="id_usr" runat="server" />
                            <asp:Label ID="Label1" runat="server" Text="Nom:"></asp:Label>
                        </td>
                        <td style="text-align:left;">
                            <asp:Label ID="Nom" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;font-weight:bold;">
                            <asp:Label ID="Label2" runat="server" Text="Cognoms:"></asp:Label>
                        </td>
                        <td style="text-align:left;">
                            <asp:Label ID="Cognoms" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;font-weight:bold;">
                            <asp:Label ID="Label3" runat="server" Text="Alies:"></asp:Label>
                        </td>
                        <td style="text-align:left;">
                            <asp:Label ID="Alies" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;font-weight:bold;">
                            <asp:Label ID="Label4" runat="server" Text="Mail:"></asp:Label>
                        </td>
                        <td style="text-align:left;">
                            <asp:LinkButton ID="Mail" runat="server">LinkButton</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;font-weight:bold;">
                            <asp:Label ID="Label5" runat="server" Text="Tipus de compte:"></asp:Label>
                        </td>
                        <td style="text-align:left;">
                            <asp:Label ID="Tipus" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;font-weight:bold;">
                            <asp:Label ID="Label6" runat="server" Text="Data naiximent:"></asp:Label>
                        </td>
                        <td style="text-align:left;">
                            <asp:Label ID="DataNaiximent" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;font-weight:bold;">
                            <asp:Label ID="Label7" runat="server" Text="Pais:"></asp:Label>
                        </td>
                        <td style="text-align:left;">
                            <asp:Label ID="Pais" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;font-weight:bold;">
                            <asp:Label ID="Label8" runat="server" Text="Regio:"></asp:Label>
                        </td>
                        <td style="text-align:left;">
                            <asp:Label ID="Regio" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;font-weight:bold;">
                            <asp:Label ID="Label9" runat="server" Text="CP:"></asp:Label>
                        </td>
                        <td style="text-align:left;">
                            <asp:Label ID="CP" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;font-weight:bold;">
                            <asp:Label ID="Label10" runat="server" Text="Localitat:"></asp:Label>
                        </td>
                        <td style="text-align:left;">
                            <asp:Label ID="Localitat" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;font-weight:bold;">
                            <asp:Label ID="Label11" runat="server" Text="Tel·lèfon:"></asp:Label>
                        </td>
                        <td style="text-align:left;">
                            <asp:Label ID="Telefon" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">
                            &nbsp;</td>
                        <td style="text-align:left;">
                            <asp:CheckBox ID="Validat" runat="server" 
                                Text="Validat (L'usuari deu de validar el compte)" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">
                            &nbsp;</td>
                        <td style="text-align:left;">
                            <asp:CheckBox ID="Activat" runat="server" Text="Activat" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">
                            &nbsp;</td>
                        <td style="text-align:left;">
                            <asp:Button ID="Button1" runat="server" 
                                PostBackUrl="~/admin/usr/DetallUSR.aspx" Text="Aplicar camvis" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">
                            &nbsp;</td>
                        <td style="text-align:left;">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">
                            &nbsp;</td>
                        <td style="text-align:left;">
                            &nbsp;</td>
                    </tr>

                </table>
            </td>
        </tr>
    </table>   
        
        
        
        
        
        
</asp:Content>

