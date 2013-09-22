<%@ Page Title="" Language="VB" MasterPageFile="~/items/Detall.master" AutoEventWireup="false" CodeFile="LlocEdt.aspx.vb" Inherits="items_LlocEdt" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function Inhabilita() {            
            document.getElementById('<%=Me.Guardar.ClientID %>').disabled = true;
            document.forms[0].submit(); //Con el Chrome no se enviaba el formulario, asi lo forzamos            
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:MultiView ID="Multivistes" runat="server">
        <asp:View ID="VistaEditar" runat="server">
        <table style="width:100%;">
        <tr style="vertical-align:top;">
            <td>                
<table style="padding: 10px; width:100%;">
            <tr>
                <td class="CeldaLeyenda">
                    <asp:Label ID="Label1" runat="server" Text="Nom:" 
                        meta:resourceKey="Label1Resource1"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="Nom" runat="server" Width="100%" MaxLength="150" 
                        meta:resourceKey="NomResource1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="CeldaLeyenda">
                    <asp:Label ID="Label5" runat="server" Text="Idioma:" 
                        meta:resourceKey="Label5Resource1"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="IdiomaLlocVore" runat="server" 
                        meta:resourceKey="IdiomaLlocVoreResource1">
                    </asp:DropDownList>
                    <asp:HiddenField ID="IdiomaLloc" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="CeldaLeyenda">
                    <asp:Label ID="Label2" runat="server" Text="Categoria:" 
                        meta:resourceKey="Label2Resource1"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="Categoria" runat="server" 
                        meta:resourceKey="CategoriaResource1">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="CeldaLeyenda">
                    <asp:Label ID="Label6" runat="server" Text="Breu descripcio:" 
                        meta:resourceKey="Label6Resource1"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="BreuDescripcio" runat="server" TextMode="MultiLine" 
                        Width="100%" Height="78px" MaxLength="500" 
                        meta:resourceKey="BreuDescripcioResource1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="CeldaLeyenda">
                    <asp:Label ID="Label7" runat="server" Text="Descripcio:" 
                        meta:resourceKey="Label7Resource1"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="Descripcio" runat="server" Height="148px" TextMode="MultiLine" 
                        Width="100%" meta:resourceKey="DescripcioResource2"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="CeldaLeyenda">
                    <asp:Label ID="Label8" runat="server" Text="Accés al lloc:" 
                        meta:resourceKey="Label8Resource1"></asp:Label>
                    <br />
                    <asp:Label ID="Label11" runat="server" Text="(Com es?)" 
                        meta:resourceKey="Label11Resource1"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="Acces" runat="server" Height="50px" TextMode="MultiLine" 
                        Width="100%" MaxLength="1000" meta:resourceKey="AccesResource1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="CeldaLeyenda">
                    <asp:Label ID="Label9" runat="server" Text="Paraules clau:" 
                        meta:resourceKey="Label9Resource1"></asp:Label>
                    <br />
                    <asp:Label ID="Label10" runat="server" Text="[Separades per (,) ]" 
                        meta:resourceKey="Label10Resource1"></asp:Label>
                </td>
                <td style="vertical-align: top;">
                    <asp:TextBox ID="ParaulesClau" runat="server" Width="100%" 
                        meta:resourceKey="ParaulesClauResource1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align:right">
                    &nbsp;</td>
                <td>
                    <asp:HiddenField ID="IDLloc" runat="server" />
                </td>
            </tr>
            </table>                
            </td>
            <td style="width:400px;">
                <table style="width:100%;">
                    <tr>
                        <td class="ColRecursos">
                            <div id="map_canvas" style="width:100%;height:100%; position:relative;"></div>                
                            <script type="text/javascript">
                                //MontarMapa(document.getElementById('<%=Me.Lat.ClientID%>').value, document.getElementById('<%=Me.Lng.ClientID%>').value, 10);
                                MontarMapa(0, 0, 10, false);

                                function ConfigurarMapa() {
                                    //Mapa
                                    <%if My.Request.QueryString("lat") = "" then %>
                                        latlng = new google.maps.LatLng(document.getElementById('<%=Me.Lat.ClientID%>').value, document.getElementById('<%=Me.Lng.ClientID%>').value);
                                    <%else %>
                                        latlng = new google.maps.LatLng(<%=My.Request.QueryString("lat") %>, <%=My.Request.QueryString("lng") %>);
                                        map.setZoom(<%=My.Request.QueryString("zoom") %>);
                                    <%end if %>
                                    map.setCenter(latlng);
                                    //Punt al mapa
                                    var marker = new google.maps.Marker({
                                        position: latlng,
                                        draggable: true
                                    });
                                    marker.setMap(map);
                                    //
                                    google.maps.event.addListener(marker, 'dragend', function (event) { GuardarLloc(event.latLng) });
                                    google.maps.event.addListener(map, 'zoom_changed', function (event) { GuardarZoom() });                                    

                                    //GUardar posicion de la marca en componentes ocultos de asp.net
                                    function GuardarLloc(Llocalitzacio) {
                                        document.getElementById('<%=Me.Lat.ClientID%>').value = Llocalitzacio.lat();
                                        document.getElementById('<%=Me.Lng.ClientID%>').value = Llocalitzacio.lng();
                                        document.getElementById('<%=Me.LatVore.ClientID%>').value = Llocalitzacio.lat();
                                        document.getElementById('<%=Me.LngVore.ClientID%>').value = Llocalitzacio.lng();                                                                                
                                    }

                                    function GuardarZoom(){
                                        document.getElementById('<%=Me.Zoom.ClientID%>').value = map.getZoom();
                                    }

                                }                    
                            </script>

                        </td>
                    </tr>
                    <tr>
                        <td>
                    <asp:Label ID="Label3" runat="server" Text="Llat:" meta:resourceKey="Label3Resource1"></asp:Label>
                    <asp:TextBox ID="LatVore" runat="server" style="text-align:right;" 
                        ReadOnly="True" meta:resourceKey="LatVoreResource1"></asp:TextBox>
                            ,
                    <asp:Label ID="Label4" runat="server" Text="Llng:" meta:resourceKey="Label4Resource1"></asp:Label>
                    <asp:TextBox ID="LngVore" runat="server" style="text-align:right;" 
                        ReadOnly="True" meta:resourceKey="LngVoreResource1"></asp:TextBox>
                            <asp:HiddenField ID="Lat" runat="server" />
                            <asp:HiddenField ID="Lng" runat="server" />
                            <asp:HiddenField ID="Zoom" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align:right;">
                            &nbsp;
                        <asp:Button ID="Guardar" runat="server" Text="Guardar" CssClass="Boton" 
                                meta:resourceKey="GuardarResource1" onclientclick="Inhabilita();" />
                        </td>
                        <td>
                            &nbsp;
                        <asp:HyperLink ID="Cancelar" NavigateUrl="~/items/Lloc.aspx" runat="server" 
                                meta:resourceKey="CancelarResource1" Text="Cancel·lar"></asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </td>
            <td></td>
        </tr>
    </table>
        </asp:View>
        <asp:View ID="VistaResultat" runat="server">
            <div style="top:0px;left:0px;right:0px;bottom:0px;text-align:center;vertical-align:middle;">
                <asp:Label ID="Missatge" CssClass="MissatgeError" runat="server" 
                    Text="Errada: No s'ha pogut guardar." meta:resourceKey="MissatgeResource1"></asp:Label>
                <br />
                <asp:HyperLink ID="LinkTornar" onClick="history.go(-1);" NavigateUrl="#" 
                    runat="server" meta:resourceKey="LinkTornarResource1" Text="Enrrere"></asp:HyperLink>
            </div>
        </asp:View>
    </asp:MultiView>

    
</asp:Content>

