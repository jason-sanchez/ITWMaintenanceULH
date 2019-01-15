Imports ITWMaintenance.Library.Interventions

Partial Class IntVnts_Level1Folder_Edit
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("currentObject") = Nothing
            ApplyAuthorizationRules()
        Else
            Me.pnlError.Visible = False
            Me.lblError.Text = ""
        End If

        Try
            Me.frmEdit.FindControl("FolderNameTextBox").Focus()
        Catch
        End Try
    End Sub

    Private Sub ApplyAuthorizationRules()
        If IntVntLevel1Folder.CanEditObject Then
            Dim obj As IntVntLevel1Folder = GetIntVntLevel1Folder()

            If obj.IsNew Then
                Me.lblHeader.Text = "Add New Intervention Level 1 Folder"
                Me.frmEdit.DefaultMode = DetailsViewMode.Insert
            Else
                Me.lblHeader.Text = "Edit Intervention Level 1 Folder"
                Me.frmEdit.DefaultMode = DetailsViewMode.Edit
            End If
        Else
            Me.lblHeader.Text = "View Intervention Level 1 Folder"
            Me.frmEdit.DefaultMode = DetailsViewMode.ReadOnly
        End If
    End Sub

    Protected Sub frmEdit_ItemCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles frmEdit.ItemCreated
        If frmEdit.DefaultMode = DetailsViewMode.Insert Then
            Dim obj As IntVntLevel1Folder = GetIntVntLevel1Folder()

            CType(frmEdit.FindControl("FolderNameTextBox"), TextBox).Text = obj.FolderName
            CType(frmEdit.FindControl("DisplayOrderTextBox"), TextBox).Text = obj.DisplayOrder
            CType(frmEdit.FindControl("DisciplineDropDown"), DropDownList).SelectedValue = obj.Discipline
            CType(frmEdit.FindControl("InactiveCheckBox"), CheckBox).Checked = obj.Inactive
        End If
    End Sub

    ' Redirect back to the list on Cancel
    Protected Sub frmEdit_ModeChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewModeEventArgs) Handles frmEdit.ModeChanging
        If e.CancelingEdit OrElse e.Cancel Then
            GoBack()
        End If
    End Sub

    Protected Sub IntVntLevel1FolderDataSource_SelectObject(ByVal sender As Object, ByVal e As Csla.Web.SelectObjectArgs) Handles IntVntLevel1FolderDataSource.SelectObject
        e.BusinessObject = GetIntVntLevel1Folder()
    End Sub

    Protected Sub IntVntLevel1FolderDataSource_InsertObject(ByVal sender As Object, ByVal e As Csla.Web.InsertObjectArgs) Handles IntVntLevel1FolderDataSource.InsertObject
        Dim obj As IntVntLevel1Folder = GetIntVntLevel1Folder()
        Csla.Data.DataMapper.Map(e.Values, obj)
        SaveIntVntLevel1Folder(obj)
    End Sub

    Protected Sub IntVntLevel1FolderDataSource_UpdateObject(ByVal sender As Object, ByVal e As Csla.Web.UpdateObjectArgs) Handles IntVntLevel1FolderDataSource.UpdateObject
        Dim obj As IntVntLevel1Folder = GetIntVntLevel1Folder()
        Csla.Data.DataMapper.Map(e.Values, obj, "intVntID")
        SaveIntVntLevel1Folder(obj)
    End Sub

    Protected Sub IntVntLevel1FolderDataSource_DeleteObject(ByVal sender As Object, ByVal e As Csla.Web.DeleteObjectArgs) Handles IntVntLevel1FolderDataSource.DeleteObject
        Try
            IntVntLevel1Folder.DeleteIntVntLevel1Folder(e.Keys("intVntID"))
            Session("currentObject") = Nothing
            GoBack()
        Catch ex As Csla.DataPortalException
            Me.pnlError.Visible = True
            Me.lblError.Text = ex.BusinessException.Message
        Catch ex As Exception
            Me.pnlError.Visible = True
            Me.lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub GoBack()
        Response.Redirect("Level1Folders.aspx?ActiveOnly=" & Request("ActiveOnly"))
    End Sub

    Private Function GetIntVntLevel1Folder() As IntVntLevel1Folder
        Dim businessObject As Object = Session("currentObject")

        If IsNothing(businessObject) OrElse Not TypeOf businessObject Is IntVntLevel1Folder Then
            Try
                Dim idString As String = Request("intVntID")

                If Not String.IsNullOrEmpty(idString) Then
                    businessObject = IntVntLevel1Folder.GetIntVntLevel1Folder(idString)
                Else
                    businessObject = IntVntLevel1Folder.NewIntVntLevel1Folder()
                End If

                Session("currentObject") = businessObject
            Catch ex As System.Security.SecurityException
                ' TODO - Create the Error page
                GoBack()
            End Try
        End If

        Return CType(businessObject, IntVntLevel1Folder)
    End Function

    Private Sub SaveIntVntLevel1Folder(ByVal theFolder As IntVntLevel1Folder)
        Try
            Session("currentObject") = theFolder.Save()
            GoBack()
        Catch ex As Csla.Validation.ValidationException
            Dim message As New System.Text.StringBuilder
            message.AppendFormat("{0}:<br/>", ex.Message)

            If theFolder.BrokenRulesCollection.Count = 1 Then
                message.AppendFormat("-{0}", theFolder.BrokenRulesCollection(0).Description)
            Else
                For Each rule As Csla.Validation.BrokenRule In _
                    theFolder.BrokenRulesCollection
                    message.AppendFormat("-{0}<br/>", rule.Description)
                Next
            End If

            Me.pnlError.Visible = True
            Me.lblError.Text = message.ToString()
        Catch ex As Csla.DataPortalException
            Me.pnlError.Visible = True
            Me.lblError.Text = ex.BusinessException.Message
        Catch ex As Exception
            Me.pnlError.Visible = True
            Me.lblError.Text = ex.Message
        End Try
    End Sub

End Class
