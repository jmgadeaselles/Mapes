Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Collections.Generic

Namespace JJ.Categories



    Public Class CategoriaClass
        Dim _Id As Guid
        Dim _Nom As String

        Public Sub New(ByVal Id As Guid, ByVal Nom As String)
            Me._Id = Id
            Me._Nom = Nom
        End Sub

        Public ReadOnly Property Id() As Guid
            Get
                Return Me._Id
            End Get
        End Property

        Public ReadOnly Property Nom() As String
            Get
                Return Me._Nom
            End Get
        End Property
    End Class



    Namespace Llocs


        Public Class LlistatCategoriesClass
            Dim _Idioma As String
            Dim _Categories As List(Of CategoriaClass)

            Public Sub New(ByVal Idioma As String)
                Me._Idioma = Idioma
                Me._Categories = New List(Of CategoriaClass)
                Me.Carregar()
            End Sub

            Public ReadOnly Property Idioma() As String
                Get
                    Return Me._Idioma
                End Get
            End Property

            Default Public ReadOnly Property Categories(ByVal Index As Integer) As CategoriaClass
                Get
                    Return Me._Categories(Index)
                End Get
            End Property

            Public ReadOnly Property Count As Integer
                Get
                    Return Me._Categories.Count
                End Get
            End Property

            Private Sub Carregar()
                Me._Categories.Clear()
                Dim Conexion As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
                Dim Comando As New SqlCommand
                Try
                    Conexion.Open()
                    Comando.Connection = Conexion
                    Comando.CommandText = "SELECT C.id, CI.text FROM Categories C"
                    Comando.CommandText += " JOIN CategoriesIdioma CI ON C.id=CI.ca_categoria"
                    Comando.CommandText += " WHERE CI.ca_idioma=@idioma AND C.habilitat=@habilitat"
                    Comando.CommandText += " ORDER BY CI.text"
                    Comando.Parameters.AddWithValue("@idioma", Me._Idioma)
                    Comando.Parameters.AddWithValue("@habilitat", True)
                    Dim Dades As SqlDataReader = Comando.ExecuteReader()
                    While Dades.Read
                        Me._Categories.Add(New CategoriaClass(Dades("id"), Dades("text")))
                    End While
                    Dades.Close()
                Catch ex As Exception
                    JJ.Registre.RegistrarErrada(ex)
                Finally
                    Conexion.Close()
                End Try
            End Sub

        End Class


    End Namespace




End Namespace