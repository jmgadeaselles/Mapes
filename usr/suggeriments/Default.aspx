<%@ Page Title="" Language="VB" MasterPageFile="~/usr/Sessio.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="usr_suggeriments_Default" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head2" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

            <br />
            <center>
            <div style="border: 1px solid #000080; width: 400px; height: 200px; background-color: #C6C6FF;">
                


    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="VistaForm" runat="server">
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align:left">
                                <asp:Label ID="Label1" runat="server" 
                                    Text="Suggeriments per a millorar Ocimap.com:" 
                                    meta:resourcekey="Label1Resource1"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="Suggeriment" runat="server" TextMode="MultiLine" Height="118px" 
                                    Width="386px" meta:resourcekey="TextResource1" MaxLength="1000"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ErrorMessage="*" ControlToValidate="Suggeriment"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="Button1" runat="server" Text="Enviar" CssClass="Boton" 
                                    meta:resourcekey="Button1Resource1" />
                            </td>
                        </tr>
                    </table>                
        </asp:View>
        <asp:View ID="VistaOK" runat="server">
            <asp:Label ID="Resultat" runat="server" 
                Text="Moltes gràcies per el suggeriment, ho tindrem en compte" 
                meta:resourcekey="ResultatResource1"></asp:Label>


            <br />
            <br />
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx" 
                meta:resourcekey="HyperLink1Resource1">Inici</asp:HyperLink>


        </asp:View>
    </asp:MultiView>

                
            </div>
            </center>


</asp:Content>

