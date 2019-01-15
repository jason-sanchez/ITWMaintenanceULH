Imports ITWMaintenance.Library.Nursing.Diagnoses
Imports Csla.Validation

Partial Class Nursing_Diagnosis_Edit
    Inherits System.Web.UI.Page

#Region " Helper Variables "

    Private _LinkFormMultiView As MultiView
    Private ReadOnly Property LinkFormMultiView() As MultiView
        Get
            If IsNothing(_LinkFormMultiView) Then
                _LinkFormMultiView = EditForm.FindControl("LinkFormMultiView")
            End If
            Return _LinkFormMultiView
        End Get
    End Property

    Private Enum LinkFormViews
        ListView = 0
        LinkNewFormView = 1
    End Enum

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("currentObject") = Nothing
            ApplyAuthorizationRules()
        Else
            Me.ErrorPanel.Visible = False
            Me.ErrorLabel.Text = ""
        End If

        Try
            Me.EditForm.FindControl("DescriptionTextBox").Focus()
        Catch ex As Exception
            Me.ErrorPanel.Visible = True
            Me.ErrorLabel.Text = Replace(ex.ToString(), vbCrLf, "<br />")
        End Try
    End Sub

    Private Sub ApplyAuthorizationRules()
        If Diagnosis.CanEditObject Then
            Dim obj As Diagnosis = GetDiagnosis()

            If obj.IsNew Then
                Me.HeaderLabel.Text = "Add New Diagnosis"
                Me.EditForm.DefaultMode = DetailsViewMode.Insert
            Else
                Me.HeaderLabel.Text = "Edit Diagnosis"
                Me.EditForm.DefaultMode = DetailsViewMode.Edit
            End If
        Else
            Me.EditForm.DefaultMode = DetailsViewMode.ReadOnly
        End If
    End Sub

    Protected Sub EditForm_ItemCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles EditForm.ItemCreated
        Dim obj As Diagnosis = GetDiagnosis()

        If EditForm.DefaultMode = FormViewMode.Edit OrElse EditForm.DefaultMode = FormViewMode.Insert Then
            ' Set the MaxLength property on the fields based on the ValidationRules for the Object
            For Each ruleURI As String In obj.GetRuleDescriptions()
                Dim ruleDesc As RuleDescription = RuleDescription.Parse(ruleURI)

                Try
                    If Not String.IsNullOrEmpty(ruleDesc.Arguments("MaxLength")) Then
                        CType(Me.EditForm.FindControl(ruleDesc.PropertyName & "TextBox"), TextBox).MaxLength = ruleDesc.Arguments("MaxLength")
                    End If
                Catch ex As Exception
                End Try
            Next
        End If

        If EditForm.DefaultMode = DetailsViewMode.Insert Then
            CType(EditForm.FindControl("DescriptionTextBox"), TextBox).Text = obj.Description
            CType(EditForm.FindControl("InactiveCheckBox"), CheckBox).Checked = obj.Inactive
        End If
    End Sub

    ' Redirect back to the list on Cancel
    Protected Sub EditForm_ModeChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewModeEventArgs) Handles EditForm.ModeChanging
        If e.CancelingEdit OrElse e.Cancel Then
            GoBack()
        End If
    End Sub

    Private Sub GoBack()
        Response.Redirect("Diagnosis_List.aspx?ActiveOnly=" & Request("ActiveOnly"))
    End Sub

    Private Function GetDiagnosis() As Diagnosis
        Dim businessObject As Object = Session("currentObject")

        If IsNothing(businessObject) OrElse Not TypeOf businessObject Is Diagnosis Then
            Try
                Dim idString As String = Request("ID")

                If Not String.IsNullOrEmpty(idString) Then
                    businessObject = Diagnosis.GetDiagnosis(idString)
                Else
                    businessObject = Diagnosis.NewDiagnosis()
                End If

                Session("currentObject") = businessObject
            Catch ex As System.Security.SecurityException
                ' TODO - Create the Error page
                GoBack()
            End Try
        End If

        Return CType(businessObject, Diagnosis)
    End Function

    Private Sub SaveDiagnosis(ByVal theDiagnosis As Diagnosis)
        Try
            Session("currentObject") = theDiagnosis.Save()
            GoBack()
        Catch ex As Csla.Validation.ValidationException
            Dim message As New System.Text.StringBuilder
            message.AppendFormat("{0}:<br/>", ex.Message)

            If theDiagnosis.BrokenRulesCollection.Count = 1 Then
                message.AppendFormat("-{0}", theDiagnosis.BrokenRulesCollection(0).Description)
            Else
                For Each rule As Csla.Validation.BrokenRule In _
                    theDiagnosis.BrokenRulesCollection
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

