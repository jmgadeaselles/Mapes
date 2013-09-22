<%@ WebHandler Language="VB" Class="Guardar" %>

Imports System
Imports System.Web

Public Class Guardar : Implements IHttpHandler, System.Web.SessionState.IReadOnlySessionState
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest        
        Dim Resposta As String = ""
        If Not JJ.Sesio.Usuari.Validat Then
            Resposta = "error.usr"
        Else
            Try
                Dim IdLloc As Guid = New Guid(My.Request.QueryString("l"))
                Dim Idioma As String = My.Request.QueryString("i")
                Dim Comentari As String = My.Request.QueryString("t")
                Dim Id As Guid = JJ.Geo.Llocs.LlocClass.AfegirComentari(IdLloc, Idioma, Comentari)
                If Id = Guid.Empty Then
                    Resposta = "error"
                Else
                    Resposta = "success:" + Id.ToString
                End If
            Catch ex As Exception
                JJ.Registre.RegistrarErrada("Errada al registrar comentari: " + ex.ToString)
                Resposta = "error"
            End Try
        End If
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