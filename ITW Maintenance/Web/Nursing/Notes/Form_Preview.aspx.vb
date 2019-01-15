Imports ITWMaintenance.Library.Nursing.Notes

Partial Class Nursing_Notes_Form_Preview
    Inherits System.Web.UI.Page

    Protected Function GetFieldTypeValue(ByVal fieldTypeKey As String) As String
        Return NursingNoteFormFieldTypes.GetValueByKey(fieldTypeKey)
    End Function

    Protected Sub FormView1_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles FormView1.DataBound
        Dim label As Label

        For i As Integer = 1 To 20
            If i < 10 Then
                label = CType(Me.FormView1.FindControl("Field0" & i & "OptionsLabel"), Label)
            Else
                label = CType(Me.FormView1.FindControl("Field" & i & "OptionsLabel"), Label)
            End If

            If Not String.IsNullOrEmpty(label.Text) Then
                Dim options As String() = label.Text.Split(vbCrLf)

                For j = 0 To UBound(options)
                    If InStr(options(j), "|") > 0 Then
                        options(j) = "<span style=""text-decoration: underline;"">" & Replace(options(j), "|", "</span>&nbsp;-&nbsp;")
                    End If
                Next

                label.Text = String.Join("<br />", options)

                ' Show the table that holds this label
                If i < 10 Then
                    CType(Me.FormView1.FindControl("Field0" & i & "OptionsTable"), Table).Visible = True
                Else
                    CType(Me.FormView1.FindControl("Field" & i & "OptionsTable"), Table).Visible = True
                End If
            End If
        Next
    End Sub

End Class
