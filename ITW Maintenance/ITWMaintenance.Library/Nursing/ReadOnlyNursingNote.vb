Imports System.Data.SqlClient

Namespace Nursing

    <Serializable()> _
    Public Class ReadOnlyNursingNote
        Inherits ReadOnlyBase(Of ReadOnlyNursingNote)

#Region " Business Methods "

        Private Shared IDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
        Public ReadOnly Property ID() As Integer
            Get
                Return GetProperty(Of Integer)(IDProperty)
            End Get
        End Property

        Private Shared FormIDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("FormID"))
        <System.ComponentModel.DataObjectField(True, True)> _
        Public ReadOnly Property FormID() As Integer
            Get
                Return GetProperty(Of Integer)(FormIDProperty)
            End Get
        End Property

        Private Shared FormNameProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("FormName", "Form Name"))
        Public ReadOnly Property FormName() As String
            Get
                Return GetProperty(Of String)(FormNameProperty)
            End Get
        End Property

        Private Shared IsFormProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("IsForm", "Is Form"))
        Public ReadOnly Property IsForm() As Boolean
            Get
                Return GetProperty(Of Boolean)(IsFormProperty)
            End Get
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return GetProperty(Of Integer)(FormIDProperty)
        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return True
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function GetFormInfo(ByVal FormID As Integer) As ReadOnlyNursingNote
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view an Evaluation")
            End If
            Return DataPortal.Fetch(Of ReadOnlyNursingNote)(New Criteria(FormID))
        End Function

        Private Sub New()
            ' Require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _FormID As Integer

            Public ReadOnly Property FormID() As Integer
                Get
                    Return Me._FormID
                End Get
            End Property

            Public Sub New(ByVal FormID As Integer)
                Me._FormID = FormID
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT [ID], [nsNtID] AS [FormID], [NName] AS [FormName], "
                    sql &= "[nFinal] AS [IsForm] "
                    sql &= "FROM [100CrNt] "
                    sql &= "WHERE [nsNtID] = " & criteria.FormID & " "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        dr.Read()
                        With dr
                            LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                            LoadProperty(Of Integer)(FormIDProperty, .GetInt32("FormID"))
                            LoadProperty(Of String)(FormNameProperty, .GetString("FormName"))
                            LoadProperty(Of Boolean)(IsFormProperty, .GetBoolean("IsForm"))
                        End With
                    End Using
                End Using
            End Using
        End Sub

#End Region

    End Class

End Namespace