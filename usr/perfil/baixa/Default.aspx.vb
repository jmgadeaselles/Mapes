
Partial Class usr_perfil_baixa_Default
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        If Not JJ.Usuaris.UsuariClass.DonarseBaixa(Me.Email.Text, Me.Contrasenya.Text) Then
            Me.Errada.Visible = True
        Else
            Me.Multivista.SetActiveView(Me.VistaBaixa)

        End If
    End Sub
End Class
