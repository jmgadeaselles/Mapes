Imports Microsoft.VisualBasic


Namespace JJ.Emailing





    Public Class SendEmailClass

        Private Shared ReadOnly Property Host() As String
            Get
                Return ConfigurationManager.AppSettings.Get("smtp_host")
            End Get
        End Property
        Private Shared ReadOnly Property Usuario() As String
            Get
                Return ConfigurationManager.AppSettings.Get("smtp_usr")
            End Get
        End Property
        Private Shared ReadOnly Property Contraseña() As String
            Get
                Return ConfigurationManager.AppSettings.Get("smtp_password")
            End Get
        End Property
        Private Shared ReadOnly Property Remitente() As System.Net.Mail.MailAddress
            Get
                Return New System.Net.Mail.MailAddress(ConfigurationManager.AppSettings.Get("smtp_from"), ConfigurationManager.AppSettings.Get("smtp_from_name"))
            End Get
        End Property


        Public Shared Function EnviarEmail(ByVal Destinatario As String, ByVal Asunto As String, ByVal Cuerpo As String) As Boolean
            Try
                Dim Mail As New System.Net.Mail.MailMessage()
                Mail.IsBodyHtml = True
                Mail.From = Remitente
                Mail.To.Add(Destinatario)
                Mail.Subject = Asunto
                Mail.Body = Cuerpo
                Dim SMTP As New System.Net.Mail.SmtpClient()
                SMTP.Host = Host
                SMTP.Credentials = New System.Net.NetworkCredential(Usuario, Contraseña)
                SMTP.Send(Mail)               
                Return True
            Catch ex As Exception
                JJ.Registre.RegistrarErrada(ex)
                Return False
            End Try
        End Function



    End Class




End Namespace

