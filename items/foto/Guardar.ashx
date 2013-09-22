<%@ WebHandler Language="VB" Class="Guardar" %>

Imports System
Imports System.Web

Public Class Guardar : Implements IHttpHandler, System.Web.SessionState.IReadOnlySessionState
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim Resposta As String = ""
        Try
            Dim Id As Guid = New Guid(My.Request.Form("IdFoto"))
            Dim IdLloc As Guid = JJ.Intern.Funcions.BBDD.GetField("ca_lloc", "Fotos", "id", Id)
            If Not JJ.Sesio.Usuari.Validat Then                
                Resposta = "error.usr"                
            ElseIf Not JJ.Geo.Llocs.LlocClass.PermetreGuardar(IdLloc) Then
                Resposta = "error.general"
            Else
                'Dim Id As Guid = New Guid(My.Request.Form("IdFoto"))
                Dim Idioma As String = My.Request.Form("Idioma")
                Dim Comentari As String = My.Request.Form("ComentariEdt")
                If JJ.Geo.Llocs.Fotos.FotoClass.SetComentari(Id, Idioma, Comentari) Then
                    Resposta = "success"
                Else
                    Resposta = "error.general"
                End If
            End If
        Catch ex As Exception
            JJ.Registre.RegistrarErrada(ex)
            Resposta = "error.general"
        End Try
        'Tornem una resposta
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