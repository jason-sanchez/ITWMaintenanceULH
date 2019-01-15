Imports System.Data.SqlClient

Namespace Evaluations

    <Serializable()> _
    Public Class ReadOnlyEvaluation
        Inherits ReadOnlyBase(Of ReadOnlyEvaluation)

        'Private _ID As Integer
        'Private _EvalID As Integer
        'Private _EvalName As String
        'Private _DisplayOrder As Integer
        'Private _Inactive As Boolean
        'Private _OneTime As Boolean
        'Private _PostDischarge As Boolean

#Region " Business Methods "

        Private Shared IDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
        Public ReadOnly Property ID() As Integer
            Get
                Return GetProperty(Of Integer)(IDProperty)
            End Get
        End Property

        Private Shared EvalIDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("EvalID"))
        <System.ComponentModel.DataObjectField(True, True)> _
        Public ReadOnly Property EvalID() As Integer
            Get
                Return GetProperty(Of Integer)(EvalIDProperty)
            End Get
        End Property

        Private Shared ParentIDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ParentID"))
        Public ReadOnly Property ParentID() As Integer
            Get
                Return GetProperty(Of Integer)(ParentIDProperty)
            End Get
        End Property

        Private Shared EvalNameProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("EvalName", "Eval Name"))
        Public ReadOnly Property EvalName() As String
            Get
                Return GetProperty(Of String)(EvalNameProperty)
            End Get
        End Property

        Private Shared DisplayOrderProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("DisplayOrder", "Display Order"))
        Public ReadOnly Property DisplayOrder() As Integer
            Get
                Return GetProperty(Of Integer)(DisplayOrderProperty)
            End Get
        End Property

        Private Shared InactiveProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("Inactive"))
        Public ReadOnly Property Inactive() As Boolean
            Get
                Return GetProperty(Of Boolean)(InactiveProperty)
            End Get
        End Property

        Private Shared OneTimeProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("OneTime", "One Time"))
        Public ReadOnly Property OneTime() As Boolean
            Get
                Return GetProperty(Of Boolean)(OneTimeProperty)
            End Get
        End Property

        Private Shared PostDischargeProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("PostDischarge", "Post Discharge"))
        Public ReadOnly Property PostDischarge() As Boolean
            Get
                Return GetProperty(Of Boolean)(PostDischargeProperty)
            End Get
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return GetProperty(Of Integer)(EvalIDProperty)
        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return True
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function GetEvalInfo(ByVal EvalID As Integer) As ReadOnlyEvaluation
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view an Evaluation")
            End If
            Return DataPortal.Fetch(Of ReadOnlyEvaluation)(New Criteria(EvalID))
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

        Friend Sub New(ByRef dr As SafeDataReader)
            With dr
                LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                LoadProperty(Of Integer)(EvalIDProperty, .GetInt32("EvalID"))
                LoadProperty(Of Integer)(ParentIDProperty, .GetInt32("ParentID"))
                LoadProperty(Of String)(EvalNameProperty, .GetString("EvalName"))
                LoadProperty(Of Integer)(DisplayOrderProperty, .GetInt32("DisplayOrder"))
                LoadProperty(Of Boolean)(InactiveProperty, .GetBoolean("Inactive"))
                LoadProperty(Of Boolean)(OneTimeProperty, .GetBoolean("OneTime"))
                LoadProperty(Of Boolean)(PostDischargeProperty, .GetBoolean("PostDC"))
            End With
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _EvalID As Integer

            Public ReadOnly Property EvalID() As Integer
                Get
                    Return Me._EvalID
                End Get
            End Property

            Public Sub New(ByVal EvalID As Integer)
                Me._EvalID = EvalID
            End Sub
        End Class

        '<RunLocal()> _
        'Protected Overloads Sub DataPortal_Create(ByVal criteria As Object)
        '    Dim c As Criteria = DirectCast(criteria, Criteria)

        '    If c.EvalID > 0 Then
        '        LoadProperty(Of Integer)(EvalIDProperty, c.EvalID)
        '    End If
        'End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT e.[ID], e.EvalID, e.ParentID, e.EName as EvalName, e.EvalLevel, "
                    sql &= "e.DOrder as DisplayOrder, e.Inactive, e.OneTime, e.PostDC "
                    sql &= "FROM [100Eval] e "
                    sql &= "WHERE e.EvalID = " & criteria.EvalID & " "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        dr.Read()
                        With dr
                            If .GetInt32("EvalLevel") = 2 Then
                                LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                                LoadProperty(Of Integer)(EvalIDProperty, .GetInt32("EvalID"))
                                LoadProperty(Of Integer)(ParentIDProperty, .GetInt32("ParentID"))
                                LoadProperty(Of String)(EvalNameProperty, .GetString("EvalName"))
                                LoadProperty(Of Integer)(DisplayOrderProperty, .GetInt32("DisplayOrder"))
                                LoadProperty(Of Boolean)(InactiveProperty, .GetBoolean("Inactive"))
                                LoadProperty(Of Boolean)(OneTimeProperty, .GetBoolean("OneTime"))
                                LoadProperty(Of Boolean)(PostDischargeProperty, .GetBoolean("PostDC"))
                            Else
                                Throw New Exception("Eval ID #" & criteria.EvalID & " is not an Evaluation")
                            End If
                        End With
                    End Using
                End Using
            End Using
        End Sub

#End Region

    End Class

End Namespace