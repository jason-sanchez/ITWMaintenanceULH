Imports ITWMaintenance.Library.Interventions
Imports ITWMaintenance.Library.Interventions.Forms
Imports ITWMaintenance.Library.Interventions.Folders

Partial Class IntVnts_Form_Edit
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("currentObject") = Nothing
            ApplyAuthorizationRules()
        ElseIf Not frmEdit.CurrentMode = FormViewMode.ReadOnly Then
            Me.pnlError.Visible = False
            Me.lblError.Text = ""

            Dim theForm As IntVntForm = GetForm()

            ' Before processing the event, we need to pull the form changes
            ' out of the FormView and put them into the object.
            theForm.FormName = CType(frmEdit.FindControl("FormNameTextBox"), TextBox).Text
            theForm.ReportDescription = CType(frmEdit.FindControl("ReportDescriptionTextBox"), TextBox).Text
            ''theForm.IntVntClass = CType(frmEdit.FindControl("IntVntClassDropDownList"), DropDownList).SelectedValue
            ''theForm.Calculated = CType(frmEdit.FindControl("CalculatedCheckBox"), CheckBox).Checked
            ''theForm.Calculation = CType(frmEdit.FindControl("CalculationTextBox"), TextBox).Text
            theForm.Custom = CType(frmEdit.FindControl("CustomCheckBox"), CheckBox).Checked
            theForm.ShortName = CType(frmEdit.FindControl("ShortNameTextBox"), TextBox).Text

            Try
                theForm.DisplayOrder = CType(frmEdit.FindControl("DisplayOrderTextBox"), TextBox).Text
                theForm.Education = CType(frmEdit.FindControl("EducationCheckBox"), CheckBox).Checked
            Catch
                theForm.DisplayOrder = 0
                theForm.Education = False
            End Try
            'theForm.Education = CType(frmEdit.FindControl("EducationCheckBox"), CheckBox).Checked
            theForm.Inactive = CType(frmEdit.FindControl("InactiveCheckBox"), CheckBox).Checked
            ''theForm.Problem = CType(frmEdit.FindControl("ProblemCheckBox"), CheckBox).Checked
            ''theForm.PatientGoal = CType(frmEdit.FindControl("PatientGoalCheckBox"), CheckBox).Checked
            theForm.Billing = CType(frmEdit.FindControl("BillingCheckBox"), CheckBox).Checked
            ''theForm.FunctionalObjectiveFinding = CType(frmEdit.FindControl("FunctionalObjectiveFindingCheckBox"), CheckBox).Checked
            ''theForm.RequiredForPediatrics = CType(frmEdit.FindControl("RequiredForPediatricsCheckBox"), CheckBox).Checked
            ''theForm.Subjective = CType(frmEdit.FindControl("SubjectiveCheckBox"), CheckBox).Checked
            ''theForm.Test = CType(frmEdit.FindControl("TestCheckBox"), CheckBox).Checked

            theForm.Field01Label = CType(frmEdit.FindControl("Field01LabelTextBox"), TextBox).Text
            theForm.Field01Type = CType(frmEdit.FindControl("Field01TypeDropDown"), DropDownList).SelectedValue
            theForm.Field01Options = CType(frmEdit.FindControl("Field01OptionsTextBox"), TextBox).Text
            theForm.Field01HelpText = CType(frmEdit.FindControl("Field01HelpTextBox"), TextBox).Text
            theForm.Field01Validation = CType(frmEdit.FindControl("Field01ValidationDropDown"), DropDownList).SelectedValue


            theForm.Field02Label = CType(frmEdit.FindControl("Field02LabelTextBox"), TextBox).Text
            theForm.Field02Type = CType(frmEdit.FindControl("Field02TypeDropDown"), DropDownList).SelectedValue
            theForm.Field02Options = CType(frmEdit.FindControl("Field02OptionsTextBox"), TextBox).Text
            theForm.Field02HelpText = CType(frmEdit.FindControl("Field02HelpTextBox"), TextBox).Text
            theForm.Field02Validation = CType(frmEdit.FindControl("Field02ValidationDropDown"), DropDownList).SelectedValue

            theForm.Field03Label = CType(frmEdit.FindControl("Field03LabelTextBox"), TextBox).Text
            theForm.Field03Type = CType(frmEdit.FindControl("Field03TypeDropDown"), DropDownList).SelectedValue
            theForm.Field03Options = CType(frmEdit.FindControl("Field03OptionsTextBox"), TextBox).Text
            theForm.Field03HelpText = CType(frmEdit.FindControl("Field03HelpTextBox"), TextBox).Text
            theForm.Field03Validation = CType(frmEdit.FindControl("Field03ValidationDropDown"), DropDownList).SelectedValue

            theForm.Field04Label = CType(frmEdit.FindControl("Field04LabelTextBox"), TextBox).Text
            theForm.Field04Type = CType(frmEdit.FindControl("Field04TypeDropDown"), DropDownList).SelectedValue
            theForm.Field04Options = CType(frmEdit.FindControl("Field04OptionsTextBox"), TextBox).Text
            theForm.Field04HelpText = CType(frmEdit.FindControl("Field04HelpTextBox"), TextBox).Text
            theForm.Field04Validation = CType(frmEdit.FindControl("Field04ValidationDropDown"), DropDownList).SelectedValue

            theForm.Field05Label = CType(frmEdit.FindControl("Field05LabelTextBox"), TextBox).Text
            theForm.Field05Type = CType(frmEdit.FindControl("Field05TypeDropDown"), DropDownList).SelectedValue
            theForm.Field05Options = CType(frmEdit.FindControl("Field05OptionsTextBox"), TextBox).Text
            theForm.Field05HelpText = CType(frmEdit.FindControl("Field05HelpTextBox"), TextBox).Text
            theForm.Field05Validation = CType(frmEdit.FindControl("Field05ValidationDropDown"), DropDownList).SelectedValue

            theForm.Field06Label = CType(frmEdit.FindControl("Field06LabelTextBox"), TextBox).Text
            theForm.Field06Type = CType(frmEdit.FindControl("Field06TypeDropDown"), DropDownList).SelectedValue
            theForm.Field06Options = CType(frmEdit.FindControl("Field06OptionsTextBox"), TextBox).Text
            theForm.Field06HelpText = CType(frmEdit.FindControl("Field06HelpTextBox"), TextBox).Text
            theForm.Field06Validation = CType(frmEdit.FindControl("Field06ValidationDropDown"), DropDownList).SelectedValue

            theForm.Field07Label = CType(frmEdit.FindControl("Field07LabelTextBox"), TextBox).Text
            theForm.Field07Type = CType(frmEdit.FindControl("Field07TypeDropDown"), DropDownList).SelectedValue
            theForm.Field07Options = CType(frmEdit.FindControl("Field07OptionsTextBox"), TextBox).Text
            theForm.Field07HelpText = CType(frmEdit.FindControl("Field07HelpTextBox"), TextBox).Text
            theForm.Field07Validation = CType(frmEdit.FindControl("Field07ValidationDropDown"), DropDownList).SelectedValue

            theForm.Field08Label = CType(frmEdit.FindControl("Field08LabelTextBox"), TextBox).Text
            theForm.Field08Type = CType(frmEdit.FindControl("Field08TypeDropDown"), DropDownList).SelectedValue
            theForm.Field08Options = CType(frmEdit.FindControl("Field08OptionsTextBox"), TextBox).Text
            theForm.Field08HelpText = CType(frmEdit.FindControl("Field08HelpTextBox"), TextBox).Text
            theForm.Field08Validation = CType(frmEdit.FindControl("Field08ValidationDropDown"), DropDownList).SelectedValue

            theForm.Field09Label = CType(frmEdit.FindControl("Field09LabelTextBox"), TextBox).Text
            theForm.Field09Type = CType(frmEdit.FindControl("Field09TypeDropDown"), DropDownList).SelectedValue
            theForm.Field09Options = CType(frmEdit.FindControl("Field09OptionsTextBox"), TextBox).Text
            theForm.Field09HelpText = CType(frmEdit.FindControl("Field09HelpTextBox"), TextBox).Text
            theForm.Field09Validation = CType(frmEdit.FindControl("Field09ValidationDropDown"), DropDownList).SelectedValue

            theForm.Field10Label = CType(frmEdit.FindControl("Field10LabelTextBox"), TextBox).Text
            theForm.Field10Type = CType(frmEdit.FindControl("Field10TypeDropDown"), DropDownList).SelectedValue
            theForm.Field10Options = CType(frmEdit.FindControl("Field10OptionsTextBox"), TextBox).Text
            theForm.Field10HelpText = CType(frmEdit.FindControl("Field10HelpTextBox"), TextBox).Text
            theForm.Field10Validation = CType(frmEdit.FindControl("Field10ValidationDropDown"), DropDownList).SelectedValue


            theForm.Field11Label = CType(frmEdit.FindControl("Field11LabelTextBox"), TextBox).Text
            theForm.Field11Type = CType(frmEdit.FindControl("Field11TypeDropDown"), DropDownList).SelectedValue
            theForm.Field11Options = CType(frmEdit.FindControl("Field11OptionsTextBox"), TextBox).Text
            theForm.Field11HelpText = CType(frmEdit.FindControl("Field11HelpTextBox"), TextBox).Text
            theForm.Field11Validation = CType(frmEdit.FindControl("Field11ValidationDropDown"), DropDownList).SelectedValue

            theForm.Field12Label = CType(frmEdit.FindControl("Field12LabelTextBox"), TextBox).Text
            theForm.Field12Type = CType(frmEdit.FindControl("Field12TypeDropDown"), DropDownList).SelectedValue
            theForm.Field12Options = CType(frmEdit.FindControl("Field12OptionsTextBox"), TextBox).Text
            theForm.Field12HelpText = CType(frmEdit.FindControl("Field12HelpTextBox"), TextBox).Text
            theForm.Field12Validation = CType(frmEdit.FindControl("Field12ValidationDropDown"), DropDownList).SelectedValue

            theForm.Field13Label = CType(frmEdit.FindControl("Field13LabelTextBox"), TextBox).Text
            theForm.Field13Type = CType(frmEdit.FindControl("Field13TypeDropDown"), DropDownList).SelectedValue
            theForm.Field13Options = CType(frmEdit.FindControl("Field13OptionsTextBox"), TextBox).Text
            theForm.Field13HelpText = CType(frmEdit.FindControl("Field13HelpTextBox"), TextBox).Text
            theForm.Field13Validation = CType(frmEdit.FindControl("Field13ValidationDropDown"), DropDownList).SelectedValue

            theForm.Field14Label = CType(frmEdit.FindControl("Field14LabelTextBox"), TextBox).Text
            theForm.Field14Type = CType(frmEdit.FindControl("Field14TypeDropDown"), DropDownList).SelectedValue
            theForm.Field14Options = CType(frmEdit.FindControl("Field14OptionsTextBox"), TextBox).Text
            theForm.Field14HelpText = CType(frmEdit.FindControl("Field14HelpTextBox"), TextBox).Text
            theForm.Field14Validation = CType(frmEdit.FindControl("Field14ValidationDropDown"), DropDownList).SelectedValue

            theForm.Field15Label = CType(frmEdit.FindControl("Field15LabelTextBox"), TextBox).Text
            theForm.Field15Type = CType(frmEdit.FindControl("Field15TypeDropDown"), DropDownList).SelectedValue
            theForm.Field15Options = CType(frmEdit.FindControl("Field15OptionsTextBox"), TextBox).Text
            theForm.Field15HelpText = CType(frmEdit.FindControl("Field15HelpTextBox"), TextBox).Text
            theForm.Field15Validation = CType(frmEdit.FindControl("Field15ValidationDropDown"), DropDownList).SelectedValue

            theForm.Field16Label = CType(frmEdit.FindControl("Field16LabelTextBox"), TextBox).Text
            theForm.Field16Type = CType(frmEdit.FindControl("Field16TypeDropDown"), DropDownList).SelectedValue
            theForm.Field16Options = CType(frmEdit.FindControl("Field16OptionsTextBox"), TextBox).Text
            theForm.Field16HelpText = CType(frmEdit.FindControl("Field16HelpTextBox"), TextBox).Text
            theForm.Field16Validation = CType(frmEdit.FindControl("Field16ValidationDropDown"), DropDownList).SelectedValue

            theForm.Field17Label = CType(frmEdit.FindControl("Field17LabelTextBox"), TextBox).Text
            theForm.Field17Type = CType(frmEdit.FindControl("Field17TypeDropDown"), DropDownList).SelectedValue
            theForm.Field17Options = CType(frmEdit.FindControl("Field17OptionsTextBox"), TextBox).Text
            theForm.Field17HelpText = CType(frmEdit.FindControl("Field17HelpTextBox"), TextBox).Text
            theForm.Field17Validation = CType(frmEdit.FindControl("Field17ValidationDropDown"), DropDownList).SelectedValue

            theForm.Field18Label = CType(frmEdit.FindControl("Field18LabelTextBox"), TextBox).Text
            theForm.Field18Type = CType(frmEdit.FindControl("Field18TypeDropDown"), DropDownList).SelectedValue
            theForm.Field18Options = CType(frmEdit.FindControl("Field18OptionsTextBox"), TextBox).Text
            theForm.Field18HelpText = CType(frmEdit.FindControl("Field18HelpTextBox"), TextBox).Text
            theForm.Field18Validation = CType(frmEdit.FindControl("Field18ValidationDropDown"), DropDownList).SelectedValue

            theForm.Field19Label = CType(frmEdit.FindControl("Field19LabelTextBox"), TextBox).Text
            theForm.Field19Type = CType(frmEdit.FindControl("Field19TypeDropDown"), DropDownList).SelectedValue
            theForm.Field19Options = CType(frmEdit.FindControl("Field19OptionsTextBox"), TextBox).Text
            theForm.Field19HelpText = CType(frmEdit.FindControl("Field19HelpTextBox"), TextBox).Text
            theForm.Field19Validation = CType(frmEdit.FindControl("Field19ValidationDropDown"), DropDownList).SelectedValue

            theForm.Field20Label = CType(frmEdit.FindControl("Field20LabelTextBox"), TextBox).Text
            theForm.Field20Type = CType(frmEdit.FindControl("Field20TypeDropDown"), DropDownList).SelectedValue
            theForm.Field20Options = CType(frmEdit.FindControl("Field20OptionsTextBox"), TextBox).Text
            theForm.Field20HelpText = CType(frmEdit.FindControl("Field20HelpTextBox"), TextBox).Text
            theForm.Field20Validation = CType(frmEdit.FindControl("Field20ValidationDropDown"), DropDownList).SelectedValue


            theForm.Locked = CType(frmEdit.FindControl("LockedCheckBox"), CheckBox).Checked
            theForm.LockNotes = CType(frmEdit.FindControl("LockNotesTextBox"), TextBox).Text
        End If

        Try
            Me.frmEdit.FindControl("FormNameTextBox").Focus()
        Catch
        End Try

        If Me.frmEdit.CurrentMode = FormViewMode.ReadOnly Then
            DirectCast(Me.frmEdit.FindControl("ParentDisciplineLabel"), Label).Text = ReadOnlyIntVntLevel1Folder.GetIntVntLevel1FolderInfo(Session("Level1ID")).Discipline
        End If

        If Not String.IsNullOrEmpty(Request("intVntID")) Then
            Session("ShowintVntID") = Request("intVntID")
        ElseIf Not String.IsNullOrEmpty(Request("ParentID")) Then
            Session("ShowintVntID") = Request("ParentID")
        End If

        ' Hide the IntVnt Tree by default
        If Not ClientScript.IsStartupScriptRegistered("HideTree") Then
            ClientScript.RegisterStartupScript(Me.GetType(), "HideTree", "HideIntVntTree();", True)
        End If
    End Sub

    Private Sub ApplyAuthorizationRules()
        If Intervention.CanEditObject Then
            Dim obj As IntVntForm = GetForm()

            If obj.IsNew Then
                Me.lblHeader.Text = "Add New Form"
                Me.frmEdit.DefaultMode = DetailsViewMode.Insert
                CType(frmEdit.FindControl("Field01ValidationDropDown"), DropDownList).Items.FindByText("Free Form Text [0]").Selected = True
                CType(frmEdit.FindControl("Field02ValidationDropDown"), DropDownList).Items.FindByText("Free Form Text [0]").Selected = True
                CType(frmEdit.FindControl("Field03ValidationDropDown"), DropDownList).Items.FindByText("Free Form Text [0]").Selected = True
                CType(frmEdit.FindControl("Field04ValidationDropDown"), DropDownList).Items.FindByText("Free Form Text [0]").Selected = True
                CType(frmEdit.FindControl("Field05ValidationDropDown"), DropDownList).Items.FindByText("Free Form Text [0]").Selected = True
                CType(frmEdit.FindControl("Field06ValidationDropDown"), DropDownList).Items.FindByText("Free Form Text [0]").Selected = True
                CType(frmEdit.FindControl("Field07ValidationDropDown"), DropDownList).Items.FindByText("Free Form Text [0]").Selected = True
                CType(frmEdit.FindControl("Field08ValidationDropDown"), DropDownList).Items.FindByText("Free Form Text [0]").Selected = True
                CType(frmEdit.FindControl("Field09ValidationDropDown"), DropDownList).Items.FindByText("Free Form Text [0]").Selected = True
                CType(frmEdit.FindControl("Field10ValidationDropDown"), DropDownList).Items.FindByText("Free Form Text [0]").Selected = True
                CType(frmEdit.FindControl("Field11ValidationDropDown"), DropDownList).Items.FindByText("Free Form Text [0]").Selected = True
                CType(frmEdit.FindControl("Field12ValidationDropDown"), DropDownList).Items.FindByText("Free Form Text [0]").Selected = True
                CType(frmEdit.FindControl("Field13ValidationDropDown"), DropDownList).Items.FindByText("Free Form Text [0]").Selected = True
                CType(frmEdit.FindControl("Field14ValidationDropDown"), DropDownList).Items.FindByText("Free Form Text [0]").Selected = True
                CType(frmEdit.FindControl("Field15ValidationDropDown"), DropDownList).Items.FindByText("Free Form Text [0]").Selected = True
                CType(frmEdit.FindControl("Field16ValidationDropDown"), DropDownList).Items.FindByText("Free Form Text [0]").Selected = True
                CType(frmEdit.FindControl("Field17ValidationDropDown"), DropDownList).Items.FindByText("Free Form Text [0]").Selected = True
                CType(frmEdit.FindControl("Field18ValidationDropDown"), DropDownList).Items.FindByText("Free Form Text [0]").Selected = True
                CType(frmEdit.FindControl("Field19ValidationDropDown"), DropDownList).Items.FindByText("Free Form Text [0]").Selected = True
                CType(frmEdit.FindControl("Field20ValidationDropDown"), DropDownList).Items.FindByText("Free Form Text [0]").Selected = True
            Else
                Me.lblHeader.Text = "Edit Existing Form"
                Me.frmEdit.DefaultMode = DetailsViewMode.Edit

                ' Can this form be removed?
                ' A form can only be removed if it has never been used.
                If obj.HasBeenUsed Then
                    CType(Me.frmEdit.FindControl("DeleteButton"), Button).Visible = False
                End If
            End If
        Else
            Me.lblHeader.Text = "View Intervention Form"
            Me.frmEdit.DefaultMode = DetailsViewMode.ReadOnly
        End If
    End Sub

    Protected Sub frmEdit_DataBound(sender As Object, e As EventArgs) Handles frmEdit.DataBound
        If frmEdit.CurrentMode = FormViewMode.ReadOnly AndAlso Not frmEdit.DataItem Is Nothing Then
            ' Build the form table
            Dim FormInfo As ReadOnlyIntVntForm = DirectCast(frmEdit.DataItem, ReadOnlyIntVntForm)
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
                    DirectCast(frmEdit.FindControl("FormTable"), Table).Rows.Add(row)
                End If
            Next
        End If
    End Sub

    Protected Sub frmEdit_ItemCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles frmEdit.ItemCreated
        Dim obj As IntVntForm = GetForm()

        If frmEdit.DefaultMode = DetailsViewMode.Insert Then
            Try
                CType(frmEdit.FindControl("FormNameTextBox"), TextBox).Text = obj.FormName
                CType(frmEdit.FindControl("ReportDescriptionTextBox"), TextBox).Text = obj.ReportDescription
                ''CType(frmEdit.FindControl("IntVntClassDropDownList"), DropDownList).SelectedValue = obj.IntVntClass
                ''CType(frmEdit.FindControl("CalculatedCheckBox"), CheckBox).Checked = obj.Calculated
                ''CType(frmEdit.FindControl("CalculationTextBox"), TextBox).Text = obj.Calculation
                CType(frmEdit.FindControl("CustomTextBox"), TextBox).Text = obj.Custom
                CType(frmEdit.FindControl("DisplayOrderTextBox"), TextBox).Text = obj.DisplayOrder
                CType(frmEdit.FindControl("EducationCheckBox"), CheckBox).Checked = obj.Education
                CType(frmEdit.FindControl("InactiveCheckBox"), CheckBox).Checked = obj.Inactive
                ''CType(frmEdit.FindControl("ProblemCheckBox"), CheckBox).Checked = obj.Problem
                ''CType(frmEdit.FindControl("PatientGoalCheckBox"), CheckBox).Checked = obj.PatientGoal
                CType(frmEdit.FindControl("BillingCheckBox"), CheckBox).Checked = obj.Billing
                ''CType(frmEdit.FindControl("FunctionalObjectiveFindingCheckBox"), CheckBox).Checked = obj.FunctionalObjectiveFinding
                ''CType(frmEdit.FindControl("RequiredForPediatricsCheckBox"), CheckBox).Checked = obj.RequiredForPediatrics
                ''CType(frmEdit.FindControl("SubjectiveCheckBox"), CheckBox).Checked = obj.Subjective
                ''CType(frmEdit.FindControl("TestCheckBox"), CheckBox).Checked = obj.Test

                CType(frmEdit.FindControl("Field01LabelTextBox"), TextBox).Text = obj.Field01Label
                CType(frmEdit.FindControl("Field01TypeDropDown"), DropDownList).SelectedValue = obj.Field01Type
                CType(frmEdit.FindControl("Field01OptionsTextBox"), TextBox).Text = obj.Field01Options
                CType(frmEdit.FindControl("Field01HelpTextBox"), TextBox).Text = obj.Field01HelpText
                CType(frmEdit.FindControl("Field01ValidationDropDown"), DropDownList).SelectedValue = obj.Field01Validation

                CType(frmEdit.FindControl("Field02LabelTextBox"), TextBox).Text = obj.Field02Label
                CType(frmEdit.FindControl("Field02TypeDropDown"), DropDownList).SelectedValue = obj.Field02Type
                CType(frmEdit.FindControl("Field02OptionsTextBox"), TextBox).Text = obj.Field02Options
                CType(frmEdit.FindControl("Field02HelpTextBox"), TextBox).Text = obj.Field02HelpText
                CType(frmEdit.FindControl("Field02ValidationDropDown"), DropDownList).SelectedValue = obj.Field02Validation

                CType(frmEdit.FindControl("Field03LabelTextBox"), TextBox).Text = obj.Field03Label
                CType(frmEdit.FindControl("Field03TypeDropDown"), DropDownList).SelectedValue = obj.Field03Type
                CType(frmEdit.FindControl("Field03OptionsTextBox"), TextBox).Text = obj.Field03Options
                CType(frmEdit.FindControl("Field03HelpTextBox"), TextBox).Text = obj.Field03HelpText
                CType(frmEdit.FindControl("Field03ValidationDropDown"), DropDownList).SelectedValue = obj.Field03Validation

                CType(frmEdit.FindControl("Field04LabelTextBox"), TextBox).Text = obj.Field04Label
                CType(frmEdit.FindControl("Field04TypeDropDown"), DropDownList).SelectedValue = obj.Field04Type
                CType(frmEdit.FindControl("Field04OptionsTextBox"), TextBox).Text = obj.Field04Options
                CType(frmEdit.FindControl("Field04HelpTextBox"), TextBox).Text = obj.Field04HelpText
                CType(frmEdit.FindControl("Field04ValidationDropDown"), DropDownList).SelectedValue = obj.Field04Validation

                CType(frmEdit.FindControl("Field05LabelTextBox"), TextBox).Text = obj.Field05Label
                CType(frmEdit.FindControl("Field05TypeDropDown"), DropDownList).SelectedValue = obj.Field05Type
                CType(frmEdit.FindControl("Field05OptionsTextBox"), TextBox).Text = obj.Field05Options
                CType(frmEdit.FindControl("Field05HelpTextBox"), TextBox).Text = obj.Field05HelpText
                CType(frmEdit.FindControl("Field05ValidationDropDown"), DropDownList).SelectedValue = obj.Field05Validation

                CType(frmEdit.FindControl("Field06LabelTextBox"), TextBox).Text = obj.Field06Label
                CType(frmEdit.FindControl("Field06TypeDropDown"), DropDownList).SelectedValue = obj.Field06Type
                CType(frmEdit.FindControl("Field06OptionsTextBox"), TextBox).Text = obj.Field06Options
                CType(frmEdit.FindControl("Field06HelpTextBox"), TextBox).Text = obj.Field06HelpText
                CType(frmEdit.FindControl("Field06ValidationDropDown"), DropDownList).SelectedValue = obj.Field06Validation

                CType(frmEdit.FindControl("Field07LabelTextBox"), TextBox).Text = obj.Field07Label
                CType(frmEdit.FindControl("Field07TypeDropDown"), DropDownList).SelectedValue = obj.Field07Type
                CType(frmEdit.FindControl("Field07OptionsTextBox"), TextBox).Text = obj.Field07Options
                CType(frmEdit.FindControl("Field07HelpTextBox"), TextBox).Text = obj.Field07HelpText
                CType(frmEdit.FindControl("Field07ValidationDropDown"), DropDownList).SelectedValue = obj.Field07Validation

                CType(frmEdit.FindControl("Field08LabelTextBox"), TextBox).Text = obj.Field08Label
                CType(frmEdit.FindControl("Field08TypeDropDown"), DropDownList).SelectedValue = obj.Field08Type
                CType(frmEdit.FindControl("Field08OptionsTextBox"), TextBox).Text = obj.Field08Options
                CType(frmEdit.FindControl("Field08HelpTextBox"), TextBox).Text = obj.Field08HelpText
                CType(frmEdit.FindControl("Field08ValidationDropDown"), DropDownList).SelectedValue = obj.Field08Validation

                CType(frmEdit.FindControl("Field09LabelTextBox"), TextBox).Text = obj.Field09Label
                CType(frmEdit.FindControl("Field09TypeDropDown"), DropDownList).SelectedValue = obj.Field09Type
                CType(frmEdit.FindControl("Field09OptionsTextBox"), TextBox).Text = obj.Field09Options
                CType(frmEdit.FindControl("Field09HelpTextBox"), TextBox).Text = obj.Field09HelpText
                CType(frmEdit.FindControl("Field09ValidationDropDown"), DropDownList).SelectedValue = obj.Field09Validation

                CType(frmEdit.FindControl("Field10LabelTextBox"), TextBox).Text = obj.Field10Label
                CType(frmEdit.FindControl("Field10TypeDropDown"), DropDownList).SelectedValue = obj.Field10Type
                CType(frmEdit.FindControl("Field10OptionsTextBox"), TextBox).Text = obj.Field10Options
                CType(frmEdit.FindControl("Field10HelpTextBox"), TextBox).Text = obj.Field10HelpText
                CType(frmEdit.FindControl("Field10ValidationDropDown"), DropDownList).SelectedValue = obj.Field10Validation


                CType(frmEdit.FindControl("Field11LabelTextBox"), TextBox).Text = obj.Field11Label
                CType(frmEdit.FindControl("Field11TypeDropDown"), DropDownList).SelectedValue = obj.Field11Type
                CType(frmEdit.FindControl("Field11OptionsTextBox"), TextBox).Text = obj.Field11Options
                CType(frmEdit.FindControl("Field11HelpTextBox"), TextBox).Text = obj.Field11HelpText
                CType(frmEdit.FindControl("Field11ValidationDropDown"), DropDownList).SelectedValue = obj.Field11Validation

                CType(frmEdit.FindControl("Field12LabelTextBox"), TextBox).Text = obj.Field12Label
                CType(frmEdit.FindControl("Field12TypeDropDown"), DropDownList).SelectedValue = obj.Field12Type
                CType(frmEdit.FindControl("Field12OptionsTextBox"), TextBox).Text = obj.Field12Options
                CType(frmEdit.FindControl("Field12HelpTextBox"), TextBox).Text = obj.Field12HelpText
                CType(frmEdit.FindControl("Field12ValidationDropDown"), DropDownList).SelectedValue = obj.Field12Validation

                CType(frmEdit.FindControl("Field13LabelTextBox"), TextBox).Text = obj.Field13Label
                CType(frmEdit.FindControl("Field13TypeDropDown"), DropDownList).SelectedValue = obj.Field13Type
                CType(frmEdit.FindControl("Field13OptionsTextBox"), TextBox).Text = obj.Field13Options
                CType(frmEdit.FindControl("Field13HelpTextBox"), TextBox).Text = obj.Field13HelpText
                CType(frmEdit.FindControl("Field13ValidationDropDown"), DropDownList).SelectedValue = obj.Field13Validation

                CType(frmEdit.FindControl("Field14LabelTextBox"), TextBox).Text = obj.Field14Label
                CType(frmEdit.FindControl("Field14TypeDropDown"), DropDownList).SelectedValue = obj.Field14Type
                CType(frmEdit.FindControl("Field14OptionsTextBox"), TextBox).Text = obj.Field14Options
                CType(frmEdit.FindControl("Field14HelpTextBox"), TextBox).Text = obj.Field14HelpText
                CType(frmEdit.FindControl("Field14ValidationDropDown"), DropDownList).SelectedValue = obj.Field14Validation

                CType(frmEdit.FindControl("Field15LabelTextBox"), TextBox).Text = obj.Field15Label
                CType(frmEdit.FindControl("Field15TypeDropDown"), DropDownList).SelectedValue = obj.Field15Type
                CType(frmEdit.FindControl("Field15OptionsTextBox"), TextBox).Text = obj.Field15Options
                CType(frmEdit.FindControl("Field15HelpTextBox"), TextBox).Text = obj.Field15HelpText
                CType(frmEdit.FindControl("Field15ValidationDropDown"), DropDownList).SelectedValue = obj.Field15Validation

                CType(frmEdit.FindControl("Field16LabelTextBox"), TextBox).Text = obj.Field16Label
                CType(frmEdit.FindControl("Field16TypeDropDown"), DropDownList).SelectedValue = obj.Field16Type
                CType(frmEdit.FindControl("Field16OptionsTextBox"), TextBox).Text = obj.Field16Options
                CType(frmEdit.FindControl("Field16HelpTextBox"), TextBox).Text = obj.Field16HelpText
                CType(frmEdit.FindControl("Field16ValidationDropDown"), DropDownList).SelectedValue = obj.Field16Validation

                CType(frmEdit.FindControl("Field17LabelTextBox"), TextBox).Text = obj.Field17Label
                CType(frmEdit.FindControl("Field17TypeDropDown"), DropDownList).SelectedValue = obj.Field17Type
                CType(frmEdit.FindControl("Field17OptionsTextBox"), TextBox).Text = obj.Field17Options
                CType(frmEdit.FindControl("Field17HelpTextBox"), TextBox).Text = obj.Field17HelpText
                CType(frmEdit.FindControl("Field17ValidationDropDown"), DropDownList).SelectedValue = obj.Field17Validation

                CType(frmEdit.FindControl("Field18LabelTextBox"), TextBox).Text = obj.Field18Label
                CType(frmEdit.FindControl("Field18TypeDropDown"), DropDownList).SelectedValue = obj.Field18Type
                CType(frmEdit.FindControl("Field18OptionsTextBox"), TextBox).Text = obj.Field18Options
                CType(frmEdit.FindControl("Field18HelpTextBox"), TextBox).Text = obj.Field18HelpText
                CType(frmEdit.FindControl("Field18ValidationDropDown"), DropDownList).SelectedValue = obj.Field18Validation

                CType(frmEdit.FindControl("Field19LabelTextBox"), TextBox).Text = obj.Field19Label
                CType(frmEdit.FindControl("Field19TypeDropDown"), DropDownList).SelectedValue = obj.Field19Type
                CType(frmEdit.FindControl("Field19OptionsTextBox"), TextBox).Text = obj.Field19Options
                CType(frmEdit.FindControl("Field19HelpTextBox"), TextBox).Text = obj.Field19HelpText
                CType(frmEdit.FindControl("Field19ValidationDropDown"), DropDownList).SelectedValue = obj.Field19Validation

                CType(frmEdit.FindControl("Field20LabelTextBox"), TextBox).Text = obj.Field20Label
                CType(frmEdit.FindControl("Field20TypeDropDown"), DropDownList).SelectedValue = obj.Field20Type
                CType(frmEdit.FindControl("Field20OptionsTextBox"), TextBox).Text = obj.Field20Options
                CType(frmEdit.FindControl("Field20HelpTextBox"), TextBox).Text = obj.Field20HelpText
                CType(frmEdit.FindControl("Field20ValidationDropDown"), DropDownList).SelectedValue = obj.Field20Validation


                CType(frmEdit.FindControl("LockedCheckBox"), CheckBox).Checked = obj.Locked
                CType(frmEdit.FindControl("LockNotesTextBox"), TextBox).Text = obj.LockNotes
            Catch
            End Try
        End If

        ' Show if this form has been used
        FormHasBeenUsedPanel.Visible = obj.HasBeenUsed

        If Not frmEdit.CurrentMode = FormViewMode.ReadOnly Then
            ' Show the Parent
            CType(frmEdit.FindControl("ParentFolderLabel"), Label).Text = ReadOnlyIntVntFolder.GetIntVntFolderInfo(obj.ParentID).FolderName


            ' Link all of the field type dropdowns with the Javascript code
            ' that shows the Options div below the dropdown if the type is
            ' changed to "combobox" or "radio"
            CType(frmEdit.FindControl("Field01TypeDropDown"), DropDownList).Attributes.Add("onchange", "javascript:FieldTypeValueChanged(this, 'divField01Options');")
            CType(frmEdit.FindControl("Field02TypeDropDown"), DropDownList).Attributes.Add("onchange", "javascript:FieldTypeValueChanged(this, 'divField02Options');")
            CType(frmEdit.FindControl("Field03TypeDropDown"), DropDownList).Attributes.Add("onchange", "javascript:FieldTypeValueChanged(this, 'divField03Options');")
            CType(frmEdit.FindControl("Field04TypeDropDown"), DropDownList).Attributes.Add("onchange", "javascript:FieldTypeValueChanged(this, 'divField04Options');")
            CType(frmEdit.FindControl("Field05TypeDropDown"), DropDownList).Attributes.Add("onchange", "javascript:FieldTypeValueChanged(this, 'divField05Options');")
            CType(frmEdit.FindControl("Field06TypeDropDown"), DropDownList).Attributes.Add("onchange", "javascript:FieldTypeValueChanged(this, 'divField06Options');")
            CType(frmEdit.FindControl("Field07TypeDropDown"), DropDownList).Attributes.Add("onchange", "javascript:FieldTypeValueChanged(this, 'divField07Options');")
            CType(frmEdit.FindControl("Field08TypeDropDown"), DropDownList).Attributes.Add("onchange", "javascript:FieldTypeValueChanged(this, 'divField08Options');")
            CType(frmEdit.FindControl("Field09TypeDropDown"), DropDownList).Attributes.Add("onchange", "javascript:FieldTypeValueChanged(this, 'divField09Options');")
            CType(frmEdit.FindControl("Field10TypeDropDown"), DropDownList).Attributes.Add("onchange", "javascript:FieldTypeValueChanged(this, 'divField10Options');")
            CType(frmEdit.FindControl("Field11TypeDropDown"), DropDownList).Attributes.Add("onchange", "javascript:FieldTypeValueChanged(this, 'divField11Options');")
            CType(frmEdit.FindControl("Field12TypeDropDown"), DropDownList).Attributes.Add("onchange", "javascript:FieldTypeValueChanged(this, 'divField12Options');")
            CType(frmEdit.FindControl("Field13TypeDropDown"), DropDownList).Attributes.Add("onchange", "javascript:FieldTypeValueChanged(this, 'divField13Options');")
            CType(frmEdit.FindControl("Field14TypeDropDown"), DropDownList).Attributes.Add("onchange", "javascript:FieldTypeValueChanged(this, 'divField14Options');")
            CType(frmEdit.FindControl("Field15TypeDropDown"), DropDownList).Attributes.Add("onchange", "javascript:FieldTypeValueChanged(this, 'divField15Options');")
            CType(frmEdit.FindControl("Field16TypeDropDown"), DropDownList).Attributes.Add("onchange", "javascript:FieldTypeValueChanged(this, 'divField16Options');")
            CType(frmEdit.FindControl("Field17TypeDropDown"), DropDownList).Attributes.Add("onchange", "javascript:FieldTypeValueChanged(this, 'divField17Options');")
            CType(frmEdit.FindControl("Field18TypeDropDown"), DropDownList).Attributes.Add("onchange", "javascript:FieldTypeValueChanged(this, 'divField18Options');")
            CType(frmEdit.FindControl("Field19TypeDropDown"), DropDownList).Attributes.Add("onchange", "javascript:FieldTypeValueChanged(this, 'divField19Options');")
            CType(frmEdit.FindControl("Field20TypeDropDown"), DropDownList).Attributes.Add("onchange", "javascript:FieldTypeValueChanged(this, 'divField20Options');")


            ' Create a script to run after the page is loaded
            ' to show the Options memo fields for the fields
            ' that previously had combobox or radio as the type.
            Dim startupScript As String = "<script type=""text/javascript"">"

            startupScript &= "FieldTypeValueChanged(document.getElementById('" & frmEdit.FindControl("Field01TypeDropDown").ClientID & "'), 'divField01Options');"
            startupScript &= "FieldTypeValueChanged(document.getElementById('" & frmEdit.FindControl("Field02TypeDropDown").ClientID & "'), 'divField02Options');"
            startupScript &= "FieldTypeValueChanged(document.getElementById('" & frmEdit.FindControl("Field03TypeDropDown").ClientID & "'), 'divField03Options');"
            startupScript &= "FieldTypeValueChanged(document.getElementById('" & frmEdit.FindControl("Field04TypeDropDown").ClientID & "'), 'divField04Options');"
            startupScript &= "FieldTypeValueChanged(document.getElementById('" & frmEdit.FindControl("Field05TypeDropDown").ClientID & "'), 'divField05Options');"
            startupScript &= "FieldTypeValueChanged(document.getElementById('" & frmEdit.FindControl("Field06TypeDropDown").ClientID & "'), 'divField06Options');"
            startupScript &= "FieldTypeValueChanged(document.getElementById('" & frmEdit.FindControl("Field07TypeDropDown").ClientID & "'), 'divField07Options');"
            startupScript &= "FieldTypeValueChanged(document.getElementById('" & frmEdit.FindControl("Field08TypeDropDown").ClientID & "'), 'divField08Options');"
            startupScript &= "FieldTypeValueChanged(document.getElementById('" & frmEdit.FindControl("Field09TypeDropDown").ClientID & "'), 'divField09Options');"
            startupScript &= "FieldTypeValueChanged(document.getElementById('" & frmEdit.FindControl("Field10TypeDropDown").ClientID & "'), 'divField10Options');"
            startupScript &= "FieldTypeValueChanged(document.getElementById('" & frmEdit.FindControl("Field11TypeDropDown").ClientID & "'), 'divField11Options');"
            startupScript &= "FieldTypeValueChanged(document.getElementById('" & frmEdit.FindControl("Field12TypeDropDown").ClientID & "'), 'divField12Options');"
            startupScript &= "FieldTypeValueChanged(document.getElementById('" & frmEdit.FindControl("Field13TypeDropDown").ClientID & "'), 'divField13Options');"
            startupScript &= "FieldTypeValueChanged(document.getElementById('" & frmEdit.FindControl("Field14TypeDropDown").ClientID & "'), 'divField14Options');"
            startupScript &= "FieldTypeValueChanged(document.getElementById('" & frmEdit.FindControl("Field15TypeDropDown").ClientID & "'), 'divField15Options');"
            startupScript &= "FieldTypeValueChanged(document.getElementById('" & frmEdit.FindControl("Field16TypeDropDown").ClientID & "'), 'divField16Options');"
            startupScript &= "FieldTypeValueChanged(document.getElementById('" & frmEdit.FindControl("Field17TypeDropDown").ClientID & "'), 'divField17Options');"
            startupScript &= "FieldTypeValueChanged(document.getElementById('" & frmEdit.FindControl("Field18TypeDropDown").ClientID & "'), 'divField18Options');"
            startupScript &= "FieldTypeValueChanged(document.getElementById('" & frmEdit.FindControl("Field19TypeDropDown").ClientID & "'), 'divField19Options');"
            startupScript &= "FieldTypeValueChanged(document.getElementById('" & frmEdit.FindControl("Field20TypeDropDown").ClientID & "'), 'divField20Options');"

            startupScript &= "</script>"

            If Not ClientScript.IsStartupScriptRegistered("clientScript") Then
                ClientScript.RegisterStartupScript(GetType(String), "clientScript", startupScript, False)
            End If

            ' Show any fields that are associated with Global Lookup items
            For Each lookupValue In obj.LookupList
                Dim FieldNumber As String
                Dim label As Label

                If lookupValue.FormField < 10 Then
                    FieldNumber = "0" & lookupValue.FormField
                Else
                    FieldNumber = lookupValue.FormField
                End If

                label = CType(frmEdit.FindControl("Field" & FieldNumber & "LookupLabel"), Label)

                If Not String.IsNullOrEmpty(label.Text) Then
                    label.Text &= "; " & lookupValue.Description
                Else
                    label.Text = lookupValue.Description
                End If
            Next
        End If
    End Sub

    ' Redirect back to the list on Cancel
    Protected Sub frmEdit_ModeChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewModeEventArgs) Handles frmEdit.ModeChanging
        If e.CancelingEdit OrElse e.Cancel Then
            GoBack()
        End If
    End Sub

    Protected Sub IntVntFormDataSource_SelectObject(ByVal sender As Object, ByVal e As Csla.Web.SelectObjectArgs) Handles IntVntFormDataSource.SelectObject
        If Me.frmEdit.CurrentMode = FormViewMode.ReadOnly Then
            e.BusinessObject = ReadOnlyIntVntForm.GetIntVntFormInfo(Request("intVntID"))
        Else
            e.BusinessObject = GetForm()
        End If
    End Sub

    Protected Sub IntVntFormDataSource_InsertObject(ByVal sender As Object, ByVal e As Csla.Web.InsertObjectArgs) Handles IntVntFormDataSource.InsertObject
        Dim obj As IntVntForm = GetForm()
        Csla.Data.DataMapper.Map(e.Values, obj)
        SaveForm(obj)
    End Sub

    Protected Sub IntVntFormDataSource_UpdateObject(ByVal sender As Object, ByVal e As Csla.Web.UpdateObjectArgs) Handles IntVntFormDataSource.UpdateObject
        Dim obj As IntVntForm = GetForm()
        Csla.Data.DataMapper.Map(e.Values, obj, "intVntID")
        SaveForm(obj)
    End Sub

    Protected Sub IntVntFormDataSource_DeleteObject(ByVal sender As Object, ByVal e As Csla.Web.DeleteObjectArgs) Handles IntVntFormDataSource.DeleteObject
        Dim theForm As IntVntForm = GetForm()
        Dim intVntLevel As String
        intVntLevel = theForm.IntVntLevel
        Dim parentID As String
        parentID = theForm.ParentID
        Dim intVntID As String
        intVntID = theForm.intVntID
        Try
            IntVntForm.DeleteIntVntForm(Request("intVntID"))
            Session("currentObject") = Nothing
            GoBack()
        Catch ex As Csla.DataPortalException
            Me.pnlError.Visible = True
            Me.lblError.Text = ex.BusinessException.Message
        Catch ex As Exception
            Me.pnlError.Visible = True
            Me.lblError.Text = ex.Message
        End Try
        If intVntLevel = 2 Then
            Response.Redirect("IntVnts.aspx?ParentID=" & parentID)
        Else
            Response.Redirect("IntVnt_Landing.aspx?ShowintVntID=" & intVntID)
        End If
    End Sub

    Private Sub GoBack()
        ' Redirect back to the landing page.
        ' Get the intVntID of the node that needs to be shown.
        Dim intVntID As String = Request("intVntID")

        ' If we are editing an existing form, Request("intVntID") will hold
        ' the Interventions ID we're looking for.  But if we've just added a new form,
        ' we'll need to pull the Interventions ID from the business object.
        If Not IsNothing(Session("currentObject")) AndAlso TypeOf Session("currentObject") Is IntVntForm Then
            intVntID = DirectCast(Session("currentObject"), IntVntForm).intVntID.ToString()
        End If

        Dim theForm As IntVntForm = GetForm()
        If theForm.IntVntLevel = 2 Then
            Response.Redirect("IntVnts.aspx?ParentID=" & theForm.ParentID)
        Else
            Response.Redirect("IntVnt_Landing.aspx?ShowintVntID=" & theForm.intVntID)
        End If
    End Sub

    Private Function GetForm() As IntVntForm
        Dim businessObject As Object = Session("currentObject")

        If IsNothing(businessObject) OrElse Not TypeOf businessObject Is IntVntForm Then
            Try
                Dim idString As String = Request("intVntID")
                Dim parentID As String = Request("ParentID")

                If Not String.IsNullOrEmpty(idString) Then
                    Try
                        businessObject = IntVntForm.GetIntVntForm(idString)
                    Catch ex As Exception

                    End Try

                ElseIf Not String.IsNullOrEmpty(parentID) Then
                    businessObject = IntVntForm.NewIntVntForm(parentID)
                Else
                    ' TODO - Create the Error page
                    Response.Redirect("IntVnt_Error.aspx")
                End If

                Session("currentObject") = businessObject
            Catch ex As System.Security.SecurityException
                ' TODO - Create the Error page
                Response.Redirect("IntVnt_Error.aspx?ErrorText=Security Exception!&ErrorDetails=" & Server.UrlEncode(ex.Message))
            End Try
        End If

        Return CType(businessObject, IntVntForm)
    End Function

    Private Sub SaveForm(ByVal theForm As IntVntForm)
        Try
            Session("currentObject") = theForm.Save()
            GoBack()
        Catch ex As Csla.Validation.ValidationException
            Dim message As New System.Text.StringBuilder
            message.AppendFormat("{0}:<br/>", ex.Message)

            If theForm.BrokenRulesCollection.Count = 1 Then
                message.AppendFormat("-{0}", theForm.BrokenRulesCollection(0).Description)
            Else
                For Each rule As Csla.Validation.BrokenRule In _
                    theForm.BrokenRulesCollection
                    message.AppendFormat("-{0}<br/>", rule.Description)
                Next
            End If

            Me.pnlError.Visible = True
            Me.lblError.Text = message.ToString()
        Catch ex As Csla.DataPortalException
            Me.pnlError.Visible = True
            Me.lblError.Text = ex.BusinessException.Message
        Catch ex As Exception
            Me.pnlError.Visible = True
            Me.lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub CopyButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Session("CopiedIntVntForm") = Session("currentObject")
        Response.Redirect("IntVnt_Landing.aspx?ShowintVntID=" & CType(Session("CopiedIntVntForm"), IntVntForm).intVntID)
    End Sub

    Protected Sub FieldCommand(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim theForm As IntVntForm = GetForm()

        If e.CommandName = "InsertField" Then
            theForm.InsertField(CInt(e.CommandArgument))
        ElseIf e.CommandName = "RemoveField" Then
            theForm.RemoveField(CInt(e.CommandArgument))
        ElseIf e.CommandName = "ClearField" Then
            theForm.ClearField(CInt(e.CommandArgument))
        End If

        Session("currentObject") = theForm
        Me.frmEdit.DataBind()
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
