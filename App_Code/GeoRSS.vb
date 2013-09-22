Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Collections.Generic


Namespace JJ.GeoRSS


    Public Class GeoRSSClass
        Dim _Llocs As List(Of JJ.Geo.Llocs.LlocReadOnlyClass)

        Public Sub New()
            Me._Llocs = New List(Of JJ.Geo.Llocs.LlocReadOnlyClass)
        End Sub

        Public ReadOnly Property Llocs As List(Of JJ.Geo.Llocs.LlocReadOnlyClass)
            Get
                Return Me._Llocs
            End Get
        End Property

        Public Function Navegar(ByVal N As Double, ByVal S As Double, ByVal O As Double, ByVal E As Double, ByVal Z As Integer, ByVal TextBuscar As String) As Boolean
            Me._Llocs.Clear()
            Dim Paraules As String() = JJ.Intern.Funcions.Textos.GetParaules(TextBuscar.Trim)
            Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
            Dim Comand As New SqlCommand
            Try
                Conexio.Open()
                Comand.Connection = Conexio

                Comand.CommandText = "SELECT L.id, L.lat, L.lng, L.ca_categoria, L.nom"
                Comand.CommandText += " ,(SELECT TOP 1 arxiu + '_200' + extensio FROM Fotos F WHERE F.ca_lloc=L.id ORDER BY visites DESC) AS foto"
                Comand.CommandText += " ,(SELECT descripcio_breu FROM LlocsIdioma LI WHERE LI.ca_lloc=L.id AND LI.ca_idioma=@idioma) AS descripcio_breu"
                If TextBuscar <> "" Then
                    Comand.CommandText += " ,COUNT(L.id) AS punts"
                End If
                Comand.CommandText += " FROM Llocs L"
                If TextBuscar <> "" Then
                    Comand.CommandText += " JOIN Claus C ON L.id=C.ca_lloc AND C.ca_idioma=@idioma"
                End If
                Comand.CommandText += " WHERE lat<=@norte AND lat>=@sur AND lng>=@oeste AND lng<=@este"
                If TextBuscar <> "" Then
                    'Comand.CommandText += " AND C.clau=@text"
                    Dim TextSQL As String = ""
                    For Each Paraula As String In Paraules
                        TextSQL += ",'" + Paraula.Replace("'", "''") + "'"
                    Next
                    TextSQL = TextSQL.Substring(1)
                    Comand.CommandText += " AND C.clau IN (" + TextSQL + ")"
                End If
                Comand.CommandText += " AND L.habilitat=@habilitat"
                If TextBuscar <> "" Then
                    Comand.CommandText += " GROUP BY L.id, L.lat, L.lng, L.ca_categoria, L.nom, L.visites"
                    Comand.CommandText += " ORDER BY punts DESC, visites DESC"
                Else
                    Comand.CommandText += " ORDER BY L.visites DESC, L.nom"
                End If

                Comand.Parameters.AddWithValue("@norte", N)
                Comand.Parameters.AddWithValue("@sur", S)
                Comand.Parameters.AddWithValue("@oeste", O)
                Comand.Parameters.AddWithValue("@este", E)
                Comand.Parameters.AddWithValue("@habilitat", True)
                Comand.Parameters.AddWithValue("@idioma", JJ.Sesio.Idioma.Valor)
                'Comand.Parameters.AddWithValue("@text", TextBuscar)
                'JJ.Registre.RegistrarEvent("Idioma RSS: " + JJ.Sesio.Idioma.Valor)
                Dim Dades As SqlDataReader = Comand.ExecuteReader()
                Dim Id As Guid = Guid.Empty
                While Dades.Read
                    If CType(Dades("id"), Guid).ToString <> Id.ToString Then
                        If (Me._Llocs.Count = 0) Or Me.SeparacionMinimaOK(Z, New JJ.Geo.PosicioClass(Dades("lat"), Dades("lng"))) Then
                            Dim Descripcio As String = IIf(Dades("descripcio_breu") Is System.DBNull.Value, "", Dades("descripcio_breu"))
                            Dim Lloc As JJ.Geo.Llocs.LlocReadOnlyClass
                            If Dades("foto") Is System.DBNull.Value Then
                                Lloc = New JJ.Geo.Llocs.LlocReadOnlyClass(Dades("id"), Dades("lat"), Dades("lng"), Dades("ca_categoria"), Dades("nom"), Descripcio)
                            Else
                                'Lloc = New JJ.Geo.Llocs.LlocReadOnlyClass(Dades("id"), Dades("lat"), Dades("lng"), Dades("ca_categoria"), Dades("nom"), Dades("descripcio_breu"), Dades("arxiu"), Dades("extensio"))
                                Lloc = New JJ.Geo.Llocs.LlocReadOnlyClass(Dades("id"), Dades("lat"), Dades("lng"), Dades("ca_categoria"), Dades("nom"), Descripcio, Dades("foto"))
                            End If
                            Me._Llocs.Add(Lloc)
                        End If
                        Id = Dades("id")
                    End If
                End While
                Dades.Close()
                Return True
            Catch ex As Exception
                JJ.Registre.RegistrarErrada(ex)
                Return False
            Finally
                Conexio.Close()
            End Try
        End Function


        Private Function SeparacionMinimaOK(ByVal Zoom As Integer, ByVal Candidat As JJ.Geo.PosicioClass) As Boolean
            'Zoom, lluny 0
            'Zoom, prop 21
            Dim Distancia As Double
            Dim SeparacioMinima As Double = Me.GetSeparacioMinima(Zoom)
            For Each Lloc As JJ.Geo.Llocs.LlocReadOnlyClass In Me._Llocs
                Distancia = Me.GetDistancia(Candidat, Lloc.Posicio)
                If (Distancia < SeparacioMinima) And (Me._Llocs.Count > 3) Then
                    Return False
                End If
            Next
            Return True
        End Function


        Private Function GetSeparacioMinima(ByVal Zoom As Integer) As Double
            Select Case Zoom
                Case 0
                    Return 200000
                Case 1
                    Return 100000
                Case 2
                    Return 50000
                Case 3
                    Return 25000
                Case 4
                    Return 15000
                Case 5
                    Return 10000
                Case 6
                    Return 5000
                Case 7
                    Return 3000
                Case 8
                    Return 2000
                Case 9
                    Return 900
                Case 10
                    Return 400
                Case 11
                    Return 200
                Case 12
                    Return 75
                Case 13
                    Return 45
                Case 14
                    Return 30
                Case 15
                    Return 5
                Case 16
                    Return 4
                Case 17
                    Return 2
                Case 18
                    Return 2
                Case 19
                    Return 2
                Case 20
                    Return 2
                Case 21
                    Return 1
                Case Else
                    Return 1
            End Select
        End Function


        Private Function GetDistancia(ByVal A As JJ.Geo.PosicioClass, ByVal B As JJ.Geo.PosicioClass) As Double
            Dim Lat As Double = A.Lat - B.Lat
            Dim Lng As Double = A.Lng - B.Lng
            Return 14000 * System.Math.Sqrt(Lat * Lat + Lng * Lng)
        End Function

    End Class


End Namespace
