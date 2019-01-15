Imports ITWMaintenance.Library.Users
Imports ITWMaintenance.Library.Security
Imports Csla.Validation
Imports ITWMaintenance.Library.Lookup

Partial Class Users_User_Edit
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("CurrentObject") = Nothing
            ApplyAuthorizationRules()
        Else
            Me.ErrorPanel.Visible = False
            Me.ErrorLabel.Text = ""
        End If

        Dim obj As ITWMaintenance.Library.Users.User = GetUser()
        DepartmentDataSource.SelectParameters("IntakeFacility").DefaultValue = obj.intakeFacility

        Try
            Me.EditFormView.FindControl("FirstNameTextBox").Focus()
        Catch
        End Try
    End Sub

    Private Sub ApplyAuthorizationRules()
        Dim obj As ITWMaintenance.Library.Users.User = GetUser()

        If ITWMaintenance.Library.Users.User.CanEditObject Then
            If obj.IsNew Then
                Me.HeaderLabel.Text = "Add New User"
                Me.EditFormView.DefaultMode = DetailsViewMode.Insert
            Else
                Me.HeaderLabel.Text = "Edit User"
                Me.EditFormView.DefaultMode = DetailsViewMode.Edit
            End If
        Else
            Me.HeaderLabel.Text = "View User Information"
            Me.EditFormView.DefaultMode = DetailsViewMode.ReadOnly
        End If
    End Sub

    Protected Sub BackButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        GoBack()
    End Sub

    Private Sub GoBack()
        Response.Redirect("UserList.aspx?SearchText=" & Request("SearchText") & "&ActiveOnly=" & Request("ActiveOnly"))
    End Sub

    Protected Sub ResetPasswordButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            PasswordReset.ResetPassword(Request("ID"))
            Me.ErrorPanel.Visible = True
            Me.ErrorLabel.Text = "Password Reset Successfully!"
        Catch ex As Csla.DataPortalException
            Me.ErrorPanel.Visible = True
            Me.ErrorLabel.Text = ex.BusinessException.Message
        Catch ex As Exception
            Me.ErrorPanel.Visible = True
            Me.ErrorLabel.Text = ex.Message
        End Try
    End Sub

#Region " Get/Save "

    Private Function GetUser() As ITWMaintenance.Library.Users.User
        Dim businessObject As Object = Session("CurrentObject")

        If IsNothing(businessObject) OrElse Not TypeOf businessObject Is ITWMaintenance.Library.Users.User Then
            Try
                Dim idString As String = Request("ID")

                If Not String.IsNullOrEmpty(idString) Then
                    businessObject = ITWMaintenance.Library.Users.User.GetUser(idString)
                ElseIf Not String.IsNullOrEmpty(Request("addNew")) AndAlso CBool(Request("addNew")) Then
                    ' No ID was passed, but request("addNew") is "true",
                    ' so add a new user
                    businessObject = ITWMaintenance.Library.Users.User.NewUser()
                Else
                    ' Unable to load the specific user, so
                    ' redirect back to the list.
                    Response.Redirect("UserList.aspx")
                End If

                Session("CurrentObject") = businessObject
            Catch ex As System.Security.SecurityException
                ' TODO - Create ErrorPage and code all pages accordingly
                Response.Redirect("../ErrorPage.aspx?ErrorText=Security Exception!&ReturnURL=Maintenance/Users.aspx&ErrorDetails=" & Server.UrlEncode(ex.Message))
            End Try
        End If

        Return CType(businessObject, ITWMaintenance.Library.Users.User)
    End Function

    Private Sub SaveUser(ByVal theUser As ITWMaintenance.Library.Users.User)
        Try
            Session("CurrentObject") = theUser.Save()
            GoBack()
        Catch ex As Csla.Validation.ValidationException
            Dim message As New System.Text.StringBuilder
            message.AppendFormat("{0}:<br/>", ex.Message)

            If theUser.BrokenRulesCollection.Count = 1 Then
                message.AppendFormat("-{0}", theUser.BrokenRulesCollection(0).Description)
            Else
                For Each rule As Csla.Validation.BrokenRule In _
                    theUser.BrokenRulesCollection
                    message.AppendFormat("-{0}<br/>", rule.Description)
                Next
            End If

            Me.ErrorPanel.Visible = True
            Me.ErrorLabel.Text = message.ToString()
        Catch ex As Csla.DataPortalException
            Me.ErrorPanel.Visible = True
            Me.ErrorLabel.Text = ex.BusinessException.Message
        Catch ex As Exception
            Me.ErrorPanel.Visible = True
            Me.ErrorLabel.Text = ex.Message
        End Try
    End Sub

#End Region

