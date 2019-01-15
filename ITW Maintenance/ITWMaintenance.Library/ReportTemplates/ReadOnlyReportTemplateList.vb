Imports System.Data.SqlClient

Namespace ReportTemplates

    <Serializable()> _
    Public Class ReadOnlyReportTemplateList
        Inherits ReadOnlyListBase(Of ReadOnlyReportTemplateList, ReadOnlyReportTemplateItem)

#Region " Factory Methods "

        Public Shared Function GetReportTemplateList(ByVal DisciplineID As Integer) As ReadOnlyReportTemplateList
            Return DataPortal.Fetch(Of ReadOnlyReportTemplateList)(New SingleCriteria(Of ReadOnlyReportTemplateList, Integer)(DisciplineID))
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _ActiveOnly As Boolean

            Public ReadOnly Property ActiveOnly() As Boolean
                Get
                    Return Me._ActiveOnly
                End Get
            End Property

            Public Sub New(ByVal ActiveOnly As Boolean)
                Me._ActiveOnly = ActiveOnly
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As SingleCriteria(Of ReadOnlyReportTemplateList, Integer))

            RaiseListChangedEvents = False

            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand

                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "[documents].[GetReportTemplateList]"

                    cmd.Parameters.AddWithValue("@disciplineID", criteria.Value)

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        IsReadOnly = False
                        While dr.Read()
                            Me.Add(New ReadOnlyReportTemplateItem(dr))
                        End While
                        IsReadOnly = True
                    End Using
                End Using
            End Using

            RaiseListChangedEvents = True
        End Sub

#End Region

    End Class

End Namespace