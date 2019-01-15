Imports ITWMaintenance.Library.Interventions
Imports ITWMaintenance.Library.Interventions.Utilities

Partial Class IntVnts_IntVnts
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ' Set the level 1 value here so we can return to the IntVnt list
            ' without having to go all the way back up.
            If Not String.IsNullOrEmpty(Request("ParentID")) Then
                Session("Level1ID") = Request("ParentID")
            End If
            Session("Level2intVntID") = Nothing

            If Not Intervention.CanAddObject Then
                Me.NewSubFolderButton.Visible = False
                Me.NewSubFormButton.Visible = False
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

            Me.ParentDisciplineLabel.Text = "Interventions (" & ReadOnlyIntVntLevel1Folder.GetIntVntLevel1FolderInfo(Session("Level1ID")).Discipline & ")"
            Me.ParentNameLabel.Text = "" & ReadOnlyIntVntLevel1Folder.GetIntVntLevel1FolderInfo(Session("Level1ID")).FolderName & "<sub><font size='0' color='grey'> 1</font></sub>"
        End If
    End Sub

    Protected Sub InterventionsGrid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles InterventionsGrid.RowCommand
        If e.CommandName = "EditItem" Then
            Response.Redirect("IntVnt_Edit.aspx?intVntID=" & e.CommandArgument & "&ParentID=" & Session("Level1ID") & "&ActiveOnly=" & Me.ActiveOnlyCheckBox.Checked)
        End If
    End Sub

    Protected Sub InterventionsGrid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles InterventionsGrid.RowDataBound
        If e.Row.RowState = DataControlRowState.Normal OrElse e.Row.RowState = DataControlRowState.Alternate Then
            If Not e.Row.DataItem Is Nothing Then
                Dim theIntVnt As ReadOnlyIntervention = CType(e.Row.DataItem, ReadOnlyIntervention)

                If Not Intervention.CanEditObject Then
                    DirectCast(e.Row.FindControl("btnEdit"), Button).Text = "View"
                End If

                DirectCast(e.Row.FindControl("InactiveCheckBox"), CheckBox).Checked = theIntVnt.Inactive
                DirectCast(e.Row.FindControl("OneTimeCheckBox"), CheckBox).Checked = theIntVnt.OneTime
                DirectCast(e.Row.FindControl("BillingCheckBox"), CheckBox).Checked = theIntVnt.Billing

                If theIntVnt.IsForm Then
                    e.Row.Attributes.Add("onclick", "javascript:window.location.href='Form_Edit.aspx?intVntID=" & theIntVnt.intVntID & "';")
                    DirectCast(e.Row.FindControl("btnEdit"), Button).OnClientClick = "javascript:window.location.href='Form_Edit.aspx?intVntID=" & theIntVnt.intVntID & "'; return false;"
                Else
                    e.Row.Attributes.Add("onclick", "javascript:window.location.href='IntVnt_Landing.aspx?intVntID=" & theIntVnt.intVntID & "&IntVntActiveOnly=True';")
                End If
            End If
        End If
    End Sub

    'Protected Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
    '    Response.Redirect("IntVnt_Edit.aspx?intVntID=&ParentID=" & Session("Level1ID") & "&ActiveOnly=" & Me.ActiveOnlyCheckBox.Checked)
    'End Sub

    Protected Sub NewSubFormButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("Form_Edit.aspx?intVntID=&ParentID=" & Session("Level1ID"))
    End Sub

    Protected Sub NewSubFolderButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("Folder_Edit.aspx?intVntID=&ParentID=" & Session("Level1ID"))
    End Sub

    Protected Sub btnGoBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGoBack.Click
        Response.Redirect("Level1Folders.aspx")
    End Sub

End Class
