Imports System.Data.SqlClient

Namespace Nursing

    <Serializable()> _
    Public Class ReadOnlyNursingNoteSearchResult
        Inherits ReadOnlyBase(Of ReadOnlyNursingNoteSearchResult)

#Region " Business Methods "

        Private Shared IDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
        Public ReadOnly Property ID() As Integer
            Get
                Return GetProperty(Of Integer)(IDProperty)
            End Get
        End Property

        Private Shared FormIDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("FormID", "Form ID"))
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

        Private Shared InactiveProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("Inactive"))
        Public ReadOnly Property Inactive() As Boolean
            Get
                Return ReadProperty(Of Boolean)(InactiveProperty)
            End Get
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return ReadProperty(Of Integer)(FormIDProperty)
        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return True
        End Function

#End Region

#Region " Factory Methods "

        Private Sub New()
            ' Require use of factory methods
        End Sub

        Friend Sub New(ByRef dr As SafeDataReader)
            With dr
                LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                LoadProperty(Of Integer)(FormIDProperty, .GetInt32("FormID"))
                LoadProperty(Of String)(FormNameProperty, .GetString("FormName"))
                LoadProperty(Of Boolean)(InactiveProperty, .GetBoolean("Inactive"))
            End With
        End Sub

#End Region

    End Class

End Namespace