Imports System.Data.SqlClient
Namespace Exports
    <Serializable()> _
    Public Class ExportUser
        Inherits ReadOnlyBase(Of ExportUser)

#Region " Business Methods"

        Private Shared InfosecProp As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("infoSecurityLevel"))
        Public ReadOnly Property Infosecurity() As Integer
            Get
                Return GetProperty(Of Integer)(InfosecProp)
            End Get
        End Property

        Private Shared DeptProp As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Department"))
        Public ReadOnly Property Dept() As String
            Get
                Return GetProperty(Of String)(DeptProp)
            End Get
        End Property

        Private Shared IntakefacilityProp As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("Intakefacility"))
        Public ReadOnly Property Intakefacility() As Integer
            Get
                Return GetProperty(Of Integer)(IntakefacilityProp)
            End Get
        End Property

#End Region

#Region "Factory Methods"

        Public Shared Function GetUserSecurity(ByVal userid As Integer) As ExportUser
            Return DataPortal.Fetch(Of ExportUser)(New Criteria(userid))
        End Function


#End Region

#Region "Data Access"

        <Serializable()> _
        Private Class Criteria

            Private _userid As String
            Public ReadOnly Property userid() As Integer
                Get
                    Return Me._userid
                End Get
            End Property

            Public Sub New(ByVal userid As Integer)
                Me._userid = userid
            End Sub
        End Class


        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)          
            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    'sql = "SELECT [department], [infoSecurityLevel] "
                    'sql &= "FROM [200User] "
                    'sql &= "WHERE [id] = '" & criteria.userid.ToString() & "'"

                    sql = " SELECT u.[department], [infoSecurityLevel], d.[intakefacility] "
                    sql &= " FROM [200User] U "
                    sql &= " INNER JOIN [130Department] D on u.department = d.department "
                    sql &= " WHERE u.[id] = '" & criteria.userid.ToString() & "' "
                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using datareader As New SafeDataReader(cmd.ExecuteReader)
                        datareader.Read()
                        With datareader
                            LoadProperty(Of Integer)(InfosecProp, .GetInt32("infoSecurityLevel"))
                            LoadProperty(Of String)(DeptProp, .GetString("department"))
                            LoadProperty(Of Integer)(IntakefacilityProp, .GetInt32("intakefacility"))
                        End With
                    End Using
                End Using
            End Using
        End Sub

#End Region


    End Class

End Namespace

