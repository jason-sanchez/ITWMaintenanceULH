Imports ITWMaintenance.Library.Nursing.Diagnoses

Partial Class Nursing_Diagnosis_List
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                If String.IsNullOrEmpty(Request("ActiveOnly")) Then
                    Me.ActiveOnlyCheckBox.Checked = True
                Else
                    Me.ActiveOnlyCheckBox.Checked = Convert.ToBoolean(Request("ActiveOnly"))
                End If
            Catch ex As Exception
                Me.ActiveOnlyCheckBox.Checked = True
            End Try
        End If
    End Sub

    Protected Sub DiagnosesGrid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DiagnosesGrid.RowCommand
        If e.CommandName = "EditItem" Then
            Response.Redirect("Diagnosis_Edit.aspx?ID=" & e.CommandArgument & "&ActiveOnly=" & Me.ActiveOnlyCheckBox.Checked)
        End If
    End Sub

    Protected Sub DiagnosesGrid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DiagnosesGrid.RowDataBound
        If e.Row.RowState = DataControlRowState.Normal OrElse e.Row.RowState = DataControlRowState.Alternate Then
            If Not e.Row.DataItem Is Nothing Then
                Dim theDiagnosis As ReadOnlyDiagnosis = CType(e.Row.DataItem, ReadOnlyDiagnosis)
                e.Row.Attributes.Add("onclick", "javascript:window.location.href='Diagnosis_Edit.aspx?ID=" & theDiagnosis.ID.ToString() & "&ActiveOnly=" & Me.ActiveOnlyCheckBox.Checked & "';")

                If theDiagnosis.Inactive Then
                    CType(e.Row.FindControl("InactiveCheckBox"), CheckBox).Checked = True
                End If
            End If
        End If
    End Sub

    Protected Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Response.Redirect("Diagnosis_Edit.aspx?ID=&ActiveOnly=" & Me.ActiveOnlyCheckBox.Checked)
    End Sub

End Class
