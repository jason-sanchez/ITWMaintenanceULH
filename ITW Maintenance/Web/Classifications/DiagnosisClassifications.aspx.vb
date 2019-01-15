Imports ITWMaintenance.Library.Classifications
Imports Csla.Validation

Partial Class Classifications_DiagnosisClassifications
    Inherits System.Web.UI.Page

    Private Enum Views
        ListView = 0
        InsertView = 1
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            Session("currentObject") = Nothing
            ApplyAuthorizationRules()
        Else
            Me.ErrorLabel.Text = ""
            Me.ErrorPanel.Visible = False
        End If
    End Sub

    Private Sub ApplyAuthorizationRules()
        Me.DiagnosisClassificationListGridView.Columns(0).Visible = DiagnosisClassificationList.CanEditObject
        Me.NewPanel.Visible = DiagnosisClassificationList.CanAddObject
    End Sub

    Private Function GetDiagnosisClassificationList() As DiagnosisClassificationList
        Dim businessObject As Object = Session("CurrentObject")

        If IsNothing(businessObject) OrElse Not TypeOf businessObject Is DiagnosisClassificationList Then
            businessObject = DiagnosisClassificationList.GetList(ActiveOnlyCheckBox.Checked)
            Session("CurrentObject") = businessObject
        End If

        Return DirectCast(businessObject, DiagnosisClassificationList)
    End Function

    Protected Sub ActiveOnlyCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ActiveOnlyCheckBox.CheckedChanged
        ' Force the list to refresh
        Session("CurrentObject") = Nothing
        Me.DiagnosisClassificationListGridView.DataBind()
    End Sub

#Region " Add New Form "

    Protected Sub NewButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NewButton.Click
        ' Must rebind the details view to force it to refresh the Category list...
        Me.InsertDetailsView.DataBind()
        Me.InsertDetailsView.DefaultMode = DetailsViewMode.Insert
        MultiView1.ActiveViewIndex = Views.InsertView

        ' Pre-Populate the DisplayOrder and clear the form
        CType(InsertDetailsView.FindControl("CategoryDropDownList"), DropDownList).SelectedIndex = 0
        CType(InsertDetailsView.FindControl("CategoryTextBox"), TextBox).Text = ""
        CType(InsertDetailsView.FindControl("DescriptionTextBox"), TextBox).Text = ""
        CType(InsertDetailsView.FindControl("InactiveCheckbox"), CheckBox).Checked = False
        InsertDetailsView.Focus()
    End Sub

    Protected Sub InsertDetailsView_ItemCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles InsertDetailsView.ItemCreated
        ' Apply the Validation rules
        ' Set the MaxLength property on the fields based on the ValidationRules for the Object
        For Each ruleURI As String In GetDiagnosisClassificationList().GetChildRuleDescriptions()
            Dim ruleDesc As RuleDescription = RuleDescription.Parse(ruleURI)

            Try
                If Not String.IsNullOrEmpty(ruleDesc.Arguments("MaxLength")) Then
                    CType(Me.InsertDetailsView.FindControl(ruleDesc.PropertyName & "TextBox"), TextBox).MaxLength = ruleDesc.Arguments("MaxLength")
                End If
            Catch ex As Exception
            End Try
        Next
    End Sub

    Protected Sub InsertDetailsView_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewInsertedEventArgs) Handles InsertDetailsView.ItemInserted
        MultiView1.ActiveViewIndex = Views.ListView
        Me.DiagnosisClassificationListGridView.DataBind()
    End Sub

    Protected Sub InsertDetailsView_ModeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles InsertDetailsView.ModeChanged
        MultiView1.ActiveViewIndex = Views.ListView
    End Sub

#End Region

#Region " List GridView "

    Protected Sub DiagnosisClassificationListGridView_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DiagnosisClassificationListGridView.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow AndAlso e.Row.RowState = DataControlRowState.Edit Then
            ' Apply the Validation rules
            ' Set the MaxLength property on the fields based on the ValidationRules for the Object
            For Each ruleURI As String In CType(e.Row.DataItem, DiagnosisClassification).GetRuleDescriptions()
                Dim ruleDesc As RuleDescription = RuleDescription.Parse(ruleURI)

                Try
                    If Not String.IsNullOrEmpty(ruleDesc.Arguments("MaxLength")) Then
                        CType(e.Row.FindControl(ruleDesc.PropertyName & "TextBox"), TextBox).MaxLength = ruleDesc.Arguments("MaxLength")
                    End If
                Catch ex As Exception
                End Try
            Next
        End If
    End Sub

#End Region

