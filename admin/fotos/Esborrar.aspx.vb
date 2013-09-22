
Partial Class admin_llocs_Esborrar
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim IdFoto As New Guid(My.Request.QueryString("id"))
        If JJ.Geo.Llocs.Fotos.FotoClass.Esborrar(IdFoto) Then
            Me.Estat.Text = "OK: Esborrada"
        Else
            Me.Estat.Text = "Errada"
        End If
    End Sub
End Class
