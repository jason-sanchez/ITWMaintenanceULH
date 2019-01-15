Imports System.Security.Principal
Imports System.Data.SqlClient

Namespace Security

    <Serializable()> _
    Public Class ITWPrincipal
        Inherits Csla.Security.BusinessPrincipalBase

        Private Sub New(ByVal Identity As IIdentity)
            MyBase.New(Identity)
        End Sub

        'Public Shared Function VerifyCredentials(ByVal UserName As String, ByVal Password As String) As Boolean
        '    Return UsernamePasswordValidator.Validate(UserName, Password)
        'End Function

        Public Shared Function Login(ByVal UserName As String, ByVal Password As String) As Boolean
            Try
                Return SetPrincipal(ITWIdentity.GetIdentity(UserName, Password))
            Catch ex As LoginException
                Throw ex
            End Try
        End Function

        Private Shared Function SetPrincipal(ByVal Identity As ITWIdentity) As Boolean
            If Identity.IsAuthenticated Then
                Csla.ApplicationContext.User = New ITWPrincipal(Identity)
            End If
            Return Identity.IsAuthenticated
        End Function

        Public Shared Sub Logout()
            Csla.ApplicationContext.User = New ITWPrincipal(ITWIdentity.UnauthenticatedIdentity())
        End Sub

        Public Overrides Function IsInRole(ByVal Role As String) As Boolean
            Return DirectCast(Me.Identity, ITWIdentity).IsInRole(Role)
        End Function

        '#Region " UsernamePasswordValidator "

        '        <Serializable()> _
        '        Private Class UsernamePasswordValidator
        '            Inherits ReadOnlyBase(Of UsernamePasswordValidator)

        '#Region " Business Methods "

        '            Private _ValidUser As Boolean
        '            Private _ID As Integer

        '            Public ReadOnly Property ValidUser() As Boolean
        '                Get
        '                    Return _ValidUser
        '                End Get
        '            End Property

        '            Protected Overrides Function GetIdValue() As Object
        '                Return _ID
        '            End Function

        '#End Region

        '#Region " Factory Methods "

        '            Public Shared Function Validate(ByVal UserName As String, ByVal Password As String) As Boolean
        '                Dim validator As UsernamePasswordValidator = DataPortal.Fetch(Of UsernamePasswordValidator)(New Criteria(UserName, Password))
        '                Return validator.ValidUser
        '            End Function

        '            Private Sub New()
        '                ' Require use of factory methods
        '            End Sub

        '#End Region

        '#Region " Data Access "

        '            <Serializable()> _
        '            Private Class Criteria
        '                Private _UserName As String
        '                Private _Password As String

        '                Public ReadOnly Property UserName() As String
        '                    Get
        '                        Return _UserName
        '                    End Get
        '                End Property

        '                Public ReadOnly Property Password() As String
        '                    Get
        '                        Return _Password
        '                    End Get
        '                End Property

        '                Public Sub New(ByVal UserName As String, ByVal Password As String)
        '                    _UserName = UserName
        '                    _Password = Password
        '                End Sub
        '            End Class

        '            Private Overloads Sub DataPortal_Fetch(ByVal Criteria As Criteria)
        '                _ValidUser = False
        '                Using conn As New SqlConnection(Database.SchedulingConnection)
        '                    conn.Open()
        '                    Using cmd As SqlCommand = conn.CreateCommand
        '                        cmd.CommandType = CommandType.StoredProcedure
        '                        cmd.CommandText = "Scheduling-User_Validate"
        '                        cmd.Parameters.AddWithValue("@UserName", Criteria.UserName)
        '                        cmd.Parameters.AddWithValue("@Password", Criteria.Password)
        '                        Using dr As New SafeDataReader(cmd.ExecuteReader)
        '                            If dr.Read() Then
        '                                _ValidUser = True
        '                            Else
        '                                _ValidUser = False
        '                            End If
        '                        End Using
        '                    End Using
        '                End Using
        '            End Sub

        '#End Region

        '        End Class

        '#End Region

    End Class

End Namespace
