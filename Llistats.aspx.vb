
Partial Class Llistats
    Inherits JJ.Intern.WebForm.PaginaIdiomaBase

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.LlistatTop.Show(Llistat.TiposTop.Visites, GetLocalResourceObject("TitolTop.Text").ToString())
        Me.LlistatNous.Show(Llistat.TiposTop.Nous, GetLocalResourceObject("TitolNous.Text").ToString())
        Me.LlistatModificats.Show(Llistat.TiposTop.Modificats, GetLocalResourceObject("TitolModificats.Text").ToString())
    End Sub
End Class
