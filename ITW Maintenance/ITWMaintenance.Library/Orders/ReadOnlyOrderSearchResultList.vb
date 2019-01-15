Imports System.Data.SqlClient

Namespace Orders

    <Serializable()> _
    Public Class ReadOnlyOrderSearchResultList
        Inherits ReadOnlyListBase(Of ReadOnlyOrderSearchResultList, ReadOnlyOrderSearchResult)

#Region " Factory Methods "

        Public Shared Function Search(ByVal OrderID As Integer, ByVal SortBy As SortField, Optional ByVal OnlySearchForms As Boolean = False) As ReadOnlyOrderSearchResultList
            Return DataPortal.Fetch(Of ReadOnlyOrderSearchResultList)(New Criteria(OrderID, SortBy, OnlySearchForms))
        End Function

        Public Shared Function Search(ByVal SearchText As String, ByVal SortBy As SortField, Optional ByVal OnlySearchForms As Boolean = False) As ReadOnlyOrderSearchResultList
            Return DataPortal.Fetch(Of ReadOnlyOrderSearchResultList)(New Criteria(SearchText, SortBy, OnlySearchForms))
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
            Private _OrderID As Integer = 0
            Private _SearchText As String
            Private _SortBy As SortField
            Private _OnlySearchForms As Boolean = False

            Public ReadOnly Property OrderID() As Integer
                Get
                    Return _OrderID
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

            Public ReadOnly Property OnlySearchForms() As Boolean
                Get
                    Return _OnlySearchForms
                End Get
            End Property

            Public Sub New(ByVal OrderID As Integer, ByVal SortBy As SortField, ByVal OnlySearchForms As Boolean)
                _OrderID = OrderID
                _SortBy = SortBy
                _OnlySearchForms = OnlySearchForms
            End Sub

            Public Sub New(ByVal SearchText As String, ByVal SortBy As SortField, ByVal OnlySearchForms As Boolean)
                _SearchText = SearchText
                _SortBy = SortBy
                _OnlySearchForms = OnlySearchForms
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            RaiseListChangedEvents = False

            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT [ID], [Name], [Path], [Form], [Inactive] "
                    sql &= "FROM [109Order] "

                    If criteria.OrderID <> 0 Then
                        sql &= "WHERE [ID] = " & criteria.OrderID & " "
                    Else
                        sql &= "WHERE [Name] LIKE '%" & criteria.SearchText & "%' "
                    End If

                    If criteria.OnlySearchForms Then
                        sql &= "AND [Form] = 1 "
                    End If

                    Select Case criteria.SortBy
                        Case SortField.ID
                            sql &= "ORDER BY [ID] "
                        Case SortField.Name
                            sql &= "ORDER BY [Name] "
                        Case Else
                            sql &= "ORDER BY CAST([Path] AS NVARCHAR(1000)) "
                    End Select

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        IsReadOnly = False
                        While dr.Read()
                            Me.Add(New ReadOnlyOrderSearchResult(dr))
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