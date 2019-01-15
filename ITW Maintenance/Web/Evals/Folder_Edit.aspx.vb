Imports ITWMaintenance.Library.Evaluations
Imports ITWMaintenance.Library.Evaluations.Folders

Partial Class Evals_Folder_Edit
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

            ' Show the [Paste Copied Form] button if a form has been copied
            CType(Me.frmEdit.FindControl("PasteFormButton"), Button).Visible = Not IsNothing(Session("CopiedEvalForm"))
        Catch
        End Try

        If Not String.IsNullOrEmpty(Request("EvalID")) Then
            Session("ShowEvalID") = Request("EvalID")
        ElseIf Not String.IsNullOrEmpty(Request("ParentID")) Then
            Session("ShowEvalID") = Request("ParentID")
        End If

        ' Hide the Eval Tree by default
        If Not ClientScript.IsStartupScriptRegistered("HideTree") Then
            ClientScript.RegisterStartupScript(Me.GetType(), "HideTree", "HideEvalTree();", True)
        End If
    End Sub

    Private Sub ApplyAuthorizationRules()
        If Evaluation.CanEditObject Then
            Dim obj As EvalFolder = GetFolder()

            If obj.IsNew Then
                Me.lblHeader.Text = "Add New Folder"
                Me.frmEdit.DefaultMode = DetailsViewMode.Insert
            Else
                Me.lblHeader.Text = "Edit Existing Folder"
                Me.frmEdit.DefaultMode = DetailsViewMode.Edit
            End If
        Else
            Me.lblHeader.Text = "View Folder"
            Me.frmEdit.DefaultMode = DetailsViewMode.ReadOnly
        End If
    End Sub

    Protected Sub frmEdit_ItemCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles frmEdit.ItemCreated
        Dim obj As EvalFolder = GetFolder()

        If frmEdit.DefaultMode = DetailsViewMode.Insert Then
            CType(frmEdit.FindControl("FolderNameTextBox"), TextBox).Text = obj.FolderName
            CType(frmEdit.FindControl("ShortNameTextBox"), TextBox).Text = obj.ShortName
            CType(frmEdit.FindControl("DisplayOrderTextBox"), TextBox).Text = obj.DisplayOrder
            CType(frmEdit.FindControl("InactiveCheckBox"), CheckBox).Checked = obj.Inactive
        End If

        ' Show the Parent
        CType(frmEdit.FindControl("ParentFolderLabel"), Label).Text = ReadOnlyEvalFolder.GetEvalFolderInfo(obj.ParentID).FolderName
    End Sub

    ' Redirect back to the list on Cancel
    Protected Sub frmEdit_ModeChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewModeEventArgs) Handles frmEdit.ModeChanging
        If e.CancelingEdit OrElse e.Cancel Then
            GoBack()
        End If
    End Sub

    Protected Sub EvalFolderDataSource_SelectObject(ByVal sender As Object, ByVal e As Csla.Web.SelectObjectArgs) Handles EvalFolderDataSource.SelectObject
        e.BusinessObject = GetFolder()
    End Sub

    Protected Sub EvalFolderDataSource_InsertObject(ByVal sender As Object, ByVal e As Csla.Web.InsertObjectArgs) Handles EvalFolderDataSource.InsertObject
        Dim obj As EvalFolder = GetFolder()
        Csla.Data.DataMapper.Map(e.Values, obj)
        SaveFolder(obj)
    End Sub

    Protected Sub EvalFolderDataSource_UpdateObject(ByVal sender As Object, ByVal e As Csla.Web.UpdateObjectArgs) Handles EvalFolderDataSource.UpdateObject
        Dim obj As EvalFolder = GetFolder()
        Csla.Data.DataMapper.Map(e.Values, obj, "EvalID")
        SaveFolder(obj)
    End Sub

    Protected Sub EvalFolderDataSource_DeleteObject(ByVal sender As Object, ByVal e As Csla.Web.DeleteObjectArgs) Handles EvalFolderDataSource.DeleteObject
        Try
            EvalFolder.DeleteEvalFolder(e.Keys("EvalID"))
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
        ' Redirect back to the landing page.
        ' Get the EvalID of the node that needs to be shown.
        Dim evalID As String = Request("EvalID")

        ' If we are editing an existing folder, Request("EvalID") will hold
        ' the Eval ID we're looking for.  But if we've just added a new folder,
        ' we'll need to pull the Eval ID from the business object.
        If Not IsNothing(Session("currentObject")) AndAlso TypeOf Session("currentObject") Is EvalFolder Then
            evalID = DirectCast(Session("currentObject"), EvalFolder).EvalID.ToString()
        End If

        Response.Redirect("Eval_Landing.aspx?ShowEvalID=" & evalID)
    End Sub

    Private Function GetFolder() As EvalFolder
        Dim businessObject As Object = Session("currentObject")

        If IsNothing(businessObject) OrElse Not TypeOf businessObject Is EvalFolder Then
            Try
                Dim idString As String = Request("EvalID")
                Dim parentID As String = Request("ParentID")

                If Not String.IsNullOrEmpty(idString) Then
                    businessObject = EvalFolder.GetEvalFolder(idString)
                ElseIf Not String.IsNullOrEmpty(parentID) Then
                    businessObject = EvalFolder.NewEvalFolder(parentID)
                Else
                    ' TODO - Create the Error page
                    Response.Redirect("Eval_Error.aspx")
                End If

                Session("currentObject") = businessObject
            Catch ex As System.Security.SecurityException
                ' TODO - Create the Error page
                Response.Redirect("Error.aspx?ErrorText=Security Exception!&ErrorDetails=" & Server.UrlEncode(ex.Message))
            End Try
        End If

        Return CType(businessObject, EvalFolder)
    End Function

    Private Sub SaveFolder(ByVal theFolder As EvalFolder)
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

    Protected Sub NewSubFormButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("Form_Edit.aspx?EvalID=&ParentID=" & Request("EvalID"))
    End Sub

    Protected Sub NewSubFolderButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("Folder_Edit.aspx?EvalID=&ParentID=" & Request("EvalID"))
    End Sub

    Protected Sub PasteFormButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("Form_PasteCopied.aspx?ParentID=" & Request("EvalID"))
    End Sub

End Class
