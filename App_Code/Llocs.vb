Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Collections.Generic

Namespace JJ.Geo


    Public Class PosicioClass
        Dim _Lat, _Lng As Double

        Public Sub New()
            Me.Clear()
        End Sub
        Public Sub New(ByVal Lat As Double, ByVal Lng As Double)
            Me._Lat = Lat
            Me._Lng = Lng
        End Sub

        Public Property Lat() As Double
            Get
                Return Me._Lat
            End Get
            Set(value As Double)
                Me._Lat = value
            End Set
        End Property

        Public Property Lng() As Double
            Get
                Return Me._Lng
            End Get
            Set(value As Double)
                Me._Lng = value
            End Set
        End Property

        Public Sub Clear()
            Me._Lat = 0
            Me._Lng = 0
        End Sub

        Public Overrides Function ToString() As String
            Dim NS As String = IIf(Me._Lat >= 0, "N", "S")
            Dim OE As String = IIf(Me._Lng >= 0, "E", "O")
            Return JJ.Intern.Funcions.Math.ConvertirDecimalAGraus(Me._Lat) + " " + NS + "&nbsp;&nbsp;&nbsp;" + JJ.Intern.Funcions.Math.ConvertirDecimalAGraus(Me._Lng) + " " + OE
        End Function
    End Class


    Namespace Llocs


        Public Class LlocClass
            Inherits JJ.DadesBase.ElementBase

            Dim _Nom As String
            Dim _Posicio As PosicioClass
            Dim _IdCategoria As Guid
            Dim _DescripcioBreu, _Descripcio, _Acces, _ParaulesClau As String
            Dim _RegistrarVisita As Boolean
            Dim ParaulesCarregades As Boolean

            Public Sub New(ByVal Idioma As String, ByVal RegistrarVisita As Boolean)
                MyBase.New(Idioma)
                Me._IdCategoria = Guid.Empty
                Me._Posicio = New PosicioClass()
                Me._DescripcioBreu = ""
                Me._Descripcio = ""
                Me._Acces = ""
                Me._ParaulesClau = ""
                Me._RegistrarVisita = RegistrarVisita
                Me.ParaulesCarregades = False
            End Sub

            Public Property Nom As String
                Get
                    Return Me._Nom
                End Get
                Set(value As String)
                    Me._Nom = value
                    If Len(Me._Nom) > 150 Then
                        Me._Nom = Me._Nom.Substring(0, 150)
                    End If
                End Set
            End Property

            Public Property IdCategoria() As Guid
                Get
                    Return Me._IdCategoria
                End Get
                Set(value As Guid)
                    Me._IdCategoria = value
                End Set
            End Property


            Public ReadOnly Property Posicio() As PosicioClass
                Get
                    Return Me._Posicio
                End Get
            End Property

            Public Property DescripcioBreu() As String
                Get
                    Return Me._DescripcioBreu
                End Get
                Set(value As String)
                    Me._DescripcioBreu = value
                    If Len(Me._DescripcioBreu) > 500 Then
                        Me._DescripcioBreu = Me._DescripcioBreu.Substring(0, 500)
                    End If
                End Set
            End Property

            Public Property Descripcio() As String
                Get
                    Return Me._Descripcio
                End Get
                Set(value As String)
                    Me._Descripcio = value
                End Set
            End Property

            Public Property Acces() As String
                Get
                    Return Me._Acces
                End Get
                Set(value As String)
                    Me._Acces = value
                    If Len(Me._Acces) > 1000 Then
                        Me._Acces = Me._Acces.Substring(0, 1000)
                    End If
                End Set
            End Property

            Public Property ParaulesClau() As String
                Get
                    If Not Me.ParaulesCarregades Then
                        Me._ParaulesClau = GetParaulesClau(Me.Id, Me.Idioma)
                        If Me._ParaulesClau <> "" Then
                            Me.ParaulesCarregades = True
                        End If
                        '    Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
                        '    Dim Comando As New SqlCommand
                        '    Try
                        '        Conexio.Open()
                        '        Comando.Connection = Conexio
                        '        Comando.CommandText = "SELECT clau FROM Claus WHERE ca_idioma=@idioma AND ca_lloc=@lloc"
                        '        Comando.Parameters.AddWithValue("@lloc", Me.Id)
                        '        Comando.Parameters.AddWithValue("@idioma", Me.Idioma)
                        '        Dim Dades As SqlDataReader = Comando.ExecuteReader()
                        '        Me._ParaulesClau = ""
                        '        While Dades.Read
                        '            Me._ParaulesClau += ", " + Dades("clau")
                        '        End While
                        '        Dades.Close()
                        '        If Me._ParaulesClau <> "" Then
                        '            Me._ParaulesClau = Me._ParaulesClau.Substring(2)
                        '        End If
                        '    Catch ex As Exception
                        '        JJ.Registre.RegistrarErrada(ex)
                        '        Return ""
                        '    Finally
                        '        Conexio.Close()
                        '    End Try
                    End If
                    Return Me._ParaulesClau
                End Get
                Set(value As String)
                    Me._ParaulesClau = value
                End Set
            End Property

            Public ReadOnly Property Autor() As JJ.Usuaris.UsuariDadesClass
                Get
                    Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
                    Dim Comand As New SqlCommand
                    Try
                        Conexio.Open()
                        Comand.Connection = Conexio
                        Comand.CommandText = "SELECT U.id,U.nom,U.cognoms,U.alies,U.img,U.img_extensio FROM Llocs L"
                        Comand.CommandText += " JOIN Usuaris U ON L.ca_autor=U.id"
                        Comand.CommandText += " WHERE L.id=@lloc"
                        Comand.Parameters.AddWithValue("@lloc", Me.Id)
                        Dim Dades As SqlDataReader = Comand.ExecuteReader()
                        If Not Dades.Read Then
                            Return New JJ.Usuaris.UsuariDadesClass()
                        Else
                            Return New JJ.Usuaris.UsuariDadesClass(Dades("id"), Dades("alies"), Dades("nom"), Dades("cognoms"), Dades("img"), Dades("img_extensio"))
                        End If
                    Catch ex As Exception
                        JJ.Registre.RegistrarErrada(ex)
                        Return New JJ.Usuaris.UsuariDadesClass()
                    Finally
                        Conexio.Close()
                    End Try
                End Get
            End Property


            Public Shared Function GetHistoric(ByVal IdLloc As Guid) As List(Of JJ.DadesBase.Historic.HistoricClass)
                Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
                Dim Comand As New SqlCommand
                Try
                    GetHistoric = New List(Of JJ.DadesBase.Historic.HistoricClass)
                    Conexio.Open()
                    Comand.Connection = Conexio
                    Comand.CommandText = "SELECT U.id,U.nom,U.cognoms,U.alies,H.data,U.img,U.img_extensio FROM HistoricLlocs HL"
                    Comand.CommandText += " JOIN Historic H ON HL.ca_historic=H.id"
                    Comand.CommandText += " JOIN Usuaris U ON H.ca_usuari=U.id"
                    Comand.CommandText += " WHERE HL.ca_lloc=@lloc"
                    Comand.CommandText += " ORDER BY H.data"
                    Comand.Parameters.AddWithValue("@lloc", IdLloc)
                    Dim Dades As SqlDataReader = Comand.ExecuteReader()
                    While Dades.Read
                        Dim Usuari As New JJ.Usuaris.UsuariDadesClass(Dades("id"), Dades("alies"), Dades("nom"), Dades("cognoms"), Dades("img"), Dades("img_extensio"))
                        Dim Item As New JJ.DadesBase.Historic.HistoricClass(Dades("data"), Usuari)
                        GetHistoric.Add(Item)
                    End While
                    Dades.Close()
                Catch ex As Exception
                    JJ.Registre.RegistrarErrada(ex)
                    GetHistoric = Nothing
                Finally
                    Conexio.Close()
                End Try
            End Function

            Public Shared Function RestituirVersio(ByVal IdHistoric As Guid) As Boolean
                Dim IdLloc, IdCategoria As Guid
                Dim Idioma, Nom, DescripcioBreu, Descripcio, Acces, ParaulesClau As String
                Dim Lat, Lng As Double

                Try
                    'Obtenim les dades
                    Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
                    Dim Comand As New SqlCommand
                    Try
                        Conexio.Open()
                        Comand.Connection = Conexio
                        Comand.CommandText = "SELECT HL.ca_lloc, HLI.ca_idioma,HL.ca_categoria,HL.lat,HL.lng, HL.nom,HLI.descripcio_breu,HLI.descripcio,HLI.acces FROM HistoricLlocs HL"
                        Comand.CommandText += " JOIN HistoricLlocsIdioma HLI ON HL.ca_historic=HLI.ca_historic"
                        Comand.CommandText += " WHERE HL.ca_historic=@id"
                        Comand.Parameters.AddWithValue("@id", IdHistoric)
                        Dim Dades As SqlDataReader = Comand.ExecuteReader()
                        If Not Dades.Read Then
                            Throw New Exception("No s'ha trobat el històric del Lloc")
                        Else
                            IdLloc = Dades("ca_lloc")
                            Idioma = Dades("ca_idioma")
                            IdCategoria = Dades("ca_categoria")
                            Lat = Dades("lat")
                            Lng = Dades("lng")
                            Nom = Dades("nom")
                            DescripcioBreu = Dades("descripcio_breu")
                            Descripcio = Dades("descripcio")
                            Acces = Dades("acces")
                        End If
                        Dades.Close()
                        ParaulesClau = GetParaulesClau(IdLloc, Idioma)
                    Finally
                        Conexio.Close()
                    End Try

                    'Restaurem
                    Dim Lloc As New LlocClass(Idioma, False)
                    Lloc.SetId(IdLloc)
                    Lloc.Nom = Nom
                    Lloc.DescripcioBreu = DescripcioBreu
                    Lloc.Descripcio = Descripcio
                    Lloc.Acces = Acces
                    Lloc.Posicio.Lat = Lat
                    Lloc.Posicio.Lng = Lng
                    Lloc.IdCategoria = IdCategoria
                    Lloc.ParaulesClau = ParaulesClau
                    Return Lloc.Guardar()
                Catch ex As Exception
                    JJ.Registre.RegistrarErrada(ex.ToString)
                    Return False
                End Try
            End Function

            Protected Overrides Sub ClearElement()
                Me._Nom = ""
                If Me._Posicio IsNot Nothing Then
                    Me._Posicio.Clear()
                End If
                Me._IdCategoria = Guid.Empty
                Me._DescripcioBreu = ""
                Me._Descripcio = ""
            End Sub

            Protected Overrides Function GuardarElement(Comand As SqlCommand) As Boolean
                If Not Me.PermetreGuardar(Comand) Then
                    JJ.Registre.RegistrarEvent("Permís denegat per guardar un lloc")
                    Return False
                Else                    
                    'Taula Llocs
                    Comand.Parameters.Clear()
                    If Me.EsNou Then
                        Comand.CommandText = "INSERT INTO Llocs"
                        Comand.CommandText += " (id,lat,lng,ca_categoria,nom,ca_autor)"
                        Comand.CommandText += " VALUES"
                        Comand.CommandText += " (@id,@lat,@lng,@ca_categoria,@nom,@ca_autor)"
                        Comand.Parameters.AddWithValue("@ca_autor", JJ.Sesio.Usuari.Id)
                    Else
                        Comand.CommandText = "UPDATE Llocs"
                        Comand.CommandText += " SET lat=@lat,lng=@lng,ca_categoria=@ca_categoria,nom=@nom"
                        Comand.CommandText += " WHERE id=@id"
                    End If
                    Comand.Parameters.AddWithValue("@id", Me.Id)
                    Comand.Parameters.AddWithValue("@lat", Me._Posicio.Lat)
                    Comand.Parameters.AddWithValue("@lng", Me._Posicio.Lng)
                    Comand.Parameters.AddWithValue("@ca_categoria", Me._IdCategoria)
                    Comand.Parameters.AddWithValue("@nom", Me._Nom)
                    If Comand.ExecuteNonQuery() <> 1 Then
                        JJ.Registre.RegistrarErrada("No s'ha guardar cap registre a la taula Llocs")
                        Return False
                    End If
                    'Taula LlocsIdioma   
                    Dim ExistixIdioma As Boolean = Me.ExistixIdiomaLloc(Comand)
                    If Me.EsNou Or (Not ExistixIdioma) Then
                        Comand.Parameters.Clear()
                        Comand.CommandText = "INSERT INTO LlocsIdioma"
                        Comand.CommandText += " (ca_lloc,ca_idioma,descripcio_breu,descripcio,acces)"
                        Comand.CommandText += " VALUES"
                        Comand.CommandText += " (@ca_lloc,@ca_idioma,@descripcio_breu,@descripcio,@acces)"
                    Else
                        Comand.Parameters.Clear()
                        Comand.CommandText = "UPDATE LlocsIdioma"
                        Comand.CommandText += " SET descripcio_breu=@descripcio_breu,descripcio=@descripcio,acces=@acces"
                        Comand.CommandText += " WHERE ca_lloc=@ca_lloc AND ca_idioma=@ca_idioma"
                    End If
                    Comand.Parameters.AddWithValue("@ca_lloc", Me.Id)
                    Comand.Parameters.AddWithValue("@ca_idioma", Me.Idioma)
                    Comand.Parameters.AddWithValue("@descripcio_breu", Me._DescripcioBreu)
                    Comand.Parameters.AddWithValue("@descripcio", Me._Descripcio)
                    Comand.Parameters.AddWithValue("@acces", Me._Acces)
                    If Comand.ExecuteNonQuery() <> 1 Then
                        JJ.Registre.RegistrarErrada("No s'ha guardar cap registre a la taula LlocsIdioma. VERIFICAR SI AL MATEIX MOMENT ALGU HA CREAT AQUEST LLOC-IDIOMA QUANT NO EXISTIA")
                        Return False
                    End If
                    'Paraules clau
                    Comand.Parameters.Clear()
                    Comand.CommandText = "DELETE FROM Claus WHERE ca_lloc=@lloc AND ca_idioma=@idioma"
                    Comand.Parameters.AddWithValue("@lloc", Me.Id)
                    Comand.Parameters.AddWithValue("@idioma", Me.Idioma)
                    Comand.ExecuteNonQuery()
                    Dim Claus() As String = Split(Me._ParaulesClau, ",")
                    Dim DicClaus As New System.Collections.Hashtable()
                    For Each Clau As String In Claus
                        Dim ClauBD As String = Clau.Trim.ToUpper
                        If ClauBD <> "" Then
                            If Len(ClauBD) > 150 Then
                                ClauBD = ClauBD.Substring(0, 150)
                            End If
                            If Not DicClaus.ContainsKey(ClauBD) Then
                                DicClaus.Add(ClauBD, "")
                                Comand.Parameters.Clear()
                                Comand.CommandText = "INSERT INTO Claus (ca_lloc,ca_idioma,clau) VALUES (@lloc,@idioma,@clau)"
                                Comand.Parameters.AddWithValue("@lloc", Me.Id)
                                Comand.Parameters.AddWithValue("@idioma", Me.Idioma)
                                Comand.Parameters.AddWithValue("@clau", ClauBD)
                                Comand.ExecuteNonQuery()
                            End If
                        End If
                    Next
                    'Historic
                    If Not GuardarHistoric(Comand) Then
                        JJ.Registre.RegistrarAdvertencia("No s'ha pogut guardar l'històric")
                    End If
                    '****

                    Return True
                End If
            End Function

            Protected Overrides Function ObrirElement(Comand As SqlCommand, IdLloc As System.Guid, ByVal IdiomaLloc As String) As Boolean
                Dim Dades As SqlDataReader
                Try
                    'Lloc
                    Comand.Parameters.Clear()
                    Comand.CommandText = "SELECT * FROM Llocs WHERE id=@id"
                    Comand.Parameters.AddWithValue("@id", IdLloc)
                    Dades = Comand.ExecuteReader()
                    If Not Dades.Read Then
                        Dades.Close()
                        Throw New Exception("No s'ha trobat el lloc '" + IdLloc.ToString + "'")
                    Else
                        Me._Nom = Dades("nom")
                        Me._Posicio.Lat = Dades("lat")
                        Me._Posicio.Lng = Dades("lng")
                        Me._IdCategoria = Dades("ca_categoria")
                        Dades.Close()
                    End If
                    'Textos
                    Comand.Parameters.Clear()
                    Comand.CommandText = "SELECT * FROM LlocsIdioma"
                    Comand.CommandText += " WHERE ca_lloc=@lloc AND ca_idioma=@idioma"
                    Comand.Parameters.AddWithValue("@lloc", IdLloc)
                    Comand.Parameters.AddWithValue("@idioma", Idioma)
                    Dades = Comand.ExecuteReader()
                    If Not Dades.Read Then
                        Me._DescripcioBreu = ""
                        Me._Descripcio = ""
                        Me._Acces = ""
                    Else
                        Me._DescripcioBreu = Dades("descripcio_breu")
                        Me._Descripcio = Dades("descripcio")
                        Me._Acces = Dades("acces")
                    End If
                    Dades.Close()
                    'Sumem la visita
                    If Me._RegistrarVisita Then
                        Comand.Parameters.Clear()
                        Comand.CommandText = "UPDATE Llocs SET visites=visites+1 WHERE id=@id"
                        Comand.Parameters.AddWithValue("@id", IdLloc)
                        If Comand.ExecuteNonQuery() <> 1 Then
                            JJ.Registre.RegistrarAdvertencia("No s'ha pogut registrar la visita al lloc: " + Me.Id.ToString)
                        End If
                    End If


                    Return True
                Finally
                    Dades.Close()
                End Try
            End Function


            Public Shared Function GetImatges(ByVal IdLloc As Guid, ByVal Idioma As String) As List(Of Fotos.FotoClass)
                Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
                Dim Comand As New SqlCommand
                GetImatges = New List(Of Fotos.FotoClass)
                Try
                    Conexio.Open()
                    Comand.Connection = Conexio
                    Comand.CommandText = "SELECT * FROM Fotos F"
                    Comand.CommandText += " WHERE F.ca_lloc=@lloc AND habilitat=@habilitat"
                    Comand.Parameters.AddWithValue("@lloc", IdLloc)
                    Comand.Parameters.AddWithValue("@habilitat", True)
                    Dim Dades As SqlDataReader = Comand.ExecuteReader()
                    While Dades.Read
                        Dim Comentari As String = GetComentariImatge(Dades("id"), Idioma)
                        Dim Foto As New Fotos.FotoClass(Dades("id"), Dades("arxiu"), Dades("extensio"), Comentari)
                        GetImatges.Add(Foto)
                    End While
                    Dades.Close()
                Catch ex As Exception
                    Return New List(Of Fotos.FotoClass)
                Finally
                    Conexio.Close()
                End Try
            End Function


            Public Shared Function PermetreGuardar(ByVal Comand As SqlCommand, ByVal IdLloc As Guid) As Boolean
                'Edicio (a aquesta cridada sols hi ha edicio)
                If Not JJ.Config.General.Seguretat.PermetreEdicioLlocs() Then
                    Return False
                Else
                    Comand.Parameters.Clear()
                    Comand.CommandText = "SELECT editable FROM Llocs WHERE id=@id"
                    Comand.Parameters.AddWithValue("@id", IdLloc)
                    Dim Dades As SqlDataReader = Comand.ExecuteReader()
                    If Not Dades.Read Then
                        PermetreGuardar = False
                    Else
                        PermetreGuardar = Dades("editable")
                    End If
                    Dades.Close()
                End If
            End Function
            Public Shared Function PermetreGuardar(ByVal IdLloc As Guid) As Boolean
                'Edicio (a aquesta cridada sols hi ha edicio)
                Try
                    If Not JJ.Config.General.Seguretat.UsuariActiu() Or Not JJ.Config.General.Seguretat.PermetreEdicioLlocs() Then
                        Return False
                    Else
                        Return JJ.Intern.Funcions.BBDD.GetField("editable", "Llocs", "id", IdLloc)
                    End If
                Catch ex As Exception
                    JJ.Registre.RegistrarErrada(ex.ToString)
                    Return False
                End Try
            End Function


            Public Shared Function AfegirComentari(ByVal IdLloc As Guid, ByVal Idioma As String, ByVal Text As String) As Guid
                If Not PermetreGuardar(IdLloc) Then
                    Return Guid.Empty
                Else
                    Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
                    Dim Comand As New SqlCommand
                    Try
                        Conexio.Open()
                        Comand.Connection = Conexio
                        Comand.CommandText = "INSERT INTO OpinioLlocs"
                        Comand.CommandText += " (id,ca_lloc,ca_idioma,ca_usuari,data,text)"
                        Comand.CommandText += " VALUES"
                        Comand.CommandText += " (@id,@ca_lloc,@ca_idioma,@ca_usuari,GETDATE(),@text)"
                        Dim Id As Guid = Guid.NewGuid()
                        Comand.Parameters.AddWithValue("@id", Id)
                        Comand.Parameters.AddWithValue("@ca_lloc", IdLloc)
                        Comand.Parameters.AddWithValue("@ca_idioma", Idioma)
                        Comand.Parameters.AddWithValue("@ca_usuari", JJ.Sesio.Usuari.Id)
                        If Len(Text) > 1000 Then
                            Text = Text.Substring(0, 1000)
                        End If
                        Comand.Parameters.AddWithValue("@text", Text)
                        Comand.ExecuteNonQuery()
                        Return Id
                    Catch ex As Exception
                        JJ.Registre.RegistrarErrada(ex)
                        Return Guid.Empty
                    Finally
                        Conexio.Close()
                    End Try
                End If
            End Function

            Public Function GetComentaris(ByVal MostratTots As Boolean) As List(Of Comentaris.ComentariClass)
                Dim Comentaris As New List(Of Comentaris.ComentariClass)
                Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
                Dim Comand As New SqlCommand
                Try
                    Conexio.Open()
                    Comand.Connection = Conexio
                    Comand.CommandText = "SELECT O.id, O.data, O.text, O.habilitat, U.id id_usuari, U.nom, U.cognoms, U.alies, U.img, U.img_extensio FROM OpinioLlocs O"
                    Comand.CommandText += " JOIN Usuaris U ON O.ca_usuari=U.id"
                    Comand.CommandText += " WHERE ca_lloc=@lloc AND ca_idioma=@idioma"
                    If Not MostratTots Then
                        Comand.CommandText += " AND O.habilitat=@habilitat"
                        Comand.Parameters.AddWithValue("@habilitat", True)
                    End If
                    Comand.CommandText += " ORDER BY O.data DESC"
                    Comand.Parameters.AddWithValue("@lloc", Me.Id)
                    Comand.Parameters.AddWithValue("@idioma", Me.Idioma)
                    Dim Dades As SqlDataReader = Comand.ExecuteReader()
                    While Dades.Read
                        Comentaris.Add(New Comentaris.ComentariClass(Dades("id"), Dades("data"), Dades("text"), Dades("habilitat"), Dades("id_usuari"), Dades("nom"), Dades("cognoms"), Dades("alies"), Dades("img"), Dades("img_extensio")))
                    End While
                    Dades.Close()
                    Return Comentaris
                Catch ex As Exception
                    JJ.Registre.RegistrarErrada(ex)
                    Return Nothing
                Finally
                    Conexio.Close()
                End Try
            End Function

            Public Shared Function Esborrar(ByVal IdLloc As Guid) As Boolean
                Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
                Dim Comand As New SqlCommand
                Try
                    Conexio.Open()
                    Comand.Connection = Conexio
                    'Esborrem les fotos
                    Comand.CommandText = "SELECT * FROM Fotos F"
                    Comand.CommandText += " WHERE F.ca_lloc=@lloc"
                    Comand.Parameters.AddWithValue("@lloc", IdLloc)
                    Dim Dades As SqlDataReader = Comand.ExecuteReader()
                    While Dades.Read
                        'JJ.Registre.RegistrarEvent(System.Web.Hosting.HostingEnvironment.MapPath("/img/"))
                        Dim Vector() As String = CType(Dades("arxiu"), String).Split("\")                        
                        Dim SubCarpeta As String = Vector(Vector.Length - 3) + "\" + Vector(Vector.Length - 2)
                        Dim Carpeta As String = System.Web.Hosting.HostingEnvironment.MapPath("/MAPES/img/fotos/" + SubCarpeta)
                        Dim NomArxiu As String = Vector(Vector.Length - 1)
                        Dim Arxius() As String = System.IO.Directory.GetFiles(Carpeta, NomArxiu + "*" + Dades("extensio"))
                        For Each Arxiu As String In Arxius
                            Dim Img As New System.IO.FileInfo(Arxiu)
                            Img.Delete()
                        Next
                    End While
                    Dades.Close()
                    'Esborrem el lloc
                    Dim NomLloc As String = JJ.Intern.Funcions.BBDD.GetField("nom", "Llocs", "id", IdLloc)
                    Comand.Parameters.Clear()
                    Comand.CommandText = "DELETE FROM Llocs WHERE id=@id"
                    Comand.Parameters.AddWithValue("@id", IdLloc)
                    If Comand.ExecuteNonQuery() > 0 Then
                        JJ.Registre.RegistrarEvent("Lloc '" + NomLloc + "' esborrat")
                        Return True
                    Else
                        Return False
                    End If
                Catch ex As Exception
                    JJ.Registre.RegistrarErrada(ex.ToString)
                    Return False
                Finally
                    Conexio.Close()
                End Try
            End Function

            Public Shared Function GetIdLlocMesVisitat() As Guid
                Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
                Dim Comand As New SqlCommand
                Try
                    Conexio.Open()
                    Comand.Connection = Conexio
                    Comand.CommandText = "SELECT TOP 1 id FROM Llocs WHERE habilitat=@true ORDER BY visites DESC"
                    Comand.Parameters.AddWithValue("@true", True)
                    Dim Dades As SqlDataReader = Comand.ExecuteReader()
                    Dades.Read()
                    GetIdLlocMesVisitat = CType(Dades("id"), Guid)
                    Dades.Close()
                Catch ex As Exception
                    JJ.Registre.RegistrarErrada(ex.ToString)
                Finally
                    Conexio.Close()
                End Try
            End Function



            Private Shared Function GetComentariImatge(ByVal IdFoto As Guid, ByVal Idioma As String) As String
                Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
                Dim Comand As New SqlCommand
                Try
                    Conexio.Open()
                    Comand.Connection = Conexio
                    Comand.CommandText = "SELECT * FROM ComentariFoto WHERE ca_foto=@foto AND ca_idioma=@idioma"
                    Comand.Parameters.AddWithValue("@foto", IdFoto)
                    Comand.Parameters.AddWithValue("@idioma", Idioma)
                    Dim Dades As SqlDataReader = Comand.ExecuteReader()
                    If Not Dades.Read Then
                        GetComentariImatge = ""
                    Else
                        GetComentariImatge = Dades("comentari")
                    End If
                    Dades.Close()
                Catch ex As Exception
                    JJ.Registre.RegistrarErrada(ex)
                    Return ""
                Finally
                    Conexio.Close()
                End Try
            End Function


            Private Function PermetreGuardar(ByVal Comand As SqlCommand) As Boolean
                If Not JJ.Config.General.Seguretat.UsuariActiu Then
                    Return False
                Else
                    If Me.EsNou Then
                        'Nou
                        Return JJ.Config.General.Seguretat.PermetreCrearLlocsNous()
                    Else
                        'Edicio
                        If Not JJ.Config.General.Seguretat.PermetreEdicioLlocs() Then
                            Return False
                        Else
                            Comand.Parameters.Clear()
                            Comand.CommandText = "SELECT editable FROM Llocs WHERE id=@id"
                            Comand.Parameters.AddWithValue("@id", Me.Id)
                            Dim Dades As SqlDataReader = Comand.ExecuteReader()
                            If Not Dades.Read Then
                                PermetreGuardar = False
                            Else
                                PermetreGuardar = Dades("editable")
                            End If
                            Dades.Close()
                        End If
                    End If
                End If
            End Function


            Private Function ExistixIdiomaLloc(ByVal Comand As SqlCommand) As Boolean
                Comand.Parameters.Clear()
                Comand.CommandText = "SELECT COUNT(ca_idioma) FROM LlocsIdioma WHERE ca_lloc=@lloc AND ca_idioma=@idioma"
                Comand.Parameters.AddWithValue("@lloc", Me.Id)
                Comand.Parameters.AddWithValue("@idioma", Me.Idioma)
                Dim Dades As SqlDataReader = Comand.ExecuteReader()
                Dades.Read()
                ExistixIdiomaLloc = (Dades(0) > 0)
                Dades.Close()
            End Function

            Private Function GuardarHistoric(ByVal Comand As SqlCommand) As Boolean
                Try
                    'Historic
                    Dim IdHistoric As Guid = Guid.NewGuid()
                    Comand.Parameters.Clear()
                    Comand.CommandText = "INSERT INTO Historic (id,data,ca_usuari) VALUES (@id,GETDATE(),@id_usuari)"
                    Comand.Parameters.AddWithValue("@id", IdHistoric)
                    Comand.Parameters.AddWithValue("@id_usuari", JJ.Sesio.Usuari.Id)
                    Comand.ExecuteNonQuery()
                    'Lloc
                    Comand.Parameters.Clear()
                    Comand.CommandText = "INSERT INTO HistoricLlocs"
                    Comand.CommandText += " (ca_lloc,ca_historic,lat,lng,ca_categoria,nom)"
                    Comand.CommandText += " VALUES"
                    Comand.CommandText += " (@id_lloc,@id_historic,@lat,@lng,@ca_categoria,@nom)"
                    Comand.Parameters.AddWithValue("@id_lloc", Me.Id)
                    Comand.Parameters.AddWithValue("@id_historic", IdHistoric)
                    Comand.Parameters.AddWithValue("@lat", Me._Posicio.Lat)
                    Comand.Parameters.AddWithValue("@lng", Me._Posicio.Lng)
                    Comand.Parameters.AddWithValue("@ca_categoria", Me._IdCategoria)
                    Comand.Parameters.AddWithValue("@nom", Me._Nom)
                    Comand.ExecuteNonQuery()
                    'LlocsIdioma
                    Comand.Parameters.Clear()
                    Comand.CommandText = "INSERT INTO HistoricLlocsIdioma"
                    Comand.CommandText += " (ca_lloc,ca_historic,ca_idioma,descripcio_breu,descripcio,acces)"
                    Comand.CommandText += " VALUES"
                    Comand.CommandText += " (@id_lloc,@id_historic,@id_idioma,@descripcio_breu,@descripcio,@acces)"
                    Comand.Parameters.AddWithValue("@id_lloc", Me.Id)
                    Comand.Parameters.AddWithValue("@id_historic", IdHistoric)
                    Comand.Parameters.AddWithValue("@id_idioma", Me.Idioma)
                    Comand.Parameters.AddWithValue("@descripcio_breu", Me._DescripcioBreu)
                    Comand.Parameters.AddWithValue("@descripcio", Me._Descripcio)
                    Comand.Parameters.AddWithValue("@acces", Me._Acces)
                    Comand.ExecuteNonQuery()
                    'Paraules clau
                    Comand.Parameters.Clear()
                    Dim Claus() As String = Split(Me._ParaulesClau, ",")
                    Dim DicClaus As New System.Collections.Hashtable()
                    For Each Clau As String In Claus
                        Dim ClauBD As String = Clau.Trim.ToUpper
                        If ClauBD <> "" Then
                            If Not DicClaus.ContainsKey(ClauBD) Then
                                DicClaus.Add(ClauBD, "")
                                Comand.Parameters.Clear()
                                Comand.CommandText = "INSERT INTO HistoricClaus (ca_lloc,ca_idioma,ca_historic,clau) VALUES (@lloc,@idioma,@historic,@clau)"
                                Comand.Parameters.AddWithValue("@lloc", Me.Id)
                                Comand.Parameters.AddWithValue("@idioma", Me.Idioma)
                                Comand.Parameters.AddWithValue("@clau", ClauBD)
                                Comand.Parameters.AddWithValue("@historic", IdHistoric)
                                Comand.ExecuteNonQuery()
                            End If
                        End If
                    Next


                    Me.DetectarAtaques(Comand, IdHistoric)

                    Return True
                Catch ex As Exception
                    JJ.Registre.RegistrarErrada("Error guardant l'històric: " + ex.ToString)
                    Return False
                End Try
            End Function


            Private Sub DetectarAtaques(ByVal Comand As SqlCommand, ByVal IdHistoric As Guid)
                Dim HIdHistoric As Guid
                Dim HiHaHistoric As Boolean = False
                Dim HLat, HLng As Double
                Dim HNom, HDescripcioBreu, HDescripcio, HAcces, HClaus As String
                Dim HIdCategoria As Guid
                'Obtenim les dades del ultim historic
                Try
                    Comand.Parameters.Clear()
                    Comand.CommandText = "SELECT TOP 2 H.id,HL.lat,HL.lng,HL.nom,HL.ca_categoria,HLI.descripcio_breu,HLI.descripcio,HLI.acces FROM Historic H"
                    Comand.CommandText += " JOIN HistoricLlocs HL ON H.id=HL.ca_historic"
                    Comand.CommandText += " JOIN HistoricLlocsIdioma HLI ON HL.ca_lloc=HLI.ca_lloc AND HL.ca_historic=HLI.ca_historic"
                    Comand.CommandText += " WHERE HL.ca_lloc=@lloc AND HLI.ca_idioma=@idioma"
                    Comand.CommandText += " ORDER BY H.data DESC"
                    Comand.Parameters.AddWithValue("@lloc", Me.Id)
                    Comand.Parameters.AddWithValue("@idioma", Me.Idioma)
                    Dim Dades As SqlDataReader = Comand.ExecuteReader()
                    If Dades.Read Then
                        If Dades.Read Then
                            'L'ultim historic es el que s'acaba de gravar
                            HiHaHistoric = True
                            HIdHistoric = Dades("id")
                            HLat = Dades("lat")
                            HLng = Dades("lng")
                            HNom = Dades("nom")
                            HDescripcioBreu = Dades("descripcio_breu")
                            HDescripcio = Dades("descripcio")
                            HAcces = Dades("acces")
                            HIdCategoria = Dades("ca_categoria")
                        End If
                    End If
                    Dades.Close()
                    If HiHaHistoric Then
                        HClaus = GetParaulesClau(Me.Id, Me.Idioma, HIdHistoric)
                    End If
                Catch ex As Exception
                    JJ.Registre.RegistrarErrada(ex.ToString)
                End Try
                'Comparem els camvits
                If HiHaHistoric Then
                    Dim AlertaPosicio As Boolean = JJ.Seguretat.ControlHistoric.AlertaDouble(Me._Posicio.Lat, HLat) Or JJ.Seguretat.ControlHistoric.AlertaDouble(Me._Posicio.Lng, HLng)
                    Dim AlertaNom As Boolean = JJ.Seguretat.ControlHistoric.AlertaString(Me._Nom, HNom)
                    Dim AlertaDescripcioBreu As Boolean = JJ.Seguretat.ControlHistoric.AlertaString(Me._DescripcioBreu, HDescripcioBreu)
                    Dim AlertaDescripcio As Boolean = JJ.Seguretat.ControlHistoric.AlertaString(Me._Descripcio, HDescripcio)
                    Dim AlertaAcces As Boolean = JJ.Seguretat.ControlHistoric.AlertaString(Me._Acces, HAcces)
                    Dim AlertaClaus As Boolean = JJ.Seguretat.ControlHistoric.AlertaString(Me._ParaulesClau, HClaus)
                    Dim AlertaCategoria As Boolean = (Me._IdCategoria.ToString <> HIdCategoria.ToString)
                    If AlertaPosicio Or AlertaNom Or AlertaDescripcioBreu Or AlertaDescripcio Or AlertaAcces Or AlertaClaus Then
                        Comand.Parameters.Clear()
                        Comand.CommandText = "INSERT INTO AlertesAtacs"
                        Comand.CommandText += " (ca_lloc,ca_historic,ca_idioma,ca_usuari,data,alerta_posicio,alerta_nom,alerta_descripcio_breu,alerta_descripcio,alerta_acces,alerta_claus,alerta_categoria)"
                        Comand.CommandText += " VALUES"
                        Comand.CommandText += " (@ca_lloc,@ca_historic,@ca_idioma,@ca_usuari,GETDATE(),@alerta_posicio,@alerta_nom,@alerta_descripcio_breu,@alerta_descripcio,@alerta_acces,@alerta_claus,@alerta_categoria)"
                        Comand.Parameters.AddWithValue("@ca_lloc", Me.Id)
                        Comand.Parameters.AddWithValue("@ca_historic", IdHistoric)
                        Comand.Parameters.AddWithValue("@ca_idioma", Me.Idioma)
                        Comand.Parameters.AddWithValue("@ca_usuari", JJ.Sesio.Usuari.Id)
                        Comand.Parameters.AddWithValue("@alerta_posicio", AlertaPosicio)
                        Comand.Parameters.AddWithValue("@alerta_nom", AlertaNom)
                        Comand.Parameters.AddWithValue("@alerta_descripcio_breu", AlertaDescripcioBreu)
                        Comand.Parameters.AddWithValue("@alerta_descripcio", AlertaDescripcio)
                        Comand.Parameters.AddWithValue("@alerta_acces", AlertaAcces)
                        Comand.Parameters.AddWithValue("@alerta_claus", AlertaClaus)
                        Comand.Parameters.AddWithValue("@alerta_categoria", AlertaCategoria)
                        Comand.ExecuteNonQuery()
                    End If
                End If
            End Sub





            Private Shared Function GetParaulesClau(ByVal IdLloc As Guid, ByVal Idioma As String) As String
                Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
                Dim Comando As New SqlCommand
                Try
                    Conexio.Open()
                    Comando.Connection = Conexio
                    Comando.CommandText = "SELECT clau FROM Claus WHERE ca_idioma=@idioma AND ca_lloc=@lloc"
                    Comando.Parameters.AddWithValue("@lloc", IdLloc)
                    Comando.Parameters.AddWithValue("@idioma", Idioma)
                    Dim Dades As SqlDataReader = Comando.ExecuteReader()
                    GetParaulesClau = ""
                    While Dades.Read
                        GetParaulesClau += ", " + Dades("clau")
                    End While
                    Dades.Close()
                    If GetParaulesClau <> "" Then
                        GetParaulesClau = GetParaulesClau.Substring(2)
                    End If
                Catch ex As Exception
                    JJ.Registre.RegistrarErrada(ex)
                    Return ""
                Finally
                    Conexio.Close()
                End Try
            End Function


            Private Shared Function GetParaulesClau(ByVal IdLloc As Guid, ByVal Idioma As String, ByVal IdHistoric As Guid) As String
                Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
                Dim Comando As New SqlCommand
                Try
                    Conexio.Open()
                    Comando.Connection = Conexio
                    Comando.CommandText = "SELECT clau FROM HistoricClaus WHERE ca_idioma=@idioma AND ca_lloc=@lloc AND ca_historic=@historic"
                    Comando.Parameters.AddWithValue("@lloc", IdLloc)
                    Comando.Parameters.AddWithValue("@idioma", Idioma)
                    Comando.Parameters.AddWithValue("@historic", IdHistoric)
                    Dim Dades As SqlDataReader = Comando.ExecuteReader()
                    GetParaulesClau = ""
                    While Dades.Read
                        GetParaulesClau += ", " + Dades("clau")
                    End While
                    Dades.Close()
                    If GetParaulesClau <> "" Then
                        GetParaulesClau = GetParaulesClau.Substring(2)
                    End If
                Catch ex As Exception
                    JJ.Registre.RegistrarErrada(ex)
                    Return ""
                Finally
                    Conexio.Close()
                End Try
            End Function
        End Class



        Public Class LlocReadOnlyClass
            Dim _Id, _IdCategoria As Guid
            Dim _Posicio As PosicioClass
            Dim _Nom, _DescripcioBreu As String
            Dim _FotoPortada As String

            Public Sub New(ByVal Id As Guid, ByVal Lat As Double, ByVal Lng As Double, ByVal IdCategoria As Guid, ByVal Nom As String, ByVal DescripcioBreu As String)
                Me._Id = Id
                Me._Posicio = New PosicioClass(Lat, Lng)
                Me._IdCategoria = IdCategoria
                Me._Nom = Nom
                Me._DescripcioBreu = DescripcioBreu
            End Sub


            Public Sub New(ByVal Id As Guid, ByVal Lat As Double, ByVal Lng As Double, ByVal IdCategoria As Guid, ByVal Nom As String, ByVal DescripcioBreu As String, ByVal Arxiu As String)
                Me.New(Id, Lat, Lng, IdCategoria, Nom, DescripcioBreu)
                Try
                    Arxiu = Arxiu.Replace("\", "/")
                    Arxiu = Arxiu.Substring(Arxiu.IndexOf("/img/"))
                    Me._FotoPortada = Arxiu
                Catch ex As Exception

                End Try
            End Sub


            Public Sub New(ByVal Id As Guid, ByVal Lat As Double, ByVal Lng As Double, ByVal IdCategoria As Guid, ByVal Nom As String, ByVal DescripcioBreu As String, ByVal Arxiu As String, ByVal Extensio As String)
                Me.New(Id, Lat, Lng, IdCategoria, Nom, DescripcioBreu)
                Try
                    Arxiu = Arxiu.Replace("\", "/")
                    Arxiu = Arxiu.Substring(Arxiu.IndexOf("/img/"))
                    Me._FotoPortada = Arxiu + "_200" + Extensio
                Catch ex As Exception

                End Try
            End Sub



            Public ReadOnly Property Id As Guid
                Get
                    Return Me._Id
                End Get
            End Property

            Public ReadOnly Property Posicio As PosicioClass
                Get
                    Return Me._Posicio
                End Get
            End Property

            Public ReadOnly Property IdCategoria As Guid
                Get
                    Return Me._IdCategoria
                End Get
            End Property

            Public ReadOnly Property Nom As String
                Get
                    Return Me._Nom
                End Get
            End Property

            Public ReadOnly Property DescripcioBreu As String
                Get
                    Return Me._DescripcioBreu
                End Get
            End Property

            Public ReadOnly Property FotoPortada() As String
                Get
                    Return Me._FotoPortada
                End Get
            End Property

        End Class



        Namespace Comentaris

            Public Class ComentariClass
                Dim _Id As Guid
                Dim _Data As Date
                Dim _Usuari As JJ.Usuaris.UsuariDadesClass
                Dim _Text As String
                Dim _Habilitat As Boolean

                Public Sub New(ByVal Id As Guid, ByVal Data As Date, ByVal Text As String, ByVal Habilitat As Boolean, ByVal IdUsuari As Guid, ByVal NomUsuari As String, ByVal CognomsUsuari As String, ByVal AliesUsuari As String, ByVal FotoBase As String, ByVal Extensio As String)
                    Me._Id = Id
                    Me._Data = Data
                    Me._Text = Text
                    Me._Habilitat = Habilitat
                    Me._Usuari = New JJ.Usuaris.UsuariDadesClass(IdUsuari, AliesUsuari, NomUsuari, CognomsUsuari, FotoBase, Extensio)
                End Sub

                Public ReadOnly Property Id As Guid
                    Get
                        Return Me._Id
                    End Get
                End Property
                Public ReadOnly Property Data As Date
                    Get
                        Return Me._Data
                    End Get
                End Property
                Public ReadOnly Property Usuari() As JJ.Usuaris.UsuariDadesClass
                    Get
                        Return Me._Usuari
                    End Get
                End Property
                Public ReadOnly Property Text As String
                    Get
                        Return Me._Text
                    End Get
                End Property
                Public ReadOnly Property Habilitat As Boolean
                    Get
                        Return Me._Habilitat
                    End Get
                End Property

            End Class





        End Namespace



        Namespace Fotos


            Public Class FotoClass
                Dim _Id As Guid
                Dim _Arxiu, _Extensio, _Comentari As String
                Dim _TitolLloc As String

                Public Sub New(ByVal Id As Guid, ByVal Idioma As String)
                    Me._Id = Id
                    Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
                    Dim Comand As New SqlCommand
                    Try
                        Conexio.Open()
                        Comand.Connection = Conexio
                        Comand.CommandText = "SELECT F.arxiu, F.extensio, CF.comentari,L.nom FROM Fotos F"
                        Comand.CommandText += " LEFT JOIN ComentariFoto CF ON F.id=CF.ca_foto AND CF.ca_idioma=@idioma"
                        Comand.CommandText += " LEFT JOIN Llocs L ON F.ca_lloc=L.id"
                        Comand.CommandText += " WHERE F.id=@foto"
                        Comand.Parameters.AddWithValue("@foto", Id)
                        Comand.Parameters.AddWithValue("@idioma", Idioma)
                        Dim Dades As SqlDataReader = Comand.ExecuteReader()
                        If Not Dades.Read Then
                            Me._Arxiu = ""
                            Me._Extensio = ""
                            Me._Comentari = ""
                            Me._TitolLloc = ""
                            Dades.Close()
                        Else
                            Me._Arxiu = Dades("arxiu")
                            Me._Extensio = Dades("extensio")
                            If Dades("comentari") Is System.DBNull.Value Then
                                Me._Comentari = ""
                            Else
                                Me._Comentari = Dades("comentari")
                            End If
                            Me._TitolLloc = Dades("nom")
                            Dades.Close()
                            Me.ContarVisita(Comand)
                        End If
                    Catch ex As Exception
                        JJ.Registre.RegistrarErrada(ex)
                    Finally
                        Conexio.Close()
                    End Try
                End Sub

                Public Sub New(ByVal Id As Guid, ByVal Arxiu As String, ByVal Extensio As String, ByVal Comentari As String)
                    Me._Id = Id
                    Me._Arxiu = Arxiu
                    Me._Extensio = Extensio
                    Me._Comentari = Comentari
                End Sub

                Public ReadOnly Property Id() As Guid
                    Get
                        Return Me._Id
                    End Get
                End Property

                Public ReadOnly Property Arxiu As String
                    Get
                        Return Me._Arxiu
                    End Get
                End Property

                Public ReadOnly Property Extensio As String
                    Get
                        Return Me._Extensio
                    End Get
                End Property

                Public ReadOnly Property Comentari As String
                    Get
                        Return Me._Comentari
                    End Get
                End Property

                Public ReadOnly Property TitolLloc As String
                    Get
                        Return Me._TitolLloc
                    End Get
                End Property

                Public ReadOnly Property Autor() As JJ.Usuaris.UsuariDadesClass
                    Get
                        Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
                        Dim Comand As New SqlCommand
                        Try
                            Conexio.Open()
                            Comand.Connection = Conexio
                            Comand.CommandText = "SELECT U.id,U.nom,U.cognoms,U.alies,U.img,U.img_extensio FROM Fotos F"
                            Comand.CommandText += " JOIN Usuaris U ON F.ca_autor=U.id"
                            Comand.CommandText += " WHERE F.id=@foto"
                            Comand.Parameters.AddWithValue("@foto", Me.Id)
                            Dim Dades As SqlDataReader = Comand.ExecuteReader()
                            If Not Dades.Read Then
                                Return New JJ.Usuaris.UsuariDadesClass()
                            Else
                                Return New JJ.Usuaris.UsuariDadesClass(Dades("id"), Dades("alies"), Dades("nom"), Dades("cognoms"), Dades("img"), Dades("img_extensio"))
                            End If
                        Catch ex As Exception
                            JJ.Registre.RegistrarErrada(ex)
                            Return New JJ.Usuaris.UsuariDadesClass()
                        Finally
                            Conexio.Close()
                        End Try
                    End Get
                End Property

                Public Shared Function GetURLFoto(ByVal IdFoto As Guid, ByVal Tamany As Integer) As String
                    'Obtenemos los datos de la foto
                    Dim __Arxiu As String = ""
                    Dim __Extensio As String = ""
                    Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
                    Dim Comand As New SqlCommand
                    Try
                        Conexio.Open()
                        Comand.Connection = Conexio
                        Comand.CommandText = "SELECT arxiu, extensio FROM Fotos WHERE id=@id"
                        Comand.Parameters.AddWithValue("@id", IdFoto)
                        Dim Dades As SqlDataReader = Comand.ExecuteReader()
                        If Dades.Read Then
                            __Arxiu = Dades("arxiu")
                            __Extensio = Dades("extensio")
                        End If
                        Dades.Close()
                    Catch ex As Exception
                        JJ.Registre.RegistrarErrada(ex.ToString)
                    Finally
                        Conexio.Close()
                    End Try
                    'montamos la URL
                    If __Arxiu = "" Or __Extensio = "" Then
                        Return ""
                    Else
                        Return JJ.Config.General.Web.GetURL(__Arxiu.Substring(__Arxiu.IndexOf("\img\") + 1)) + "_" + Tamany.ToString + __Extensio
                    End If
                End Function

                Public Shared Function GetComentari(ByVal IdFoto As Guid, ByVal Idioma As String) As String
                    Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
                    Dim Comand As New SqlCommand
                    Try
                        Conexio.Open()
                        Comand.Connection = Conexio
                        Comand.CommandText = "SELECT comentari FROM ComentariFoto WHERE ca_foto=@foto AND ca_idioma=@idioma"
                        Comand.Parameters.AddWithValue("@foto", IdFoto)
                        Comand.Parameters.AddWithValue("@idioma", Idioma)
                        Dim Dades As SqlDataReader = Comand.ExecuteReader()
                        If Not Dades.Read Then
                            GetComentari = ""
                        Else
                            GetComentari = Dades("comentari")
                        End If
                        Dades.Close()
                    Catch ex As Exception
                        JJ.Registre.RegistrarErrada(ex)
                        Return ""
                    Finally
                        Conexio.Close()
                    End Try
                End Function

                Public Shared Function SetComentari(ByVal IdFoto As Guid, ByVal Idioma As String, ByVal NouComentari As String) As Boolean
                    Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
                    Dim Comand As New SqlCommand
                    Try
                        Conexio.Open()
                        Comand.Connection = Conexio
                        If Not PermetreGuardar(Comand, IdFoto) Then
                            Return False
                        Else
                            'Mirem si hi ha un comentari
                            Comand.Parameters.Clear()
                            Comand.CommandText = "SELECT ca_foto FROM ComentariFoto WHERE ca_foto=@foto AND ca_idioma=@idioma"
                            Comand.Parameters.AddWithValue("@foto", IdFoto)
                            Comand.Parameters.AddWithValue("@idioma", Idioma)
                            Dim Dades As SqlDataReader = Comand.ExecuteReader()
                            Dim HiHaComentari As Boolean = Dades.Read
                            Dades.Close()
                            'Guardem el comentari
                            Comand.Parameters.Clear()
                            If Not HiHaComentari Then
                                Comand.CommandText = "INSERT INTO ComentariFoto (ca_foto,ca_idioma,comentari) VALUES (@foto,@idioma,@comentari)"
                            Else
                                Comand.CommandText = "UPDATE ComentariFoto SET comentari=@comentari WHERE ca_foto=@foto AND ca_idioma=@idioma"
                            End If
                            Comand.Parameters.AddWithValue("@foto", IdFoto)
                            Comand.Parameters.AddWithValue("@idioma", Idioma)
                            Comand.Parameters.AddWithValue("@comentari", NouComentari)
                            If Len(NouComentari) > 500 Then
                                NouComentari = NouComentari.Substring(0, 500)
                            End If
                            If Comand.ExecuteNonQuery() = 0 Then
                                Return False
                            Else
                                If GuardarHistoricComentari(Comand, IdFoto, Idioma, NouComentari) Then
                                    Return True
                                Else
                                    JJ.Registre.RegistrarAdvertencia("No s'ha pogut guardar al historic els camvis del comentari de foto")
                                    Return False
                                End If
                            End If
                        End If
                    Catch ex As Exception
                        JJ.Registre.RegistrarErrada(ex)
                        Return False
                    Finally
                        Conexio.Close()
                    End Try
                End Function


                Public Shared Function PermetreGuardar(ByVal Comand As SqlCommand, ByVal IdFoto As Guid) As Boolean
                    Comand.CommandText = "SELECT editable FROM Llocs L"
                    Comand.CommandText += " JOIN Fotos F ON L.id=F.ca_lloc"
                    Comand.CommandText += " WHERE F.id=@foto"
                    Comand.Parameters.AddWithValue("@foto", IdFoto)
                    Dim Dades As SqlDataReader = Comand.ExecuteReader()
                    If Not Dades.Read Then
                        Return False
                    Else
                        PermetreGuardar = Dades("editable")
                    End If
                    Dades.Close()
                End Function


                Public Shared Function GetHistoric(ByVal IdFoto As Guid) As List(Of JJ.DadesBase.Historic.HistoricClass)
                    Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
                    Dim Comand As New SqlCommand
                    Try
                        GetHistoric = New List(Of JJ.DadesBase.Historic.HistoricClass)
                        Conexio.Open()
                        Comand.Connection = Conexio
                        Comand.CommandText = "SELECT U.id,U.nom,U.cognoms,U.alies,H.data,U.img, U.img_extensio FROM HistoricComentariFoto H"
                        Comand.CommandText += " JOIN Usuaris U ON H.ca_usuari=U.id"
                        Comand.CommandText += " WHERE H.ca_foto=@foto"
                        Comand.CommandText += " ORDER BY H.data"
                        Comand.Parameters.AddWithValue("@foto", IdFoto)
                        Dim Dades As SqlDataReader = Comand.ExecuteReader()
                        While Dades.Read
                            Dim Usuari As New JJ.Usuaris.UsuariDadesClass(Dades("id"), Dades("alies"), Dades("nom"), Dades("cognoms"), Dades("img"), Dades("img_extensio"))
                            Dim Item As New JJ.DadesBase.Historic.HistoricClass(Dades("data"), Usuari)
                            GetHistoric.Add(Item)
                        End While
                        Dades.Close()
                    Catch ex As Exception
                        JJ.Registre.RegistrarErrada(ex)
                        GetHistoric = Nothing
                    Finally
                        Conexio.Close()
                    End Try
                End Function


                Public Shared Function GuardarHistoricComentari(ByVal Comand As SqlCommand, ByVal IdFoto As Guid, ByVal Idioma As String, ByVal Comentari As String) As Boolean
                    Try
                        Comand.Parameters.Clear()
                        Comand.CommandText = "INSERT INTO HistoricComentariFoto"
                        Comand.CommandText += " (ca_foto,ca_idioma,ca_usuari,data,comentari)"
                        Comand.CommandText += " VALUES"
                        Comand.CommandText += " (@foto,@idioma,@usuari,GETDATE(),@comentari)"
                        Comand.Parameters.AddWithValue("@foto", IdFoto)
                        Comand.Parameters.AddWithValue("@idioma", Idioma)
                        Comand.Parameters.AddWithValue("@usuari", JJ.Sesio.Usuari.Id)
                        Comand.Parameters.AddWithValue("@comentari", Comentari)
                        Return (Comand.ExecuteNonQuery() = 1)
                    Catch ex As Exception
                        JJ.Registre.RegistrarAdvertencia("Errada al guardar el historic del comentaris de fotos: " + ex.ToString)
                        Return False
                    End Try
                End Function

                Public Shared Function RestituirVersio(ByVal IdHistoric As Guid) As Boolean
                    Dim __IdFoto As Guid = Guid.Empty
                    Dim __Idioma As String = ""
                    Dim __Comentari As String = ""
                    Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
                    Dim Comand As New SqlCommand
                    Try
                        Conexio.Open()
                        Comand.Connection = Conexio
                        Comand.CommandText = "SELECT ca_foto,ca_idioma,comentari FROM HistoricComentariFoto WHERE id=@id"
                        Comand.Parameters.AddWithValue("@id", IdHistoric)
                        Dim Dades As SqlDataReader = Comand.ExecuteReader()
                        If Dades.Read Then
                            __IdFoto = Dades("ca_foto")
                            __Idioma = Dades("ca_idioma")
                            __Comentari = Dades("comentari")
                        End If
                        Dades.Close()
                    Catch ex As Exception
                        JJ.Registre.RegistrarErrada(ex.ToString)
                    Finally
                        Conexio.Close()
                    End Try
                    If __IdFoto = Guid.Empty Or __Idioma = "" Then
                        Return False
                    Else
                        Return SetComentari(__IdFoto, __Idioma, __Comentari)
                    End If
                End Function

                Public Shared Function Esborrar(ByVal IdFoto As Guid) As Boolean
                    Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
                    Dim Comand As New SqlCommand
                    Dim NomArxiu As String = ""
                    Try                        
                        Conexio.Open()
                        Comand.Connection = Conexio
                        'Esborrem les fotos
                        Comand.CommandText = "SELECT * FROM Fotos F"
                        Comand.CommandText += " WHERE F.id=@foto"
                        Comand.Parameters.AddWithValue("@foto", IdFoto)
                        Dim Dades As SqlDataReader = Comand.ExecuteReader()
                        If Dades.Read Then
                            'JJ.Registre.RegistrarEvent(System.Web.Hosting.HostingEnvironment.MapPath("/img/"))
                            Dim Vector() As String = CType(Dades("arxiu"), String).Split("\")
                            Dim SubCarpeta As String = Vector(Vector.Length - 3) + "\" + Vector(Vector.Length - 2)
                            Dim Carpeta As String = System.Web.Hosting.HostingEnvironment.MapPath("/MAPES/img/fotos/" + SubCarpeta)
                            NomArxiu = Vector(Vector.Length - 1)
                            Dim Arxius() As String = System.IO.Directory.GetFiles(Carpeta, NomArxiu + "*" + Dades("extensio"))
                            For Each Arxiu As String In Arxius
                                Dim Img As New System.IO.FileInfo(Arxiu)
                                Img.Delete()
                            Next
                        End If
                        Dades.Close()
                        'Esborrem la foto                       
                        Comand.Parameters.Clear()
                        Comand.CommandText = "DELETE FROM Fotos WHERE id=@id"
                        Comand.Parameters.AddWithValue("@id", IdFoto)
                        If Comand.ExecuteNonQuery() > 0 Then
                            JJ.Registre.RegistrarEvent("Foto '" + NomArxiu + "' esborrada")
                            Return True
                        Else
                            Return False
                        End If
                    Catch ex As Exception
                        JJ.Registre.RegistrarErrada(ex.ToString)
                        Return False
                    Finally
                        Conexio.Close()
                    End Try
                End Function

                Public Shared Function GetURLImatge(ByVal RutaImgLocal As Object) As String
                    If RutaImgLocal Is System.DBNull.Value Then
                        Return ""
                    Else
                        Dim Ruta As String = CType(RutaImgLocal, String)
                        Ruta = Ruta.Replace("\", "/")
                        Return Ruta.Substring(Ruta.IndexOf("/img/"))
                    End If
                End Function

                Private Sub ContarVisita(ByVal Comand As SqlCommand)
                    Try
                        Comand.Parameters.Clear()
                        Comand.CommandText = "UPDATE Fotos SET visites=visites+1 WHERE id=@id"
                        Comand.Parameters.AddWithValue("@id", Me._Id)
                        Comand.ExecuteNonQuery()
                    Catch ex As Exception
                        JJ.Registre.RegistrarErrada(ex)
                    End Try
                End Sub

            End Class


        End Namespace


    End Namespace



End Namespace
