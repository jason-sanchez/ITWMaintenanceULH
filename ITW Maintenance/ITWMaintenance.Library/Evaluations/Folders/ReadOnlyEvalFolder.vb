Imports System.Data.SqlClient

Namespace Evaluations

    Namespace Folders

        <Serializable()> _
        Public Class ReadOnlyEvalFolder
            Inherits ReadOnlyBase(Of ReadOnlyEvalFolder)

            'Private _ID As Integer
            'Private _EvalID As Integer
            'Private _ParentID As Integer = 0
            'Private _FolderName As String

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

            Private Shared FolderNameProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("FolderName", "Folder Name"))
            Public ReadOnly Property FolderName() As String
                Get
                    Return GetProperty(Of String)(FolderNameProperty)
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

            Public Shared Function GetEvalFolderInfo(ByVal EvalID As Integer) As ReadOnlyEvalFolder
                If Not CanGetObject() Then
                    Throw New System.Security.SecurityException("User not authorized to view an Eval Level 1 Folder")
                End If
                Return DataPortal.Fetch(Of ReadOnlyEvalFolder)(New Criteria(EvalID))
            End Function

            Private Sub New()
                ' Require use of factory methods
            End Sub

            'Friend Sub New(ByRef dr As SafeDataReader)
            '    With dr
            '        Me._ID = .GetInt32("ID")
            '        Me._EvalID = .GetInt32("EvalID")
            '        Me._ParentID = .GetInt32("ParentID")
            '        Me._FolderName = .GetString("FolderName")
            '    End With
            'End Sub

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

                        sql = "SELECT e.[ID], e.EvalID, e.ParentID, e.EName as FolderName "
                        sql &= "FROM [100Eval] e "
                        sql &= "WHERE e.EvalID = " & criteria.EvalID & " "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql

                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            dr.Read()
                            With dr
                                LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                                LoadProperty(Of Integer)(EvalIDProperty, .GetInt32("EvalID"))
                                LoadProperty(Of Integer)(ParentIDProperty, .GetInt32("ParentID"))
                                LoadProperty(Of String)(FolderNameProperty, .GetString("FolderName"))
                            End With
                        End Using
                    End Using
                End Using
            End Sub

#End Region

        End Class

    End Namespace

End Namespace
