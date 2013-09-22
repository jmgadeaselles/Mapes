<%@ Page Title="" Language="VB" MasterPageFile="~/admin/Detall.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="admin_llocs_admin_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../../../js/Mapa.js" type="text/javascript"></script>
    <link href="../../../estils/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function mostrarVore(idObrir) {
            document.getElementById('FonsDetallHistoric').style.visibility = 'visible';
            document.getElementById('DetallHistoric').style.visibility = 'visible';
            $('#DetallHistoric').html('Loading...');
            $('#DetallHistoric').load('Vore.aspx?id=' + idObrir);
            return false;
        }
        function restituir(idHistoric) {
            if (confirm('Restituir Lloc amb històric id=' + idHistoric + '?')) {
                document.getElementById('FonsDetallHistoric').style.visibility = 'visible';
                document.getElementById('DetallHistoric').style.visibility = 'visible';
                $('#DetallHistoric').html('Loading...');
                $('#DetallHistoric').load('Restituir.aspx?id=' + idHistoric);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="FonsDetallHistoric" class="FonsDetall"></div>
    <div id="DetallHistoric" style="position:absolute;top:50px;left:50px;width: 800px;height: 500px; visibility:hidden;z-index:99999999;background-color:White;"></div>
    <asp:HiddenField ID="IdLloc" runat="server" />
    <asp:Label ID="Titol" runat="server" Text="Label"></asp:Label>
    <br />
    <asp:CheckBox ID="Habilitat" runat="server" Text="Habilitat" />
    <br />
    <asp:CheckBox ID="Editable" runat="server" Text="Editable" />
    <br />
    <asp:Button ID="BotoGuardar" runat="server" Text="Guardar" PostBackUrl="~/admin/llocs/admin/Guardar.aspx" />
&nbsp;<asp:Label ID="Resultat" runat="server" Text="Resultat" Visible="False"></asp:Label>
    <br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Històric"></asp:Label>
    <br />
    <asp:SqlDataSource ID="Dades" runat="server"></asp:SqlDataSource>
    <asp:GridView ID="Taula" runat="server" AutoGenerateColumns="False" 
        EnableModelValidation="True" ShowHeader="False">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="Idioma" runat="server" Text='<%# eval("nom") %>'></asp:Label>
                    <br />
                    <asp:CheckBox ID="HWeb" runat="server" Text="Habilitat Web" Checked='<%# eval("habilitat_web") %>' Enabled="false" />
                    &nbsp;<asp:CheckBox ID="HDades" runat="server" Text="Habilitat dades" Checked='<%# eval("habilitat_dades") %>' Enabled="false" />
                    <br />
                    <asp:SqlDataSource ID="DadesIdioma" runat="server"></asp:SqlDataSource>
                    <asp:HiddenField ID="CodiIdioma" runat="server" Value='<%# eval("codi") %>' />
                    <asp:GridView ID="TaulaIdioma" runat="server" EnableModelValidation="True" OnRowDataBound="TaulaIdiomaRowDataBound">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkVore" runat="server" CommandArgument='<%# eval("id") %>'>Vore</asp:LinkButton>
                                    &nbsp;-
                                    <asp:LinkButton ID="LinkRestituir" runat="server">Restituir</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <br />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

</asp:Content>

