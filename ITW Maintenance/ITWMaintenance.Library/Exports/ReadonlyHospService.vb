Imports System.Data.SqlClient
Namespace Exports
    <Serializable()> _
    Public Class ReadonlyHospService
        Inherits ReadOnlyBase(Of ReadonlyHospService)

#Region "Business Methods"

        Private Shared hospsvcProp As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("HospSvc"))
        Public ReadOnly Property hospsvc() As String
            Get
                Return GetProperty(Of String)(hospsvcProp)
            End Get
        End Property

        Private Shared descriptionProp As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Description"))
        Public ReadOnly Property desc() As String
            Get
                Return GetProperty(Of String)(descriptionProp)
            End Get
        End Property

        Private Shared intakeProp As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("IntakeFacility"))
        Public ReadOnly Property intake() As Integer
            Get
                Return GetProperty(Of Integer)(intakeProp)
            End Get
        End Property

        Private Shared deptProp As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Department"))
        Public ReadOnly Property dept() As String
            Get
                Return GetProperty(Of String)(deptProp)
            End Get
        End Property

#End Region

#Region " Authorization Rules"

        Public Shared Function CanGetObject() As Boolean
            Return True
        End Function

#End Region


#Region " Factory Methods "

        Friend Sub New(ByRef dr As SafeDataReader)
            With dr
                LoadProperty(Of String)(hospsvcProp, .GetString("HospSvc"))
                'LoadProperty(Of String)(descriptionProp, .GetString("Description"))
            End With
        End Sub

#End Region






    End Class
End Namespace

