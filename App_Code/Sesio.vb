Imports Microsoft.VisualBasic
Imports System.Data.SqlClient


Namespace JJ


    Public MustInherit Class Sesio

        Public Shared ReadOnly Property Usuari() As UsuariClass
            Get                
                If System.Web.HttpContext.Current.Session("USUARI") Is Nothing Then
                    Return New UsuariClass()
                Else
                    Return System.Web.HttpContext.Current.Session("USUARI")
                End If
            End Get
        End Property


        Public Shared ReadOnly Property EsDispositiuMobil() As Boolean
            Get
                Try
                    If My.Request.Browser.IsMobileDevice Then
                        Return True
                    ElseIf My.Request.UserAgent IsNot Nothing Then
                        Dim s As String = My.Request.UserAgent.ToUpper
                        Return s.Contains("IPHONE") OrElse s.Contains("ANDROID") _
                            OrElse s.Contains("BLACKBERRY") OrElse s.Contains("MOBILE") _
                            OrElse s.Contains("WINDOWS CE") OrElse s.Contains("OPERA MINI") _
                            OrElse s.Contains("PALM")
                    Else
                        Return False
                    End If
                Catch ex As Exception
                    Return False
                End Try
            End Get
        End Property


        Public Class UsuariClass
            Dim _Id As Guid
            Dim _Alies, _Nom, _Cognoms, _Mail, _FotoBase, _Extensio As String
            Dim _Tipus As JJ.Usuaris.TipusUsuari
            Dim _DadesUsuariWeb As JJ.Usuaris.UsuariDadesClass

            Public Sub New()
                Me.Clear()
            End Sub

            Public ReadOnly Property Id As Guid
                Get
                    Return Me._Id
                End Get
            End Property

            Public ReadOnly Property Tipus() As JJ.Usuaris.TipusUsuari
                Get
                    Return Me._Tipus
                End Get
            End Property

            Public ReadOnly Property [Alias] As String
                Get
                    Return Me._Alies
                End Get
            End Property

            Public ReadOnly Property Nom As String
                Get
                    Return Me._Nom
                End Get
            End Property

            Public ReadOnly Property Cognoms As String
                Get
                    Return Me._Cognoms
                End Get
            End Property

            Public ReadOnly Property Mail As String
                Get
                    Return Me._Mail
                End Get
            End Property

            Public ReadOnly Property Validat() As Boolean
                Get
                    Return (Me._Id <> Guid.Empty)
                End Get
            End Property

            Public Function Validar(ByVal Mail As String, ByVal Contrasenya As String) As Boolean
                Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
                Dim Comand As New SqlCommand
                Try
                    Conexio.Open()
                    Comand.Connection = Conexio
                    Comand.CommandText = "SELECT id, alies, mail, contrasenya, nom, cognoms, img, img_extensio, tipus, actiu, validat FROM Usuaris WHERE mail=@mail AND contrasenya=@contrasenya"
                    Comand.Parameters.AddWithValue("@mail", Mail)
                    Comand.Parameters.AddWithValue("@contrasenya", Contrasenya)
                    Dim Dades As SqlDataReader = Comand.ExecuteReader()
                    If Dades.Read Then
                        If CType(Dades("validat"), Boolean) And CType(Dades("actiu"), Boolean) And (Mail = CType(Dades("mail"), String)) And (Contrasenya = CType(Dades("contrasenya"), String)) Then
                            Me._Id = Dades("id")
                            Me._Alies = Dades("alies")
                            Me._Nom = Dades("nom")
                            Me._Cognoms = Dades("cognoms")
                            Me._Mail = Dades("mail")
                            Me._FotoBase = Dades("img")
                            Me._Extensio = Dades("img_extensio")
                            Me._Tipus = IIf(Dades("tipus") = "0", JJ.Usuaris.TipusUsuari.Administrador, JJ.Usuaris.TipusUsuari.Usuari)
                            System.Web.HttpContext.Current.Session.Timeout = 9999
                            System.Web.HttpContext.Current.Session.Add("USUARI", Me)
                            If System.Web.HttpContext.Current.Session("USUARI") Is Nothing Then
                                Registre.RegistrarErrada("Usuari " + Me._Alies + " validat, pero no s'ha pogur crear les dades de sesió.")
                            End If
                        End If
                    End If
                    Dades.Close()
                    If Me.Validat() Then
                        If Config.General.Sesio.RegistrarIniciSesioOK Then
                            Registre.RegistrarEvent("S'ha validat l'usuari " + Me._Alies)
                        End If
                    Else
                        If Config.General.Sesio.RegistrarIniciSesioInvalit Then
                            Registre.RegistrarAdvertencia("Validació d'usuari incorrecta. Mail: " + Mail + ".   IP: " + System.Web.HttpContext.Current.Request.UserHostAddress)
                        End If
                    End If
                    Return Me.Validat()
                Catch ex As Exception
                    JJ.Registre.RegistrarErrada(ex)
                    Return False
                Finally
                    Conexio.Close()
                End Try
            End Function

            Public ReadOnly Property DadesWeb() As JJ.Usuaris.UsuariDadesClass
                Get
                    If Me._DadesUsuariWeb Is Nothing Then
                        Me._DadesUsuariWeb = New JJ.Usuaris.UsuariDadesClass(Me._Id, Me._Alies, Me._Nom, Me._Cognoms, Me._FotoBase, Me._Extensio)
                    End If
                    Return Me._DadesUsuariWeb
                End Get
            End Property


            Public Sub Desconectar()
                If JJ.Config.General.Sesio.RegistrarIniciSesioOK() Then
                    JJ.Registre.RegistrarEvent("S'ha desconectat l'usuari '" + Me._Alies + "'.")
                End If
                Me.Clear()
            End Sub


            Private Sub Clear()
                Me._Id = Guid.Empty
                Me._Alies = ""
                Me._Nom = ""
                Me._Cognoms = ""
                Me._Mail = ""
                Me._Tipus = Usuaris.TipusUsuari.Usuari
                Me._DadesUsuariWeb = Nothing
            End Sub

        End Class


        Public Class Idioma

            Private Shared Function GetIdioma() As String
                Try
                    If System.Web.HttpContext.Current.Session("IDIOMA") IsNot Nothing Then
                        'JJ.Registre.RegistrarEvent("1: " + System.Web.HttpContext.Current.Session("IDIOMA") + "(Abans " + System.Globalization.CultureInfo.CurrentCulture.ToString + ")")
                        'System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(System.Web.HttpContext.Current.Session("IDIOMA"))
                        'System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CreateSpecificCulture(System.Web.HttpContext.Current.Session("IDIOMA"))
                        'JJ.Registre.RegistrarEvent("1: " + System.Web.HttpContext.Current.Session("IDIOMA") + "(Despres " + System.Globalization.CultureInfo.CurrentCulture.ToString + ")")
                        Return System.Web.HttpContext.Current.Session("IDIOMA")
                    Else
                        'JJ.Registre.RegistrarEvent("2")
                        Dim IdiomesUsuari() As String
                        If System.Web.HttpContext.Current.Request.UserLanguages IsNot Nothing Then
                            IdiomesUsuari = System.Web.HttpContext.Current.Request.UserLanguages
                            Dim IdiomesWeb As New JJ.Idiomes.LlistatIdiomesWeb()
                            For Each Temp As String In IdiomesUsuari
                                Dim Idioma As String = Temp.Substring(0, 2)
                                For i As Integer = 0 To IdiomesWeb.Count - 1
                                    If Idioma = IdiomesWeb(i).Codi Then
                                        Return Idioma
                                    End If
                                Next
                            Next
                        End If
                        'JJ.Registre.RegistrarEvent("4: " + System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName)
                        'If System.Globalization.CultureInfo.CurrentCulture Is Nothing Then
                        '    Return "es"
                        'Else
                        Return System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName
                        'End If
                    End If
                Catch ex As Exception
                    JJ.Registre.RegistrarErrada(ex.ToString)
                    Return "es"
                End Try
            End Function


            Public Shared Property Valor As String
                Get
                    Dim I As String = GetIdioma()
                    If I.IndexOf("-") >= 0 Then
                        Return Split(I, "-")(0)
                    Else
                        Return I
                    End If
                End Get
                Set(ByVal value As String)
                    If System.Web.HttpContext.Current.Session("IDIOMA") IsNot Nothing Then
                        System.Web.HttpContext.Current.Session.Add("IDIOMA", value)
                    Else
                        System.Web.HttpContext.Current.Session("IDIOMA") = value
                    End If
                    If (value.IndexOf("-") < 0) Then
                        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo(value + "-" + GetPaisIdioma(value))
                        System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo(value + "-" + GetPaisIdioma(value))
                        'System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.Name)
                        'System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.Name)
                    Else
                        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo(value)
                        System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo(value)
                    End If
                    'Guardamos una cookie
                    Dim Cookie As New System.Web.HttpCookie("IDIOMA", value)
                    Cookie.Expires = Now.AddYears(10)
                    My.Response.Cookies.Add(Cookie)
                End Set
            End Property


            Private Function GetIdiomaPaisUsuarioPorDefecto() As String
                Return System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName
            End Function


            Public Shared Function GetPaisIdioma(ByVal Idioma As String) As String
                Select Case Idioma
                    Case "ca"
                        Return "ES"
                    Case "en"
                        Return "US"
                    Case Else
                        Return Idioma.ToUpper
                End Select
            End Function

        End Class





        'Public Class Pais

        '    Public Shared Property Valor As String
        '        Get
        '            If System.Web.HttpContext.Current.Session("PAIS") IsNot Nothing Then
        '                Return System.Web.HttpContext.Current.Session("PAIS")
        '            Else
        '                Return System.Globalization.CultureInfo.CurrentUICul
        '            End If
        '        End Get
        '        Set(ByVal value As String)
        '            If System.Web.HttpContext.Current.Session("PAIS") IsNot Nothing Then
        '                System.Web.HttpContext.Current.Session.Add("PAIS", value)
        '            Else
        '                System.Web.HttpContext.Current.Session("PAIS") = value
        '            End If
        '            'Guardamos una cookie
        '            Dim Cookie As New System.Web.HttpCookie("PAIS", value)
        '            Cookie.Expires = Now.AddYears(10)
        '            My.Response.Cookies.Add(Cookie)
        '        End Set
        '    End Property


        '    Private Function GetIdiomaPaisUsuarioPorDefecto() As String
        '        Return System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageNam
        '    End Function



        'End Class


    End Class






End Namespace

