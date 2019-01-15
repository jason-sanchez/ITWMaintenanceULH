
Partial Class IntVnts_IntVnt_Landing
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request("intVntID") > "" Then
            Session("Level2intVntID") = Request("intVntID")
        End If

        If Not String.IsNullOrEmpty(Request("IntVntActiveOnly")) Then
            Session("IntVntActiveOnly") = Request("IntVntActiveOnly")
        End If

        If Not String.IsNullOrEmpty(Request("ShowintVntID")) Then
            Session("ShowintVntID") = Request("ShowintVntID")
        Else
            Session("ShowintVntID") = Nothing
        End If

        ' Show the IntVnt Tree by default
        If Not ClientScript.IsStartupScriptRegistered("ShowTree") Then
            ClientScript.RegisterStartupScript(Me.GetType(), "ShowTree", "ShowIntVntTree();", True)
        End If
    End Sub

End Class
