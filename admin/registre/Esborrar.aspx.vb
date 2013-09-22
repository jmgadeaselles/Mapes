Imports System.Data.SqlClient


Partial Class admin_registre_Esborrar
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
        Dim Comand As New SqlCommand
        Try
            Conexio.Open()
            Comand.Connection = Conexio
            Comand.CommandText = "DELETE FROM REGISTRE"
            Comand.ExecuteNonQuery()
        Catch ex As Exception
            JJ.Registre.RegistrarErrada(ex.ToString)
        Finally
            Conexio.Close()
        End Try
    End Sub
End Class
