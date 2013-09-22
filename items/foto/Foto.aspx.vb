
Partial Class items_foto_Foto
    Inherits JJ.Intern.WebForm.PaginaIdiomaBase

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Parametres
        Dim Id As Guid = New Guid(My.Request.QueryString("id"))
        Dim Idioma As String = My.Request.QueryString("i")
        If Idioma = "" Then
            Idioma = JJ.Sesio.Idioma.Valor
        End If
        'Foto        
        Dim Foto As New JJ.Geo.Llocs.Fotos.FotoClass(Id, Idioma)
        Dim Ruta As String = Foto.Arxiu.Replace("\", "/") + "_800" + Foto.Extensio
        Dim Root As String = Server.MapPath("/")
        Ruta = Ruta.Substring(Root.Length)
        Me.Titol.Text = Foto.TitolLloc
        Me.FotoImg.ImageUrl = "../" + Ruta
        'Me.FotoImg.ImageUrl = "~/" + Ruta
        Me.Comentari.Text = Foto.Comentari
        'Autor
        Dim DadesAutor As JJ.Usuaris.UsuariDadesClass = Foto.Autor
        If DadesAutor.Id = Guid.Empty Then
            Me.Autor.Text = ""
        Else
            Me.Autor.Text = DadesAutor.Text
        End If
        Me.FotoAutor.ImageUrl = DadesAutor.Foto
        Me.LinkHistorial.OnClientClick = "MostrarHistorialFoto('" + Id.ToString + "','" + Idioma + "'); return false;"
        'Editar
        Me.ImgEditar.Visible = JJ.Sesio.Usuari.Validat
        If Me.ImgEditar.Visible Then
            Me.ImgEditar.OnClientClick = "editarComentariFoto('" + Id.ToString + "','" + Idioma + "');return false;"
        End If
    End Sub
End Class