#Region " ItemStatusListDataSource "

    Protected Sub DiagnosisClassificationListDataSource_DeleteObject(ByVal sender As Object, ByVal e As Csla.Web.DeleteObjectArgs) Handles DiagnosisClassificationListDataSource.DeleteObject
        Try
            Dim obj As DiagnosisClassificationList = GetDiagnosisClassificationList()
            Dim id As Integer = CInt(e.Keys.Item("Id"))

            obj.Remove(id)
            Session("currentObject") = obj.Save
            e.RowsAffected = 1
        Catch ex As Csla.DataPortalException
            Me.ErrorLabel.Text = ex.BusinessException.Message
            Me.ErrorPanel.Visible = True
            e.RowsAffected = 0
        Catch ex As Exception
            Me.ErrorLabel.Text = ex.Message
            Me.ErrorPanel.Visible = True
            e.RowsAffected = 0
        End Try
    End Sub

    Protected Sub DiagnosisClassificationListDataSource_InsertObject(ByVal sender As Object, ByVal e As Csla.Web.InsertObjectArgs) Handles DiagnosisClassificationListDataSource.InsertObject
        Try
            Dim obj As DiagnosisClassificationList = GetDiagnosisClassificationList()
            Dim newClassification As DiagnosisClassification = obj.AddNew()
            Dim refreshCategoryList As Boolean = False

            If Not String.IsNullOrEmpty(CType(InsertDetailsView.FindControl("CategoryTextBox"), TextBox).Text) Then
                newClassification.Category = CType(InsertDetailsView.FindControl("CategoryTextBox"), TextBox).Text
                refreshCategoryList = True
            Else
                newClassification.Category = CType(InsertDetailsView.FindControl("CategoryDropDownList"), DropDownList).SelectedValue
            End If

            newClassification.Description = e.Values.Item("Description").ToString
            newClassification.Inactive = e.Values.Item("Inactive")

            'Csla.Data.DataMapper.Map(e.Values, newClassification)

            'Session("currentObject") = obj.Save()
            ' or (the second method re-orders the list)
            obj.Save()
            Session("currentObject") = Nothing
            Session("currentObject") = GetDiagnosisClassificationList()

            If refreshCategoryList Then
                DiagnosisClassificationCategoryList.InvalidateCache()
            End If

            e.RowsAffected = 1
        Catch ex As Csla.DataPortalException
            Me.ErrorLabel.Text = ex.BusinessException.Message
            Me.ErrorPanel.Visible = True
            e.RowsAffected = 0
        Catch ex As Exception
            Me.ErrorLabel.Text = ex.Message
            Me.ErrorPanel.Visible = True
            e.RowsAffected = 0
        End Try
    End Sub

    Protected Sub DiagnosisClassificationListDataSource_SelectObject(ByVal sender As Object, ByVal e As Csla.Web.SelectObjectArgs) Handles DiagnosisClassificationListDataSource.SelectObject
        e.BusinessObject = GetDiagnosisClassificationList()
    End Sub

    Protected Sub DiagnosisClassificationListDataSource_UpdateObject(ByVal sender As Object, ByVal e As Csla.Web.UpdateObjectArgs) Handles DiagnosisClassificationListDataSource.UpdateObject
        Try
            Dim obj As DiagnosisClassificationList = GetDiagnosisClassificationList()
            Dim classification As DiagnosisClassification = obj.GetDiagnosisClassificationByID(CInt(e.Keys.Item("ID")))
            Dim refreshCategoryList As Boolean = False
            Dim editRow As GridViewRow = DiagnosisClassificationListGridView.Rows(DiagnosisClassificationListGridView.EditIndex)

            If Not String.IsNullOrEmpty(CType(editRow.FindControl("CategoryTextBox"), TextBox).Text) Then
                classification.Category = CType(editRow.FindControl("CategoryTextBox"), TextBox).Text
                refreshCategoryList = True
            Else
                classification.Category = CType(editRow.FindControl("CategoryDropDownList"), DropDownList).SelectedValue
            End If

            classification.Description = e.Values.Item("Description").ToString
            classification.Inactive = e.Values.Item("Inactive")

            'Session("currentObject") = obj.Save()
            ' or (the second method re-orders the list)
            obj.Save()
            Session("currentObject") = Nothing
            Session("currentObject") = GetDiagnosisClassificationList()

            If refreshCategoryList Then
                DiagnosisClassificationCategoryList.InvalidateCache()
            End If

            e.RowsAffected = 1
        Catch ex As Csla.DataPortalException
            Me.ErrorLabel.Text = ex.BusinessException.Message
            Me.ErrorPanel.Visible = True
            e.RowsAffected = 0
        Catch ex As Exception
            Me.ErrorLabel.Text = ex.Message
            Me.ErrorPanel.Visible = True
            e.RowsAffected = 0
        End Try
    End Sub

#End Region

#Region " Category Data Source "

    Protected Sub CategoryDataSource_SelectObject(ByVal sender As Object, ByVal e As Csla.Web.SelectObjectArgs) Handles CategoryDataSource.SelectObject
        e.BusinessObject = DiagnosisClassificationCategoryList.GetCategories()
    End Sub

#End Region

End Class
