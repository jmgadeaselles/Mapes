Imports Microsoft.VisualBasic
Imports System.Data.SqlClient


Namespace JJ.Usuaris

    Public Enum TipusUsuari
        Administrador = 0
        Usuari = 1
    End Enum

    Public Class UsuariClass
        Dim _Id As Guid
        Dim _Mail, _Alies, _Contrasenya, _Nom, _Cognoms As String
        Dim _Tipus As TipusUsuari
        Dim _DataNaiximent As Date
        Dim _IdPais As String
        Dim _Regio, _Localitat, _CP As String
        Dim _Telefon As String
        Dim _Actiu, _Validat As Boolean
        Dim _Nou As Boolean

        Public Sub New()
            Me._Id = Guid.NewGuid()
            Me.Clear()
            Me._Nou = True
        End Sub

        Public Sub Clear()
            Me._Mail = ""
            Me._Alies = ""
            Me._Nom = ""
            Me._Cognoms = ""
            Me._Tipus = TipusUsuari.Usuari
            Me._DataNaiximent = Date.MinValue
            Me._IdPais = ""
            Me._Regio = ""
            Me._Localitat = ""
            Me._CP = ""
            Me._Telefon = ""
            Me._Actiu = True
            Me._Validat = Not JJ.Config.General.Usuaris.ValidarMailAlAlta
        End Sub

        Public ReadOnly Property Id() As Guid
            Get
                Return Me._Id
            End Get
        End Property
        Public Property Mail As String
            Get
                Return Me._Mail
            End Get
            Set(value As String)
                Me._Mail = value
            End Set
        End Property
        Public Property Alies As String
            Get
                Return Me._Alies
            End Get
            Set(value As String)
                Me._Alies = value
            End Set
        End Property
        Public WriteOnly Property Contrasenya As String
            Set(value As String)
                Me._Contrasenya = value
            End Set
        End Property
        Public Property Nom As String
            Get
                Return Me._Nom
            End Get
            Set(value As String)
                Me._Nom = value
            End Set
        End Property
        Public Property Cognoms As String
            Get
                Return Me._Cognoms
            End Get
            Set(value As String)
                Me._Cognoms = value
            End Set
        End Property
        Public Property Tipus As TipusUsuari
            Get
                Return Me._Tipus
            End Get
            Set(value As TipusUsuari)
                Me._Tipus = value
            End Set
        End Property
        Public Property DataNaixinent As Date
            Get
                Return Me._DataNaiximent
            End Get
            Set(value As Date)
                Me._DataNaiximent = value
            End Set
        End Property
        Public Property IdPais As String
            Get
                Return Me._IdPais
            End Get
            Set(value As String)
                Me._IdPais = value
            End Set
        End Property
        Public Property Regio As String
            Get
                Return Me._Regio
            End Get
            Set(value As String)
                Me._Regio = value
            End Set
        End Property
        Public Property Localitat As String
            Get
                Return Me._Localitat
            End Get
            Set(value As String)
                Me._Localitat = value
            End Set
        End Property
        Public Property CP As String
            Get
                Return Me._CP
            End Get
            Set(value As String)
                Me._CP = value
            End Set
        End Property
        Public Property Telefon As String
            Get
                Return Me._Telefon
            End Get
            Set(value As String)
                Me._Telefon = value
            End Set
        End Property
        Public Property Actiu As Boolean
            Get
                Return Me._Actiu
            End Get
            Set(value As Boolean)
                Me._Actiu = value
            End Set
        End Property
        Public ReadOnly Property Validat As Boolean
            Get
                Return Me._Validat
            End Get
        End Property

        Public Function Load(ByVal IdUsuari As Guid) As Boolean
            Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
            Dim Comand As New SqlCommand
            Try
                Conexio.Open()
                Comand.Connection = Conexio
                Comand.CommandText = "SELECT * FROM Usuaris WHERE id=@id"
                Comand.Parameters.AddWithValue("@id", IdUsuari)
                Dim Dades As SqlDataReader = Comand.ExecuteReader()
                If Not Dades.Read Then
                    Throw New Exception("El usuari " + IdUsuari.ToString + " no s'ha trobat")
                Else
                    Me._Alies = Dades("alies")
                    Me._Mail = Dades("mail")
                    Me._Nom = Dades("nom")
                    Me._Cognoms = Dades("cognoms")
                    Me._DataNaiximent = Dades("data_naiximent")
                    Me._IdPais = Dades("ca_pais")
                    Me._Regio = Dades("regio")
                    Me._CP = Dades("cp")
                    Me._Localitat = Dades("localitat")
                    Me._Telefon = Dades("telefon")
                    Me._Tipus = Dades("tipus")
                    Me._Validat = Dades("validat")


                End If
                Dades.Close()
                Return True
            Catch ex As Exception
                JJ.Registre.RegistrarErrada(ex)
                Return False
            Finally
                Conexio.Close()
            End Try
        End Function


        Public Function Save() As Boolean
            If Me._Alies.Length > 50 Then
                Me._Alies = Me._Alies.Substring(0, 50)
            End If
            If Me._Mail.Length > 320 Then
                Me._Mail = Me._Mail.Substring(0, 320)
            End If
            If Me._Contrasenya.Length > 20 Then
                Me._Contrasenya = Me._Contrasenya.Substring(0, 20)
            End If
            If Me._Nom.Length > 50 Then
                Me._Nom = Me._Nom.Substring(0, 50)
            End If
            If Me._Cognoms.Length > 50 Then
                Me._Cognoms = Me._Cognoms.Substring(0, 50)
            End If
            If Me._Regio.Length > 50 Then
                Me._Regio = Me._Regio.Substring(0, 50)
            End If
            If Me._Localitat.Length > 50 Then
                Me._Localitat = Me._Localitat.Substring(0, 50)
            End If
            If Me._CP.Length > 10 Then
                Me._CP = Me._CP.Substring(0, 10)
            End If
            If Me._Telefon.Length > 30 Then
                Me._Telefon = Me._Telefon.Substring(0, 30)
            End If
            Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
            Dim Comand As New SqlCommand
            Try
                Conexio.Open()
                Comand.Connection = Conexio
                If Me._Nou Then
                    Comand.CommandText = "INSERT INTO Usuaris"
                    Comand.CommandText += " (id,alies,mail,contrasenya,tipus,nom,cognoms,data_naiximent,ca_pais,regio,localitat,cp,telefon,actiu,validat)"
                    Comand.CommandText += " VALUES"
                    Comand.CommandText += " (@id,@alies,@mail,@contrasenya,@tipus,@nom,@cognoms,@data_naiximent,@ca_pais,@regio,@localitat,@cp,@telefon,@actiu,@validat)"
                    Comand.Parameters.AddWithValue("@validat", Me._Validat)
                Else
                    Comand.CommandText = "UPDATE Usuaris"
                    Comand.CommandText += " SET alies=@alies,mail=@mail,contrasenya=@contrasenya,tipus=@tipus,nom=@nom,cognoms=@cognoms,data_naiximent=@data_naiximent,ca_pais=@ca_pais,regio=@regio,localitat=@localitat,cp=@cp,telefon=@telefon,actiu=@actiu"
                    Comand.CommandText += " WHERE id=@id"
                End If
                Comand.Parameters.AddWithValue("@id", Me._Id)
                Comand.Parameters.AddWithValue("@alies", Me._Alies)
                Comand.Parameters.AddWithValue("@mail", Me._Mail)
                Comand.Parameters.AddWithValue("@contrasenya", Me._Contrasenya)
                Comand.Parameters.AddWithValue("@tipus", Me._Tipus)
                Comand.Parameters.AddWithValue("@nom", Me._Nom)
                Comand.Parameters.AddWithValue("@cognoms", Me._Cognoms)
                Comand.Parameters.AddWithValue("@data_naiximent", Me._DataNaiximent)
                Comand.Parameters.AddWithValue("@ca_pais", Me._IdPais)
                Comand.Parameters.AddWithValue("@regio", Me._Regio)
                Comand.Parameters.AddWithValue("@localitat", Me._Localitat)
                Comand.Parameters.AddWithValue("@cp", Me._CP)
                Comand.Parameters.AddWithValue("@telefon", Me._Telefon)
                Comand.Parameters.AddWithValue("@actiu", Me._Actiu)
                Comand.ExecuteNonQuery()
                If Not Me._Validat Then
                    Dim ClauActivacio As String = Me._Id.ToString.Substring(0, 8).ToUpper
                    Dim URL As String = JJ.Config.General.Web.GetURL("/usr/Alta.aspx?m=" + Me._Mail + "&k=" + ClauActivacio)
                    JJ.Emailing.SendEmailClass.EnviarEmail(Me._Mail, "Clau d'activacio", "Es necesita activar el teu compte amb aquesta clau:" + ClauActivacio + "<br><br>Si vols amb aquest enllaç pots també activar el compter: <a href='" + URL + "'>Activar</a>")
                End If
                Return True
            Catch ex As Exception
                JJ.Registre.RegistrarErrada(ex)
                Return False
            Finally
                Conexio.Close()
            End Try
        End Function

        Public Shared Function SaveDadesPersonals(ByVal Id As Guid, ByVal Nom As String, ByVal Cognoms As String, ByVal DataNaiximent As Date, ByVal IdPais As String, ByVal Regio As String, ByVal CP As String, ByVal Localitat As String, ByVal Telefon As String) As Boolean
            If Nom.Length > 50 Then
                Nom = Nom.Substring(0, 50)
            End If
            If Cognoms.Length > 50 Then
                Cognoms = Cognoms.Substring(0, 50)
            End If
            If Regio.Length > 50 Then
                Regio = Regio.Substring(0, 50)
            End If
            If Localitat.Length > 50 Then
                Localitat = Localitat.Substring(0, 50)
            End If
            If CP.Length > 10 Then
                CP = CP.Substring(0, 10)
            End If
            If Telefon.Length > 30 Then
                Telefon = Telefon.Substring(0, 30)
            End If
            Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
            Dim Comand As New SqlCommand
            Try
                Conexio.Open()
                Comand.Connection = Conexio
                Comand.CommandText = "UPDATE Usuaris"
                Comand.CommandText += " SET nom=@nom,cognoms=@cognoms,data_naiximent=@data_naiximent,ca_pais=@ca_pais,regio=@regio,localitat=@localitat,cp=@cp,telefon=@telefon"
                Comand.CommandText += " WHERE id=@id"
                Comand.Parameters.AddWithValue("@id", Id)
                Comand.Parameters.AddWithValue("@nom", Nom)
                Comand.Parameters.AddWithValue("@cognoms", Cognoms)
                Comand.Parameters.AddWithValue("@data_naiximent", DataNaiximent)
                Comand.Parameters.AddWithValue("@ca_pais", IdPais)
                Comand.Parameters.AddWithValue("@regio", Regio)
                Comand.Parameters.AddWithValue("@localitat", Localitat)
                Comand.Parameters.AddWithValue("@cp", CP)
                Comand.Parameters.AddWithValue("@telefon", Telefon)
                Comand.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                JJ.Registre.RegistrarErrada(ex)
                Return False
            Finally
                Conexio.Close()
            End Try
        End Function


        Public Shared Function MailUnic(ByVal Mail As String) As Boolean
            Dim N As Integer = JJ.Intern.Funcions.BBDD.GetField("COUNT(mail)", "Usuaris", "mail", Mail)
            Return (N = 0)
        End Function
        Public Shared Function AliesUnic(ByVal Alies As String) As Boolean
            Dim N As Integer = JJ.Intern.Funcions.BBDD.GetField("COUNT(alies)", "Usuaris", "alies", Alies)
            Return (N = 0)
        End Function
        Public Shared Function ContrasenyaForta(ByVal Contrasenya As String) As Boolean
            Return (Contrasenya.Length >= 6)
        End Function
        Public Shared Function Validar(ByVal Mail As String, ByVal ClauActivacio As String) As Boolean
            Dim IdClau As Guid = JJ.Intern.Funcions.BBDD.GetField("id", "usuaris", "mail", Mail)
            Dim Clau As String = IdClau.ToString.Substring(0, 8)
            If ClauActivacio.ToUpper <> Clau.ToUpper Then
                Return False
            Else
                Return JJ.Intern.Funcions.BBDD.SetField("validat", True, "usuaris", "mail", Mail)
            End If
        End Function

        Public Shared Function ActualitzarImatge(ByVal IdUsuari As Guid, ByVal Imatge As String, ByVal ExtensioImg As String) As Boolean
            Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
            Dim Comand As New SqlCommand
            Try
                Conexio.Open()
                Comand.Connection = Conexio
                Comand.CommandText = "UPDATE Usuaris SET img=@img,img_extensio=@extensio WHERE id=@id"
                Comand.Parameters.AddWithValue("@img", Imatge)
                Comand.Parameters.AddWithValue("@extensio", ExtensioImg)
                Comand.Parameters.AddWithValue("@id", IdUsuari)
                Return (Comand.ExecuteNonQuery = 1)
            Catch ex As Exception
                JJ.Registre.RegistrarErrada(ex)
                Return False
            Finally
                Conexio.Close()
            End Try
        End Function

        Public Shared Function DonarseBaixa(ByVal Email As String, ByVal Contrasenya As String) As Boolean
            DonarseBaixa = False            
            Dim DonarBaixa As Boolean = False
            Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
            Dim Comand As New SqlCommand
            Try
                Conexio.Open()
                Comand.Connection = Conexio
                Comand.CommandText = "SELECT mail,contrasenya FROM Usuaris WHERE mail=@mail AND contrasenya=@contrasenya"
                Comand.Parameters.AddWithValue("@mail", Email)
                Comand.Parameters.AddWithValue("@contrasenya", Contrasenya)
                Dim Dades As SqlDataReader
                Try
                    Dades = Comand.ExecuteReader()
                    If Not Dades.Read Then
                        Return False
                    Else
                        If (Email <> CType(Dades("mail"), String)) Or (Contrasenya <> CType(Dades("contrasenya"), String)) Then
                            Return False
                        Else
                            DonarBaixa = True
                        End If
                    End If
                Finally
                    If Dades IsNot Nothing Then
                        Dades.Close()
                    End If
                End Try
                If DonarBaixa Then
                    Comand.Parameters.Clear()
                    Comand.CommandText = "DELETE FROM Usuaris WHERE mail=@mail"
                    Comand.Parameters.AddWithValue("@mail", Email)
                    If Comand.ExecuteNonQuery() = 0 Then
                        Return False
                    Else
                        JJ.Sesio.Usuari.Desconectar()
                        JJ.Registre.RegistrarEvent("S'ha donat de baixa l'usuari " + Email)
                        Return True
                    End If
                End If
            Catch ex As Exception
                JJ.Registre.RegistrarErrada(ex)
                Return False
            Finally
                Conexio.Close()
            End Try
        End Function

    End Class


    Public Class UsuariDadesClass
        Dim _Id As Guid
        Dim _Alies, _Nom, _Cognoms As String
        Dim _Foto, _FotoXicoteta, _FotoGran As String

        Public Sub New()
            Me._Id = Guid.Empty
            Me._Alies = ""
            Me._Nom = ""
            Me._Cognoms = ""
            Me.RefrescaImatgePerfil("", "")
        End Sub

        Public Sub New(ByVal Id As Guid, ByVal Alies As String, ByVal Nom As String, ByVal Cognoms As String, ByVal FotoBase As String, ByVal Extensio As String)
            Me._Id = Id
            Me._Alies = Alies
            Me._Nom = Nom
            Me._Cognoms = Cognoms
            Me.RefrescaImatgePerfil(FotoBase, Extensio)
        End Sub

        Public ReadOnly Property Id As Guid
            Get
                Return Me._Id
            End Get
        End Property
        Public ReadOnly Property Alies As String
            Get
                Return Me._Alies
            End Get
        End Property
        Public ReadOnly Property Nom As String
            Get
                Return Me._Nom
            End Get
        End Property
        Public ReadOnly Property Cognoms As String
            Get
                Return Me._Cognoms
            End Get
        End Property
        Public ReadOnly Property Foto As String
            Get
                Return Me._Foto
            End Get
        End Property
        Public ReadOnly Property FotoXicoteta() As String
            Get
                Return Me._FotoXicoteta
            End Get
        End Property
        Public ReadOnly Property FotoGran() As String
            Get
                Return Me._FotoGran
            End Get
        End Property

        Public ReadOnly Property Text() As String
            Get
                'If Me._Nom <> "" And Me._Cognoms <> "" Then
                '    Return Me._Cognoms + ", " + Me._Nom
                'ElseIf Me._Nom <> "" Then
                '    Return Me._Nom
                'ElseIf Me._Cognoms <> "" Then
                '    Return Me._Cognoms
                'ElseIf Me._Alies <> "" Then
                '    Return Me._Alies
                'Else
                '    Return "???"
                'End If
                If Me._Alies <> "" Then
                    Return Me._Alies
                Else
                    Return "???"
                End If
            End Get
        End Property

        Public Function RefrescaImatgePerfil(ByVal FotoBase As String, ByVal Extensio As String) As Boolean
            Try
                If FotoBase = "" Or Extensio = "" Then
                    Me._Foto = JJ.Config.General.Web.GetURL("img/usr.png")
                    Me._FotoXicoteta = JJ.Config.General.Web.GetURL("img/usr_xic.png")
                    Me._FotoGran = JJ.Config.General.Web.GetURL("img/usr_gran.png")
                Else
                    FotoBase = FotoBase.Replace("\", "/")
                    FotoBase = FotoBase.Substring(FotoBase.IndexOf("/img/") + 1)
                    Dim ConfigFotos As New JJ.DadesBase.Fotos.FotoClass.FotosUsuarisEscalades()
                    For i As Integer = 0 To ConfigFotos.Count - 1
                        Dim Tamany As JJ.Grafics.Size = ConfigFotos.Item(i)
                        Select Case i
                            Case 0 : Me._FotoXicoteta = JJ.Config.General.Web.GetURL(FotoBase + "_" + Tamany.Width.ToString + Extensio)
                            Case 1 : Me._Foto = JJ.Config.General.Web.GetURL(FotoBase + "_" + Tamany.Width.ToString + Extensio)
                            Case 2 : Me._FotoGran = JJ.Config.General.Web.GetURL(FotoBase + "_" + Tamany.Width.ToString + Extensio)
                        End Select
                    Next
                End If
                Return True
            Catch ex As Exception
                JJ.Registre.RegistrarErrada(ex)
                Return False
            End Try
        End Function

    End Class

End Namespace
