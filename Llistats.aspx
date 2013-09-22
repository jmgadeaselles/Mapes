<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Llistats.aspx.vb" Inherits="Llistats" meta:resourcekey="PageResource1" %>

<%@ Register src="Llistat.ascx" tagname="Llistat" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div style="background-color:White; padding: 0px; margin: 0px; top: 33px; position: absolute; left: 0px; right: 0px; bottom: 0px; height:auto;">
    
    
        <table style="width: 100%;" cellspacing="20">
            <tr>
                <td style="padding: 0px; border: 1px solid #000080; vertical-align:top; background-color:#D2D2FF; width:33%;">                    
                    <uc1:Llistat ID="LlistatTop" runat="server" />
                </td>
                <td style="padding: 0px; border: 1px solid #000080; vertical-align:top;background-color:#D2D2FF; width:33%;">                    
                    <uc1:Llistat ID="LlistatNous" runat="server" />
                </td>
                <td style="padding: 0px; border: 1px solid #000080; vertical-align:top;background-color:#D2D2FF; width:33%;">                    
                    <uc1:Llistat ID="LlistatModificats" runat="server" />
                </td>
            </tr>
        </table> 

    
    </div>

</asp:Content>

