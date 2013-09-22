Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Net
Imports System.Collections.Generic

Namespace JJ.Idiomes

    Public Class IdiomaClass
        Dim _Codi, _Nom As String

        Public Sub New(ByVal Codi As String, ByVal Nom As String)
            Me._Codi = Codi
            Me._Nom = Nom
        End Sub

        Public ReadOnly Property Codi As String
            Get
                Return Me._Codi
            End Get
        End Property

        Public ReadOnly Property Nom As String
            Get
                Return Me._Nom
            End Get
        End Property

        Public Shared Function GetNomIdioma(ByVal CodiIdioma As String) As String
            Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
            Dim Comand As New SqlCommand
            Try
                Conexio.Open()
                Comand.Connection = Conexio
                Comand.CommandText = "SELECT nom FROM Idiomes WHERE codi=@codi"
                Comand.Parameters.AddWithValue("@codi", CodiIdioma.ToLower)
                Dim Dades As SqlDataReader = Comand.ExecuteReader()
                If Dades.Read Then
                    GetNomIdioma = Dades("nom")
                Else
                    GetNomIdioma = ""
                End If
                Dades.Close()
            Catch ex As Exception
                JJ.Registre.RegistrarErrada(ex)
                Return ""
            Finally
                Conexio.Close()
            End Try


        End Function

    End Class


    Public MustInherit Class IdiomesBase
        Protected _Llistat As List(Of IdiomaClass)

        Protected Sub New(ByVal Filtre As String)
            Me._Llistat = New List(Of IdiomaClass)
            Me.Carregar(Filtre)
        End Sub

        Default Public ReadOnly Property Item(ByVal Index As Integer) As IdiomaClass
            Get
                Return Me._Llistat(Index)
            End Get
        End Property

        Public ReadOnly Property Count() As Integer
            Get
                Return Me._Llistat.Count
            End Get
        End Property

        Private Sub Carregar(ByVal Filtre As String)
            Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
            Dim Comand As New SqlCommand
            Try
                Conexio.Open()
                Comand.Connection = Conexio
                Comand.CommandText = "SELECT codi, nom FROM Idiomes"
                If Filtre <> "" Then
                    Comand.CommandText += " WHERE " + Filtre
                End If
                Comand.CommandText += "ORDER BY nom"
                Dim Dades As SqlDataReader = Comand.ExecuteReader()
                While Dades.Read
                    Dim Idioma As New IdiomaClass(Dades("codi"), Dades("nom"))
                    Me._Llistat.Add(Idioma)
                End While
                Dades.Close()
            Catch ex As Exception
                JJ.Registre.RegistrarErrada(ex)                
            Finally
                Conexio.Close()
            End Try
        End Sub

    End Class



    Public Class LlistatIdiomesWeb
        Inherits IdiomesBase

        Public Sub New()
            MyBase.New("habilitat_web='1'")
        End Sub
    End Class



    Public Class LlistatIdiomesDades
        Inherits IdiomesBase

        Public Sub New()
            MyBase.New("habilitat_dades='1'")
        End Sub
    End Class


    Public Class Traductor


        Private Function Traduir(ByVal TextEntrada As String, ByVal IdiomaEixida As String, ByVal IdiomaEntrada As String) As String
            'asigne su id de apicacion en la siguiente variable!!!
            'Puede obterno de https://www.bing.com/developers/appids.aspx
            'Registrandose con su Live ID
            '**************************************************************
            '**************************************************************
            Dim BingId As String
            '**************************************************************
            Dim resultado As String = ""
            If BingId Is Nothing Then Return ("FALTA EL ID DE BING....despues asignelo a la variable BingId" + vbCrLf + "Para obtenerlo visite:" + vbCrLf + "www.bing.com/developers/appids.aspx")

            'Preparando servicio
            Dim Uri As String = "http://api.microsofttranslator.com/v2/Http.svc/Translate?appId=" + BingId + "&amp;text=" + TextEntrada + "&amp;from=" + IdiomaEntrada + "&amp;to=" + IdiomaEixida
            'Preparando la respuestweb
            Dim Solicitud As HttpWebRequest = CType(WebRequest.Create(Uri), HttpWebRequest)

            'Obteniendo la respuesta de la api
            Dim Respuesta As WebResponse = Solicitud.GetResponse
            Dim RespuestaXml As System.Xml.XmlTextReader = New System.Xml.XmlTextReader(Respuesta.GetResponseStream())

            While RespuestaXml.Read
                resultado = RespuestaXml.ReadString
            End While

            RespuestaXml.Close()
            Return resultado

        End Function

    End Class

End Namespace