<%@ Page Title="" Language="VB" MasterPageFile="~/items/Detall.master" AutoEventWireup="false" CodeFile="Lloc.aspx.vb" Inherits="items_Lloc" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7/jquery.js"></script>    
    <script type="text/javascript" src="/js/jquery.form.js"></script>
    <script type="text/javascript">
        function pulsar(e) {
            tecla = document.all ? e.keyCode : e.which;
            if (tecla == 13) {
                $("#ErradaComentari").html('&nbsp;');
                var id = document.getElementById('<%=Me.IdLloc.ClientID%>').value;                
                var idioma = document.getElementById('<%=Me.IdiomaLloc.ClientID%>').value;
                var text = document.getElementById('NouComentari').value;
                $.get(UrlBase() + '/items/comentari/Guardar.ashx', { l: id, i: idioma, t: text }, function (resposta) {
                    if (resposta.indexOf('success') >= 0) {
                        //OK                                                                        
                        $('table#<%=Me.TaulaComentaris.ClientID%> > tbody > tr:first').before('<tr class="fons_item"><td style="vertical-align:top;width:30px;"><img src="<%=JJ.Sesio.Usuari.DadesWeb.FotoXicoteta %>"></td><td style="vertical-align:top;"><span class="AutorComentari"><%=JJ.Sesio.Usuari.DadesWeb.Text %></span> <span class="DataComentari">Ara</span><br />' + text + '</td></tr>');
                        document.getElementById('NouComentari').value = '';
                    } else if (resposta.indexOf('error.usr') >= 0) {
                        //No validat
                        var linkConectar = '<%=JJ.Config.General.Web.GetURL("/usr/Accedir.aspx?url=" + My.Request.Url.ToString) %>';
                        $('#ErradaComentari').html('La sesió ha caducat. <a href="' + linkConectar + '">Iniciar sesió</a>');
                    } else {
                        //Error
                        $('#ErradaComentari').html('Errada');
                    }
                }) 

            }
            return (tecla != 13);
        }


        function MostrarLink() {
            document.getElementById('CapaLink').style.visibility = 'visible';
            return false;
        }


        function TancarLink() {
            document.getElementById('CapaLink').style.visibility = 'hidden';
            return false;
        }


    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <table style="width:100%;">
        <tr style="vertical-align:top;">
            <td style="padding: 5px">
                <asp:HiddenField ID="Lat" runat="server" />
                <asp:HiddenField ID="Lng" runat="server" />
                <asp:HiddenField ID="Zoom" runat="server" />
                <asp:HiddenField ID="IdCategoria" runat="server" />
                <asp:HiddenField ID="IdLloc" runat="server" />
                <asp:HiddenField ID="IdiomaLloc" runat="server" />
                    <table style="width: 100%;">
                        <tr>
                            <td style="width:1px; vertical-align:top;">
                                <asp:Image ID="ImatgeCategoria" runat="server" 
                                    meta:resourcekey="ImatgeCategoriaResource1" />
                            </td>
                            <td style="text-align:left; vertical-align:top;">
                                <asp:Label ID="DescripcioBreu" CssClass="DescripcioBreu" runat="server" 
                                    Text="Label" meta:resourcekey="DescripcioBreuResource1"></asp:Label>
                            </td>
                            <td class="PanelAutor">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width:50px; vertical-align:top;">
                                            <asp:Image ID="FotoAutor" runat="server" ImageUrl="~/img/usr.png" 
                                                meta:resourcekey="FotoAutorResource1" />
                                        </td>
                                        <td style="vertical-align:top;">
                                            <asp:Label ID="Label7" runat="server" Text="Autor:" Font-Bold="True" 
                                                meta:resourcekey="Label7Resource1"></asp:Label>
                                            <br />
                                            <asp:Label ID="Autor" runat="server" Text="Juan Miguel Gadea Sellés" 
                                                meta:resourcekey="AutorResource2"></asp:Label>
                                            <br  />
                                            <asp:LinkButton ID="LinkHistorial" runat="server" 
                                                meta:resourcekey="LinkHistorialResource1">Historial</asp:LinkButton>
                                            <div id="PanelHistorial" class="PanelHistorial"></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                

<script type="text/javascript"><!--
    google_ad_client = "ca-pub-6504858052643983";
    /* OCIMAP.2 */
    google_ad_slot = "9325452974";
    google_ad_width = 468;
    google_ad_height = 60;
