Imports System.Data.SqlClient

Namespace Orders

    <Serializable()> _
    Public Class ReadOnlyOrderCatalogItemList
        Inherits ReadOnlyListBase(Of ReadOnlyOrderCatalogItemList, ReadOnlyOrderCatalogItem)

#Region " Factory Methods "

        Public Shared Function GetOrders(ByVal SearchText As String) As ReadOnlyOrderCatalogItemList
            Return DataPortal.Fetch(Of ReadOnlyOrderCatalogItemList)(New Criteria(SearchText))
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _SearchText As String

            Public ReadOnly Property SearchText() As String
                Get
                    Return Me._SearchText
                End Get
            End Property

            Public Sub New(ByVal SearchText As String)
                Me._SearchText = SearchText
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            RaiseListChangedEvents = False

            Using conn As New SqlConnection(Database.ITWCernerConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT [ID], [Alias], [Description], [OrderGroup], [LabCollect], [AutoExpandDetails] "
                    sql &= "FROM [OrderCatalog] "
                    sql &= "WHERE (1=1) "
                    If Not String.IsNullOrEmpty(criteria.SearchText) Then
                        sql &= "AND ([Alias] LIKE '" & criteria.SearchText & "%' "
                        sql &= "    OR [Description] LIKE '" & criteria.SearchText & "%' "
                        sql &= "    OR [OrderGroup] LIKE '" & criteria.SearchText & "%') "
                    End If
                    sql &= "ORDER BY [Alias] "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        IsReadOnly = False
                        While dr.Read()
                            Me.Add(New ReadOnlyOrderCatalogItem(dr))
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