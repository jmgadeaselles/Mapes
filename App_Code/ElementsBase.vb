Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Collections.Generic

Namespace JJ.DadesBase


    Public MustInherit Class ElementBase
        Protected MustOverride Function ObrirElement(ByVal Comand As SqlCommand, ByVal IdLloc As Guid, ByVal IdiomaLloc As String) As Boolean
        Protected MustOverride Function GuardarElement(ByVal Comand As SqlCommand) As Boolean
        Protected MustOverride Sub ClearElement()
        Dim _Id As Guid
        Dim _Idioma As String
        Dim _Nou As Boolean

        Public Sub New(ByVal Idioma As String)
            Me.Nou(Idioma)
        End Sub

        Public ReadOnly Property Id() As Guid
            Get
                Return Me._Id
            End Get
        End Property

        Public ReadOnly Property Idioma As String
            Get
                Return Me._Idioma
            End Get
        End Property

        Public ReadOnly Property EsNou() As Boolean
            Get
                Return Me._Nou
            End Get
        End Property

        Public Sub Nou(ByVal Idioma As String)
            Me._Id = Guid.NewGuid()
            Me._Idioma = Idioma
            Me._Nou = True
            Me.ClearElement()
        End Sub

        Public Function Obrir(ByVal Id As Guid, ByVal Idioma As String) As Boolean
            Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
            Dim Comand As New SqlCommand
            Try
                Conexio.Open()
                Comand.Connection = Conexio
                If Not Me.ObrirElement(Comand, Id, Idioma) Then
                    Return False
                Else
                    Me._Id = Id
                    Me._Idioma = Idioma
                    Me._Nou = False
                    Return True
                End If
            Catch ex As Exception
                JJ.Registre.RegistrarErrada(ex)
                Return False
            Finally
                Conexio.Close()
            End Try
        End Function

        Public Function Guardar() As Boolean            
            Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
            Dim Comand As New SqlCommand
            Try
                Conexio.Open()
                Comand.Connection = Conexio
                'Comand.CommandText = "BEGIN TRAN"
                'Comand.ExecuteNonQuery()
                Dim OK As Boolean = Me.GuardarElement(Comand)
                If OK Then
                    'Comand.CommandText = "COMMIT TRAN"
                    'Comand.ExecuteNonQuery()
                    Return True
                Else
                    'Comand.CommandText = "ROLLBACK TRAN"
                    'Comand.ExecuteNonQuery()
                    Return False
                End If
            Catch ex As Exception
                Try
                    'Comand.CommandText = "ROLLBACK TRAN"
                    'Comand.ExecuteNonQuery()
                Catch ex1 As Exception
                    'No fem res
                End Try
                JJ.Registre.RegistrarErrada(ex)
                Return False
            Finally
                Conexio.Close()
            End Try

        End Function

        Public Sub SetId(ByVal IdElement As Guid)
            Me._Id = IdElement
            Me._Nou = False
            Me.ClearElement()
        End Sub
    End Class


    Namespace ElementsPerIdioma


        Public Class ElementIdiomaClass
            Dim _Idioma, _Text As String

            Public Sub New(ByVal Idioma As String, ByVal Text As String)
                Me._Idioma = Idioma
                Me._Text = Text
            End Sub

            Public ReadOnly Property Idioma() As String
                Get
                    Return Me._Idioma
                End Get
            End Property

            Public Property Text() As String
                Get
                    Return Me._Text
                End Get
                Set(value As String)
                    Me._Text = value
                End Set
            End Property
        End Class

        Public Class LlistatElementsIdiomaClass
            Dim _Idiomes As JJ.Idiomes.LlistatIdiomesDades
            Dim _Item As Hashtable

            Public Sub New()
                Me._Item = New Hashtable()
                Me._Idiomes = New JJ.Idiomes.LlistatIdiomesDades()
                For i As Integer = 0 To Me._Idiomes.Count - 1
                    Dim Element As New ElementIdiomaClass(Me._Idiomes(i).Codi, "")
                    Me._Item.Add(Me._Idiomes(i).Codi, Element)
                Next
            End Sub

            Default Public ReadOnly Property Item(ByVal Index As Integer) As ElementIdiomaClass
                Get
                    Return Me._Item(Me._Idiomes(Index).Codi)
                End Get
            End Property
            Default Public ReadOnly Property Item(ByVal Idioma As String) As ElementIdiomaClass
                Get
                    Return Me._Item(Idioma)
                End Get
            End Property

            Public ReadOnly Property Count() As Integer
                Get
                    Return Me._Idiomes.Count
                End Get
            End Property


            Public Sub Clear()
                For i As Integer = 0 To Me._Item.Count - 1
                    Dim Element As ElementIdiomaClass = Me._Item(Me._Idiomes(i).Codi)
                    Element.Text = ""
                Next
            End Sub

        End Class


    End Namespace



    Namespace Fotos


        Public Class LlistatFotosClass
            Dim _Item As List(Of FotoClass)



        End Class



        Public Class FotoClass
            Dim _FotoGran, _FotoMitjana, _FotoXicoteta As String
            Dim _Comentari As JJ.DadesBase.ElementsPerIdioma.LlistatElementsIdiomaClass
            Dim _Nou As Boolean

            Public Sub New(ByVal NovaFoto As String)
                Me._Nou = True
                'Comentari
                Me._Comentari = New JJ.DadesBase.ElementsPerIdioma.LlistatElementsIdiomaClass()
                'Fotos escalades
                Dim Config As New FotosEscalades()
                Dim FotoRedimensionada As System.Drawing.Bitmap = JJ.Grafics.Imatges.RedimensionarImatge(NovaFoto, Config(0).Width, Config(0).Height)




            End Sub
            Public Sub New(ByVal FotoGran As String, ByVal FotoMitjana As String, ByVal FotoXicoteta As String, ByVal Comentari As ElementsPerIdioma.LlistatElementsIdiomaClass)
                Me._Nou = False
                Me._FotoGran = FotoGran
                Me._FotoMitjana = FotoMitjana
                Me._FotoXicoteta = FotoXicoteta
                Me._Comentari = Comentari
            End Sub

            Public ReadOnly Property EsNou() As Boolean
                Get
                    Return Me._Nou
                End Get
            End Property

            Public ReadOnly Property FotoGran() As String
                Get
                    Return Me._FotoGran
                End Get
            End Property
            Public ReadOnly Property FotoMitjana() As String
                Get
                    Return Me._FotoMitjana
                End Get
            End Property
            Public ReadOnly Property FitoXicoteta() As String
                Get
                    Return Me._FotoXicoteta
                End Get
            End Property

            Public ReadOnly Property Comentari() As ElementsPerIdioma.LlistatElementsIdiomaClass
                Get
                    Return Me._Comentari
                End Get
            End Property



            Public Class FotosEscalades
                Dim _Tamanys As List(Of JJ.Grafics.Size)

                Public Sub New()
                    Me._Tamanys = New List(Of JJ.Grafics.Size)
                    Me._Tamanys.Add(New JJ.Grafics.Size(CType(ConfigurationManager.AppSettings("foto1.ample").ToString(), Integer), CType(ConfigurationManager.AppSettings("foto1.alt").ToString(), Integer)))
                    Me._Tamanys.Add(New JJ.Grafics.Size(CType(ConfigurationManager.AppSettings("foto2.ample").ToString(), Integer), CType(ConfigurationManager.AppSettings("foto2.alt").ToString(), Integer)))
                    Me._Tamanys.Add(New JJ.Grafics.Size(CType(ConfigurationManager.AppSettings("foto3.ample").ToString(), Integer), CType(ConfigurationManager.AppSettings("foto3.alt").ToString(), Integer)))
                End Sub

                Default Public ReadOnly Property Item(ByVal Index As Integer) As JJ.Grafics.Size
                    Get
                        Return Me._Tamanys(Index)
                    End Get
                End Property

                Public ReadOnly Property Count() As Integer
                    Get
                        Return Me._Tamanys.Count
                    End Get
                End Property

            End Class


            Public Class FotosUsuarisEscalades
                Dim _Tamanys As List(Of JJ.Grafics.Size)

                Public Sub New()
                    Me._Tamanys = New List(Of JJ.Grafics.Size)
                    Me._Tamanys.Add(New JJ.Grafics.Size(CType(ConfigurationManager.AppSettings("foto1.usr.ample").ToString(), Integer), CType(ConfigurationManager.AppSettings("foto1.usr.alt").ToString(), Integer)))
                    Me._Tamanys.Add(New JJ.Grafics.Size(CType(ConfigurationManager.AppSettings("foto2.usr.ample").ToString(), Integer), CType(ConfigurationManager.AppSettings("foto2.usr.alt").ToString(), Integer)))
                    Me._Tamanys.Add(New JJ.Grafics.Size(CType(ConfigurationManager.AppSettings("foto3.usr.ample").ToString(), Integer), CType(ConfigurationManager.AppSettings("foto3.usr.alt").ToString(), Integer)))
                End Sub

                Default Public ReadOnly Property Item(ByVal Index As Integer) As JJ.Grafics.Size
                    Get
                        Return Me._Tamanys(Index)
                    End Get
                End Property

                Public ReadOnly Property Count() As Integer
                    Get
                        Return Me._Tamanys.Count
                    End Get
                End Property

            End Class



        End Class




    End Namespace


    Namespace Historic


        Public Class HistoricClass
            Dim _Usuari As JJ.Usuaris.UsuariDadesClass
            Dim _Data As Date

            Public Sub New(ByVal Data As Date, ByVal Usuari As JJ.Usuaris.UsuariDadesClass)
                Me._Data = Data
                Me._Usuari = Usuari
            End Sub

            Public ReadOnly Property Data As Date
                Get
                    Return Me._Data
                End Get
            End Property

            Public ReadOnly Property Usuari As JJ.Usuaris.UsuariDadesClass
                Get
                    Return Me._Usuari
                End Get
            End Property



        End Class


    End Namespace




End Namespace
