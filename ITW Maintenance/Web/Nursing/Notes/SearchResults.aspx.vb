Imports ITWMaintenance.Library.Nursing.Notes

Partial Class Nursing_Notes_SearchResults
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            If Not String.IsNullOrEmpty(Request("SearchText")) Then
                SearchTextBox.Text = Request("SearchText")
            End If

            SortByDropDownList.DataSource = [Enum].GetNames(GetType(ReadOnlyNursingNoteSearchResultList.SortField))
            SortByDropDownList.DataBind()
        End If

        If Not String.IsNullOrEmpty(SearchTextBox.Text) Then
            SearchResultDataSource.SelectParameters.Clear()
            Try
                SearchResultDataSource.SelectParameters.Add("FormID", CInt(SearchTextBox.Text))
            Catch ex As Exception
                ' The SearchText is not a number, so do a text search
                SearchResultDataSource.SelectParameters.Add("SearchText", SearchTextBox.Text)
            End Try

            SearchResultDataSource.SelectParameters.Add("SortBy", [Enum].Parse(GetType(ReadOnlyNursingNoteSearchResultList.SortField), SortByDropDownList.SelectedValue))
            SearchResultDataSource.SelectParameters.Add("OnlySearchForms", False)
            SearchResultsGridView.DataBind()
        Else
            SearchResultDataSource.SelectParameters.Add("FormID", -1)
        End If
    End Sub

    Protected Sub SearchResultsGridView_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchResultsGridView.DataBound
        If SearchResultsGridView.Rows.Count = 1 Then
            LoadRow(0)
        End If
    End Sub

    Protected Sub SearchResultsGridView_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles SearchResultsGridView.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Try
                Dim result As ReadOnlyNursingNoteSearchResult = DirectCast(e.Row.DataItem, ReadOnlyNursingNoteSearchResult)

                If result.IsForm Then
                    CType(e.Row.FindControl("IconImage"), Image).ImageUrl = "~/Images/icodoc4.gif"
                Else
                    CType(e.Row.FindControl("IconImage"), Image).ImageUrl = "~/Images/folderclosed.gif"
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Protected Sub SearchResultsGridView_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles SearchResultsGridView.SelectedIndexChanging
        LoadRow(e.NewSelectedIndex)
    End Sub

    Private Sub LoadRow(ByVal Index As Integer)
        Dim ID As Integer = Me.SearchResultsGridView.DataKeys(Index)(0)
        Response.Redirect("NursingNote_Landing.aspx?ShowNursingNoteID=" & ID)
    End Sub

End Class
