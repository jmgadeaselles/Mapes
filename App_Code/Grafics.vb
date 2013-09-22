Imports Microsoft.VisualBasic
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing.Text
Imports System.Drawing.Rectangle
Imports System.Drawing


Namespace JJ.Grafics

    Public Class Size
        Dim _Width, _Height As Integer

        Public Sub New(ByVal Width As Integer, ByVal Height As Integer)
            Me._Width = Width
            Me._Height = Height
        End Sub

        Public ReadOnly Property Width() As Integer
            Get
                Return Me._Width
            End Get
        End Property

        Public ReadOnly Property Height() As Integer
            Get
                Return Me._Height
            End Get
        End Property

    End Class


    Public Class CapchaClass
        Dim _Width, _Height As Integer
        Dim _Length As Integer
        Dim _TexturaFondo As HatchStyle


        Public Sub New()
            Me._Height = 100
            Me._Width = 200
            Me._Length = 6
            Me._TexturaFondo = HatchStyle.Min
        End Sub


        Public Property Width() As Integer
            Get
                Return Me._Width
            End Get
            Set(ByVal value As Integer)
                Me._Width = value
            End Set
        End Property
        Public Property Height() As Integer
            Get
                Return Me._Height
            End Get
            Set(ByVal value As Integer)
                Me._Height = value
            End Set
        End Property
        Public Property Length() As Integer
            Get
                Return Me._Length
            End Get
            Set(ByVal value As Integer)
                Me._Length = value
            End Set
        End Property
        Public Property TexturaFondo() As HatchStyle
            Get
                Return Me._TexturaFondo
            End Get
            Set(ByVal value As HatchStyle)
                Me._TexturaFondo = value
            End Set
        End Property


        Public Function Generar(ByVal path As String) As String

            '*** Editable Values
            Dim BackgroundColor As [String]() = New [String]() {255, 255, 255} ' The 3 numbers represent in order RED, GREEN, BLUE for the captcha's background color
            Dim RandomBackgroundNoiseColor As Boolean = True ' True / False. If you choose True, BackgroundNoiseColor will not apply 
            Dim RandomTextColor As Boolean = True ' True / False. If you choose True, TextColor will not apply 
            Dim BackgroundNoiseColor As [String]() = New [String]() {150, 150, 150} ' The 3 numbers represent in order RED, GREEN, BLUE
            Dim TextColor As [String]() = New [String]() {200, 200, 200} ' The 3 numbers represent in order RED, GREEN, BLUE
            'Dim BackgroundNoiseTexture As HatchStyle = HatchStyle.Min ' replace ".Min" with any of the following: Horizontal, Vertical, ForwardDiagonal, BackwardDiagonal, Cross, DiagonalCross, Percent05, Percent10, Percent20, Percent25, Percent30, Percent40, Percent50, Percent60, Percent70, Percent75, Percent80, Percent90, LightDownwardDiagonal, LightUpwardDiagonal, DarkDownwardDiagonal, DarkUpwardDiagonal, WideDownwardDiagonal, WideUpwardDiagonal, LightVertical, LightHorizontal, NarrowVertical, NarrowHorizontal, DarkVertical, DarkHorizontal, DashedDownwardDiagonal, DashedUpwardDiagonal, DashedHorizontal, DashedVertical, SmallConfetti, LargeConfetti, ZigZag, Wave, DiagonalBrick, HorizontalBrick, Weave, Plaid, Divot, DottedGrid, DottedDiamond, Shingle, Trellis, Sphere, SmallGrid, SmallCheckerBoard, LargeCheckerBoard, OutlinedDiamond, SolidDiamond, LargeGrid, Min, Max

            'Dim length As Integer = 6 ' Number of characters to generate
            '*** END Editable Values


            'Dim height As Integer = 100
            'Dim width As Integer = 200
            Me._Width = Me._Width + ((Me._Length - 6) * 30)
            Dim ranRotate As New Random
            'Dim strText As String = Left(Replace(System.Guid.NewGuid().ToString(), "-", ""), length)
            Dim strText As String = Replace(System.Guid.NewGuid().ToString(), "-", "").Substring(0, Me._Length)
            Dim bmpCanvas As New Bitmap(Me._Width, Me._Height, PixelFormat.Format24bppRgb)
            Dim graCanvas As Graphics = Graphics.FromImage(bmpCanvas)
            Dim recF As New RectangleF(0, 0, Me._Width, Me._Height)
            Dim bruBackground As Brush
            Dim letterBrush As SolidBrush

            graCanvas.TextRenderingHint = TextRenderingHint.AntiAlias

            If RandomBackgroundNoiseColor = True Then
                bruBackground = New HatchBrush(Me._TexturaFondo, Color.FromArgb((ranRotate.Next(0, 255)), (ranRotate.Next(0, 255)), (ranRotate.Next(0, 255))), Color.FromArgb(BackgroundColor(0), BackgroundColor(1), BackgroundColor(2)))
            Else
                bruBackground = New HatchBrush(Me._TexturaFondo, Color.FromArgb(BackgroundNoiseColor(0), BackgroundNoiseColor(1), BackgroundNoiseColor(2)), Color.FromArgb(BackgroundColor(0), BackgroundColor(1), BackgroundColor(2)))
            End If

            graCanvas.FillRectangle(bruBackground, recF)

            If RandomTextColor = True Then
                letterBrush = New SolidBrush(Color.FromArgb((ranRotate.Next(0, 255)), (ranRotate.Next(0, 255)), (ranRotate.Next(0, 255))))
            Else
                letterBrush = New SolidBrush(Color.FromArgb(TextColor(0), TextColor(1), TextColor(2)))
            End If

            Dim matRotate As New System.Drawing.Drawing2D.Matrix
            Dim i As Integer
            For i = 0 To Len(strText) - 1
                matRotate.Reset()
                matRotate.RotateAt(ranRotate.Next(-30, 30), New PointF(Me._Width / (Len(strText) + 1) * i, Me._Height * 0.5))
                graCanvas.Transform = matRotate
                If i = 0 Then
                    graCanvas.DrawString(strText.Chars(i), New Font("Comic Sans MS", 25, FontStyle.Italic), letterBrush, Me._Width / (Len(strText) + 1) * i, Me._Height * 0.4) 'draw ‘the text on our image
                ElseIf i = 1 Then
                    graCanvas.DrawString(strText.Chars(i), New Font("Arial", 30, FontStyle.Bold), letterBrush, Me._Width / (Len(strText) + 1) * i, Me._Height * 0.1) 'draw ‘the text on our image
                ElseIf i = 2 Then
                    graCanvas.DrawString(strText.Chars(i), New Font("Times New Roman", 25, FontStyle.Italic), letterBrush, Me._Width / (Len(strText) + 1) * i, Me._Height * 0.5) 'draw ‘the text on our image
                ElseIf i = 3 Then
                    graCanvas.DrawString(strText.Chars(i), New Font("Georgia", 35, FontStyle.Bold), letterBrush, Me._Width / (Len(strText) + 1) * i, Me._Height * 0.1) 'draw ‘the text on our image
                ElseIf i = 4 Then
                    graCanvas.DrawString(strText.Chars(i), New Font("Verdana", 25, FontStyle.Italic), letterBrush, Me._Width / (Len(strText) + 1) * i, Me._Height * 0.5) 'draw ‘the text on our image
                ElseIf i = 5 Then
                    graCanvas.DrawString(strText.Chars(i), New Font("Geneva", 30, FontStyle.Bold), letterBrush, Me._Width / (Len(strText) + 1) * i, Me._Height * 0.1) 'draw ‘the text on our image
                Else
                    graCanvas.DrawString(strText.Chars(i), New Font("Arial", 30, FontStyle.Italic), letterBrush, Me._Width / Len(strText) * i, Me._Height * 0.5) 'draw ‘the text on our image
                End If
                graCanvas.ResetTransform()
            Next

            bmpCanvas.Save(path, ImageFormat.Gif)
            graCanvas.Dispose()
            bmpCanvas.Dispose()

            Return strText

        End Function

    End Class


    Public NotInheritable Class Imatges


        Public Shared Function RedimensionarImatge(ByVal StreamImatge As System.IO.Stream, ByVal NouAmple As Single, ByVal NouAlt As Single) As System.Drawing.Bitmap
            Try
                Dim Original As New System.Drawing.Bitmap(StreamImatge)
                Dim Ample As Single = Original.Width
                Dim Alt As Single = Original.Height

                Dim FactorX As Single = Ample / NouAmple
                Dim FactorY As Single = Alt / NouAlt
                Dim Factor As Single = IIf(FactorX > FactorY, FactorX, FactorY)

                Dim Desti As New System.Drawing.Bitmap(Ample / Factor, Alt / Factor, Drawing.Imaging.PixelFormat.Format24bppRgb)
                Dim Grafic As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(Desti)
                Grafic.DrawImage(Original, 0, 0, Ample / Factor, Alt / Factor)
                Return Desti                
            Catch ex As Exception
                JJ.Registre.RegistrarErrada(ex)
                Throw ex
            End Try
        End Function



        Public Shared Function RedimensionarImatge(ByVal RutaFitxer As String, ByVal NouAmple As Single, ByVal NouAlt As Single) As System.Drawing.Bitmap
            Dim Fitxer As System.IO.StreamReader
            Try
                Fitxer = New System.IO.StreamReader(My.Request.MapPath(RutaFitxer))
                Return RedimensionarImatge(Fitxer.BaseStream, NouAmple, NouAlt)
            Catch ex As Exception
                JJ.Registre.RegistrarErrada(ex)
                Throw ex
            Finally
                Fitxer.Close()
            End Try
        End Function



    End Class


End Namespace
