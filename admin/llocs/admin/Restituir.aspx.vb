
Partial Class admin_llocs_admin_Restituir
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim IdHistoric As New Guid(My.Request.QueryString("id"))
        If JJ.Geo.Llocs.LlocClass.RestituirVersio(IdHistoric) Then
            Me.Estat.Text = "OK"
        Else
            Me.Estat.Text = "Errada"
        End If

    End Sub
End Class
