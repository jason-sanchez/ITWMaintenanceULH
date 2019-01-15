﻿Imports ITWMaintenance.Library.Evaluations.Forms

Partial Class Evals_Form_PasteCopied
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsNothing(Session("CopiedEvalForm")) AndAlso Not String.IsNullOrEmpty(Request("ParentID")) Then
            Dim oldForm As EvalForm = DirectCast(Session("CopiedEvalForm"), EvalForm)
            Dim newForm As EvalForm = EvalForm.NewEvalForm(CType(Request("ParentID"), Integer))

            Try
                EvalForm.CopyEval(oldForm, newForm)
                newForm = newForm.Save()

                ' Allow the copied form to be pasted multiple times
                'Session("CopiedEvalForm") = Nothing

                Response.Redirect("Eval_Landing.aspx?ShowEvalID=" & newForm.EvalID)
            Catch ex As Csla.Validation.ValidationException
                Dim message As New System.Text.StringBuilder
                message.AppendFormat("{0}:<br/>", ex.Message)

                If newForm.BrokenRulesCollection.Count = 1 Then
                    message.AppendFormat("-{0}", newForm.BrokenRulesCollection(0).Description)
                Else
                    For Each rule As Csla.Validation.BrokenRule In _
                        newForm.BrokenRulesCollection
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
        End If
    End Sub

End Class
