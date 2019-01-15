Imports System.Data.SqlClient

Namespace Nursing

    Namespace Diagnoses

        <Serializable()> _
        Public Class DiagnosisFormList
            Inherits BusinessListBase(Of DiagnosisFormList, DiagnosisForm)

#Region " Business Methods "

            Public Overloads Sub AddNew()
                Me.Add(DiagnosisForm.NewDiagnosisForm(0))
            End Sub

            Public Overloads Sub AddNew(ByVal FormID As Integer)
                Me.Add(DiagnosisForm.NewDiagnosisForm(FormID))
            End Sub

            Public Overloads Sub Remove(ByVal FormID As Integer)
                For Each form As DiagnosisForm In Me
                    If form.FormID = FormID Then
                        Me.Remove(form)
                    End If
                Next
            End Sub

            Public Function GetChildRuleDescriptions() As String()
                Return DiagnosisForm.NewDiagnosisForm(0).GetRuleDescriptions()
            End Function

#End Region

#Region " Factory Methods "

            Public Shared Function NewDiagnosisFormList() As DiagnosisFormList
                Return DataPortal.CreateChild(Of DiagnosisFormList)()
            End Function

            Public Shared Function GetDiagnosisFormList(ByVal DiagnosisID As Integer) As DiagnosisFormList
                Return DataPortal.FetchChild(Of DiagnosisFormList)(New Criteria(DiagnosisID))
            End Function

            Private Sub New()
                Me.AllowNew = True
            End Sub

#End Region

#Region " Data Access "

            <Serializable()> _
            Private Class Criteria
                Private _DiagnosisID As Integer

                Public ReadOnly Property DiagnosisID() As Integer
                    Get
                        Return Me._DiagnosisID
                    End Get
                End Property

                Public Sub New(ByVal DiagnosisID As Integer)
                    Me._DiagnosisID = DiagnosisID
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
                        sql &= "WHERE b.[NursingDiagID] = " & criteria.DiagnosisID & " "
                        sql &= "ORDER BY crnt.[NName] "

                        cmd.CommandText = sql
                        cmd.CommandType = CommandType.Text

                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            With dr
                                While .Read()
                                    Me.Add(DiagnosisForm.GetDiagnosisForm(dr))
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

End Namespace