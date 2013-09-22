
Partial Class admin_avisos_Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.Dades.ConnectionString = JJ.Config.BBDD.CadenaConexio
        Me.Taula.DataSourceID = "Dades"
        Me.Dades.SelectCommand = "SELECT L.id,L.nom,AT.data,U.alies,AT.alerta_posicio AS posicio,AT.alerta_nom AS nom,AT.alerta_categoria AS categoria,AT.alerta_descripcio_breu AS breu,AT.alerta_descripcio AS descripcio,AT.alerta_acces AS acces,AT.alerta_claus AS claus FROM AlertesAtacs AT"
        Me.Dades.SelectCommand += " JOIN Llocs L ON AT.ca_lloc=L.id"
        Me.Dades.SelectCommand += " JOIN Usuaris U ON AT.ca_usuari=U.id"
        Me.Dades.SelectCommand += " ORDER BY AT.data DESC"
    End Sub



    Protected Sub Taula_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Taula.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Boton detalles
            Dim BotoDetalls As LinkButton = e.Row.FindControl("LinkAdmin")
            BotoDetalls.PostBackUrl = "#"
            BotoDetalls.OnClientClick = "MostrarDetall('../llocs/admin/Default.aspx','" + BotoDetalls.CommandArgument + "');return false;"
            'Dim BotoEsborrar As LinkButton = e.Row.FindControl("LinkEsborrar")
            'BotoEsborrar.Attributes.Add("onClick", "if(confirm('¿Esborrar categoria " & BotoEsborrar.CommandArgument & "?')){ window.location='Esborrar.aspx?id=" & BotoEsborrar.CommandArgument & "' } return false;")
        End If
    End Sub
End Class
