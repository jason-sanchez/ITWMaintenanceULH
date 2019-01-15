Imports System.Data.SqlClient

Namespace Interventions

    <Serializable()> _
    Public Class ReadOnlyIntVntLevel1FolderList
        Inherits ReadOnlyListBase(Of ReadOnlyIntVntLevel1FolderList, ReadOnlyIntVntLevel1Folder)

#Region " Factory Methods "

        Public Shared Function GetIntVntLevel1FolderList(ByVal ActiveOnly As Boolean) As ReadOnlyIntVntLevel1FolderList
            Return DataPortal.Fetch(Of ReadOnlyIntVntLevel1FolderList)(New Criteria(ActiveOnly))
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _ActiveOnly As Boolean

            Public ReadOnly Property ActiveOnly() As Boolean
                Get
                    Return Me._ActiveOnly
                End Get
            End Property

            Public Sub New(ByVal ActiveOnly As Boolean)
                Me._ActiveOnly = ActiveOnly
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            RaiseListChangedEvents = False

            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT i.[ID], i.intVntID, i.iName as FolderName, "
                    sql &= "d.disName as Discipline, i.DOrder as DisplayOrder, i.Inactive "
                    sql &= "FROM [110intVnt] i "
                    sql &= "INNER JOIN [116Discipline] d on i.discipline = d.disID "
                    sql &= "WHERE i.intVntLevel = 1 "
                    If criteria.ActiveOnly Then
                        sql &= "AND i.Inactive = 0 "
                    End If
                    sql &= "ORDER BY d.disName, i.DOrder, i.intVntID "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        IsReadOnly = False
                        While dr.Read()
                            Me.Add(New ReadOnlyIntVntLevel1Folder(dr))
                        End While
                        IsReadOnly = True
                    End Using
                End Using
            End Using

            RaiseListChangedEvents = True
        End Sub

#End Region

    End Class

End Namespace