
Partial Class usr_Tancar
    Inherits JJ.Intern.WebForm.PaginaIdiomaBase

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        JJ.Sesio.Usuari.Desconectar()
    End Sub
End Class
