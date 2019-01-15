Imports System.Data.SqlClient

Namespace Evaluations

    <Serializable()> _
    Public Class ReadOnlyEvaluationList
        Inherits ReadOnlyListBase(Of ReadOnlyEvaluationList, ReadOnlyEvaluation)

#Region " Factory Methods "

        Public Shared Function GetEvaluationList(ByVal ParentID As Integer, ByVal ActiveOnly As Boolean) As ReadOnlyEvaluationList
            Return DataPortal.Fetch(Of ReadOnlyEvaluationList)(New Criteria(ParentID, ActiveOnly))
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _ParentID As Integer
            Private _ActiveOnly As Boolean

            Public ReadOnly Property ParentID() As Integer
                Get
                    Return Me._ParentID
                End Get
            End Property

            Public ReadOnly Property ActiveOnly() As Boolean
                Get
                    Return Me._ActiveOnly
                End Get
            End Property

            Public Sub New(ByVal ParentID As Integer, ByVal ActiveOnly As Boolean)
                Me._ParentID = ParentID
                Me._ActiveOnly = ActiveOnly
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            RaiseListChangedEvents = False

            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT e.[ID], e.EvalID, e.parentID, e.EName as EvalName, "
                    sql &= "e.DOrder as DisplayOrder, e.Inactive, e.OneTime, e.PostDC "
                    sql &= "FROM [100Eval] e "
                    sql &= "WHERE e.ParentID = " & criteria.ParentID & " "
                    If criteria.ActiveOnly Then
                        sql &= "AND e.Inactive = 0 "
                    End If
                    sql &= "ORDER BY e.DOrder "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        IsReadOnly = False
                        While dr.Read()
                            Me.Add(New ReadOnlyEvaluation(dr))
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