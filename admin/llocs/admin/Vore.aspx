<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Vore.aspx.vb" Inherits="admin_llocs_admin_Vore" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   <script type="text/javascript">
       function tancar() {
           document.getElementById('FonsDetallHistoric').style.visibility = 'hidden';
           document.getElementById('DetallHistoric').style.visibility = 'hidden';           
           return false;
       }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="background-color:White;">
        <div style="text-align:right;">
            <a href="#" onclick="tancar(); return false;"><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/img/tancar.png" /></a>
        </div>
        <div>
            <asp:Label ID="Titol" runat="server" Text="Label" Font-Bold="True" 
                Font-Size="Large"></asp:Label>
            <br />
            <asp:Label ID="Data" runat="server" Text="Label" Font-Bold="True"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Usuari:"></asp:Label>
&nbsp;<asp:Label ID="Usuari" runat="server" Text="Label"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label5" runat="server" Font-Bold="True" Text="Categoria:"></asp:Label>
&nbsp;<asp:Label ID="Categoria" runat="server" Text="Label"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Descripcio breu:" Font-Bold="True"></asp:Label>
            <br />
            <asp:Label ID="DescripcioBreu" runat="server" Text="Label"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Descripció:" Font-Bold="True"></asp:Label>
            <br />
            <asp:Label ID="Descripcio" runat="server" Text="Label"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Text="Acces:" Font-Bold="True"></asp:Label>
            <br />
            <asp:Label ID="Acces" runat="server" Text="Label"></asp:Label>            
            <br />
            <br />
            <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="Paraules clau:"></asp:Label>
            <br />
            <asp:Label ID="Paraules" runat="server" Text="Label"></asp:Label>
        </div>
    </div>
    </form>
</body>
</html>
