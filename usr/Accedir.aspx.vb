
Partial Class usr_Accedir
    Inherits JJ.Intern.WebForm.PaginaIdiomaBase

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim Titol As Label = Master.FindControl("Titol")
        Titol.Text = GetLocalResourceObject("Button1Resource1.Text").ToString()
        Dim Link As HyperLink = Master.FindControl("LinkEditar")
        Link.Visible = False
        Dim Idiomes As DropDownList = Master.FindControl("IdiomesItem")
        Idiomes.Enabled = False
        '
        If Me.Mail.Text <> "" And Me.Contrasenya.Text <> "" Then
            JJ.Sesio.Usuari.Validar(Me.Mail.Text, Me.Contrasenya.Text)
            If JJ.Sesio.Usuari.Validat Then
                If My.Request.QueryString("url") <> "" Then
                    My.Response.Redirect(System.Web.HttpUtility.UrlDecode(My.Request.QueryString.ToString.Replace("url=", "")))
                Else
                    My.Response.Redirect("/") 'Hola.aspx
                End If
            Else
                Me.Errada.Visible = True
            End If
        End If
    End Sub
End Class
