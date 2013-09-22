<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Historial.aspx.vb" Inherits="items_Historial" meta:resourcekey="PageResource1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>    
    <link href="/estils/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%;">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Historial" Font-Bold="True" 
                        meta:resourcekey="Label1Resource1"></asp:Label>
                </td>
                <td style="text-align:right;">
                    <% If My.Request.QueryString("tipo").ToUpper = "LLOC" Then%>
                        <a href="#" onclick="TancarHistorial();">
                    <% Else%>
                        <a href="#" onclick="TancarHistorialFoto();">
                    <% End If%>
                    <img alt="" border="0" src="/img/tancar.png" />
                    </a>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Table ID="Historial" runat="server" CssClass="Llistat" CellSpacing="0" 
                        meta:resourcekey="HistorialResource1">
                    </asp:Table>
                </td>
            </tr>            
        </table>


        
        
    </div>
    </form>
</body>
</html>
