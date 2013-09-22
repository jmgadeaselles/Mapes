<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PujarFoto.aspx.vb" Inherits="items_img_PujarFoto" meta:resourcekey="PageResource1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../estils/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7/jquery.js"></script>    
    <script type="text/javascript" src="../../js/jquery.form.js"></script>
<script type="text/javascript">

    $('form').ajaxForm({


        beforeSend: function () {
            document.getElementById('status').style.visibility = 'visible';
            document.getElementById('loading').style.visibility = 'visible';
            $('input').attr('readonly', true)
            document.getElementById('BotoPujar').disabled = true;
            document.getElementById('BotoCancelar').disabled = true;
        },

        uploadProgress: function (event, position, total, percentComplete) {
            var percentVal = percentComplete + '%';
            bar.width(percentVal)
            percent.html(percentVal);
            //console.log(percentVal, position, total);
        },

        complete: function (xhr) {
            var resposta = xhr.responseText;            
            if (resposta.indexOf('success') >= 0) {
                document.getElementById('loading').src = '../../img/ok2.jpg';
                $('#status').html('<input id="BotoOK" class="Boton" onclick="document.location.reload();" type="button" value="OK" />');
            } else if (resposta.indexOf('error.usr') >= 0) {
                document.getElementById('loading').src = '../../img/error.jpg';
                $('#status').html('<input id="BotoError" onclick="document.location.href=' + String.fromCharCode(39) + '../usr/Accedir.aspx?url=' + document.location.href + String.fromCharCode(39) + '" type="button" value="<%=GetLocalResourceObject("SesioCaducada.Text").ToString%>" />');
            } else {
                document.getElementById('loading').src = '../../img/error.jpg';
                $('#status').html('<input id="BotoError" onclick="TancarPujarFoto();" type="button" value="Error" />');
            }
        }
    });
    
          
    </script>    
</head>
<body>
    <form id="formupload" runat="server" action="img/FileHandler.ashx" enctype="multipart/form-data" method="post">    
    <div style="padding: 15px">
        <table style="width: 100%;">
            <tr>
                <td style="text-align:right;">
                    <asp:Label ID="Label1" runat="server" Text="Foto:" 
                        meta:resourcekey="Label1Resource1"></asp:Label>                    
                </td>
                <td>
                    <input type="file" name="myfile[]" multiple style="width: 100%" 
                        accept="*.jpg;*.jpeg;*.gif;*.png" />
                </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    <asp:Label ID="Label3" runat="server" Text="Idioma:" 
                        meta:resourcekey="Label3Resource1"></asp:Label>                    
                </td>
                <td>
                    <asp:Label ID="Idioma" runat="server" Text="Idioma" 
                        meta:resourcekey="IdiomaResource1"></asp:Label><asp:HiddenField
                        ID="CodiIdioma" runat="server" />
                    <asp:HiddenField ID="IdLloc" runat="server" /> 
                </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    <asp:Label ID="Label2" runat="server" Text="Comentari:" 
                        meta:resourcekey="Label2Resource1"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="Comentari" runat="server" 
                        meta:resourcekey="ComentariResource1"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    <input id="BotoPujar" type="submit" value="Pujar foto" class="Boton" /></td>
                <td>
                    <input id="BotoCancelar" onclick="TancarPujarFoto();" type="button" 
                        value="Cancel·lar" /></td>
            </tr>
            <tr style="height:50px;">
                <td style="text-align:right;">    
                   <img id="loading" alt="" src="../../img/loading2.gif" style="visibility:hidden" /></td>
                <td><div id="status" style="visibility:hidden"><%=GetLocalResourceObject("Pujant.Text").ToString%></div></td>
            </tr>
        </table>
    </div>
    </form>

</body>
</html>
