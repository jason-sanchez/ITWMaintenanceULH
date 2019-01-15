Imports ITWMaintenance.Library.Nursing
Imports ITWMaintenance.Library.Nursing.Notes
Imports System.Xml

Partial Class Nursing_NursingFormPicker
    Inherits System.Web.UI.UserControl

    Private Enum Views
        TreeView = 0
        SearchResults = 1
    End Enum

    Private _SelectedFormID As Integer = 0
    Public Event FormSelect(ByVal SelectedFormID As Integer)

    Public ReadOnly Property SelectedFormID() As Integer
        Get
            Return Me._SelectedFormID
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            SearchMultiView.ActiveViewIndex = Views.TreeView

            SortByDropDownList.DataSource = [Enum].GetNames(GetType(ReadOnlyNursingNoteSearchResultList.SortField))
            SortByDropDownList.DataBind()
        End If
    End Sub

    Protected Sub SearchButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchButton.Click
        If Not String.IsNullOrEmpty(SearchTextBox.Text) Then
            SearchResultDataSource.SelectParameters.Clear()
            Try
                SearchResultDataSource.SelectParameters.Add("FormID", CInt(SearchTextBox.Text))
            Catch ex As Exception
                ' The SearchText is not a number, so do a text search
                SearchResultDataSource.SelectParameters.Add("SearchText", SearchTextBox.Text)
            End Try

            SearchResultDataSource.SelectParameters.Add("SortBy", [Enum].Parse(GetType(ReadOnlyNursingNoteSearchResultList.SortField), SortByDropDownList.SelectedValue))
            SearchResultDataSource.SelectParameters.Add("OnlySearchForms", True)
            SearchResultsGridView.DataBind()

            SearchMultiView.ActiveViewIndex = Views.SearchResults
        Else
            SearchMultiView.ActiveViewIndex = Views.TreeView
        End If
    End Sub

    Protected Sub FormTreeView_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles FormTreeView.DataBinding
        ' Leave this code here!
        ' When this code was in the Page_Load(), the load for this user control wasn't firing before
        ' the tree was databound.  Thus, the datasource was empty and the tree had nothing to bind to.
        If IsNothing(TreeXmlDataSource.Data) OrElse String.IsNullOrEmpty(TreeXmlDataSource.Data) Then
            Dim tree As ReadOnlyNursingNoteTree = ReadOnlyNursingNoteTree.GetNursingNoteTree(6, Me.ActiveOnlyCheckBox.Checked, True)

            TreeXmlDataSource.Data = tree.TreeDataXML
            TreeXmlDataSource.DataBind()
        End If
    End Sub

    Protected Sub FormTreeView_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles FormTreeView.SelectedNodeChanged
        If Not IsNothing(FormTreeView.SelectedNode) Then
            If FormTreeView.SelectedNode.ChildNodes.Count = 0 Then
                ' Just because this item doesn't have children doesn't
                ' guarantee that it's a form, so we need to double-check before
                ' allowing the user to select it. Note that we can't use
                ' the DataItem of the Node since this fires on post-back, 
                ' and the DataItem only exists between the time the tree is DataBound
                ' until the page is rendered.
                Dim note As ReadOnlyNursingNote = ReadOnlyNursingNote.GetInfo(FormTreeView.SelectedNode.Value)

                If note.IsForm Then
                    Me._SelectedFormID = FormTreeView.SelectedNode.Value
                    RaiseEvent FormSelect(Me._SelectedFormID)
                End If
            End If
        End If
    End Sub

    Protected Sub SearchResultsGridView_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchResultsGridView.SelectedIndexChanged
        If Not IsNothing(SearchResultsGridView.SelectedValue) Then
            Me._SelectedFormID = SearchResultsGridView.SelectedValue
            RaiseEvent FormSelect(Me._SelectedFormID)
        End If
    End Sub

    Protected Sub FormTreeView_TreeNodeDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles FormTreeView.TreeNodeDataBound
        Dim item As XmlElement = CType(e.Node.DataItem, XmlElement)

        If Convert.ToBoolean(item.GetAttribute("Final")) Then
            e.Node.ImageUrl = "~/Images/icodoc4.gif"
        Else
            e.Node.ImageUrl = "~/Images/folderclosed.gif"
        End If

        If Convert.ToBoolean(item.GetAttribute("Inactive")) Then
            e.Node.Text &= " (inactive)"
        End If
    End Sub

    Protected Sub ActiveOnlyCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ActiveOnlyCheckBox.CheckedChanged
        TreeXmlDataSource.Data = ""
        FormTreeView.DataBind()
    End Sub

End Class