//-->
</script>
<script type="text/javascript"
src="http://pagead2.googlesyndication.com/pagead/show_ads.js">
</script>
                                
                <br />
                <asp:Label ID="Label1" runat="server" CssClass="TitolSeccioLloc" 
                    Text="Descripció" meta:resourcekey="Label1Resource1"></asp:Label>                
                <br />
                <asp:Label ID="Descripcio" runat="server" Text="Label" 
                    meta:resourcekey="DescripcioResource1"></asp:Label>
                <br />
                <br />
                <asp:Label ID="Label2" runat="server" CssClass="TitolSeccioLloc" Text="Accés" 
                    meta:resourcekey="Label2Resource1"></asp:Label>
                <br />
                <asp:Label ID="Acces" runat="server" Text="Label" 
                    meta:resourcekey="AccesResource1"></asp:Label>


                <br />
                <br />
                <span class="TitolSeccioLloc"><asp:Label ID="Label3" runat="server" 
                    Text="Fotos" meta:resourcekey="Label3Resource1"></asp:Label>
                <asp:Label ID="Label4" runat="server" Text=" (" 
                    meta:resourcekey="Label4Resource1"></asp:Label>
                <asp:LinkButton ID="LinkPujarFoto" CssClass="LinkPujarFoto" runat="server" 
                    meta:resourcekey="LinkPujarFotoResource1">Pujar foto</asp:LinkButton>
                <asp:Label ID="Label6" runat="server" Text=")" 
                    meta:resourcekey="Label6Resource1"></asp:Label></span>                
                <div id="FonsPujarFoto" class="FonsDetall"></div>
                <div id="PanelPujarFoto" class="PujarFoto"></div>
                <br />
                <div id="Fotos">
                    <%                   
                                                
                        
                        Dim Id As Guid
                        Try
                            Id = New Guid(My.Request.QueryString("id"))
                        Catch ex As Exception
                            Id = JJ.Geo.Llocs.LlocClass.GetIdLlocMesVisitat()
                        End Try
                        Dim Idioma As String = My.Request.QueryString("i")
                        If Idioma = "" Then
                            Idioma = JJ.Sesio.Idioma.Valor
                        End If
                        Dim Fotos As List(Of JJ.Geo.Llocs.Fotos.FotoClass) = JJ.Geo.Llocs.LlocClass.GetImatges(ID, Idioma)
                        For Each Foto As JJ.Geo.Llocs.Fotos.FotoClass In Fotos
                            Dim Ruta As String = Foto.Arxiu + "_200" + Foto.Extensio
                            Dim Root As String = Server.MapPath("/")
                            Ruta = JJ.Config.General.Web.GetURL(Ruta.Substring(Root.Length))
                            Ruta = Ruta.Replace("\", "/")
                            %>
                            <a href="#" onClick="mostrarFoto('<%=Foto.Id.ToString%>','<%=Idioma%>','<%=Ruta%>')" class="preview" title="<%=Foto.Comentari %>"><img src="<%=Ruta %>" alt="<%=Foto.Comentari %>" border="0" /></a>
                            <%
                        Next
                        
                    %>
                </div>
                <br />
                <asp:Label ID="Label5" runat="server" CssClass="TitolSeccioLloc" 
                    Text="Comentaris" meta:resourcekey="Label5Resource1"></asp:Label>                
                <br />                
                <asp:MultiView ID="MultivistesComentaris" runat="server">
                    <asp:View ID="VistaAcces" runat="server">
                        <span class="PanelIniciarSessioComentaris">
                            <asp:Label ID="Label9" runat="server" 
                            Text="Per a poder afegir comentaris, hi ha que iniciar sessió." 
                            meta:resourcekey="Label9Resource1"></asp:Label>
                            <br />
                            <asp:LinkButton ID="LinkIniciarSesio" runat="server" 
                            meta:resourcekey="LinkIniciarSesioResource1">Iniciar sessió</asp:LinkButton>                            
                        </span>
                    </asp:View>
                    <asp:View ID="VistaAfegir" runat="server">
                        <asp:Label ID="Label8" runat="server" Text="Escriu un comentari:" 
                            meta:resourcekey="Label8Resource1"></asp:Label>
                        <br />
                        <input id="NouComentari" style="width:100%;" maxlength="1000" type="text" onkeypress="return pulsar(event);" />
                        <br />
                        <div id="ErradaComentari" class="MissatgeError">&nbsp;</div>
                    </asp:View>
                </asp:MultiView>
                <br />                                    
                <asp:Table ID="TaulaComentaris" runat="server" Width="100%" CellSpacing="0" 
                    meta:resourcekey="TaulaComentarisResource1">
                </asp:Table>                
                <br />
            </td>
            <td style="width:400px;">
                <table style="width:100%; height:100%;">
                    <tr style="height:100%">
                        <td class="ColRecursos">
                            <div id="map_canvas" style="width:100%;height:100%; position:relative;"></div>                
                            <script type="text/javascript">
                                MontarMapa(document.getElementById('<%=Me.Lat.ClientID%>').value, document.getElementById('<%=Me.Lng.ClientID%>').value, 14, false);

                                function ConfigurarMapa() {
                                    //Mapa
                                    latlng = new google.maps.LatLng(document.getElementById('<%=Me.Lat.ClientID%>').value, document.getElementById('<%=Me.Lng.ClientID%>').value);
                                    map.setCenter(latlng);
                                    //Punt al mapa
                                    var icono = '<%=JJ.Config.General.Web.GetURL("/img/cat/" + Me.IdCategoria.Value + "(x).png") %>';
                                    var marker = new google.maps.Marker({
                                        position: latlng,
                                        icon: icono
                                    });
                                    marker.setMap(map);
                                }                    
                            </script>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;">
                            <asp:Label ID="Posicio" runat="server" Text="Label" CssClass="Coordenades" 
                                meta:resourcekey="PosicioResource1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left;">
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/img/link.png" 
                                meta:resourcekey="Image2Resource1" />
                            <asp:LinkButton ID="LinkButton2" runat="server" 
                                OnClientClick="MostrarLink();return false;" 
                                meta:resourcekey="LinkButton2Resource1">Enllaç a aquest lloc</asp:LinkButton>
                            <div id="CapaLink" class="CapaLink">                                                                
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label10" runat="server" 
                                                Text="Pots copiar l'adreça i pegar-la on vullgues:" 
                                                meta:resourcekey="Label10Resource1"></asp:Label>
                                        </td>
                                        <td style="text-align:right;">
                                            <a href="#" onClick="TancarLink();return false;"><img alt="" border="0" src="../img/tancar.png" /></a>                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding-right: 7px">
                                            <asp:TextBox ID="URL" runat="server" Width="100%" 
                                                meta:resourcekey="URLResource1"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>                            
                            <br />                            
                            


                        </td>
                    </tr>


                    <tr>
                        <td style="padding-top: 7px; vertical-align:top;">
                            <!--fb:like send="false" layout="button_count" width="450" show_faces="false" action="recommend"></fb:like-->
                            <fb:like href='<%=JJ.Config.General.Web.GetURLBase() + "?id=" + Id.ToString + "&i=" + Iif(My.Request.QueryString("i")<>"",My.Request.QueryString("i"),JJ.Sesio.Idioma.Valor) %>&og=1' send="false" layout="button_count" width="450" show_faces="false" action="recommend"></fb:like>
                            &nbsp;&nbsp;

                            <!-- Inserta esta etiqueta donde quieras que aparezca Botón +1. -->
                            <div class="g-plusone" data-size="small" data-href='<%=JJ.Config.General.Web.GetURLBase() + "?id=" + Id.ToString + "&i=" + Iif(My.Request.QueryString("i")<>"",My.Request.QueryString("i"),JJ.Sesio.Idioma.Valor) %>&og=1'></div>

                            <!-- Inserta esta etiqueta después de la última etiqueta de Botón +1. -->
                            <script type="text/javascript">
                                window.___gcfg = { lang: '<%=JJ.Sesio.Idioma.Valor %>' };

                                (function () {
                                    var po = document.createElement('script'); po.type = 'text/javascript'; po.async = true;
                                    po.src = 'https://apis.google.com/js/plusone.js';
                                    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(po, s);
                                })();
                            </script>


                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;max-width:400px;">
                            <script type="text/javascript"><!--
                                google_ad_client = "ca-pub-6504858052643983";
                                /* OCIMAP.1 */
                                google_ad_slot = "9846293778";
                                google_ad_width = 336;
                                google_ad_height = 280;
                            //-->
                            </script>
                            <script type="text/javascript"
                            src="http://pagead2.googlesyndication.com/pagead/show_ads.js">
                            </script>                        
                        </td>
                    </tr>




                </table>
            </td> 
        </tr> 




    </table>

</asp:Content>

