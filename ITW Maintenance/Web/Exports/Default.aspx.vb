Imports System
Imports System.IO
Imports System.Text
Imports System.Data
Imports System.Web.UI
'Imports System.Windows.Forms
Imports System.Configuration
'Imports Microsoft.Office.Interop.Excel
Imports System.Globalization
Imports ITWMaintenance.Library.Exports
Imports System.Data.SqlClient


Public Module GlobalVariables
    Public cDatabase As String
    Public cCatalog As String
    Public cObject As String
    Public param1data As String
    Public param2data As String
    Public param3data As String
    Public var1 As String
    Public var2 As String
    Public var3 As String
    Public dropselection As String
End Module

Public Class _Default
    Inherits System.Web.UI.Page
    Dim exp As Export
    Dim itm As Export
    Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("ITW").ConnectionString)


    Private Sub MesgBox(ByVal sMessage As String)
        Dim msg As String
        msg = "<script language='javascript'>"
        msg += "alert('" & sMessage & "');"
        msg += "<" & "/script>"
        Response.Write(msg)
    End Sub

    Public Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Not IsPostBack Then

            'Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("ITW12").ConnectionString)
            Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("ITW").ConnectionString)
                Dim sqlcomm As New SqlCommand("Select * from [105infoCollection] where id=@id ", conn)
                sqlcomm.Parameters.Add("@id", SqlDbType.BigInt).Value = 1

                conn.Open()
                Dim reader As SqlDataReader = sqlcomm.ExecuteReader()
                If reader.Read() Then
                    If reader("param1").ToString() = "NA" Or reader("param1") = "null" Then
                        Label1.Visible = False
                        TextBox1.Visible = False
                        Label1.Enabled = False
                        TextBox1.Enabled = False
                    Else
                        TextBox1.Visible = True
                        Label1.Visible = True
                        Label1.Enabled = True
                        TextBox1.Enabled = True
                        Label1.Text = reader("param1").ToString()
                    End If

                    If reader("param2").ToString() = "NA" Or reader("param2") = "null" Then
                        Label2.Visible = False
                        TextBox2.Visible = False
                        Label2.Enabled = False
                        TextBox2.Enabled = False
                    Else
                        Label2.Visible = True
                        TextBox2.Visible = True
                        Label2.Enabled = True
                        TextBox2.Enabled = True
                        Label2.Text = reader("param2").ToString()
                    End If

                    If reader("param3").ToString() = "NA" Or reader("param3") = "null" Then
                        Label3.Visible = False
                        TextBox3.Visible = False
                        Label3.Enabled = False
                        TextBox3.Enabled = False
                    Else
                        Label3.Visible = True
                        TextBox3.Visible = True
                        Label3.Enabled = True
                        TextBox3.Enabled = True
                        Label3.Text = reader("param3").ToString()
                    End If

                End If



                'itm.cDataBase = reader("cDataBase").ToString()
                'exp.cCatalog = reader("cDataBase").ToString()
                'exp.cObject = reader("cObject").ToString()

                'exp.param1datatype = reader("Param1DataType").ToString()
                'exp.param2datatype = reader("Param2DataType").ToString()
                'exp.param3datatype = reader("Param3DataType").ToString()

                'exp.var1 = reader("var1").ToString()
                'exp.var2 = reader("var2").ToString()
                'exp.var3 = reader("var3").ToString()


                'GlobalVariables.cDatabase = reader("cDataBase").ToString()
                'GlobalVariables.cCatalog = reader("cCatalog").ToString()
                'GlobalVariables.cObject = reader("cObject").ToString()

                'GlobalVariables.param1data = reader("Param1DataType").ToString()
                'GlobalVariables.param2data = reader("Param2DataType").ToString()
                'GlobalVariables.param3data = reader("Param3DataType").ToString()

                'GlobalVariables.var1 = reader("var1").ToString()
                'GlobalVariables.var2 = reader("var2").ToString()
                'GlobalVariables.var3 = reader("var3").ToString()

            End Using

        End If

    End Sub

    Public Sub collectionDrop_SelectedIndexChanged(sender As Object, e As EventArgs) Handles collectionDrop.SelectedIndexChanged
        Try
            Dim dselect As Integer = Convert.ToInt32(collectionDrop.SelectedIndex) + 1

            bindlabels(dselect)

            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Message", "alert('Error occured : " & ex.Message.ToString() & "');", True)
            MesgBox(ex.ToString())
        End Try

    End Sub

    Public Sub bindlabels(dselect As Int32)
        Dim sdeselect As String = Convert.ToString(dselect)
        GlobalVariables.dropselection = dselect
        Dim dt As New System.Data.DataTable()
        Dim adp As New SqlDataAdapter()
        Try

            Dim cmd As New SqlCommand("Select * from [105infoCollection] where id=" & dselect & " ", con)
            adp.SelectCommand = cmd
            adp.Fill(dt)

            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("param1").ToString() = "NA" Or dt.Rows(0)("param1") = "null" Then
                    Label1.Visible = False
                    TextBox1.Visible = False
                    Label1.Enabled = False
                    TextBox1.Enabled = False
                Else
                    Label1.Visible = True
                    Label1.Enabled = True
                    TextBox1.Visible = True
                    TextBox1.Enabled = True
                    Label1.Text = dt.Rows(0)("param1").ToString()
                End If

                If dt.Rows(0)("param2").ToString() = "NA" Or dt.Rows(0)("param2") = "null" Then
                    Label2.Visible = False
                    TextBox2.Visible = False
                    Label2.Enabled = False
                    TextBox2.Enabled = False
                Else
                    Label2.Visible = True
                    TextBox2.Visible = True
                    Label2.Enabled = True
                    TextBox2.Enabled = True
                    Label2.Text = dt.Rows(0)("param2").ToString()
                End If

                If dt.Rows(0)("param3").ToString() = "NA" Or dt.Rows(0)("param3") = "null" Then
                    Label3.Visible = False
                    TextBox3.Visible = False
                    Label3.Enabled = False
                    TextBox3.Enabled = False
                Else
                    Label3.Visible = True
                    TextBox3.Visible = True
                    Label3.Enabled = True
                    TextBox3.Enabled = True
                    Label3.Text = dt.Rows(0)("param3").ToString()
                End If

                If dt.Rows(0)("param1DataType") = "datetime" Then
                    DateValidator1.Enabled = True
                Else
                    DateValidator1.Enabled = False
                End If

                If dt.Rows(0)("param2DataType") = "datetime" Then
                    DateValidator2.Enabled = True
                Else
                    DateValidator2.Enabled = False
                End If

                If dt.Rows(0)("param1DataType") = "datetime" And dt.Rows(0)("param2DataType") = "datetime" Then
                    CompareValidator1.Enabled = True
                Else
                    CompareValidator1.Enabled = False

                End If

            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Message", "alert('Error occured : " & ex.Message.ToString() & "');", True)

        Finally

            GlobalVariables.cDatabase = dt.Rows(0)("cDataBase").ToString()
            GlobalVariables.cCatalog = dt.Rows(0)("cCatalog").ToString()
            GlobalVariables.cObject = dt.Rows(0)("cObject").ToString()

            GlobalVariables.param1data = dt.Rows(0)("Param1DataType").ToString()
            GlobalVariables.param2data = dt.Rows(0)("Param2DataType").ToString()
            GlobalVariables.param3data = dt.Rows(0)("Param3DataType").ToString()

            GlobalVariables.var1 = dt.Rows(0)("var1").ToString()
            GlobalVariables.var2 = dt.Rows(0)("var2").ToString()
            GlobalVariables.var3 = dt.Rows(0)("var3").ToString()


            dt.Clear()
            dt.Dispose()
            adp.Dispose()
        End Try
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        ' Dim sqlstring As String = "server='" & cDatabase & "';database='" & cCatalog & "';uid=sa;pwd=password"
        'Dim sqlstring As String = "server='" & cDatabase & "';database='" & cCatalog & "';uid=sysmax;pwd=sysmax"
        Dim sqlstring As String = "server='" & cDatabase & "';database='" & cCatalog & "';uid=sysmax;pwd=Condor!"
        Dim dat As New System.Data.DataTable()
        Dim adp As New SqlDataAdapter()

        Using conn As New SqlConnection(sqlstring)

            Dim sqlcomm As New SqlCommand("Select * from [105infoCollection] where id = '" & dropselection & "'", conn)

            Dim dbo As String = "dbo."
            Dim commtext As String = String.Concat(dbo, cObject)

            sqlcomm.CommandText = commtext
            sqlcomm.CommandType = CommandType.StoredProcedure

            If TextBox1.Text <> "" Then
                sqlcomm.Parameters.AddWithValue(var1, TextBox1.Text)
            End If

            If TextBox2.Text <> "" Then
                sqlcomm.Parameters.AddWithValue(var2, TextBox2.Text)
            End If

            If TextBox3.Text <> "" Then
                sqlcomm.Parameters.AddWithValue(var3, TextBox3.Text)
            End If

            If param1data = "datetime" And param2data = "datetime" Then

                If Date.Parse(TextBox2.Text, CultureInfo.InvariantCulture) > Date.Parse(TextBox1.Text, CultureInfo.InvariantCulture).AddDays(60) Then
                    MesgBox("Date Range Cannot exceed 60 days!")
                Else

                    Try
                        conn.Open()

                        Dim dt As New System.Data.DataTable()
                        Dim DataAdapter As SqlDataAdapter = New SqlDataAdapter
                        DataAdapter.SelectCommand = sqlcomm
                        DataAdapter.Fill(dt)

                        Response.Clear()
                        Response.ContentType = "application/excel"
                        Response.AddHeader("Content-Disposition", "attachment; filename=Report.csv")

                        Dim stringb As New StringBuilder()
                        For c As Integer = 0 To dt.Columns.Count - 1
                            stringb.Append(dt.Columns(c).ColumnName + ",")
                        Next
                        stringb.Append(Environment.NewLine)

                        For r As Integer = 0 To dt.Rows.Count - 1
                            For k As Integer = 0 To dt.Columns.Count - 1
                                stringb.Append(dt.Rows(r)(k).ToString().Replace(",", "") + ",")
                            Next
                            stringb.Append(Environment.NewLine)
                        Next
                        Response.Write(stringb.ToString())
                        conn.Close()
                        Response.End()


                    Catch ex As Exception

                        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Message", "alert('Error occured : " & ex.Message.ToString() & "');", True)
                        MesgBox(ex.ToString())

                    Finally


                    End Try
                End If
            Else
                Try
                    conn.Open()

                    Dim dt As New System.Data.DataTable()
                    Dim DataAdapter As SqlDataAdapter = New SqlDataAdapter
                    DataAdapter.SelectCommand = sqlcomm
                    DataAdapter.Fill(dt)

                    Response.Clear()
                    Response.ContentType = "application/excel"
                    Response.AddHeader("Content-Disposition", "attachment; filename=Report.csv")

                    Dim stringb As New StringBuilder()
                    For c As Integer = 0 To dt.Columns.Count - 1
                        stringb.Append(dt.Columns(c).ColumnName + ",")
                    Next
                    stringb.Append(Environment.NewLine)

                    For r As Integer = 0 To dt.Rows.Count - 1
                        For k As Integer = 0 To dt.Columns.Count - 1
                            stringb.Append(dt.Rows(r)(k).ToString().Replace(",", "") + ",")
                        Next
                        stringb.Append(Environment.NewLine)
                    Next
                    Response.Write(stringb.ToString())
                    conn.Close()
                    Response.End()

                Catch ex As Exception

                    ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Message", "alert('Error occured : " & ex.Message.ToString() & "');", True)
                    MesgBox(ex.ToString())

                Finally

                End Try
            End If
        End Using
    End Sub


End Class

