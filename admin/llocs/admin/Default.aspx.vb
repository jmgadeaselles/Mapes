Imports System.Data.SqlClient


Partial Class admin_llocs_admin_Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.IdLloc.Value = My.Request.QueryString("id")
        If My.Request.QueryString("resultat") IsNot Nothing Then
            Me.Resultat.Visible = True
            If My.Request.QueryString("resultat") = "OK" Then
                Me.Resultat.Text = "OK"
                Me.Resultat.ForeColor = Drawing.Color.Green
            Else
                Me.Resultat.Text = "ERROR"
                Me.Resultat.ForeColor = Drawing.Color.Red
            End If
        End If
        Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
        Dim Comand As New SqlCommand
        Try
            Conexio.Open()
            Comand.Connection = Conexio
            'Nom, Habilitat y editable
            Comand.CommandText = "SELECT nom, habilitat, editable FROM Llocs WHERE id=@id"
            Comand.Parameters.AddWithValue("@id", New Guid(Me.IdLloc.Value))
            Dim Dades As SqlDataReader = Comand.ExecuteReader()
            If Dades.Read Then
                Me.Titol.Text = Dades("nom")
                Me.Habilitat.Checked = Dades("habilitat")
                Me.Editable.Checked = Dades("editable")
            End If
            Dades.Close()
        Catch ex As Exception
            JJ.Registre.RegistrarErrada(ex.ToString)
        Finally
            Conexio.Close()
        End Try
        'HIstoric
        Me.Dades.ConnectionString = JJ.Config.BBDD.CadenaConexio
        Me.Taula.DataSourceID = "Dades"
        Me.Dades.SelectCommand = "SELECT nom, codi, habilitat_web, habilitat_dades FROM IDIOMES ORDER BY nom"
    End Sub

    Protected Sub Taula_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Taula.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                'Boton detalles
                If Me.IdLloc.Value <> "" Then
                    Dim CodiIdioma As HiddenField = e.Row.FindControl("CodiIdioma")
                    Dim DadesIdioma As SqlDataSource = e.Row.FindControl("DadesIdioma")
                    DadesIdioma.ConnectionString = JJ.Config.BBDD.CadenaConexio
                    Dim TaulaIdioma As GridView = e.Row.FindControl("TaulaIdioma")
                    TaulaIdioma.DataSourceID = "DadesIdioma"
                    DadesIdioma.SelectCommand = "SELECT H.id, H.data, U.alies, HL.nom, CI.text AS categoria, HL.lat, HL.lng, LEN(HLI.descripcio_breu) breu, LEN(CAST(HLI.descripcio as nvarchar(4000))) AS descripcio, LEN(HLI.acces) acces"
                    DadesIdioma.SelectCommand += " FROM Historic H"
                    DadesIdioma.SelectCommand += " JOIN HistoricLlocs HL ON H.id=HL.ca_historic"
                    DadesIdioma.SelectCommand += " JOIN HistoricLlocsIdioma HLI ON HL.ca_lloc=HLI.ca_lloc AND HL.ca_historic=HLI.ca_historic"
                    DadesIdioma.SelectCommand += " JOIN Usuaris U ON H.ca_usuari=U.id"
                    DadesIdioma.SelectCommand += " JOIN CategoriesIdioma CI ON HL.ca_categoria=CI.ca_categoria AND CI.ca_idioma='ca'"
                    DadesIdioma.SelectCommand += " WHERE HL.ca_lloc=@id_lloc AND HLI.ca_idioma='" + CodiIdioma.Value.Replace("'", "''") + "'"
                    DadesIdioma.SelectCommand += " ORDER BY H.data DESC"
                    DadesIdioma.SelectParameters.Add(New Parameter("id_lloc", Data.DbType.Guid, Me.IdLloc.Value))
                End If
            End If
        Catch ex As Exception
            JJ.Registre.RegistrarErrada(ex.ToString)
        End Try
    End Sub


    Protected Sub TaulaIdiomaRowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim LinkVore As LinkButton = e.Row.FindControl("LinkVore")
                LinkVore.PostBackUrl = "#"
                LinkVore.OnClientClick = "mostrarVore('" + LinkVore.CommandArgument + "');return false;"
                Dim LinkRestituir As LinkButton = e.Row.FindControl("LinkRestituir")
                LinkRestituir.PostBackUrl = "#"
                LinkRestituir.OnClientClick = "restituir('" + LinkVore.CommandArgument + "');return false;"
            End If
        Catch ex As Exception
            JJ.Registre.RegistrarErrada(ex.ToString)
        End Try
    End Sub

End Class
