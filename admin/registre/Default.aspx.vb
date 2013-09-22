Imports System.Data.SqlClient

Partial Class admin_registre_Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Me.Usuari.Items.Count = 0 Then
            Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
            Dim Comand As New SqlCommand
            Try
                Conexio.Open()
                Comand.Connection = Conexio
                Comand.CommandText = "SELECT DISTINCT IP FROM REGISTRE ORDER BY IP"
                Dim Temp As SqlDataReader = Comand.ExecuteReader()
                Me.IP.Items.Add("")
                While Temp.Read
                    Me.IP.Items.Add(Temp("IP"))
                End While
                Temp.Close()
                Comand.CommandText = "SELECT DISTINCT USUARI FROM REGISTRE ORDER BY USUARI"
                Temp = Comand.ExecuteReader()
                Me.Usuari.Items.Add("")
                While Temp.Read
                    Me.Usuari.Items.Add(Temp("USUARI"))
                End While
                Temp.Close()
                Comand.CommandText = "SELECT DISTINCT USER_AGENT FROM REGISTRE WHERE USER_AGENT IS NOT NULL ORDER BY USER_AGENT"
                Temp = Comand.ExecuteReader()
                Me.UserAgent.Items.Add("")
                While Temp.Read
                    Me.UserAgent.Items.Add(Temp("USER_AGENT"))
                End While
                Temp.Close()
            Catch ex As Exception
                JJ.Registre.RegistrarErrada(ex.ToString)
            Finally
                Conexio.Close()
            End Try
        End If
    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        If Me.VoreErradades.Checked Or Me.VoreAdvertencies.Checked Or Me.VoreEvents.Checked Then
            Me.Dades.ConnectionString = JJ.Config.BBDD.CadenaConexio
            Me.Taula.DataSourceID = "Dades"
            Me.Dades.SelectCommand = "SELECT DATA, IP, USUARI, TIPUS, CAST(USER_AGENT AS NVARCHAR(30)) AS USER_AGENT, TEXT FROM REGISTRE"
            Dim Where As Boolean = False
            If Me.VoreErradades.Checked Then
                Me.Dades.SelectCommand += " WHERE (TIPUS='2'"
                Where = True
            End If
            If Me.VoreAdvertencies.Checked Then
                If Where Then
                    Me.Dades.SelectCommand += " OR TIPUS='1'"
                Else
                    Me.Dades.SelectCommand += " WHERE (TIPUS='1'"
                    Where = True
                End If
            End If
            If Me.VoreEvents.Checked Then
                If Where Then
                    Me.Dades.SelectCommand += " OR TIPUS='0'"
                Else
                    Me.Dades.SelectCommand += " WHERE (TIPUS='0'"
                    Where = True
                End If
            End If
            Me.Dades.SelectCommand += ")"
            If Me.Usuari.Text <> "" Then
                If Where Then
                    Me.Dades.SelectCommand += " AND USUARI='" + Me.Usuari.Text.Replace("'", "''") + "'"
                Else
                    Me.Dades.SelectCommand += " WHERE USUARI='" + Me.Usuari.Text.Replace("'", "''") + "'"
                    Where = True
                End If                
            End If
            If Me.IP.Text <> "" Then
                If Where Then
                    Me.Dades.SelectCommand += " AND IP='" + Me.IP.Text.Replace("'", "''") + "'"
                Else
                    Me.Dades.SelectCommand += " WHERE IP='" + Me.IP.Text.Replace("'", "''") + "'"
                    Where = True
                End If
            End If
            Me.Dades.SelectCommand += " ORDER BY DATA DESC"
        Else
            Me.Dades.SelectCommand = ""
            Me.Taula.DataSourceID = ""
        End If
    End Sub
End Class
