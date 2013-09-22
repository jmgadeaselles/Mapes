
Partial Class admin_config_Default
    Inherits System.Web.UI.Page

    Protected Sub BotoGuardar_Click(sender As Object, e As System.EventArgs) Handles BotoGuardar.Click
        Me.Multivistes.SetActiveView(Me.VistaOK)
    End Sub

    Protected Sub VistaDades_Activate(sender As Object, e As System.EventArgs) Handles VistaDades.Activate
        Me.CheckCrear.Checked = JJ.Config.General.Seguretat.PermetreCrearLlocsNous
        Me.CheckEditar.Checked = JJ.Config.General.Seguretat.PermetreEdicioLlocs
        Me.CheckAltes.Checked = JJ.Config.General.Usuaris.PermetreAltesUsuaris
        Me.CheckAltaEmail.Checked = JJ.Config.General.Usuaris.ValidarMailAlAlta
        Me.RegistrarValidacionsCorrectes.Checked = JJ.Config.General.Sesio.RegistrarIniciSesioOK
        Me.RegistrarValidacioInvalides.Checked = JJ.Config.General.Sesio.RegistrarIniciSesioInvalit        
    End Sub


    Protected Sub VistaOK_Activate(sender As Object, e As System.EventArgs) Handles VistaOK.Activate
        JJ.Config.General.Seguretat.PermetreCrearLlocsNous = Me.CheckCrear.Checked
        JJ.Config.General.Seguretat.PermetreEdicioLlocs = Me.CheckEditar.Checked
        JJ.Config.General.Usuaris.PermetreAltesUsuaris = Me.CheckAltes.Checked
        JJ.Config.General.Usuaris.ValidarMailAlAlta = Me.CheckAltaEmail.Checked
        JJ.Config.General.Sesio.RegistrarIniciSesioOK = Me.RegistrarValidacionsCorrectes.Checked
        JJ.Config.General.Sesio.RegistrarIniciSesioInvalit = Me.RegistrarValidacioInvalides.Checked        
    End Sub


End Class
