
Partial Class admin_suggeriments_Default
    Inherits System.Web.UI.Page

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        Me.Dades.ConnectionString = JJ.Config.BBDD.CadenaConexio
        Me.Taula.DataSourceID = "Dades"
        Me.Dades.SelectCommand = "SELECT S.id AS ID, Data, Cognoms + ', ' + Nom AS Usuari, Text AS Suggeriment, Llegit FROM Suggeriments S"
        Me.Dades.SelectCommand += " JOIN Usuaris U ON S.ca_usuari=U.id"
        Me.Dades.SelectCommand += " ORDER BY data DESC"
    End Sub

    Protected Sub Taula_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Taula.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Boton detalles
            Dim BotoDetalls As LinkButton = e.Row.FindControl("LinkEsborrar")
            BotoDetalls.PostBackUrl = "#"
            BotoDetalls.OnClientClick = "EsborrarSugg('" + BotoDetalls.CommandArgument + "');return false;"
        End If
    End Sub
End Class
