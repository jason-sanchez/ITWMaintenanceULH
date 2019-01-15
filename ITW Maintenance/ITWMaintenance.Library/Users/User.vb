Imports ITWMaintenance.Library.Lookup
Imports System.Data.SqlClient

Namespace Users

    <Serializable()> _
    Public Class User
        Inherits BusinessBase(Of User)

#Region " Business Methods "

        Private Shared IDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ID"))
        <System.ComponentModel.DataObjectField(True, True)> _
        Public ReadOnly Property ID() As Integer
            Get
                Return GetProperty(Of Integer)(IDProperty)
            End Get
        End Property

        Private Shared FirstNameProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("FirstName", "First Name"))
        Public Property FirstName() As String
            Get
                Return GetProperty(Of String)(FirstNameProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(FirstNameProperty, value)
            End Set
        End Property

        Private Shared LastNameProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("LastName", "Last Name"))
        Public Property LastName() As String
            Get
                Return GetProperty(Of String)(LastNameProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(LastNameProperty, value)
            End Set
        End Property

        Private Shared FullNameProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("FullName", "Full Name"))
        Public Property FullName() As String
            Get
                Return GetProperty(Of String)(FullNameProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(FullNameProperty, value)
            End Set
        End Property

        Private Shared InitialsProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Initials"))
        Public Property Initials() As String
            Get
                Return GetProperty(Of String)(InitialsProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(InitialsProperty, value)
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

        Private Shared PagerProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Pager"))
        Public Property Pager() As String
            Get
                Return GetProperty(Of String)(PagerProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(PagerProperty, value)
            End Set
        End Property

        Private Shared EmailProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Email"))
        Public Property Email() As String
            Get
                Return GetProperty(Of String)(EmailProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(EmailProperty, value)
            End Set
        End Property

        Private Shared UserNameProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("UserName"))
        Public Property UserName() As String
            Get
                Return GetProperty(Of String)(UserNameProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(UserNameProperty, value)
            End Set
        End Property

        Private Shared PasswordChangeDateProperty As PropertyInfo(Of SmartDate) = RegisterProperty(New PropertyInfo(Of SmartDate)("PasswordChangeDate", "Password Change Date"))
        Public ReadOnly Property PasswordChangeDate() As SmartDate
            Get
                Return GetProperty(Of SmartDate)(PasswordChangeDateProperty)
            End Get
        End Property

        Private Shared GroupIDProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("GroupID", "Group"))
        Public Property GroupID() As Integer
            Get
                Return GetProperty(Of Integer)(GroupIDProperty)
            End Get
            Set(ByVal value As Integer)
                SetProperty(Of Integer)(GroupIDProperty, value)
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

        Private Shared AdministratorProperty As PropertyInfo(Of Boolean) = RegisterProperty(New PropertyInfo(Of Boolean)("Administrator"))
        Public Property Administrator() As Boolean
            Get
                Return GetProperty(Of Boolean)(AdministratorProperty)
            End Get
            Set(ByVal value As Boolean)
                SetProperty(Of Boolean)(AdministratorProperty, value)
            End Set
        End Property

        Private Shared UserRoleProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("UserRole", "User Role"))
        Public Property UserRole() As String
            Get
                Return GetProperty(Of String)(UserRoleProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(UserRoleProperty, value)
            End Set
        End Property

        Private Shared DepartmentProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("Department"))
        Public Property Department() As String
            Get
                Return GetProperty(Of String)(DepartmentProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(DepartmentProperty, value)
            End Set
        End Property

        Private Shared DisciplineProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("Discipline"))
        Public Property Discipline() As Integer
            Get
                Return GetProperty(Of Integer)(DisciplineProperty)
            End Get
            Set(ByVal value As Integer)
                SetProperty(Of Integer)(DisciplineProperty, value)
            End Set
        End Property

        Private Shared ExportSecurityLevelProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("ExportSecurityLevel", "Export Security Level"))
        Public Property ExportSecurityLevel() As Integer
            Get
                Return GetProperty(Of Integer)(ExportSecurityLevelProperty)
            End Get
            Set(ByVal value As Integer)
                SetProperty(Of Integer)(ExportSecurityLevelProperty, value)
            End Set
        End Property

        Private Shared USAProviderIDProperty As PropertyInfo(Of String) = RegisterProperty(New PropertyInfo(Of String)("USAProviderID", "USA Provider ID"))
        Public Property USAProviderID() As String
            Get
                Return GetProperty(Of String)(USAProviderIDProperty)
            End Get
            Set(ByVal value As String)
                SetProperty(Of String)(USAProviderIDProperty, value)
            End Set
        End Property

        Private Shared PhysicianNumberProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("PhysicianNumber", "Physician Number"))
        Public Property PhysicianNumber() As Integer
            Get
                Return GetProperty(Of Integer)(PhysicianNumberProperty)
            End Get
            Set(ByVal value As Integer)
                SetProperty(Of Integer)(PhysicianNumberProperty, value)
            End Set
        End Property

        Private Shared PhysicianGroupProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("PhysicianGroup", "Physician Group"))
        Public Property PhysicianGroup() As Integer
            Get
                Return GetProperty(Of Integer)(PhysicianGroupProperty)
            End Get
            Set(ByVal value As Integer)
                SetProperty(Of Integer)(PhysicianGroupProperty, value)
            End Set
        End Property

        Private Shared MedicalDirectorFacilityProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("MedicalDirectorFacility", "Medical Director Facility"))
        Public Property MedicalDirectorFacility() As Integer
            Get
                Return GetProperty(Of Integer)(MedicalDirectorFacilityProperty)
            End Get
            Set(ByVal value As Integer)
                SetProperty(Of Integer)(MedicalDirectorFacilityProperty, value)
            End Set
        End Property

        Private Shared intakeFacilityProperty As PropertyInfo(Of Integer) = RegisterProperty(New PropertyInfo(Of Integer)("intakeFacility", "intake Facility"))
        Public Property intakeFacility() As Integer
            Get
                Return GetProperty(Of Integer)(intakeFacilityProperty)
            End Get
            Set(ByVal value As Integer)
                SetProperty(Of Integer)(intakeFacilityProperty, value)
            End Set
        End Property

        Protected Overrides Function GetIdValue() As Object
            Return GetProperty(Of Integer)(IDProperty)
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, FirstNameProperty)
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                                    New Validation.CommonRules.MaxLengthRuleArgs(FirstNameProperty, 50))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, LastNameProperty)
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                                    New Validation.CommonRules.MaxLengthRuleArgs(LastNameProperty, 50))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, FullNameProperty)
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                                    New Validation.CommonRules.MaxLengthRuleArgs(FullNameProperty, 50))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringRequired, UserNameProperty, 0)
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                                    New Validation.CommonRules.MaxLengthRuleArgs(UserNameProperty, 50), 0)
            ValidationRules.AddRule(Of User)(AddressOf ValidateUniqueUserName(Of User), UserNameProperty, 1)

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                                    New Validation.CommonRules.MaxLengthRuleArgs(InitialsProperty, 5))

            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                                    New Validation.CommonRules.MaxLengthRuleArgs(PhoneProperty, 20))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                                    New Validation.CommonRules.MaxLengthRuleArgs(PagerProperty, 50))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.StringMaxLength, _
                                    New Validation.CommonRules.MaxLengthRuleArgs(EmailProperty, 50))
            ValidationRules.AddRule(AddressOf Validation.CommonRules.IntegerMinValue, _
                                    New Validation.CommonRules.IntegerMinValueRuleArgs(intakeFacilityProperty, 1))

            ValidationRules.AddRule(Of User)(AddressOf ValidateGroupID(Of User), GroupIDProperty)
            'ValidationRules.AddRule(Of User)(AddressOf ValidateUserRole(Of User), UserRoleProperty)
            ValidationRules.AddRule(Of User)(AddressOf ValidateDepartment(Of User), DepartmentProperty)
            'ValidationRules.AddRule(Of User)(AddressOf ValidateDiscipline(Of User), DisciplineProperty)
            ValidationRules.AddRule(Of User)(AddressOf ValidateMedicalDirectorFacility(Of User), MedicalDirectorFacilityProperty)
            ValidationRules.AddRule(Of User)(AddressOf ValidatePhysicianGroup(Of User), PhysicianGroupProperty)
        End Sub

        Private Shared Function ValidateUniqueUserName(Of T As User)(ByVal target As T, ByVal e As Validation.RuleArgs) As Boolean
            If User.Exists(target.GetProperty(Of String)(UserNameProperty), target.GetProperty(Of Integer)(IDProperty)) Then
                e.Description = "That UserName has already been used!"
                e.Severity = Validation.RuleSeverity.Error
                Return False
            Else
                Return True
            End If
        End Function

        Private Shared Function ValidateGroupID(Of T As User)(ByVal target As T, ByVal e As Validation.RuleArgs) As Boolean
            If target.GetProperty(Of Integer)(GroupIDProperty) = 0 Then
                e.Description = "Please select a Group for this user."
                e.Severity = Validation.RuleSeverity.Error
                Return False
            Else
                Dim userGroups As ReadOnlyUserGroupList = ReadOnlyUserGroupList.GetUserGroupList()

                If Not userGroups.Contains(target.GetProperty(Of Integer)(GroupIDProperty)) Then
                    e.Description = "The User Group is invalid."
                    e.Severity = Validation.RuleSeverity.Error
                    Return False
                End If
            End If

            Return True
        End Function

        Private Shared Function ValidateUserRole(Of T As User)(ByVal target As T, ByVal e As Validation.RuleArgs) As Boolean
            If String.IsNullOrEmpty(target.GetProperty(Of String)(UserRoleProperty)) Then
                e.Description = "Please select a Role for this user."
                e.Severity = Validation.RuleSeverity.Error
                Return False
            Else
                Dim userRoles As UserRoleList = UserRoleList.GetUserRoles()

                If Not userRoles.Contains(target.GetProperty(Of String)(UserRoleProperty)) Then
                    e.Description = "The User Role is invalid."
                    e.Severity = Validation.RuleSeverity.Error
                    Return False
                End If
            End If

            Return True
        End Function

        Private Shared Function ValidateDepartment(Of T As User)(ByVal target As T, ByVal e As Validation.RuleArgs) As Boolean
            If String.IsNullOrEmpty(target.GetProperty(Of String)(DepartmentProperty)) Then
                e.Description = "Please select a Department for this user."
                e.Severity = Validation.RuleSeverity.Error
                Return False
            Else
                Dim departments As DepartmentList = DepartmentList.GetDepartments()

                If Not departments.Contains(target.GetProperty(Of String)(DepartmentProperty)) Then
                    e.Description = "The Department is invalid."
                    e.Severity = Validation.RuleSeverity.Error
                    Return False
                End If
            End If

            Return True
        End Function

        Private Shared Function ValidateDiscipline(Of T As User)(ByVal target As T, ByVal e As Validation.RuleArgs) As Boolean
            If target.GetProperty(Of Integer)(DisciplineProperty) = 0 Then
                e.Description = "Please select a Discipline for this user."
                e.Severity = Validation.RuleSeverity.Error
                Return False
            Else
                Dim disciplines As DisciplineList = DisciplineList.GetAllDisciplines()

                If Not disciplines.Contains(target.GetProperty(Of Integer)(DisciplineProperty)) Then
                    e.Description = "The Discipline is invalid."
                    e.Severity = Validation.RuleSeverity.Error
                    Return False
                End If
            End If

            Return True
        End Function

        Private Shared Function ValidateMedicalDirectorFacility(Of T As User)(ByVal target As T, ByVal e As Validation.RuleArgs) As Boolean
            If target.GetProperty(Of Integer)(MedicalDirectorFacilityProperty) > 0 Then
                Dim facilities As FacilityList = FacilityList.GetFacilities()

                If Not facilities.Contains(target.GetProperty(Of Integer)(MedicalDirectorFacilityProperty)) Then
                    e.Description = "The Medical Director Facility is invalid."
                    e.Severity = Validation.RuleSeverity.Error
                    Return False
                End If
            End If

            Return True
        End Function

        Private Shared Function ValidatePhysicianGroup(Of T As User)(ByVal target As T, ByVal e As Validation.RuleArgs) As Boolean
            If target.GetProperty(Of Integer)(PhysicianGroupProperty) > 0 Then
                Dim physicianGroups As PhysicianGroupList = PhysicianGroupList.GetPhysicianGroups()

                If Not physicianGroups.Contains(target.GetProperty(Of Integer)(PhysicianGroupProperty)) Then
                    e.Description = "The Physician Group is invalid."
                    e.Severity = Validation.RuleSeverity.Error
                    Return False
                End If
            End If

            Return True
        End Function

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

        Public Shared Function NewUser() As User
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add a User Account")
            End If
            Return DataPortal.Create(Of User)()
        End Function

        Public Shared Function GetUser(ByVal UserID As Integer) As User
            If Not CanGetObject() Then
                Throw New System.Security.SecurityException("User not authorized to view a User Account")
            End If
            Return DataPortal.Fetch(Of User)(New Criteria(UserID))
        End Function

        Public Shared Sub DeleteUser(ByVal UserID As Integer)
            If Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove a User Account")
            End If

            DataPortal.Delete(New Criteria(UserID))
        End Sub

        Public Overloads Function Save() As User
            If IsDeleted AndAlso Not CanDeleteObject() Then
                Throw New System.Security.SecurityException("User not authorized to remove a User Account")
            ElseIf IsNew AndAlso Not CanAddObject() Then
                Throw New System.Security.SecurityException("User not authorized to add a User Account")
            ElseIf Not CanEditObject() Then
                Throw New System.Security.SecurityException("User not authorized to update a User Account")
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
            Private _UserID As Integer

            Public ReadOnly Property UserID() As Integer
                Get
                    Return _UserID
                End Get
            End Property

            Public Sub New(ByVal UserID As Integer)
                _UserID = UserID
            End Sub
        End Class

        <RunLocal()> _
        Protected Overloads Sub DataPortal_Create()
            LoadProperty(Of Integer)(intakeFacilityProperty, DirectCast(Csla.ApplicationContext.User.Identity, Security.ITWIdentity).IntakeFacility)
            ValidationRules.CheckRules()
        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal Criteria As Criteria)
            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    Dim sql As String

                    sql = "SELECT [ID], LastName, FirstName, FullName, Initials, Phone, "
                    sql &= "Pager, Email, UserName, PswdChgDate AS PasswordChangeDate, "
                    sql &= "GroupID, Inactive, Admin, UserRole, Department, Discipline, "
                    sql &= "InfoSecurityLevel AS ExportSecurityLevel, ProvID AS USAProviderID, "
                    sql &= "PhysNum AS PhysicianNumber, PhysGroup AS PhysicianGroupID, "
                    sql &= "MedDirFacility AS MedicalDirectorFacility, intakeFacility "
                    sql &= "FROM [200User] "
                    sql &= "WHERE [ID] = " & Criteria.UserID & " "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    Using dr As New SafeDataReader(cmd.ExecuteReader)
                        dr.Read()
                        With dr
                            LoadProperty(Of Integer)(IDProperty, .GetInt32("ID"))
                            LoadProperty(Of String)(FirstNameProperty, .GetString("FirstName"))
                            LoadProperty(Of String)(LastNameProperty, .GetString("LastName"))
                            LoadProperty(Of String)(FullNameProperty, .GetString("FullName"))
                            LoadProperty(Of String)(InitialsProperty, .GetString("Initials"))
                            LoadProperty(Of String)(PhoneProperty, .GetString("Phone"))
                            LoadProperty(Of String)(PagerProperty, .GetString("Pager"))
                            LoadProperty(Of String)(EmailProperty, .GetString("Email"))
                            LoadProperty(Of String)(UserNameProperty, .GetString("UserName"))
                            LoadProperty(Of SmartDate)(PasswordChangeDateProperty, .GetSmartDate("PasswordChangeDate"))
                            LoadProperty(Of Integer)(GroupIDProperty, .GetInt32("GroupID"))
                            LoadProperty(Of Boolean)(InactiveProperty, .GetBoolean("Inactive"))
                            LoadProperty(Of Boolean)(AdministratorProperty, .GetBoolean("Admin"))
                            LoadProperty(Of String)(UserRoleProperty, .GetString("UserRole"))
                            LoadProperty(Of String)(DepartmentProperty, .GetString("Department"))
                            LoadProperty(Of Integer)(DisciplineProperty, .GetInt32("Discipline"))
                            LoadProperty(Of Integer)(ExportSecurityLevelProperty, .GetInt32("ExportSecurityLevel"))
                            LoadProperty(Of String)(USAProviderIDProperty, .GetString("USAProviderID"))
                            LoadProperty(Of Integer)(PhysicianNumberProperty, .GetInt32("PhysicianNumber"))
                            LoadProperty(Of Integer)(PhysicianGroupProperty, .GetInt32("PhysicianGroupID"))
                            LoadProperty(Of Integer)(MedicalDirectorFacilityProperty, .GetInt32("MedicalDirectorFacility"))
                            LoadProperty(Of Integer)(intakeFacilityProperty, .GetInt32("intakeFacility"))
                        End With
                    End Using
                End Using
            End Using
        End Sub

        Protected Overrides Sub DataPortal_Insert()
            Dim sql As String

            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    sql = "INSERT INTO [200User] ([LastName], [FirstName], "
                    sql &= "FullName, Initials, Phone, Pager, Email, [UserName], "
                    sql &= "[Password], PswdChgDate, [GroupID], [Inactive], "
                    sql &= "[Admin], UserRole, Department, Discipline, InfoSecurityLevel, "
                    sql &= "ProvID, PhysNum, PhysGroup, MedDirFacility, [intakeFacility]) "
                    sql &= "VALUES (@LastName, @FirstName, @FullName, @Initials, "
                    sql &= "@Phone, @Pager, @Email, @UserName, @Password, '1/1/1900', "
                    sql &= "@GroupID, @Inactive, @Admin, @UserRole, @Department, @Discipline, "
                    sql &= "@ExportSecurityLevel, @USAProviderID, @PhysicianNumber, "
                    sql &= "@PhysicianGroupID, @MedicalDirectorFacility, @intakeFacility) "         ' 06/05/2017 MW- #6825- Only add intakeFacility for Insert Query
                    sql &= "SELECT @NewID = SCOPE_IDENTITY() "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    cmd.Parameters.AddWithValue("@LastName", ReadProperty(Of String)(LastNameProperty))
                    cmd.Parameters.AddWithValue("@FirstName", ReadProperty(Of String)(FirstNameProperty))
                    cmd.Parameters.AddWithValue("@FullName", ReadProperty(Of String)(FullNameProperty))
                    cmd.Parameters.AddWithValue("@Initials", ReadProperty(Of String)(InitialsProperty))
                    cmd.Parameters.AddWithValue("@Phone", ReadProperty(Of String)(PhoneProperty))
                    cmd.Parameters.AddWithValue("@Pager", ReadProperty(Of String)(PagerProperty))
                    cmd.Parameters.AddWithValue("@Email", ReadProperty(Of String)(EmailProperty))
                    cmd.Parameters.AddWithValue("@UserName", ReadProperty(Of String)(UserNameProperty))
                    cmd.Parameters.AddWithValue("@Password", "password")
                    cmd.Parameters.AddWithValue("@GroupID", ReadProperty(Of Integer)(GroupIDProperty))
                    cmd.Parameters.AddWithValue("@Inactive", ReadProperty(Of Boolean)(InactiveProperty))
                    cmd.Parameters.AddWithValue("@Admin", ReadProperty(Of Boolean)(AdministratorProperty))
                    cmd.Parameters.AddWithValue("@UserRole", ReadProperty(Of String)(UserRoleProperty))
                    cmd.Parameters.AddWithValue("@Department", ReadProperty(Of String)(DepartmentProperty))
                    cmd.Parameters.AddWithValue("@Discipline", ReadProperty(Of Integer)(DisciplineProperty))
                    cmd.Parameters.AddWithValue("@ExportSecurityLevel", ReadProperty(Of Integer)(ExportSecurityLevelProperty))
                    cmd.Parameters.AddWithValue("@USAProviderID", ReadProperty(Of String)(USAProviderIDProperty))
                    cmd.Parameters.AddWithValue("@PhysicianNumber", ReadProperty(Of Integer)(PhysicianNumberProperty))
                    cmd.Parameters.AddWithValue("@PhysicianGroupID", ReadProperty(Of Integer)(PhysicianGroupProperty))
                    cmd.Parameters.AddWithValue("@MedicalDirectorFacility", ReadProperty(Of Integer)(MedicalDirectorFacilityProperty))
                    cmd.Parameters.AddWithValue("@intakeFacility", ReadProperty(Of Integer)(intakeFacilityProperty))

                    Dim param As New SqlParameter("@NewID", SqlDbType.Int)
                    param.Direction = ParameterDirection.Output
                    cmd.Parameters.Add(param)

                    ' Save the ID...
                    LoadProperty(Of Integer)(IDProperty, CInt(cmd.ExecuteScalar()))
                End Using

                Using Logcmd As SqlCommand = conn.CreateCommand
                    Dim logDescription As String

                    logDescription = DirectCast(Csla.ApplicationContext.User.Identity, Security.ITWIdentity).Name & "Added New UserID: '" & ReadProperty(Of Integer)(IDProperty) & "' and UserName: '" & ReadProperty(Of String)(UserNameProperty) & "'."

                    sql = "insert into [060transLog] (logDate, logType, userID, "
                    sql &= "logDescription) values ("
                    sql &= "GetDate(), "
                    sql &= "'User Added',"
                    sql &= DirectCast(Csla.ApplicationContext.User.Identity, Security.ITWIdentity).UserID & ", "
                    sql &= "'" & Replace(logDescription, "'", "''") & "') "

                    Logcmd.CommandType = CommandType.Text
                    Logcmd.CommandText = sql

                    Logcmd.Parameters.AddWithValue("@UserName", ReadProperty(Of String)(UserNameProperty))

                    Logcmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        Protected Overrides Sub DataPortal_Update()
            Dim sql As String

            If IsSelfDirty Then
                Using conn As New SqlConnection(Database.ITWConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand
                        sql = "UPDATE [200User] SET "
                        sql &= "LastName = @LastName, "
                        sql &= "FirstName = @FirstName, "
                        sql &= "FullName = @FullName, "
                        sql &= "Initials = @Initials, "
                        sql &= "Phone = @Phone, "
                        sql &= "Pager = @Pager, "
                        sql &= "Email = @Email, "
                        sql &= "UserName = @UserName, "
                        sql &= "GroupID = @GroupID, "
                        sql &= "Inactive = @Inactive, "
                        sql &= "Admin = @Admin, "
                        sql &= "UserRole = @UserRole, "
                        sql &= "Department = @Department, "
                        sql &= "Discipline = @Discipline, "
                        sql &= "InfoSecurityLevel = @ExportSecurityLevel, "
                        sql &= "ProvID = @USAProviderID, "
                        sql &= "PhysNum = @PhysicianNumber, "
                        sql &= "PhysGroup = @PhysicianGroup, "
                        sql &= "MedDirFacility = @MedicalDirectorFacility "
                        sql &= "WHERE [ID] = @ID "

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql

                        cmd.Parameters.AddWithValue("@ID", ReadProperty(Of Integer)(IDProperty))
                        cmd.Parameters.AddWithValue("@LastName", ReadProperty(Of String)(LastNameProperty))
                        cmd.Parameters.AddWithValue("@FirstName", ReadProperty(Of String)(FirstNameProperty))
                        cmd.Parameters.AddWithValue("@FullName", ReadProperty(Of String)(FullNameProperty))
                        cmd.Parameters.AddWithValue("@Initials", ReadProperty(Of String)(InitialsProperty))
                        cmd.Parameters.AddWithValue("@Phone", ReadProperty(Of String)(PhoneProperty))
                        cmd.Parameters.AddWithValue("@Pager", ReadProperty(Of String)(PagerProperty))
                        cmd.Parameters.AddWithValue("@Email", ReadProperty(Of String)(EmailProperty))
                        cmd.Parameters.AddWithValue("@UserName", ReadProperty(Of String)(UserNameProperty))
                        cmd.Parameters.AddWithValue("@GroupID", ReadProperty(Of Integer)(GroupIDProperty))
                        cmd.Parameters.AddWithValue("@Inactive", ReadProperty(Of Boolean)(InactiveProperty))
                        cmd.Parameters.AddWithValue("@Admin", ReadProperty(Of Boolean)(AdministratorProperty))
                        cmd.Parameters.AddWithValue("@UserRole", ReadProperty(Of String)(UserRoleProperty))
                        cmd.Parameters.AddWithValue("@Department", ReadProperty(Of String)(DepartmentProperty))
                        cmd.Parameters.AddWithValue("@Discipline", ReadProperty(Of Integer)(DisciplineProperty))
                        cmd.Parameters.AddWithValue("@ExportSecurityLevel", ReadProperty(Of Integer)(ExportSecurityLevelProperty))
                        cmd.Parameters.AddWithValue("@USAProviderID", ReadProperty(Of String)(USAProviderIDProperty))
                        cmd.Parameters.AddWithValue("@PhysicianNumber", ReadProperty(Of Integer)(PhysicianNumberProperty))
                        cmd.Parameters.AddWithValue("@PhysicianGroup", ReadProperty(Of Integer)(PhysicianGroupProperty))
                        cmd.Parameters.AddWithValue("@MedicalDirectorFacility", ReadProperty(Of Integer)(MedicalDirectorFacilityProperty))
                        cmd.ExecuteNonQuery()
                    End Using
                    Using Logcmd As SqlCommand = conn.CreateCommand
                        Dim logDescription As String

                        logDescription = DirectCast(Csla.ApplicationContext.User.Identity, Security.ITWIdentity).Name & " Modified UserID: '" & ReadProperty(Of Integer)(IDProperty) & "' and UserName: '" & ReadProperty(Of String)(UserNameProperty) & "'."

                        sql = "insert into [060transLog] (logDate, logType, userID, "
                        sql &= "logDescription) values ("
                        sql &= "GetDate(), "
                        sql &= "'User Modified', "
                        sql &= DirectCast(Csla.ApplicationContext.User.Identity, Security.ITWIdentity).UserID & ", "
                        sql &= "'" & Replace(logDescription, "'", "''") & "') "

                        Logcmd.CommandType = CommandType.Text
                        Logcmd.CommandText = sql

                        Logcmd.Parameters.AddWithValue("@UserName", ReadProperty(Of String)(UserNameProperty))

                        Logcmd.ExecuteNonQuery()
                    End Using
                End Using
            End If
        End Sub

        Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria(ReadProperty(Of Integer)(IDProperty)))
        End Sub

        Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
            Dim sql As String
            Using conn As New SqlConnection(Database.ITWConnection)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand
                    sql = "DELETE FROM [200User] "
                    sql &= "WHERE [ID] = @ID "

                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = sql

                    cmd.Parameters.AddWithValue("@ID", criteria.UserID)
                    cmd.ExecuteNonQuery()
                End Using
                Using Logcmd As SqlCommand = conn.CreateCommand
                    Dim logDescription As String

                    logDescription = DirectCast(Csla.ApplicationContext.User.Identity, Security.ITWIdentity).Name & " Deleted UserID: '" & ReadProperty(Of Integer)(IDProperty) & "' and UserName: '" & ReadProperty(Of String)(UserNameProperty) & "'."

                    sql = "insert into [060transLog] (logDate, logType, userID, "
                    sql &= "logDescription) values ("
                    sql &= "GetDate(), "
                    sql &= "'User Deleted', "
                    sql &= DirectCast(Csla.ApplicationContext.User.Identity, Security.ITWIdentity).UserID & ", "
                    sql &= "'" & Replace(logDescription, "'", "''") & "') "

                    Logcmd.CommandType = CommandType.Text
                    Logcmd.CommandText = sql

                    Logcmd.Parameters.AddWithValue("@UserName", ReadProperty(Of String)(UserNameProperty))

                    Logcmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

