Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Collections.Generic


Namespace JJ.Geografic



    Namespace Paissos


        Public Class PaisClass
            Dim _Iso, _Nom As String

            Public Sub New(ByVal NouIso As String, ByVal NouNom As String)
                Me._Iso = NouIso
                Me._Nom = NouNom
            End Sub

            Public ReadOnly Property Iso As String
                Get
                    Return Me._Iso
                End Get
            End Property
            Public ReadOnly Property Nom As String
                Get
                    Return Me._Nom
                End Get
            End Property



        End Class


        Public Class LlistatPaissosClass
            Dim _Paissos As List(Of PaisClass)            

            Public Sub New()
                Me._Paissos = New List(Of PaisClass)
                Dim Conexio As New SqlConnection(JJ.Config.BBDD.CadenaConexio)
                Dim Comand As New SqlCommand
                Try
                    Conexio.Open()
                    Comand.Connection = Conexio
                    Comand.CommandText = "SELECT iso, nomEN FROM Paissos WHERE habilitat=@true ORDER BY nomEN"
                    Comand.Parameters.AddWithValue("@true", True)
                    Dim Dades As SqlDataReader = Comand.ExecuteReader()
                    While Dades.Read
                        Me._Paissos.Add(New PaisClass(CType(Dades("iso"), String), CType(Dades("nomEN"), String)))
                    End While
                    Dades.Close()
                Catch ex As Exception
                    JJ.Registre.RegistrarErrada(ex)
                Finally
                    Conexio.Close()
                End Try
            End Sub

            Default Public ReadOnly Property Item(ByVal Index As Integer) As PaisClass
                Get
                    Return Me._Paissos(Index)
                End Get
            End Property

            Public ReadOnly Property Count() As Integer
                Get
                    Return Me._Paissos.Count
                End Get
            End Property

        End Class


    End Namespace


End Namespace





