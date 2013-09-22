
Partial Class _Default
    Inherits JJ.Intern.WebForm.PaginaIdiomaBase


    Protected Overrides Sub InitializeCulture()
        'idioma
        If My.Request.QueryString("i") <> "" Then
            JJ.Sesio.Idioma.Valor = My.Request.QueryString("i").Trim
        End If
        MyBase.InitializeCulture()
    End Sub


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim Titol As String = "Ocimap.com"
        If My.Request.QueryString("id") <> "" Then
            'JJ.Registre.RegistrarEvent("Glob: " + System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName)
            Dim Id As Guid = New Guid(My.Request.QueryString("id"))
            Dim Idioma As String = My.Request.QueryString("i")
            'JJ.Registre.RegistrarEvent("Idioma GET: " + Idioma)
            If Idioma = "" Then
                Idioma = JJ.Sesio.Idioma.Valor
            End If            
            'Lloc            
            Dim Lloc As New JJ.Geo.Llocs.LlocClass(Idioma, False)
            If Lloc.Obrir(Id, Idioma) Then
                Titol = Lloc.Nom
                Dim Root As String = Server.MapPath("/")
                Dim RutaImg, RutaImgGran As String
                Dim Fotos As List(Of JJ.Geo.Llocs.Fotos.FotoClass) = JJ.Geo.Llocs.LlocClass.GetImatges(Id, Idioma)
                If Fotos.Count > 0 Then
                    Dim Foto As JJ.Geo.Llocs.Fotos.FotoClass = Fotos(0)
                    RutaImg = Foto.Arxiu + "_200" + Foto.Extensio
                    RutaImg = JJ.Config.General.Web.GetURL(RutaImg.Substring(Root.Length))
                    RutaImg = RutaImg.Replace("\", "/")
                    RutaImgGran = Foto.Arxiu + "_800" + Foto.Extensio
                    RutaImgGran = JJ.Config.General.Web.GetURL(RutaImgGran.Substring(Root.Length))
                    RutaImgGran = RutaImgGran.Replace("\", "/")
                Else
                    RutaImg = JJ.Config.General.Web.GetURL("img/ocimap.png")
                    RutaImgGran = RutaImg
                End If

                'Meta Tags
                CType(Master.FindControl("DadesHead"), Literal).Text = JJ.Intern.WebForm.HeadWebForm.Generar(Lloc.Nom, Lloc.ParaulesClau, Lloc.DescripcioBreu)

                'Open Graph
                If My.Request.QueryString("og") = "1" Then
                    Me.LinkIdiomes.Text = ""
                    Me.fbOpenGraph.Text = "<meta name=""description"" content=""" + Lloc.DescripcioBreu.Replace(vbCrLf, " ").Replace("  ", " ").Replace("""", "'") + " - " + JJ.Config.General.Web.GetURLBase() + """ />" + vbCrLf + _
                    "<meta property=""og:title"" content=""" + Lloc.Nom.Replace("""", "'") + """ />" + vbCrLf + _
                    "<meta property=""og:type"" content=""sport"" />" + vbCrLf + _
                    "<meta property=""og:url"" content=""" + JJ.Config.General.Web.GetURLBase + "?id=" + Id.ToString + "&i=" + Idioma + "&og=1" + """ />" + vbCrLf + _
                    "<meta property=""og:image"" content=""" + RutaImg + """ />" + vbCrLf + _
                    "<meta property=""og:site_name"" content=""OciMap.com"" />" + vbCrLf + _
                    "<meta property=""fb:admins"" content=""100002386188175"" />"
                Else
                    Me.fbOpenGraph.Text = ""
                End If
            End If
        End If
        '''

        If Not (My.Request.QueryString("og") = "1") Then
            Me.LinkIdiomes.Text = JJ.Intern.WebForm.HeadWebForm.IdiomesAlternatius(My.Request.Url.ToString, Titol)
        End If
    End Sub
End Class
