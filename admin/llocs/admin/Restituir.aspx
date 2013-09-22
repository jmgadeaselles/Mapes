<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Restituir.aspx.vb" Inherits="admin_llocs_admin_Restituir" %>

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
    <div style="background-color:White; text-align:center;">
    
        <asp:Label ID="Estat" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:LinkButton ID="LinkButton1" runat="server" 
            onclientclick="tancar(); return false;" PostBackUrl="#">Tancar</asp:LinkButton>
        <br />
    </div>
    </form>
</body>
</html>
