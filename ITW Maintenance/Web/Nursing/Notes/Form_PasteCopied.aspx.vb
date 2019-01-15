Imports ITWMaintenance.Library.Nursing.Notes

Partial Class Nursing_Notes_Form_PasteCopied
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsNothing(Session("CopiedNursingNoteForm")) AndAlso Not String.IsNullOrEmpty(Request("ParentID")) Then
            Dim oldForm As NursingNoteForm = DirectCast(Session("CopiedNursingNoteForm"), NursingNoteForm)
            Dim newForm As NursingNoteForm = NursingNoteForm.NewNursingNoteForm(CType(Request("ParentID"), Integer))

            Try
                NursingNoteForm.CopyForm(oldForm, newForm)
                newForm = newForm.Save()

                ' Allow the copied form to be pasted multiple times
                'Session("CopiedEvalForm") = Nothing

                Response.Redirect("NursingNote_Landing.aspx?ShowNursingNoteID=" & newForm.FormID)
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
