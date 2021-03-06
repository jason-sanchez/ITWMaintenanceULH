Imports System.Data.SqlClient

Namespace Interventions

    <Serializable()> _
    Public Class ReadOnlyIntVntLevel1Folder
        Inherits ReadOnlyBase(Of ReadOnlyIntVntLevel1Folder)

        'Private _ID As Integer
        'Private _intVntID As Integer
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

        Private Shared intVntIDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("intVntID"))
        <System.ComponentModel.DataObjectField(True, True)> _
        Public ReadOnly Property intVntID() As Integer
            Get
                Return GetProperty(Of Integer)(intVntIDProperty)
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
            Return ReadProperty(Of Integer)(intVntIDProperty)
        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return True
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function GetIntVntLevel1FolderInfo(ByVal intVntID As Integer) As ReadOnlyIntVntLevel1Folder
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view an Intervention Level 1 Folder")
            End If
            Return DataPortal.Fetch(Of ReadOnlyIntVntLevel1Folder)(New Criteria(intVntID))
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

        Friend Sub New(ByRef dr As SafeDataReader)
            With dr
                LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                LoadProperty(Of Integer)(intVntIDProperty, .GetInt32("intVntID"))
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

                    sql = "SELECT i.[ID], i.intVntID, i.iName as FolderName, i.intVntLevel, "
                    sql &= "d.disName as Discipline, i.DOrder as DisplayOrder, i.Inactive "
                    sql &= "FROM [110intVnt] i "
                    sql &= "INNER JOIN [116Discipline] d on i.discipline = d.disID "
                    sql &= "WHERE i.intVntID = " & criteria.intVntID & " "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        If dr.Read() Then
                            With dr
                                If .GetInt32("intVntLevel") = 1 Then
                                    LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                                    LoadProperty(Of Integer)(intVntIDProperty, .GetInt32("intVntID"))
                                    LoadProperty(Of String)(FolderNameProperty, .GetString("FolderName"))
                                    LoadProperty(Of String)(DisciplineProperty, .GetString("Discipline"))
                                    LoadProperty(Of Integer)(DisplayOrderProperty, .GetInt32("DisplayOrder"))
                                    LoadProperty(Of Boolean)(InactiveProperty, .GetBoolean("Inactive"))
                                Else
                                    Throw New Exception("Interventions ID #" & criteria.intVntID & " is not a Level 1 Interventions Folder")
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