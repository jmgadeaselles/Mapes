
Partial Class items_LlocEdt
    Inherits JJ.Intern.WebForm.PaginaIdiomaBase

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Validacio
        If Not JJ.Sesio.Usuari.Validat Then
            My.Response.Redirect(JJ.Config.General.Web.GetURL("/usr/Accedir.aspx?url=" + My.Request.Url.ToString))
        End If
        '
        Dim Link As HyperLink = Master.FindControl("LinkEditar")
        Link.Visible = False
        'Idiomes
        Dim Idiomes As New JJ.Idiomes.LlistatIdiomesDades()
        For i As Integer = 0 To Idiomes.Count - 1
            Dim IdiomaItem As JJ.Idiomes.IdiomaClass = Idiomes(i)
            Me.IdiomaLlocVore.Items.Add(New ListItem(IdiomaItem.Nom, IdiomaItem.Codi))
        Next
        If My.Request.QueryString("i") <> "" Then
            Me.IdiomaLlocVore.SelectedValue = My.Request.QueryString("i")
        Else
            Me.IdiomaLlocVore.SelectedValue = JJ.Sesio.Idioma.Valor
        End If
        Me.IdiomaLloc.Value = Me.IdiomaLlocVore.SelectedValue
        'Categories        
        Dim Categories As New JJ.Categories.Llocs.LlistatCategoriesClass(JJ.Sesio.Idioma.Valor)
        For i As Integer = 0 To Categories.Count - 1
            Me.Categoria.Items.Add(New ListItem(Categories(i).Nom, Categories(i).Id.ToString))
        Next
        'Coordenades
        'If (Me.IDLloc.Value = "") Then
        '    If My.Request.QueryString("lat") <> "" Then
        '        Me.Lat.Value = My.Request.QueryString("lat").Replace(",", ".")
        '        Me.LatVore.Text = Me.Lat.Value
        '    End If
        '    If My.Request.QueryString("lng") <> "" Then
        '        Me.Lng.Value = My.Request.QueryString("lng").Replace(",", ".")
        '        Me.LngVore.Text = Me.Lng.Value
        '    End If
        'End If        

        'If (Me.IDLloc.Value = "") Or (Me.IDLloc.Value = Guid.Empty.ToString) Then
        If (Me.IDLloc.Value = "") Or (My.Request("v") = "1") Then
            If My.Request.QueryString("lat") <> "" Then
                Me.Lat.Value = My.Request.QueryString("lat").Replace(",", ".")
                Me.LatVore.Text = Me.Lat.Value
            End If
            If My.Request.QueryString("lng") <> "" Then
                Me.Lng.Value = My.Request.QueryString("lng").Replace(",", ".")
                Me.LngVore.Text = Me.Lng.Value
            End If
            If My.Request.QueryString("zoom") <> "" Then
                Me.Zoom.Value = My.Request.QueryString("zoom")
            End If
            'ElseIf (Me.IDLloc.Value = Guid.Empty.ToString) Then
            '    'If My.Request.QueryString("lat") <> "" Then
            '    'Me.Lat.Value = My.Request.QueryString("lat").Replace(",", ".")
            '    Me.Lat.Value = Me.LatVore.Text
            '    'End If
            '    'If My.Request.QueryString("lng") <> "" Then
            '    'Me.Lng.Value = My.Request.QueryString("lng").Replace(",", ".")
            '    Me.Lng.Value = Me.LngVore.Text
            '    'End If
        End If



        '
        'Recopilem dades   
        If (Me.IDLloc.Value <> "") Then
            Me.Multivistes.SetActiveView(Me.VistaResultat)
            Me.GuardarDades()
        Else
            Me.Multivistes.SetActiveView(Me.VistaEditar)
            Me.CarregarDades()
        End If
    End Sub


    Private Sub CarregarDades()
        'Vista editar
        Dim Id As Guid
        If Trim(My.Request.QueryString("id")) = "" Then
            Id = Guid.Empty
        Else
            Id = New Guid(My.Request.QueryString("id"))
        End If
        Me.IDLloc.Value = Id.ToString
        'Lloc
        Dim Lloc As New JJ.Geo.Llocs.LlocClass(Me.IdiomaLloc.Value, False)
        If Id = Guid.Empty Then
            Lloc.Nou(Me.IdiomaLloc.Value)
            Me.Cancelar.NavigateUrl = "javascript:tancar_detall();"
        Else
            If Lloc.Obrir(Id, Me.IdiomaLloc.Value) Then
                Dim Titol As Label = Master.FindControl("Titol")
                Titol.Text = Lloc.Nom
                Me.Nom.Text = Lloc.Nom
                Me.BreuDescripcio.Text = Lloc.DescripcioBreu
                Me.Descripcio.Text = Lloc.Descripcio
                Me.Acces.Text = Lloc.Acces
                Me.Lat.Value = Lloc.Posicio.Lat.ToString.Replace(",", ".")
                Me.Lng.Value = Lloc.Posicio.Lng.ToString.Replace(",", ".")
                'Me.Lat.Value = Lloc.Posicio.Lat.ToString
                'Me.Lng.Value = Lloc.Posicio.Lng.ToString
                Me.LatVore.Text = Me.Lat.Value
                Me.LngVore.Text = Me.Lng.Value
                Me.Categoria.SelectedValue = Lloc.IdCategoria.ToString
                Me.IdiomaLlocVore.SelectedValue = Lloc.Idioma
                Me.IdiomaLloc.Value = Lloc.Idioma
                Me.ParaulesClau.Text = Lloc.ParaulesClau
                Me.Cancelar.NavigateUrl += "?id=" + Lloc.Id.ToString + "&i=" + Lloc.Idioma
            End If
            Me.IdiomaLlocVore.Enabled = False
        End If
    End Sub

    Private Sub GuardarDades()
        'Vista guardar
        Dim Lloc As New JJ.Geo.Llocs.LlocClass(Me.IdiomaLloc.Value, False)
        If Me.IDLloc.Value = Guid.Empty.ToString Then
            Lloc.Nou(Me.IdiomaLloc.Value)
        Else
            Lloc.SetId(New Guid(Me.IDLloc.Value))
        End If
        Lloc.Nom = Me.Nom.Text
        Lloc.DescripcioBreu = Me.BreuDescripcio.Text
        Lloc.Descripcio = Me.Descripcio.Text
        Lloc.Acces = Me.Acces.Text
        'If JJ.Config.General.Web.GetWebLocal() Then
        Lloc.Posicio.Lat = Double.Parse(Me.Lat.Value.Replace(".", ","))
        Lloc.Posicio.Lng = Double.Parse(Me.Lng.Value.Replace(".", ","))
        'Else
        'Lloc.Posicio.Lat = Double.Parse(Me.Lat.Value)
        'Lloc.Posicio.Lng = Double.Parse(Me.Lng.Value)
        'End If
        Lloc.IdCategoria = New Guid(Me.Categoria.SelectedValue)
        Lloc.ParaulesClau = Me.ParaulesClau.Text.Trim + "," + Me.Nom.Text
        If Lloc.Guardar() Then
            My.Response.Redirect(JJ.Config.General.Web.GetURL("/items/Lloc.aspx?id=" + Lloc.Id.ToString + "&i=" + Me.IdiomaLloc.Value + "&v=1"))
        End If
    End Sub
End Class
