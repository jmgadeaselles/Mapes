Imports System.Data.SqlClient


Partial Class usr_suggeriments_Default
    Inherits JJ.Intern.WebForm.PaginaIdiomaBase

    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        If Not JJ.Varios.Suggeriments.Registrar(Me.Suggeriment.Text) Then
            Me.Resultat.Text = "Error"
            Me.Resultat.ForeColor = Drawing.Color.Red
        End If
        Me.MultiView1.SetActiveView(VistaOK)
    End Sub

End Class
