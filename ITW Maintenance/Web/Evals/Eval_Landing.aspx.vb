
Partial Class Evals_Eval_Landing
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request("EvalID") > "" Then
            Session("Level2EvalID") = Request("EvalID")
        End If

        If Not String.IsNullOrEmpty(Request("EvalActiveOnly")) Then
            Session("EvalActiveOnly") = Request("EvalActiveOnly")
        End If

        If Not String.IsNullOrEmpty(Request("ShowEvalID")) Then
            Session("ShowEvalID") = Request("ShowEvalID")
        Else
            Session("ShowEvalID") = Nothing
        End If

        ' Show the Eval Tree by default
        If Not ClientScript.IsStartupScriptRegistered("ShowTree") Then
            ClientScript.RegisterStartupScript(Me.GetType(), "ShowTree", "ShowEvalTree();", True)
        End If
    End Sub

End Class
