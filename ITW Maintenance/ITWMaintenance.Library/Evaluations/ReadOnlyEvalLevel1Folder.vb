Imports System.Data.SqlClient

Namespace Evaluations

    <Serializable()> _
    Public Class ReadOnlyEvalLevel1Folder
        Inherits ReadOnlyBase(Of ReadOnlyEvalLevel1Folder)

        'Private _ID As Integer
        'Private _EvalID As Integer
        'Private _FolderName As String
        'Private _Discipline As String
        'Private _DisplayOrder As Integer
        'Private _Inactive As Boolean

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

        Private Shared FolderNameProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("FolderName", "Folder Name"))
        Public ReadOnly Property FolderName() As String
            Get
                Return GetProperty(Of String)(FolderNameProperty)
            End Get
        End Property

        Private Shared DisciplineProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Discipline"))
        Public ReadOnly Property Discipline() As String
            Get
                Return GetProperty(Of String)(DisciplineProperty)
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

        Protected Overrides Function GetIdValue() As Object
            Return ReadProperty(Of Integer)(EvalIDProperty)
        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return True
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function GetEvalLevel1FolderInfo(ByVal EvalID As Integer) As ReadOnlyEvalLevel1Folder
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view an Eval Level 1 Folder")
            End If
            Return DataPortal.Fetch(Of ReadOnlyEvalLevel1Folder)(New Criteria(EvalID))
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

        Friend Sub New(ByRef dr As SafeDataReader)
            With dr
                LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                LoadProperty(Of Integer)(EvalIDProperty, .GetInt32("EvalID"))
                LoadProperty(Of String)(FolderNameProperty, .GetString("FolderName"))
                LoadProperty(Of String)(DisciplineProperty, .GetString("Discipline"))
                LoadProperty(Of Integer)(DisplayOrderProperty, .GetInt32("DisplayOrder"))
                LoadProperty(Of Boolean)(InactiveProperty, .GetBoolean("Inactive"))
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

        <RunLocal()> _
        Protected Overloads Sub DataPortal_Create(ByVal criteria As Object)
            Dim c As Criteria = DirectCast(criteria, Criteria)

            If c.EvalID > 0 Then
                LoadProperty(Of Integer)(EvalIDProperty, c.EvalID)
            End If
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT e.[ID], e.EvalID, e.EName as FolderName, e.EvalLevel, "
                    sql &= "d.disName as Discipline, e.DOrder as DisplayOrder, e.Inactive "
                    sql &= "FROM [100Eval] e "
                    sql &= "INNER JOIN [116Discipline] d on e.discipline = d.disID "
                    sql &= "WHERE e.EvalID = " & criteria.EvalID & " "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        If dr.Read() Then
                            With dr
                                If .GetInt32("EvalLevel") = 1 Then
                                    LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                                    LoadProperty(Of Integer)(EvalIDProperty, .GetInt32("EvalID"))
                                    LoadProperty(Of String)(FolderNameProperty, .GetString("FolderName"))
                                    LoadProperty(Of String)(DisciplineProperty, .GetString("Discipline"))
                                    LoadProperty(Of Integer)(DisplayOrderProperty, .GetInt32("DisplayOrder"))
                                    LoadProperty(Of Boolean)(InactiveProperty, .GetBoolean("Inactive"))
                                Else
                                    Throw New Exception("Eval ID #" & criteria.EvalID & " is not a Level 1 Eval Folder")
                                End If
                            End With
                        End If
                    End Using
                End Using
            End Using
        End Sub

#End Region

    End Class

End Namespace