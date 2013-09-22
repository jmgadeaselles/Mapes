
Partial Class usr_perfil_Default
    Inherits JJ.Intern.WebForm.PaginaIdiomaBase

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Me.Pais.Items.Count = 0 Then
            '*****************************
            '   CARGA
            '*****************************
            'Desplegable paises
            If Me.Pais.Items.Count = 0 Then
                Dim Paissos As New JJ.Geografic.Paissos.LlistatPaissosClass()
                For i As Integer = 0 To Paissos.Count - 1
                    Me.Pais.Items.Add(New ListItem(Paissos(i).Nom, Paissos(i).Iso))
                Next
            End If
            'Dades de perfil
            Dim Usuari As New JJ.Usuaris.UsuariClass()
            If Not Usuari.Load(JJ.Sesio.Usuari.Id) Then
                Me.Missatge.Text = GetLocalResourceObject("UsuariNoTrobat.Text").ToString()
                Me.Missatge.ForeColor = Drawing.Color.Red
                Me.Missatge.Visible = True
            Else
                Me.Foto.ImageUrl = JJ.Sesio.Usuari.DadesWeb.FotoGran
                With Usuari
                    Me.Alies.Text = .Alies
                    Me.Nom.Text = .Nom
                    Me.Cognoms.Text = .Cognoms
                    Me.Mail.Text = .Mail
                    Me.DataNaiximent.Text = .DataNaixinent.ToString("dd/mm/yyyy")
                    Me.Pais.SelectedValue = .IdPais.ToString
                    Me.Regio.Text = .Regio
                    Me.CP.Text = .CP
                    Me.Localitat.Text = .Localitat
                    Me.Telefon.Text = .Telefon
                    Me.LinkCamviarImatge.PostBackUrl = ""
                    Me.LinkCamviarImatge.OnClientClick = "MostrarCamviarImgPerfil(); return false;"
                End With
            End If
        Else
            '*************************************************************
            '   GUARDAR
            '*************************************************************
            If Not JJ.Usuaris.UsuariClass.SaveDadesPersonals(JJ.Sesio.Usuari.Id, Me.Nom.Text, Me.Cognoms.Text, JJ.Intern.Funcions.ValidacioDades.GetDate(Me.DataNaiximent.Text, "dd/mm/yyyy"), Me.Pais.SelectedValue, Me.Regio.Text, Me.CP.Text, Me.Localitat.Text, Me.Telefon.Text) Then
                Me.Missatge.Text = GetLocalResourceObject("NoGuardat.Text").ToString
                Me.Missatge.ForeColor = Drawing.Color.Red
                Me.Missatge.Visible = True
            Else
                Me.Missatge.Text = GetLocalResourceObject("Guardat.Text").ToString
                Me.Missatge.ForeColor = Drawing.Color.Green
                Me.Missatge.Visible = True
            End If
        End If


    End Sub
End Class
