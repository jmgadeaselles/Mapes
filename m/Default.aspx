<%@ Page Title="" Language="VB" MasterPageFile="~/m/MasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="m_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false&language=<%=JJ.Sesio.Idioma.Valor %>&region=ES"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7/jquery.min.js"></script>
    <script src="js/Mapa.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">   
    <div id="map_canvas" class="DivMapa"></div>


    <div id="LlistatLlocs" class="LlistatLlocs"></div>


    <script type="text/javascript">
        MontarMapa(40.396764, -3.713379, 2, true);
        NavegarMapa('');
    </script>
</asp:Content>

