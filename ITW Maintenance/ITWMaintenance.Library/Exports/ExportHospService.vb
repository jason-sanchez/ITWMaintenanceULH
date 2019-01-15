Imports System.Data.SqlClient
Namespace Exports
    <Serializable()> _
    Public Class ExportHospService
        Inherits ReadOnlyListBase(Of ExportHospService, ReadonlyHospService)


#Region "Authorization Rules"

        Public Shared Function CanGetObject() As Boolean
            Return True
        End Function

#End Region

#Region "Factory Methods"

        Public Shared Function GetHospSvc(ByVal dept As String) As ExportHospService
            Return DataPortal.Fetch(Of ExportHospService)(New Criteria(dept))
        End Function
#End Region

#Region "Data Access"

        <Serializable()> _
        Private Class Criteria

            Private _dept As String
            Public ReadOnly Property dept() As String
                Get
                    Return Me._dept
                End Get
            End Property

            Public Sub New(ByVal dept As String)
                Me._dept = dept
            End Sub

        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT DISTINCT [HospSvc] "
                    sql &= "FROM [130HospSvc] "
                    sql &= "WHERE [department] = '" & criteria.dept.ToString() & "' "
                    sql &= "Order by [HospSvc] "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using datareader As New SafeDataReader(cmd.ExecuteReader)
                        IsReadOnly = False
                        While datareader.Read()
                            Me.Add(New ReadonlyHospService(datareader))
                        End While
                        IsReadOnly = True
                    End Using
                End Using
            End Using


        End Sub




#End Region



    End Class
End Namespace

