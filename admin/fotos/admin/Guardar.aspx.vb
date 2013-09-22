Imports System.Data.SqlClient

Partial Class admin_llocs_admin_Guardar
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Me.Label1.Text = ""
        'For Each Claus As String In My.Request.Form.Keys
        '    Me.Label1.Text += "<br />" + Claus
        'Next
        Dim IdFoto As New Guid(My.Request.Form("ctl00$ContentPlaceHolder1$IdFoto"))
        Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
        Dim Comand As New SqlCommand
        Dim URL As String
        Try
            Conexio.Open()
            Comand.Connection = Conexio
            Comand.CommandText = "UPDATE Fotos SET habilitat=@habilitat WHERE id=@id"
            Comand.Parameters.AddWithValue("@id", IdFoto)
            Comand.Parameters.AddWithValue("@habilitat", My.Request.Form("ctl00$ContentPlaceHolder1$Habilitat") IsNot Nothing)
            Comand.ExecuteNonQuery()
            URL = "Default.aspx?id=" + IdFoto.ToString + "&resultat=OK"
        Catch ex As Exception
            JJ.Registre.RegistrarErrada(ex.ToString)
            URL = "Default.aspx?id=" + IdFoto.ToString + "&resultat=ERROR"
        Finally
            Conexio.Close()
        End Try
        My.Response.Redirect(URL)
    End Sub
End Class
