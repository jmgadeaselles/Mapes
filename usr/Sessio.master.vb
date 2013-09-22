
Partial Class usr_Sessio
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not JJ.Sesio.Usuari.Validat Then
            My.Response.Redirect(JJ.Config.General.Web.GetURL("/usr/Accedir.aspx?url=" + My.Request.Url.ToString))
        End If
    End Sub
End Class

