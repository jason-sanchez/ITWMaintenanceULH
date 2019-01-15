Imports ITWMaintenance.Library.Evaluations
Imports ITWMaintenance.Library.Evaluations.Utilities

Partial Class Evals_Eval_Edit
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("currentObject") = Nothing
            ApplyAuthorizationRules()

            If Session("Level2EvalID") <> Request("EvalID") Then
                Session("Level2EvalID") = Request("EvalID")
            End If
        Else
            Me.pnlError.Visible = False
            Me.lblError.Text = ""
        End If

        Try
            Me.frmEdit.FindControl("EvalNameTextBox").Focus()
        Catch
        End Try

        ' Hide the Eval Tree by default
        If Not ClientScript.IsStartupScriptRegistered("HideTree") Then
            ClientScript.RegisterStartupScript(Me.GetType(), "HideTree", "HideEvalTree();", True)
        End If
    End Sub

    Private Sub ApplyAuthorizationRules()
        If Evaluation.CanEditObject Then
            Dim obj As Evaluation = GetEval()

            If obj.IsNew Then
                Me.lblHeader.Text = "Add New Evaluation"
                Me.frmEdit.DefaultMode = DetailsViewMode.Insert
            Else
                Me.lblHeader.Text = "Edit Evaluation"
                Me.frmEdit.DefaultMode = DetailsViewMode.Edit

                Try
                    ' Try to show the new folder, new form, and paste buttons
                    DirectCast(Me.frmEdit.FindControl("ChildFunctionsPanel"), Panel).Visible = True
                    DirectCast(Me.frmEdit.FindControl("RebuildPanel"), Panel).Visible = True

                    ' Show the [Paste Copied Form] button if a form has been copied
                    CType(Me.frmEdit.FindControl("PasteFormButton"), Button).Visible = Not IsNothing(Session("CopiedEvalForm"))
                Catch ex As Exception
                End Try
            End If
        Else
            Me.lblHeader.Text = "Evaluation Details"
            Me.frmEdit.DefaultMode = DetailsViewMode.ReadOnly
        End If
    End Sub

    Protected Sub frmEdit_ItemCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles frmEdit.ItemCreated
        Dim obj As Evaluation = GetEval()

        If frmEdit.DefaultMode = DetailsViewMode.Insert Then
            CType(frmEdit.FindControl("EvalNameTextBox"), TextBox).Text = obj.EvalName
            CType(frmEdit.FindControl("DisplayOrderTextBox"), TextBox).Text = obj.DisplayOrder
            CType(frmEdit.FindControl("InactiveCheckBox"), CheckBox).Checked = obj.Inactive
            CType(frmEdit.FindControl("OneTimeCheckBox"), CheckBox).Checked = obj.OneTime
            CType(frmEdit.FindControl("PostDischargeCheckBox"), CheckBox).Checked = obj.PostDischarge
        End If

        ' Show the Parent
        CType(frmEdit.FindControl("ParentFolderLabel"), Label).Text = ReadOnlyEvalLevel1Folder.GetEvalLevel1FolderInfo(obj.ParentID).FolderName
    End Sub

    ' Redirect back to the list on Cancel
    Protected Sub frmEdit_ModeChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewModeEventArgs) Handles frmEdit.ModeChanging
        If e.CancelingEdit OrElse e.Cancel Then
            GoBack()
        End If
    End Sub

    Protected Sub EvalDataSource_SelectObject(ByVal sender As Object, ByVal e As Csla.Web.SelectObjectArgs) Handles EvalDataSource.SelectObject
        e.BusinessObject = GetEval()
    End Sub

    Protected Sub EvalDataSource_InsertObject(ByVal sender As Object, ByVal e As Csla.Web.InsertObjectArgs) Handles EvalDataSource.InsertObject
        Dim obj As Evaluation = GetEval()
        Csla.Data.DataMapper.Map(e.Values, obj)
        SaveEval(obj)
    End Sub

    Protected Sub EvalDataSource_UpdateObject(ByVal sender As Object, ByVal e As Csla.Web.UpdateObjectArgs) Handles EvalDataSource.UpdateObject
        Dim obj As Evaluation = GetEval()
        Csla.Data.DataMapper.Map(e.Values, obj, "EvalID")
        SaveEval(obj)
    End Sub

    Protected Sub EvalDataSource_DeleteObject(ByVal sender As Object, ByVal e As Csla.Web.DeleteObjectArgs) Handles EvalDataSource.DeleteObject
        Try
            Evaluation.DeleteEvaluation(e.Keys("EvalID"))
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
        If String.IsNullOrEmpty(Session("Level2EvalID")) Then
            ' Go back to the list of evals since the user was adding a new eval
            Response.Redirect("Evals.aspx?ParentID=" & Request("ParentID") & "&ActiveOnly=" & Request("ActiveOnly"))
        Else
            Response.Redirect("Eval_Landing.aspx")
        End If
    End Sub

    Private Function GetEval() As Evaluation
        Dim businessObject As Object = Session("currentObject")

        If IsNothing(businessObject) OrElse Not TypeOf businessObject Is Evaluation Then
            Try
                Dim idString As String = Request("EvalID")
                Dim parentID As String = Session("Level1ID")

                If Not String.IsNullOrEmpty(idString) Then
                    businessObject = Evaluation.GetEvaluation(idString)
                ElseIf Not String.IsNullOrEmpty(parentID) Then
                    businessObject = Evaluation.NewEvaluation(parentID)
                Else
                    ' TODO - Create the Error page
                    Response.Redirect("Eval_Error.aspx")
                End If

                Session("currentObject") = businessObject
            Catch ex As System.Security.SecurityException
                ' TODO - Create the Error page
                Response.Redirect("Eval_Error.aspx?ErrorText=Security Exception!&ErrorDetails=" & Server.UrlEncode(ex.Message))
            End Try
        End If

        Return CType(businessObject, Evaluation)
    End Function

    Private Sub SaveEval(ByVal theEval As Evaluation)
        Try
            Session("currentObject") = theEval.Save()
            ' If the user just added a new eval, we need to set the
            ' level2 variable so that we load that eval, instead of
            ' returning to the list.
            Session("Level2EvalID") = GetEval().EvalID
            GoBack()
        Catch ex As Csla.Validation.ValidationException
            Dim message As New System.Text.StringBuilder
            message.AppendFormat("{0}:<br/>", ex.Message)

            If theEval.BrokenRulesCollection.Count = 1 Then
                message.AppendFormat("-{0}", theEval.BrokenRulesCollection(0).Description)
            Else
                For Each rule As Csla.Validation.BrokenRule In _
                    theEval.BrokenRulesCollection
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
        Response.Redirect("Form_Edit.aspx?EvalID=&ParentID=" & Session("Level2EvalID"))
    End Sub

    Protected Sub NewSubFolderButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("Folder_Edit.aspx?EvalID=&ParentID=" & Session("Level2EvalID"))
    End Sub

    Protected Sub PasteFormButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("PasteCopiedEval.aspx?ParentID=" & Session("Level2EvalID"))
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
                    Dim builder As New ShortNameBuilder(Session("Level2EvalID"))

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
            Case "EPaths"
                Try
                    Dim builder As New EPathBuilder(Session("Level2EvalID"))

                    If builder.BuildEPaths Then
                        Return True
                    Else
                        ' Didn't work and no error throw, so check if there is an error
                        ' we can throw
                        If Not IsNothing(builder.TheError) Then
                            Throw builder.TheError
                        Else
                            ' This should never be hit but just in case...
                            Throw New Exception("Unable to rebuild EGroups")
                        End If
                    End If
                Catch ex As Exception
                    Throw ex
                End Try
            Case "EGroups"
                Try
                    Dim builder As New EGroupBuilder(Session("Level2EvalID"))

                    If builder.BuildEGroups Then
                        Return True
                    Else
                        ' Didn't work and no error throw, so check if there is an error
                        ' we can throw
                        If Not IsNothing(builder.TheError) Then
                            Throw builder.TheError
                        Else
                            ' This should never be hit but just in case...
                            Throw New Exception("Unable to rebuild EGroups")
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
