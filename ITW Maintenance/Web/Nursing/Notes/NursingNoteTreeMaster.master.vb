Imports ITWMaintenance.Library.Security
Imports ITWMaintenance.Library.Nursing.Notes
Imports ITWMaintenance.Library.Lookup
Imports System.Xml

Partial Class Nursing_Notes_NursingNoteTreeMaster
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
            Me.ActiveOnlyCheckBox.Checked = True
            Try
                If Not String.IsNullOrEmpty(Session("NursingNotesActiveOnly")) Then
                    Me.ActiveOnlyCheckBox.Checked = Session("NursingNotesActiveOnly")
                End If
            Catch
            End Try
        End If

        ' Bind the tree
        If Not Page.IsPostBack() Then
            ' Since we use the selected discipline from the DisciplineDropDownList, 
            ' we need to bind it first
            Me.DisciplineDropDownList.DataSource = DisciplinesWithNursingNotesList.GetDisciplines()
            Me.DisciplineDropDownList.DataBind()
            ' Let's default to "Nursing"
            Me.DisciplineDropDownList.SelectedValue = 6

            If Not String.IsNullOrEmpty(Session("ShowNursingNoteID")) Then
                Try
                    ' Try to default the discipline dropdown list based on the form
                    ' to show's discipline so the form will be in the tree
                    Dim NursingNote As ReadOnlyNursingNote = ReadOnlyNursingNote.GetInfo(Session("ShowNursingNoteID"))
                    Me.DisciplineDropDownList.SelectedValue = NursingNote.Discipline
                    Me.ActiveOnlyCheckBox.Checked = Not NursingNote.Inactive
                Catch ex As Exception
                End Try
            End If
        End If
        Dim tree As ReadOnlyNursingNoteTree = ReadOnlyNursingNoteTree.GetNursingNoteTree(CInt(Me.DisciplineDropDownList.SelectedValue), Me.ActiveOnlyCheckBox.Checked, False)

        Me.TreeXmlDataSource.Data = tree.TreeDataXML
        Me.TreeXmlDataSource.DataBind()

        ToggleNursingNoteLinkPanel.Visible = True

        ' Show the copied form's information (if a form has been copied)
        If Not IsNothing(Session("CopiedNursingNoteForm")) Then
            Dim FormInfo As NursingNoteForm = CType(Session("CopiedNursingNoteForm"), NursingNoteForm)

            CopiedFormPanel.Visible = True
            CopiedFormLabel.Text = "Copied: #" & FormInfo.FormID & " - " & FormInfo.FormName
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

        If Convert.ToBoolean(item.GetAttribute("Final")) Then
            e.Node.NavigateUrl = "Form_Edit.aspx?FormID=" & item.GetAttribute("ID")
            e.Node.ImageUrl = "~/Images/icodoc4.gif"
        Else
            e.Node.Text = "<span class=""NursingNoteFolder"" FolderID=""" & e.Node.Value & """>" & e.Node.Text & "</span>"
            e.Node.NavigateUrl = "Folder_Edit.aspx?FolderID=" & item.GetAttribute("ID")
            e.Node.ImageUrl = "~/Images/folderclosed.gif"
        End If

        ' If the current node is the node we need to show, 
        ' loop through its parents and expand them
        If Not String.IsNullOrEmpty(Session("ShowNursingNoteID")) AndAlso e.Node.Value = Session("ShowNursingNoteID") Then
            e.Node.Selected = True

            Dim parent As TreeNode = e.Node.Parent()

            Do While Not parent Is Nothing
                parent.Expanded = True

                parent = parent.Parent
            Loop
        End If
    End Sub

    Protected Sub ActiveOnlyCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ActiveOnlyCheckBox.CheckedChanged
        Session("NursingNotesActiveOnly") = Me.ActiveOnlyCheckBox.Checked
    End Sub

    Protected Sub ClearCopiedLinkButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClearCopiedLinkButton.Click
        Session("CopiedNursingNoteForm") = Nothing
        CopiedFormPanel.Visible = False
    End Sub

    Protected Sub AllDisciplinesCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles AllDisciplinesCheckBox.CheckedChanged
        Dim PreviouslySelected As Integer = Me.DisciplineDropDownList.SelectedValue

        If AllDisciplinesCheckBox.Checked Then
            Me.DisciplineDropDownList.DataSource = DisciplineList.GetTherapyDisciplines()
            Me.DisciplineDropDownList.DataBind()
        Else
            Me.DisciplineDropDownList.DataSource = DisciplinesWithNursingNotesList.GetDisciplines()
            Me.DisciplineDropDownList.DataBind()
        End If

        Try
            Me.DisciplineDropDownList.SelectedValue = PreviouslySelected
        Catch ex As Exception
            ' Rebind the tree so the data matches the new selection
            ' (which will default to the first item in the list)
            Dim tree As ReadOnlyNursingNoteTree = ReadOnlyNursingNoteTree.GetNursingNoteTree(CInt(Me.DisciplineDropDownList.SelectedValue), Me.ActiveOnlyCheckBox.Checked, False)

            Me.TreeXmlDataSource.Data = tree.TreeDataXML
            Me.TreeXmlDataSource.DataBind()
        End Try
    End Sub

    Protected Sub AddNewFolderLinkButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AddNewFolderLinkButton.Click
        Response.Redirect("~/Nursing/Notes/Folder_Edit.aspx?ParentID=0&Discipline=" & DisciplineDropDownList.SelectedValue)
    End Sub

End Class