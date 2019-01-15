Imports System.Data.SqlClient

Namespace Users

    <Serializable()> _
    Public Class ReadOnlyUser
        Inherits ReadOnlyBase(Of ReadOnlyUser)

#Region " Business Methods "

        Private Shared IDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
        Public ReadOnly Property ID() As Integer
            Get
                Return GetProperty(Of Integer)(IDProperty)
            End Get
        End Property

        Private Shared FullNameProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("FullName", "Full Name"))
        Public ReadOnly Property FullName() As String
            Get
                Return GetProperty(Of String)(FullNameProperty)
            End Get
        End Property

        Private Shared UserNameProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("UserName", "User Name"))
        Public ReadOnly Property UserName() As String
            Get
                Return GetProperty(Of String)(UserNameProperty)
            End Get
        End Property

        Private Shared DisciplineProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Discipline"))
        Public ReadOnly Property Discipline() As String
            Get
                Return GetProperty(Of String)(DisciplineProperty)
            End Get
        End Property

        Private Shared InactiveProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("Inactive"))
        Public ReadOnly Property Inactive() As Boolean
            Get
                Return GetProperty(Of Boolean)(InactiveProperty)
            End Get
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return GetProperty(Of Integer)(IDProperty)
        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return True
        End Function

#End Region

#Region " Factory Methods "

        Private Sub New()
            ' Require use of factory methods
        End Sub

        Friend Sub New(ByRef dr As SafeDataReader)
            With dr
                LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                LoadProperty(Of String)(FullNameProperty, .GetString("FullName"))
                LoadProperty(Of String)(UserNameProperty, .GetString("UserName"))
                LoadProperty(Of String)(DisciplineProperty, .GetString("Discipline"))
                LoadProperty(Of Boolean)(InactiveProperty, .GetBoolean("Inactive"))
            End With
        End Sub

        Public Shared Function GetUser(ByVal UserID As Integer) As ReadOnlyUser
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view a User")
            End If
            Return DataPortal.Fetch(Of ReadOnlyUser)(New Criteria(UserID))
        End Function

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _UserID As Integer

            Public ReadOnly Property UserID() As Integer
                Get
                    Return Me._UserID
                End Get
            End Property

            Public Sub New(ByVal UserID As Integer)
                Me._UserID = UserID
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT u.[ID], (u.[LastName] + ', ' + u.[FirstName]) AS FullName, "
                    sql &= "u.[UserName], ISNULL(d.disName, '') AS Discipline, u.Inactive "
                    sql &= "FROM [200User] u "
                    sql &= "LEFT JOIN [116Discipline] d ON u.discipline = d.disID "
                    sql &= "WHERE u.[ID] = " & criteria.UserID.ToString() & " "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        dr.Read()
                        With dr
                            LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                            LoadProperty(Of String)(FullNameProperty, .GetString("FullName"))
                            LoadProperty(Of String)(UserNameProperty, .GetString("UserName"))
                            LoadProperty(Of String)(DisciplineProperty, .GetString("Discipline"))
                            LoadProperty(Of Boolean)(InactiveProperty, .GetBoolean("Inactive"))
                        End With
                    End Using
                End Using
            End Using
        End Sub

#End Region

    End Class

End Namespace