Imports System.Data.SqlClient

Namespace Orders

    <Serializable()> _
    Public Class ReadOnlyOrderSearchResult
        Inherits ReadOnlyBase(Of ReadOnlyOrderSearchResult)

#Region " Business Methods "

        Private Shared IDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
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

        Private Shared PathProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Path"))
        Public ReadOnly Property Path() As String
            Get
                Return GetProperty(Of String)(PathProperty)
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
            Return ReadProperty(Of Integer)(IDProperty)
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
                LoadProperty(Of String)(NameProperty, .GetString("Name"))
                LoadProperty(Of String)(PathProperty, .GetString("Path"))
                LoadProperty(Of Boolean)(IsFormProperty, .GetBoolean("Form"))
                LoadProperty(Of Boolean)(InactiveProperty, .GetBoolean("Inactive"))
            End With
        End Sub

#End Region

    End Class

End Namespace