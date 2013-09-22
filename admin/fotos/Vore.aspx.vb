
Partial Class admin_fotos_Vore
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim IdImg As New Guid(My.Request.QueryString("id"))
        Dim FotoJJ As New JJ.Geo.Llocs.Fotos.FotoClass(IdImg, "ca")
        Me.Foto.ImageUrl = JJ.Geo.Llocs.Fotos.FotoClass.GetURLFoto(IdImg, 800)
    End Sub
End Class
