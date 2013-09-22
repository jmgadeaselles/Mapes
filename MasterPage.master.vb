Imports System.Globalization

Partial Class MasterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Me.DadesHead.Text = "" Then
                Dim Descripcio As String = CType(GetLocalResourceObject("MetaDescripcio.Text"), String)
                Dim Claus As String = CType(GetLocalResourceObject("MetaClaus.Text"), String)
                Me.DadesHead.Text = JJ.Intern.WebForm.HeadWebForm.Generar("Ocimap.com", Claus, Descripcio)
            End If
            'Idiomes
            If Me.DesplegableIdioma.Items.Count = 0 Then
                Dim Idiomes As New JJ.Idiomes.LlistatIdiomesWeb()
                For i As Integer = 0 To Idiomes.Count - 1
                    Dim Idioma As JJ.Idiomes.IdiomaClass = Idiomes(i)
                    Me.DesplegableIdioma.Items.Add(New ListItem(Idioma.Nom, Idioma.Codi))
                Next
                Me.DesplegableIdioma.SelectedValue = JJ.Sesio.Idioma.Valor
                'JJ.Sesio.Idioma.Valor = Me.DesplegableIdioma.SelectedValue
            End If
            'SI ES UNA ARANYA..... SOLUCIO PER LO DEL IFRAME. Si es un buscador directament li torne el lloc amb items/lloc.aspx
            If My.Request.Browser.Crawler And (My.Request.QueryString("id") <> "") Then
                Dim Id As Guid = New Guid(My.Request.QueryString("id"))
                Dim Idioma2 As String = My.Request.QueryString("i")
                If Idioma2 = "" Then
                    Idioma2 = JJ.Sesio.Idioma.Valor
                End If
                My.Response.Write(JJ.Intern.ClientWebClass.Navegar(JJ.Config.General.Web.GetURL("/items/lloc.aspx?id=" + Id.ToString + "&i=" + Idioma2)))
                My.Response.Flush()
            End If
        Catch ex As Exception
            JJ.Registre.RegistrarErrada(ex.ToString)
        End Try
    End Sub

    'Protected Sub DesplegableIdioma_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DesplegableIdioma.SelectedIndexChanged
    '    JJ.Sesio.Idioma.Valor = Me.DesplegableIdioma.SelectedItem.Value
    '    Page.UICulture = JJ.Sesio.Idioma.Valor
    '    Page.Response.Redirect(My.Request.RawUrl)
    'End Sub

End Class

