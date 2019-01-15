Imports System.Security.Principal
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationManager

Namespace Security

    <Serializable()> _
    Public Class ITWIdentity
        Inherits ReadOnlyBase(Of ITWIdentity)

        Implements IIdentity

#Region " Business Methods "

        Protected Overrides Function GetIdValue() As Object
            Return Me._UserID
        End Function

#Region " IsInRole "

        Private _Roles As New List(Of String)

        Friend Function IsInRole(ByVal Role As String) As Boolean
            Return Me._Roles.Contains(Role)
        End Function

#End Region

#Region " IIdentity "

        Private _IsAuthenticated As Boolean
        Private _ErrorText As String
        Private _UserID As Integer
        Private _FullName As String
        Private _PasswordExpired As Boolean = False
        Private _UserGroup As Integer = 0
        Private _SecurityLevel As Integer = 0
        Private _IntakeFacility As Integer = 0

        Public ReadOnly Property AuthenticationType() As String _
            Implements System.Security.Principal.IIdentity.AuthenticationType
            Get
                Return "ITW"
            End Get
        End Property

        Public ReadOnly Property IsAuthenticated() As Boolean _
            Implements System.Security.Principal.IIdentity.IsAuthenticated
            Get
                Return Me._IsAuthenticated
            End Get
        End Property

        Public ReadOnly Property Name() As String _
            Implements System.Security.Principal.IIdentity.Name
            Get
                Return Me._FullName
            End Get
        End Property

        Public ReadOnly Property UserID() As Integer
            Get
                Return Me._UserID
            End Get
        End Property

        Public ReadOnly Property PasswordExpired() As Boolean
            Get
                Return Me._PasswordExpired
            End Get
        End Property

        Public ReadOnly Property UserGroup() As Integer
            Get
                Return Me._UserGroup
            End Get
        End Property

        Public ReadOnly Property SecurityLevel() As Integer
            Get
                Return Me._SecurityLevel
            End Get
        End Property

        Public ReadOnly Property IntakeFacility() As Integer
            Get
                Return Me._IntakeFacility
            End Get
        End Property

#End Region

#End Region

#Region " Factory Methods "

        Friend Shared Function UnauthenticatedIdentity() As ITWIdentity
            Return New ITWIdentity
        End Function

        Friend Shared Function GetIdentity(ByVal UserName As String, ByVal Password As String) As ITWIdentity
            Dim itwIdentity As ITWIdentity = DataPortal.Fetch(Of ITWIdentity)(New Criteria(UserName, Password))

            If itwIdentity._ErrorText > "" Then
                Throw New LoginException(itwIdentity._ErrorText)
            End If

            Return itwIdentity
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _UserName As String
            Private _Password As String

            Public ReadOnly Property UserName() As String
                Get
                    Return _UserName
                End Get
            End Property

            Public ReadOnly Property Password() As String
                Get
                    Return _Password
                End Get
            End Property

            Public Sub New(ByVal UserName As String, ByVal Password As String)
                _UserName = UserName
                _Password = Password
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal Criteria As Criteria)
            Dim resetCount As Boolean = True

            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()

                Try
                    Using cmd As SqlCommand = conn.CreateCommand
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "User_Retrieve"
                        cmd.Parameters.AddWithValue("@UserName", Criteria.UserName)

                        ' This stored procedure returns the user's entire record
                        ' based on the username alone. It is up to us to validate
                        ' the password, make sure they haven't locked themselves out,
                        ' and make sure they don't need to change their password.
                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            If dr.Read() Then
                                If dr.GetBoolean("inactive") Then
                                    resetCount = False
                                    Throw New LoginException("Your user account is inactive.")
                                End If

                                If dr.GetInt32("logonAttempts") > 4 Then
                                    resetCount = False
                                    Throw New LoginException("Your user account has been locked for security reasons." & _
                                        vbCrLf & vbCrLf & "Please have an administrator reset your password and try again.")
                                End If

                                If dr.GetString("password") <> Criteria.Password Then
                                    resetCount = False
                                    Throw New LoginException("Invalid password.")
                                End If

                                Dim MinimumSecurityLevel As Integer = AppSettings("MinimumSecurityLevel")

                                If dr.GetInt32("UserGroup") <> 1 AndAlso dr.GetInt32("InfoSecurityLevel") < MinimumSecurityLevel Then
                                    resetCount = True
                                    Throw New LoginException("Your account does not have permission to access this site.")
                                End If

                                ' *****************************************************************
                                ' No errors, so load the user
                                Me._UserID = dr.GetInt32("ID")
                                Me._FullName = dr.GetString("FullName")

                                Me._UserGroup = dr.GetInt32("UserGroup")

                                ' 2/2/2015 Matt - Set the user role based on the Group, but remember
                                ' that users who are not in Group 1 are now able to log into the site
                                ' as read-only users. Also note that the menu items are now based on the
                                ' user's [InfoSecuriyLevel].
                                If Me._UserGroup = 1 Then
                                    Me._Roles.Add("Administrator")
                                Else
                                    Me._Roles.Add("ReadOnly")
                                End If

                                Me._SecurityLevel = dr.GetInt32("InfoSecurityLevel")

                                'If dr.GetString("GroupName") = "Scheduling Administrators" Then
                                '    Me._Roles.Add("Administrator")
                                'Else
                                '    Me._Roles.Add("Standard")
                                'End If

                                'If dr.NextResult Then
                                '    While dr.Read
                                '        Me._Roles.Add(dr.GetString(0))
                                '    End While
                                'End If

                                Me._IntakeFacility = dr.GetInt32("intakeFacility")

                                If dr.GetInt32("DaysSincePasswordChanged") > 180 Then
                                    Me._PasswordExpired = True
                                End If

                                Me._IsAuthenticated = True
                            Else
                                Me._UserID = 0
                                Me._FullName = ""
                                Me._IsAuthenticated = False
                            End If
                        End Using
                    End Using
                Catch ex As LoginException
                    Me._ErrorText = ex.Message
                    Me._IsAuthenticated = False
                Finally
                    If resetCount Then
                        Using cmd As SqlCommand = conn.CreateCommand
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.CommandText = "User_LoggedIn"
                            cmd.Parameters.AddWithValue("@UserName", Criteria.UserName)

                            cmd.ExecuteNonQuery()
                        End Using
                    End If
                End Try
            End Using
        End Sub

#End Region

    End Class

End Namespace
