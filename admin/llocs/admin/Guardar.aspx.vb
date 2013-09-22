Imports System.Data.SqlClient

Partial Class admin_llocs_admin_Guardar
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim IdLloc As New Guid(My.Request.Form("ctl00$ContentPlaceHolder1$IdLloc"))
        Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
        Dim Comand As New SqlCommand
        Dim URL As String
        Try
            Conexio.Open()
            Comand.Connection = Conexio
            Comand.CommandText = "UPDATE Llocs SET habilitat=@habilitat,editable=@editable WHERE id=@id"
            Comand.Parameters.AddWithValue("@id", IdLloc)
            Comand.Parameters.AddWithValue("@habilitat", My.Request.Form("ctl00$ContentPlaceHolder1$Habilitat") IsNot Nothing)
            Comand.Parameters.AddWithValue("@editable", My.Request.Form("ctl00$ContentPlaceHolder1$Editable") IsNot Nothing)
            Comand.ExecuteNonQuery()
            URL = "Default.aspx?id=" + IdLloc.ToString + "&resultat=OK"
        Catch ex As Exception
            JJ.Registre.RegistrarErrada(ex.ToString)
            URL = "Default.aspx?id=" + IdLloc.ToString + "&resultat=ERROR"
        Finally
            Conexio.Close()
        End Try
        My.Response.Redirect(URL)
    End Sub
End Class
