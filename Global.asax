<%@ Application Language="VB" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Código que se ejecuta al iniciarse la aplicación
    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Código que se ejecuta al cerrarse la aplicación        
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim Errada As Exception = Server.GetLastError().GetBaseException()
            If Errada Is Nothing Then
                JJ.Registre.RegistrarErrada("Error de aplicación en Global.asax. No s'ha pogut recuperat la ultima errada ja que esta buida.")
            Else
                JJ.Registre.RegistrarErrada("Error de aplicación en Global.asax. " + Errada.ToString)
            End If
        Catch ex As Exception
            JJ.Registre.RegistrarErrada("Errada produïda en el Global.asax: (Application_Error)" + VBCRLF + ex.ToString)
        End Try
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        Try
            'Registramos si es un buscador....
            If My.Request.Browser.Crawler Then
                JJ.Registre.RegistrarEvent("Visita boot: " + My.Request.UserAgent)
            End If
            'Idioma            
            Dim Cookie As System.Web.HttpCookie = My.Request.Cookies("IDIOMA")
            If Cookie IsNot Nothing Then
                JJ.Sesio.Idioma.Valor = Cookie.Value
            ElseIf My.Request.UserLanguages IsNot Nothing AndAlso My.Request.UserLanguages.Length > 0 Then
                JJ.Sesio.Idioma.Valor = My.Request.UserLanguages(0)
            End If
        Catch ex As Exception
            JJ.Registre.RegistrarErrada(ex.ToString)            
        End Try
        'Es un dispositiu mobil
        'INHABILITAT DE MOMENT, FINS QUE ES POSE LA VERSIO MOBIL
        If JJ.Sesio.EsDispositiuMobil Then
            If My.Request.Url.ToString.ToLower.IndexOf(JJ.Config.General.Web.Mobile.GetURLBase().ToLower) < 0 Then
                Dim URL As String = My.Request.Url.ToString.ToLower.Replace("/mapes/", "/")
                URL = URL.Replace(JJ.Config.General.Web.GetURLBase(), JJ.Config.General.Web.Mobile.GetURLBase())
                My.Response.Redirect(URL)
            End If
        End If
    End Sub


</script>