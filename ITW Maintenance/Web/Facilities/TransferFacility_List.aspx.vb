Imports ITWMaintenance.Library.Facilities

Partial Class Facilities_TransferFacility_List
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            FacilityTypeDropDownList.DataSource = [Enum].GetNames(GetType(ReadOnlyTransferFacilityList.FacilityType))
            FacilityTypeDropDownList.DataBind()

            If TransferFacility.CanAddObject Then
                Me.AddButton.Visible = True
            End If

            If Not String.IsNullOrEmpty(Request("SearchText")) Then
                Me.SearchTextBox.Text = Request("SearchText")
            End If

            If Not String.IsNullOrEmpty(Request("ActiveOnly")) Then
                Try
                    Me.ActiveOnlyCheckBox.Checked = CBool(Request("ActiveOnly"))
                Catch ex As Exception
                End Try
            End If

            If Not String.IsNullOrEmpty(Request("FacilityType")) Then
                Try
                    Me.FacilityTypeDropDownList.SelectedValue = Request("FacilityType")
                Catch ex As Exception

                End Try
            End If
        End If

        FacilityListGridView.DataBind()
    End Sub

    Private Function GetFacilities() As ReadOnlyTransferFacilityList
        Dim businessObject As Object = Session("CurrentObject")

        If Not String.IsNullOrEmpty(SearchTextBox.Text) Then
            businessObject = ReadOnlyTransferFacilityList.GetFacilityList(SearchTextBox.Text)
        Else
            businessObject = ReadOnlyTransferFacilityList.GetFacilityList([Enum].Parse(GetType(ReadOnlyTransferFacilityList.FacilityType), FacilityTypeDropDownList.SelectedValue), ActiveOnlyCheckBox.Checked)
        End If
        Session("CurrentObject") = businessObject

        Return DirectCast(businessObject, ReadOnlyTransferFacilityList)
    End Function

    Protected Sub FacilityListGridView_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles FacilityListGridView.RowDataBound
        Dim facility As ReadOnlyTransferFacility = Nothing

        If Not IsNothing(e.Row.DataItem) Then
            If TransferFacility.CanEditObject Then
                facility = DirectCast(e.Row.DataItem, ReadOnlyTransferFacility)

                e.Row.Attributes.Add("onclick", "window.location.href='TransferFacility_Edit.aspx?ID=" & facility.ID & "&SearchText=" & Me.SearchTextBox.Text & "&ActiveOnly=" & Me.ActiveOnlyCheckBox.Checked & "&FacilityType=" & Me.FacilityTypeDropDownList.SelectedValue & "';")
                CType(e.Row.FindControl("EditButton"), Button).OnClientClick = "window.location.href='TransferFacility_Edit.aspx?ID=" & facility.ID & "&SearchText=" & Me.SearchTextBox.Text & "&ActiveOnly=" & Me.ActiveOnlyCheckBox.Checked & "&FacilityType=" & Me.FacilityTypeDropDownList.SelectedValue & "';"
            End If
        End If
    End Sub

    Protected Sub NewButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AddButton.Click
        Response.Redirect("TransferFacility_Edit.aspx?addNew=true&SearchText=" & Me.SearchTextBox.Text & "&ActiveOnly=" & Me.ActiveOnlyCheckBox.Checked & "&FacilityType=" & Me.FacilityTypeDropDownList.SelectedValue)
    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        ' You can only change the SkinID in the PreInit method of the Page
        Try
            If Not TransferFacility.CanEditObject Then
                'If the user can't edit the object, don't render this grid as "clickable"
                'HACK - We must do this because we're using Master Pages
                Dim m As System.Web.UI.MasterPage = Master
                FacilityListGridView.SkinID = ""
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub TransferFacilityListDataSource_SelectObject(ByVal sender As Object, ByVal e As Csla.Web.SelectObjectArgs) Handles TransferFacilityListDataSource.SelectObject
        e.BusinessObject = GetFacilities()
    End Sub

End Class
