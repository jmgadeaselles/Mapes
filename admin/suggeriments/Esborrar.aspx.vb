
Partial Class admin_llocs_Esborrar
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim IdLloc As New Guid(My.Request.QueryString("id"))
        If JJ.Varios.Suggeriments.Esborrar(IdLloc) Then
            Me.Estat.Text = "OK: Esborrat"
        Else
            Me.Estat.Text = "Errada"
        End If
    End Sub
End Class
