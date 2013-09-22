Imports System.Data.SqlClient


Partial Class admin_llocs_admin_Vore
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim Id As New Guid(My.Request.QueryString("id"))
        Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
        Dim Comand As New SqlCommand
        Try
            Conexio.Open()
            Comand.Connection = Conexio
            'Fitxa
            Comand.CommandText = "SELECT HL.nom, H.data, HLI.descripcio_breu, HLI.descripcio, HLI.acces, U.alies, CI.text AS categoria FROM Historic H"
            Comand.CommandText += " JOIN HistoricLlocs HL ON H.id=HL.ca_historic"
            Comand.CommandText += " JOIN HistoricLlocsIdioma HLI ON HL.ca_lloc=HLI.ca_lloc AND HL.ca_historic=HLI.ca_historic"
            Comand.CommandText += " JOIN Usuaris U ON H.ca_usuari=U.id"
            Comand.CommandText += " JOIN CategoriesIdioma CI ON HL.ca_categoria=CI.ca_categoria AND CI.ca_idioma='ca'"
            Comand.CommandText += " WHERE H.id=@id"
            Comand.Parameters.AddWithValue("@id", Id)
            Dim Dades As SqlDataReader = Comand.ExecuteReader()
            If Dades.Read Then
                Me.Titol.Text = Dades("nom")
                Me.Data.Text = Dades("data")
                Me.Usuari.Text = Dades("alies")
                Me.Categoria.Text = Dades("categoria")
                Me.DescripcioBreu.Text = Dades("descripcio_breu")
                Me.Descripcio.Text = Dades("descripcio")
                Me.Acces.Text = Dades("acces")
            End If
            Dades.Close()
            'Paraules clau
            Me.Paraules.Text = ""
            Comand.Parameters.Clear()
            Comand.CommandText = "SELECT clau FROM HistoricClaus"
            Comand.CommandText += " WHERE ca_historic=@id"
            Comand.CommandText += " ORDER BY clau"
            Comand.Parameters.AddWithValue("@id", Id)
            Dades = Comand.ExecuteReader()
            While Dades.Read
                Me.Paraules.Text += ", " + Dades("clau")
            End While
            Dades.Close()
            If Me.Paraules.Text.Length > 0 Then
                Me.Paraules.Text = Me.Paraules.Text.Substring(2)
            End If
        Catch ex As Exception
            JJ.Registre.RegistrarErrada(ex.ToString)
        Finally
            Conexio.Close()
        End Try
    End Sub
End Class
