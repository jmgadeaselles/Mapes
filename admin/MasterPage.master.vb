
Partial Class admin_MasterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not JJ.Sesio.Usuari.Validat Or JJ.Sesio.Usuari.Tipus = JJ.Usuaris.TipusUsuari.Usuari Then
            My.Response.Redirect(JJ.Config.General.Web.GetURL("/admin/Denegat.aspx"))
        End If
    End Sub
End Class

