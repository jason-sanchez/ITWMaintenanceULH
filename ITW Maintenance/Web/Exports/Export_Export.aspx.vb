Imports ITWMaintenance.Library.Exports
Imports System.Data.SqlClient
Imports Csla.Validation
Imports System.Reflection
Imports ITWMaintenance.Library.Security
Imports System.Text
Imports System.IO


Partial Class Export_Export
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Session("CurrentObject") = Nothing

        Else
            Me.ErrorPanel.Visible = False
            Me.ErrorLabel.Text = ""
        End If

        Try
            Me.EFormView.Focus()
        Catch
        End Try

    End Sub

    Private Function GetExportParam() As ExportParameters
        Dim bo As Object = Session("CurrentObject")

        If IsNothing(bo) OrElse Not TypeOf bo Is ExportParameters Then
            Try
                Dim idstring As String = Request("ID")
                bo = ExportParameters.GetExportParam(idstring)

            Catch ex As Exception

            End Try
        End If

        ViewState("param1") = bo.param1()
        ViewState("param2") = bo.param2()
        ViewState("param3") = bo.param3()
        ViewState("param4") = bo.param4()
        ViewState("var1") = bo.var1()
        ViewState("var2") = bo.var2()
        ViewState("var3") = bo.var3()
        ViewState("var4") = bo.var4()
        ViewState("cObject") = bo.cObject()
        ViewState("cCatalog") = bo.cCatalog()
        ViewState("param1datatype") = bo.param1datatype()
        ViewState("param2datatype") = bo.param2datatype()
        ViewState("param3datatype") = bo.param3datatype()
        ViewState("param4datatype") = bo.param4datatype()
        ViewState("daterangelimit") = bo.daterangelimit()
        ViewState("intake") = Request("Intake")
        ViewState("ExportName") = Request("Export")

        Return DirectCast(bo, ExportParameters)

    End Function


    Public Sub ExportDataSource_SelectObject(ByVal sender As Object, ByVal e As Csla.Web.SelectObjectArgs) Handles ExportDataSource.SelectObject
        e.BusinessObject = GetExportParam()
    End Sub

    Public Sub HospSvcDataSource_SelectObject(ByVal sender As Object, ByVal e As Csla.Web.SelectObjectArgs) Handles HospSvcDataSource.SelectObject
        e.BusinessObject = GetHospSvc()
    End Sub

    Public Sub DisciplineDataSource_SelectObject(ByVal sender As Object, ByVal e As Csla.Web.SelectObjectArgs) Handles DisciplineDataSource.SelectObject
        e.BusinessObject = GetDiscipline()
    End Sub

    Public Sub submitBtn_Click(sender As Object, e As EventArgs) Handles submitBtn.Click
        Runexport()

    End Sub

    Public Sub Runexport()
        Dim bo As New Exportsp

        Try
            Dim p1 As String = CType(EFormView.FindControl("param1Txt"), TextBox).Text
            Dim p2 As String = CType(EFormView.FindControl("param2Txt"), TextBox).Text
            Dim p3 As String = CType(EFormView.FindControl("param3Drop"), DropDownList).SelectedValue
            Dim p4 As String = CType(EFormView.FindControl("param4Drop"), DropDownList).SelectedValue
            'Dim p3 As String = CType(EFormView.FindControl("param3Txt"), TextBox).Text
            Dim v1 As String = ViewState("var1")
            Dim v2 As String = ViewState("var2")
            Dim v3 As String = ViewState("var3")
            Dim v4 As String = ViewState("var4")
            Dim c1 As String = ViewState("cCatalog")
            Dim c2 As String = ViewState("cObject")
            Dim d1 As String = ViewState("param1datatype")
            Dim d2 As String = ViewState("param2datatype")
            Dim d3 As String = ViewState("param3datatype")
            Dim d4 As String = ViewState("param4datatype")
            Dim dr1 As Integer = ViewState("daterangelimit")
            Dim intake As Integer = ViewState("intake")


            bo = Exportsp.RunExport(dr1, p1, p2, p3, p4, v1, v2, v3, v4, c1, c2, d1, d2, d3, d4, intake)

            Response.Clear()
            Response.ContentType = "application/excel"
            Response.AddHeader("Content-Disposition", "attachment; filename=Report.csv")
            Response.Write(bo.stringb)
            Response.End()


        Catch tex As Threading.ThreadAbortException

        Catch ex As Exception

            Me.ErrorPanel.Visible = True
            Me.ErrorLabel.Text = ex.Message.ToString()

        End Try

    End Sub

    Protected Sub backBtn_Click(sender As Object, e As EventArgs) Handles backBtn.Click
        Response.Redirect("Export_List.aspx?SearchText=" & Request("SearchText") & "&ActiveOnly=" & Request("ActiveOnly") & "&FacilityType=" & Request("FacilityType"))
    End Sub

    Protected Sub EFormView_DataBound(sender As Object, e As EventArgs) Handles EFormView.DataBound
        Me.HeaderName.Text = ViewState("ExportName")


        If ViewState("param1") = "" Then
            DirectCast(EFormView.FindControl("param1Txt"), TextBox).Visible = False
            DirectCast(EFormView.FindControl("RequiredFieldValidator1"), RequiredFieldValidator).EnableClientScript = False
            Me.HeaderParam.Visible = False

        End If

        If ViewState("param2") = "" Then
            DirectCast(EFormView.FindControl("param2Txt"), TextBox).Visible = False
            DirectCast(EFormView.FindControl("RequiredFieldValidator2"), RequiredFieldValidator).Enabled = False
        End If

        If ViewState("param3") = "" Then
            DirectCast(EFormView.FindControl("param3Drop"), DropDownList).Visible = False
            DirectCast(EFormView.FindControl("RequiredFieldValidator3"), RequiredFieldValidator).Enabled = False
        Else
            DirectCast(EFormView.FindControl("param3Drop"), DropDownList).Items.Insert(0, New ListItem("--Select One --"))
            DirectCast(EFormView.FindControl("param3Drop"), DropDownList).SelectedIndex = 0
        End If

        If ViewState("param4") = "" Then
            DirectCast(EFormView.FindControl("param4Drop"), DropDownList).Visible = False
            DirectCast(EFormView.FindControl("RequiredFieldValidator4"), RequiredFieldValidator).Enabled = False
        Else
            DirectCast(EFormView.FindControl("param4Drop"), DropDownList).Items.Insert(0, New ListItem("--Select One --"))
            DirectCast(EFormView.FindControl("param4Drop"), DropDownList).SelectedIndex = 0
        End If



    End Sub

    Private Function GetDiscipline() As ExportDiscipline

        Dim bo As Object = Session("CurrentObject")

        bo = ExportDiscipline.GetDiscipline()

        Return DirectCast(bo, ExportDiscipline)

    End Function

    Private Function GetHospSvc() As ExportHospService
        Dim bo As Object = Session("CurrentObject")

        GetUserSecurity()

        Dim d As String = ViewState("dept")

        bo = ExportHospService.GetHospSvc(d)

        Return DirectCast(bo, ExportHospService)

    End Function
    Private Function GetUserSecurity() As ExportUser
        Dim bo As Object = Session("CurrentObject")

        Dim userid As Integer = DirectCast(DirectCast(HttpContext.Current.Session("CslaPrincipal"), ITWPrincipal).Identity, ITWIdentity).UserID
        bo = ExportUser.GetUserSecurity(userID)

        ViewState("dept") = bo.Dept()

        Return DirectCast(bo, ExportUser)

    End Function


End Class
