
Partial Class admin_fotos_Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Me.CampHabilitat.Items.Count = 0 Then
            Me.CampHabilitat.Items.Add(New ListItem("", ""))
            Me.CampHabilitat.Items.Add(New ListItem("Si", "SI"))
            Me.CampHabilitat.Items.Add(New ListItem("No", "NO"))
        End If
        Me.Dades.ConnectionString = JJ.Config.BBDD.CadenaConexio
        Me.Taula.DataSourceID = "Dades"
        Me.Dades.SelectCommand = "SELECT DISTINCT F.id, F.arxiu, F.extensio, F.habilitat FROM Llocs L"
        Me.Dades.SelectCommand += " JOIN Fotos F ON L.id=F.ca_lloc"
        Me.Dades.SelectCommand += " LEFT JOIN ComentariFoto CF ON F.id=CF.ca_foto"
        Dim Filtre As Boolean = False
        If Me.Text.Text <> "" Then
            Filtre = True
            Me.Dades.SelectCommand += " WHERE (L.nom LIKE '%" + Me.Text.Text.Replace("'", "''") + "%' OR CF.comentari LIKE '%" + Me.Text.Text.Replace("'", "''") + "%')"
        End If
        If Me.CampHabilitat.SelectedValue <> "" Then
            Me.Dades.SelectCommand += IIf(Filtre, " AND", " WHERE")
            Me.Dades.SelectCommand += " F.habilitat='" + IIf(Me.CampHabilitat.SelectedValue = "SI", "1", "0") + "'"
        End If

    End Sub



    Protected Sub Taula_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Taula.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Columna 1
            Dim Foto As Image = e.Row.FindControl("Foto")
            Dim CampID As HiddenField = e.Row.FindControl("CampID")
            Dim CampArxiu As HiddenField = e.Row.FindControl("CampArxiu")
            Dim CampExtensio As HiddenField = e.Row.FindControl("CampExtensio")
            Dim Ruta As String = JJ.Config.General.Web.GetURL(CampArxiu.Value.Substring(CampArxiu.Value.IndexOf("\img\"))) + "_100" + CampExtensio.Value
            Foto.ImageUrl = Ruta.Replace("\", "/")
            'Columna 2
            Dim LinkObrir As LinkButton = e.Row.FindControl("LinkObrir")
            LinkObrir.PostBackUrl = "#"
            LinkObrir.OnClientClick = "MostrarDetall('/admin/fotos/Vore.aspx','" + CampID.Value + "');return false;"
            Dim LinkAdmin As LinkButton = e.Row.FindControl("LinkAdmin")
            LinkAdmin.PostBackUrl = "#"
            LinkAdmin.OnClientClick = "MostrarDetall('/admin/fotos/admin/Default.aspx','" + CampID.Value + "');return false;"
            Dim LinkEsborrar As LinkButton = e.Row.FindControl("LinkEsborrar")
            LinkEsborrar.PostBackUrl = "#"
            LinkEsborrar.OnClientClick = "EsborrarFoto('" + CampID.Value + "');return false;"
        End If
    End Sub
End Class
