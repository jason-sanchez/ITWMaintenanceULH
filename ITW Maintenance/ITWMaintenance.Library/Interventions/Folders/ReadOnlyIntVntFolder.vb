Imports System.Data.SqlClient

Namespace Interventions

    Namespace Folders

        <Serializable()> _
        Public Class ReadOnlyIntVntFolder
            Inherits ReadOnlyBase(Of ReadOnlyIntVntFolder)

            'Private _ID As Integer
            'Private _intVntID As Integer
            'Private _ParentID As Integer = 0
            'Private _FolderName As String

#Region " Business Methods "

            Private Shared IDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
            Public ReadOnly Property ID() As Integer
                Get
                    Return GetProperty(Of Integer)(IDProperty)
                End Get
            End Property

            Private Shared intVntIDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("intVntID"))
            <System.ComponentModel.DataObjectField(True, True)> _
            Public ReadOnly Property intVntID() As Integer
                Get
                    Return GetProperty(Of Integer)(intVntIDProperty)
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
                Return ReadProperty(Of Integer)(intVntIDProperty)
            End Function

#End Region

#Region " Authorization Rules "

            Public Shared Function CanGetObject() As Boolean
                Return True
            End Function

#End Region

#Region " Factory Methods "

            Public Shared Function GetIntVntFolderInfo(ByVal intVntID As Integer) As ReadOnlyIntVntFolder
                If Not CanGetObject() Then
                    Throw New System.Security.SecurityException("User not authorized to view an Intervention Level 1 Folder")
                End If
                Return DataPortal.Fetch(Of ReadOnlyIntVntFolder)(New Criteria(intVntID))
            End Function

            Private Sub New()
                ' Require use of factory methods
            End Sub

            'Friend Sub New(ByRef dr As SafeDataReader)
            '    With dr
            '        Me._ID = .GetInt32("ID")
            '        Me._intVntID = .GetInt32("intVntID")
            '        Me._ParentID = .GetInt32("ParentID")
            '        Me._FolderName = .GetString("FolderName")
            '    End With
            'End Sub

#End Region

#Region " Data Access "

            <Serializable()> _
            Private Class Criteria
                Private _intVntID As Integer

                Public ReadOnly Property intVntID() As Integer
                    Get
                        Return Me._intVntID
                    End Get
                End Property

                Public Sub New(ByVal intVntID As Integer)
                    Me._intVntID = intVntID
                End Sub
            End Class

            <RunLocal()> _
            Protected Overloads Sub DataPortal_Create(ByVal criteria As Object)
                Dim c As Criteria = DirectCast(criteria, Criteria)

                If c.intVntID > 0 Then
                    LoadProperty(Of Integer)(intVntIDProperty, c.intVntID)
                End If
            End Sub

            Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
                Using conn As New SqlConnection(Database.ITWConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand
                        Dim sql As String

                        sql = "SELECT i.[ID], i.intVntID, i.ParentID, i.iName as FolderName "
                        sql &= "FROM [110IntVnt] i "
                        sql &= "WHERE i.intVntID = " & criteria.intVntID & " "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql

                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            dr.Read()
                            With dr
                                LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                                LoadProperty(Of Integer)(intVntIDProperty, .GetInt32("intVntID"))
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
