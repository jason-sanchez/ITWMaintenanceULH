Imports System.Data.SqlClient

Namespace Lookup

    <Serializable()> _
    Public Class ReadOnlyUserGroup
        Inherits ReadOnlyBase(Of ReadOnlyUserGroup)

#Region " Business Methods "

        Private Shared IDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
        Public ReadOnly Property ID() As Integer
            Get
                Return GetProperty(Of Integer)(IDProperty)
            End Get
        End Property

        Private Shared DescriptionProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Description"))
        Public ReadOnly Property Description() As String
            Get
                Return GetProperty(Of String)(DescriptionProperty)
            End Get
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return GetProperty(Of Integer)(IDProperty)
        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return True
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function GetUserGroupInfo(ByVal GroupID As Integer) As ReadOnlyUserGroup
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view a User Group")
            End If
            Return DataPortal.Fetch(Of ReadOnlyUserGroup)(New Criteria(GroupID))
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

        Friend Sub New(ByRef dr As SafeDataReader)
            With dr
                LoadProperty(Of Integer)(IDProperty, .GetInt32("GroupID"))
                LoadProperty(Of String)(DescriptionProperty, .GetString("Description"))
            End With
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _GroupID As Integer

            Public ReadOnly Property GroupID() As Integer
                Get
                    Return Me._GroupID
                End Get
            End Property

            Public Sub New(ByVal GroupID As Integer)
                Me._GroupID = GroupID
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT [GroupID], [Description] "
                    sql &= "FROM [200UGroup] "
                    sql &= "WHERE [GroupID] = " & criteria.GroupID & " "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        dr.Read()
                        With dr
                            LoadProperty(Of Integer)(IDProperty, .GetInt32("GroupID"))
                            LoadProperty(Of String)(DescriptionProperty, .GetString("Description"))
                        End With
                    End Using
                End Using
            End Using
        End Sub

#End Region

    End Class

End Namespace