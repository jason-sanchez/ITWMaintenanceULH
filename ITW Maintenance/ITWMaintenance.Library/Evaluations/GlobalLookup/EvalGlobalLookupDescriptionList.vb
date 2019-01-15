Imports System.Data.SqlClient

Namespace Evaluations

    Namespace GlobalLookup

        <Serializable()> _
        Public Class EvalGlobalLookupDescriptionList
            Inherits NameValueListBase(Of String, String)

            Private Shared _LookupDescriptions As EvalGlobalLookupDescriptionList

#Region " Factory Methods "

            Public Shared Function GetDescriptionList() As EvalGlobalLookupDescriptionList
                Return DataPortal.Fetch(Of EvalGlobalLookupDescriptionList)()
            End Function


            Private Sub New()
                ' require use of factory methods
            End Sub

#End Region

#Region " Data Access "

            Private Overloads Sub DataPortal_Fetch()
                Me.RaiseListChangedEvents = False

                Using conn As New SqlConnection(Database.ITWConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand
                        Dim sql As String

                        sql = "SELECT [Description] "
                        sql &= "FROM [100GlobalLookup] "
                        sql &= "WHERE [Type] = 'eval' "
                        sql &= "GROUP BY [Description] "
                        sql &= "ORDER BY [Description] "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql

                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            IsReadOnly = False
                            With dr
                                While .Read()
                                    Me.Add(New NameValuePair(.GetString("Description"), .GetString("Description")))
                                End While
                            End With
                            IsReadOnly = True
                        End Using
                    End Using
                End Using

                Me.RaiseListChangedEvents = True
            End Sub

#End Region

        End Class

    End Namespace

End Namespace