#Region " EditFormView "

    Protected Sub EditFormView_ItemCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles EditFormView.ItemCreated
        Dim obj As ITWMaintenance.Library.Users.User = GetUser()

        If EditFormView.DefaultMode = FormViewMode.Edit OrElse EditFormView.DefaultMode = FormViewMode.Insert Then
            ' Set the MaxLength property on the fields based on the ValidationRules for the Object
            For Each ruleURI As String In obj.GetRuleDescriptions()
                Dim ruleDesc As RuleDescription = RuleDescription.Parse(ruleURI)

                Try
                    If Not String.IsNullOrEmpty(ruleDesc.Arguments("MaxLength")) Then
                        CType(Me.EditFormView.FindControl(ruleDesc.PropertyName & "TextBox"), TextBox).MaxLength = ruleDesc.Arguments("MaxLength")
                    End If
                Catch ex As Exception
                End Try
            Next
        End If

        ' The following code is in place to pre-populate default values for a new object,
        ' and to remember what the user has entered on a new item if there is an error.
        If EditFormView.DefaultMode = DetailsViewMode.Insert Then
            CType(EditFormView.FindControl("FirstNameTextBox"), TextBox).Text = obj.FirstName
            CType(EditFormView.FindControl("LastNameTextBox"), TextBox).Text = obj.LastName
            CType(EditFormView.FindControl("FullNameTextBox"), TextBox).Text = obj.FullName
            CType(EditFormView.FindControl("InitialsTextBox"), TextBox).Text = obj.Initials
            CType(EditFormView.FindControl("PhoneTextBox"), TextBox).Text = obj.Phone
            CType(EditFormView.FindControl("PagerTextBox"), TextBox).Text = obj.Pager
            CType(EditFormView.FindControl("EmailTextBox"), TextBox).Text = obj.Email

            CType(EditFormView.FindControl("InactiveCheckBox"), CheckBox).Checked = obj.Inactive
            CType(EditFormView.FindControl("AdministratorCheckBox"), CheckBox).Checked = obj.Administrator
            CType(EditFormView.FindControl("UserNameTextBox"), TextBox).Text = obj.UserName
            If obj.GroupID > 0 Then
                CType(EditFormView.FindControl("UserGroupDropDownList"), DropDownList).SelectedValue = obj.GroupID
            End If
            If Not String.IsNullOrEmpty(obj.UserRole) Then
                Try
                    CType(EditFormView.FindControl("UserRoleDropDownList"), DropDownList).SelectedValue = obj.UserRole
                Catch ex As Exception
                End Try
            End If
            If Not String.IsNullOrEmpty(obj.Department) Then
                Try
                    CType(EditFormView.FindControl("DepartmentDropDownList"), DropDownList).SelectedValue = obj.Department
                Catch ex As Exception
                End Try
            End If
            If obj.Discipline > 0 Then
                Try
                    CType(EditFormView.FindControl("DisciplineDropDownList"), DropDownList).SelectedValue = obj.Discipline
                Catch ex As Exception
                End Try
            End If
            Try
                CType(EditFormView.FindControl("ExportSecurityLevelDropDownList"), DropDownList).SelectedValue = obj.ExportSecurityLevel
            Catch ex As Exception
            End Try
            CType(EditFormView.FindControl("USAProviderIDTextBox"), TextBox).Text = obj.USAProviderID
            CType(EditFormView.FindControl("PhysicianNumberTextBox"), TextBox).Text = obj.PhysicianNumber
            If obj.PhysicianGroup > 0 Then
                CType(EditFormView.FindControl("PhysicianGroupDropDownList"), DropDownList).SelectedValue = obj.PhysicianGroup
            End If
            If obj.MedicalDirectorFacility > 0 Then
                CType(EditFormView.FindControl("MedicalDirectorFacilityDropDownList"), DropDownList).SelectedValue = obj.MedicalDirectorFacility
            End If
        End If
    End Sub

    ' Redirect back to the list on Cancel
    Protected Sub EditFormView_ModeChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewModeEventArgs) Handles EditFormView.ModeChanging
        If e.CancelingEdit Then
            GoBack()
        End If
    End Sub

#End Region

#Region " User Data Source "

    Protected Sub UserDataSource_SelectObject(ByVal sender As Object, ByVal e As Csla.Web.SelectObjectArgs) Handles UserDataSource.SelectObject
        e.BusinessObject = GetUser()
    End Sub

    Protected Sub UserDataSource_InsertObject(ByVal sender As Object, ByVal e As Csla.Web.InsertObjectArgs) Handles UserDataSource.InsertObject
        Dim obj As ITWMaintenance.Library.Users.User = GetUser()
        Csla.Data.DataMapper.Map(e.Values, obj)
        SaveUser(obj)
    End Sub

    Protected Sub UserDataSource_UpdateObject(ByVal sender As Object, ByVal e As Csla.Web.UpdateObjectArgs) Handles UserDataSource.UpdateObject
        Dim obj As ITWMaintenance.Library.Users.User = GetUser()
        Csla.Data.DataMapper.Map(e.Values, obj, "ID")
        SaveUser(obj)
    End Sub

    Protected Sub UserDataSource_DeleteObject(ByVal sender As Object, ByVal e As Csla.Web.DeleteObjectArgs) Handles UserDataSource.DeleteObject
        Try
            ITWMaintenance.Library.Users.User.DeleteUser(e.Keys("ID"))
            Session("CurrentObject") = Nothing
            GoBack()
        Catch ex As Csla.DataPortalException
            Me.ErrorPanel.Visible = True
            Me.ErrorLabel.Text = ex.BusinessException.Message
        Catch ex As Exception
            Me.ErrorPanel.Visible = True
            Me.ErrorLabel.Text = ex.Message
        End Try
    End Sub

#End Region
End Class
