Imports System.Data.SqlClient

Namespace Exports
    <Serializable()> _
    Public Class ExportDiscipline
        Inherits ReadOnlyListBase(Of ExportDiscipline, ReadonlyDiscipline)

#Region "Authorization Rules"

        Public Shared Function CanGetObject() As Boolean
            Return True
        End Function

#End Region

#Region "Factory Methods"

        Public Shared Function GetDiscipline() As ExportDiscipline
            Return DataPortal.Fetch(Of ExportDiscipline)()
        End Function

        Private Overloads Sub DataPortal_Fetch()

            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT DISTINCT [DisName] "
                    sql &= "FROM [116Discipline] "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using datareader As New SafeDataReader(cmd.ExecuteReader)
                        IsReadOnly = False
                        While datareader.Read()
                            Me.Add(New ReadonlyDiscipline(datareader))
                        End While
                        IsReadOnly = True
                    End Using
                End Using
            End Using


        End Sub


#End Region





    End Class
End Namespace

