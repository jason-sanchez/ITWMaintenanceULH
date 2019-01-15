Imports ITWMaintenance.Library.Orders
Imports ITWMaintenance.Library.Lookup
Imports Csla.Validation

Partial Class Orders_EditOrder
    Inherits System.Web.UI.Page

    Private Enum FacilityActionViews
        ListView = 0
        InsertView = 1
    End Enum

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("currentObject") = Nothing
            ApplyAuthorizationRules()
        Else
            Me.ErrorPanel.Visible = False
            Me.ErrorLabel.Text = ""
        End If

        Try
            Me.EditFormView.FindControl("AliasTextBox").Focus()
        Catch
        End Try
    End Sub

    Private Sub ApplyAuthorizationRules()
        Dim obj As OrderCatalogItem = GetOrder()

        If OrderCatalogItem.CanEditObject Then
            If obj.IsNew Then
                Me.HeaderLabel.Text = "Add New Order"
                Me.EditFormView.DefaultMode = DetailsViewMode.Insert
            Else
                Me.HeaderLabel.Text = "Edit Order"
                Me.EditFormView.DefaultMode = DetailsViewMode.Edit
            End If
        Else
            Me.HeaderLabel.Text = "View Order Information"
            Me.EditFormView.DefaultMode = DetailsViewMode.ReadOnly
        End If
    End Sub

    Private Sub GoBack()
        Response.Redirect("OrdersCatalog.aspx?SearchText=" & Request("SearchText"))
    End Sub

    Protected Sub BackButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        GoBack()
    End Sub

    Private Function GetOrder() As OrderCatalogItem
        Dim businessObject As Object = Session("currentObject")

        If IsNothing(businessObject) OrElse Not TypeOf businessObject Is OrderCatalogItem Then
            Try
                Dim idString As String = Request("ID")

                If Not String.IsNullOrEmpty(idString) Then
                    businessObject = OrderCatalogItem.GetOrderCatalogItem(idString)
                ElseIf Not String.IsNullOrEmpty(Request("addNew")) AndAlso CBool(Request("addNew")) Then
                    ' No ID was passed, but request("addNew") is "true", so add a new Item
                    businessObject = OrderCatalogItem.NewOrderCatalogItem()
                Else
                    ' Unable to load the specific Item, so
                    ' redirect back to the list.
                    GoBack()
                End If

                Session("CurrentObject") = businessObject
            Catch ex As System.Security.SecurityException
                ' TODO - Create the error page
                Response.Redirect("ErrorPage.aspx?ErrorText=Security Exception!&ReturnURL=Worklist.aspx&ErrorDetails=" & Server.UrlEncode(ex.Message))
            End Try
        End If

        Return CType(businessObject, OrderCatalogItem)
    End Function

    Private Sub SaveItem(ByVal theOrder As OrderCatalogItem)
        Try
            Session("CurrentObject") = theOrder.Save()
            GoBack()
        Catch ex As Csla.Validation.ValidationException
            Dim message As New System.Text.StringBuilder
            message.AppendFormat("{0}:<br/>", ex.Message)

            If theOrder.BrokenRulesCollection.Count = 1 Then
                message.AppendFormat("-{0}", theOrder.BrokenRulesCollection(0).Description)
            Else
                For Each rule As Csla.Validation.BrokenRule In _
                    theOrder.BrokenRulesCollection
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

