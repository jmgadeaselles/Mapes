
Partial Class m_Default
    Inherits JJ.Intern.WebForm.PaginaIdiomaBase

    Protected Overrides Sub InitializeCulture()
        'idioma
        If My.Request.QueryString("i") <> "" Then
            JJ.Sesio.Idioma.Valor = My.Request.QueryString("i").Trim
        End If
        MyBase.InitializeCulture()
    End Sub

End Class
