Imports System.Data.SqlClient

Namespace Orders

    <Serializable()> _
    Public Class ReadOnlyOrderFormField
        Inherits ReadOnlyBase(Of ReadOnlyOrderFormField)

#Region " Business Methods "

        Private Shared IDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
        <System.ComponentModel.DataObjectField(True, True)> _
        Public ReadOnly Property ID() As Integer
            Get
                Return GetProperty(Of Integer)(IDProperty)
            End Get
        End Property

        Private Shared LabelProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Label", "Field Label"))
        Public ReadOnly Property Label() As String
            Get
                Return GetProperty(Of String)(LabelProperty)
            End Get
        End Property

        Private Shared TypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Type"))
        Public ReadOnly Property Type() As String
            Get
                Return GetProperty(Of String)(TypeProperty)
            End Get
        End Property

        Private Shared OptionsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Options"))
        Public ReadOnly Property Options() As String
            Get
                Return GetProperty(Of String)(OptionsProperty)
            End Get
        End Property

        Private Shared HelpTextProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("HelpText"))
        Public ReadOnly Property HelpText() As String
            Get
                Return GetProperty(Of String)(HelpTextProperty)
            End Get
        End Property

        Private Shared DisplayOrderProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("DisplayOrder", "Display Order"))
        Public ReadOnly Property DisplayOrder() As Integer
            Get
                Return GetProperty(Of Integer)(DisplayOrderProperty)
            End Get
        End Property

        Private Shared DefaultValueProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("DefaultValue", "Default Value"))
        Public ReadOnly Property DefaultValue() As String
            Get
                Return GetProperty(Of String)(DefaultValueProperty)
            End Get
        End Property

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return True
        End Function

#End Region

#Region " Factory Methods "

        Private Sub New()
            ' Require use of Factory methods
        End Sub

        Friend Shared Function GetOrderFormFieldInfo(ByVal dr As SafeDataReader) As ReadOnlyOrderFormField
            Return DataPortal.FetchChild(Of ReadOnlyOrderFormField)(dr)
        End Function

#End Region

#Region " Data Access "

        Private Sub Child_Fetch(ByVal dr As Csla.Data.SafeDataReader)
            With dr
                LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                LoadProperty(Of String)(LabelProperty, .GetString("Label"))
                ' TODO - Code the Evals this way
                LoadProperty(Of String)(TypeProperty, OrderFormFieldTypes.GetValueByKey(.GetString("Type")))
                LoadProperty(Of String)(OptionsProperty, .GetString("Options"))
                LoadProperty(Of String)(HelpTextProperty, .GetString("HelpText"))
                LoadProperty(Of Integer)(DisplayOrderProperty, .GetInt32("DisplayOrder"))
                LoadProperty(Of String)(DefaultValueProperty, .GetString("DefaultValue"))
            End With
        End Sub

#End Region

    End Class

End Namespace