Imports ITWMaintenance.Library.Orders

Partial Class Orders_OrdersCatalog
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            If Not String.IsNullOrEmpty(Request("SearchText")) Then
                SearchTextBox.Text = Request("SearchText")
            End If

            Dim script As String

            script = "function Search() {"
            script &= "     document.getElementById('" & SearchButton.ClientID() & "').click(); "
            script &= "}"

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "search", script, True)

            OrdersCatalogGridView.DataBind()
        End If
    End Sub

    Protected Sub OrdersCatalogDataSource_SelectObject(sender As Object, e As Csla.Web.SelectObjectArgs) Handles OrdersCatalogDataSource.SelectObject
        e.BusinessObject = ReadOnlyOrderCatalogItemList.GetOrders(SearchTextBox.Text)
    End Sub

    Protected Sub OrdersCatalogGridView_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles OrdersCatalogGridView.RowCommand
        If e.CommandName = "EditItem" Then
            Response.Redirect("EditOrder.aspx?ID=" & e.CommandArgument & "&SearchText=" & SearchTextBox.Text)
        End If
    End Sub

    Protected Sub AddOrderButton_Click(sender As Object, e As System.EventArgs) Handles AddOrderButton.Click
        Response.Redirect("EditOrder.aspx?AddNew=true&SearchText=" & SearchTextBox.Text)
    End Sub

    Protected Sub SearchButton_Click(sender As Object, e As System.EventArgs) Handles SearchButton.Click
        OrdersCatalogGridView.DataBind()
    End Sub

    Protected Sub OrdersCatalogGridView_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles OrdersCatalogGridView.RowDataBound
        If e.Row.RowType = DataControlRowType.Pager Then
            ' Add some text before the page numbers
            Dim HeaderTextCell As New TableCell()
            HeaderTextCell.Text = "View Page:"
            HeaderTextCell.CssClass = "PagerHeader"
            CType(e.Row.Cells(0).Controls.Item(0), Table).Rows(0).Cells.AddAt(0, HeaderTextCell)

            ' Show how many pages were returned if there are more than 10
            If OrdersCatalogGridView.PageCount > OrdersCatalogGridView.PagerSettings.PageButtonCount Then
                Dim PageCountCell As New TableCell()
                PageCountCell.Text = " of " & OrdersCatalogGridView.PageCount.ToString()
                CType(e.Row.Cells(0).Controls.Item(0), Table).Rows(0).Cells.Add(PageCountCell)
            End If
        End If
    End Sub

End Class