Imports System.Data.SqlClient


Partial Class admin_categories_Esborrar
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim IdCat As Guid = New Guid(My.Request.QueryString("id"))
        Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
        Dim Comand As New SqlCommand
        Try
            Conexio.Open()
            Comand.Connection = Conexio
            Comand.CommandText = "DELETE FROM Categories WHERE id=@id"
            Comand.Parameters.AddWithValue("@id", IdCat)
            Comand.ExecuteNonQuery()
        Catch ex As Exception
            JJ.Registre.RegistrarErrada(ex.ToString)
        Finally
            Conexio.Close()
        End Try
        My.Response.Redirect("Default.aspx")
    End Sub
End Class
