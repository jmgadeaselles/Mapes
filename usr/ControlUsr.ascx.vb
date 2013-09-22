
Partial Class usr_ControlUsr
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not JJ.Sesio.Usuari.Validat Then
            Me.Multivistes.SetActiveView(Me.VistaAnonim)
        Else
            Me.Nom.Text = JJ.Sesio.Usuari.Nom
            Me.Multivistes.SetActiveView(Me.VistaUsr)
        End If
    End Sub
End Class
