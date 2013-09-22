Imports System.Data.SqlClient
Imports System.IO


Partial Class admin_categories_DetallCat
    Inherits System.Web.UI.Page

    Dim Nou As Boolean
    Dim Arxiu1, Arxiu2 As String

    Private Sub CarregarDades(ByVal IdCat As Guid)
        Dim Categories As Hashtable = Me.GetCategories(New Guid(IdCate.Value))
        'Fila imatge 1
        Dim FilaImg As New TableRow()
        Dim CeldaImg1 As New TableCell()
        CeldaImg1.Style.Add("text-align", "right")
        If Not Nou Then
            Dim Img1 As New Image()
            Img1.ImageUrl = "~/img/cat/" + IdCate.Value + ".png"
            CeldaImg1.Controls.Add(Img1)
            'Dim Espai As New Label()
            'Espai.Text = " "
            'CeldaImg1.Controls.Add(Espai)
            'Dim Img2 As New Image()
            'Img2.ImageUrl = "~/img/cat/" + IdCate.Value + "(x).png"
            'CeldaImg1.Controls.Add(Img2)
        End If
        Dim CeldaImg2 As New TableCell()
        Dim TextCamviar As New Label()
        TextCamviar.Text = "Camviar imatge gran:"
        CeldaImg2.Controls.Add(TextCamviar)
        Dim UP As New FileUpload()
        UP.ID = "UP"
        Me.Arxiu1 = UP.UniqueID
        CeldaImg2.Controls.Add(UP)
        FilaImg.Cells.Add(CeldaImg1)
        FilaImg.Cells.Add(CeldaImg2)
        Me.Taula.Rows.Add(FilaImg)
        'Fila imatge 2
        Dim FilaImgB2 As New TableRow()
        Dim CeldaImgB1 As New TableCell()
        CeldaImgB1.Style.Add("text-align", "right")
        If Not Nou Then
            'Dim Img1 As New Image()
            'Img1.ImageUrl = "~/img/cat/" + IdCate.Value + ".png"
            'CeldaImgB1.Controls.Add(Img1)
            'Dim Espai As New Label()
            'Espai.Text = " "
            'CeldaImgB1.Controls.Add(Espai)
            Dim Img2 As New Image()
            Img2.ImageUrl = "~/img/cat/" + IdCate.Value + "(x).png"
            CeldaImgB1.Controls.Add(Img2)
        End If
        Dim CeldaImgB2 As New TableCell()
        Dim TextCamviarB As New Label()
        TextCamviarB.Text = "Camviar imatge xicoteta:"
        CeldaImgB2.Controls.Add(TextCamviarB)
        Dim UP2 As New FileUpload()
        UP2.ID = "UP2"
        Me.Arxiu2 = UP2.UniqueID
        CeldaImgB2.Controls.Add(UP2)
        FilaImgB2.Cells.Add(CeldaImgB1)
        FilaImgB2.Cells.Add(CeldaImgB2)
        Me.Taula.Rows.Add(FilaImgB2)
        'Files textos
        Dim Idiomes As New JJ.Idiomes.LlistatIdiomesWeb()
        For i As Integer = 0 To Idiomes.Count - 1
            Dim Fila As New TableRow()
            Dim Celda1 As New TableCell()
            Celda1.Style.Add("text-align", "right")
            Dim LeyendaIdioma As New Label()
            LeyendaIdioma.Text = Idiomes(i).Nom + ": "
            Celda1.Controls.Add(LeyendaIdioma)
            Dim Celda2 As New TableCell()
            Dim Text As New TextBox()
            If Not Nou Then
                Text.Text = Categories(Idiomes(i).Codi)
            End If
            Text.ID = "Cat" + Idiomes(i).Codi
            Celda2.Controls.Add(Text)
            Fila.Cells.Add(Celda1)
            Fila.Cells.Add(Celda2)
            Me.Taula.Rows.Add(Fila)
        Next
        'Fila habilitat
        Dim FilaHabilitat As New TableRow()
        Dim CeldaBuit As New TableCell()
        Dim CeldaCheck As New TableCell()
        Dim Check As New CheckBox()
        Check.ID = "CheckHabilitat"
        Check.Text = "Habilitat"
        If Nou Then
            Check.Checked = True
        ElseIf Not Page.IsPostBack Then
            Check.Checked = JJ.Intern.Funcions.BBDD.GetField("habilitat", "Categories", "id", New Guid(IdCate.Value))
        End If
        CeldaCheck.Controls.Add(Check)
        FilaHabilitat.Cells.Add(CeldaBuit)
        FilaHabilitat.Cells.Add(CeldaCheck)
        Me.Taula.Rows.Add(FilaHabilitat)
        'Fila botons
        Dim FilaBaix As New TableRow()
        Dim CeldaOK As New TableCell()
        CeldaOK.Style.Add("text-align", "right")
        Dim BotoOK As New Button()
        BotoOK.Text = "Guardar"
        'BotoOK.PostBackUrl = "~/admin/categories/DetallCat.aspx"
        AddHandler BotoOK.Click, AddressOf Me.GuardarClic
        CeldaOK.Controls.Add(BotoOK)
        Dim CeldaCancel As New TableCell()
        Dim BotoCancel As New Button()
        BotoCancel.Text = "Cancel·lar"
        BotoCancel.PostBackUrl = "#"
        BotoCancel.OnClientClick = "AmagaDetall();return false;"
        CeldaCancel.Controls.Add(BotoCancel)
        FilaBaix.Cells.Add(CeldaOK)
        FilaBaix.Cells.Add(CeldaCancel)
        Me.Taula.Rows.Add(FilaBaix)
    End Sub


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If My.Request.QueryString("id") <> "" Then
            Me.IdCate.Value = My.Request.QueryString("id")
            Me.Nou = False
        Else
            Me.IdCate.Value = Guid.Empty.ToString
            Me.Nou = True
        End If
        Me.CarregarDades(New Guid(Me.IdCate.Value))
        Me.MultiVistes.SetActiveView(Me.VistaEdicio)
        'If My.Request.Files.Count > 0 Then
        '    Me.Label1.Text = "(" + My.Request.Files.Count.ToString + ")" + My.Request.Files.Keys(0) + " | " + My.Request.Files.Keys(1)
        'End If        
    End Sub



    Protected Sub GuardarClic(sender As Object, e As System.EventArgs)
        Me.MultiVistes.SetActiveView(Me.VistaOK)
    End Sub

    Protected Sub VistaOK_Activate(sender As Object, e As System.EventArgs) Handles VistaOK.Activate
        If Me.GuardarCategoria() Then
            Me.Resultat.Text = "OK"
        Else
            Me.Resultat.Text = "ERROR"
        End If
    End Sub






    Private Function GuardarCategoria() As Boolean
        Dim MiId As Guid
        If Me.IdCate.Value = Guid.Empty.ToString Then
            Me.Nou = True
            MiId = Guid.NewGuid()
            Me.IdCate.Value = MiId.ToString
        Else
            Me.Nou = False
            MiId = New Guid(Me.IdCate.Value)
        End If
        'Me.Label1.Text = "Nou: " + Nou.ToString
        Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
        Dim Comand As New SqlCommand
        Try
            Conexio.Open()
            Comand.Connection = Conexio

            If Nou Then
                'categoria
                Comand.CommandText = "INSERT INTO Categories (id,habilitat) VALUES (@id,@habilitat)"
                Comand.Parameters.AddWithValue("@id", MiId)
                Comand.Parameters.AddWithValue("@habilitat", CType(Me.Taula.FindControl("CheckHabilitat"), CheckBox).Checked)
                Comand.ExecuteNonQuery()
                'textos
                For i As Integer = 2 To Me.Taula.Rows.Count - 3
                    Dim Fila As TableRow = Me.Taula.Rows(i)
                    Comand.Parameters.Clear()
                    Comand.CommandText = "INSERT INTO CategoriesIdioma (ca_categoria,ca_idioma,text) VALUES (@ca_categoria,@ca_idioma,@text)"
                    Comand.Parameters.AddWithValue("@ca_categoria", MiId)
                    Dim obj As Control = Fila.Cells(1).Controls(0)
                    If obj.ID.Length = 5 Then
                        Dim Idioma As String = obj.ID.Substring(3, 2)
                        Dim Valor As String = My.Request.Form(obj.UniqueID)
                        Comand.Parameters.AddWithValue("@ca_idioma", Idioma)
                        Comand.Parameters.AddWithValue("@text", Valor)
                        Comand.ExecuteNonQuery()
                    Else
                        JJ.Registre.RegistrarEvent("-> " + obj.ID)
                    End If
                Next
            Else
                'categoria
                Comand.CommandText = "UPDATE Categories SET habilitat=@habilitat WHERE id=@id"
                Comand.Parameters.AddWithValue("@id", MiId)
                Comand.Parameters.AddWithValue("@habilitat", CType(Me.Taula.FindControl("CheckHabilitat"), CheckBox).Checked)
                Comand.ExecuteNonQuery()
                'textos
                For i As Integer = 2 To Me.Taula.Rows.Count - 3
                    Dim Fila As TableRow = Me.Taula.Rows(i)
                    Comand.Parameters.Clear()
                    Comand.CommandText = "UPDATE CategoriesIdioma SET text=@text WHERE ca_categoria=@ca_categoria AND ca_idioma=@ca_idioma"
                    'Comand.CommandText = "INSERT INTO CategoriesIdioma (ca_categoria,ca_idioma,text) VALUES (@ca_categoria,@ca_idioma,@text)"
                    Comand.Parameters.AddWithValue("@ca_categoria", MiId)
                    Dim obj As Control = Fila.Cells(1).Controls(0)
                    If obj.ID.Length = 5 Then
                        Dim Idioma As String = obj.ID.Substring(3, 2)
                        Dim Valor As String = My.Request.Form(obj.UniqueID)
                        'Me.Label1.Text += "<br />" + obj.ID + ";'" + Valor + "';"
                        Comand.Parameters.AddWithValue("@ca_idioma", Idioma)
                        Comand.Parameters.AddWithValue("@text", Valor)
                        If Comand.ExecuteNonQuery() = 0 Then
                            ''
                            'Comand.Parameters.Clear()
                            Comand.CommandText = "INSERT INTO CategoriesIdioma (ca_categoria,ca_idioma,text) VALUES (@ca_categoria,@ca_idioma,@text)"
                            'Comand.Parameters.AddWithValue("@ca_categoria", MiId)
                            'Dim obj As Control = Fila.Cells(1).Controls(0)
                            'If obj.ID.Length = 5 Then
                            'Dim Idioma As String = obj.ID.Substring(3, 2)
                            'Dim Valor As String = My.Request.Form(obj.UniqueID)
                            'Comand.Parameters.AddWithValue("@ca_idioma", Idioma)
                            'Comand.Parameters.AddWithValue("@text", Valor)
                            Comand.ExecuteNonQuery()
                            'Else
                            'JJ.Registre.RegistrarEvent("-> " + obj.ID)
                            'End If
                            ''
                        End If
                    Else
                        JJ.Registre.RegistrarEvent("-> " + obj.ID)
                    End If
                Next
            End If
            'Imatges
            If My.Request.Files.Count = 0 Then
                Return True
            Else
                Return Me.GuardarImatges()
            End If
        Catch ex As Exception
            JJ.Registre.RegistrarErrada(ex.ToString)
            Return False
        Finally
            Conexio.Close()
        End Try
    End Function


    Private Function GuardarImatges() As Boolean
        Try
            'Ruta i nom de les fotos               
            Dim Ruta As String = Context.Server.MapPath("~/img/cat")
            Ruta = Ruta + "\" + Me.IdCate.Value
            If My.Request.Files("ctl00$ContentPlaceHolder1$UP") IsNot Nothing Then
                If My.Request.Files("ctl00$ContentPlaceHolder1$UP").InputStream.Length > 0 Then
                    'JJ.Grafics.Imatges.RedimensionarImatge(My.Request.Files("ctl00$ContentPlaceHolder1$UP").InputStream, 50, 50).Save(Ruta + ".png")
                    My.Request.Files("ctl00$ContentPlaceHolder1$UP").SaveAs(Ruta + ".png")
                End If
            End If
            If My.Request.Files("ctl00$ContentPlaceHolder1$UP2") IsNot Nothing Then
                If My.Request.Files("ctl00$ContentPlaceHolder1$UP2").InputStream.Length > 0 Then
                    'JJ.Grafics.Imatges.RedimensionarImatge(My.Request.Files("ctl00$ContentPlaceHolder1$UP2").InputStream, 25, 25).Save(Ruta + "(x).png")
                    My.Request.Files("ctl00$ContentPlaceHolder1$UP2").SaveAs(Ruta + "(x).png")
                End If
            End If
            Return True
        Catch ex As Exception
            JJ.Registre.RegistrarErrada(ex.ToString)
            Return False
        End Try
    End Function


    Private Function GetCategories(ByVal IdCategoria As Guid) As Hashtable
        GetCategories = New Hashtable()
        Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
        Dim Comand As New SqlCommand
        Try
            Conexio.Open()
            Comand.Connection = Conexio
            Comand.CommandText = "SELECT ca_idioma, text FROM CategoriesIdioma WHERE ca_categoria=@categoria"
            Comand.Parameters.AddWithValue("@categoria", IdCategoria)
            Dim Dades As SqlDataReader = Comand.ExecuteReader()
            While Dades.Read
                GetCategories.Add(Dades("ca_idioma"), Dades("text"))
            End While
            Dades.Close()
        Catch ex As Exception
            JJ.Registre.RegistrarErrada(ex.ToString)
            Return Nothing
        Finally
            Conexio.Close()
        End Try

    End Function


End Class