#Region " EditFormView "

    Protected Sub EditFormView_DataBound(sender As Object, e As EventArgs) Handles EditFormView.DataBound
        Dim order As OrderCatalogItem = GetOrder()

        Try
            With DirectCast(EditFormView.FindControl("OrderGroupDropDownList"), DropDownList)
                If .Items.FindByValue(order.OrderGroup) Is Nothing Then
                    .Items.Add(New ListItem(order.OrderGroup, order.OrderGroup))
                End If
                .SelectedValue = order.OrderGroup
            End With
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub EditOrderDetailsView_ItemCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles EditFormView.ItemCreated
        Dim obj As OrderCatalogItem = GetOrder()

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
            DirectCast(EditFormView.FindControl("AliasTextBox"), TextBox).Text = obj.Alias
            DirectCast(EditFormView.FindControl("DescriptionTextBox"), TextBox).Text = obj.Description
            DirectCast(EditFormView.FindControl("OrderGroupTextBox"), TextBox).Text = obj.OrderGroup
            DirectCast(EditFormView.FindControl("LabCollectCheckBox"), CheckBox).Checked = obj.LabCollect
            DirectCast(EditFormView.FindControl("AutoExpandDetailsCheckBox"), CheckBox).Checked = obj.AutoExpandDetails

            ' Don't bind the checklist here, otherwise checklist items for the new item
            '  will not be saved.  If you must put this line back in, you will probably
            '  need to first call UpdateChecklist() to save what the user has entered
            '  (although I haven't tested to make sure that will work).
            'ChecklistDataList.DataBind()
        End If
    End Sub

    Protected Sub EditOrderDetailsView_ModeChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewModeEventArgs) Handles EditFormView.ModeChanging
        ' Redirect back to the list on Cancel
        If e.CancelingEdit Then
            GoBack()
        End If
    End Sub

#End Region

#Region " OrderDataSource "

    Protected Sub OrderDataSource_SelectObject(ByVal sender As Object, ByVal e As Csla.Web.SelectObjectArgs) Handles OrderDataSource.SelectObject
        e.BusinessObject = GetOrder()
    End Sub

    Protected Sub OrderDataSource_InsertObject(ByVal sender As Object, ByVal e As Csla.Web.InsertObjectArgs) Handles OrderDataSource.InsertObject
        Dim obj As OrderCatalogItem = GetOrder()
        Dim ignoreFields() As String = {"ID"}

        Csla.Data.DataMapper.Map(e.Values, obj, ignoreFields)

        ' Update the Order Group.
        ' Use the textbox if they have entered something there; otherwise use the dropdown.
        If Not String.IsNullOrEmpty(DirectCast(EditFormView.FindControl("OrderGroupTextBox"), TextBox).Text) Then
            obj.OrderGroup = DirectCast(EditFormView.FindControl("OrderGroupTextBox"), TextBox).Text

            ' Since the user entered a new group, we need to rebuild the list of groups
            OrderGroups.InvalidateCache()
        Else
            obj.OrderGroup = DirectCast(EditFormView.FindControl("OrderGroupDropDownList"), DropDownList).SelectedValue
        End If

        ' Update the Facility Actions
        UpdateFacilityActions()

        SaveItem(obj)
    End Sub

    Protected Sub OrderDataSource_UpdateObject(ByVal sender As Object, ByVal e As Csla.Web.UpdateObjectArgs) Handles OrderDataSource.UpdateObject
        Dim obj As OrderCatalogItem = GetOrder()
        Dim ignoreFields() As String = {"ID"}

        Csla.Data.DataMapper.Map(e.Values, obj, ignoreFields)

        ' Update the Order Group since it's not data-bound.
        obj.OrderGroup = DirectCast(EditFormView.FindControl("OrderGroupDropDownList"), DropDownList).SelectedValue

        ' Update the Facility Actions
        UpdateFacilityActions()

        SaveItem(obj)
    End Sub

    Protected Sub OrderDataSource_DeleteObject(ByVal sender As Object, ByVal e As Csla.Web.DeleteObjectArgs) Handles OrderDataSource.DeleteObject
        Try
            OrderCatalogItem.DeleteOrderCatalogItem(e.Keys("ID"))
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

#Region " FacilityActionDataSource "

    Protected Sub FacilityActionsDataSource_SelectObject(sender As Object, e As Csla.Web.SelectObjectArgs) Handles FacilityActionsDataSource.SelectObject
        e.BusinessObject = GetOrder().OrderFacilityActionItems
    End Sub

