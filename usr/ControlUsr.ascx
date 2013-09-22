        <%@ Control Language="VB" AutoEventWireup="false" CodeFile="ControlUsr.ascx.vb" Inherits="usr_ControlUsr" %>
<asp:MultiView ID="Multivistes" runat="server">
    <asp:View ID="VistaAnonim" runat="server">        
        <div id="menu1" class="menu" style="position: absolute; z-index: 999; height: auto; top:-33px; right: 85px;">        
        <ul>          
          <li class="nivel1"><asp:HyperLink ID="HyperLink4" runat="server" 
                  NavigateUrl="~/usr/Alta.aspx" meta:resourceKey="HyperLink4Resource1" 
                  Text="Registrarse"></asp:HyperLink></li>
          <li class="nivel1">|</li>
          <li class="nivel1"><asp:HyperLink ID="HyperLink1" runat="server" 
                  NavigateUrl="~/usr/AccedirPag.aspx" meta:resourceKey="HyperLink1Resource1" 
                  Text="Accedir"></asp:HyperLink></li>
        </ul>
        </div>
        
    </asp:View>    
    <asp:View ID="VistaUsr" runat="server">
        <div id="menu2" class="menu" style="position: absolute; z-index: 999; height: auto; top:-33px; right: 85px;">
        <ul>  
          <li class="nivel1"><asp:HyperLink ID="HyperLink6" runat="server" 
                  NavigateUrl="javascript:detall_nou();" meta:resourceKey="HyperLink6Resource1" 
                  Text="Nou lloc"></asp:HyperLink></li>        
          <li class="nivel1">|</li>        
          <li class="nivel1"><asp:HyperLink ID="NOM" runat="server" 
                  NavigateUrl="javascript:return false;" meta:resourceKey="NOMResource1" 
                  Text="NOM"></asp:HyperLink>            
          <!--[if lte IE 6]><a href="#" class="nivel1ie">Opción 1<table class="falsa"><tr><td><![endif]--> 
	        <ul>
                <% If JJ.Sesio.Usuari.Tipus= JJ.Usuaris.TipusUsuari.Administrador then %>
                <li><asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/admin/" 
                        ForeColor="#CC0000" Font-Bold="True" meta:resourceKey="HyperLink3Resource1" 
                        Text="ADMIN"></asp:HyperLink></li>		        
                <% End if %>
		        <li><asp:HyperLink ID="HyperLink5" runat="server" 
                        NavigateUrl="~/usr/perfil/Default.aspx" meta:resourceKey="HyperLink5Resource1" 
                        Text="Perfil"></asp:HyperLink></li> 
                <li><asp:HyperLink ID="HyperLink7" runat="server" meta:resourceKey="HyperLink7Resource1" 
                        NavigateUrl="~/usr/suggeriments/Default.aspx" Text="Enviar sugerencia"></asp:HyperLink></li>
                               
	        </ul>
           <!--[if lte IE 6]></td></tr></table></a><![endif]-->    
        </li>
        <li class="nivel1">|</li>
        <li class="nivel1"><asp:HyperLink ID="HyperLink2" runat="server" 
                NavigateUrl="~/usr/Tancar.aspx" meta:resourceKey="HyperLink2Resource1" 
                Text="Tancar"></asp:HyperLink></li>
        </ul>
        </div>        
    </asp:View>
</asp:MultiView>

