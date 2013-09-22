<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Editar.aspx.vb" Inherits="items_foto_Editar" meta:resourcekey="PageResource1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7/jquery.js"></script>    
    <script type="text/javascript" src="/js/jquery.form.js"></script>
    <script type="text/javascript">

        function AnarValidar() {
            parent.document.location.href = UrlBase() + '/usr/AccedirPag.aspx';
        }


        $('form').ajaxForm({


            beforeSend: function () {
                $('input').attr('readonly', true)
            },

            uploadProgress: function (event, position, total, percentComplete) {
                //var percentVal = percentComplete + '%';
                //bar.width(percentVal)
                //percent.html(percentVal);
                //console.log(percentVal, position, total);
            },

            complete: function (xhr) {
                var resposta = xhr.responseText;
                if (resposta.indexOf('success') >= 0) {
                    var capa = $("#CapaComentari");
                    var nouComentari = document.getElementById('ComentariEdt');
                    capa.html(nouComentari.value);
                    tancarEditarFoto();
                } else if (resposta.indexOf('error.usr') >= 0) {
                    //No validat
                    var boto = $("#CapaGuardar");
                    boto.html('<span class="MissatgeError">No estat validat</span> <input type="button" onClick="AnarValidar();" value="Validar" />');
                } else {
                    //Errada
                    var boto = $("#CapaGuardar");
                    boto.html('<span class="MissatgeError">Errada</span>');
                }
            }
        });
    
          
    </script>    
</head>
<body>
    <form id="form1" runat="server" action="/items/foto/Guardar.ashx" enctype="multipart/form-data" method="post">
    <div style="padding: 3px;">
        <asp:MultiView ID="Multivistes" runat="server" ActiveViewIndex="1">
            <asp:View ID="VistaSenseSesio" runat="server">
                <center>
                    <asp:HyperLink ID="HyperLink1" runat="server" 
                        NavigateUrl="~/usr/AccedirPag.aspx" meta:resourcekey="HyperLink1Resource1">Iniciar sesió</asp:HyperLink>
                </center>
            </asp:View>
            <asp:View ID="VistaSesio" runat="server">        
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="ComentariEdt" runat="server" CssClass="TextEditarFoto" 
                                MaxLength="500" meta:resourcekey="ComentariEdtResource1"></asp:TextBox>                    
                            <asp:HiddenField ID="IdFoto" runat="server" />
                            <asp:HiddenField ID="Idioma" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width:auto">
                            <div id="CapaGuardar" style="position:relative; text-align:right;">
                                <asp:Button ID="BotoGuardar" runat="server" Text="Guardar" 
                                    meta:resourcekey="BotoGuardarResource1" /></div>
                        </td>
                        <td style="text-align:left;">
                            <asp:LinkButton ID="LinkCancelar" runat="server" 
                                onclientclick="tancarEditarFoto();return false" 
                                meta:resourcekey="LinkCancelarResource1">Cancel·lar</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </asp:View>
        </asp:MultiView>


    </div>
    </form>
</body>
</html>
