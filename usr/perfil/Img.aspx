<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Img.aspx.vb" Inherits="usr_perfil_Img" EnableViewStateMac="false" meta:resourcekey="PageResource1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../estils/StyleSheet.css" rel="stylesheet" type="text/css" />        
    <script type="text/javascript" src="../../js/JJ.js"></script> 
    <script src="../../js/jquery.form.js" type="text/javascript"></script>   
    <script type="text/javascript">

        $(document).ready(function () {

            var bOKImg = false;

            $('form').ajaxForm({

                beforeSend: function () {
                    document.getElementById('status').style.visibility = 'visible';
                    document.getElementById('loading').style.visibility = 'visible';
                    $('input').attr('readonly', true)
                    document.getElementById('BotoPujar').disabled = true;
                    document.getElementById('BotoError').disabled = true;
                },

                uploadProgress: function (event, position, total, percentComplete) {
                    //var percentVal = percentComplete + '%';
                    //bar.width(percentVal)
                    //percent.html(percentVal);
                    //console.log(percentVal, position, total);
                },

                complete: function (xhr) {
                    var resposta = xhr.responseText;
                    //alert(resposta);
                    if (resposta.indexOf('success') >= 0) {
                        bOKImg = true;
                        document.getElementById('loading').src = '../../img/ok2.jpg';
                        $('#status').html('<input id="BotoOK" onclick="document.location.reload();" type="button" value="OK" />');
                    } else if (resposta.indexOf('error.usr') >= 0) {
                        document.getElementById('loading').src = '../../img/error.jpg';
                        $('#status').html('<input id="BotoError" onclick="document.location.href=' + String.fromCharCode(39) + '../usr/Accedir.aspx?url=' + document.location.href + String.fromCharCode(39) + '" type="button" value="Sesió caducada" />');
                    } else if (bOKImg = false) {
                        document.getElementById('loading').src = '../../img/error.jpg';
                        $('#status').html('<input id="BotoError" onclick="TancarImgPerfil();" type="button" value="Error" />');
                    }
                }


            })
        });

          
    </script>    
</head>
<body>
    <form id="formuploadimg" runat="server" action="GuardarImg.ashx" enctype="multipart/form-data" method="post">
    <div>
        <table style="width: 100%;">
            <tr>
                <td style="text-align:right;">                    
                    <asp:Label ID="Label2" runat="server" Text="Selecciona la imatge:" 
                        meta:resourcekey="Label2Resource1"></asp:Label>
                </td>
                <td style="text-align:left;">
                    <input id="File1" type="file" name="myfile[]" multiple accept="*.jpg;*.jpeg;*.gif;*.png" />
                </td>
            </tr>
            <tr>
                <td style="text-align:right;">                    
                    <input id="BotoPujar" type="submit" value='<%=GetLocalResourceObject("BotoPujar.Text").ToString %>' class="Boton" /></td>
                <td style="text-align:left;">
                    <input id="BotoError" type="button" value='<%=GetLocalResourceObject("BotoCancelar.Text").ToString %>' 
                        onclick="TancarImgPerfil(); return false;" />
                </td>
            </tr>
            <tr>
                <td style="text-align:right;">                    
                    <img id="loading" alt="" src="../../img/loading2.gif" style="visibility:hidden" />
                </td>
                <td style="text-align:left;">
                    <div id="status" style="visibility:hidden">
                        <asp:Label ID="Label1" runat="server" Text="Pujant la foto..." 
                            meta:resourcekey="Label1Resource1"></asp:Label></div>
                </td>
            </tr>
        </table> 
    </div>
    </form>
</body>
</html>
