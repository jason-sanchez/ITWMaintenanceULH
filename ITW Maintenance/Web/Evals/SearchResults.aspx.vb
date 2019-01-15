Imports ITWMaintenance.Library.Evaluations

Partial Class Evals_SearchResults
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            SearchTextTextBox.Text = Request("SearchText")

            SortByDropDownList.DataSource = [Enum].GetNames(GetType(ReadOnlyEvalSearchResultList.SortField))
            SortByDropDownList.DataBind()
        End If

        SearchResultDataSource.SelectParameters.Clear()

        If Not String.IsNullOrEmpty(SearchTextTextBox.Text) Then
            Try
                SearchResultDataSource.SelectParameters.Add("EvalID", CInt(SearchTextTextBox.Text))
            Catch ex As Exception
                ' The SearchText is not a number, so do a text search
                SearchResultDataSource.SelectParameters.Add("SearchText", SearchTextTextBox.Text)
            End Try
        Else
            SearchResultDataSource.SelectParameters.Add("EvalID", -1)
        End If

        SearchResultDataSource.SelectParameters.Add("SortBy", [Enum].Parse(GetType(ReadOnlyEvalSearchResultList.SortField), SortByDropDownList.SelectedValue))

        SearchResultsGridView.DataBind()
    End Sub

    Protected Sub SearchResultsGridView_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchResultsGridView.DataBound
        If SearchResultsGridView.Rows.Count = 1 Then
            LoadRow(0)
        End If
    End Sub

    Protected Sub SearchResultsGridView_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles SearchResultsGridView.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Try
                Dim result As ReadOnlyEvalSearchResult = DirectCast(e.Row.DataItem, ReadOnlyEvalSearchResult)

                If result.Form Then
                    CType(e.Row.FindControl("EvalIconImage"), Image).ImageUrl = "~/Images/icodoc4.gif"
                Else
                    CType(e.Row.FindControl("EvalIconImage"), Image).ImageUrl = "~/Images/folderclosed.gif"
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Protected Sub SearchResultsGridView_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles SearchResultsGridView.SelectedIndexChanging
        LoadRow(e.NewSelectedIndex)
    End Sub

    Private Sub LoadRow(ByVal Index As Integer)
        Dim evalID As Integer = Me.SearchResultsGridView.DataKeys(Index)(0)
        Dim level2 As Integer = Me.SearchResultsGridView.DataKeys(Index)(1)
        Dim activeOnly As Boolean = True

        Try
            ' Active Only is the opposite of the inactive checkbox's value
            activeOnly = Not CType(Me.SearchResultsGridView.Rows(Index).Cells(4).Controls(0), CheckBox).Checked
        Catch ex As Exception
        End Try

        ' So we don't get any errors, we need to set the level1
        ' and level2 session variable before loading the eval.
        Session("Level1ID") = ReadOnlyEvaluation.GetEvalInfo(level2).ParentID
        Session("Level2EvalID") = level2

        Response.Redirect("Eval_Landing.aspx?EvalID=" & level2 & "&ShowEvalID=" & evalID & "&EvalActiveOnly=" & activeOnly)
    End Sub

End Class
