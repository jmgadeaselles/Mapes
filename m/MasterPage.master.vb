
Partial Class m_MasterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            'Idiomes
            If Me.DesplegableIdioma.Items.Count = 0 Then
                Dim Idiomes As New JJ.Idiomes.LlistatIdiomesWeb()
                For i As Integer = 0 To Idiomes.Count - 1
                    Dim Idioma As JJ.Idiomes.IdiomaClass = Idiomes(i)
                    Me.DesplegableIdioma.Items.Add(New ListItem(Idioma.Nom, Idioma.Codi))
                Next
                Me.DesplegableIdioma.SelectedValue = JJ.Sesio.Idioma.Valor
                'JJ.Sesio.Idioma.Valor = Me.DesplegableIdioma.SelectedValue
            End If
        Catch ex As Exception
            JJ.Registre.RegistrarErrada(ex.ToString)
        End Try
    End Sub
End Class

