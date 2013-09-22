Imports System.Data.SqlClient


Partial Class admin_llocs_admin_Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.IdFoto.Value = My.Request.QueryString("id")
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
            Comand.CommandText = "SELECT habilitat FROM Fotos WHERE id=@id"
            Comand.Parameters.AddWithValue("@id", New Guid(Me.IdFoto.Value))
            Dim Dades As SqlDataReader = Comand.ExecuteReader()
            If Dades.Read Then
                Me.Habilitat.Checked = Dades("habilitat")
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
                If Me.IdFoto.Value <> "" Then
                    Dim CodiIdioma As HiddenField = e.Row.FindControl("CodiIdioma")
                    Dim DadesIdioma As SqlDataSource = e.Row.FindControl("DadesIdioma")
                    DadesIdioma.ConnectionString = JJ.Config.BBDD.CadenaConexio
                    Dim TaulaIdioma As GridView = e.Row.FindControl("TaulaIdioma")
                    TaulaIdioma.DataSourceID = "DadesIdioma"
                    'DadesIdioma.SelectCommand = "SELECT H.id, H.data, U.alies, HL.nom, CI.text AS categoria, HL.lat, HL.lng, LEN(HLI.descripcio_breu) breu, LEN(CAST(HLI.descripcio as nvarchar(4000))) AS descripcio, LEN(HLI.acces) acces"
                    'DadesIdioma.SelectCommand += " FROM Historic H"
                    'DadesIdioma.SelectCommand += " JOIN HistoricLlocs HL ON H.id=HL.ca_historic"
                    'DadesIdioma.SelectCommand += " JOIN HistoricLlocsIdioma HLI ON HL.ca_lloc=HLI.ca_lloc AND HL.ca_historic=HLI.ca_historic"
                    'DadesIdioma.SelectCommand += " JOIN Usuaris U ON H.ca_usuari=U.id"
                    'DadesIdioma.SelectCommand += " JOIN CategoriesIdioma CI ON HL.ca_categoria=CI.ca_categoria AND CI.ca_idioma='ca'"
                    'DadesIdioma.SelectCommand += " WHERE HL.ca_lloc=@id_lloc AND HLI.ca_idioma='" + CodiIdioma.Value.Replace("'", "''") + "'"
                    'DadesIdioma.SelectCommand += " ORDER BY H.data DESC"
                    'DadesIdioma.SelectParameters.Add(New Parameter("id_lloc", Data.DbType.Guid, Me.IdLloc.Value))
                    DadesIdioma.SelectCommand = "SELECT HCF.id, HCF.data, U.alies, HCF.comentari, LEN(HCF.comentari) AS 'Long.Comentari'"
                    DadesIdioma.SelectCommand += " FROM Fotos F"
                    DadesIdioma.SelectCommand += " JOIN ComentariFoto CF ON F.id=CF.ca_foto "
                    DadesIdioma.SelectCommand += " JOIN HistoricComentariFoto HCF ON CF.ca_foto=HCF.ca_foto AND CF.ca_idioma=HCF.ca_idioma "
                    DadesIdioma.SelectCommand += " JOIN Usuaris U ON HCF.ca_usuari=U.id "
                    DadesIdioma.SelectCommand += " WHERE F.id=@id_foto AND CF.ca_idioma=@idioma"
                    DadesIdioma.SelectCommand += " ORDER BY HCF.data DESC"
                    DadesIdioma.SelectParameters.Add(New Parameter("id_foto", Data.DbType.Guid, Me.IdFoto.Value))
                    DadesIdioma.SelectParameters.Add(New Parameter("idioma", Data.DbType.String, CodiIdioma.Value))
                End If
            End If
        Catch ex As Exception
            JJ.Registre.RegistrarErrada(ex.ToString)
        End Try
    End Sub


    Protected Sub TaulaIdiomaRowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                'Dim LinkVore As LinkButton = e.Row.FindControl("LinkVore")
                'LinkVore.PostBackUrl = "#"
                'LinkVore.OnClientClick = "mostrarVore('" + LinkVore.CommandArgument + "');return false;"
                Dim LinkRestituir As LinkButton = e.Row.FindControl("LinkRestituir")
                LinkRestituir.PostBackUrl = "#"
                LinkRestituir.OnClientClick = "restituir('" + LinkRestituir.CommandArgument + "');return false;"
            End If
        Catch ex As Exception
            JJ.Registre.RegistrarErrada(ex.ToString)
        End Try
    End Sub

End Class