#End Region

#Region " Exists "

        Public Shared Function Exists(ByVal UserID As Integer)
            Return ExistsCommand.ExistsByID(UserID)
        End Function

        Public Shared Function Exists(ByVal UserName As String, ByVal IgnoreID As Integer) As Boolean
            Return ExistsCommand.ExistsByUserName(UserName, IgnoreID)
        End Function

        <Serializable()> _
        Private Class ExistsCommand
            Inherits CommandBase

            Private Enum SearchMethod
                UserID = 1
                UserName = 2
            End Enum

            Private _SearchMethod As SearchMethod
            Private _UserID As Integer
            Private _UserName As String
            Private _IgnoreID As Integer = 0
            Private _Exists As Boolean

            Public ReadOnly Property ValidUserID() As Boolean
                Get
                    Return Me._Exists
                End Get
            End Property

            Public ReadOnly Property UserNameAlreadyExists() As Boolean
                Get
                    Return Me._Exists
                End Get
            End Property

            Public Shared Function ExistsByID(ByVal UserID As Integer) As Boolean
                Dim result As ExistsCommand
                result = DataPortal.Execute(Of ExistsCommand)(New ExistsCommand(UserID))
                Return result.ValidUserID
            End Function

            Public Shared Function ExistsByUserName(ByVal UserName As String, ByVal IgnoreID As Integer) As Boolean
                Dim result As ExistsCommand
                result = DataPortal.Execute(Of ExistsCommand)(New ExistsCommand(UserName, 0))
                Return result.UserNameAlreadyExists
            End Function

            Private Sub New(ByVal UserID As Integer)
                Me._UserID = UserID
                Me._SearchMethod = SearchMethod.UserID
            End Sub

            Private Sub New(ByVal UserName As String, ByVal IgnoreID As Integer)
                Me._UserName = UserName
                Me._IgnoreID = IgnoreID
                Me._SearchMethod = SearchMethod.UserName
            End Sub

            Protected Overrides Sub DataPortal_Execute()
                Using conn As New SqlConnection(Database.ITWConnection)
                    conn.Open()
                    Using cmd As SqlCommand = conn.CreateCommand
                        Dim sql As String

                        sql = "SELECT COUNT([ID]) AS [Exists] "
                        sql &= "FROM [200User] "
                        If Me._SearchMethod = SearchMethod.UserID Then
                            sql &= "WHERE [ID] = " & Me._UserID.ToString() & " "
                        Else
                            sql &= "WHERE [UserName] = '" & Me._UserName & "' "
                            If Me._IgnoreID > 0 Then
                                sql &= "AND [ID] <> " & Me._IgnoreID.ToString() & " "
                            End If
                        End If

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = sql

                        Dim count As Integer = CInt(cmd.ExecuteScalar)
                        Me._Exists = (count > 0)
                    End Using
                End Using
            End Sub

        End Class

#End Region

    End Class

End Namespace