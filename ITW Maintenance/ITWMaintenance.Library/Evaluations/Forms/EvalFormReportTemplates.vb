Imports System.Data.SqlClient

Namespace Evaluations

    Namespace Forms

        <Serializable()> _
        Public Class EvalFormReportTemplates
            Inherits NameValueListBase(Of Integer, String)

            Private Shared _EvalFormReportTemplates As EvalFormReportTemplates

#Region " Business Methods "

            Public Overloads Shared Function Contains(ByVal ReportTemplateID As Integer) As Boolean
                For Each type As NameValuePair In GetReportTemplates()
                    If type.Key = ReportTemplateID Then
                        Return True
                    End If
                Next

                Return False
            End Function

            Public Shared Function GetValueByKey(ByVal Key As Integer) As String
                For Each Type As NameValuePair In GetReportTemplates()
                    If Type.Key = Key Then
                        Return Type.Value
                    End If
                Next

                ' Couldn't find the Value because this key wasn't in the list,
                ' so just return the key.
                Return Key
            End Function

            ''' <summary>
            ''' Clears the in-memory list cache
            ''' so the list of roles is reloaded on
            ''' next request.
            ''' </summary>
            Public Shared Sub InvalidateCache()
                _EvalFormReportTemplates = Nothing
            End Sub

#End Region

#Region " Factory Methods "

            Public Shared Function GetReportTemplates() As EvalFormReportTemplates
                If _EvalFormReportTemplates Is Nothing Then
                    _EvalFormReportTemplates = DataPortal.Fetch(Of EvalFormReportTemplates)()
                End If

                Return _EvalFormReportTemplates
            End Function

            Private Sub New()
                ' require use of factory methods
            End Sub

#End Region

#Region " Data Access "

            Private Overloads Sub DataPortal_Fetch()
                Me.RaiseListChangedEvents = False
                IsReadOnly = False
                Me.Add(New NameValuePair(0, ""))
                Using Conn As New SqlConnection(Database.ITWConnection)
                    Conn.Open()
                    Using cmd As SqlCommand = Conn.CreateCommand
                        Dim sql As String

                        sql = "SELECT [rptTypeID] AS [ID], [Description] "
                        sql &= "FROM [105rptType] "
                        sql &= "WHERE [rptCat] = 'report-template' "
                        sql &= "ORDER BY [dOrder] "

                        cmd.CommandText = sql
                        cmd.CommandType = CommandType.Text

                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            With dr
                                While .Read()
                                    Me.Add(New NameValuePair(dr("ID"), dr("Description")))
                                End While
                            End With
                        End Using
                    End Using
                End Using
                IsReadOnly = True
                Me.RaiseListChangedEvents = True
            End Sub

#End Region

        End Class

    End Namespace

End Namespace