Imports ITWMaintenance.Library.Interventions
Imports ITWMaintenance.Library.Interventions.Utilities

Partial Class IntVnts_IntVnt_Edit
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("currentObject") = Nothing
            ApplyAuthorizationRules()

            If Session("Level2IntVntID") <> Request("intVntID") Then
                Session("Level2IntVntID") = Request("intVntID")
            End If
        Else
            Me.pnlError.Visible = False
            Me.lblError.Text = ""
        End If

        Try
            Me.frmEdit.FindControl("IntVntNameTextBox").Focus()
        Catch
        End Try

        ' Hide the IntVnt Tree by default
        If Not ClientScript.IsStartupScriptRegistered("HideTree") Then
            ClientScript.RegisterStartupScript(Me.GetType(), "HideTree", "HideIntVntTree();", True)
        End If
    End Sub

    Private Sub ApplyAuthorizationRules()
        If Intervention.CanEditObject Then
            Dim obj As Intervention = GetEval()

            If obj.IsNew Then
                Me.lblHeader.Text = "Add New Intervention"
                Me.frmEdit.DefaultMode = DetailsViewMode.Insert
            Else
                Me.lblHeader.Text = "Edit Intervention"
                Me.frmEdit.DefaultMode = DetailsViewMode.Edit

                Try
                    ' Try to show the new folder, new form, and paste buttons
                    DirectCast(Me.frmEdit.FindControl("ChildFunctionsPanel"), Panel).Visible = True
                    DirectCast(Me.frmEdit.FindControl("RebuildPanel"), Panel).Visible = True

                    ' Show the [Paste Copied Form] button if a form has been copied
                    CType(Me.frmEdit.FindControl("PasteFormButton"), Button).Visible = Not IsNothing(Session("CopiedIntVntForm"))
                Catch ex As Exception
                End Try
            End If
        Else
            Me.lblHeader.Text = "View Intervention"
            Me.frmEdit.DefaultMode = DetailsViewMode.ReadOnly
        End If
    End Sub

    Protected Sub frmEdit_ItemCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles frmEdit.ItemCreated
        Dim obj As Intervention = GetEval()

        If frmEdit.DefaultMode = DetailsViewMode.Insert Then
            CType(frmEdit.FindControl("IntVntNameTextBox"), TextBox).Text = obj.IntVntName
            CType(frmEdit.FindControl("DisplayOrderTextBox"), TextBox).Text = obj.DisplayOrder
            CType(frmEdit.FindControl("InactiveCheckBox"), CheckBox).Checked = obj.Inactive
            CType(frmEdit.FindControl("OneTimeCheckBox"), CheckBox).Checked = obj.OneTime
            CType(frmEdit.FindControl("BillingCheckBox"), CheckBox).Checked = obj.Billing
        End If

        ' Show the Parent
        CType(frmEdit.FindControl("ParentFolderLabel"), Label).Text = ReadOnlyIntVntLevel1Folder.GetIntVntLevel1FolderInfo(obj.ParentID).FolderName
    End Sub

    ' Redirect back to the list on Cancel
    Protected Sub frmEdit_ModeChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewModeEventArgs) Handles frmEdit.ModeChanging
        If e.CancelingEdit OrElse e.Cancel Then
            GoBack()
        End If
    End Sub

    Protected Sub IntVntDataSource_SelectObject(ByVal sender As Object, ByVal e As Csla.Web.SelectObjectArgs) Handles IntVntDataSource.SelectObject
        e.BusinessObject = GetEval()
    End Sub

    Protected Sub IntVntDataSource_InsertObject(ByVal sender As Object, ByVal e As Csla.Web.InsertObjectArgs) Handles IntVntDataSource.InsertObject
        Dim obj As Intervention = GetEval()
        Csla.Data.DataMapper.Map(e.Values, obj)
        SaveEval(obj)
    End Sub

    Protected Sub IntVntDataSource_UpdateObject(ByVal sender As Object, ByVal e As Csla.Web.UpdateObjectArgs) Handles IntVntDataSource.UpdateObject
        Dim obj As Intervention = GetEval()
        Csla.Data.DataMapper.Map(e.Values, obj, "intVntID")
        SaveEval(obj)
    End Sub

    Protected Sub IntVntDataSource_DeleteObject(ByVal sender As Object, ByVal e As Csla.Web.DeleteObjectArgs) Handles IntVntDataSource.DeleteObject
        Try
            Intervention.DeleteIntervention(e.Keys("intVntID"))
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
        If String.IsNullOrEmpty(Session("Level2intVntID")) Then
            ' Go back to the list of IntVnts since the user was adding a new IntVnt
            Response.Redirect("IntVnts.aspx?ParentID=" & Request("ParentID") & "&ActiveOnly=" & Request("ActiveOnly"))
        Else
            Response.Redirect("IntVnt_Landing.aspx")
        End If
    End Sub

    Private Function GetEval() As Intervention
        Dim businessObject As Object = Session("currentObject")

        If IsNothing(businessObject) OrElse Not TypeOf businessObject Is Intervention Then
            Try
                Dim idString As String = Request("intVntID")
                Dim parentID As String = Session("Level1ID")

                If Not String.IsNullOrEmpty(idString) Then
                    businessObject = Intervention.GetIntervention(idString)
                ElseIf Not String.IsNullOrEmpty(parentID) Then
                    businessObject = Intervention.NewIntervention(parentID)
                Else
                    ' TODO - Create the Error page
                    Response.Redirect("IntVnt_Error.aspx")
                End If

                Session("currentObject") = businessObject
            Catch ex As System.Security.SecurityException
                ' TODO - Create the Error page
                Response.Redirect("IntVnt_Error.aspx?ErrorText=Security Exception!&ErrorDetails=" & Server.UrlEncode(ex.Message))
            End Try
        End If

        Return CType(businessObject, Intervention)
    End Function

    Private Sub SaveEval(ByVal theIntVnt As Intervention)
        Try
            Session("currentObject") = theIntVnt.Save()
            ' If the user just added a new IntVnt, we need to set the
            ' level2 variable so that we load that IntVnt, instead of
            ' returning to the list.
            Session("Level2intVntID") = GetEval().intVntID
            GoBack()
        Catch ex As Csla.Validation.ValidationException
            Dim message As New System.Text.StringBuilder
            message.AppendFormat("{0}:<br/>", ex.Message)

            If theIntVnt.BrokenRulesCollection.Count = 1 Then
                message.AppendFormat("-{0}", theIntVnt.BrokenRulesCollection(0).Description)
            Else
                For Each rule As Csla.Validation.BrokenRule In _
                    theIntVnt.BrokenRulesCollection
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
        Response.Redirect("Form_Edit.aspx?intVntID=&ParentID=" & Session("Level2intVntID"))
    End Sub

    Protected Sub NewSubFolderButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("Folder_Edit.aspx?intVntID=&ParentID=" & Session("Level2intVntID"))
    End Sub

    Protected Sub PasteFormButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("PasteCopiedIntVnt.aspx?ParentID=" & Session("Level2intVntID"))
    End Sub

    Protected Sub RebuildButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ' What routines are we going to run?
        Dim cbl As CheckBoxList
        Dim processes As String = ""

        Try
            cbl = CType(Me.frmEdit.FindControl("RebuildPanel").FindControl("RebuildCheckBoxList"), CheckBoxList)

            For Each item As ListItem In cbl.Items
                If item.Selected Then
                    RunRebuildRoutine(item.Value)
                    processes &= "<br />-" & Trim(item.Text)
                    item.Selected = False
                End If
            Next

            If Not String.IsNullOrEmpty(processes) Then
                CType(Me.frmEdit.FindControl("RebuildPanel").FindControl("RebuildResultLabel"), Label).Text = "Process Completed Successfully!" & processes
            Else
                CType(Me.frmEdit.FindControl("RebuildPanel").FindControl("RebuildResultLabel"), Label).Text = "No processes selected!"
            End If
        Catch ex As Exception
            CType(Me.frmEdit.FindControl("RebuildPanel").FindControl("RebuildResultLabel"), Label).Text = ex.Message
        End Try
    End Sub

    Private Function RunRebuildRoutine(ByVal Routine As String) As Boolean
        Select Case Routine
            Case "ShortNames"
                Try
                    Dim builder As New ShortNameBuilder(Session("Level2intVntID"))

                    If builder.BuildShortNames Then
                        Return True
                    Else
                        ' Didn't work and no error throw, so check if there is an error
                        ' we can throw
                        If Not IsNothing(builder.TheError) Then
                            Throw builder.TheError
                        Else
                            ' This should never be hit but just in case...
                            Throw New Exception("Unable to rebuild Short Names")
                        End If
                    End If
                Catch ex As Exception
                    Throw ex
                End Try
            Case "iPaths"
                Try
                    Dim builder As New iPathBuilder(Session("Level2intVntID"))

                    If builder.BuildiPaths Then
                        Return True
                    Else
                        ' Didn't work and no error throw, so check if there is an error
                        ' we can throw
                        If Not IsNothing(builder.TheError) Then
                            Throw builder.TheError
                        Else
                            ' This should never be hit but just in case...
                            Throw New Exception("Unable to rebuild iGroups")
                        End If
                    End If
                Catch ex As Exception
                    Throw ex
                End Try
            Case "iGroups"
                Try
                    Dim builder As New iGroupBuilder(Session("Level2intVntID"))

                    If builder.BuildiGroups Then
                        Return True
                    Else
                        ' Didn't work and no error throw, so check if there is an error
                        ' we can throw
                        If Not IsNothing(builder.TheError) Then
                            Throw builder.TheError
                        Else
                            ' This should never be hit but just in case...
                            Throw New Exception("Unable to rebuild iGroups")
                        End If
                    End If
                Catch ex As Exception
                    Throw ex
                End Try
            Case Else
                Throw New Exception("Unable to run routine: " & Routine)
        End Select
    End Function

End Class
