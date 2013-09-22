<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Foto.aspx.vb" Inherits="items_foto_Foto" meta:resourcekey="PageResource1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="/estils/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-38955465-1']);
        _gaq.push(['_trackPageview']);

        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="DetallFoto">
        <table cellpadding="0" cellspacing="0" style="width: 100%; height:100%">
            <tr>
                <td style="background-color:Black; width:800px; text-align:center;">                                        
                    <asp:Image ID="FotoImg" runat="server" CssClass="MiFoto" 
                        meta:resourcekey="FotoImgResource1" />                    
                </td>
                <td style="vertical-align:top;">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:Label ID="Titol" runat="server" CssClass="TitolDetall" Text="Label" 
                                    meta:resourcekey="TitolResource1"></asp:Label>                                                                
                            </td>
                            <td style="text-align:right; width: auto; vertical-align:top;">
                                <a href="#" onClick="tancar_foto();"> 
                                    <img alt="" border="0" src="../../img/tancar.png" /></a>
                            </td>
                        </tr>
                        <tr>
                            <td>


                                <table style="width: 100%;" class="PanelAutor">
                                    <tr>
                                        <td style="width:50px; vertical-align:top;">
                                            <asp:Image ID="FotoAutor" runat="server" ImageUrl="~/img/usr.png" 
                                                meta:resourcekey="FotoAutorResource1" />
                                        </td>
                                        <td style="vertical-align:top;">
                                            <asp:Label ID="Label7" runat="server" Text="Autor:" Font-Bold="True" 
                                                meta:resourcekey="Label7Resource1"></asp:Label>
                                            <br />
                                            <asp:Label ID="Autor" runat="server" Text="Nom autor" 
                                                meta:resourcekey="AutorResource2"></asp:Label>
                                            <br  />
                                            <asp:LinkButton ID="LinkHistorial" runat="server" 
                                                meta:resourcekey="LinkHistorialResource1">Historial</asp:LinkButton>
                                            <div id="PanelHistorialFoto" class="PanelHistorial"></div>
                                        </td>
                                    </tr>
                                </table>



                                <div id="CapaComentari"><asp:Label ID="Comentari" runat="server" Text="Label" 
                                        meta:resourcekey="ComentariResource1"></asp:Label></div>
                                <asp:ImageButton ID="ImgEditar" runat="server" ImageUrl="~/img/editar.jpg" 
                                    BorderStyle="None" BorderWidth="0px" 
                                    onclientclick="editarComentariFoto();return false;" PostBackUrl="#" 
                                    meta:resourcekey="ImgEditarResource1" />   
                                <div id="FonsEditarComentari" class="FonsEditarComentari"></div>
                                <div id="EditarComentari" class="EditarComentari"></div>                                                             
                            </td>
                        </tr>                        
                       
                    </table>                     
                </td>
            </tr>
        </table></div>        
    </form>
</body>
</html>
