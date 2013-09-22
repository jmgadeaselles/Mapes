Imports Microsoft.VisualBasic
Imports System.Data.SqlClient


Namespace JJ


    Public Class Registre

        Public Enum TipusEvents
            Informatiu = 0
            Advertencia = 1
            Errada = 2
        End Enum

        Public Shared Sub RegistrarErrada(ByVal Errada As Exception)
            Registrar(TipusEvents.Errada, Errada.ToString)
        End Sub
        Public Shared Sub RegistrarErrada(ByVal Errada As String)
            Registrar(TipusEvents.Errada, Errada)
        End Sub

        Public Shared Sub RegistrarAdvertencia(ByVal Text As String)
            Registrar(TipusEvents.Advertencia, Text)
        End Sub

        Public Shared Sub RegistrarEvent(ByVal Text As String)
            Registrar(TipusEvents.Informatiu, Text)
        End Sub


        Private Shared Sub Registrar(ByVal Tipus As TipusEvents, ByVal Text As String)
            Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
            Dim Comand As New SqlCommand
            Dim Temp As String
            Try
                Conexio.Open()
                Comand.Connection = Conexio
                Comand.CommandText = "INSERT INTO Registre (id,ip,usuari,tipus,text,user_agent) VALUES (@id,@ip,@usuari,@tipus,@text,@user_agent)"
                Comand.Parameters.AddWithValue("@id", Guid.NewGuid())
                Comand.Parameters.AddWithValue("@ip", System.Web.HttpContext.Current.Request.UserHostAddress)
                Comand.Parameters.AddWithValue("@usuari", JJ.Sesio.Usuari.Alias)
                Comand.Parameters.AddWithValue("@tipus", Tipus)
                If Text.Length > 4000 Then Text = Text.Substring(0, 4000)
                Comand.Parameters.AddWithValue("@text", Text)
                Temp = System.Web.HttpContext.Current.Request.UserAgent
                If Temp.Length > 200 Then Temp = Temp.Substring(0, 200)
                Comand.Parameters.AddWithValue("@user_agent", Temp)
                Comand.ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            Finally
                Conexio.Close()
            End Try
        End Sub



    End Class


End Namespace

