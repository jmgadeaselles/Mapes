
Imports System.Data.SqlClient

Partial Class admin_usr_DetallUSR
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If My.Request.QueryString("id") <> "" Then
            Me.CarregarDades(New Guid(My.Request.QueryString("id")))
        Else
            Me.GuardarCamvis(New Guid(Me.id_usr.Value))
        End If
    End Sub


    Sub CarregarDades(ByVal IdUsr As Guid)        
        Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
        Dim Comand As New SqlCommand
        Try
            Conexio.Open()
            Comand.Connection = Conexio
            Comand.CommandText = "SELECT U.*,P.nomEN FROM Usuaris U"
            Comand.CommandText += " JOIN Paissos P ON U.ca_pais=P.iso"
            Comand.CommandText += " WHERE U.id=@id"
            Comand.Parameters.AddWithValue("@id", IdUsr)
            Dim Dades As SqlDataReader = Comand.ExecuteReader()
            If Dades.Read Then
                Me.id_usr.Value = CType(Dades("id"), Guid).ToString
                Me.Nom.Text = Dades("nom")
                Me.Cognoms.Text = Dades("cognoms")
                Me.Alies.Text = Dades("alies")
                Me.Mail.Text = Dades("mail")
                Me.Mail.PostBackUrl = "mailto:" + Me.Mail.Text
                If Dades("tipus") = 0 Then
                    Me.Tipus.Text = "Administrador"
                Else
                    Me.Tipus.Text = "Usuari"
                End If
                Me.DataNaiximent.Text = Dades("data_naiximent")
                Me.Pais.Text = Dades("nomEN")
                Me.Regio.Text = Dades("regio")
                Me.CP.Text = Dades("CP")
                Me.Localitat.Text = Dades("localitat")
                Me.Telefon.Text = Dades("telefon")

                Me.Validat.Checked = Dades("validat")
                Me.Activat.Checked = Dades("actiu")

                Dim Usr As New JJ.Usuaris.UsuariDadesClass(Dades("id"), Dades("alies"), Dades("nom"), Dades("cognoms"), Dades("img"), Dades("img_extensio"))
                Me.Foto.ImageUrl = Usr.FotoGran

            End If
            Dades.Close()
        Catch ex As Exception
            JJ.Registre.RegistrarErrada(ex)
        Finally
            Conexio.Close()
        End Try
    End Sub


    Sub GuardarCamvis(ByVal IdUsr As Guid)        
        Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
        Dim Comand As New SqlCommand
        Try
            Conexio.Open()
            Comand.Connection = Conexio
            Comand.CommandText = "UPDATE Usuaris SET validat=@validat, actiu=@actiu WHERE id=@id"
            Comand.Parameters.AddWithValue("@validat", Me.Validat.Checked)
            Comand.Parameters.AddWithValue("@actiu", Me.Activat.Checked)
            Comand.Parameters.AddWithValue("@id", IdUsr)
            If Comand.ExecuteNonQuery() = 0 Then
                Me.Resultat.Text = "No s'han pogut guardar els camvis."
                Me.Resultat.ForeColor = Drawing.Color.Red                
            Else
                Me.Resultat.Text = "Camvits guardats."
                Me.Resultat.ForeColor = Drawing.Color.Green
            End If
            Me.Resultat.Visible = True
        Catch ex As Exception
            Me.Resultat.Text = "Errada: " + ex.ToString
            Me.Resultat.ForeColor = Drawing.Color.Red
            Me.Resultat.Visible = True
            JJ.Registre.RegistrarErrada(ex)
        Finally
            Conexio.Close()
        End Try

    End Sub


End Class
