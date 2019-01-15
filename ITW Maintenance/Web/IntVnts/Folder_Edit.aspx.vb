Imports ITWMaintenance.Library.Interventions
Imports ITWMaintenance.Library.Interventions.Folders

Partial Class IntVnts_Folder_Edit
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
            CType(Me.frmEdit.FindControl("PasteFormButton"), Button).Visible = Not IsNothing(Session("CopiedIntVntForm"))
        Catch
        End Try

        If Not String.IsNullOrEmpty(Request("intVntID")) Then
            Session("ShowintVntID") = Request("intVntID")
        ElseIf Not String.IsNullOrEmpty(Request("ParentID")) Then
            Session("ShowintVntID") = Request("ParentID")
        End If

        ' Hide the IntVnt Tree by default
        If Not ClientScript.IsStartupScriptRegistered("HideTree") Then
            ClientScript.RegisterStartupScript(Me.GetType(), "HideTree", "HideIntVntTree();", True)
        End If
    End Sub

    Private Sub ApplyAuthorizationRules()
        If Intervention.CanEditObject Then
            Dim obj As IntVntFolder = GetFolder()

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
        Dim obj As IntVntFolder = GetFolder()

        If frmEdit.DefaultMode = DetailsViewMode.Insert Then
            DirectCast(frmEdit.FindControl("FolderNameTextBox"), TextBox).Text = obj.FolderName
            DirectCast(frmEdit.FindControl("ShortNameTextBox"), TextBox).Text = obj.ShortName
            DirectCast(frmEdit.FindControl("DisplayOrderTextBox"), TextBox).Text = obj.DisplayOrder
            DirectCast(frmEdit.FindControl("InactiveCheckBox"), CheckBox).Checked = obj.Inactive
        End If

        ' Show the Parent
        If obj.ParentID > 0 Then
            DirectCast(frmEdit.FindControl("ParentFolderLabel"), Label).Text = ReadOnlyIntVntFolder.GetIntVntFolderInfo(obj.ParentID).FolderName
        Else
            DirectCast(frmEdit.FindControl("ParentFolderLabel"), Label).Text = "No Parent; This is a level 1 folder"
        End If
    End Sub

    ' Redirect back to the list on Cancel
    Protected Sub frmEdit_ModeChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewModeEventArgs) Handles frmEdit.ModeChanging
        If e.CancelingEdit OrElse e.Cancel Then
            GoBack()
        End If
    End Sub

    Protected Sub IntVntFolderDataSource_SelectObject(ByVal sender As Object, ByVal e As Csla.Web.SelectObjectArgs) Handles IntVntFolderDataSource.SelectObject
        e.BusinessObject = GetFolder()
    End Sub

    Protected Sub IntVntFolderDataSource_InsertObject(ByVal sender As Object, ByVal e As Csla.Web.InsertObjectArgs) Handles IntVntFolderDataSource.InsertObject
        Dim obj As IntVntFolder = GetFolder()
        Csla.Data.DataMapper.Map(e.Values, obj)
        SaveFolder(obj)
    End Sub

    Protected Sub IntVntFolderDataSource_UpdateObject(ByVal sender As Object, ByVal e As Csla.Web.UpdateObjectArgs) Handles IntVntFolderDataSource.UpdateObject
        Dim obj As IntVntFolder = GetFolder()
        Csla.Data.DataMapper.Map(e.Values, obj, "intVntID")
        SaveFolder(obj)
    End Sub

    Protected Sub IntVntFolderDataSource_DeleteObject(ByVal sender As Object, ByVal e As Csla.Web.DeleteObjectArgs) Handles IntVntFolderDataSource.DeleteObject
        Try
            IntVntFolder.DeleteIntVntFolder(e.Keys("intVntID"))
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
        ' Get the intVntID of the node that needs to be shown.
        Dim intVntID As String = Request("intVntID")

        ' If we are editing an existing folder, Request("intVntID") will hold
        ' the Interventions ID we're looking for.  But if we've just added a new folder,
        ' we'll need to pull the Interventions ID from the business object.
        If Not IsNothing(Session("currentObject")) AndAlso TypeOf Session("currentObject") Is IntVntFolder Then
            intVntID = DirectCast(Session("currentObject"), IntVntFolder).intVntID.ToString()
        End If

        'Response.Redirect("IntVnt_Landing.aspx?ShowintVntID=" & intVntID)
        Dim theFolder As IntVntFolder = GetFolder()
        If theFolder.IntVntLevel = 2 Then
            Response.Redirect("IntVnts.aspx?ParentID=" & theFolder.ParentID)
        Else
            Response.Redirect("IntVnt_Landing.aspx?ShowintVntID=" & intVntID)
        End If
    End Sub

    Private Function GetFolder() As IntVntFolder
        Dim businessObject As Object = Session("currentObject")

        If IsNothing(businessObject) OrElse Not TypeOf businessObject Is IntVntFolder Then
            Try
                Dim idString As String = Request("intVntID")
                Dim parentID As String = Request("ParentID")

                If Not String.IsNullOrEmpty(idString) Then
                    businessObject = IntVntFolder.GetIntVntFolder(idString)
                ElseIf Not String.IsNullOrEmpty(parentID) Then
                    businessObject = IntVntFolder.NewIntVntFolder(parentID)
                Else
                    ' TODO - Create the Error page
                    Response.Redirect("IntVnt_Error.aspx")
                End If

                Session("currentObject") = businessObject
            Catch ex As System.Security.SecurityException
                ' TODO - Create the Error page
                Response.Redirect("Error.aspx?ErrorText=Security Exception!&ErrorDetails=" & Server.UrlEncode(ex.Message))
            End Try
        End If

        Return CType(businessObject, IntVntFolder)
    End Function

    Private Sub SaveFolder(ByVal theFolder As IntVntFolder)
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
        Response.Redirect("Form_Edit.aspx?intVntID=&ParentID=" & Request("intVntID"))
    End Sub

    Protected Sub NewSubFolderButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("Folder_Edit.aspx?intVntID=&ParentID=" & Request("intVntID"))
    End Sub

    Protected Sub PasteFormButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("Form_PasteCopied.aspx?ParentID=" & Request("intVntID"))
    End Sub

End Class
