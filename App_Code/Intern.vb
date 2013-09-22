Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Math
Imports System.IO

Namespace JJ.Intern


    Namespace Funcions


        Public Class BBDD


            Public Shared Function GetField(ByVal Camp As String, ByVal Taula As String, ByVal CampFiltre As String, ByVal ValorFiltre As Object) As Object
                Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
                Dim Comand As New SqlCommand
                Try
                    Conexio.Open()
                    Comand.Connection = Conexio
                    Comand.CommandText = "SELECT " + Camp + " AS DATA FROM " + Taula + " WHERE " + CampFiltre + "=@valor"
                    Comand.Parameters.AddWithValue("@valor", ValorFiltre)
                    Dim Dades As SqlDataReader = Comand.ExecuteReader()
                    Dades.Read()
                    GetField = Dades("DATA")
                    Dades.Close()
                Catch ex As Exception
                    JJ.Registre.RegistrarErrada(ex.ToString)
                    Throw ex
                Finally
                    Conexio.Close()
                End Try
            End Function


            Public Shared Function SetField(ByVal CampSet As String, ByVal ValorSet As Object, ByVal Taula As String, ByVal CampFiltre As String, ByVal ValorFiltre As Object) As Boolean
                Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
                Dim Comand As New SqlCommand
                Try                    
                    Conexio.Open()
                    Comand.Connection = Conexio
                    Comand.CommandText = "UPDATE " + Taula + " SET " + CampSet + "=@valor_set" + " WHERE " + CampFiltre + "=@valor_filtre"
                    Comand.Parameters.AddWithValue("@valor_set", ValorSet)
                    Comand.Parameters.AddWithValue("@valor_filtre", ValorFiltre)
                    Return (Comand.ExecuteNonQuery() > 0)
                Catch ex As Exception
                    JJ.Registre.RegistrarErrada(ex.ToString)
                    Return False
                Finally
                    Conexio.Close()
                End Try
            End Function


        End Class


        Public Class ValidacioDades

            Public Shared Function GetDate(ByVal Valor As String, Optional ByVal Format As String = "dd/mm/yyyy") As Date
                If Valor = "" Then
                    Return Date.MinValue
                Else
                    Try
                        Return DateTime.ParseExact(Valor, Format, System.Globalization.CultureInfo.InvariantCulture)
                    Catch ex As Exception
                        Return Date.MinValue
                    End Try
                End If
            End Function


        End Class


        Public Class Web

            Public Shared Function GetURLPaginaActual() As String
                Dim IniciParam As Integer = My.Request.RawUrl.IndexOf("?")
                If IniciParam < 0 Then
                    Return My.Request.RawUrl
                Else
                    Return My.Request.RawUrl.Substring(0, IniciParam)
                End If
            End Function


        End Class


        Public Class Math


            Public Shared Function ConvertirDecimalAGraus(ByVal Valor As Double) As String                
                Dim DecDegAbs As Decimal = Abs(Valor)
                Dim ReturnValue As String = "'"
                Dim DegreeSymbol As String = "°"
                Dim MinutesSymbol As String = "’"
                Dim SecondsSymbol As String = """"
                Dim Degrees As String = Truncate(DecDegAbs) & DegreeSymbol
                Dim MinutesDecimal As Decimal = (DecDegAbs - Truncate(DecDegAbs)) * 60
                Dim SecondsDecimal As Decimal = (MinutesDecimal - Truncate(MinutesDecimal))
                Dim Minutes As String = Truncate(MinutesDecimal) & MinutesSymbol
                Dim Seconds As String = String.Format("{0:##.0000}", (SecondsDecimal * 60)) & SecondsSymbol
                ReturnValue = Degrees & " " & Minutes & " " & Seconds
                Return ReturnValue
            End Function



        End Class



        Public Class Textos


            Public Shared Function GetParaules(ByVal Text As String) As String()
                Text = Text.Trim
                While Text.IndexOf("  ") >= 0
                    Text = Text.Replace("  ", " ")
                End While
                Return Split(Text, " ")
            End Function


        End Class

    End Namespace


    Namespace WebForm


        Public Class PaginaIdiomaBase
            Inherits System.Web.UI.Page

            Protected Overrides Sub InitializeCulture()
                If (JJ.Sesio.Idioma.Valor.IndexOf("-") < 0) Then
                    System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo(JJ.Sesio.Idioma.Valor + "-" + JJ.Sesio.Idioma.GetPaisIdioma(JJ.Sesio.Idioma.Valor))
                    System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CreateSpecificCulture(JJ.Sesio.Idioma.Valor + "-" + JJ.Sesio.Idioma.GetPaisIdioma(JJ.Sesio.Idioma.Valor))
                Else
                    System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo(JJ.Sesio.Idioma.Valor)
                    System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CreateSpecificCulture(JJ.Sesio.Idioma.Valor)
                End If
                MyBase.InitializeCulture()




                'If JJ.Sesio.Idioma.Valor.IndexOf("-") < 0 Then
                '    System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.Name)
                '    System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CreateSpecificCulture(System.Threading.Thread.CurrentThread.CurrentCulture.Name)
                'Else
                '    System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo(JJ.Sesio.Idioma.Valor)
                '    System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CreateSpecificCulture(JJ.Sesio.Idioma.Valor)
                'End If
                'MyBase.InitializeCulture()
            End Sub
        End Class

        Public Class HeadWebForm

            Public Shared Function Generar(ByVal Titol As String, ByVal ParaulesClau As String, ByVal Descripcio As String, Optional ByVal ValorRobots As String = "all") As String                
                Return "<title>" + Titol + "</title>" + _
                        "<meta http-equiv=""content-language"" content=""" + JJ.Sesio.Idioma.Valor + """>" + _
                        "<meta name=""Keywords"" content=""" + ParaulesClau + """ />" + _
                        "<meta name=""Description"" content=""" + Descripcio + """ />" + _
                        "<meta name=""Robots"" content=""" + ValorRobots + """ />" + _
                        "<meta http-equiv=""Pragma"" content=""no-cache"" >" + _
                        "<meta http-equiv=""expires"" content=""-1"" >"
            End Function


            Public Shared Function IdiomesAlternatius(ByVal URL As String, ByVal Titol As String) As String
                Try
                    Dim Parametres As System.Collections.Specialized.StringCollection = ObtindreParametresGet(URL)
                    Dim i As Integer = URL.IndexOf("?")
                    If i < 0 Then
                        URL += "?"
                    Else
                        URL = URL.Substring(0, i + 1)
                    End If
                    For i = 0 To Parametres.Count - 1
                        URL += Parametres(i) + "&"
                    Next
                    Dim Idiomes As New JJ.Idiomes.LlistatIdiomesWeb()
                    Dim Valors As String = ""
                    For x As Integer = 0 To Idiomes.Count - 1
                        Dim IdiomaLink As JJ.Idiomes.IdiomaClass = Idiomes(x)
                        Valors += "<link title=""" + Titol + """ Type=""text/html"" rel=""alternate"" hreflang=""" + IdiomaLink.Codi + """ href=""" + URL + "i=" + IdiomaLink.Codi + """ />" + vbCrLf
                    Next
                    Return Valors
                Catch ex As Exception
                    JJ.Registre.RegistrarErrada(ex.ToString)
                    Return ""
                End Try
            End Function


            Private Shared Function ObtindreParametresGet(ByVal URL As String) As System.Collections.Specialized.StringCollection
                Dim Parametres As New System.Collections.Specialized.StringCollection()
                Dim i As Integer = URL.IndexOf("?")
                If i > 0 Then
                    URL = URL.Substring(i + 1)
                    Dim Valors As String() = Split(URL, "&")
                    For i = 0 To Valors.Length - 1
                        Dim Parts As String() = Split(Valors(i), "=")
                        If Parts.Length = 2 Then
                            If Parts(0).ToUpper <> "I" Then
                                Parametres.Add(Parts(0) + "=" + Parts(1))
                            End If
                        End If
                    Next
                End If
                Return Parametres
            End Function


        End Class


    End Namespace


    Public Class ClientWebClass

        Public Shared Function Navegar(ByVal URL As String) As String
            Try
                Dim Client As New System.Net.WebClient()
                Client.Headers.Add("user_agent", My.Request.UserAgent)
                Dim Dades As Stream = Client.OpenRead(URL)
                Dim reader As New StreamReader(Dades)
                Dim s As String = reader.ReadToEnd()
                Navegar = s
                Dades.Close()
                reader.Close()
            Catch ex As Exception
                JJ.Registre.RegistrarErrada(ex.ToString)
                Return ""
            End Try
        End Function

    End Class


End Namespace