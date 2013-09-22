Imports Microsoft.VisualBasic
Imports System.Data.SqlClient

Namespace JJ.Config


    Public NotInheritable Class BBDD

        Public Shared ReadOnly Property CadenaConexio() As String
            Get
                Return ConfigurationManager.ConnectionStrings.Item("ApplicationServices").ConnectionString
            End Get
        End Property

    End Class



    Public Class General

        Public Class Sesio


            Public Shared Property RegistrarIniciSesioOK() As Boolean
                Get
                    Return GetValor("SESIO.INICI-OK.REGISTRAR")
                End Get
                Set(value As Boolean)
                    SetValor("SESIO.INICI-OK.REGISTRAR", value)
                End Set
            End Property

            Public Shared Property RegistrarIniciSesioInvalit() As Boolean
                Get
                    Return GetValor("SESIO.INICI-ERROR.REGISTRAR")
                End Get
                Set(value As Boolean)
                    SetValor("SESIO.INICI-ERROR.REGISTRAR", value)
                End Set
            End Property

        End Class


        Public Class Usuaris

            Public Shared Property PermetreAltesUsuaris() As Boolean 
                Get
                    Return GetValor("PERMETRE_ALTES_USR")
                End Get
                Set(value As Boolean)
                    SetValor("PERMETRE_ALTES_USR", value)
                End Set
            End Property


            Public Shared Property ValidarMailAlAlta() As Boolean
                Get
                    Return GetValor("VALIDAR_MAIL_ALTA_USUARI")
                End Get
                Set(value As Boolean)
                    SetValor("VALIDAR_MAIL_ALTA_USUARI", value)
                End Set
            End Property


        End Class


        Public Class Web


            Public Shared Function GetURLBase() As String
                Dim Context As HttpContext = HttpContext.Current
                Dim URL As String = Context.Request.Url.Scheme + System.Uri.SchemeDelimiter + Context.Request.Url.Host + IIf(Context.Request.Url.Port <> 80, ":" + Context.Request.Url.Port.ToString, "") + "/"
                If URL.ToLower.Contains("/mapes/") Then
                    URL = URL.ToLower.Replace("/mapes/", "/")
                End If
                Return URL
                'Dim URL As String
                'If GetWebLocal() Then
                '    '    'URL = Context.Request.Url.Scheme + "://" + Context.Request.Url.Authority + Context.Request.ApplicationPath.TrimEnd("/") + "/"
                '    URL = "http://localhost/"
                'Else
                '    URL = "http://www.ocimap.com/"
                '    'URL = Context.Request.Url.Scheme(+System.Uri.SchemeDelimiter + Context.Request.Url.Host + IIf(Context.Request.Url.Port <> 80, ":" + Context.Request.Url.Port.ToString, "") + "/")
                'End If
                'Return URL
            End Function

            Public Shared Function GetURL(ByVal Path As String) As String
                Dim URL As String = GetURLBase()
                If Path.StartsWith("/") Then
                    Return URL + Path.Substring(1)
                Else
                    Return URL + Path
                End If
            End Function

            Public Shared Function GetWebLocal() As Boolean
                Return System.Web.HttpContext.Current.Request.IsLocal
                'Dim URL As String = GetURLBase().ToString
                'If URL.ToUpper.IndexOf("LOCALHOST") >= 0 Then
                '    Return True
                'End If
                'If URL.IndexOf("127.0.0") >= 0 Then
                '    Return True
                'End If
                'Return False
            End Function


            Public Class Mobile



                Public Shared Function GetURLBase() As String
                    Dim Context As HttpContext = HttpContext.Current
                    Dim URL As String = Context.Request.Url.Scheme + System.Uri.SchemeDelimiter + Context.Request.Url.Host + IIf(Context.Request.Url.Port <> 80, ":" + Context.Request.Url.Port.ToString, "") + "/m/"
                    If URL.ToLower.Contains("/mapes/") Then
                        URL = URL.ToLower.Replace("/mapes/", "/")
                    End If
                    Return URL
                End Function

                Public Shared Function GetURL(ByVal Path As String) As String
                    Dim URL As String = GetURLBase()
                    If Path.StartsWith("/") Then
                        Return URL + Path.Substring(1)
                    Else
                        Return URL + Path
                    End If
                End Function

                Public Shared Function GetWebLocal() As Boolean
                    Return System.Web.HttpContext.Current.Request.IsLocal
                End Function



            End Class


        End Class

        Public Class Seguretat


            Public Shared Property PermetreCrearLlocsNous() As Boolean
                Get
                    Return GetValor("LLOCS.NOUS.CREAR")
                End Get
                Set(value As Boolean)
                    SetValor("LLOCS.NOUS.CREAR", value)
                End Set
            End Property

            Public Shared Property PermetreEdicioLlocs() As Boolean
                Get
                    Return GetValor("LLOCS.EDITABLES")
                End Get
                Set(value As Boolean)
                    SetValor("LLOCS.EDITABLES", value)
                End Set
            End Property

            Public Shared ReadOnly Property UsuariActiu() As Boolean
                Get
                    Try
                        If Not JJ.Sesio.Usuari.Validat Then
                            Return False
                        Else
                            Return CType(JJ.Intern.Funcions.BBDD.GetField("actiu", "Usuaris", "id", JJ.Sesio.Usuari.Id), Boolean)
                        End If
                    Catch ex As Exception
                        JJ.Registre.RegistrarErrada(ex.ToString)
                        Return False
                    End Try
                End Get
            End Property


        End Class

        Private Shared Function GetValor(ByVal Variable As String) As Object
            Dim Valor As Object = Nothing
            Dim Tipus As String = ""
            Dim Conexio As New SqlConnection(BBDD.CadenaConexio)
            Dim Comand As New SqlCommand
            Try
                Conexio.Open()
                Comand.Connection = Conexio
                Comand.CommandText = "SELECT tipus,valor FROM Config WHERE variable=@variable"
                Comand.Parameters.AddWithValue("@variable", Variable)
                Dim Dades As SqlDataReader = Comand.ExecuteReader()
                If Not Dades.Read Then
                    Throw New Exception("No s'ha trobat la configuració per a la variable '" + Variable + "'.")
                Else
                    Valor = Dades("valor")
                    Tipus = Dades("tipus")
                End If
                Dades.Close()
            Catch ex As Exception
                JJ.Registre.RegistrarErrada(ex)
                Throw ex
            Finally
                Conexio.Close()
            End Try
            'Validacio de les dades
            Select Case Tipus
                Case "STRING"
                    Return Valor
                Case "BOOLEAN"
                    Return CType(Valor, Boolean)
                Case "INTEGER"
                    Return CType(Valor, Integer)
                Case "DOUBLE"
                    Return CType(Valor, Double)
                Case "DATE"
                    Return CType(Valor, Date)
                Case Else
                    Throw New Exception("La variable de configuració '" + Variable + "' es d'un tipus no esperat: '" + Tipus + "'")
            End Select
        End Function

        Private Shared Function SetValor(ByVal Variable As String, ByVal Valor As Object) As Boolean
            Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
            Dim Comand As New SqlCommand
            Try
                Conexio.Open()
                Comand.Connection = Conexio
                Comand.CommandText = "UPDATE Config SET valor=@valor WHERE variable=@variable"
                Comand.Parameters.AddWithValue("@valor", Valor)
                Comand.Parameters.AddWithValue("@variable", Variable)
                Return (Comand.ExecuteNonQuery() = 1)
            Catch ex As Exception
                JJ.Registre.RegistrarErrada(ex.ToString)
                Return False
            Finally
                Conexio.Close()
            End Try
        End Function

    End Class



    Public Class Imatges

        Public Shared ReadOnly Property CarpetaImatgesTemp() As String
            Get
                Return JJ.Config.General.Web.GetURL("/temp")
            End Get
        End Property


        

    End Class




End Namespace