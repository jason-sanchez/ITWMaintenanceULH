Imports System.Data.SqlClient

Namespace Users

    <Serializable()> _
    Public Class ReadOnlyUserList
        Inherits ReadOnlyListBase(Of ReadOnlyUserList, ReadOnlyUser)

#Region " Factory Methods "

        Public Shared Function GetUserList() As ReadOnlyUserList
            Return DataPortal.Fetch(Of ReadOnlyUserList)(New Criteria(0, True, Nothing))
        End Function

        Public Shared Function GetUserList(ByVal ActiveOnly As Boolean) As ReadOnlyUserList
            Return DataPortal.Fetch(Of ReadOnlyUserList)(New Criteria(0, ActiveOnly, Nothing))
        End Function

        Public Shared Function GetUserList(ByVal GroupID As Integer) As ReadOnlyUserList
            Return DataPortal.Fetch(Of ReadOnlyUserList)(New Criteria(GroupID, True, Nothing))
        End Function

        Public Shared Function GetUserList(ByVal GroupID As Integer, ByVal ActiveOnly As Boolean) As ReadOnlyUserList
            Return DataPortal.Fetch(Of ReadOnlyUserList)(New Criteria(GroupID, ActiveOnly, Nothing))
        End Function

        Public Shared Function GetUserList(ByVal SearchText As String) As ReadOnlyUserList
            Return DataPortal.Fetch(Of ReadOnlyUserList)(New Criteria(0, True, SearchText))
        End Function

        Public Shared Function GetUserList(ByVal SearchText As String, ByVal ActiveOnly As Boolean) As ReadOnlyUserList
            Return DataPortal.Fetch(Of ReadOnlyUserList)(New Criteria(0, ActiveOnly, SearchText))
        End Function

        Public Shared Function GetUserList(ByVal SearchText As String, ByVal GroupID As Integer) As ReadOnlyUserList
            Return DataPortal.Fetch(Of ReadOnlyUserList)(New Criteria(GroupID, True, SearchText))
        End Function

        Public Shared Function GetUserList(ByVal SearchText As String, ByVal GroupID As Integer, ByVal ActiveOnly As Boolean) As ReadOnlyUserList
            Return DataPortal.Fetch(Of ReadOnlyUserList)(New Criteria(GroupID, ActiveOnly, SearchText))
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

        Public Overloads Function Contains(ByVal UserID As Integer) As Boolean
            For Each item As ReadOnlyUser In Me
                If item.ID = UserID Then
                    Return True
                End If
            Next
            Return False
        End Function

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _GroupID As Integer
            Private _ActiveOnly As Boolean
            Private _SearchText As String

            Public ReadOnly Property GroupID() As Integer
                Get
                    Return Me._GroupID
                End Get
            End Property

            Public ReadOnly Property ActiveOnly() As Boolean
                Get
                    Return Me._ActiveOnly
                End Get
            End Property

            Public ReadOnly Property SearchText() As String
                Get
                    Return Me._SearchText
                End Get
            End Property

            Public Sub New(ByVal GroupID As Integer, ByVal ActiveOnly As Boolean, ByVal SearchText As String)
                Me._GroupID = GroupID
                Me._ActiveOnly = ActiveOnly
                Me._SearchText = SearchText
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            RaiseListChangedEvents = False

            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT u.[ID], (u.[LastName] + ', ' + u.[FirstName]) AS FullName, "
                    sql &= "u.[UserName], ISNULL(d.disName, '') AS Discipline, u.Inactive, u.intakeFacility "
                    sql &= "FROM [200User] u "
                    sql &= "LEFT JOIN [116Discipline] d ON u.discipline = d.disID "
                    sql &= "WHERE (1=1) "

                    If Not String.IsNullOrEmpty(criteria.SearchText) Then
                        ' Searching by Name.  Allow a fuzzy search on {Last Name, First Name}
                        Dim NameArray() As String = Split(criteria.SearchText, ",")

                        If UBound(NameArray) = 0 Then
                            ' Search on first name or username
                            sql &= "AND (u.[LastName] LIKE '" & Trim(NameArray(0)) & "%' "
                            sql &= "    OR u.[UserName] LIKE '" & NameArray(0) & "%') "
                        Else
                            ' Search on last name and first name
                            sql &= "AND u.[LastName] LIKE '" & Trim(NameArray(0)) & "%' "
                            sql &= "AND u.[FirstName] LIKE '" & Trim(NameArray(1)) & "%' "
                        End If
                    End If

                    If criteria.GroupID > 0 Then
                        sql &= "AND u.[GroupID] = " & criteria.GroupID.ToString() & " "
                    End If

                    If criteria.ActiveOnly Then
                        sql &= "AND u.[Inactive] = 0 "
                    End If
                    '06/02/2017 MW- #6825- filter list of facilities to user's
                    sql &= "AND u.[intakeFacility] = " & DirectCast(Csla.ApplicationContext.User.Identity, Security.ITWIdentity).IntakeFacility & " "
                    sql &= "ORDER BY u.[LastName], u.[FirstName] "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        IsReadOnly = False
                        While dr.Read()
                            Me.Add(New ReadOnlyUser(dr))
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