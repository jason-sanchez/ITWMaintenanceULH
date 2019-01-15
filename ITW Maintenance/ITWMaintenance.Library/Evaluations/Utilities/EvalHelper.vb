Namespace Evaluations

    Namespace Utilities

        Public Class EvalHelper

            Public Shared Function GetNextEvalID(ByRef cmd As SqlClient.SqlCommand) As Integer
                Dim sql As String
                Dim nextEvalID As Integer = 0

                ' Get the next EvalID
                sql = "SELECT MAX(EvalID) + 1 AS NextEvalID "
                sql &= "FROM [100Eval] "

                cmd.CommandType = CommandType.Text
                cmd.CommandText = sql
                Using dr As New SafeDataReader(cmd.ExecuteReader)
                    If dr.Read() Then
                        nextEvalID = dr.GetInt32("NextEvalID")
                    End If
                End Using

                Return nextEvalID
            End Function

        End Class

    End Namespace

End Namespace