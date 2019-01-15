Imports ITWMaintenance.Library.Evaluations.GlobalLookup
Imports Csla.Validation

Partial Class Evals_EvalGlobalLookupValues
    Inherits System.Web.UI.Page

    Private Enum Views
        ListView = 0
        InsertView = 1
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            Session("currentObject") = Nothing
            ApplyAuthorizationRules()

            SearchFieldDropDownList.DataSource = [Enum].GetNames(GetType(EvalGlobalLookupList.SearchField))
            SearchFieldDropDownList.DataBind()
        Else
            Me.ErrorLabel.Text = ""
            Me.ErrorPanel.Visible = False
        End If
    End Sub

    Private Sub ApplyAuthorizationRules()
        Me.EvalGlobalLookupListGridView.Columns(0).Visible = EvalGlobalLookupList.CanEditObject
        Me.NewPanel.Visible = EvalGlobalLookupList.CanAddObject
    End Sub

    Private Function GetEvalGlobalLookupList() As EvalGlobalLookupList
        Dim businessObject As Object = Session("CurrentObject")

        businessObject = EvalGlobalLookupList.GetEvalGlobalLookupList(SearchTextBox.Text, [Enum].Parse(GetType(EvalGlobalLookupList.SearchField), SearchFieldDropDownList.SelectedValue))
        Session("CurrentObject") = businessObject

        Return DirectCast(businessObject, EvalGlobalLookupList)
    End Function

    Protected Sub SearchButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchButton.Click
        EvalGlobalLookupListGridView.DataBind()
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

    Protected Sub InsertDetailsView_ItemCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles InsertDetailsView.ItemCreated
        ' Apply the Validation rules
        ' Set the MaxLength property on the fields based on the ValidationRules for the Object
        For Each ruleURI As String In GetEvalGlobalLookupList().GetChildRuleDescriptions()
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
        Me.EvalGlobalLookupListGridView.DataBind()
    End Sub

    Protected Sub InsertDetailsView_ModeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles InsertDetailsView.ModeChanged
        MultiView1.ActiveViewIndex = Views.ListView
    End Sub

#End Region

#Region " List GridView "

    Protected Sub EvalGlobalLookupListGridView_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles EvalGlobalLookupListGridView.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lookup As EvalGlobalLookup = CType(e.Row.DataItem, EvalGlobalLookup)

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
                ' Show the EPath instead of the FormNumber, and show the FieldLabel instead of the FieldNumber
                Try
                    If Not String.IsNullOrEmpty(lookup.EPath) Then
                        CType(e.Row.FindControl("FormNumberLabel"), Label).Text = lookup.EPath
                        CType(e.Row.FindControl("FormFieldLabel"), Label).Text = lookup.FieldLabel
                    End If
                Catch ex As Exception
                End Try
            End If
        End If
    End Sub

#End Region

#Region " EvalGlobalLookupListDataSource "

    Protected Sub EvalGlobalLookupListDataSource_DeleteObject(ByVal sender As Object, ByVal e As Csla.Web.DeleteObjectArgs) Handles EvalGlobalLookupListDataSource.DeleteObject
        Try
            Dim obj As EvalGlobalLookupList = GetEvalGlobalLookupList()
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

    Protected Sub EvalGlobalLookupListDataSource_InsertObject(ByVal sender As Object, ByVal e As Csla.Web.InsertObjectArgs) Handles EvalGlobalLookupListDataSource.InsertObject
        Try
            Dim obj As EvalGlobalLookupList = GetEvalGlobalLookupList()
            Dim lookup As EvalGlobalLookup = obj.AddNew

            Csla.Data.DataMapper.Map(e.Values, lookup)

            'Session("currentObject") = obj.Save()
            ' or (the second method re-orders the list)
            obj.Save()
            Session("currentObject") = Nothing
            Session("currentObject") = GetEvalGlobalLookupList()

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

    Protected Sub EvalGlobalLookupListDataSource_SelectObject(ByVal sender As Object, ByVal e As Csla.Web.SelectObjectArgs) Handles EvalGlobalLookupListDataSource.SelectObject
        e.BusinessObject = GetEvalGlobalLookupList()
    End Sub

    Protected Sub EvalGlobalLookupListDataSource_UpdateObject(ByVal sender As Object, ByVal e As Csla.Web.UpdateObjectArgs) Handles EvalGlobalLookupListDataSource.UpdateObject
        Try
            Dim obj As EvalGlobalLookupList = GetEvalGlobalLookupList()
            Dim lookup As EvalGlobalLookup = obj.GetEvalGlobalLookupByID(CInt(e.Keys.Item("ID")))

            Csla.Data.DataMapper.Map(e.Values, lookup)

            'Session("currentObject") = obj.Save()
            ' or (the second method re-orders the list)
            obj.Save()
            Session("currentObject") = Nothing
            Session("currentObject") = GetEvalGlobalLookupList()

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
