Imports ITWMaintenance.Library.Exports
Imports ITWMaintenance.Library.Security

Partial Class Export_List
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

        End If
        ExportListGridView.DataBind()
    End Sub

    Private Function GetUserSecurity() As ExportUser
        Dim bo As Object = Session("CurrentObject")

        Dim userid As Integer = DirectCast(DirectCast(HttpContext.Current.Session("CslaPrincipal"), ITWPrincipal).Identity, ITWIdentity).UserID
        bo = ExportUser.GetUserSecurity(userid)

        ViewState("infosec") = bo.Infosecurity()
        ViewState("dept") = bo.Dept()
        ViewState("intakefacility") = bo.Intakefacility()

        Return DirectCast(bo, ExportUser)

    End Function

    Private Function GetExports() As ExportList
        Dim businessObject As Object = Session("CurrentObject")

        GetUserSecurity()

        Dim i As Integer = ViewState("infosec")
        Dim d As String = ViewState("dept")
        Dim f As Integer = ViewState("intakefacility")

        businessObject = ExportList.GetExportList(i, d, f)

        Return DirectCast(businessObject, ExportList)

    End Function


    Protected Sub exportListGridView_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ExportListGridView.RowDataBound
        ' ****Use this to add parameters to the export
        Dim Export As ReadonlyExport = Nothing
        Dim intake As Integer = ViewState("intakefacility")

        If Not IsNothing(e.Row.DataItem) Then
            Export = DirectCast(e.Row.DataItem, ReadonlyExport)

            e.Row.Attributes.Add("onclick", "window.location.href = 'Export_Export.aspx?ID= " & Export.ID & "&Intake= " & intake & "&Export=" & Export.CollectionName & "'; ")
            CType(e.Row.FindControl("ExportButton"), Button).OnClientClick = "window.location.href = 'Export_Export.aspx?ID=" & Export.ID & "&Intake= " & intake & "&Export=" & Export.CollectionName & "';"
        End If

    End Sub

    'Protected Sub NewButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AddButton.Click
    '    Response.Redirect("Export_Edit.aspx?addNew=true")
    'End Sub
    'Protected Sub export_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles 

    ' End Sub
    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

    End Sub

    Protected Sub EditFormView_SelectObject(ByVal sender As Object, ByVal e As Csla.Web.SelectObjectArgs) Handles ExportDataSource.SelectObject
        e.BusinessObject = GetExports()
    End Sub


    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles ExportListGridView.RowCommand
        Response.Redirect("Export_Edit.aspx?addNew=true")
    End Sub


End Class
