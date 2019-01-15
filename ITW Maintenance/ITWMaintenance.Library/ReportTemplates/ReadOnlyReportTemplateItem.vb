Imports System.Data.SqlClient

Namespace ReportTemplates

    <Serializable()> _
    Public Class ReadOnlyReportTemplateItem
        Inherits ReadOnlyBase(Of ReadOnlyReportTemplateItem)

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

        Private Shared DisciplineProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Discipline"))
        Public ReadOnly Property Discipline() As String
            Get
                Return GetProperty(Of String)(DisciplineProperty)
            End Get
        End Property

        Protected Function GetIDValue() As Object
            Return GetProperty(Of Integer)(IDProperty)
        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return True
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function GetReportTemplateInfo(ByVal rptTypeId As Integer) As ReadOnlyReportTemplateItem
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view a Report.")
            End If
            Return DataPortal.Fetch(Of ReadOnlyReportTemplateItem)(New Criteria(rptTypeId))
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

        Friend Sub New(ByRef dr As SafeDataReader)
            With dr
                LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                LoadProperty(Of String)(DescriptionProperty, .GetString("Description"))
                LoadProperty(Of String)(DisciplineProperty, .GetString("Discipline"))
            End With
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

                    sql = "SELECT [ID], [Description], [Discipline] "
                    sql &= "FROM [105rptType] "
                    sql &= "WHERE [ID] = " & criteria.ID & " "
                    sql &= "ORDER BY [Description] "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        dr.Read()
                        With dr
                            LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                            LoadProperty(Of String)(DescriptionProperty, .GetString("Description"))
                            LoadProperty(Of String)(DisciplineProperty, .GetString("Discipline"))
                        End With
                    End Using
                End Using
            End Using
        End Sub

#End Region

    End Class

End Namespace