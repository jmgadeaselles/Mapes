
Partial Class Llistat
    Inherits System.Web.UI.UserControl

    Dim _Text As String

    Public Enum TiposTop
        Visites = 0
        Nous = 1
        Modificats = 2
    End Enum

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.Dades.ConnectionString = JJ.Config.BBDD.CadenaConexio
    End Sub

    Public Sub Show(ByVal Top As TiposTop, ByVal Text As String)
        Me._Text = Text
        Me.Dades.SelectCommand = "SELECT '/?id=' + CAST(L.id AS varchar(50)) AS id, L.lat, L.lng, L.ca_categoria, L.nom"
        Me.Dades.SelectCommand += " ,(SELECT TOP 1 arxiu + '_100' + extensio FROM Fotos F WHERE F.ca_lloc=L.id ORDER BY visites DESC) AS foto"
        Me.Dades.SelectCommand += " ,(SELECT descripcio_breu FROM LlocsIdioma LI WHERE LI.ca_lloc=L.id AND LI.ca_idioma='" + JJ.Sesio.Idioma.Valor + "') AS descripcio_breu"
        Select Case Top
            Case TiposTop.Nous
                Me.Dades.SelectCommand += ",(SELECT MIN(H.data) FROM HistoricLlocs HL JOIN Historic H ON HL.ca_historic=H.id WHERE HL.ca_lloc=L.id) AS data_lloc"
            Case TiposTop.Modificats
                Me.Dades.SelectCommand += ",(SELECT MAX(H.data) FROM HistoricLlocs HL JOIN Historic H ON HL.ca_historic=H.id WHERE HL.ca_lloc=L.id) AS data_lloc"
        End Select
        Me.Dades.SelectCommand += " FROM Llocs L"
        Me.Dades.SelectCommand += " WHERE L.habilitat='1'"
        Select Case Top
            Case TiposTop.Visites
                Me.Dades.SelectCommand += " ORDER BY L.visites DESC, L.nom"
            Case TiposTop.Nous, TiposTop.Modificats
                Me.Dades.SelectCommand += " ORDER BY data_lloc DESC"
        End Select
    End Sub

    Protected Sub Taula_DataBound(sender As Object, e As System.EventArgs) Handles Taula.DataBound
        CType(Me.Taula.HeaderRow.FindControl("Titol"), Label).Text = Me._Text        
    End Sub
End Class
