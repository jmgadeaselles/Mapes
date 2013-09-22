Imports Microsoft.VisualBasic
Imports System.Data.SqlClient


Namespace JJ.Varios

    Public Class Suggeriments


        Public Shared Function Registrar(ByVal Text As String) As Boolean
            Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
            Dim Comand As New SqlCommand
            Try
                Conexio.Open()
                Comand.Connection = Conexio
                Comand.CommandText = "INSERT Suggeriments"
                Comand.CommandText += " (id,ca_usuari,data,text)"
                Comand.CommandText += " VALUES"
                Comand.CommandText += " (@id,@ca_usuari,GETDATE(),@text)"
                Comand.Parameters.AddWithValue("@id", Guid.NewGuid())
                Comand.Parameters.AddWithValue("@ca_usuari", JJ.Sesio.Usuari.Id)
                Comand.Parameters.AddWithValue("@text", Text)
                Return Comand.ExecuteNonQuery() = 1
            Catch ex As Exception
                JJ.Registre.RegistrarErrada(ex)
                Return False
            Finally
                Conexio.Close()
            End Try
        End Function


        Public Shared Function Esborrar(ByVal IdSuggeriment As Guid) As Boolean
            Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
            Dim Comand As New SqlCommand
            Try
                Conexio.Open()
                Comand.Connection = Conexio
                Comand.CommandText = "DELETE FROM Suggeriments"
                Comand.CommandText += " WHERE id=@id"
                Comand.Parameters.AddWithValue("@id", IdSuggeriment)
                Return Comand.ExecuteNonQuery() = 1
            Catch ex As Exception
                JJ.Registre.RegistrarErrada(ex)
                Return False
            Finally
                Conexio.Close()
            End Try
        End Function

    End Class


End Namespace
