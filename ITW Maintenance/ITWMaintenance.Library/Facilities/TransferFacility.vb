Imports ITWMaintenance.Library.Lookup
Imports System.Data.SqlClient

Namespace Facilities

    <Serializable()> _
    Public Class TransferFacility
        Inherits BusinessBase(Of TransferFacility)

#Region " Business Methods "

        Private Shared IDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
        <System.ComponentModel.DataObjectField(True, True)> _
        Public ReadOnly Property ID() As Integer
            Get
                Return GetProperty(Of Integer)(IDProperty)
            End Get
        End Property

        Private Shared NameProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Name"))
        Public Property Name() As String
            Get
                Return GetProperty(Of String)(NameProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(NameProperty, value)
            End Set
        End Property

        Private Shared AddressLine1Property As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("AddressLine1", "Address Line 1"))
        Public Property AddressLine1() As String
            Get
                Return GetProperty(Of String)(AddressLine1Property)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(AddressLine1Property, value)
            End Set
        End Property

        Private Shared AddressLine2Property As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("AddressLine2", "Address Line 2"))
        Public Property AddressLine2() As String
            Get
                Return GetProperty(Of String)(AddressLine2Property)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(AddressLine2Property, value)
            End Set
        End Property

        Private Shared CityProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("City"))
        Public Property City() As String
            Get
                Return GetProperty(Of String)(CityProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(CityProperty, value)
            End Set
        End Property

        Private Shared StateProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("State"))
        Public Property State() As String
            Get
                Return GetProperty(Of String)(StateProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(StateProperty, value)
            End Set
        End Property

        Private Shared ZipProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Zip"))
        Public Property Zip() As String
            Get
                Return GetProperty(Of String)(ZipProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(ZipProperty, value)
            End Set
        End Property

        Private Shared ContactNameProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("ContactName", "Contact Name"))
        Public Property ContactName() As String
            Get
                Return GetProperty(Of String)(ContactNameProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(ContactNameProperty, value)
            End Set
        End Property

        Private Shared PhoneProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Phone"))
        Public Property Phone() As String
            Get
                Return GetProperty(Of String)(PhoneProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(PhoneProperty, value)
            End Set
        End Property

        Private Shared FaxProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Fax"))
        Public Property Fax() As String
            Get
                Return GetProperty(Of String)(FaxProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(FaxProperty, value)
            End Set
        End Property

        Private Shared FacilityTypeProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("FacilityType", "Facility Type"))
        Public Property FacilityType() As String
            Get
                Return GetProperty(Of String)(FacilityTypeProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(FacilityTypeProperty, value)
            End Set
        End Property

        Private Shared SMSIDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("SMSID", "SMS ID"))
        Public Property SMSID() As Integer
            Get
                Return GetProperty(Of Integer)(SMSIDProperty)
            End Get
            Set(ByVal value As Integer)
                SetProperty(Of Integer)(SMSIDProperty, value)
            End Set
        End Property

        Private Shared LastModifiedProperty As PropertyInfo(Of SmartDate) = RegisterProperty(New PropertyInfo(Of SmartDate)("LastModified", "Last Modified"))
        Public ReadOnly Property LastModified() As SmartDate
            Get
                Return GetProperty(Of SmartDate)(LastModifiedProperty)
            End Get
        End Property

        Private Shared TransferFacilityProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("TransferFacility", "Transfer Facility"))
        Public Property TransferFacility() As Boolean
            Get
                Return GetProperty(Of Boolean)(TransferFacilityProperty)
            End Get
            Set(ByVal value As Boolean)
                SetProperty(Of Boolean)(TransferFacilityProperty, value)
            End Set
        End Property

        Private Shared OutpatientTherapyProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("OutpatientTherapy", "Outpatient Therapy"))
        Public Property OutpatientTherapy() As Boolean
            Get
                Return GetProperty(Of Boolean)(OutpatientTherapyProperty)
            End Get
            Set(ByVal value As Boolean)
                SetProperty(Of Boolean)(OutpatientTherapyProperty, value)
            End Set
        End Property

        Private Shared AcuteFacilityProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("AcuteFacility", "Acute Facility"))
        Public Property AcuteFacility() As Boolean
            Get
                Return GetProperty(Of Boolean)(AcuteFacilityProperty)
            End Get
            Set(ByVal value As Boolean)
                SetProperty(Of Boolean)(AcuteFacilityProperty, value)
            End Set
        End Property

        Private Shared InactiveProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("Inactive"))
        Public Property Inactive() As Boolean
            Get
                Return GetProperty(Of Boolean)(InactiveProperty)
            End Get
            Set(ByVal value As Boolean)
                SetProperty(Of Boolean)(InactiveProperty, value)
            End Set
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return GetProperty(Of Integer)(IDProperty)
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, NameProperty)
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                                    New Validation.CommonRules.MaxLengthRuleArgs(NameProperty, 50))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                                    New Validation.CommonRules.MaxLengthRuleArgs(AddressLine1Property, 50))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                                    New Validation.CommonRules.MaxLengthRuleArgs(AddressLine2Property, 50))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                                    New Validation.CommonRules.MaxLengthRuleArgs(CityProperty, 50))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                                    New Validation.CommonRules.MaxLengthRuleArgs(StateProperty, 2))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                                    New Validation.CommonRules.MaxLengthRuleArgs(ZipProperty, 10))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                                    New Validation.CommonRules.MaxLengthRuleArgs(ContactNameProperty, 50))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                                    New Validation.CommonRules.MaxLengthRuleArgs(PhoneProperty, 20))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                                    New Validation.CommonRules.MaxLengthRuleArgs(FaxProperty, 20))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                                    New Validation.CommonRules.MaxLengthRuleArgs(FacilityTypeProperty, 25))
        End Sub

        Public Function GetRuleDescriptions() As String()
            Return ValidationRules.GetRuleDescriptions()
        End Function

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()
            ' Add AuthorizationRules here
        End Sub

        Public Shared Function CanAddObject() As Boolean
            Return True 'Csla.ApplicationContext.User.IsInRole("Systemax Employee")
        End Function

        Public Shared Function CanGetObject() As Boolean
            Return True 'Csla.ApplicationContext.User.IsInRole("Systemax Employee")
        End Function

        Public Shared Function CanDeleteObject() As Boolean
            ' Use the inactive switch
            Return False 'Csla.ApplicationContext.User.IsInRole("Systemax Employee")
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return True 'Csla.ApplicationContext.User.IsInRole("Systemax Employee")
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function NewFacility() As TransferFacility
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add a Facility")
            End If
            Return DataPortal.Create(Of TransferFacility)()
        End Function

        Public Shared Function GetFacility(ByVal FacilityID As Integer) As TransferFacility
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view a Facility")
            End If
            Return DataPortal.Fetch(Of TransferFacility)(New Criteria(FacilityID))
        End Function

        Public Shared Sub DeleteFacility(ByVal FacilityID As Integer)
            If Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove a Facility")
            End If

            DataPortal.Delete(New Criteria(FacilityID))
        End Sub

        Public Overloads Function Save() As TransferFacility
            If IsDeleted AndAlso Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove a Facility")
            ElseIf IsNew AndAlso Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add a Facility")
            ElseIf Not CanEditObject() Then
                Throw New System.Security.SecurityException("User not authorized to update a Facility")
            End If

            Return MyBase.Save()
        End Function

        Private Sub New()
            ' Require use of Factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _FacilityID As Integer

            Public ReadOnly Property FacilityID() As Integer
                Get
                    Return _FacilityID
                End Get
            End Property

            Public Sub New(ByVal FacilityID As Integer)
                _FacilityID = FacilityID
            End Sub
        End Class

        <RunLocal()> _
        Protected Overloads Sub DataPortal_Create()
            LoadProperty(Of Boolean)(InactiveProperty, False)
            ValidationRules.CheckRules()
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal Criteria As Criteria)
            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT [ID], [Facility], [Addr1], [Addr2], [City], [State], "
                    sql &= "[Zip], [Contact], [Phone], [Fax], [SMSNum], [FacilityType], "
                    sql &= "[modified], [TransferFacility], [opTherapy], [AcuteFacility], "
                    sql &= "[Inactive] "
                    sql &= "FROM [111TranFac] "
                    sql &= "WHERE [ID] = " & Criteria.FacilityID.ToString() & " "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        dr.Read()
                        With dr
                            LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                            LoadProperty(Of String)(NameProperty, .GetString("Facility"))
                            LoadProperty(Of String)(AddressLine1Property, .GetString("Addr1"))
                            LoadProperty(Of String)(AddressLine2Property, .GetString("Addr2"))
                            LoadProperty(Of String)(CityProperty, .GetString("City"))
                            LoadProperty(Of String)(StateProperty, .GetString("State"))
                            LoadProperty(Of String)(ZipProperty, .GetString("Zip"))
                            LoadProperty(Of String)(ContactNameProperty, .GetString("Contact"))
                            LoadProperty(Of String)(PhoneProperty, .GetString("Phone"))
                            LoadProperty(Of String)(FaxProperty, .GetString("Fax"))
                            LoadProperty(Of Integer)(SMSIDProperty, .GetInt32("SMSNum"))
                            LoadProperty(Of String)(FacilityTypeProperty, .GetString("FacilityType"))
                            LoadProperty(Of SmartDate)(LastModifiedProperty, .GetSmartDate("modified"))
                            LoadProperty(Of Boolean)(TransferFacilityProperty, .GetBoolean("TransferFacility"))
                            LoadProperty(Of Boolean)(OutpatientTherapyProperty, .GetBoolean("opTherapy"))
                            LoadProperty(Of Boolean)(AcuteFacilityProperty, .GetBoolean("AcuteFacility"))
                            LoadProperty(Of Boolean)(InactiveProperty, .GetBoolean("Inactive"))
                        End With
                    End Using
                End Using
            End Using
        End Sub

        Protected Overrides Sub DataPortal_Insert()
            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "INSERT INTO [111TranFac] ([Facility], [Addr1], [Addr2], [City], "
                    sql &= "[State], [Zip], [Contact], [Phone], [Fax], [SMSNum], [FacilityType], "
                    sql &= "[modified], [TransferFacility], [opTherapy], [AcuteFacility], [Inactive]) "
                    sql &= "VALUES (@Facility, @Addr1, @Addr2, @City, @State, @Zip, @Contact, "
                    sql &= "@Phone, @Fax, @SMSNum, @FacilityType, GetDate(), @TransferFacility, "
                    sql &= "@opTherapy, @AcuteFacility, @Inactive) "
                    sql &= "SELECT @NewID = SCOPE_IDENTITY() "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    cmd.Parameters.AddWithValue("@Facility", ReadProperty(Of String)(NameProperty))
                    cmd.Parameters.AddWithValue("@Addr1", ReadProperty(Of String)(AddressLine1Property))
                    cmd.Parameters.AddWithValue("@Addr2", ReadProperty(Of String)(AddressLine2Property))
                    cmd.Parameters.AddWithValue("@City", ReadProperty(Of String)(CityProperty))
                    cmd.Parameters.AddWithValue("@State", ReadProperty(Of String)(StateProperty))
                    cmd.Parameters.AddWithValue("@Zip", ReadProperty(Of String)(ZipProperty))
                    cmd.Parameters.AddWithValue("@Contact", ReadProperty(Of String)(ContactNameProperty))
                    cmd.Parameters.AddWithValue("@Phone", ReadProperty(Of String)(PhoneProperty))
                    cmd.Parameters.AddWithValue("@Fax", ReadProperty(Of String)(FaxProperty))
                    cmd.Parameters.AddWithValue("@SMSNum", ReadProperty(Of Integer)(SMSIDProperty))
                    cmd.Parameters.AddWithValue("@FacilityType", ReadProperty(Of String)(FacilityTypeProperty))
                    cmd.Parameters.AddWithValue("@TransferFacility", ReadProperty(Of Boolean)(TransferFacilityProperty))
                    cmd.Parameters.AddWithValue("@opTherapy", ReadProperty(Of Boolean)(OutpatientTherapyProperty))
                    cmd.Parameters.AddWithValue("@AcuteFacility", ReadProperty(Of Boolean)(AcuteFacilityProperty))
                    cmd.Parameters.AddWithValue("@Inactive", ReadProperty(Of Boolean)(InactiveProperty))

                    Dim param As New SqlParameter("@NewID", SqlDbType.Int)
                    param.Direction = ParameterDirection.Output
                    cmd.Parameters.Add(param)

                    ' Save the ID...
                    LoadProperty(Of Integer)(IDProperty, CInt(cmd.ExecuteScalar()))
                End Using
            End Using
        End Sub

        Protected Overrides Sub DataPortal_Update()
            If IsSelfDirty Then
                Using conn As New SqlConnection(Database.ITWConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand
                        Dim sql As String

                        sql = "UPDATE [111TranFac] SET "
                        sql &= "[Facility] = @Facility, "
                        sql &= "[Addr1] = @Addr1, "
                        sql &= "[Addr2] = @Addr2, "
                        sql &= "[City] = @City, "
                        sql &= "[State] = @State, "
                        sql &= "[Zip] = @Zip, "
                        sql &= "[Contact] = @Contact, "
                        sql &= "[Phone] = @Phone, "
                        sql &= "[Fax] = @Fax, "
                        sql &= "[SMSNum] = @SMSNum, "
                        sql &= "[FacilityType] = @FacilityType, "
                        sql &= "[modified] = GetDate(), "
                        sql &= "[TransferFacility] = @TransferFacility, "
                        sql &= "[opTherapy] = @opTherapy, "
                        sql &= "[AcuteFacility] = @AcuteFacility, "
                        sql &= "[Inactive] = @Inactive "
                        sql &= "WHERE [ID] = @ID "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql

                        cmd.Parameters.AddWithValue("@ID", ReadProperty(Of Integer)(IDProperty))
                        cmd.Parameters.AddWithValue("@Facility", ReadProperty(Of String)(NameProperty))
                        cmd.Parameters.AddWithValue("@Addr1", ReadProperty(Of String)(AddressLine1Property))
                        cmd.Parameters.AddWithValue("@Addr2", ReadProperty(Of String)(AddressLine2Property))
                        cmd.Parameters.AddWithValue("@City", ReadProperty(Of String)(CityProperty))
                        cmd.Parameters.AddWithValue("@State", ReadProperty(Of String)(StateProperty))
                        cmd.Parameters.AddWithValue("@Zip", ReadProperty(Of String)(ZipProperty))
                        cmd.Parameters.AddWithValue("@Contact", ReadProperty(Of String)(ContactNameProperty))
                        cmd.Parameters.AddWithValue("@Phone", ReadProperty(Of String)(PhoneProperty))
                        cmd.Parameters.AddWithValue("@Fax", ReadProperty(Of String)(FaxProperty))
                        cmd.Parameters.AddWithValue("@SMSNum", ReadProperty(Of Integer)(SMSIDProperty))
                        cmd.Parameters.AddWithValue("@FacilityType", ReadProperty(Of String)(FacilityTypeProperty))
                        cmd.Parameters.AddWithValue("@TransferFacility", ReadProperty(Of Boolean)(TransferFacilityProperty))
                        cmd.Parameters.AddWithValue("@opTherapy", ReadProperty(Of Boolean)(OutpatientTherapyProperty))
                        cmd.Parameters.AddWithValue("@AcuteFacility", ReadProperty(Of Boolean)(AcuteFacilityProperty))
                        cmd.Parameters.AddWithValue("@Inactive", ReadProperty(Of Boolean)(InactiveProperty))
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
            End If
        End Sub

        Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria(ReadProperty(Of Integer)(IDProperty)))
        End Sub

        Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "DELETE FROM [111TranFac] "
                    sql &= "WHERE [ID] = @ID "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    cmd.Parameters.AddWithValue("@ID", criteria.FacilityID)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

#End Region

    End Class

End Namespace