#End Region

    Protected Sub OrderFacilityActionGridView_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow AndAlso Not IsNothing(e.Row.DataItem) Then
            ' Bind the discipline list
            With DirectCast(e.Row.FindControl("FacilityActionDisciplineDropDownList"), DropDownList)
                .DataSource = DisciplineList.GetAllDisciplines()
                .DataBind()

                ' Try to set the selected discipline. Add it if it's not in the list (ie, was removed)
                If .Items.FindByValue(DirectCast(e.Row.DataItem, OrderFacilityActionItem).Discipline) Is Nothing Then
                    If DirectCast(e.Row.DataItem, OrderFacilityActionItem).Discipline > 0 Then
                        .Items.Add(New ListItem(DirectCast(e.Row.DataItem, OrderFacilityActionItem).DisciplineDescription & " (inactive)", DirectCast(e.Row.DataItem, OrderFacilityActionItem).Discipline))
                    Else
                        .Items.Add(New ListItem(DirectCast(e.Row.DataItem, OrderFacilityActionItem).DisciplineDescription, DirectCast(e.Row.DataItem, OrderFacilityActionItem).Discipline))
                    End If
                End If

                .SelectedValue = DirectCast(e.Row.DataItem, OrderFacilityActionItem).Discipline
            End With
        End If
    End Sub

    Protected Sub OrderFacilityActionGridView_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        ' Remove the corresponding Facility Action record
        GetOrder().OrderFacilityActionItems.RemoveAt(e.RowIndex)
    End Sub

    Private Sub UpdateFacilityActions()
        Dim FacilityActionsGridView As GridView = DirectCast(EditFormView.FindControl("OrderFacilityActionGridView"), GridView)

        For i As Integer = 0 To FacilityActionsGridView.Rows.Count - 1
            GetOrder().OrderFacilityActionItems(i).IntakeFacility = DirectCast(FacilityActionsGridView.Rows(i).FindControl("FacilityActionIntakeFacilityDropDownList"), DropDownList).SelectedValue
            GetOrder().OrderFacilityActionItems(i).Discipline = DirectCast(FacilityActionsGridView.Rows(i).FindControl("FacilityActionDisciplineDropDownList"), DropDownList).SelectedValue
            GetOrder().OrderFacilityActionItems(i).ActionCode = DirectCast(FacilityActionsGridView.Rows(i).FindControl("ActionCodeTextBox"), TextBox).Text
        Next
    End Sub

    Protected Sub AddNewFacilityAction_Click(sender As Object, e As System.EventArgs)
        With DirectCast(EditFormView.FindControl("FacilityActionMultiView"), MultiView)
            .ActiveViewIndex = FacilityActionViews.InsertView
            With DirectCast(.FindControl("NewFacilityActionDetailsView"), DetailsView)
                .DefaultMode = DetailsViewMode.Insert
                DirectCast(.FindControl("NewFacilityActionIntakeFacilityDropDownList"), DropDownList).SelectedIndex = 0
                DirectCast(.FindControl("NewFacilityActionDisciplineDropDownList"), DropDownList).SelectedIndex = 0
                DirectCast(.FindControl("NewFacilityActionActionCodeTextBox"), TextBox).Text = ""
            End With
        End With
    End Sub

    Protected Sub NewFacilityActionDetailsView_DataBound(sender As Object, e As EventArgs)
        With DirectCast(EditFormView.FindControl("FacilityActionMultiView").FindControl("NewFacilityActionDetailsView"), DetailsView)
            ' Bind the discipline list
            With DirectCast(.FindControl("NewFacilityActionDisciplineDropDownList"), DropDownList)
                .DataSource = DisciplineList.GetAllDisciplines()
                .DataBind()
            End With
        End With
    End Sub

    Protected Sub NewFacilityActionDetailsView_ItemInserting(sender As Object, e As DetailsViewInsertEventArgs)
        ' Add the new item to the list
        ' The Discipline dropdown isn't data-bound, so we must manually update the value
        GetOrder().OrderFacilityActionItems.AddNew(e.Values("IntakeFacility"), DirectCast(EditFormView.FindControl("FacilityActionMultiView").FindControl("NewFacilityActionDetailsView").FindControl("NewFacilityActionDisciplineDropDownList"), DropDownList).SelectedValue, e.Values("ActionCode"))

        ' Re-bind the grid (so the new item will show)
        DirectCast(EditFormView.FindControl("OrderFacilityActionGridView"), GridView).DataBind()

        ' Change the view back over to the grid
        DirectCast(EditFormView.FindControl("FacilityActionMultiView"), MultiView).ActiveViewIndex = FacilityActionViews.ListView
    End Sub

End Class