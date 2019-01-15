Imports ITWMaintenance.Library.Evaluations

Partial Class Evals_Evals
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ' Set the level 1 value here so we can return to the eval list
            ' without having to go all the way back up.
            If Not String.IsNullOrEmpty(Request("ParentID")) Then
                Session("Level1ID") = Request("ParentID")
            End If
            Session("Level2EvalID") = Nothing

            If Not Evaluation.CanAddObject Then
                btnAddNew.Visible = False
            End If

            Try
                If String.IsNullOrEmpty(Request("ActiveOnly")) Then
                    Me.ActiveOnlyCheckBox.Checked = True
                Else
                    Me.ActiveOnlyCheckBox.Checked = Convert.ToBoolean(Request("ActiveOnly"))
                End If
            Catch ex As Exception
                Me.ActiveOnlyCheckBox.Checked = True
            End Try

            Me.ParentNameLabel.Text = ReadOnlyEvalLevel1Folder.GetEvalLevel1FolderInfo(Session("Level1ID")).FolderName
        End If
    End Sub

    Protected Sub EvaluationsGrid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles EvaluationsGrid.RowCommand
        If e.CommandName = "EditItem" Then
            Response.Redirect("Eval_Edit.aspx?EvalID=" & e.CommandArgument & "&ParentID=" & Session("Level1ID") & "&ActiveOnly=" & Me.ActiveOnlyCheckBox.Checked)
        End If
    End Sub

    Protected Sub EvaluationsGrid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles EvaluationsGrid.RowDataBound
        If e.Row.RowState = DataControlRowState.Normal OrElse e.Row.RowState = DataControlRowState.Alternate Then
            If Not e.Row.DataItem Is Nothing Then
                Dim theEval As ReadOnlyEvaluation = CType(e.Row.DataItem, ReadOnlyEvaluation)

                If Not Evaluation.CanEditObject Then
                    DirectCast(e.Row.FindControl("btnEdit"), Button).Text = "View"
                End If

                DirectCast(e.Row.FindControl("InactiveCheckBox"), CheckBox).Checked = theEval.Inactive
                DirectCast(e.Row.FindControl("OneTimeCheckBox"), CheckBox).Checked = theEval.OneTime
                DirectCast(e.Row.FindControl("PostDischargeCheckBox"), CheckBox).Checked = theEval.PostDischarge

                e.Row.Attributes.Add("onclick", "javascript:loadEval('" & theEval.EvalID.ToString() & "');")
            End If
        End If
    End Sub

    Protected Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Response.Redirect("Eval_Edit.aspx?EvalID=&ParentID=" & Session("Level1ID") & "&ActiveOnly=" & Me.ActiveOnlyCheckBox.Checked)
    End Sub

    Protected Sub btnGoBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGoBack.Click
        Response.Redirect("Level1Folders.aspx")
    End Sub

End Class
