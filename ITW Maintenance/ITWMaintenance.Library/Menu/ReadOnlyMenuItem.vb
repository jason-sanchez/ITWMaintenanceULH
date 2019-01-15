Namespace Menu

    <Serializable()> _
    Public Class ReadOnlyMenuItem
        Inherits ReadOnlyBase(Of ReadOnlyMenuItem)

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

        Private Shared LinkProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Link"))
        Public ReadOnly Property Link() As String
            Get
                Return GetProperty(Of String)(LinkProperty)
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

        Private Sub New()
            ' Require use of factory methods
        End Sub

        Friend Sub New(ByRef dr As SafeDataReader)
            With dr
                LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                LoadProperty(Of String)(DescriptionProperty, .GetString("Description"))
                LoadProperty(Of String)(LinkProperty, .GetString("Link"))
            End With
        End Sub

#End Region

    End Class

End Namespace