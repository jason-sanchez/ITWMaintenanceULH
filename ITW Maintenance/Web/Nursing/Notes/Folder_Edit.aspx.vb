Imports ITWMaintenance.Library.Nursing.Notes
Imports Csla.Validation

Partial Class Nursing_Notes_Folder_Edit
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
            CType(Me.frmEdit.FindControl("PasteFormButton"), Button).Visible = Not IsNothing(Session("CopiedNursingNoteForm"))
        Catch
        End Try

        If Not String.IsNullOrEmpty(Request("FolderID")) Then
            Session("ShowNursingNoteID") = Request("FolderID")
        ElseIf Not String.IsNullOrEmpty(Request("ParentID")) Then
            Session("ShowNursingNoteID") = Request("ParentID")
        End If

        ' Hide the Eval Tree by default
        If Not ClientScript.IsStartupScriptRegistered("HideTree") Then
            ClientScript.RegisterStartupScript(Me.GetType(), "HideTree", "HideNursingNoteTree();", True)
        End If
    End Sub

    Private Sub ApplyAuthorizationRules()
        If NursingNoteFolder.CanEditObject Then
            Dim obj As NursingNoteFolder = GetFolder()

            If obj.IsNew Then
                Me.lblHeader.Text = "Add New Folder"
                Me.frmEdit.DefaultMode = DetailsViewMode.Insert
            Else
                Me.lblHeader.Text = "Edit Existing Folder"
                Me.frmEdit.DefaultMode = DetailsViewMode.Edit
            End If
        Else
            Me.frmEdit.DefaultMode = DetailsViewMode.ReadOnly
        End If
    End Sub

    Protected Sub frmEdit_ItemCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles frmEdit.ItemCreated
        Dim obj As NursingNoteFolder = GetFolder()

        If frmEdit.DefaultMode = FormViewMode.Edit OrElse frmEdit.DefaultMode = FormViewMode.Insert Then
            ' Set the MaxLength property on the fields based on the ValidationRules for the Object
            For Each ruleURI As String In obj.GetRuleDescriptions()
                Dim ruleDesc As RuleDescription = RuleDescription.Parse(ruleURI)

                Try
                    If Not String.IsNullOrEmpty(ruleDesc.Arguments("MaxLength")) Then
                        CType(Me.frmEdit.FindControl(ruleDesc.PropertyName & "TextBox"), TextBox).MaxLength = ruleDesc.Arguments("MaxLength")
                    End If
                Catch ex As Exception
                End Try
            Next
        End If

        If frmEdit.DefaultMode = DetailsViewMode.Insert Then
            CType(frmEdit.FindControl("ParentNameLabel"), Label).Text = obj.ParentName
            CType(frmEdit.FindControl("FolderNameTextBox"), TextBox).Text = obj.FolderName
            CType(frmEdit.FindControl("DisplayOrderTextBox"), TextBox).Text = obj.DisplayOrder
            CType(frmEdit.FindControl("InactiveCheckBox"), CheckBox).Checked = obj.Inactive
        End If
    End Sub

    ' Redirect back to the list on Cancel
    Protected Sub frmEdit_ModeChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewModeEventArgs) Handles frmEdit.ModeChanging
        If e.CancelingEdit OrElse e.Cancel Then
            GoBack()
        End If
    End Sub

    Protected Sub EvalFolderDataSource_SelectObject(ByVal sender As Object, ByVal e As Csla.Web.SelectObjectArgs) Handles NursingNoteFolderDataSource.SelectObject
        e.BusinessObject = GetFolder()
    End Sub

    Protected Sub EvalFolderDataSource_InsertObject(ByVal sender As Object, ByVal e As Csla.Web.InsertObjectArgs) Handles NursingNoteFolderDataSource.InsertObject
        Dim obj As NursingNoteFolder = GetFolder()
        Csla.Data.DataMapper.Map(e.Values, obj, "ParentName")
        SaveFolder(obj)
    End Sub

    Protected Sub EvalFolderDataSource_UpdateObject(ByVal sender As Object, ByVal e As Csla.Web.UpdateObjectArgs) Handles NursingNoteFolderDataSource.UpdateObject
        Dim obj As NursingNoteFolder = GetFolder()
        Csla.Data.DataMapper.Map(e.Values, obj, "FolderID")
        SaveFolder(obj)
    End Sub

    Protected Sub EvalFolderDataSource_DeleteObject(ByVal sender As Object, ByVal e As Csla.Web.DeleteObjectArgs) Handles NursingNoteFolderDataSource.DeleteObject
        Try
            NursingNoteFolder.DeleteNursingNoteFolder(e.Keys("FolderID"))
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
        ' Get the NsNtID of the node that needs to be shown.
        Dim folderID As String = Request("FolderID")

        ' If we are editing an existing folder, Request("FolderID") will hold
        ' the NsNt ID we're looking for.  But if we've just added a new folder,
        ' we'll need to pull the NsNt ID from the business object.
        If Not IsNothing(Session("currentObject")) AndAlso TypeOf Session("currentObject") Is NursingNoteFolder Then
            folderID = DirectCast(Session("currentObject"), NursingNoteFolder).FolderID.ToString()
        End If

        Response.Redirect("NursingNote_Landing.aspx?ShowNursingNoteID=" & folderID)
    End Sub

    Private Function GetFolder() As NursingNoteFolder
        Dim businessObject As Object = Session("currentObject")

        If IsNothing(businessObject) OrElse Not TypeOf businessObject Is NursingNoteFolder Then
            Try
                Dim idString As String = Request("FolderID")
                Dim parentID As String = Request("ParentID")

                If Not String.IsNullOrEmpty(idString) Then
                    businessObject = NursingNoteFolder.GetNursingNoteFolder(idString)
                ElseIf Not String.IsNullOrEmpty(parentID) Then
                    If Not String.IsNullOrEmpty(Request("Discipline")) Then
                        businessObject = NursingNoteFolder.NewNursingNoteFolder(parentID, Request("Discipline"))
                    Else
                        businessObject = NursingNoteFolder.NewNursingNoteFolder(parentID)
                    End If
                Else
                    ' TODO - Create the Error page
                    Response.Redirect("NursingNote_Error.aspx")
                End If

                Session("currentObject") = businessObject
            Catch ex As System.Security.SecurityException
                ' TODO - Create the Error page
                Response.Redirect("Error.aspx?ErrorText=Security Exception!&ErrorDetails=" & Server.UrlEncode(ex.Message))
            End Try
        End If

        Return CType(businessObject, NursingNoteFolder)
    End Function

    Private Sub SaveFolder(ByVal theFolder As NursingNoteFolder)
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
        Response.Redirect("Form_Edit.aspx?FormID=&ParentID=" & Request("FolderID"))
    End Sub

    Protected Sub NewSubFolderButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("Folder_Edit.aspx?FolderID=&ParentID=" & Request("FolderID"))
    End Sub

    Protected Sub PasteFormButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("Form_PasteCopied.aspx?ParentID=" & Request("FolderID"))
    End Sub

End Class
