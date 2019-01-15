Imports System.Data.SqlClient

Namespace Orders

    <Serializable()> _
    Public Class ReadOnlyOrderList
        Inherits ReadOnlyListBase(Of ReadOnlyOrderList, ReadOnlyOrder)

#Region " Factory Methods "

        Public Shared Function GetOrderList(ByVal ParentID As Integer, ByVal ActiveOnly As Boolean) As ReadOnlyOrderList
            Return DataPortal.Fetch(Of ReadOnlyOrderList)(New Criteria(ParentID, ActiveOnly))
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _ParentID As Integer
            Private _ActiveOnly As Boolean

            Public ReadOnly Property ParentID() As Integer
                Get
                    Return Me._ParentID
                End Get
            End Property

            Public ReadOnly Property ActiveOnly() As Boolean
                Get
                    Return Me._ActiveOnly
                End Get
            End Property

            Public Sub New(ByVal ParentID As Integer, ByVal ActiveOnly As Boolean)
                Me._ParentID = ParentID
                Me._ActiveOnly = ActiveOnly
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            RaiseListChangedEvents = False

            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT o.[ID], o.[ParentID], o.[Name], o.[Path], o.[Form], o.[DisplayOrder], "
                    sql &= "dFrom.[DisName] AS FromDiscipline, dTo.[DisName] AS ToDiscipline, "
                    sql &= "o.[QuickPick], o.[Inactive] "
                    sql &= "FROM [109Order] o "
                    sql &= "LEFT JOIN [116Discipline] dFrom ON o.[FromDiscipline] = dFrom.[disID] "
                    sql &= "LEFT JOIN [116Discipline] dTo ON o.[ToDiscipline] = dTo.[disID] "
                    sql &= "WHERE o.[ParentID] = " & criteria.ParentID & " "
                    If criteria.ActiveOnly Then
                        sql &= "AND o.[Inactive] = 0 "
                    End If
                    sql &= "ORDER BY o.[DisplayOrder] "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        IsReadOnly = False
                        While dr.Read()
                            Me.Add(New ReadOnlyOrder(dr))
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