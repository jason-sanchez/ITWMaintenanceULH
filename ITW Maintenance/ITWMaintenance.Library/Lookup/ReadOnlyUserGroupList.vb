Imports System.Data.SqlClient

Namespace Lookup

    <Serializable()> _
    Public Class ReadOnlyUserGroupList
        Inherits ReadOnlyListBase(Of ReadOnlyUserGroupList, ReadOnlyUserGroup)

#Region " Factory Methods "

        Public Shared Function GetUserGroupList() As ReadOnlyUserGroupList
            Return DataPortal.Fetch(Of ReadOnlyUserGroupList)()
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

        Public Overloads Function Contains(ByVal GroupID As Integer) As Boolean
            For Each item As ReadOnlyUserGroup In Me
                If item.ID = GroupID Then
                    Return True
                End If
            Next
            Return False
        End Function

#End Region

#Region " Data Access "

        Private Overloads Sub DataPortal_Fetch()
            RaiseListChangedEvents = False

            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT [GroupID], [Description] "
                    sql &= "FROM [200UGroup] "
                    sql &= "ORDER BY [Description] "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        IsReadOnly = False
                        While dr.Read()
                            Me.Add(New ReadOnlyUserGroup(dr))
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