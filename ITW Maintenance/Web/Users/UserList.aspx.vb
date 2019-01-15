Imports ITWMaintenance.Library.Users

Partial Class Users_UserList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If ITWMaintenance.Library.Users.User.CanAddObject Then
                Me.AddUserButton.Visible = True
            End If

            If Not String.IsNullOrEmpty(Request("SearchText")) Then
                Me.SearchTextBox.Text = Request("SearchText")
            End If

            If Not String.IsNullOrEmpty(Request("ActiveOnly")) Then
                Try
                    Me.ActiveOnlyCheckBox.Checked = CBool(Request("ActiveOnly"))
                Catch ex As Exception
                End Try
            End If
        End If
    End Sub

    Protected Sub UserListGridView_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles UserListGridView.RowCommand
        If e.CommandName = "EditItem" Then
            Response.Redirect("User_Edit.aspx?ID=" & e.CommandArgument & "&SearchText=" & Me.SearchTextBox.Text & "&ActiveOnly=" & Me.ActiveOnlyCheckBox.Checked)
        End If
    End Sub

    Protected Sub UserListGridView_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles UserListGridView.RowDataBound
        Dim user As ReadOnlyUser = Nothing

        If Not IsNothing(e.Row.DataItem) Then
            If ITWMaintenance.Library.Users.User.CanEditObject Then
                user = DirectCast(e.Row.DataItem, ReadOnlyUser)

                e.Row.Attributes.Add("onclick", "window.location.href='User_Edit.aspx?ID=" & user.ID & "&SearchText=" & Me.SearchTextBox.Text & "&ActiveOnly=" & Me.ActiveOnlyCheckBox.Checked & "';")
            End If
        End If
    End Sub

    Protected Sub NewButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AddUserButton.Click
        Response.Redirect("User_Edit.aspx?addNew=true&SearchText=" & Me.SearchTextBox.Text & "&ActiveOnly=" & Me.ActiveOnlyCheckBox.Checked)
    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        ' You can only change the SkinID in the PreInit method of the Page
        Try
            If Not ITWMaintenance.Library.Users.User.CanEditObject Then
                'If the user can't edit the object, don't render this grid as "clickable"
                'HACK - We must do this because we're using Master Pages
                Dim m As System.Web.UI.MasterPage = Master
                UserListGridView.SkinID = ""
            End If
        Catch ex As Exception
        End Try
    End Sub

End Class
