
Partial Class usr_AccedirPag
    Inherits JJ.Intern.WebForm.PaginaIdiomaBase

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Me.Mail.Text <> "" And Me.Contrasenya.Text <> "" Then
            JJ.Sesio.Usuari.Validar(Me.Mail.Text, Me.Contrasenya.Text)
            If JJ.Sesio.Usuari.Validat Then
                If My.Request.QueryString("url") <> "" Then
                    My.Response.Redirect(My.Request.QueryString("url"))
                Else
                    My.Response.Redirect(JJ.Config.General.Web.GetURLBase())
                End If
            Else
                Me.Errada.Visible = True
            End If
        End If
    End Sub

End Class
