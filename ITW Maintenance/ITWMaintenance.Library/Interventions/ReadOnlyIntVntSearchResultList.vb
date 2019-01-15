Imports System.Data.SqlClient

Namespace Interventions

    <Serializable()> _
    Public Class ReadOnlyIntVntSearchResultList
        Inherits ReadOnlyListBase(Of ReadOnlyIntVntSearchResultList, ReadOnlyIntVntSearchResult)

#Region " Factory Methods "

        Public Shared Function Search(ByVal intVntID As Integer, ByVal SortBy As SortField) As ReadOnlyIntVntSearchResultList
            Return DataPortal.Fetch(Of ReadOnlyIntVntSearchResultList)(New Criteria(intVntID, SortBy))
        End Function

        Public Shared Function Search(ByVal SearchText As String, ByVal SortBy As SortField) As ReadOnlyIntVntSearchResultList
            Return DataPortal.Fetch(Of ReadOnlyIntVntSearchResultList)(New Criteria(SearchText, SortBy))
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
            Private _intVntID As Integer = 0
            Private _SearchText As String
            Private _SortBy As SortField

            Public ReadOnly Property intVntID() As Integer
                Get
                    Return _intVntID
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

            Public Sub New(ByVal intVntID As Integer, ByVal SortBy As SortField)
                _intVntID = intVntID
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

                    sql = "SELECT [ID], intVntID, iName, iPath, "
                    sql &= "iFinal, Inactive, Level2 "
                    sql &= "FROM [110intVnt] "
                    If criteria.intVntID > 0 Then
                        sql &= "WHERE intVntID = " & criteria.intVntID & " "
                    Else
                        sql &= "WHERE iName LIKE '%" & criteria.SearchText & "%' "
                    End If

                    Select Case criteria.SortBy
                        Case SortField.ID
                            sql &= "ORDER BY intVntID "
                        Case SortField.Name
                            sql &= "ORDER BY iName "
                        Case Else
                            sql &= "ORDER BY iPath "
                    End Select

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        IsReadOnly = False
                        While dr.Read()
                            Me.Add(New ReadOnlyIntVntSearchResult(dr))
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