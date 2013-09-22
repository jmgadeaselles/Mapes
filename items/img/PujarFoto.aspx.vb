
Partial Class items_img_PujarFoto
    Inherits JJ.Intern.WebForm.PaginaIdiomaBase

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Validacio
        If Not JJ.Sesio.Usuari.Validat Then
            My.Response.Redirect(JJ.Config.General.Web.GetURL("/usr/Accedir.aspx?url=" + My.Request.Url.ToString))
        End If
        'Lloc
        Me.IdLloc.Value = My.Request.QueryString("id")
        'Idioma
        Me.Idioma.Text = JJ.Idiomes.IdiomaClass.GetNomIdioma(My.Request.QueryString("i"))
        Me.CodiIdioma.Value = My.Request.QueryString("i")        
    End Sub
End Class
