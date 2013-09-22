
Partial Class items_Historial
    Inherits JJ.Intern.WebForm.PaginaIdiomaBase

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim Id As Guid
        If Trim(My.Request.QueryString("id")) = "" Then
            Id = Guid.Empty
        Else
            Try
                Id = New Guid(My.Request.QueryString("id"))
            Catch ex As Exception
                Id = Guid.Empty
            End Try            
        End If
        If Id <> Guid.Empty Then
            Dim Historic As List(Of JJ.DadesBase.Historic.HistoricClass)
            Select Case My.Request.QueryString("tipo").ToUpper
                Case "LLOC"
                    Historic = JJ.Geo.Llocs.LlocClass.GetHistoric(Id)
                Case "FOTO"
                    Historic = JJ.Geo.Llocs.Fotos.FotoClass.GetHistoric(Id)
                Case Else
                    Historic = JJ.Geo.Llocs.LlocClass.GetHistoric(Id)
            End Select
            For Each Registre As JJ.DadesBase.Historic.HistoricClass In Historic

                Dim Fila As New TableRow()
                Dim Celda1 As New TableCell()
                Celda1.Text = Registre.Data.ToUniversalTime.ToString
                Fila.Cells.Add(Celda1)
                Dim Celda2 As New TableCell()
                Celda2.Text = Registre.Usuari.Text
                Fila.Cells.Add(Celda2)

                Fila.CssClass = "fons_item"

                Me.Historial.Rows.Add(Fila)
            Next
        End If
    End Sub
End Class