#Region " DiagnosisDataSource "

    Protected Sub DiagnosisDataSource_SelectObject(ByVal sender As Object, ByVal e As Csla.Web.SelectObjectArgs) Handles DiagnosisDataSource.SelectObject
        e.BusinessObject = GetDiagnosis()
    End Sub

    Protected Sub DiagnosisDataSource_InsertObject(ByVal sender As Object, ByVal e As Csla.Web.InsertObjectArgs) Handles DiagnosisDataSource.InsertObject
        Dim obj As Diagnosis = GetDiagnosis()
        Csla.Data.DataMapper.Map(e.Values, obj)
        SaveDiagnosis(obj)
    End Sub

    Protected Sub DiagnosisDataSource_UpdateObject(ByVal sender As Object, ByVal e As Csla.Web.UpdateObjectArgs) Handles DiagnosisDataSource.UpdateObject
        Dim obj As Diagnosis = GetDiagnosis()
        Csla.Data.DataMapper.Map(e.Values, obj, "ID")
        SaveDiagnosis(obj)
    End Sub

    Protected Sub DiagnosisDataSource_DeleteObject(ByVal sender As Object, ByVal e As Csla.Web.DeleteObjectArgs) Handles DiagnosisDataSource.DeleteObject
        Try
            Diagnosis.DeleteDiagnosis(e.Keys("ID"))
            Session("currentObject") = Nothing
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

    Protected Sub LinkedFormsDataSource_SelectObject(ByVal sender As Object, ByVal e As Csla.Web.SelectObjectArgs) Handles LinkedFormsDataSource.SelectObject
        e.BusinessObject = DirectCast(GetDiagnosis(), Diagnosis).LinkedForms
    End Sub

    Protected Sub LinkNewFormButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ToggleLinkFormView()
    End Sub

    Protected Sub LinkedFormsGridView_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs)
        DirectCast(GetDiagnosis(), Diagnosis).LinkedForms.RemoveAt(e.RowIndex)
    End Sub

    Private Sub ToggleLinkFormView()
        If LinkFormMultiView.ActiveViewIndex = LinkFormViews.ListView Then
            LinkFormMultiView.ActiveViewIndex = LinkFormViews.LinkNewFormView
            CType(Me.EditForm.FindControl("LinkNewFormButton"), LinkButton).Text = "Cancel"
            CType(Me.EditForm.FindControl("NursingFormPicker1").FindControl("SearchTextBox"), TextBox).Focus()
        Else
            LinkFormMultiView.ActiveViewIndex = LinkFormViews.ListView
            CType(Me.EditForm.FindControl("LinkNewFormButton"), LinkButton).Text = "Link Form..."
        End If
    End Sub

    Protected Sub NursingFormPicker1_FormSelect(ByVal SelectedFormID As Integer)
        ' Add the selected form to the LinkedForms collection
        DirectCast(GetDiagnosis(), Diagnosis).LinkedForms.AddNew(SelectedFormID)
        DirectCast(EditForm.FindControl("LinkedFormsGridView"), GridView).DataBind()
        ToggleLinkFormView()
    End Sub

End Class
