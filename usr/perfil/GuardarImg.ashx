<%@ WebHandler Language="VB" Class="GuardarImg" Debug="true" %>

Imports System
Imports System.Web
Imports System.IO

Public Class GuardarImg : Implements IHttpHandler, System.Web.SessionState.IReadOnlySessionState
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest       
        Dim Resposta As String = "error"
        Dim Ruta, Extensio As String
        'Dim IdFoto As Guid = Guid.NewGuid()
        Dim ConfigFotos As JJ.DadesBase.Fotos.FotoClass.FotosUsuarisEscalades
        Try
            ConfigFotos = New JJ.DadesBase.Fotos.FotoClass.FotosUsuarisEscalades
            If Not JJ.Sesio.Usuari.Validat Then
                Resposta = "error.usr"
            Else
                'Ruta i nom de les fotos                
                Dim NomArxiu As String = Path.GetFileName(context.Request.Files(0).FileName)
                Extensio = Path.GetExtension(context.Request.Files(0).FileName).ToLower
                Ruta = context.Server.MapPath("~/img/usr")
                Dim Hui As Date = Now()
                Ruta = Ruta + "\" + Hui.Year.ToString
                If Not System.IO.Directory.Exists(Ruta) Then
                    System.IO.Directory.CreateDirectory(Ruta)
                End If
                Ruta = Ruta + "\" + Hui.Month.ToString
                If Not System.IO.Directory.Exists(Ruta) Then
                    System.IO.Directory.CreateDirectory(Ruta)
                End If
                'Ruta = Ruta + "\" + NomArxiu.Substring(0, NomArxiu.Length - Extensio.Length) + "_" + IdFoto.ToString
                Ruta = Ruta + "\" + JJ.Sesio.Usuari.Id.ToString
                'Guardem les fotos...
                ConfigFotos = New JJ.DadesBase.Fotos.FotoClass.FotosUsuarisEscalades()
                For i As Integer = 0 To ConfigFotos.Count - 1
                    Dim Tamany As JJ.Grafics.Size = ConfigFotos.Item(i)
                    Try
                        System.IO.File.Delete(Ruta + "_" + Tamany.Width.ToString + Extensio)
                    Catch ex As Exception
                        'No fem res
                    End Try
                    JJ.Grafics.Imatges.RedimensionarImatge(context.Request.Files(0).InputStream, Tamany.Width, Tamany.Height).Save(Ruta + "_" + Tamany.Width.ToString + Extensio)
                Next
                'Guardem les dades
                If Not JJ.Usuaris.UsuariClass.ActualitzarImatge(JJ.Sesio.Usuari.Id, Ruta, Extensio) Then
                    Throw New Exception("No s'han pogut guardar les dades referent a la nova imatge d'usuari")
                Else
                    JJ.Sesio.Usuari.DadesWeb.RefrescaImatgePerfil(Ruta, Extensio)
                    Resposta = "success"
                End If
            End If
        Catch ex As Exception
            'Esborrem les fotos per error...
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

End Class