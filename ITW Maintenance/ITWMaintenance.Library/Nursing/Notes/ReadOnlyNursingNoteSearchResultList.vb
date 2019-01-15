Imports System.Data.SqlClient

Namespace Nursing

    Namespace Notes

        <Serializable()> _
        Public Class ReadOnlyNursingNoteSearchResultList
            Inherits ReadOnlyListBase(Of ReadOnlyNursingNoteSearchResultList, ReadOnlyNursingNoteSearchResult)

#Region " Factory Methods "

            Public Shared Function Search(ByVal FormID As Integer, ByVal SortBy As SortField, Optional ByVal OnlySearchForms As Boolean = False) As ReadOnlyNursingNoteSearchResultList
                Return DataPortal.Fetch(Of ReadOnlyNursingNoteSearchResultList)(New Criteria(FormID, SortBy, OnlySearchForms))
            End Function

            Public Shared Function Search(ByVal SearchText As String, ByVal SortBy As SortField, Optional ByVal OnlySearchForms As Boolean = False) As ReadOnlyNursingNoteSearchResultList
                Return DataPortal.Fetch(Of ReadOnlyNursingNoteSearchResultList)(New Criteria(SearchText, SortBy, OnlySearchForms))
            End Function

            Private Sub New()
                ' Require use of factory methods
            End Sub

#End Region

#Region " Data Access "

            Public Enum SortField
                Name = 1
                ID = 2
            End Enum

            <Serializable()> _
            Private Class Criteria
                Private _FormID As Integer = 0
                Private _SearchText As String
                Private _SortBy As SortField
                Private _OnlySearchForms As Boolean = False

                Public ReadOnly Property FormID() As Integer
                    Get
                        Return _FormID
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

                Public Sub New(ByVal FormID As Integer, ByVal SortBy As SortField, ByVal OnlySearchForms As Boolean)
                    _FormID = FormID
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

                        sql = "SELECT n.[nsNtID] AS [ID], n.[NName] AS [Name], "
                        sql &= "d.[disName] AS [Discipline], n.[NInactive] AS [Inactive], "
                        sql &= "n.[nFinal] AS [Final] "
                        sql &= "FROM [100CrNt] n "
                        sql &= "LEFT JOIN [116Discipline] d ON n.[discipline] = d.[DisID] "

                        If criteria.FormID > 0 Then
                            sql &= "WHERE n.[nsNtID] = " & criteria.FormID & " "
                        Else
                            sql &= "WHERE n.[NName] LIKE '%" & criteria.SearchText & "%' "
                        End If

                        If criteria.OnlySearchForms Then
                            sql &= "AND n.[NFinal] = 1 "
                        End If

                        Select Case criteria.SortBy
                            Case SortField.ID
                                sql &= "ORDER BY n.[nsNtID] "
                            Case Else
                                sql &= "ORDER BY n.[NName] "
                        End Select

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql

                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            IsReadOnly = False
                            While dr.Read()
                                Me.Add(New ReadOnlyNursingNoteSearchResult(dr))
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

End Namespace