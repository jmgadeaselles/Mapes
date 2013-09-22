<%@ WebHandler Language="VB" Class="FileHandler" %>

Imports System
Imports System.Web
Imports System.IO
Imports System.Data.SqlClient

Public Class FileHandler : Implements IHttpHandler, System.Web.SessionState.IReadOnlySessionState
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest        
        Dim Resposta As String = "error"
        Dim IdFoto As Guid = Guid.NewGuid()
        Dim ConfigFotos As JJ.DadesBase.Fotos.FotoClass.FotosEscalades
        Dim Ruta, Extensio As String
        
        
        Try
            Dim IdLloc As Guid = New Guid(My.Request.Form("IdLloc"))
            If Not JJ.Sesio.Usuari.Validat Then
                Resposta = "error.usr"
            ElseIf Not JJ.Geo.Llocs.LlocClass.PermetreGuardar(IdLloc) Then
                Resposta = "error"
            Else
                'Ruta i nom de les fotos
                Dim NomArxiu As String = Path.GetFileName(context.Request.Files(0).FileName)
                Extensio = Path.GetExtension(context.Request.Files(0).FileName).ToLower
                Ruta = context.Server.MapPath("~/img/fotos")
                Dim Hui As Date = Now()
                Ruta = Ruta + "\" + Hui.Year.ToString
                If Not System.IO.Directory.Exists(Ruta) Then
                    System.IO.Directory.CreateDirectory(Ruta)
                End If
                Ruta = Ruta + "\" + Hui.Month.ToString
                If Not System.IO.Directory.Exists(Ruta) Then
                    System.IO.Directory.CreateDirectory(Ruta)
                End If
                Ruta = Ruta + "\" + NomArxiu.Substring(0, NomArxiu.Length - Extensio.Length) + "_" + IdFoto.ToString
                'Guardem les fotos            
                ConfigFotos = New JJ.DadesBase.Fotos.FotoClass.FotosEscalades()
                For i As Integer = 0 To ConfigFotos.Count - 1
                    Dim Tamany As JJ.Grafics.Size = ConfigFotos.Item(i)
                    JJ.Grafics.Imatges.RedimensionarImatge(context.Request.Files(0).InputStream, Tamany.Width, Tamany.Height).Save(Ruta + "_" + Tamany.Width.ToString + Extensio)
                Next
                'dades            
                'Dim IdLloc As Guid = New Guid(My.Request.Form("IdLloc"))
                Dim Idioma As String = My.Request.Form("CodiIdioma")
                Dim Comentari As String = My.Request.Form("Comentari")
                If Me.GuardarDades(IdLloc, IdFoto, Idioma, Ruta, Extensio, Comentari) Then
                    Resposta = "success"
                Else
                    Throw New Exception("No s'ha pogut guardar les dades.")
                End If
            End If
        Catch ex As Exception
            'Borrem les fotos per l'errada
            Try
                If ConfigFotos IsNot Nothing Then
                    For i As Integer = 0 To ConfigFotos.Count - 1
                        Dim Tamany As JJ.Grafics.Size = ConfigFotos.Item(i)
                        System.IO.File.Delete(Ruta + "_" + Tamany.Width.ToString + Extensio)
                        'JJ.Grafics.Imatges.RedimensionarImatge(context.Request.Files(0).InputStream, Tamany.Width, Tamany.Height).Save(Ruta + "_" + Tamany.Width.ToString + Extensio)
                    Next
                End If
            Catch ex1 As Exception
                JJ.Registre.RegistrarErrada(ex1)
            End Try
            Resposta = "error"
            JJ.Registre.RegistrarErrada(ex)
        End Try
        context.Response.Clear()
        context.Response.ContentType = "text/plain"
        context.Response.Write(Resposta)
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property
    
    
    Private Function GuardarDades(ByVal IdLloc As Guid, ByVal IdFoto As Guid, ByVal Idioma As String, ByVal Arxiu As String, ByVal Extensio As String, ByVal Comentari As String) As Boolean
        Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
        Dim Comand As New SqlCommand
        Try
            'Throw New Exception("Errada de proves")
            Conexio.Open()
            Comand.Connection = Conexio
            If Not JJ.Geo.Llocs.LlocClass.PermetreGuardar(Comand, IdLloc) Then
                Return False
            Else
                'transaccio
                Comand.Parameters.Clear()
                Comand.CommandText = "BEGIN TRAN"
                Comand.ExecuteNonQuery()
                'Foto
                Comand.CommandText = "INSERT INTO Fotos (id,ca_lloc,arxiu,extensio,ca_autor) VALUES (@id,@ca_lloc,@arxiu,@extensio,@ca_autor)"
                Comand.Parameters.AddWithValue("@id", IdFoto)
                Comand.Parameters.AddWithValue("@ca_lloc", IdLloc)
                Comand.Parameters.AddWithValue("@arxiu", Arxiu)
                Comand.Parameters.AddWithValue("@extensio", Extensio)
                Comand.Parameters.AddWithValue("@ca_autor", JJ.Sesio.Usuari.Id)
                Comand.ExecuteNonQuery()
                'Comentari     
                Comand.Parameters.Clear()
                Comand.CommandText = "INSERT INTO ComentariFoto (ca_foto,ca_idioma,comentari) VALUES (@ca_foto,@ca_idioma,@comentari)"
                Comand.Parameters.AddWithValue("@ca_foto", IdFoto)
                Comand.Parameters.AddWithValue("@ca_idioma", Idioma)
                Comand.Parameters.AddWithValue("@comentari", Comentari)
                Comand.ExecuteNonQuery()
                'Historic
                GuardarDades = JJ.Geo.Llocs.Fotos.FotoClass.GuardarHistoricComentari(Comand, IdFoto, Idioma, Comentari)                
                'fi transaccio
                Comand.Parameters.Clear()
                Comand.CommandText = "COMMIT TRAN"
                Comand.ExecuteNonQuery()
            End If
        Catch ex As Exception
            JJ.Registre.RegistrarErrada(ex)
            Comand.Parameters.Clear()
            Comand.CommandText = "ROLLBACK TRAN"
            Comand.ExecuteNonQuery()
            Return False
        Finally
            Conexio.Close()
        End Try
        
    End Function

End Class