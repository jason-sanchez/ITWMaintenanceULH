Imports ITWMaintenance.Library.Nursing.GlobalLookup
Imports Csla.Validation

Partial Class Nursing_NursingNoteGlobalLookupValues
    Inherits System.Web.UI.Page

    Private Enum Views
        ListView = 0
        InsertView = 1
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            Session("currentObject") = Nothing
            ApplyAuthorizationRules()

            SearchFieldDropDownList.DataSource = [Enum].GetNames(GetType(NursingNoteGlobalLookupList.SearchField))
            SearchFieldDropDownList.DataBind()
        Else
            Me.ErrorLabel.Text = ""
            Me.ErrorPanel.Visible = False
        End If
    End Sub

    Private Sub ApplyAuthorizationRules()
        Me.NursingNoteGlobalLookupListGridView.Columns(0).Visible = NursingNoteGlobalLookupList.CanEditObject
        Me.NewPanel.Visible = NursingNoteGlobalLookupList.CanAddObject
    End Sub

    Private Function GetNursingNoteGlobalLookupList() As NursingNoteGlobalLookupList
        Dim businessObject As Object = Session("CurrentObject")

        businessObject = NursingNoteGlobalLookupList.GetGlobalLookupList(SearchTextBox.Text, [Enum].Parse(GetType(NursingNoteGlobalLookupList.SearchField), SearchFieldDropDownList.SelectedValue))
        Session("CurrentObject") = businessObject

        Return DirectCast(businessObject, NursingNoteGlobalLookupList)
    End Function

    Protected Sub SearchButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchButton.Click
        NursingNoteGlobalLookupListGridView.DataBind()
    End Sub

#Region " Add New Form "

    Protected Sub NewButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NewButton.Click
        Me.InsertDetailsView.DefaultMode = DetailsViewMode.Insert
        MultiView1.ActiveViewIndex = Views.InsertView

        ' Clear the form
        CType(InsertDetailsView.FindControl("FormNumberTextBox"), TextBox).Text = ""
        CType(InsertDetailsView.FindControl("FormFieldDropDownList"), DropDownList).SelectedIndex = 0
        CType(InsertDetailsView.FindControl("FormNumberTextBox"), TextBox).Focus()
    End Sub

    Protected Sub InsertDetailsView_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles InsertDetailsView.DataBound
        ' Add the "Select..." item to the Description dropdown
        DirectCast(InsertDetailsView.FindControl("DescriptionDropDown"), DropDownList).Items.Insert(0, "Select...")
    End Sub

    Protected Sub InsertDetailsView_ItemCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles InsertDetailsView.ItemCreated
        ' Apply the Validation rules
        ' Set the MaxLength property on the fields based on the ValidationRules for the Object
        For Each ruleURI As String In GetNursingNoteGlobalLookupList().GetChildRuleDescriptions()
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
        Me.NursingNoteGlobalLookupListGridView.DataBind()
    End Sub

    Protected Sub InsertDetailsView_ModeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles InsertDetailsView.ModeChanged
        MultiView1.ActiveViewIndex = Views.ListView
    End Sub

#End Region

#Region " List GridView "

    Protected Sub NursingNoteGlobalLookupListGridView_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles NursingNoteGlobalLookupListGridView.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lookup As NursingNoteGlobalLookup = CType(e.Row.DataItem, NursingNoteGlobalLookup)

            If e.Row.RowState = DataControlRowState.Edit Then
                ' Apply the Validation rules
                ' Set the MaxLength property on the fields based on the ValidationRules for the Object
                For Each ruleURI As String In lookup.GetRuleDescriptions()
                    Dim ruleDesc As RuleDescription = RuleDescription.Parse(ruleURI)

                    Try
                        If Not String.IsNullOrEmpty(ruleDesc.Arguments("MaxLength")) Then
                            CType(e.Row.FindControl(ruleDesc.PropertyName & "TextBox"), TextBox).MaxLength = ruleDesc.Arguments("MaxLength")
                        End If
                    Catch ex As Exception
                    End Try
                Next
            Else
                ' Show the Path instead of the FormNumber, and show the FieldLabel instead of the FieldNumber
                Try
                    If Not String.IsNullOrEmpty(lookup.Path) Then
                        CType(e.Row.FindControl("FormNumberLabel"), Label).Text = lookup.Path
                        CType(e.Row.FindControl("FormFieldLabel"), Label).Text = lookup.FieldLabel
                    End If
                Catch ex As Exception
                End Try
            End If
        End If
    End Sub

#End Region

#Region " NursingNoteGlobalLookupListDataSource "

    Protected Sub NursingNoteGlobalLookupListDataSource_DeleteObject(ByVal sender As Object, ByVal e As Csla.Web.DeleteObjectArgs) Handles NursingNoteGlobalLookupListDataSource.DeleteObject
        Try
            Dim obj As NursingNoteGlobalLookupList = GetNursingNoteGlobalLookupList()
            Dim id As Integer = CInt(e.Keys.Item("ID"))

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

    Protected Sub NursingNoteGlobalLookupListDataSource_InsertObject(ByVal sender As Object, ByVal e As Csla.Web.InsertObjectArgs) Handles NursingNoteGlobalLookupListDataSource.InsertObject
        Try
            Dim obj As NursingNoteGlobalLookupList = GetNursingNoteGlobalLookupList()
            Dim lookup As NursingNoteGlobalLookup = obj.AddNew

            'Csla.Data.DataMapper.Map(e.Values, lookup)
            If DirectCast(InsertDetailsView.FindControl("DescriptionDropDown"), DropDownList).SelectedIndex > 0 Then
                lookup.Description = DirectCast(InsertDetailsView.FindControl("DescriptionDropDown"), DropDownList).SelectedValue
            Else
                lookup.Description = DirectCast(InsertDetailsView.FindControl("DescriptionTextBox"), TextBox).Text
            End If
            lookup.FormNumber = DirectCast(InsertDetailsView.FindControl("FormNumberTextBox"), TextBox).Text
            lookup.FormField = DirectCast(InsertDetailsView.FindControl("FormFieldDropDownList"), DropDownList).SelectedValue

            'Session("currentObject") = obj.Save()
            ' or (the second method re-orders the list)
            obj.Save()
            Session("currentObject") = Nothing
            Session("currentObject") = GetNursingNoteGlobalLookupList()

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

    Protected Sub NursingNoteGlobalLookupListDataSource_SelectObject(ByVal sender As Object, ByVal e As Csla.Web.SelectObjectArgs) Handles NursingNoteGlobalLookupListDataSource.SelectObject
        e.BusinessObject = GetNursingNoteGlobalLookupList()
    End Sub

    Protected Sub NursingNoteGlobalLookupListDataSource_UpdateObject(ByVal sender As Object, ByVal e As Csla.Web.UpdateObjectArgs) Handles NursingNoteGlobalLookupListDataSource.UpdateObject
        Try
            Dim obj As NursingNoteGlobalLookupList = GetNursingNoteGlobalLookupList()
            Dim lookup As NursingNoteGlobalLookup = obj.GetGlobalLookupByID(CInt(e.Keys.Item("ID")))

            Csla.Data.DataMapper.Map(e.Values, lookup)

            'Session("currentObject") = obj.Save()
            ' or (the second method re-orders the list)
            obj.Save()
            Session("currentObject") = Nothing
            Session("currentObject") = GetNursingNoteGlobalLookupList()

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

End Class
