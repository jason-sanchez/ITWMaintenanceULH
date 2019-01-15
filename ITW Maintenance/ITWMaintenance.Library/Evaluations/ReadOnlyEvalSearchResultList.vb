Imports System.Data.SqlClient

Namespace Evaluations

    <Serializable()> _
    Public Class ReadOnlyEvalSearchResultList
        Inherits ReadOnlyListBase(Of ReadOnlyEvalSearchResultList, ReadOnlyEvalSearchResult)

#Region " Factory Methods "

        Public Shared Function Search(ByVal EvalID As Integer, ByVal SortBy As SortField) As ReadOnlyEvalSearchResultList
            Return DataPortal.Fetch(Of ReadOnlyEvalSearchResultList)(New Criteria(EvalID, SortBy))
        End Function

        Public Shared Function Search(ByVal SearchText As String, ByVal SortBy As SortField) As ReadOnlyEvalSearchResultList
            Return DataPortal.Fetch(Of ReadOnlyEvalSearchResultList)(New Criteria(SearchText, SortBy))
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        Public Enum SortField
            Path = 1
            Name = 2
            ID = 3
        End Enum

        <Serializable()> _
        Private Class Criteria
            Private _EvalID As Integer = 0
            Private _SearchText As String
            Private _SortBy As SortField

            Public ReadOnly Property EvalID() As Integer
                Get
                    Return _EvalID
                End Get
            End Property

            Public ReadOnly Property SearchText() As String
                Get
                    Return _SearchText
                End Get
            End Property

            Public ReadOnly Property SortBy() As SortField
                Get
                    Return _SortBy
                End Get
            End Property

            Public Sub New(ByVal EvalID As Integer, ByVal SortBy As SortField)
                _EvalID = EvalID
                _SortBy = SortBy
            End Sub

            Public Sub New(ByVal SearchText As String, ByVal SortBy As SortField)
                _SearchText = SearchText
                _SortBy = SortBy
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            RaiseListChangedEvents = False

            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT [ID], EvalID, EName, EPath, "
                    sql &= "EFinal, Inactive, Level2 "
                    sql &= "FROM [100Eval] "
                    If criteria.EvalID > 0 Then
                        sql &= "WHERE EvalID = " & criteria.EvalID & " "
                    Else
                        sql &= "WHERE EName LIKE '%" & criteria.SearchText & "%' "
                    End If

                    Select Case criteria.SortBy
                        Case SortField.ID
                            sql &= "ORDER BY EvalID "
                        Case SortField.Name
                            sql &= "ORDER BY EName "
                        Case Else
                            sql &= "ORDER BY EPath "
                    End Select

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        IsReadOnly = False
                        While dr.Read()
                            Me.Add(New ReadOnlyEvalSearchResult(dr))
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