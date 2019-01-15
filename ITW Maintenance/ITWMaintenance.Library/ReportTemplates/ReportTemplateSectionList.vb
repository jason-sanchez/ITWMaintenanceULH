Imports System.Data.SqlClient

Namespace ReportTemplates

    <Serializable()> _
    Public Class ReportTemplateSectionList
        Inherits BusinessListBase(Of ReportTemplateSectionList, ReportTemplateSectionItem)

#Region " Business Methods "

        Public Overloads Sub AddNew()
            Me.Add(ReportTemplateSectionItem.NewDiagnosisForm(0))
        End Sub

        Public Overloads Sub AddNew(ByVal FormID As Integer)
            Me.Add(ReportTemplateSectionItem.NewDiagnosisForm(FormID))
        End Sub

        Public Overloads Sub Remove(ByVal FormID As Integer)
            For Each form As ReportTemplateSectionItem In Me
                If form.FormID = FormID Then
                    Me.Remove(form)
                End If
            Next
        End Sub

        Public Function GetChildRuleDescriptions() As String()
            Return ReportTemplateSectionItem.NewDiagnosisForm(0).GetRuleDescriptions()
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewReportTemplateSectionList() As ReportTemplateSectionList
            Return DataPortal.CreateChild(Of ReportTemplateSectionList)()
        End Function

        Public Shared Function GetReportTemplateSectionList(ByVal DiagnosisID As Integer) As ReportTemplateSectionList
            Return DataPortal.FetchChild(Of ReportTemplateSectionList)(New Criteria(DiagnosisID))
        End Function

        Private Sub New()
            Me.AllowNew = True
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _ID As Integer

            Public ReadOnly Property ID() As Integer
                Get
                    Return Me._ID
                End Get
            End Property

            Public Sub New(ByVal ID As Integer)
                Me._ID = ID
            End Sub
        End Class

        Private Sub Child_Fetch(ByVal criteria As Criteria)
            Me.RaiseListChangedEvents = False
            Using Conn As New SqlConnection(Database.ITWConnection)
                Conn.Open()
                Using cmd As SqlCommand = Conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT b.[Form] AS [FormID], crnt.[NName] AS [FormName] "
                    sql &= "FROM [120NDBridge] b "
                    sql &= "INNER JOIN [100CrNt] crnt ON b.[Form] = crnt.[nsNtID] "
                    sql &= "WHERE b.[NursingDiagID] = " & criteria.ID & " "
                    sql &= "ORDER BY crnt.[NName] "

                    cmd.CommandText = sql
                    cmd.CommandType = CommandType.Text

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        With dr
                            While .Read()
                                Me.Add(ReportTemplateSectionItem.GetReportTemplateSectionItem(dr))
                            End While
                        End With
                    End Using
                End Using
            End Using
            Me.RaiseListChangedEvents = True
        End Sub

#End Region

    End Class

End Namespace