Namespace Interventions

    Namespace Utilities

        Public Class IntVntHelper

            Public Shared Function GetNextintVntID(ByRef cmd As SqlClient.SqlCommand) As Integer
                Dim sql As String
                Dim nextintVntID As Integer = 0

                ' Get the next intVntID
                sql = "SELECT MAX(intVntID) + 1 AS NextintVntID "
                sql &= "FROM [110IntVnt] "

                cmd.CommandType = CommandType.Text
                cmd.CommandText = sql
                Using dr As New SafeDataReader(cmd.ExecuteReader)
                    If dr.Read() Then
                        nextintVntID = dr.GetInt32("NextintVntID")
                    End If
                End Using

                Return nextintVntID
            End Function

        End Class

    End Namespace

End Namespace