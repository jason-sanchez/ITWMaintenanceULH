
Partial Class Nursing_Notes_NursingNote_Landing
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not String.IsNullOrEmpty(Request("ShowNursingNoteID")) Then
            Session("ShowNursingNoteID") = Request("ShowNursingNoteID")
        End If
    End Sub

End Class
