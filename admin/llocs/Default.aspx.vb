
Partial Class admin_llocs_Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Me.Habilitat.Items.Count = 0 Then
            'Habilitat
            Me.Habilitat.Items.Add(New ListItem("", ""))
            Me.Habilitat.Items.Add(New ListItem("Si", "SI"))
            Me.Habilitat.Items.Add(New ListItem("No", "NO"))
            Me.Habilitat.SelectedIndex = 0
            'Editable
            Me.Editable.Items.Add(New ListItem("", ""))
            Me.Editable.Items.Add(New ListItem("Si", "SI"))
            Me.Editable.Items.Add(New ListItem("No", "NO"))
            Me.Editable.SelectedIndex = 0
        End If
        '''''
        Dim W As Boolean = False
        Me.Dades.ConnectionString = JJ.Config.BBDD.CadenaConexio
        Me.Taula.DataSourceID = "Dades"
        Me.Dades.SelectCommand = "SELECT ID, NOM, HABILITAT, EDITABLE FROM LLOCS"
        If Me.Nom.Text <> "" Then
            W = True
            Me.Dades.SelectCommand += " WHERE NOM LIKE '%" + Me.Nom.Text.Replace("'", "''") + "%'"
        End If
        If Me.Habilitat.SelectedValue <> "" Then
            Dim ValorHabilitat As String = IIf(Me.Habilitat.SelectedValue = "SI", "1", "0")
            If W Then
                Me.Dades.SelectCommand += " AND HABILITAT='" + ValorHabilitat + "'"
            Else
                W = True
                Me.Dades.SelectCommand += " WHERE HABILITAT='" + ValorHabilitat + "'"
            End If
        End If
        If Me.Editable.SelectedValue <> "" Then
            Dim ValorEditable As String = IIf(Me.Editable.SelectedValue = "SI", "1", "0")
            If W Then
                Me.Dades.SelectCommand += " AND EDITABLE='" + ValorEditable + "'"
            Else
                W = True
                Me.Dades.SelectCommand += " WHERE EDITABLE='" + ValorEditable + "'"
            End If
        End If
        Me.Dades.SelectCommand += " ORDER BY NOM"
    End Sub

    Protected Sub Taula_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Taula.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim BotoAdmin As LinkButton = e.Row.FindControl("LinkAdmin")
            BotoAdmin.PostBackUrl = "#"
            BotoAdmin.OnClientClick = "MostrarDetall('/admin/llocs/admin/Default.aspx','" + BotoAdmin.CommandArgument + "');return false;"
            Dim BotoObrir As LinkButton = e.Row.FindControl("LinkObrir")
            BotoObrir.PostBackUrl = "#"
            BotoObrir.OnClientClick = "MostrarDetall('../../items/Lloc.aspx','" + BotoObrir.CommandArgument + "');return false;"
            Dim BotoEsborrar As LinkButton = e.Row.FindControl("LinkEsborrar")
            BotoEsborrar.PostBackUrl = "#"
            BotoEsborrar.OnClientClick = "EsborrarLloc('" + BotoObrir.CommandArgument + "');return false;"

        End If
    End Sub
End Class
