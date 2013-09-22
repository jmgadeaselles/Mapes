Imports System.Xml
Imports System.Data.SqlClient

Partial Class Sitemap
    Inherits System.Web.UI.Page

    Dim xtw As System.Xml.XmlWriter

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Response.Clear()
        'Esta linha vai fazer com que a página retorne um XML e não mais HTML
        Response.ContentType = "text/xml"

        'Criar o arquivo em memória usando a codificação UTF8
        xtw = New XmlTextWriter(Response.OutputStream, Encoding.UTF8)
        'A linha abaixo vai identar o código para não ficar em uma linha só.
        'xtw.Formatting = Formatting.Indented

        'Escreve a declaração do documento <?xml version="1.0" encoding="utf-8"?>
        xtw.WriteStartDocument()
        'cria o elemento raiz <urlset>
        xtw.WriteStartElement("urlset")
        'Adiciona o atributo xmlns
        xtw.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9")

        'Chama a função que vai criar o elemento URL no XML 
        ' com loc = http://www.cbsa.com.br/default.aspx, 
        ' lastmod = Data atual, 
        ' changefreq = always 
        ' e priority = 1.0 */
        Dim Conexion As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
        Dim Comand As New SqlCommand
        Try
            Conexion.Open()
            Comand.Connection = Conexion
            Comand.CommandText = "SELECT L.id, L.nom"
            'Comand.CommandText += " ,(SELECT TOP 1 arxiu + '_200' + extensio FROM Fotos F WHERE F.ca_lloc=L.id ORDER BY visites DESC) AS foto"
            'Comand.CommandText += " ,(SELECT descripcio_breu FROM LlocsIdioma LI WHERE LI.ca_lloc=L.id AND LI.ca_idioma=@idioma) AS descripcio_breu"
            Comand.CommandText += " FROM Llocs L"
            Comand.CommandText += " WHERE L.habilitat=@habilitat"
            Comand.CommandText += " ORDER BY L.visites DESC, L.nom"
            Comand.Parameters.AddWithValue("@habilitat", True)
            Comand.Parameters.AddWithValue("@idioma", JJ.Sesio.Idioma.Valor)
            Dim Dades As SqlDataReader = Comand.ExecuteReader()
            While Dades.Read
                WriteURLElement(JJ.Config.General.Web.GetURLBase + "?" + CType(Dades("id"), Guid).ToString, Now, "1.0")
            End While
            Dades.Close()
        Catch ex As Exception
            JJ.Registre.RegistrarErrada(ex.ToString)
        Finally
            Conexion.Close()
        End Try


        'libera o XmlTextWriter
        xtw.Flush()
        'fechar o XmlTextWriter
        xtw.Close()
        'Termina aqui
        Response.End()
    End Sub


    Public Sub WriteURLElement(loc As String, lastmod As DateTime, priority As String)
        'Cria o elemento URL
        xtw.WriteStartElement("url")
        'Cria o elemento loc
        xtw.WriteElementString("loc", loc)
        'Cria o elemento lastmod
        xtw.WriteElementString("lastmod", lastmod.ToString("yyyy-MM-dd"))
        'Cria o elemento changefreq
        xtw.WriteElementString("changefreq", "always")
        'Cria o elemento priority
        xtw.WriteElementString("priority", priority)
        'Fecha o elemento URL
        xtw.WriteEndElement()
    End Sub

End Class
