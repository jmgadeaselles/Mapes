
Partial Class admin_categories_Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.Dades.ConnectionString = JJ.Config.BBDD.CadenaConexio
        Me.Taula.DataSourceID = "Dades"
        Me.Dades.SelectCommand = "SELECT C.id, CI.text, C.habilitat FROM Categories C JOIN CategoriesIdioma CI ON C.id=CI.ca_categoria WHERE CI.ca_idioma='ca' ORDER BY CI.text"
    End Sub


    Protected Sub Taula_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Taula.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Boton detalles
            Dim BotoDetalls As LinkButton = e.Row.FindControl("LinkEditar")
            BotoDetalls.PostBackUrl = "#"
            BotoDetalls.OnClientClick = "MostrarDetall('DetallCat.aspx','" + BotoDetalls.CommandArgument + "');return false;"
            Dim BotoEsborrar As LinkButton = e.Row.FindControl("LinkEsborrar")
            BotoEsborrar.Attributes.Add("onClick", "if(confirm('¿Esborrar categoria " & BotoEsborrar.CommandArgument & "?')){ window.location='Esborrar.aspx?id=" & BotoEsborrar.CommandArgument & "' } return false;")
        End If
    End Sub
End Class
