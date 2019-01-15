Imports System.Data.SqlClient

Namespace Evaluations

    <Serializable()> _
    Public Class ReadOnlyEvalSearchResult
        Inherits ReadOnlyBase(Of ReadOnlyEvalSearchResult)

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

        Private Shared ENameProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("EName"))
        Public ReadOnly Property EName() As String
            Get
                Return GetProperty(Of String)(ENameProperty)
            End Get
        End Property

        Private Shared EPathProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("EPath"))
        Public ReadOnly Property EPath() As String
            Get
                Return GetProperty(Of String)(EPathProperty)
            End Get
        End Property

        Private Shared FormProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("Form"))
        Public ReadOnly Property Form() As Boolean
            Get
                Return ReadProperty(Of Boolean)(FormProperty)
            End Get
        End Property

        Private Shared InactiveProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("Inactive"))
        Public ReadOnly Property Inactive() As Boolean
            Get
                Return ReadProperty(Of Boolean)(InactiveProperty)
            End Get
        End Property

        Private Shared Level2Property As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("Level2"))
        Public ReadOnly Property Level2() As Integer
            Get
                Return ReadProperty(Of Integer)(Level2Property)
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

        Private Sub New()
            ' Require use of factory methods
        End Sub

        Friend Sub New(ByRef dr As SafeDataReader)
            With dr
                LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                LoadProperty(Of Integer)(EvalIDProperty, .GetInt32("EvalID"))
                LoadProperty(Of String)(ENameProperty, .GetString("EName"))
                LoadProperty(Of String)(EPathProperty, .GetString("EPath"))
                LoadProperty(Of Boolean)(FormProperty, .GetBoolean("EFinal"))
                LoadProperty(Of Boolean)(InactiveProperty, .GetBoolean("Inactive"))
                LoadProperty(Of Integer)(Level2Property, .GetInt32("Level2"))
            End With
        End Sub

#End Region

    End Class

End Namespace