Imports Microsoft.VisualBasic


Namespace JJ.Seguretat


    Public Class ControlHistoric


        Public Shared Function AlertaString(ByVal Nou As String, ByVal Vell As String) As Boolean
            Dim Diferencia As Integer = Vell.Length - Nou.Length
            If Diferencia <= 0 Then
                Return False
            Else
                Dim Percentatje As Double = 100 * Diferencia / Vell.Length
                Return (Percentatje > 15)
            End If
        End Function


        Public Shared Function AlertaDouble(ByVal Nou As Double, ByVal Vell As Double) As Boolean
            Dim Diferencia As Double = Vell - Nou
            If Diferencia <= 0 Then
                Return False
            Else
                Dim Percentatje As Double = 100 * Diferencia / Vell
                Return (Percentatje > 0.10000000000000001)
            End If
        End Function


    End Class


End Namespace