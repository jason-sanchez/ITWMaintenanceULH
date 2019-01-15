Imports System.Data.SqlClient

Namespace Facilities

    <Serializable()> _
    Public Class ReadOnlyTransferFacility
        Inherits ReadOnlyBase(Of ReadOnlyTransferFacility)

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

        Private Shared ContactNameProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("ContactName", "Contact Name"))
        Public ReadOnly Property ContactName() As String
            Get
                Return GetProperty(Of String)(ContactNameProperty)
            End Get
        End Property

        Private Shared PhoneProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Phone"))
        Public ReadOnly Property Phone() As String
            Get
                Return GetProperty(Of String)(PhoneProperty)
            End Get
        End Property

        Private Shared TransferFacilityProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("TransferFacility", "Transfer Facility"))
        Public ReadOnly Property TransferFacility() As Boolean
            Get
                Return GetProperty(Of Boolean)(TransferFacilityProperty)
            End Get
        End Property

        Private Shared OutpatientTherapyProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("OutpatientTherapy", "Outpatient Therapy"))
        Public ReadOnly Property OutpatientTherapy() As Boolean
            Get
                Return GetProperty(Of Boolean)(OutpatientTherapyProperty)
            End Get
        End Property

        Private Shared AcuteFacilityProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("AcuteFacility", "Acute Facility"))
        Public ReadOnly Property AcuteFacility() As Boolean
            Get
                Return GetProperty(Of Boolean)(AcuteFacilityProperty)
            End Get
        End Property

        Private Shared InactiveProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("Inactive"))
        Public ReadOnly Property Inactive() As Boolean
            Get
                Return GetProperty(Of Boolean)(InactiveProperty)
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
                LoadProperty(Of String)(NameProperty, .GetString("Facility"))
                LoadProperty(Of String)(ContactNameProperty, .GetString("Contact"))
                LoadProperty(Of String)(PhoneProperty, .GetString("Phone"))
                LoadProperty(Of Boolean)(TransferFacilityProperty, .GetBoolean("TransferFacility"))
                LoadProperty(Of Boolean)(OutpatientTherapyProperty, .GetBoolean("opTherapy"))
                LoadProperty(Of Boolean)(AcuteFacilityProperty, .GetBoolean("AcuteFacility"))
                LoadProperty(Of Boolean)(InactiveProperty, .GetBoolean("Inactive"))
            End With
        End Sub

        Public Shared Function GetFacility(ByVal FacilityID As Integer) As ReadOnlyTransferFacility
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view a Transfer Facility")
            End If
            Return DataPortal.Fetch(Of ReadOnlyTransferFacility)(New Criteria(FacilityID))
        End Function

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _FacilityID As Integer

            Public ReadOnly Property FacilityID() As Integer
                Get
                    Return Me._FacilityID
                End Get
            End Property

            Public Sub New(ByVal FacilityID As Integer)
                Me._FacilityID = FacilityID
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT [ID], [Facility], [Contact], [Phone], [TransferFacility], "
                    sql &= "[opTherapy], [AcuteFacility], [Inactive] "
                    sql &= "FROM [111TranFac] "
                    sql &= "WHERE [ID] = " & criteria.FacilityID.ToString() & " "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        dr.Read()
                        With dr
                            LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                            LoadProperty(Of String)(NameProperty, .GetString("Facility"))
                            LoadProperty(Of String)(ContactNameProperty, .GetString("Contact"))
                            LoadProperty(Of String)(PhoneProperty, .GetString("Phone"))
                            LoadProperty(Of Boolean)(TransferFacilityProperty, .GetBoolean("TransferFacility"))
                            LoadProperty(Of Boolean)(OutpatientTherapyProperty, .GetBoolean("opTherapy"))
                            LoadProperty(Of Boolean)(AcuteFacilityProperty, .GetBoolean("AcuteFacility"))
                            LoadProperty(Of Boolean)(InactiveProperty, .GetBoolean("Inactive"))
                        End With
                    End Using
                End Using
            End Using
        End Sub

#End Region

    End Class

End Namespace