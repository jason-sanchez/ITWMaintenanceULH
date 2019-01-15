Imports ITWMaintenance.Library.Interventions.Forms
Imports ITWMaintenance.Library.Interventions
Imports ITWMaintenance.Library.Interventions.Utilities

Partial Class IntVnts_Form_Preview
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ' Set the level 1 value here so we can return to the IntVnt list
            ' without having to go all the way back up.
            If Not String.IsNullOrEmpty(Request("ParentID")) Then
                Session("Level1ID") = Request("ParentID")
            End If

            DirectCast(Me.FormView1.FindControl("ParentDisciplineLabel"), Label).Text = ReadOnlyIntVntLevel1Folder.GetIntVntLevel1FolderInfo(Session("Level1ID")).Discipline
        End If
    End Sub

    Protected Sub FormView1_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles FormView1.DataBound
        If Not FormView1.DataItem Is Nothing Then
            ' Build the form table
            Dim FormInfo As ReadOnlyIntVntForm = DirectCast(FormView1.DataItem, ReadOnlyIntVntForm)
            Dim FieldLabel As String = Nothing
            Dim FieldType As String = Nothing
            Dim FieldOptions As String = Nothing
            Dim FieldHelpText As String = Nothing
            Dim FieldValidation As String = Nothing

            For i As Int16 = 1 To 20
                ' Set the variables using a helper function to make the code cleaner
                SetValues(FormInfo, i, FieldLabel, FieldType, FieldOptions, FieldHelpText, FieldValidation)

                ' Build the row, if the field has been defined
                If Not String.IsNullOrEmpty(FieldLabel) Then
                    Dim row As New TableRow
                    Dim cell As New TableCell
                    Dim label As New Label
                    Dim panel As Panel

                    cell.Style.Add("vertical-align", "top")

                    label.Text = i & ". " & FieldLabel
                    label.Font.Bold = True
                    cell.Controls.Add(label)

                    label = New Label
                    label.Text = " - " & FieldType
                    cell.Controls.Add(label)

                    If Not String.IsNullOrEmpty(FieldValidation) AndAlso Not FieldValidation.Contains("[0]") Then
                        panel = New Panel
                        panel.CssClass = "FormFieldDetails"

                        label = New Label
                        label.CssClass = "DetailsHeading"
                        label.Text = "Validation: "
                        panel.Controls.Add(label)

                        label = New Label
                        label.Text = FieldValidation
                        panel.Controls.Add(label)

                        cell.Controls.Add(panel)
                    End If

                    If Not String.IsNullOrEmpty(FieldHelpText) Then
                        panel = New Panel
                        panel.CssClass = "FormFieldDetails"

                        label = New Label
                        label.CssClass = "DetailsHeading"
                        label.Text = "Help Text: "
                        panel.Controls.Add(label)

                        label = New Label
                        label.Text = FieldHelpText
                        panel.Controls.Add(label)

                        cell.Controls.Add(panel)
                    End If

                    If Not String.IsNullOrEmpty(FieldOptions) Then
                        panel = New Panel
                        panel.CssClass = "FormFieldDetails"

                        label = New Label
                        label.CssClass = "DetailsHeading"
                        label.Text = "Options: "
                        panel.Controls.Add(label)

                        Dim optionsPanel As New Panel
                        optionsPanel.CssClass = "OptionsDiv"

                        Dim options As String() = FieldOptions.Split(vbCrLf)

                        For j = 0 To UBound(options)
                            If InStr(options(j), "|") > 0 Then
                                options(j) = "<span style=""text-decoration: underline;"">" & Replace(options(j), "|", "</span>&nbsp;-&nbsp;")
                            End If
                            If j = 0 Then
                                If String.IsNullOrEmpty(Trim(options(j))) Then
                                    options(j) = " &lt;blank by default&gt;"
                                Else
                                    options(j) &= " &lt;default&gt;"
                                End If
                            End If
                        Next

                        label = New Label
                        label.Text = String.Join("<br />", options)
                        optionsPanel.Controls.Add(label)
                        panel.Controls.Add(optionsPanel)

                        cell.Controls.Add(panel)
                    End If

                    row.Cells.Add(cell)
                    DirectCast(FormView1.FindControl("FormTable"), Table).Rows.Add(row)
                End If
            Next
        End If
    End Sub

    Private Sub SetValues(ByRef FormInfo As ReadOnlyIntVntForm, ByRef i As Int16, ByRef FieldLabel As String, ByRef FieldType As String, ByRef FieldOptions As String, ByRef FieldHelpText As String, ByRef FieldValidation As String)
        Select Case i
            Case 1
                FieldLabel = FormInfo.Field01Label
                FieldType = FormInfo.Field01Type
                FieldOptions = FormInfo.Field01Options
                FieldHelpText = FormInfo.Field01HelpText
                FieldValidation = FormInfo.Field01Validation
            Case 2
                FieldLabel = FormInfo.Field02Label
                FieldType = FormInfo.Field02Type
                FieldOptions = FormInfo.Field02Options
                FieldHelpText = FormInfo.Field02HelpText
                FieldValidation = FormInfo.Field02Validation
            Case 3
                FieldLabel = FormInfo.Field03Label
                FieldType = FormInfo.Field03Type
                FieldOptions = FormInfo.Field03Options
                FieldHelpText = FormInfo.Field03HelpText
                FieldValidation = FormInfo.Field03Validation
            Case 4
                FieldLabel = FormInfo.Field04Label
                FieldType = FormInfo.Field04Type
                FieldOptions = FormInfo.Field04Options
                FieldHelpText = FormInfo.Field04HelpText
                FieldValidation = FormInfo.Field04Validation
            Case 5
                FieldLabel = FormInfo.Field05Label
                FieldType = FormInfo.Field05Type
                FieldOptions = FormInfo.Field05Options
                FieldHelpText = FormInfo.Field05HelpText
                FieldValidation = FormInfo.Field05Validation
            Case 6
                FieldLabel = FormInfo.Field06Label
                FieldType = FormInfo.Field06Type
                FieldOptions = FormInfo.Field06Options
                FieldHelpText = FormInfo.Field06HelpText
                FieldValidation = FormInfo.Field06Validation
            Case 7
                FieldLabel = FormInfo.Field07Label
                FieldType = FormInfo.Field07Type
                FieldOptions = FormInfo.Field07Options
                FieldHelpText = FormInfo.Field07HelpText
                FieldValidation = FormInfo.Field07Validation
            Case 8
                FieldLabel = FormInfo.Field08Label
                FieldType = FormInfo.Field08Type
                FieldOptions = FormInfo.Field08Options
                FieldHelpText = FormInfo.Field08HelpText
                FieldValidation = FormInfo.Field08Validation
            Case 9
                FieldLabel = FormInfo.Field09Label
                FieldType = FormInfo.Field09Type
                FieldOptions = FormInfo.Field09Options
                FieldHelpText = FormInfo.Field09HelpText
                FieldValidation = FormInfo.Field09Validation
            Case 10
                FieldLabel = FormInfo.Field10Label
                FieldType = FormInfo.Field10Type
                FieldOptions = FormInfo.Field10Options
                FieldHelpText = FormInfo.Field10HelpText
                FieldValidation = FormInfo.Field10Validation
            Case 11
                FieldLabel = FormInfo.Field11Label
                FieldType = FormInfo.Field11Type
                FieldOptions = FormInfo.Field11Options
                FieldHelpText = FormInfo.Field11HelpText
                FieldValidation = FormInfo.Field11Validation
            Case 12
                FieldLabel = FormInfo.Field12Label
                FieldType = FormInfo.Field12Type
                FieldOptions = FormInfo.Field12Options
                FieldHelpText = FormInfo.Field12HelpText
                FieldValidation = FormInfo.Field12Validation
            Case 13
                FieldLabel = FormInfo.Field13Label
                FieldType = FormInfo.Field13Type
                FieldOptions = FormInfo.Field13Options
                FieldHelpText = FormInfo.Field13HelpText
                FieldValidation = FormInfo.Field13Validation
            Case 14
                FieldLabel = FormInfo.Field14Label
                FieldType = FormInfo.Field14Type
                FieldOptions = FormInfo.Field14Options
                FieldHelpText = FormInfo.Field14HelpText
                FieldValidation = FormInfo.Field14Validation
            Case 15
                FieldLabel = FormInfo.Field15Label
                FieldType = FormInfo.Field15Type
                FieldOptions = FormInfo.Field15Options
                FieldHelpText = FormInfo.Field15HelpText
                FieldValidation = FormInfo.Field15Validation
            Case 16
                FieldLabel = FormInfo.Field16Label
                FieldType = FormInfo.Field16Type
                FieldOptions = FormInfo.Field16Options
                FieldHelpText = FormInfo.Field16HelpText
                FieldValidation = FormInfo.Field16Validation
            Case 17
                FieldLabel = FormInfo.Field17Label
                FieldType = FormInfo.Field17Type
                FieldOptions = FormInfo.Field17Options
                FieldHelpText = FormInfo.Field17HelpText
                FieldValidation = FormInfo.Field17Validation
            Case 18
                FieldLabel = FormInfo.Field18Label
                FieldType = FormInfo.Field18Type
                FieldOptions = FormInfo.Field18Options
                FieldHelpText = FormInfo.Field18HelpText
                FieldValidation = FormInfo.Field18Validation
            Case 19
                FieldLabel = FormInfo.Field19Label
                FieldType = FormInfo.Field19Type
                FieldOptions = FormInfo.Field19Options
                FieldHelpText = FormInfo.Field19HelpText
                FieldValidation = FormInfo.Field19Validation
            Case 20
                FieldLabel = FormInfo.Field20Label
                FieldType = FormInfo.Field20Type
                FieldOptions = FormInfo.Field20Options
                FieldHelpText = FormInfo.Field20HelpText
                FieldValidation = FormInfo.Field20Validation
        End Select
    End Sub

End Class
