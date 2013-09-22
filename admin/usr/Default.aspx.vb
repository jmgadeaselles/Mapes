
Partial Class admin_usr_Default
    Inherits System.Web.UI.Page


    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        Me.Dades.ConnectionString = JJ.Config.BBDD.CadenaConexio
        Me.Taula.DataSourceID = "Dades"
        Me.Dades.SelectCommand = "SELECT ID, ALIES, MAIL, TIPUS, U.NOM, COGNOMS, P.nomEN PAIS FROM USUARIS U JOIN Paissos P ON U.ca_pais=P.iso"
        If Me.Usuari.Text <> "" Then
            Me.Dades.SelectCommand += " WHERE U.Alies LIKE '%" + Me.Usuari.Text.Replace("'", "''") + "%'"
            Me.Dades.SelectCommand += " OR U.Nom LIKE '%" + Me.Usuari.Text.Replace("'", "''") + "%'"
            Me.Dades.SelectCommand += " OR U.Cognoms LIKE '%" + Me.Usuari.Text.Replace("'", "''") + "%'"
            Me.Dades.SelectCommand += " OR U.Mail LIKE '%" + Me.Usuari.Text.Replace("'", "''") + "%'"
        End If
    End Sub

    Protected Sub Taula_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Taula.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Boton detalles
            Dim BotoDetalls As LinkButton = e.Row.FindControl("LinkEditar")
            BotoDetalls.PostBackUrl = "#"
            BotoDetalls.OnClientClick = "MostrarDetall('DetallUSR.aspx','" + BotoDetalls.CommandArgument + "');return false;"
        End If
    End Sub


End Class
