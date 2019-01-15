Imports System.Data.SqlClient

Namespace Interventions

    <Serializable()> _
    Public Class ReadOnlyIntVntSearchResult
        Inherits ReadOnlyBase(Of ReadOnlyIntVntSearchResult)

#Region " Business Methods "

        Private Shared IDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
        Public ReadOnly Property ID() As Integer
            Get
                Return GetProperty(Of Integer)(IDProperty)
            End Get
        End Property

        Private Shared IntVntIDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("intVntID"))
        <System.ComponentModel.DataObjectField(True, True)> _
        Public ReadOnly Property intVntID() As Integer
            Get
                Return GetProperty(Of Integer)(intVntIDProperty)
            End Get
        End Property

        Private Shared iNameProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("iName"))
        Public ReadOnly Property iName() As String
            Get
                Return GetProperty(Of String)(iNameProperty)
            End Get
        End Property

        Private Shared iPathProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("iPath"))
        Public ReadOnly Property iPath() As String
            Get
                Return GetProperty(Of String)(iPathProperty)
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
            Return ReadProperty(Of Integer)(intVntIDProperty)
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
                LoadProperty(Of Integer)(intVntIDProperty, .GetInt32("intVntID"))
                LoadProperty(Of String)(iNameProperty, .GetString("iName"))
                LoadProperty(Of String)(iPathProperty, .GetString("iPath"))
                LoadProperty(Of Boolean)(FormProperty, .GetBoolean("iFinal"))
                LoadProperty(Of Boolean)(InactiveProperty, .GetBoolean("Inactive"))
                LoadProperty(Of Integer)(Level2Property, .GetInt32("Level2"))
            End With
        End Sub

#End Region

    End Class

End Namespace