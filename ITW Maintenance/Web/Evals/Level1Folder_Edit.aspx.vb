Imports ITWMaintenance.Library.Evaluations

Partial Class Evals_Level1Folder_Edit
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
        If EvalLevel1Folder.CanEditObject Then
            Dim obj As EvalLevel1Folder = GetEvalLevel1Folder()

            If obj.IsNew Then
                Me.lblHeader.Text = "Add New Eval Level 1 Folder"
                Me.frmEdit.DefaultMode = DetailsViewMode.Insert
            Else
                Me.lblHeader.Text = "Edit Eval Level 1 Folder"
                Me.frmEdit.DefaultMode = DetailsViewMode.Edit
            End If
        Else
            Me.lblHeader.Text = "Level 1 Folder Details"
            Me.frmEdit.DefaultMode = DetailsViewMode.ReadOnly
        End If
    End Sub

    Protected Sub frmEdit_ItemCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles frmEdit.ItemCreated
        If frmEdit.DefaultMode = DetailsViewMode.Insert Then
            Dim obj As EvalLevel1Folder = GetEvalLevel1Folder()

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

    Protected Sub EvalLevel1FolderDataSource_SelectObject(ByVal sender As Object, ByVal e As Csla.Web.SelectObjectArgs) Handles EvalLevel1FolderDataSource.SelectObject
        e.BusinessObject = GetEvalLevel1Folder()
    End Sub

    Protected Sub EvalLevel1FolderDataSource_InsertObject(ByVal sender As Object, ByVal e As Csla.Web.InsertObjectArgs) Handles EvalLevel1FolderDataSource.InsertObject
        Dim obj As EvalLevel1Folder = GetEvalLevel1Folder()
        Csla.Data.DataMapper.Map(e.Values, obj)
        SaveEvalLevel1Folder(obj)
    End Sub

    Protected Sub EvalLevel1FolderDataSource_UpdateObject(ByVal sender As Object, ByVal e As Csla.Web.UpdateObjectArgs) Handles EvalLevel1FolderDataSource.UpdateObject
        Dim obj As EvalLevel1Folder = GetEvalLevel1Folder()
        Csla.Data.DataMapper.Map(e.Values, obj, "EvalID")
        SaveEvalLevel1Folder(obj)
    End Sub

    Protected Sub EvalLevel1FolderDataSource_DeleteObject(ByVal sender As Object, ByVal e As Csla.Web.DeleteObjectArgs) Handles EvalLevel1FolderDataSource.DeleteObject
        Try
            EvalLevel1Folder.DeleteEvalLevel1Folder(e.Keys("EvalID"))
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

    Private Function GetEvalLevel1Folder() As EvalLevel1Folder
        Dim businessObject As Object = Session("currentObject")

        If IsNothing(businessObject) OrElse Not TypeOf businessObject Is EvalLevel1Folder Then
            Try
                Dim idString As String = Request("EvalID")

                If Not String.IsNullOrEmpty(idString) Then
                    businessObject = EvalLevel1Folder.GetEvalLevel1Folder(idString)
                Else
                    businessObject = EvalLevel1Folder.NewEvalLevel1Folder()
                End If

                Session("currentObject") = businessObject
            Catch ex As System.Security.SecurityException
                ' TODO - Create the Error page
                GoBack()
            End Try
        End If

        Return CType(businessObject, EvalLevel1Folder)
    End Function

    Private Sub SaveEvalLevel1Folder(ByVal theFolder As EvalLevel1Folder)
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
