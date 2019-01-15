Imports System.Data.SqlClient

Namespace Security

    <Serializable()> _
    Public Class PasswordReset
        Inherits CommandBase

        Public Shared Sub ResetPassword(ByVal UserID As Integer)
            Dim cmd As New PasswordReset(UserID)
            cmd = DataPortal.Execute(Of PasswordReset)(cmd)
        End Sub

        Private _UserID As Integer

        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal UserID As Integer)
            Me._UserID = UserID
        End Sub

        Protected Overrides Sub DataPortal_Execute()
            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    ' Reset the password...
                    sql = "UPDATE [200User] SET "
                    sql &= "[Password] = 'password', "
                    sql &= "[PswdChgDate] = '1/1/1900', "
                    sql &= "[LogonAttempts] = 0 "
                    sql &= "WHERE [ID] = " & Me._UserID.ToString() & " "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql
                    cmd.ExecuteNonQuery()
                End Using
                Using Logcmd As SqlCommand = conn.CreateCommand
                    Dim Sql As String
                    Dim logDescription As String

                    logDescription = "Password Reset for User ID: '" & Me._UserID.ToString() & "'."

                    Sql = "insert into [060transLog] (logDate, logType, userID, "
                    Sql &= "logDescription) values ("
                    Sql &= "GetDate(), "
                    Sql &= "'Password Reset', "
                    Sql &= Me._UserID.ToString() & ", "
                    Sql &= "'" & Replace(logDescription, "'", "''") & "') "

                    Logcmd.CommandType = CommandType.Text
                    Logcmd.CommandText = Sql
                    Logcmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

    End Class

End Namespace