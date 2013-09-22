
Partial Class usr_Alta
    Inherits JJ.Intern.WebForm.PaginaIdiomaBase

    Dim Usuari As JJ.Usuaris.UsuariClass

    Protected Sub Paso1_Load(sender As Object, e As System.EventArgs) Handles Paso1.Load
        If My.Request.QueryString("k") <> "" Then
            Me.Mail.Text = My.Request.QueryString("m")
            Me.ClauActivacio.Text = My.Request.QueryString("k")
            Me.Button2_Click(sender, e)
        Else
            Me.Missatge.Visible = False
            If Me.Pais.Items.Count = 0 Then
                Dim Paissos As New JJ.Geografic.Paissos.LlistatPaissosClass()
                For i As Integer = 0 To Paissos.Count - 1
                    Me.Pais.Items.Add(New ListItem(Paissos(i).Nom, Paissos(i).Iso))
                Next
            End If
        End If
        'Permes?
        If Not JJ.Config.General.Usuaris.PermetreAltesUsuaris Then            
            Me.MostrarErrada(GetLocalResourceObject("ProhibitCrearUsuaris.Text").ToString())
            Exit Sub
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles BotoNext.Click
        'Permes?
        If Not JJ.Config.General.Usuaris.PermetreAltesUsuaris Then
            Me.MostrarErrada(GetLocalResourceObject("ProhibitCrearUsuaris.Text").ToString())
            Exit Sub
        End If
        'Mail unic
        If Not JJ.Usuaris.UsuariClass.MailUnic(Me.Mail.Text) Then
            Me.MostrarErrada(GetLocalResourceObject("ExistixMail.Text").ToString())
            Exit Sub
        End If
        'Alies unic
        If Not JJ.Usuaris.UsuariClass.AliesUnic(Me.Alies.Text) Then
            Me.MostrarErrada(GetLocalResourceObject("ExistixAlies.Text").ToString())
            Exit Sub
        End If
        'Contrasenya forta
        If Not JJ.Usuaris.UsuariClass.ContrasenyaForta(Me.Contrasenya.Text) Then
            Me.MostrarErrada(GetLocalResourceObject("ContrasenyaNoValida.Text").ToString())
            Exit Sub
        End If
        Dim Vector() As String = Split(Me.DataNaiximent.Text, "/")
        Try
            If Not IsNumeric(Vector(2)) OrElse CType(Vector(2), Integer) < 1900 OrElse CType(Vector(2), Integer) > 2100 Then
                Me.MostrarErrada(GetLocalResourceObject("DataNoCorrecta.Text").ToString())
                Exit Sub
            End If
        Catch ex As Exception
            Me.MostrarErrada(GetLocalResourceObject("DataNoCorrecta.Text").ToString())
            Exit Sub
        End Try
        'Següent pas
        Me.Usuari = New JJ.Usuaris.UsuariClass()
        Me.Usuari.Alies = Me.Alies.Text
        Me.Usuari.Mail = Me.Mail.Text
        Me.Usuari.Contrasenya = Me.Contrasenya.Text
        Me.Usuari.Tipus = JJ.Usuaris.TipusUsuari.Usuari
        Me.Usuari.Nom = Me.Nom.Text
        Me.Usuari.Cognoms = Me.Cognoms.Text
        Me.Usuari.DataNaixinent = JJ.Intern.Funcions.ValidacioDades.GetDate(Me.DataNaiximent.Text, "dd/mm/yyyy")
        Me.Usuari.IdPais = Me.Pais.SelectedItem.Value
        Me.Usuari.Regio = Me.Regio.Text
        Me.Usuari.Localitat = Me.Localitat.Text
        Me.Usuari.CP = Me.CP.Text
        Me.Usuari.Telefon = Me.Telefon.Text
        If Not Me.Usuari.Save() Then
            Me.MostrarErrada(GetLocalResourceObject("ErrorAlta.Text").ToString())
        Else
            If JJ.Config.General.Usuaris.ValidarMailAlAlta Then
                'Validar mail
                Me.Pasos.SetActiveView(Me.Paso2)
            Else
                'Fi
                Me.Pasos.SetActiveView(Me.Paso3)
            End If
        End If
    End Sub


    Private Sub MostrarErrada(ByVal Text As String)
        Me.Missatge.Text = Text
        Me.Missatge.Visible = True
    End Sub

    Protected Sub Button2_Click(sender As Object, e As System.EventArgs) Handles Button2.Click
        'Dim IdBBDD As String = JJ.Intern.Funcions.BBDD.GetField("id", "usuaris", "mail", Me.Mail.Text).ToString
        'IdBBDD = IdBBDD.Substring(0, 8)
        If Not JJ.Usuaris.UsuariClass.Validar(Me.Mail.Text, Me.ClauActivacio.Text) Then
            Me.ErrorClau.Visible = True
            Me.Pasos.SetActiveView(Me.Paso2)
        Else
            Me.Pasos.SetActiveView(Me.Paso3)
        End If
    End Sub
End Class
