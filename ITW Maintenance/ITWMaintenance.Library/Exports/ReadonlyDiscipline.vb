Imports System.Data.SqlClient
Namespace Exports
    <Serializable()> _
    Public Class ReadonlyDiscipline
        Inherits ReadOnlyBase(Of ReadonlyDiscipline)

#Region "Business Methods"
        Private Shared DisIDProp As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("DisID"))
        Public ReadOnly Property DisID() As Integer
            Get
                Return GetProperty(Of Integer)(DisIDProp)
            End Get
        End Property

        Private Shared DisNameProp As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("DisName"))
        Public ReadOnly Property DisName() As String
            Get
                Return GetProperty(Of String)(DisNameProp)
            End Get
        End Property

#End Region

#Region "Authorization Rules"

        Public Shared Function CanGetObject() As Boolean
            Return True
        End Function

#End Region

#Region "Factory Methods"

        Friend Sub New(ByRef dr As SafeDataReader)
            With dr
                LoadProperty(Of String)(DisNameProp, .GetString("DisName"))
            End With
        End Sub


#End Region


    End Class
End Namespace

