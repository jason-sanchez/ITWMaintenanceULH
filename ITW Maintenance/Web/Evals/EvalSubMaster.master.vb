Imports ITWMaintenance.Library.Security
Imports ITWMaintenance.Library.Evaluations
Imports ITWMaintenance.Library.Evaluations.Forms
Imports System.Xml

Partial Class Evals_EvalSubMaster
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Duplicate this here since this runs before the MainMaster page
        If IsNothing(HttpContext.Current.Session("CslaPrincipal")) Then
            LogOff()
        Else
            Try
                If Not DirectCast(HttpContext.Current.Session("CslaPrincipal"), ITWPrincipal).Identity.IsAuthenticated Then
                    LogOff()
                End If
            Catch ex As Exception
                LogOff()
            End Try
        End If

        If Not Page.IsPostBack() Then
            Level1EvalsLink.NavigateUrl = "Evals.aspx?ParentID=" & Session("Level1ID")
            Level1EvalsLink.Text = ReadOnlyEvalLevel1Folder.GetEvalLevel1FolderInfo(Session("Level1ID")).FolderName

            If Not String.IsNullOrEmpty(Session("Level2EvalID")) Then
                Level2EvalLabel.Text = ReadOnlyEvaluation.GetEvalInfo(Session("Level2EvalID")).EvalName
            Else
                Level2EvalLabel.Text = "New Evaluation"
            End If

            Try
                Me.ActiveOnlyCheckBox.Checked = Session("EvalActiveOnly")
            Catch
                Me.ActiveOnlyCheckBox.Checked = True
            End Try
        End If

        If Not String.IsNullOrEmpty(Session("Level2EvalID")) Then
            Dim tree As ReadOnlyEvalTree = ReadOnlyEvalTree.GetEvalTree(Session("Level2EvalID"), Me.ActiveOnlyCheckBox.Checked)

            Me.TreeXmlDataSource.Data = tree.TreeDataXML
            Me.TreeXmlDataSource.DataBind()

            ToggleEvalLinkPanel.Visible = True
        Else
            Dim tree As ReadOnlyEvalTree = ReadOnlyEvalTree.GetEvalTree(0, Me.ActiveOnlyCheckBox.Checked)

            Me.TreeXmlDataSource.Data = tree.TreeDataXML
            Me.TreeXmlDataSource.DataBind()

            ToggleEvalLinkPanel.Visible = False
        End If

        If Not IsNothing(Session("CopiedEvalForm")) Then
            Dim FormInfo As EvalForm = CType(Session("CopiedEvalForm"), EvalForm)

            CopiedFormPanel.Visible = True
            CopiedFormLabel.Text = "Copied: #" & FormInfo.EvalID & " - " & FormInfo.EPath
        Else
            CopiedFormPanel.Visible = False
        End If
    End Sub

    Private Sub LogOff()
        ITWMaintenance.Library.Security.ITWPrincipal.Logout()
        Session("CslaPrincipal") = Csla.ApplicationContext.User
        Session.Abandon()
        FormsAuthentication.SignOut()
        FormsAuthentication.RedirectToLoginPage()
    End Sub

    Protected Sub TreeView1_TreeNodeDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles TreeView1.TreeNodeDataBound
        Dim item As XmlElement = CType(e.Node.DataItem, XmlElement)

        If item.GetAttribute("EvalLevel") = "2" Then
            e.Node.Text = "<span class=""EvalFolder"" EvalID=""" & e.Node.Value & """>" & e.Node.Text & "</span>"
            e.Node.NavigateUrl = "Eval_Edit.aspx?EvalID=" & item.GetAttribute("EvalID")
            e.Node.ImageUrl = "~/Images/folderclosed.gif"
        Else
            If Convert.ToBoolean(item.GetAttribute("EFinal")) Then
                e.Node.NavigateUrl = "Form_Edit.aspx?EvalID=" & item.GetAttribute("EvalID")
                e.Node.ImageUrl = "~/Images/icodoc4.gif"
            Else
                e.Node.Text = "<span class=""EvalFolder"" EvalID=""" & e.Node.Value & """>" & e.Node.Text & "</span>"
                e.Node.NavigateUrl = "Folder_Edit.aspx?EvalID=" & item.GetAttribute("EvalID")
                e.Node.ImageUrl = "~/Images/folderclosed.gif"
            End If
        End If

        ' If the current node is the node we need to show, 
        ' loop through its parents and expand them
        If Not String.IsNullOrEmpty(Session("ShowEvalID")) AndAlso e.Node.Value = Session("ShowEvalID") Then
            e.Node.Selected = True

            Dim parent As TreeNode = e.Node.Parent()

            Do While Not parent Is Nothing
                parent.Expanded = True

                parent = parent.Parent
            Loop
        End If
    End Sub

    Protected Sub ActiveOnlyCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ActiveOnlyCheckBox.CheckedChanged
        Session("EvalActiveOnly") = Me.ActiveOnlyCheckBox.Checked
    End Sub

    Protected Sub ClearCopiedLinkButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClearCopiedLinkButton.Click
        Session("CopiedEvalForm") = Nothing
        CopiedFormPanel.Visible = False
    End Sub

End Class

