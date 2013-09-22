
Partial Class items_foto_Editar
    Inherits JJ.Intern.WebForm.PaginaIdiomaBase

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Validacio
        If JJ.Sesio.Usuari.Validat Then
            Dim Id As Guid = New Guid(My.Request.QueryString("id"))
            Me.IdFoto.Value = Id.ToString
            Me.Idioma.Value = My.Request.QueryString("i")
            Me.ComentariEdt.Text = JJ.Geo.Llocs.Fotos.FotoClass.GetComentari(Id, Me.Idioma.Value)
            Me.Multivistes.SetActiveView(Me.VistaSesio)
        Else
            Me.Multivistes.SetActiveView(Me.VistaSenseSesio)
        End If
    End Sub

End Class
