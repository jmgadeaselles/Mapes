Imports System.Data.SqlClient
Imports System.Xml

Partial Class rss_Dades
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load        
        Select Case My.Request.QueryString("sv")
            Case "buscar"
                'Defenir
            Case Else
                'Navegar
                Me.Navegar()
        End Select
    End Sub



    Private Sub Navegar()
        My.Response.Clear()
        My.Response.ContentType = "text/xml"
        My.Response.Charset = "utf-8"
        My.Response.CacheControl = "public"
        'My.Response.ExpiresAbsolute = Date.Now.AddYears(-10)
        'My.Response.ContentEncoding = System.Text.Encoding.UTF8
        'My.Response.BufferOutput = True
        'My.Response.Buffer = True
        Dim stream As New System.IO.MemoryStream
        Dim XMLwrite As New XmlTextWriter(stream, System.Text.Encoding.UTF8)

        XMLwrite.WriteStartDocument()

        XMLwrite.WriteWhitespace(Environment.NewLine)
        XMLwrite.WriteStartElement("rss")
        'XMLwrite.WriteAttributeString("xmlns:content", "http://purl.org/rss/1.0/modules/content/")
        'XMLwrite.WriteAttributeString("xmlns:rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#")
        'XMLwrite.WriteAttributeString("xmlns:dc", "http://purl.org/dc/elements/1.1/")
        'XMLwrite.WriteAttributeString("xmlns:media", "http://search.yahoo.com/mrss")
        XMLwrite.WriteAttributeString("xmlns:gml", "http://www.opengis.net/gml")
        'XMLwrite.WriteAttributeString("xmlns:taxo", "http://purl.org/rss/1.0/modules/taxonomy/")
        XMLwrite.WriteAttributeString("xmlns:georss", "http://www.georss.org/georss")
        XMLwrite.WriteAttributeString("xmlns:geo", "http://www.w3.org/2003/01/geo/wgs84_pos#")
        XMLwrite.WriteAttributeString("version", "2.0")

        XMLwrite.WriteWhitespace(Environment.NewLine)
        XMLwrite.WriteStartElement("channel")

        XMLwrite.WriteElementString("title", "Título XML")
        XMLwrite.WriteElementString("description", "Descripcio xml")
        'XMLwrite.WriteElementString("layer", "Layer XML")
        XMLwrite.WriteElementString("image", "http://localhost/wikitourinfo/img/tancar.jpg")

        Dim N As Double
        Dim S As Double
        Dim E As Double
        Dim O As Double
        Dim Z As Integer
        Dim TextBuscar As String

        'If JJ.Config.General.Web.GetWebLocal() Then
        N = CType(My.Request.QueryString("N").Replace(".", ","), Double)
        S = CType(My.Request.QueryString("S").Replace(".", ","), Double)
        E = CType(My.Request.QueryString("E").Replace(".", ","), Double)
        O = CType(My.Request.QueryString("O").Replace(".", ","), Double)
        'Else
        'N = CType(My.Request.QueryString("N"), Double)
        'S = CType(My.Request.QueryString("S"), Double)
        'E = CType(My.Request.QueryString("E"), Double)
        'O = CType(My.Request.QueryString("O"), Double)
        'End If
        Z = CType(My.Request.QueryString("Z"), Integer)
        TextBuscar = My.Request.QueryString("T")

        Dim GeoRSS As New JJ.GeoRSS.GeoRSSClass()
        If Not GeoRSS.Navegar(N, S, O, E, Z, TextBuscar) Then
            JJ.Registre.RegistrarAdvertencia("El GeoRSS ha tornat False al invocarse 'Navegar'")
        Else
            For Each Lloc As JJ.Geo.Llocs.LlocReadOnlyClass In GeoRSS.Llocs
                'Inici Item
                XMLwrite.WriteStartElement("item")
                XMLwrite.WriteElementString("guid", Lloc.Id.ToString)
                XMLwrite.WriteElementString("icon", JJ.Config.General.Web.GetURL("/img/cat/" + Lloc.IdCategoria.ToString.ToUpper + "(x).png"))
                XMLwrite.WriteElementString("title", Lloc.Nom)
                XMLwrite.WriteElementString("description", Lloc.DescripcioBreu)
                XMLwrite.WriteElementString("link", "http://www.nucinet.com")
                XMLwrite.WriteElementString("lat", Lloc.Posicio.Lat.ToString.Replace(",", "."))
                XMLwrite.WriteElementString("long", Lloc.Posicio.Lng.ToString.Replace(",", "."))
                XMLwrite.WriteElementString("img", Lloc.FotoPortada)
                'fin item
                XMLwrite.WriteWhitespace(Environment.NewLine)
                XMLwrite.WriteEndElement()
            Next
        End If

        'fin chanel
        XMLwrite.WriteWhitespace(Environment.NewLine)
        XMLwrite.WriteEndElement()


        'fin rss
        XMLwrite.WriteWhitespace(Environment.NewLine)
        XMLwrite.WriteEndElement()

        XMLwrite.WriteEndDocument()
        XMLwrite.Flush()

        Dim reader As IO.StreamReader
        stream.Position = 0
        reader = New IO.StreamReader(stream)
        Dim bytes() As Byte = System.Text.Encoding.UTF8.GetBytes(reader.ReadToEnd())
        My.Response.BinaryWrite(bytes)
    End Sub





    Private Sub NavegarOld()
        My.Response.Clear()
        My.Response.ContentType = "text/xml"
        My.Response.Charset = "utf-8"
        My.Response.CacheControl = "public"
        'My.Response.ExpiresAbsolute = Date.Now.AddYears(-10)
        'My.Response.ContentEncoding = System.Text.Encoding.UTF8
        'My.Response.BufferOutput = True
        'My.Response.Buffer = True
        Dim stream As New System.IO.MemoryStream
        Dim XMLwrite As New XmlTextWriter(stream, System.Text.Encoding.UTF8)

        XMLwrite.WriteStartDocument()

        XMLwrite.WriteWhitespace(Environment.NewLine)
        XMLwrite.WriteStartElement("rss")
        'XMLwrite.WriteAttributeString("xmlns:content", "http://purl.org/rss/1.0/modules/content/")
        'XMLwrite.WriteAttributeString("xmlns:rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#")
        'XMLwrite.WriteAttributeString("xmlns:dc", "http://purl.org/dc/elements/1.1/")
        'XMLwrite.WriteAttributeString("xmlns:media", "http://search.yahoo.com/mrss")
        XMLwrite.WriteAttributeString("xmlns:gml", "http://www.opengis.net/gml")
        'XMLwrite.WriteAttributeString("xmlns:taxo", "http://purl.org/rss/1.0/modules/taxonomy/")
        XMLwrite.WriteAttributeString("xmlns:georss", "http://www.georss.org/georss")
        XMLwrite.WriteAttributeString("xmlns:geo", "http://www.w3.org/2003/01/geo/wgs84_pos#")
        XMLwrite.WriteAttributeString("version", "2.0")

        XMLwrite.WriteWhitespace(Environment.NewLine)
        XMLwrite.WriteStartElement("channel")

        XMLwrite.WriteElementString("title", "Título XML")
        XMLwrite.WriteElementString("description", "Descripcio xml")
        'XMLwrite.WriteElementString("layer", "Layer XML")
        XMLwrite.WriteElementString("image", "http://localhost/wikitourinfo/img/tancar.jpg")

        Dim N As Double = CType(My.Request.QueryString("N").Replace(".", ","), Double)
        Dim S As Double = CType(My.Request.QueryString("S").Replace(".", ","), Double)
        Dim E As Double = CType(My.Request.QueryString("E").Replace(".", ","), Double)
        Dim O As Double = CType(My.Request.QueryString("O").Replace(".", ","), Double)
        'MiWeb.Registro.RegistrarEvento(My.Request.QueryString.ToString)
        'JJ.Registre.RegistrarEvent(My.Request.QueryString.ToString)
        'MiWeb.Registro.RegistrarEvento(N.ToString)
        Dim Conexion As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
        Dim Comando As New SqlCommand

        Try
            Conexion.Open()
            Comando.Connection = Conexion
            'Comando.CommandText = "SELECT L.id, L.lat, L.lng, LI.nombre, LI.descripcion FROM Lugar L"
            'Comando.CommandText += " JOIN LugarIdiomas LI ON L.id=LI.ca_lugar"
            'Comando.CommandText += " WHERE lat<=@norte AND lat>=@sur AND lng>=@oeste AND lng<=@este"

            Comando.CommandText = "SELECT L.id, L.lat, L.lng, L.ca_categoria, L.nom AS nombre, LI.descripcio_breu AS descripcion FROM Llocs L"
            Comando.CommandText += " JOIN LlocsIdioma LI ON L.id=LI.ca_lloc"
            Comando.CommandText += " WHERE lat<=@norte AND lat>=@sur AND lng>=@oeste AND lng<=@este"

            Comando.Parameters.AddWithValue("@norte", N)
            Comando.Parameters.AddWithValue("@sur", S)
            Comando.Parameters.AddWithValue("@oeste", O)
            Comando.Parameters.AddWithValue("@este", E)
            Dim Datos As SqlDataReader = Comando.ExecuteReader()
            While Datos.Read
                XMLwrite.WriteStartElement("item")



                'XMLwrite.WriteElementString("id", "101")
                XMLwrite.WriteElementString("guid", CType(Datos("id"), Guid).ToString)
                'XMLwrite.WriteElementString("icon", JJ.Config.General.Web.GetURL("/img/tancar.jpg"))
                XMLwrite.WriteElementString("icon", JJ.Config.General.Web.GetURL("/img/cat/" + CType(Datos("ca_categoria"), Guid).ToString.ToUpper + "(x).png"))
                XMLwrite.WriteElementString("title", Datos("nombre"))
                XMLwrite.WriteElementString("description", Datos("descripcion"))
                XMLwrite.WriteElementString("link", "http://www.nucinet.com")
                'XMLwrite.WriteElementString("georss:point", Datos("lat") + " " + Datos("lng"))
                XMLwrite.WriteElementString("geo:lat", CType(Datos("lat"), Double).ToString.Replace(",", "."))
                XMLwrite.WriteElementString("geo:long", CType(Datos("lng"), Double).ToString.Replace(",", "."))
                'XMLwrite.WriteElementString("display-order", "521")


                'fin item
                XMLwrite.WriteWhitespace(Environment.NewLine)
                XMLwrite.WriteEndElement()
            End While
            Datos.Close()
        Catch ex As Exception
            JJ.Registre.RegistrarErrada(ex)
        Finally
            Conexion.Close()
        End Try

        'XMLwrite.WriteStartElement("item")

        ''XMLwrite.WriteElementString("id", "101")
        'XMLwrite.WriteElementString("guid", "101")
        'XMLwrite.WriteElementString("icon", "http://localhost/wikitourinfo/img/tancar.jpg")
        'XMLwrite.WriteElementString("title", "Millena")
        'XMLwrite.WriteElementString("description", "Descripcion")
        'XMLwrite.WriteElementString("link", "http://www.nucinet.com")
        ''XMLwrite.WriteElementString("image", "http://localhost/wikitourinfo/img/tancar.jpg")
        'XMLwrite.WriteElementString("georss:point", "46.31409 -122.22616")
        'XMLwrite.WriteElementString("geo:lat", "46.31409")
        'XMLwrite.WriteElementString("geo:long", "-122.22616")
        ''XMLwrite.WriteElementString("display-order", "521")


        ''fin item
        'XMLwrite.WriteWhitespace(Environment.NewLine)
        'XMLwrite.WriteEndElement()



        'XMLwrite.WriteStartElement("item")

        ''XMLwrite.WriteElementString("id", "101")
        'XMLwrite.WriteElementString("guid", "101")
        'XMLwrite.WriteElementString("icon", "http://localhost/wikitourinfo/img/tancar.jpg")
        'XMLwrite.WriteElementString("title", "Gorga")
        'XMLwrite.WriteElementString("description", "Descripcion")
        'XMLwrite.WriteElementString("link", "http://www.nucinet.com")
        ''XMLwrite.WriteElementString("image", "http://localhost/wikitourinfo/img/tancar.jpg")
        'XMLwrite.WriteElementString("georss:point", "44.31409 -122.22616")
        'XMLwrite.WriteElementString("geo:lat", "44.31409")
        'XMLwrite.WriteElementString("geo:long", "-122.22616")
        ''XMLwrite.WriteElementString("display-order", "521")


        ''fin item
        'XMLwrite.WriteWhitespace(Environment.NewLine)
        'XMLwrite.WriteEndElement()



        'fin chanel
        XMLwrite.WriteWhitespace(Environment.NewLine)
        XMLwrite.WriteEndElement()


        'fin rss
        XMLwrite.WriteWhitespace(Environment.NewLine)
        XMLwrite.WriteEndElement()

        XMLwrite.WriteEndDocument()
        XMLwrite.Flush()

        Dim reader As IO.StreamReader
        stream.Position = 0
        reader = New IO.StreamReader(stream)
        Dim bytes() As Byte = System.Text.Encoding.UTF8.GetBytes(reader.ReadToEnd())
        My.Response.BinaryWrite(bytes)

        '(Hi ha que llevar) Retard per comprovar la recarrega
        'For i As Integer = 0 To 99999999
        '    Dim y As Integer = 0
        'Next
    End Sub

End Class
