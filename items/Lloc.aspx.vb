


Partial Class items_Lloc
    Inherits JJ.Intern.WebForm.PaginaIdiomaBase

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Dim Id As Guid
            Try
                Id = New Guid(My.Request.QueryString("id"))
            Catch ex As Exception
                Id = JJ.Geo.Llocs.LlocClass.GetIdLlocMesVisitat()
            End Try
            Dim Idioma As String = My.Request.QueryString("i")
            If Idioma = "" Then
                Idioma = JJ.Sesio.Idioma.Valor
            End If
            Me.IdLloc.Value = Id.ToString
            Me.IdiomaLloc.Value = Idioma
            'Lloc
            Dim RegistrarVisita As Boolean = (My.Request.QueryString("v") = "")
            Dim Lloc As New JJ.Geo.Llocs.LlocClass(Idioma, RegistrarVisita)
            If Lloc.Obrir(Id, Idioma) Then
                Dim Titol As Label = Master.FindControl("Titol")
                Titol.Text = Lloc.Nom
                Me.DescripcioBreu.Text = Lloc.DescripcioBreu.Replace(vbCrLf, "<br />")                
                Me.Descripcio.Text = Lloc.Descripcio.Replace(vbCrLf, "<br />")
                Me.Acces.Text = Lloc.Acces.Replace(vbCrLf, "<br />")
                Me.Lat.Value = Lloc.Posicio.Lat.ToString.Replace(",", ".")
                Me.Lng.Value = Lloc.Posicio.Lng.ToString.Replace(",", ".")
                Me.IdCategoria.Value = Lloc.IdCategoria.ToString
                Me.ImatgeCategoria.ImageUrl = JJ.Config.General.Web.GetURL("/img/cat/" + Me.IdCategoria.Value + ".png")
                Me.Posicio.Text = Lloc.Posicio.ToString
                Dim DadesAutor As JJ.Usuaris.UsuariDadesClass = Lloc.Autor
                If DadesAutor.Id = Guid.Empty Then
                    Me.Autor.Text = ""
                Else
                    Me.Autor.Text = DadesAutor.Text
                End If
                Me.FotoAutor.ImageUrl = DadesAutor.Foto
                If JJ.Sesio.Usuari.Validat Then
                    'Me.LinkPujarFoto.PostBackUrl = "#"
                    Me.LinkPujarFoto.PostBackUrl = ""
                    Me.LinkPujarFoto.OnClientClick = "MostrarPujarFoto('" + Id.ToString + "','" + Idioma + "'); return false;"
                Else
                    Me.LinkPujarFoto.PostBackUrl = JJ.Config.General.Web.GetURL("/usr/Accedir.aspx?url=" + My.Request.Url.ToString)
                End If
                Me.LinkHistorial.OnClientClick = "MostrarHistorial('" + Id.ToString + "','" + Idioma + "'); return false;"
                'Comentaris
                If JJ.Sesio.Usuari.Validat Then
                    Me.MultivistesComentaris.SetActiveView(Me.VistaAfegir)
                Else
                    Me.LinkIniciarSesio.PostBackUrl = JJ.Config.General.Web.GetURL("/usr/Accedir.aspx?url=" + My.Request.Url.ToString)
                    Me.MultivistesComentaris.SetActiveView(Me.VistaAcces)
                End If
                Dim Comentaris As List(Of JJ.Geo.Llocs.Comentaris.ComentariClass) = Lloc.GetComentaris(False)
                If Comentaris IsNot Nothing Then
                    For Each Comentari As JJ.Geo.Llocs.Comentaris.ComentariClass In Comentaris

                        Dim CeldaUsr As New TableCell()
                        CeldaUsr.Style.Add("vertical-align", "top")
                        CeldaUsr.Style.Add("width", "30px;")
                        CeldaUsr.Text = "<img src='" + Comentari.Usuari.FotoXicoteta + "'>"
                        Dim CeldaText As New TableCell()
                        CeldaText.Style.Add("vertical-align", "top")
                        CeldaText.Text = "<span class='AutorComentari'>" + Comentari.Usuari.Text + "</span> <span class='DataComentari'>" + Comentari.Data.ToUniversalTime.ToString + "</span><br><span>" + Comentari.Text + "</span>"

                        Dim Fila As New TableRow()
                        Fila.CssClass = "fons_item"
                        Fila.Cells.Add(CeldaUsr)
                        Fila.Cells.Add(CeldaText)

                        Me.TaulaComentaris.Rows.Add(Fila)
                    Next
                End If
                'Si no hi ha cap comentari, afegim una fila
                If Me.TaulaComentaris.Rows.Count = 0 Then
                    Dim CeldaUsr As New TableCell()
                    CeldaUsr.Style.Add("vertical-align", "top")
                    CeldaUsr.Style.Add("width", "30px;")
                    CeldaUsr.Text = ""
                    Dim CeldaText As New TableCell()
                    CeldaText.Style.Add("vertical-align", "top")
                    CeldaText.Text = ""

                    Dim Fila As New TableRow()
                    Fila.CssClass = "fons_item"
                    Fila.Cells.Add(CeldaUsr)
                    Fila.Cells.Add(CeldaText)

                    Me.TaulaComentaris.Rows.Add(Fila)

                End If
                'Titol de la pagina
                CType(Master.FindControl("HeadTitol"), Literal).Text = JJ.Intern.WebForm.HeadWebForm.Generar(Lloc.Nom, Lloc.ParaulesClau, Lloc.DescripcioBreu)
                'Enllaç        
                Me.URL.Text = JJ.Config.General.Web.GetURLBase() + "?id=" + Id.ToString + "&og=1"
                'Facebook
                'Me.fbOpenGraph.Text = "<meta name=""description"" content=""" + Me.DescripcioBreu.Text + """ />" ' + _
                '"<meta property=""og:title"" content=""" + Titol.Text + """ />" + _
                '"<meta property=""og:type"" content=""sport"" />" + _
                '"<meta property=""og:url"" content=""" + My.Request.Url.ToString.Replace("/mapes/", "/") + "&t=" + Now.ToString("yyyymmddhhnnss") + """ />" + _
                '"<meta property=""og:image"" content=""http://www.ocimap.com/img/usr.png?t=" + Now.ToString("yyyymmddhhnnss") + """ />" + _
                '"<meta property=""og:site_name"" content=""OciMap.com"" />" + _
                '"<meta property=""fb:admins"" content=""100002386188175"" />"

                'Me.fbOpenGraph.Text = "<meta name=""description"" content=""" + Me.DescripcioBreu.Text.Replace("""", "'") + """ />" + vbCrLf + _
                '"<meta property=""og:title"" content=""" + Lloc.Nom.Replace("""", "'") + """ />" + vbCrLf + _
                '"<meta property=""og:type"" content=""sport"" />" + vbCrLf + _
                '"<meta property=""og:url"" content=""" + Me.URL.Text + """ />" + vbCrLf + _
                '"<meta property=""og:image"" content=""http://www.ocimap.com/img/usr.png?t=" + Now.ToString("yyyymmddhhnnss") + """ />" + vbCrLf + _
                '"<meta property=""og:site_name"" content=""OciMap.com"" />" ' + vbCrLf + _
                '"<meta property=""fb:admins"" content=""100002386188175"" />" + vbCrLf + _
                '"<meta property=""fb:app_id"" content=""469282516456901"" />"





            End If
        Catch ex As Exception
            JJ.Registre.RegistrarErrada("URL: " + My.Request.Url.ToString + vbCrLf + "URL: " + My.Request.RawUrl + vbCrLf + ex.ToString)
        End Try
    End Sub




End Class
