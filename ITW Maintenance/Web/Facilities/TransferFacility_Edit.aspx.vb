Imports ITWMaintenance.Library.Facilities
Imports Csla.Validation

Partial Class Facilities_TransferFacility_Edit
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("CurrentObject") = Nothing
            ApplyAuthorizationRules()
        Else
            Me.ErrorPanel.Visible = False
            Me.ErrorLabel.Text = ""
        End If

        Try
            Me.EditFormView.Focus()
        Catch
        End Try
    End Sub

    Private Sub ApplyAuthorizationRules()
        Dim obj As TransferFacility = GetFacility()

        If TransferFacility.CanEditObject Then
            If obj.IsNew Then
                Me.HeaderLabel.Text = "Add New Facility"
                Me.EditFormView.DefaultMode = DetailsViewMode.Insert
            Else
                Me.HeaderLabel.Text = "Edit Facility"
                Me.EditFormView.DefaultMode = DetailsViewMode.Edit
            End If
        Else
            Me.HeaderLabel.Text = "View Facility Information"
            Me.EditFormView.DefaultMode = DetailsViewMode.ReadOnly
        End If
    End Sub

    Protected Sub BackButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        GoBack()
    End Sub

    Private Sub GoBack()
        Response.Redirect("TransferFacility_List.aspx?SearchText=" & Request("SearchText") & "&ActiveOnly=" & Request("ActiveOnly") & "&FacilityType=" & Request("FacilityType"))
    End Sub

#Region " Get/Save "

    Private Function GetFacility() As TransferFacility
        Dim businessObject As Object = Session("CurrentObject")

        If IsNothing(businessObject) OrElse Not TypeOf businessObject Is TransferFacility Then
            Try
                Dim idString As String = Request("ID")

                If Not String.IsNullOrEmpty(idString) Then
                    businessObject = TransferFacility.GetFacility(idString)
                ElseIf Not String.IsNullOrEmpty(Request("addNew")) AndAlso CBool(Request("addNew")) Then
                    ' No ID was passed, but request("addNew") is "true",
                    ' so add a new user
                    businessObject = TransferFacility.NewFacility()
                Else
                    ' Unable to load the specific user, so
                    ' redirect back to the list.
                    GoBack()
                End If

                Session("CurrentObject") = businessObject
            Catch ex As System.Security.SecurityException
                ' TODO - Create ErrorPage and code all pages accordingly
                Response.Redirect("../ErrorPage.aspx?ErrorText=Security Exception!&ReturnURL=Facilities/TransferFacility_List.aspx&ErrorDetails=" & Server.UrlEncode(ex.Message))
            End Try
        End If

        Return CType(businessObject, TransferFacility)
    End Function

    Private Sub SaveFacility(ByVal theFacility As TransferFacility)
        Try
            Session("CurrentObject") = theFacility.Save()
            GoBack()
        Catch ex As Csla.Validation.ValidationException
            Dim message As New System.Text.StringBuilder
            message.AppendFormat("{0}:<br/>", ex.Message)

            If theFacility.BrokenRulesCollection.Count = 1 Then
                message.AppendFormat("-{0}", theFacility.BrokenRulesCollection(0).Description)
            Else
                For Each rule As Csla.Validation.BrokenRule In _
                    theFacility.BrokenRulesCollection
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
        Dim obj As TransferFacility = GetFacility()

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
            CType(EditFormView.FindControl("NameTextBox"), TextBox).Text = obj.Name
            CType(EditFormView.FindControl("ContactNameTextBox"), TextBox).Text = obj.ContactName
            CType(EditFormView.FindControl("AddressLine1TextBox"), TextBox).Text = obj.AddressLine1
            CType(EditFormView.FindControl("AddressLine2TextBox"), TextBox).Text = obj.AddressLine2
            CType(EditFormView.FindControl("CityTextBox"), TextBox).Text = obj.City
            CType(EditFormView.FindControl("StateTextBox"), TextBox).Text = obj.State
            CType(EditFormView.FindControl("ZipTextBox"), TextBox).Text = obj.Zip
            CType(EditFormView.FindControl("PhoneTextBox"), TextBox).Text = obj.Phone
            CType(EditFormView.FindControl("FaxTextBox"), TextBox).Text = obj.Fax
            CType(EditFormView.FindControl("FacilityTypeTextBox"), TextBox).Text = obj.FacilityType
            CType(EditFormView.FindControl("SMSIDTextBox"), TextBox).Text = obj.SMSID

            CType(EditFormView.FindControl("TransferFacilityCheckBox"), CheckBox).Checked = obj.TransferFacility
            CType(EditFormView.FindControl("OutpatientTherapyCheckBox"), CheckBox).Checked = obj.OutpatientTherapy
            CType(EditFormView.FindControl("AcuteFacilityCheckBox"), CheckBox).Checked = obj.AcuteFacility
            CType(EditFormView.FindControl("InactiveCheckBox"), CheckBox).Checked = obj.Inactive
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

    Protected Sub FacilityDataSource_SelectObject(ByVal sender As Object, ByVal e As Csla.Web.SelectObjectArgs) Handles FacilityDataSource.SelectObject
        e.BusinessObject = GetFacility()
    End Sub

    Protected Sub FacilityDataSource_InsertObject(ByVal sender As Object, ByVal e As Csla.Web.InsertObjectArgs) Handles FacilityDataSource.InsertObject
        Dim obj As TransferFacility = GetFacility()
        Csla.Data.DataMapper.Map(e.Values, obj)
        SaveFacility(obj)
    End Sub

    Protected Sub FacilityDataSource_UpdateObject(ByVal sender As Object, ByVal e As Csla.Web.UpdateObjectArgs) Handles FacilityDataSource.UpdateObject
        Dim obj As TransferFacility = GetFacility()
        Csla.Data.DataMapper.Map(e.Values, obj, New String() {"ID", "LastModified"})
        SaveFacility(obj)
    End Sub

    Protected Sub FacilityDataSource_DeleteObject(ByVal sender As Object, ByVal e As Csla.Web.DeleteObjectArgs) Handles FacilityDataSource.DeleteObject
        Try
            TransferFacility.DeleteFacility(e.Keys("ID"))
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
