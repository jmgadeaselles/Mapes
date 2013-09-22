<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register src="Legal.ascx" tagname="Legal" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">    
    <asp:Literal ID="LinkIdiomes" runat="server"></asp:Literal>

    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false&language=<%=JJ.Sesio.Idioma.Valor %>&region=ES"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7/jquery.min.js"></script>
    <script type="text/javascript" src="/js/Mapa.js"></script>

   <script type="text/javascript">
       function tancar_foto() {
           window.parent.document.getElementById('fons_foto').style.visibility = 'hidden';
           window.parent.document.getElementById('contingut_foto').style.visibility = 'hidden';
           window.parent.document.getElementById('PanelHistorialFoto').style.visibility = 'hidden';
           return false;
       }

       function editarComentariFoto(id,idioma) {
           document.getElementById('FonsEditarComentari').style.visibility = 'visible';
           document.getElementById('EditarComentari').style.visibility = 'visible';
           $('#EditarComentari').html('Loading...');
           $('#EditarComentari').load(UrlBase() + 'items/foto/Editar.aspx?id=' + id + '&i=' + idioma);
           return false;
       }



       function tancarEditarFoto() {
           window.parent.document.getElementById('FonsEditarComentari').style.visibility = 'hidden';
           window.parent.document.getElementById('EditarComentari').style.visibility = 'hidden';
           return false;
       }



       function MostrarHistorialFoto(id) {
           document.getElementById('PanelHistorialFoto').style.visibility = 'visible';
           $('#PanelHistorialFoto').html('Loading...');
           $('#PanelHistorialFoto').load(UrlBase() + 'items/Historial.aspx?id=' + id + "&tipo=foto");
           return false;
       }


       function TancarHistorialFoto() {
           document.getElementById('PanelHistorialFoto').style.visibility = 'hidden';
           return false;
       }


       function buscar(e) {
           tecla = document.all ? e.keyCode : e.which;
           if (tecla == 13) {
               textBuscarJS = document.getElementById('textBuscar').value;
               Refrescar(map, objectesArray);
               //NavegarMapa(document.getElementById('textBuscar').value);
           }
           return (tecla != 13);
       }


    </script>           


    <asp:Literal ID="fbOpenGraph" runat="server" 
        meta:resourcekey="fbOpenGraphResource1"></asp:Literal>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">




    <div id="Llistat" class="BarraLeft">
    
        <ul class="tabs">
            <li><a href="#tab1"><asp:Label ID="Label1" runat="server" Text="Navegar" 
                    meta:resourcekey="Label1Resource1"></asp:Label></a></li>
            <!--<li><a href="#tab2">
                <asp:Label ID="Label2" runat="server" Text="Novetats"></asp:Label></a></li>-->
        </ul>
    
        <div class="tab_container">            

            <div id="tab1" class="tab_content">

                <div style="padding: 5px; background-color: #FFFF99; border: 1px solid #000080; margin-bottom: 10px; text-align: center;">
                    <asp:LinkButton ID="LinkButton1" runat="server" Text="Comparte tus sitios" 
                        PostBackUrl="javascript:detall_nou();" Font-Bold="True" Font-Size="Large" meta:resourcekey="Participa"></asp:LinkButton>
                </div>                

                <input id="textBuscar" type="text" style="padding: 4px; width:285px;" onkeypress="return buscar(event);" /><input class="Boton" id="Button1" type="button" style="width:75px; padding: 4px; border-left: none;" value="<%=GetLocalResourceObject("BotoCercar.Text").ToString %>" onclick="NavegarMapa(document.getElementById('textBuscar').value);" />                    

                <div class="ScrollLlistat" style="top:150px;">
                    <div id="LlistatLlocs" class="LlistatLlocs">
                    <!--Contenido del bloque de texto-->
                    </div>
                </div>
            </div>  
            
            <div id="tab2" class="tab_content">                
                <!--Contenido del bloque de texto-->
            </div>
    
        </div>        
        
    </div>

    <div style="bottom:0px; position:absolute; width:400px;text-align:center; font-size: small;">        
        <uc1:Legal ID="Legal1" runat="server" />        
    </div>

    <div id="map_canvas" class="DivMapa"></div>
    <div id="fons_detall" class="FonsDetall"></div>
    <div id="contingut_detall" class="ContingutDetall">
        <iframe id="contingut" frameborder="0" height="100%" width="100%" style="border;padding:0;margin:0;overflow:hidden">Carregant...</iframe>
    </div>
    <div id="fons_foto" class="FonsFoto"></div>
    <div id="contingut_foto" class="ContingutFoto"><asp:Label ID="Label3" runat="server" meta:resourcekey="Label3Resource1"></asp:Label></div>
   

    <script type="text/javascript">
        MontarMapa(40.396764, -3.713379, 2, true);
        NavegarMapa(document.getElementById('textBuscar').value);

        //TAULES
        $(document).ready(function () {
            //Cuando el sitio carga...
            $(".tab_content").hide(); //Esconde todo el contenido
            $("ul.tabs li:first").addClass("active").show(); //Activa la primera tab
            $(".tab_content:first").show(); //Muestra el contenido de la primera tab
            //On Click Event
            $("ul.tabs li").click(function () {
                $("ul.tabs li").removeClass("active"); //Elimina las clases activas
                $(this).addClass("active"); //Agrega la clase activa a la tab seleccionada
                $(".tab_content").hide(); //Esconde todo el contenido de la tab
                var activeTab = $(this).find("a").attr("href"); //Encuentra el valor del atributo href para identificar la tab activa + el contenido                
                $(activeTab).fadeIn(); //Agrega efecto de transición (fade) en el contenido activo                
                return true;
            });



            <%
            if My.Request.QueryString("id") <> "" then
                %>
                $(document).ready(function(){ detall('<%=My.Request.QueryString("id")%>') }) 
                //detall('<%=My.Request.QueryString("id")%>');
                <%
            end if 
            %>





        });

    </script>



</asp:Content>