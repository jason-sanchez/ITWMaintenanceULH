Imports ITWMaintenance.Library.Interventions

Partial Class IntVnts_Level1Folders
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Session("Level1ID") = Nothing
            Session("Level2IntVntID") = Nothing

            If Not IntVntLevel1Folder.CanAddObject Then
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
        End If
    End Sub

    Protected Sub Level1FoldersGrid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Level1FoldersGrid.RowCommand
        If e.CommandName = "EditItem" Then
            Response.Redirect("Level1Folder_Edit.aspx?intVntID=" & e.CommandArgument & "&ActiveOnly=" & Me.ActiveOnlyCheckBox.Checked)
        End If
    End Sub

    Protected Sub Level1FoldersGrid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Level1FoldersGrid.RowDataBound
        If e.Row.RowState = DataControlRowState.Normal OrElse e.Row.RowState = DataControlRowState.Alternate Then
            If Not e.Row.DataItem Is Nothing Then
                Dim theFolder As ReadOnlyIntVntLevel1Folder = CType(e.Row.DataItem, ReadOnlyIntVntLevel1Folder)

                If Not IntVntLevel1Folder.CanEditObject Then
                    DirectCast(e.Row.FindControl("btnEdit"), Button).Text = "View"
                End If

                DirectCast(e.Row.FindControl("InactiveCheckBox"), CheckBox).Checked = theFolder.Inactive

                e.Row.Attributes.Add("onclick", "javascript:window.location.href='IntVnts.aspx?ParentID=" & theFolder.intVntID.ToString() & "';")
            End If
        End If
    End Sub

    Protected Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Response.Redirect("Level1Folder_Edit.aspx?EvalID=&ActiveOnly=" & Me.ActiveOnlyCheckBox.Checked)
    End Sub

End Class
