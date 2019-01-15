Imports ITWMaintenance.Library.Security
Imports ITWMaintenance.Library.Interventions
Imports ITWMaintenance.Library.Interventions.Forms
Imports System.Xml

Partial Class IntVnts_IntVntSubMaster
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
            ParentDisciplineLabel.Text = "Interventions (" & ReadOnlyIntVntLevel1Folder.GetIntVntLevel1FolderInfo(Session("Level1ID")).Discipline & ")<br>"
            Level1IntVntsLink.NavigateUrl = "IntVnts.aspx?ParentID=" & Session("Level1ID")
            Level1IntVntsLink.Text = ReadOnlyIntVntLevel1Folder.GetIntVntLevel1FolderInfo(Session("Level1ID")).FolderName & "<sub><font size='0' color='grey'> 1</font></sub>"

            If Not String.IsNullOrEmpty(Session("Level2intVntID")) Then
                Level2IntVntLabel.Text = ReadOnlyIntervention.GetIntVntInfo(Session("Level2intVntID")).IntVntName
            Else
                Level2IntVntLabel.Text = "New Intervention"
            End If

            Try
                Me.ActiveOnlyCheckBox.Checked = Session("IntVntActiveOnly")
            Catch
                Me.ActiveOnlyCheckBox.Checked = True
            End Try
        End If

        If Not String.IsNullOrEmpty(Session("Level2intVntID")) Then
            Dim tree As ReadOnlyIntVntTree = ReadOnlyIntVntTree.GetIntVntTree(Session("Level2intVntID"), Me.ActiveOnlyCheckBox.Checked)

            Me.TreeXmlDataSource.Data = tree.TreeDataXML
            Me.TreeXmlDataSource.DataBind()

            ToggleIntVntLinkPanel.Visible = True
        Else
            Dim tree As ReadOnlyIntVntTree = ReadOnlyIntVntTree.GetIntVntTree(0, Me.ActiveOnlyCheckBox.Checked)

            Me.TreeXmlDataSource.Data = tree.TreeDataXML
            Me.TreeXmlDataSource.DataBind()

            ToggleIntVntLinkPanel.Visible = False
        End If

        If Not IsNothing(Session("CopiedIntVntForm")) Then
            Dim FormInfo As IntVntForm = CType(Session("CopiedIntVntForm"), IntVntForm)

            CopiedFormPanel.Visible = True
            CopiedFormLabel.Text = "Copied: #" & FormInfo.intVntID & " - " & FormInfo.iPath
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

        If item.GetAttribute("IntVntLevel") = "2" Then
            e.Node.Text = "<span class=""IntVntFolder"" intVntID=""" & e.Node.Value & """>" & e.Node.Text & "</span>"
            e.Node.NavigateUrl = "IntVnt_Edit.aspx?intVntID=" & item.GetAttribute("intVntID")
            e.Node.ImageUrl = "~/Images/folderclosed.gif"
        Else
            If Convert.ToBoolean(item.GetAttribute("iFinal")) Then
                e.Node.NavigateUrl = "Form_Edit.aspx?intVntID=" & item.GetAttribute("intVntID")
                e.Node.ImageUrl = "~/Images/icodoc4.gif"
            Else
                e.Node.Text = "<span class=""IntVntFolder"" intVntID=""" & e.Node.Value & """>" & e.Node.Text & "</span>"
                e.Node.NavigateUrl = "Folder_Edit.aspx?intVntID=" & item.GetAttribute("intVntID")
                e.Node.ImageUrl = "~/Images/folderclosed.gif"
            End If
        End If

        ' If the current node is the node we need to show, 
        ' loop through its parents and expand them
        If Not String.IsNullOrEmpty(Session("ShowintVntID")) AndAlso e.Node.Value = Session("ShowintVntID") Then
            e.Node.Selected = True

            Dim parent As TreeNode = e.Node.Parent()

            Do While Not parent Is Nothing
                parent.Expanded = True

                parent = parent.Parent
            Loop
        End If
    End Sub

    Protected Sub ActiveOnlyCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ActiveOnlyCheckBox.CheckedChanged
        Session("IntVntActiveOnly") = Me.ActiveOnlyCheckBox.Checked
    End Sub

    Protected Sub ClearCopiedLinkButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClearCopiedLinkButton.Click
        Session("CopiedIntVntForm") = Nothing
        CopiedFormPanel.Visible = False
    End Sub

End Class

