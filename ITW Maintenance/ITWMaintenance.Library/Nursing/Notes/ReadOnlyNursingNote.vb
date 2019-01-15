Imports System.Data.SqlClient

Namespace Nursing

    Namespace Notes

        <Serializable()> _
        Public Class ReadOnlyNursingNote
            Inherits ReadOnlyBase(Of ReadOnlyNursingNote)

#Region " Business Methods "

            Private Shared IDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
            <System.ComponentModel.DataObjectField(True, True)> _
            Public ReadOnly Property ID() As Integer
                Get
                    Return GetProperty(Of Integer)(IDProperty)
                End Get
            End Property

            Private Shared NameProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Name"))
            Public ReadOnly Property Name() As String
                Get
                    Return GetProperty(Of String)(NameProperty)
                End Get
            End Property

            Private Shared DisciplineProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("Discipline"))
            Public ReadOnly Property Discipline() As Integer
                Get
                    Return GetProperty(Of Integer)(DisciplineProperty)
                End Get
            End Property

            Private Shared IsFormProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("IsForm", "Is Form"))
            Public ReadOnly Property IsForm() As Boolean
                Get
                    Return GetProperty(Of Boolean)(IsFormProperty)
                End Get
            End Property

            Private Shared InactiveProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("Inactive"))
            Public ReadOnly Property Inactive() As Boolean
                Get
                    Return GetProperty(Of Boolean)(InactiveProperty)
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

            Public Shared Function GetInfo(ByVal ID As Integer) As ReadOnlyNursingNote
                If Not CanGetObject() Then
                    Throw New System.Security.SecurityException("User not authorized to view a Nursing Note")
                End If
                Return DataPortal.Fetch(Of ReadOnlyNursingNote)(New Criteria(ID))
            End Function

            Private Sub New()
                ' Require use of factory methods
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

            Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
                Using conn As New SqlConnection(Database.ITWConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand
                        Dim sql As String

                        sql = "SELECT [nsNtID] AS [ID], [NName] AS [Name], "
                        sql &= "[discipline], [nFinal] AS [IsForm], [nInactive] AS [Inactive] "
                        sql &= "FROM [100CrNt] "
                        sql &= "WHERE [nsNtID] = " & criteria.ID & " "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql

                        Using dr As New SafeDataReader(cmd.ExecuteReader)
                            dr.Read()
                            With dr
                                LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                                LoadProperty(Of String)(NameProperty, .GetString("Name"))
                                LoadProperty(Of Integer)(DisciplineProperty, .GetInt32("Discipline"))
                                LoadProperty(Of Boolean)(IsFormProperty, .GetBoolean("IsForm"))
                                LoadProperty(Of Boolean)(InactiveProperty, .GetBoolean("Inactive"))
                            End With
                        End Using
                    End Using
                End Using
            End Sub

#End Region

        End Class

    End Namespace

End Namespace