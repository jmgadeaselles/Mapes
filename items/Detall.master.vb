
Partial Class items_Detall
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Idiomes        
        If Me.IdiomesItem.Items.Count = 0 Then

            Dim Idiomes As New JJ.Idiomes.LlistatIdiomesDades()
            For i As Integer = 0 To Idiomes.Count - 1
                Dim Idioma As JJ.Idiomes.IdiomaClass = Idiomes(i)
                Me.IdiomesItem.Items.Add(New ListItem(Idioma.Nom, Idioma.Codi))
            Next
            'Idioma per defecte...
            If My.Request.QueryString("i") <> "" Then
                Me.IdiomesItem.SelectedValue = My.Request.QueryString("i")
            Else
                Me.IdiomesItem.SelectedValue = JJ.Sesio.Idioma.Valor
            End If
            'si li pasem un idioma no reconegut...
            If (My.Request.QueryString("i") <> "") AndAlso (My.Request.QueryString("i") <> Me.IdiomesItem.SelectedValue) Then
                Me.IdiomesItem.SelectedValue = JJ.Sesio.Idioma.Valor
            End If
        End If
        'Enllaç editar
        Me.LinkEditar.NavigateUrl = JJ.Config.General.Web.GetURL("/items/LlocEdt.aspx?id=" + My.Request.QueryString("id") + "&i=" + Me.IdiomesItem.SelectedValue)
    End Sub
End Class

