Imports ITWMaintenance.Library.Nursing.Notes
Imports Csla.Validation

Partial Class Nursing_Notes_Form_Edit
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("currentObject") = Nothing
            ApplyAuthorizationRules()
        Else
            Me.pnlError.Visible = False
            Me.lblError.Text = ""

            Dim theForm As NursingNoteForm = GetForm()

            ' Before processing the event, we need to pull the form changes
            ' out of the FormView and put them into the object.
            theForm.FormName = CType(frmEdit.FindControl("FormNameTextBox"), TextBox).Text
            theForm.DisplayOrder = CType(frmEdit.FindControl("DisplayOrderTextBox"), TextBox).Text
            theForm.Inactive = CType(frmEdit.FindControl("InactiveCheckBox"), CheckBox).Checked
            theForm.Required = CType(frmEdit.FindControl("RequiredCheckBox"), CheckBox).Checked
            theForm.Education = CType(frmEdit.FindControl("EducationCheckBox"), CheckBox).Checked
            theForm.PCarOnly = CType(frmEdit.FindControl("PCarOnlyCheckBox"), CheckBox).Checked
            theForm.NursingNoteOnly = CType(frmEdit.FindControl("NursingNoteOnlyCheckBox"), CheckBox).Checked
            'theForm.IncludeGraphics = CType(frmEdit.FindControl("IncludeGraphicsCheckBox"), CheckBox).Checked
            theForm.RedirectTo = CType(frmEdit.FindControl("RedirectToDropDownList"), DropDownList).SelectedValue
            theForm.Calculated = CType(frmEdit.FindControl("CalculatedCheckBox"), CheckBox).Checked
            theForm.Calculation = CType(frmEdit.FindControl("CalculationTextBox"), TextBox).Text
            theForm.Locked = CType(frmEdit.FindControl("LockedCheckBox"), CheckBox).Checked
            theForm.LockNotes = CType(frmEdit.FindControl("LockNotesTextBox"), TextBox).Text

            theForm.Field01Label = CType(frmEdit.FindControl("Field01LabelTextBox"), TextBox).Text
            theForm.Field01Type = CType(frmEdit.FindControl("Field01TypeDropDown"), DropDownList).SelectedValue
            theForm.Field01Options = CType(frmEdit.FindControl("Field01OptionsTextBox"), TextBox).Text
            theForm.Field01HelpText = CType(frmEdit.FindControl("Field01HelpTextBox"), TextBox).Text

            theForm.Field02Label = CType(frmEdit.FindControl("Field02LabelTextBox"), TextBox).Text
            theForm.Field02Type = CType(frmEdit.FindControl("Field02TypeDropDown"), DropDownList).SelectedValue
            theForm.Field02Options = CType(frmEdit.FindControl("Field02OptionsTextBox"), TextBox).Text
            theForm.Field02HelpText = CType(frmEdit.FindControl("Field02HelpTextBox"), TextBox).Text

            theForm.Field03Label = CType(frmEdit.FindControl("Field03LabelTextBox"), TextBox).Text
            theForm.Field03Type = CType(frmEdit.FindControl("Field03TypeDropDown"), DropDownList).SelectedValue
            theForm.Field03Options = CType(frmEdit.FindControl("Field03OptionsTextBox"), TextBox).Text
            theForm.Field03HelpText = CType(frmEdit.FindControl("Field03HelpTextBox"), TextBox).Text

            theForm.Field04Label = CType(frmEdit.FindControl("Field04LabelTextBox"), TextBox).Text
            theForm.Field04Type = CType(frmEdit.FindControl("Field04TypeDropDown"), DropDownList).SelectedValue
            theForm.Field04Options = CType(frmEdit.FindControl("Field04OptionsTextBox"), TextBox).Text
            theForm.Field04HelpText = CType(frmEdit.FindControl("Field04HelpTextBox"), TextBox).Text

            theForm.Field05Label = CType(frmEdit.FindControl("Field05LabelTextBox"), TextBox).Text
            theForm.Field05Type = CType(frmEdit.FindControl("Field05TypeDropDown"), DropDownList).SelectedValue
            theForm.Field05Options = CType(frmEdit.FindControl("Field05OptionsTextBox"), TextBox).Text
            theForm.Field05HelpText = CType(frmEdit.FindControl("Field05HelpTextBox"), TextBox).Text

            theForm.Field06Label = CType(frmEdit.FindControl("Field06LabelTextBox"), TextBox).Text
            theForm.Field06Type = CType(frmEdit.FindControl("Field06TypeDropDown"), DropDownList).SelectedValue
            theForm.Field06Options = CType(frmEdit.FindControl("Field06OptionsTextBox"), TextBox).Text
            theForm.Field06HelpText = CType(frmEdit.FindControl("Field06HelpTextBox"), TextBox).Text

            theForm.Field07Label = CType(frmEdit.FindControl("Field07LabelTextBox"), TextBox).Text
            theForm.Field07Type = CType(frmEdit.FindControl("Field07TypeDropDown"), DropDownList).SelectedValue
            theForm.Field07Options = CType(frmEdit.FindControl("Field07OptionsTextBox"), TextBox).Text
            theForm.Field07HelpText = CType(frmEdit.FindControl("Field07HelpTextBox"), TextBox).Text

            theForm.Field08Label = CType(frmEdit.FindControl("Field08LabelTextBox"), TextBox).Text
            theForm.Field08Type = CType(frmEdit.FindControl("Field08TypeDropDown"), DropDownList).SelectedValue
            theForm.Field08Options = CType(frmEdit.FindControl("Field08OptionsTextBox"), TextBox).Text
            theForm.Field08HelpText = CType(frmEdit.FindControl("Field08HelpTextBox"), TextBox).Text

            theForm.Field09Label = CType(frmEdit.FindControl("Field09LabelTextBox"), TextBox).Text
            theForm.Field09Type = CType(frmEdit.FindControl("Field09TypeDropDown"), DropDownList).SelectedValue
            theForm.Field09Options = CType(frmEdit.FindControl("Field09OptionsTextBox"), TextBox).Text
            theForm.Field09HelpText = CType(frmEdit.FindControl("Field09HelpTextBox"), TextBox).Text

            theForm.Field10Label = CType(frmEdit.FindControl("Field10LabelTextBox"), TextBox).Text
            theForm.Field10Type = CType(frmEdit.FindControl("Field10TypeDropDown"), DropDownList).SelectedValue
            theForm.Field10Options = CType(frmEdit.FindControl("Field10OptionsTextBox"), TextBox).Text
            theForm.Field10HelpText = CType(frmEdit.FindControl("Field10HelpTextBox"), TextBox).Text


            theForm.Field11Label = CType(frmEdit.FindControl("Field11LabelTextBox"), TextBox).Text
            theForm.Field11Type = CType(frmEdit.FindControl("Field11TypeDropDown"), DropDownList).SelectedValue
            theForm.Field11Options = CType(frmEdit.FindControl("Field11OptionsTextBox"), TextBox).Text
            theForm.Field11HelpText = CType(frmEdit.FindControl("Field11HelpTextBox"), TextBox).Text

            theForm.Field12Label = CType(frmEdit.FindControl("Field12LabelTextBox"), TextBox).Text
            theForm.Field12Type = CType(frmEdit.FindControl("Field12TypeDropDown"), DropDownList).SelectedValue
            theForm.Field12Options = CType(frmEdit.FindControl("Field12OptionsTextBox"), TextBox).Text
            theForm.Field12HelpText = CType(frmEdit.FindControl("Field12HelpTextBox"), TextBox).Text

            theForm.Field13Label = CType(frmEdit.FindControl("Field13LabelTextBox"), TextBox).Text
            theForm.Field13Type = CType(frmEdit.FindControl("Field13TypeDropDown"), DropDownList).SelectedValue
            theForm.Field13Options = CType(frmEdit.FindControl("Field13OptionsTextBox"), TextBox).Text
            theForm.Field13HelpText = CType(frmEdit.FindControl("Field13HelpTextBox"), TextBox).Text

            theForm.Field14Label = CType(frmEdit.FindControl("Field14LabelTextBox"), TextBox).Text
            theForm.Field14Type = CType(frmEdit.FindControl("Field14TypeDropDown"), DropDownList).SelectedValue
            theForm.Field14Options = CType(frmEdit.FindControl("Field14OptionsTextBox"), TextBox).Text
            theForm.Field14HelpText = CType(frmEdit.FindControl("Field14HelpTextBox"), TextBox).Text

            theForm.Field15Label = CType(frmEdit.FindControl("Field15LabelTextBox"), TextBox).Text
            theForm.Field15Type = CType(frmEdit.FindControl("Field15TypeDropDown"), DropDownList).SelectedValue
            theForm.Field15Options = CType(frmEdit.FindControl("Field15OptionsTextBox"), TextBox).Text
            theForm.Field15HelpText = CType(frmEdit.FindControl("Field15HelpTextBox"), TextBox).Text

            theForm.Field16Label = CType(frmEdit.FindControl("Field16LabelTextBox"), TextBox).Text
            theForm.Field16Type = CType(frmEdit.FindControl("Field16TypeDropDown"), DropDownList).SelectedValue
            theForm.Field16Options = CType(frmEdit.FindControl("Field16OptionsTextBox"), TextBox).Text
            theForm.Field16HelpText = CType(frmEdit.FindControl("Field16HelpTextBox"), TextBox).Text

            theForm.Field17Label = CType(frmEdit.FindControl("Field17LabelTextBox"), TextBox).Text
            theForm.Field17Type = CType(frmEdit.FindControl("Field17TypeDropDown"), DropDownList).SelectedValue
            theForm.Field17Options = CType(frmEdit.FindControl("Field17OptionsTextBox"), TextBox).Text
            theForm.Field17HelpText = CType(frmEdit.FindControl("Field17HelpTextBox"), TextBox).Text

            theForm.Field18Label = CType(frmEdit.FindControl("Field18LabelTextBox"), TextBox).Text
            theForm.Field18Type = CType(frmEdit.FindControl("Field18TypeDropDown"), DropDownList).SelectedValue
            theForm.Field18Options = CType(frmEdit.FindControl("Field18OptionsTextBox"), TextBox).Text
            theForm.Field18HelpText = CType(frmEdit.FindControl("Field18HelpTextBox"), TextBox).Text

            theForm.Field19Label = CType(frmEdit.FindControl("Field19LabelTextBox"), TextBox).Text
            theForm.Field19Type = CType(frmEdit.FindControl("Field19TypeDropDown"), DropDownList).SelectedValue
            theForm.Field19Options = CType(frmEdit.FindControl("Field19OptionsTextBox"), TextBox).Text
            theForm.Field19HelpText = CType(frmEdit.FindControl("Field19HelpTextBox"), TextBox).Text

            theForm.Field20Label = CType(frmEdit.FindControl("Field20LabelTextBox"), TextBox).Text
            theForm.Field20Type = CType(frmEdit.FindControl("Field20TypeDropDown"), DropDownList).SelectedValue
            theForm.Field20Options = CType(frmEdit.FindControl("Field20OptionsTextBox"), TextBox).Text
            theForm.Field20HelpText = CType(frmEdit.FindControl("Field20HelpTextBox"), TextBox).Text
        End If



        Try
            Me.frmEdit.FindControl("FormNameTextBox").Focus()
        Catch
        End Try

        If Not String.IsNullOrEmpty(Request("FormID")) Then
            Session("ShowNursingNoteID") = Request("FormID")
        ElseIf Not String.IsNullOrEmpty(Request("ParentID")) Then
            Session("ShowNursingNoteID") = Request("ParentID")
        End If

        ' Hide the Tree by default
        If Not ClientScript.IsStartupScriptRegistered("HideTree") Then
            ClientScript.RegisterStartupScript(Me.GetType(), "HideTree", "HideNursingNoteTree();", True)
        End If
    End Sub

    Private Sub ApplyAuthorizationRules()
        If NursingNoteForm.CanEditObject Then
            Dim obj As NursingNoteForm = GetForm()

            If obj.IsNew Then
                Me.lblHeader.Text = "Add New Form"
                Me.frmEdit.DefaultMode = DetailsViewMode.Insert
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
            Me.frmEdit.DefaultMode = DetailsViewMode.ReadOnly
        End If
    End Sub

    Protected Sub frmEdit_ItemCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles frmEdit.ItemCreated
        Dim obj As NursingNoteForm = GetForm()

        If frmEdit.DefaultMode = DetailsViewMode.Insert Then
            Try
                CType(frmEdit.FindControl("ParentNameLabel"), Label).Text = obj.ParentName
                CType(frmEdit.FindControl("FormNameTextBox"), TextBox).Text = obj.FormName
                CType(frmEdit.FindControl("DisplayOrderTextBox"), TextBox).Text = obj.DisplayOrder
                CType(frmEdit.FindControl("InactiveCheckBox"), CheckBox).Checked = obj.Inactive
                CType(frmEdit.FindControl("RequiredCheckBox"), CheckBox).Checked = obj.Required
                CType(frmEdit.FindControl("EducationCheckBox"), CheckBox).Checked = obj.Education
                CType(frmEdit.FindControl("PCarOnlyCheckBox"), CheckBox).Checked = obj.PCarOnly
                CType(frmEdit.FindControl("NursingNoteOnlyCheckBox"), CheckBox).Checked = obj.NursingNoteOnly
                'CType(frmEdit.FindControl("IncludeGraphicsCheckBox"), CheckBox).Checked = obj.IncludeGraphics
                CType(frmEdit.FindControl("RedirectToDropDownList"), DropDownList).SelectedValue = obj.RedirectTo
                CType(frmEdit.FindControl("CalculatedCheckBox"), CheckBox).Checked = obj.Calculated
                CType(frmEdit.FindControl("CalculationTextBox"), TextBox).Text = obj.Calculation
                CType(frmEdit.FindControl("LockedCheckBox"), CheckBox).Checked = obj.Locked
                CType(frmEdit.FindControl("LockNotesTextBox"), TextBox).Text = obj.LockNotes

                CType(frmEdit.FindControl("Field01LabelTextBox"), TextBox).Text = obj.Field01Label
                CType(frmEdit.FindControl("Field01TypeDropDown"), DropDownList).SelectedValue = obj.Field01Type
                CType(frmEdit.FindControl("Field01OptionsTextBox"), TextBox).Text = obj.Field01Options
                CType(frmEdit.FindControl("Field01HelpTextBox"), TextBox).Text = obj.Field01HelpText

                CType(frmEdit.FindControl("Field02LabelTextBox"), TextBox).Text = obj.Field02Label
                CType(frmEdit.FindControl("Field02TypeDropDown"), DropDownList).SelectedValue = obj.Field02Type
                CType(frmEdit.FindControl("Field02OptionsTextBox"), TextBox).Text = obj.Field02Options
                CType(frmEdit.FindControl("Field02HelpTextBox"), TextBox).Text = obj.Field02HelpText

                CType(frmEdit.FindControl("Field03LabelTextBox"), TextBox).Text = obj.Field03Label
                CType(frmEdit.FindControl("Field03TypeDropDown"), DropDownList).SelectedValue = obj.Field03Type
                CType(frmEdit.FindControl("Field03OptionsTextBox"), TextBox).Text = obj.Field03Options
                CType(frmEdit.FindControl("Field03HelpTextBox"), TextBox).Text = obj.Field03HelpText

                CType(frmEdit.FindControl("Field04LabelTextBox"), TextBox).Text = obj.Field04Label
                CType(frmEdit.FindControl("Field04TypeDropDown"), DropDownList).SelectedValue = obj.Field04Type
                CType(frmEdit.FindControl("Field04OptionsTextBox"), TextBox).Text = obj.Field04Options
                CType(frmEdit.FindControl("Field04HelpTextBox"), TextBox).Text = obj.Field04HelpText

                CType(frmEdit.FindControl("Field05LabelTextBox"), TextBox).Text = obj.Field05Label
                CType(frmEdit.FindControl("Field05TypeDropDown"), DropDownList).SelectedValue = obj.Field05Type
                CType(frmEdit.FindControl("Field05OptionsTextBox"), TextBox).Text = obj.Field05Options
                CType(frmEdit.FindControl("Field05HelpTextBox"), TextBox).Text = obj.Field05HelpText

                CType(frmEdit.FindControl("Field06LabelTextBox"), TextBox).Text = obj.Field06Label
                CType(frmEdit.FindControl("Field06TypeDropDown"), DropDownList).SelectedValue = obj.Field06Type
                CType(frmEdit.FindControl("Field06OptionsTextBox"), TextBox).Text = obj.Field06Options
                CType(frmEdit.FindControl("Field06HelpTextBox"), TextBox).Text = obj.Field06HelpText

                CType(frmEdit.FindControl("Field07LabelTextBox"), TextBox).Text = obj.Field07Label
                CType(frmEdit.FindControl("Field07TypeDropDown"), DropDownList).SelectedValue = obj.Field07Type
                CType(frmEdit.FindControl("Field07OptionsTextBox"), TextBox).Text = obj.Field07Options
                CType(frmEdit.FindControl("Field07HelpTextBox"), TextBox).Text = obj.Field07HelpText

                CType(frmEdit.FindControl("Field08LabelTextBox"), TextBox).Text = obj.Field08Label
                CType(frmEdit.FindControl("Field08TypeDropDown"), DropDownList).SelectedValue = obj.Field08Type
                CType(frmEdit.FindControl("Field08OptionsTextBox"), TextBox).Text = obj.Field08Options
                CType(frmEdit.FindControl("Field08HelpTextBox"), TextBox).Text = obj.Field08HelpText

                CType(frmEdit.FindControl("Field09LabelTextBox"), TextBox).Text = obj.Field09Label
                CType(frmEdit.FindControl("Field09TypeDropDown"), DropDownList).SelectedValue = obj.Field09Type
                CType(frmEdit.FindControl("Field09OptionsTextBox"), TextBox).Text = obj.Field09Options
                CType(frmEdit.FindControl("Field09HelpTextBox"), TextBox).Text = obj.Field09HelpText

                CType(frmEdit.FindControl("Field10LabelTextBox"), TextBox).Text = obj.Field10Label
                CType(frmEdit.FindControl("Field10TypeDropDown"), DropDownList).SelectedValue = obj.Field10Type
                CType(frmEdit.FindControl("Field10OptionsTextBox"), TextBox).Text = obj.Field10Options
                CType(frmEdit.FindControl("Field10HelpTextBox"), TextBox).Text = obj.Field10HelpText


                CType(frmEdit.FindControl("Field11LabelTextBox"), TextBox).Text = obj.Field11Label
                CType(frmEdit.FindControl("Field11TypeDropDown"), DropDownList).SelectedValue = obj.Field11Type
                CType(frmEdit.FindControl("Field11OptionsTextBox"), TextBox).Text = obj.Field11Options
                CType(frmEdit.FindControl("Field11HelpTextBox"), TextBox).Text = obj.Field11HelpText

                CType(frmEdit.FindControl("Field12LabelTextBox"), TextBox).Text = obj.Field12Label
                CType(frmEdit.FindControl("Field12TypeDropDown"), DropDownList).SelectedValue = obj.Field12Type
                CType(frmEdit.FindControl("Field12OptionsTextBox"), TextBox).Text = obj.Field12Options
                CType(frmEdit.FindControl("Field12HelpTextBox"), TextBox).Text = obj.Field12HelpText

                CType(frmEdit.FindControl("Field13LabelTextBox"), TextBox).Text = obj.Field13Label
                CType(frmEdit.FindControl("Field13TypeDropDown"), DropDownList).SelectedValue = obj.Field13Type
                CType(frmEdit.FindControl("Field13OptionsTextBox"), TextBox).Text = obj.Field13Options
                CType(frmEdit.FindControl("Field13HelpTextBox"), TextBox).Text = obj.Field13HelpText

                CType(frmEdit.FindControl("Field14LabelTextBox"), TextBox).Text = obj.Field14Label
                CType(frmEdit.FindControl("Field14TypeDropDown"), DropDownList).SelectedValue = obj.Field14Type
                CType(frmEdit.FindControl("Field14OptionsTextBox"), TextBox).Text = obj.Field14Options
                CType(frmEdit.FindControl("Field14HelpTextBox"), TextBox).Text = obj.Field14HelpText

                CType(frmEdit.FindControl("Field15LabelTextBox"), TextBox).Text = obj.Field15Label
                CType(frmEdit.FindControl("Field15TypeDropDown"), DropDownList).SelectedValue = obj.Field15Type
                CType(frmEdit.FindControl("Field15OptionsTextBox"), TextBox).Text = obj.Field15Options
                CType(frmEdit.FindControl("Field15HelpTextBox"), TextBox).Text = obj.Field15HelpText

                CType(frmEdit.FindControl("Field16LabelTextBox"), TextBox).Text = obj.Field16Label
                CType(frmEdit.FindControl("Field16TypeDropDown"), DropDownList).SelectedValue = obj.Field16Type
                CType(frmEdit.FindControl("Field16OptionsTextBox"), TextBox).Text = obj.Field16Options
                CType(frmEdit.FindControl("Field16HelpTextBox"), TextBox).Text = obj.Field16HelpText

                CType(frmEdit.FindControl("Field17LabelTextBox"), TextBox).Text = obj.Field17Label
                CType(frmEdit.FindControl("Field17TypeDropDown"), DropDownList).SelectedValue = obj.Field17Type
                CType(frmEdit.FindControl("Field17OptionsTextBox"), TextBox).Text = obj.Field17Options
                CType(frmEdit.FindControl("Field17HelpTextBox"), TextBox).Text = obj.Field17HelpText

                CType(frmEdit.FindControl("Field18LabelTextBox"), TextBox).Text = obj.Field18Label
                CType(frmEdit.FindControl("Field18TypeDropDown"), DropDownList).SelectedValue = obj.Field18Type
                CType(frmEdit.FindControl("Field18OptionsTextBox"), TextBox).Text = obj.Field18Options
                CType(frmEdit.FindControl("Field18HelpTextBox"), TextBox).Text = obj.Field18HelpText

                CType(frmEdit.FindControl("Field19LabelTextBox"), TextBox).Text = obj.Field19Label
                CType(frmEdit.FindControl("Field19TypeDropDown"), DropDownList).SelectedValue = obj.Field19Type
                CType(frmEdit.FindControl("Field19OptionsTextBox"), TextBox).Text = obj.Field19Options
                CType(frmEdit.FindControl("Field19HelpTextBox"), TextBox).Text = obj.Field19HelpText

                CType(frmEdit.FindControl("Field20LabelTextBox"), TextBox).Text = obj.Field20Label
                CType(frmEdit.FindControl("Field20TypeDropDown"), DropDownList).SelectedValue = obj.Field20Type
                CType(frmEdit.FindControl("Field20OptionsTextBox"), TextBox).Text = obj.Field20Options
                CType(frmEdit.FindControl("Field20HelpTextBox"), TextBox).Text = obj.Field20HelpText
            Catch
            End Try
        End If

        ' Show if this form has been used
        FormHasBeenUsedPanel.Visible = obj.HasBeenUsed

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
    End Sub

    ' Redirect back to the list on Cancel
    Protected Sub frmEdit_ModeChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewModeEventArgs) Handles frmEdit.ModeChanging
        If e.CancelingEdit OrElse e.Cancel Then
            GoBack()
        End If
    End Sub

    Protected Sub NursingNoteFormDataSource_SelectObject(ByVal sender As Object, ByVal e As Csla.Web.SelectObjectArgs) Handles NursingNoteFormDataSource.SelectObject
        e.BusinessObject = GetForm()
    End Sub

    Protected Sub NursingNoteFormDataSource_InsertObject(ByVal sender As Object, ByVal e As Csla.Web.InsertObjectArgs) Handles NursingNoteFormDataSource.InsertObject
        Dim obj As NursingNoteForm = GetForm()
        Csla.Data.DataMapper.Map(e.Values, obj, "ParentName")
        SaveForm(obj)
    End Sub

    Protected Sub NursingNoteFormDataSource_UpdateObject(ByVal sender As Object, ByVal e As Csla.Web.UpdateObjectArgs) Handles NursingNoteFormDataSource.UpdateObject
        Dim obj As NursingNoteForm = GetForm()
        Csla.Data.DataMapper.Map(e.Values, obj, New String() {"FormID", "ParentName"})
        SaveForm(obj)
    End Sub

    Protected Sub EvalFormDataSource_DeleteObject(ByVal sender As Object, ByVal e As Csla.Web.DeleteObjectArgs) Handles NursingNoteFormDataSource.DeleteObject
        Try
            NursingNoteForm.DeleteNursingNoteForm(Request("FormID"))
            Session("currentObject") = Nothing
            GoBack()
        Catch ex As Csla.DataPortalException
            Me.pnlError.Visible = True
            Me.lblError.Text = ex.BusinessException.Message
        Catch ex As Exception
            Me.pnlError.Visible = True
            Me.lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub GoBack()
        ' Redirect back to the landing page.
        ' Get the FormID of the node that needs to be shown.
        Dim formID As String = Request("FormID")

        ' If we are editing an existing form, Request("FormID") will hold
        ' the Form ID we're looking for.  But if we've just added a new form,
        ' we'll need to pull the Form ID from the business object.
        If Not IsNothing(Session("currentObject")) AndAlso TypeOf Session("currentObject") Is NursingNoteForm Then
            formID = DirectCast(Session("currentObject"), NursingNoteForm).FormID.ToString()
        End If

        Response.Redirect("NursingNote_Landing.aspx?ShowNursingNoteID=" & formID)
    End Sub

    Private Function GetForm() As NursingNoteForm
        Dim businessObject As Object = Session("currentObject")

        If IsNothing(businessObject) OrElse Not TypeOf businessObject Is NursingNoteForm Then
            Try
                Dim idString As String = Request("FormID")
                Dim parentID As String = Request("ParentID")

                If Not String.IsNullOrEmpty(idString) Then
                    businessObject = NursingNoteForm.GetNursingNoteForm(idString)
                ElseIf Not String.IsNullOrEmpty(parentID) Then
                    businessObject = NursingNoteForm.NewNursingNoteForm(parentID)
                Else
                    ' TODO - Create the Error page
                    Response.Redirect("NursingNote_Error.aspx")
                End If

                Session("currentObject") = businessObject
            Catch ex As System.Security.SecurityException
                ' TODO - Create the Error page
                Response.Redirect("Error.aspx?ErrorText=Security Exception!&ErrorDetails=" & Server.UrlEncode(ex.Message))
            End Try
        End If

        Return CType(businessObject, NursingNoteForm)
    End Function

    Private Sub SaveForm(ByVal theForm As NursingNoteForm)
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
        Session("CopiedNursingNoteForm") = Session("currentObject")
        Response.Redirect("NursingNote_Landing.aspx?ShowNursingNoteID=" & CType(Session("CopiedNursingNoteForm"), NursingNoteForm).FormID)
    End Sub

    Protected Sub FieldCommand(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim theForm As NursingNoteForm = GetForm()

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

End Class
