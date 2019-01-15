<%@ Page Title="ITW Maintenance - Evaluations - Form Edit" Language="VB" MasterPageFile="EvalSubMaster.master" AutoEventWireup="false" CodeFile="Form_Edit.aspx.vb" Inherits="Evals_Form_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<style type="text/css">
		.FormFieldDetails {
			margin-left: 20px;
			margin-top: 7px;
		}
		.OptionsDiv {
			margin-left: 30px;
			margin-bottom: 10px;
		}
		.DetailsHeading {
			font-weight: bold;
			color: #606060;
		}
	</style>
    
    <script type="text/javascript">
        function ToggleDiv(divName) {
            var div = document.getElementById(divName);

            if (div.style.display == "none")
                div.style.display = "block";
            else
                div.style.display = "none";
        }

        function FieldTypeValueChanged(dropdown, divName) {
            var div = document.getElementById(divName);
            var value = dropdown.options[dropdown.selectedIndex].value;

            if (value == "combobox" || value == "radio" || value == "scriblet" || value == "prepopulated memo")
                div.style.display = "block";
            else
                div.style.display = "none";
        }
        
        function PreviewReport() {
            window.open('Form_Preview.aspx?EvalID=<%= Request("EvalID") %>', 'FormPreview', "width=750,height=500,resizable,scrollbars");
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <div style="float: left; width: 300px;"><h3><asp:Literal ID="lblHeader" runat="server" /></h3></div>
    <asp:Panel ID="FormHasBeenUsedPanel" runat="server" Visible="false" CssClass="Attention" style="float: left; margin-bottom: 5px; width: 250px;">**This form has been used!</asp:Panel>
    
    <asp:panel id="pnlError" runat="server" Visible="false" cssclass="divError"><asp:Label ID="lblError" runat="server" /></asp:panel>
    <asp:FormView ID="frmEdit" runat="server" DataSourceID="EvalFormDataSource" DefaultMode="Edit" style="clear: both; min-width: 700px;">
        <EditItemTemplate>
            <table border="0" cellpadding="5" cellspacing="0" width="100%">
                <tr>
                    <td>Eval ID:</td>
                    <td colspan="5"><asp:Label ID="EvalIDLabel" runat="server" Text='<%# Eval("EvalID") %>' /></td>
                </tr>
                <tr>
                    <td>Parent Folder:</td>
                    <td colspan="5"><asp:Label ID="ParentFolderLabel" runat="server" /></td>
                </tr>
                <tr>
                    <td>Full Path:</td>
                    <td colspan="5"><asp:Label ID="EPathLabel" runat="server" Text='<%# Eval("EPath") %>' /></td>
                </tr>
                <tr>
                    <td>Form Name:</td>
                    <td colspan="5"><asp:TextBox ID="FormNameTextBox" runat="server" Text='<%# Bind("FormName") %>' 
                        MaxLength="200" Width="400" TabIndex="1" />&nbsp;<asp:RequiredFieldValidator runat="server" 
                        id="RequiredFieldValidator1" ControlToValidate="FormNameTextBox" Text="(required)" /></td>
                </tr>
                <tr>
                    <td>Short Name:</td>
                    <td colspan="5"><asp:TextBox ID="ShortNameTextBox" runat="server" Text='<%# Bind("ShortName") %>' 
                        MaxLength="200" Width="400" TabIndex="2" /></td>
                </tr>
                <tr>
                    <td>Report Description:</td>
                    <td colspan="5"><asp:TextBox ID="ReportDescriptionTextBox" runat="server" Text='<%# Bind("ReportDescription") %>' 
                        TextMode="MultiLine" Rows="3" Columns="45" TabIndex="3" /></td>
                </tr>
                <tr>
                    <td>EClass:</td>
                    <td colspan="5"><asp:DropDownList ID="EClassDropDownList" runat="server" DataSourceID="eClassDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("EClass") %>' TabIndex="4" /></td>
                </tr>
                <tr>
                    <td>EGroup Order:</td>
                    <td colspan="5"><asp:TextBox ID="EGroupOrderTextBox" runat="server" Text='<%# Bind("EGroupOrder") %>' 
                        Width="50" TabIndex="5" />&nbsp;<asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="EGroupOrderTextBox" ErrorMessage="(invalid)" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator></td>
                </tr>
                <tr>
                    <td>EGroup:</td>
                    <td colspan="5"><asp:TextBox ID="EGroupTextBox" runat="server" Text='<%# Bind("EGroup") %>'
                        width="400" MaxLength="200" TabIndex="6" /></td>
                </tr>
                <tr>
                    <td>Calculated:</td>
                    <td colspan="5">
						<asp:RadioButton ID="CalculatedRadioButton" runat="server" GroupName="Calculated" Text="Calculated" TabIndex="7" Checked='<%# Bind("Calculated")%>' />
						<asp:RadioButton ID="NoResultRadioButton" runat="server" GroupName="Calculated" Text="No Result" TabIndex="7" Checked='<%# Bind("NoResult")%>' />

						<%--<asp:CheckBox ID="CalculatedCheckBox" runat="server" Checked='<%# Bind("Calculated") %>' 
                        Text="" TabIndex="7" />--%>&nbsp;<asp:TextBox ID="CalculationTextBox" Runat="server" 
                        Text='<%# Bind("Calculation") %>' TabIndex="8" />
						
						<asp:RadioButton ID="NoCalculationRadioButton" runat="server" GroupName="Calculated" Text="No Calculation" />
                    </td>
                </tr>
				<%--<tr>
					<td>Report Template:</td>
					<td colspan="5"><asp:DropDownList ID="ReportTemplateDropDownList" runat="server" DataSourceID="ReportTemplatesDataSource" DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("ReportTemplateID") %>' TabIndex="9" /></td>
				</tr>--%>
                <tr>
                    <td>Display Order:</td>
                    <td colspan="5"><asp:TextBox ID="DisplayOrderTextBox" runat="server" Text='<%# Bind("DisplayOrder") %>' 
                        Width="50" TabIndex="10" />&nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ErrorMessage="(invalid)" ControlToValidate="DisplayOrderTextBox" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%;">Required:</td>
                    <td style="width: 12%;"><asp:CheckBox ID="RequiredCheckBox" runat="server" Checked='<%# Bind("Required") %>' 
                        TabIndex="11" /></td>
                    <td style="width: 5%;">&nbsp;</td>
                    <td style="width: 30%;">Functional Objective Finding:</td>
                    <td style="width: 12%;"><asp:CheckBox ID="FunctionalObjectiveFindingCheckBox" runat="server" Checked='<%# Bind("FunctionalObjectiveFinding") %>'
                        TabIndex="16" /></td>
                    <td style="width: 16%;">&nbsp;</td>
                </tr>
                <tr>
                    <td class="Attention">Inactive:</td>
                    <td><asp:CheckBox ID="InactiveCheckBox" runat="server" Checked='<%# Bind("Inactive") %>' TabIndex="12" /></td>
                    <td>&nbsp;</td>
                    <td>Required for Pediatrics:</td>
                    <td><asp:CheckBox ID="RequiredForPediatricsCheckBox" runat="server" Checked='<%# Bind("RequiredForPediatrics") %>' 
                        TabIndex="17" /></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Problem:</td>
                    <td><asp:CheckBox ID="ProblemCheckBox" runat="server" Checked='<%# Bind("Problem") %>' TabIndex="13" /></td>
                    <td>&nbsp;</td>
                    <td>Subjective:</td>
                    <td><asp:CheckBox ID="SubjectiveCheckBox" runat="server" Checked='<%# Bind("Subjective") %>' TabIndex="18" /></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Patient Goal:</td>
                    <td><asp:CheckBox ID="PatientGoalCheckBox" runat="server" Checked='<%# Bind("PatientGoal") %>' TabIndex="14" /></td>
                    <td>&nbsp;</td>
                    <td>Test:</td>
                    <td><asp:CheckBox ID="TestCheckBox" runat="server" Checked='<%# Bind("Test") %>' TabIndex="19" /></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Outcome:</td>
                    <td colspan="5"><asp:CheckBox ID="OutcomeCheckBox" runat="server" Checked='<%# Bind("Outcome") %>' TabIndex="15" /></td>
                </tr>
                <tr>
                    <td colspan="6">&nbsp;</td>
                </tr>
            </table>
            <!-- Eval Form Fields -->
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>01:</td>
                    <td>
                        <asp:TextBox ID="Field01LabelTextBox" runat="server" Width="150" MaxLength="30" 
                            Text='<%# Bind("Field01Label") %>' TabIndex="20" />&nbsp;
                        <asp:DropDownList ID="Field01TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field01Type") %>' TabIndex="21" />&nbsp;
                        <asp:DropDownList ID="Field01ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field01Validation") %>' TabIndex="22" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                            onclick="javascript:ToggleDiv('divField01Help');" />&nbsp;
                        <asp:Button ID="Field01InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="1" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field01RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="1" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field01ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="1" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field01LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField01Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field01OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field01Options") %>' TabIndex="23" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField01Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field01HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field01HelpText") %>' TabIndex="24" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>02:</td>
                    <td>
                        <asp:TextBox ID="Field02LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field02Label") %>' TabIndex="25" />&nbsp;
                        <asp:DropDownList ID="Field02TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field02Type") %>' TabIndex="26" />&nbsp;
                        <asp:DropDownList ID="Field02ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field02Validation") %>' TabIndex="27" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField02Help');" />&nbsp;
                        <asp:Button ID="Field02InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="2" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field02RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="2" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field02ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="2" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field02LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField02Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field02OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field02Options") %>' TabIndex="28" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField02Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field02HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field02HelpText") %>' TabIndex="29" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>03:</td>
                    <td>
                        <asp:TextBox ID="Field03LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field03Label") %>' TabIndex="30" />&nbsp;
                        <asp:DropDownList ID="Field03TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field03Type") %>' TabIndex="31" />&nbsp;
                        <asp:DropDownList ID="Field03ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field03Validation") %>' TabIndex="32" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField03Help');" />&nbsp;
                        <asp:Button ID="Field03InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="3" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field03RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="3" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field03ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="3" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field03LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField03Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field03OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field03Options") %>' TabIndex="33" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField03Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field03HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field03HelpText") %>' TabIndex="34" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>04:</td>
                    <td>
                        <asp:TextBox ID="Field04LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field04Label") %>' TabIndex="35" />&nbsp;
                        <asp:DropDownList ID="Field04TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field04Type") %>' TabIndex="36" />&nbsp;
                        <asp:DropDownList ID="Field04ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field04Validation") %>' TabIndex="37" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField04Help');" />&nbsp;
                        <asp:Button ID="Field04InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="4" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field04RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="4" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field04ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="4" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field04LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField04Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field04OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field04Options") %>' TabIndex="38" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField04Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field04HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field04HelpText") %>' TabIndex="39" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>05:</td>
                    <td>
                        <asp:TextBox ID="Field05LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field05Label") %>' TabIndex="40" />&nbsp;
                        <asp:DropDownList ID="Field05TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field05Type") %>' TabIndex="41" />&nbsp;
                        <asp:DropDownList ID="Field05ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field05Validation") %>' TabIndex="42" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField05Help');" />&nbsp;
                        <asp:Button ID="Field05InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="5" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field05RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="5" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field05ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="5" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field05LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField05Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field05OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field05Options") %>' TabIndex="43" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField05Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field05HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field05HelpText") %>' TabIndex="44" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>06:</td>
                    <td>
                        <asp:TextBox ID="Field06LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field06Label") %>' TabIndex="45" />&nbsp;
                        <asp:DropDownList ID="Field06TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field06Type") %>' TabIndex="46" />&nbsp;
                        <asp:DropDownList ID="Field06ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field06Validation") %>' TabIndex="47" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField06Help');" />&nbsp;
                        <asp:Button ID="Field06InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="6" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field06RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="6" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field06ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="6" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field06LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField06Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field06OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field06Options") %>' TabIndex="48" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField06Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field06HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field06HelpText") %>' TabIndex="49" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>07:</td>
                    <td>
                        <asp:TextBox ID="Field07LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field07Label") %>' TabIndex="50" />&nbsp;
                        <asp:DropDownList ID="Field07TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field07Type") %>' TabIndex="51" />&nbsp;
                        <asp:DropDownList ID="Field07ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field07Validation") %>' TabIndex="52" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField07Help');" />&nbsp;
                        <asp:Button ID="Field07InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="7" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field07RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="7" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field07ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="7" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field07LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField07Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field07OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field07Options") %>' TabIndex="53" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField07Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field07HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field07HelpText") %>' TabIndex="54" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>08:</td>
                    <td>
                        <asp:TextBox ID="Field08LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field08Label") %>' TabIndex="55" />&nbsp;
                        <asp:DropDownList ID="Field08TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field08Type") %>' TabIndex="56" />&nbsp;
                        <asp:DropDownList ID="Field08ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field08Validation") %>' TabIndex="57" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField08Help');" />&nbsp;
                        <asp:Button ID="Field08InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="8" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field08RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="8" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field08ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="8" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field08LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField08Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field08OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field08Options") %>' TabIndex="58" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField08Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field08HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field08HelpText") %>' TabIndex="59" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>09:</td>
                    <td>
                        <asp:TextBox ID="Field09LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field09Label") %>' TabIndex="60" />&nbsp;
                        <asp:DropDownList ID="Field09TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field09Type") %>' TabIndex="61" />&nbsp;
                        <asp:DropDownList ID="Field09ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field09Validation") %>' TabIndex="62" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField09Help');" />&nbsp;
                        <asp:Button ID="Field09InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="9" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field09RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="9" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field09ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="9" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field09LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField09Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field09OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field09Options") %>' TabIndex="63" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField09Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field09HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field09HelpText") %>' TabIndex="64" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>10:</td>
                    <td>
                        <asp:TextBox ID="Field10LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field10Label") %>' TabIndex="65" />&nbsp;
                        <asp:DropDownList ID="Field10TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field10Type") %>' TabIndex="66" />&nbsp;
                        <asp:DropDownList ID="Field10ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field10Validation") %>' TabIndex="67" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField10Help');" />&nbsp;
                        <asp:Button ID="Field10InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="10" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field10RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="10" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field10ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="10" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field10LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField10Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field10OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field10Options") %>' TabIndex="68" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField10Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field10HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field10HelpText") %>' TabIndex="69" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                
                <tr>
                    <td>11:</td>
                    <td>
                        <asp:TextBox ID="Field11LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field11Label") %>' TabIndex="70" />&nbsp;
                        <asp:DropDownList ID="Field11TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field11Type") %>' TabIndex="71" />&nbsp;
                        <asp:DropDownList ID="Field11ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field11Validation") %>' TabIndex="72" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField11Help');" />&nbsp;
                        <asp:Button ID="Field11InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="11" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field11RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="11" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field11ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="11" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field11LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField11Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field11OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field11Options") %>' TabIndex="73" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField11Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field11HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field11HelpText") %>' TabIndex="74" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>12:</td>
                    <td>
                        <asp:TextBox ID="Field12LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field12Label") %>' TabIndex="75" />&nbsp;
                        <asp:DropDownList ID="Field12TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field12Type") %>' TabIndex="76" />&nbsp;
                        <asp:DropDownList ID="Field12ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field12Validation") %>' TabIndex="77" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField12Help');" />&nbsp;
                        <asp:Button ID="Field12InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="12" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field12RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="12" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field12ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="12" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field12LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField12Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field12OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field12Options") %>' TabIndex="78" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField12Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field12HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field12HelpText") %>' TabIndex="79" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>13:</td>
                    <td>
                        <asp:TextBox ID="Field13LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field13Label") %>' TabIndex="80" />&nbsp;
                        <asp:DropDownList ID="Field13TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field13Type") %>' TabIndex="81" />&nbsp;
                        <asp:DropDownList ID="Field13ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field13Validation") %>' TabIndex="82" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField13Help');" />&nbsp;
                        <asp:Button ID="Field13InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="13" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field13RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="13" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field13ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="13" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field13LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField13Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field13OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field13Options") %>' TabIndex="83" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField13Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field13HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field13HelpText") %>' TabIndex="84" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>14:</td>
                    <td>
                        <asp:TextBox ID="Field14LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field14Label") %>' TabIndex="85" />&nbsp;
                        <asp:DropDownList ID="Field14TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field14Type") %>' TabIndex="86" />&nbsp;
                        <asp:DropDownList ID="Field14ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field14Validation") %>' TabIndex="87" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField14Help');" />&nbsp;
                        <asp:Button ID="Field14InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="14" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field14RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="14" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field14ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="14" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field14LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField14Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field14OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field14Options") %>' TabIndex="88" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField14Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field14HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field14HelpText") %>' TabIndex="89" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>15:</td>
                    <td>
                        <asp:TextBox ID="Field15LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field15Label") %>' TabIndex="90" />&nbsp;
                        <asp:DropDownList ID="Field15TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field15Type") %>' TabIndex="91" />&nbsp;
                        <asp:DropDownList ID="Field15ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field15Validation") %>' TabIndex="92" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField15Help');" />&nbsp;
                        <asp:Button ID="Field15InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="15" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field15RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="15" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field15ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="15" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field15LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField15Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field15OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field15Options") %>' TabIndex="93" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField15Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field15HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field15HelpText") %>' TabIndex="94" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>16:</td>
                    <td>
                        <asp:TextBox ID="Field16LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field16Label") %>' TabIndex="95" />&nbsp;
                        <asp:DropDownList ID="Field16TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field16Type") %>' TabIndex="96" />&nbsp;
                        <asp:DropDownList ID="Field16ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field16Validation") %>' TabIndex="97" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField16Help');" />&nbsp;
                        <asp:Button ID="Field16InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="16" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field16RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="16" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field16ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="16" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field16LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField16Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field16OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field16Options") %>' TabIndex="98" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField16Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field16HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field16HelpText") %>' TabIndex="99" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>17:</td>
                    <td>
                        <asp:TextBox ID="Field17LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field17Label") %>' TabIndex="100" />&nbsp;
                        <asp:DropDownList ID="Field17TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field17Type") %>' TabIndex="101" />&nbsp;
                        <asp:DropDownList ID="Field17ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field17Validation") %>' TabIndex="102" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField17Help');" />&nbsp;
                        <asp:Button ID="Field17InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="17" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field17RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="17" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field17ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="17" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field17LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField17Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field17OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field17Options") %>' TabIndex="103" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField17Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field17HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field17HelpText") %>' TabIndex="104" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>18:</td>
                    <td>
                        <asp:TextBox ID="Field18LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field18Label") %>' TabIndex="105" />&nbsp;
                        <asp:DropDownList ID="Field18TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field18Type") %>' TabIndex="106" />&nbsp;
                        <asp:DropDownList ID="Field18ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field18Validation") %>' TabIndex="107" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField18Help');" />&nbsp;
                        <asp:Button ID="Field18InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="18" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field18RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="18" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field18ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="18" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field18LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField18Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field18OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field18Options") %>' TabIndex="108" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField18Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field18HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field18HelpText") %>' TabIndex="109" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>19:</td>
                    <td>
                        <asp:TextBox ID="Field19LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field19Label") %>' TabIndex="110" />&nbsp;
                        <asp:DropDownList ID="Field19TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field19Type") %>' TabIndex="111" />&nbsp;
                        <asp:DropDownList ID="Field19ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field19Validation") %>' TabIndex="112" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField19Help');" />&nbsp;
                        <asp:Button ID="Field19InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="19" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field19RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="19" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field19ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="19" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field19LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField19Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field19OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field19Options") %>' TabIndex="113" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField19Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field19HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field19HelpText") %>' TabIndex="114" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>20:</td>
                    <td>
                        <asp:TextBox ID="Field20LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field20Label") %>' TabIndex="115" />&nbsp;
                        <asp:DropDownList ID="Field20TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field20Type") %>' TabIndex="116" />&nbsp;
                        <asp:DropDownList ID="Field20ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field20Validation") %>' TabIndex="117" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField20Help');" />&nbsp;
                        <asp:Button ID="Field20ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="20" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field20LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField20Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field20OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field20Options") %>' TabIndex="118" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField20Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field20HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field20HelpText") %>' TabIndex="119" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table><br />
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td colspan="2"><asp:CheckBox ID="LockedCheckBox" runat="server" Text="Locked" 
                        TextAlign="Left" Checked='<%# Bind("Locked") %>' TabIndex="120" /></td>
                </tr>
                <tr>
                    <td colspan="2"><asp:TextBox ID="LockNotesTextBox" runat="server" TextMode="MultiLine"
                        Rows="3" Columns="78" Text='<%# Bind("LockNotes") %>' TabIndex="121" /></td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td><asp:Button ID="UpdateButton" runat="server" CausesValidation="True" 
                        CommandName="Update" Text="Save Changes" TabIndex="122" UseSubmitBehavior="false" />&nbsp;&nbsp;&nbsp;<asp:Button ID="UpdateCancelButton" 
                        runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel/Undo" TabIndex="123" UseSubmitBehavior="false" /></td>
                    <td align="right"><asp:Button ID="CopyButton" runat="server" Text="Copy This Form" CausesValidation="false" 
                        OnClick="CopyButton_Click" TabIndex="124" UseSubmitBehavior="false" /></td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td><input type="button" id="PreviewButton" value="Preview Form" 
                        onclick="PreviewReport();" tabindex="125" />&nbsp;<strong>Please save changes before previewing!</strong></td>
                    <td align="right"><asp:Button ID="DeleteButton" runat="server" Text="Delete This Form"
                        CausesValidation="false" CommandName="Delete" 
                        OnClientClick="if (!confirm('Are you sure you wish to remove this form?')) return false;" UseSubmitBehavior="false" /></td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
            </table>
        </EditItemTemplate>
        <InsertItemTemplate>
            <table border="0" cellpadding="5" cellspacing="0">
                <tr>
                    <td>Parent Folder:</td>
                    <td colspan="5"><asp:Label ID="ParentFolderLabel" runat="server" /></td>
                </tr>
                <tr>
                    <td>Form Name:</td>
                    <td colspan="5"><asp:TextBox ID="FormNameTextBox" runat="server" Text='<%# Bind("FormName") %>' 
                        MaxLength="200" Width="400" TabIndex="1" />&nbsp;<asp:RequiredFieldValidator runat="server" 
                        id="RequiredFieldValidator1" ControlToValidate="FormNameTextBox" Text="(required)" /></td>
                </tr>
                <!--<tr>
                    <td>Short Name:</td>
                    <td colspan="5"><asp:TextBox ID="ShortNameTextBox" runat="server" Text='<%# Bind("ShortName") %>' 
                        MaxLength="200" Width="400" TabIndex="2" /></td>
                </tr>-->
                <tr>
                    <td>Report Description:</td>
                    <td colspan="5"><asp:TextBox ID="ReportDescriptionTextBox" runat="server" Text='<%# Bind("ReportDescription") %>' 
                        TextMode="MultiLine" Rows="3" Columns="45" TabIndex="3" /></td>
                </tr>
                <tr>
                    <td>EClass:</td>
                    <td colspan="5"><asp:DropDownList ID="EClassDropDownList" runat="server" DataSourceID="eClassDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("EClass") %>' TabIndex="4" /></td>
                </tr>
                <tr>
                    <td>Calculated:</td>
                    <td colspan="5">
						<asp:RadioButton ID="CalculatedRadioButton" runat="server" GroupName="Calculated" Text="Calculated" TabIndex="5" Checked='<%# Bind("Calculated")%>' />
						<asp:RadioButton ID="NoResultRadioButton" runat="server" GroupName="Calculated" Text="No Result" TabIndex="5" Checked='<%# Bind("NoResult")%>' />

						<%--<asp:CheckBox ID="CalculatedCheckBox" runat="server" Checked='<%# Bind("Calculated") %>' 
                        Text="" TabIndex="5" />--%>&nbsp;<asp:TextBox ID="CalculationTextBox" Runat="server" 
                        Text='<%# Bind("Calculation") %>' TabIndex="6" />
						
						<asp:RadioButton ID="NoCalculationRadioButton" runat="server" GroupName="Calculated" Text="No Calculation" />
                    </td>
                </tr>
				<%--<tr>
					<td>Report Template:</td>
					<td colspan="5"><asp:DropDownList ID="ReportTemplateDropDownList" runat="server" DataSourceID="ReportTemplatesDataSource" DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("ReportTemplateID") %>' TabIndex="7" /></td>
				</tr>--%>
                <tr>
                    <td>Display Order:</td>
                    <td colspan="5"><asp:TextBox ID="DisplayOrderTextBox" runat="server" Text='<%# Bind("DisplayOrder") %>' 
                        Width="50" TabIndex="8" />&nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ControlToValidate="DisplayOrderTextBox" ErrorMessage="(invalid)" Operator="DataTypeCheck" 
                        Type="Integer"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%;">Required:</td>
                    <td style="width: 12%;"><asp:CheckBox ID="RequiredCheckBox" runat="server"
                        Checked='<%# Bind("Required") %>' TabIndex="9" /></td>
                    <td style="width: 5%;">&nbsp;</td>
                    <td style="width: 30%;">Functional Objective Finding:</td>
                    <td style="width: 12%;"><asp:CheckBox ID="FunctionalObjectiveFindingCheckBox" runat="server" 
                        Checked='<%# Bind("FunctionalObjectiveFinding") %>' TabIndex="14" /></td>
                    <td style="width: 16%;">&nbsp;</td>
                </tr>
                <tr>
                    <td>Inactive:</td>
                    <td><asp:CheckBox ID="InactiveCheckBox" runat="server" 
                        Checked='<%# Bind("Inactive") %>' TabIndex="10" /></td>
                    <td>&nbsp;</td>
                    <td>Required for Pediatrics:</td>
                    <td><asp:CheckBox ID="RequiredForPediatricsCheckBox" runat="server" 
                        Checked='<%# Bind("RequiredForPediatrics") %>' TabIndex="15" /></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Problem:</td>
                    <td><asp:CheckBox ID="ProblemCheckBox" runat="server" 
                        Checked='<%# Bind("Problem") %>' TabIndex="11" /></td>
                    <td>&nbsp;</td>
                    <td>Subjective:</td>
                    <td><asp:CheckBox ID="SubjectiveCheckBox" runat="server" 
                        Checked='<%# Bind("Subjective") %>' TabIndex="16" /></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Patient Goal:</td>
                    <td><asp:CheckBox ID="PatientGoalCheckBox" runat="server" 
                        Checked='<%# Bind("PatientGoal") %>' TabIndex="12" /></td>
                    <td>&nbsp;</td>
                    <td>Test:</td>
                    <td><asp:CheckBox ID="TestCheckBox" runat="server" 
                        Checked='<%# Bind("Test") %>' TabIndex="17" /></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Outcome:</td>
                    <td colspan="5"><asp:CheckBox ID="OutcomeCheckBox" runat="server" 
                        Checked='<%# Bind("Outcome") %>' TabIndex="13" /></td>
                </tr>
                <tr>
                    <td colspan="6">&nbsp;</td>
                </tr>
            </table>
            <!-- Eval Form Fields -->
            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td>01:</td>
                    <td>
                        <asp:TextBox ID="Field01LabelTextBox" runat="server" Width="150" MaxLength="30" 
                            Text='<%# Bind("Field01Label") %>' TabIndex="20" />&nbsp;
                        <asp:DropDownList ID="Field01TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field01Type") %>' TabIndex="21" />&nbsp;
                        <asp:DropDownList ID="Field01ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field01Validation") %>' TabIndex="22" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                            onclick="javascript:ToggleDiv('divField01Help');" />&nbsp;
                        <asp:Button ID="Field01InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="1" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field01RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="1" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field01ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="1" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field01LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField01Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field01OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field01Options") %>' TabIndex="23" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField01Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field01HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field01HelpText") %>' TabIndex="24" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>02:</td>
                    <td>
                        <asp:TextBox ID="Field02LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field02Label") %>' TabIndex="25" />&nbsp;
                        <asp:DropDownList ID="Field02TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field02Type") %>' TabIndex="26" />&nbsp;
                        <asp:DropDownList ID="Field02ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field02Validation") %>' TabIndex="27" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField02Help');" />&nbsp;
                        <asp:Button ID="Field02InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="2" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field02RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="2" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field02ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="2" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field02LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField02Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field02OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field02Options") %>' TabIndex="28" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField02Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field02HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field02HelpText") %>' TabIndex="29" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>03:</td>
                    <td>
                        <asp:TextBox ID="Field03LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field03Label") %>' TabIndex="30" />&nbsp;
                        <asp:DropDownList ID="Field03TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field03Type") %>' TabIndex="31" />&nbsp;
                        <asp:DropDownList ID="Field03ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field03Validation") %>' TabIndex="32" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField03Help');" />&nbsp;
                        <asp:Button ID="Field03InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="3" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field03RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="3" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field03ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="3" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field03LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField03Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field03OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field03Options") %>' TabIndex="33" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField03Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field03HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field03HelpText") %>' TabIndex="34" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>04:</td>
                    <td>
                        <asp:TextBox ID="Field04LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field04Label") %>' TabIndex="35" />&nbsp;
                        <asp:DropDownList ID="Field04TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field04Type") %>' TabIndex="36" />&nbsp;
                        <asp:DropDownList ID="Field04ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field04Validation") %>' TabIndex="37" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField04Help');" />&nbsp;
                        <asp:Button ID="Field04InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="4" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field04RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="4" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field04ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="4" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field04LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField04Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field04OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field04Options") %>' TabIndex="38" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField04Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field04HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field04HelpText") %>' TabIndex="39" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>05:</td>
                    <td>
                        <asp:TextBox ID="Field05LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field05Label") %>' TabIndex="40" />&nbsp;
                        <asp:DropDownList ID="Field05TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field05Type") %>' TabIndex="41" />&nbsp;
                        <asp:DropDownList ID="Field05ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field05Validation") %>' TabIndex="42" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField05Help');" />&nbsp;
                        <asp:Button ID="Field05InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="5" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field05RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="5" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field05ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="5" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field05LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField05Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field05OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field05Options") %>' TabIndex="43" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField05Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field05HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field05HelpText") %>' TabIndex="44" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>06:</td>
                    <td>
                        <asp:TextBox ID="Field06LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field06Label") %>' TabIndex="45" />&nbsp;
                        <asp:DropDownList ID="Field06TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field06Type") %>' TabIndex="46" />&nbsp;
                        <asp:DropDownList ID="Field06ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field06Validation") %>' TabIndex="47" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField06Help');" />&nbsp;
                        <asp:Button ID="Field06InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="6" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field06RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="6" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field06ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="6" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field06LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField06Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field06OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field06Options") %>' TabIndex="48" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField06Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field06HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field06HelpText") %>' TabIndex="49" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>07:</td>
                    <td>
                        <asp:TextBox ID="Field07LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field07Label") %>' TabIndex="50" />&nbsp;
                        <asp:DropDownList ID="Field07TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field07Type") %>' TabIndex="51" />&nbsp;
                        <asp:DropDownList ID="Field07ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field07Validation") %>' TabIndex="52" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField07Help');" />&nbsp;
                        <asp:Button ID="Field07InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="7" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field07RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="7" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field07ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="7" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field07LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField07Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field07OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field07Options") %>' TabIndex="53" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField07Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field07HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field07HelpText") %>' TabIndex="54" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>08:</td>
                    <td>
                        <asp:TextBox ID="Field08LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field08Label") %>' TabIndex="55" />&nbsp;
                        <asp:DropDownList ID="Field08TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field08Type") %>' TabIndex="56" />&nbsp;
                        <asp:DropDownList ID="Field08ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field08Validation") %>' TabIndex="57" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField08Help');" />&nbsp;
                        <asp:Button ID="Field08InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="8" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field08RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="8" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field08ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="8" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field08LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField08Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field08OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field08Options") %>' TabIndex="58" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField08Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field08HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field08HelpText") %>' TabIndex="59" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>09:</td>
                    <td>
                        <asp:TextBox ID="Field09LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field09Label") %>' TabIndex="60" />&nbsp;
                        <asp:DropDownList ID="Field09TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field09Type") %>' TabIndex="61" />&nbsp;
                        <asp:DropDownList ID="Field09ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field09Validation") %>' TabIndex="62" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField09Help');" />&nbsp;
                        <asp:Button ID="Field09InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="9" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field09RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="9" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field09ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="9" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field09LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField09Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field09OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field09Options") %>' TabIndex="63" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField09Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field09HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field09HelpText") %>' TabIndex="64" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>10:</td>
                    <td>
                        <asp:TextBox ID="Field10LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field10Label") %>' TabIndex="65" />&nbsp;
                        <asp:DropDownList ID="Field10TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field10Type") %>' TabIndex="66" />&nbsp;
                        <asp:DropDownList ID="Field10ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field10Validation") %>' TabIndex="67" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField10Help');" />&nbsp;
                        <asp:Button ID="Field10InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="10" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field10RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="10" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field10ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="10" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field10LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField10Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field10OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field10Options") %>' TabIndex="68" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField10Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field10HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field10HelpText") %>' TabIndex="69" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                
                <tr>
                    <td>11:</td>
                    <td>
                        <asp:TextBox ID="Field11LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field11Label") %>' TabIndex="70" />&nbsp;
                        <asp:DropDownList ID="Field11TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field11Type") %>' TabIndex="71" />&nbsp;
                        <asp:DropDownList ID="Field11ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field11Validation") %>' TabIndex="72" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField11Help');" />&nbsp;
                        <asp:Button ID="Field11InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="11" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field11RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="11" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field11ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="11" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field11LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField11Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field11OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field11Options") %>' TabIndex="73" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField11Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field11HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field11HelpText") %>' TabIndex="74" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>12:</td>
                    <td>
                        <asp:TextBox ID="Field12LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field12Label") %>' TabIndex="75" />&nbsp;
                        <asp:DropDownList ID="Field12TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field12Type") %>' TabIndex="76" />&nbsp;
                        <asp:DropDownList ID="Field12ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field12Validation") %>' TabIndex="77" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField12Help');" />&nbsp;
                        <asp:Button ID="Field12InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="12" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field12RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="12" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field12ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="12" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field12LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField12Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field12OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field12Options") %>' TabIndex="78" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField12Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field12HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field12HelpText") %>' TabIndex="79" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>13:</td>
                    <td>
                        <asp:TextBox ID="Field13LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field13Label") %>' TabIndex="80" />&nbsp;
                        <asp:DropDownList ID="Field13TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field13Type") %>' TabIndex="81" />&nbsp;
                        <asp:DropDownList ID="Field13ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field13Validation") %>' TabIndex="82" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField13Help');" />&nbsp;
                        <asp:Button ID="Field13InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="13" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field13RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="13" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field13ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="13" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field13LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField13Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field13OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field13Options") %>' TabIndex="83" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField13Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field13HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field13HelpText") %>' TabIndex="84" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>14:</td>
                    <td>
                        <asp:TextBox ID="Field14LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field14Label") %>' TabIndex="85" />&nbsp;
                        <asp:DropDownList ID="Field14TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field14Type") %>' TabIndex="86" />&nbsp;
                        <asp:DropDownList ID="Field14ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field14Validation") %>' TabIndex="87" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField14Help');" />&nbsp;
                        <asp:Button ID="Field14InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="14" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field14RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="14" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field14ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="14" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field14LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField14Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field14OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field14Options") %>' TabIndex="88" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField14Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field14HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field14HelpText") %>' TabIndex="89" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>15:</td>
                    <td>
                        <asp:TextBox ID="Field15LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field15Label") %>' TabIndex="90" />&nbsp;
                        <asp:DropDownList ID="Field15TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field15Type") %>' TabIndex="91" />&nbsp;
                        <asp:DropDownList ID="Field15ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field15Validation") %>' TabIndex="92" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField15Help');" />&nbsp;
                        <asp:Button ID="Field15InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="15" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field15RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="15" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field15ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="15" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field15LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField15Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field15OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field15Options") %>' TabIndex="93" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField15Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field15HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field15HelpText") %>' TabIndex="94" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>16:</td>
                    <td>
                        <asp:TextBox ID="Field16LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field16Label") %>' TabIndex="95" />&nbsp;
                        <asp:DropDownList ID="Field16TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field16Type") %>' TabIndex="96" />&nbsp;
                        <asp:DropDownList ID="Field16ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field16Validation") %>' TabIndex="97" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField16Help');" />&nbsp;
                        <asp:Button ID="Field16InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="16" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field16RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="16" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field16ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="16" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field16LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField16Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field16OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field16Options") %>' TabIndex="98" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField16Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field16HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field16HelpText") %>' TabIndex="99" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>17:</td>
                    <td>
                        <asp:TextBox ID="Field17LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field17Label") %>' TabIndex="100" />&nbsp;
                        <asp:DropDownList ID="Field17TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field17Type") %>' TabIndex="101" />&nbsp;
                        <asp:DropDownList ID="Field17ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field17Validation") %>' TabIndex="102" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField17Help');" />&nbsp;
                        <asp:Button ID="Field17InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="17" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field17RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="17" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field17ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="17" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field17LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField17Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field17OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field17Options") %>' TabIndex="103" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField17Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field17HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field17HelpText") %>' TabIndex="104" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>18:</td>
                    <td>
                        <asp:TextBox ID="Field18LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field18Label") %>' TabIndex="105" />&nbsp;
                        <asp:DropDownList ID="Field18TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field18Type") %>' TabIndex="106" />&nbsp;
                        <asp:DropDownList ID="Field18ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field18Validation") %>' TabIndex="107" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField18Help');" />&nbsp;
                        <asp:Button ID="Field18InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="18" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field18RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="18" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field18ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="18" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field18LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField18Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field18OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field18Options") %>' TabIndex="108" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField18Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field18HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field18HelpText") %>' TabIndex="109" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>19:</td>
                    <td>
                        <asp:TextBox ID="Field19LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field19Label") %>' TabIndex="110" />&nbsp;
                        <asp:DropDownList ID="Field19TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field19Type") %>' TabIndex="111" />&nbsp;
                        <asp:DropDownList ID="Field19ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field19Validation") %>' TabIndex="112" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField19Help');" />&nbsp;
                        <asp:Button ID="Field19InsertButton" runat="server" Text=" + " ToolTip="Insert Field Here" CommandArgument="19" CommandName="InsertField" OnCommand="FieldCommand" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field19RemoveButton" runat="server" Text=" - " ToolTip="Remove this Field" CommandArgument="19" CommandName="RemoveField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to remove this field?')) return false;" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="Field19ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="19" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field19LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField19Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field19OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field19Options") %>' TabIndex="113" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField19Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field19HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field19HelpText") %>' TabIndex="114" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>20:</td>
                    <td>
                        <asp:TextBox ID="Field20LabelTextBox" runat="server" Width="150" MaxLength="30" 
                        Text='<%# Bind("Field20Label") %>' TabIndex="115" />&nbsp;
                        <asp:DropDownList ID="Field20TypeDropDown" runat="server" DataSourceID="FieldTypeDataSource" 
                        DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field20Type") %>' TabIndex="116" />&nbsp;
                        <asp:DropDownList ID="Field20ValidationDropDown" runat="server" DataSourceID="FieldValidationDataSource" 
                            DataTextField="Value" DataValueField="Key" SelectedValue='<%# Bind("Field20Validation") %>' TabIndex="117" />&nbsp;
                        <img src="../Images/help.gif" style="border: 0px; cursor: pointer;" alt="Modify Help Text" 
                        onclick="javascript:ToggleDiv('divField20Help');" />&nbsp;
                        <asp:Button ID="Field20ClearButton" runat="server" Text=" C " ToolTip="Clear this Filed" CommandArgument="20" CommandName="ClearField" OnCommand="FieldCommand" OnClientClick="if (!confirm('Are you sure you wish to clear this field?')) return false;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                        <asp:Label ID="Field20LookupLabel" runat="server" Visible="true" Text="" CssClass="Attention" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="vertical-align: top; overflow: visible; padding-bottom: 10px;">
                        <div id="divField20Options" style="display: none; float: left; margin-right: 5px;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Options (one per line), or Notes:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field20OptionsTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="40" Text='<%# Bind("Field20Options") %>' TabIndex="118" /></td>
                                </tr>
                            </table>
                        </div>
                        <div id="divField20Help" style="display: none; float: left;">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="left">Help Text:</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Field20HelpTextBox" runat="server" TextMode="MultiLine"
                                            Rows="3" Columns="25" Text='<%# Bind("Field20HelpText") %>' TabIndex="119" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table><br />
            <table border="0" cellpadding="3" cellspacing="0">
                <tr>
                    <td><asp:CheckBox ID="LockedCheckBox" runat="server" Text="Locked" 
                        TextAlign="Left" Checked='<%# Bind("Locked") %>' TabIndex="120" /></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="LockNotesTextBox" runat="server" TextMode="MultiLine"
                        Rows="3" Columns="75" Text='<%# Bind("LockNotes") %>' TabIndex="121" /></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td><asp:Button ID="InsertButton" runat="server" 
                        CausesValidation="True" CommandName="Insert" Text="Add Form" TabIndex="122" UseSubmitBehavior="false" />
                        &nbsp;&nbsp;&nbsp;<asp:Button ID="InsertCancelButton" runat="server" 
                        CausesValidation="False" CommandName="Cancel" Text="Cancel" TabIndex="123" UseSubmitBehavior="false" /></td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
            </table>
        </InsertItemTemplate>
        <ItemTemplate>
            <table cellpadding="3" cellspacing="0" border="0" width="100%">
                <tr>
                    <td colspan="5">Eval ID: <asp:Label ID="EvalIDLabel" runat="server" Text='<%# Eval("EvalID") %>' /></td>
                </tr>
                <tr>
                    <td colspan="5"><h2><asp:Literal ID="EPathLabel" runat="server" Text='<%# Eval("EPath") %>' /></h2></td>
                </tr>
                <tr>
                    <td>Required:</td>
                    <td><asp:CheckBox ID="RequiredCheckBox" runat="server" Checked='<%# Bind("Required") %>' Enabled="false" /></td>
                    <td style="width: 20px;">&nbsp;</td>
                    <td>Calculated:</td>
                    <td><asp:CheckBox ID="CalculatedCheckBox" runat="server" Checked='<%# Bind("Calculated") %>' Enabled="false" />&nbsp;
                        <asp:Label ID="CalculationLabel" runat="server" Text='<%# Bind("Calculation") %>' /></td>
                </tr>
                <tr>
                    <td class="Attention">Inactive:</td>
                    <td><asp:CheckBox ID="InactiveCheckBox" runat="server" Checked='<%# Bind("Inactive") %>' Enabled="false" /></td>
                    <td>&nbsp;</td>
                    <td>Functional Objective Finding:</td>
                    <td><asp:CheckBox ID="FunctionalObjectiveFindingCheckBox" runat="server" Checked='<%# Bind("FunctionalObjectiveFinding") %>' Enabled="false" /></td>
                </tr>
                <tr>
                    <td>Problem:</td>
                    <td><asp:CheckBox ID="ProblemCheckBox" runat="server" Checked='<%# Bind("Problem") %>' Enabled="false" /></td>
                    <td>&nbsp;</td>
                    <td>Required for Pediatrics:</td>
                    <td><asp:CheckBox ID="RequiredForPediatricsCheckBox" runat="server" Checked='<%# Bind("RequiredForPediatrics") %>' Enabled="false" /></td>
                </tr>
                <tr>
                    <td>Patient Goal:</td>
                    <td><asp:CheckBox ID="PatientGoalCheckBox" runat="server" Checked='<%# Bind("PatientGoal") %>' Enabled="false" /></td>
                    <td>&nbsp;</td>
                    <td>Subjective:</td>
                    <td><asp:CheckBox ID="SubjectiveCheckBox" runat="server" Checked='<%# Bind("Subjective") %>' Enabled="false" /></td>
                </tr>
                <tr>
                    <td>Outcome:</td>
                    <td><asp:CheckBox ID="OutcomeCheckBox" runat="server" Checked='<%# Bind("Outcome") %>' Enabled="false" /></td>
                    <td>&nbsp;</td>
                    <td>Test:</td>
                    <td><asp:CheckBox ID="TestCheckBox" runat="server" Checked='<%# Bind("Test") %>' Enabled="false" /></td>
                </tr>
            </table>
            <br />
			<asp:Table ID="FormTable" runat="server" CellPadding="3" CellSpacing="0" Width="100%" />
			<br /><br />
			<div>
				<asp:Button ID="BackButton" runat="server" CausesValidation="False" CommandName="Cancel" 
					Text="Back" UseSubmitBehavior="false" />
				<input type="button" id="PreviewButton" value="Print Form" 
                    onclick="PreviewReport();" style="float: right;" />
			</div>
        </ItemTemplate>
    </asp:FormView>
    <csla:CslaDataSource ID="EvalFormDataSource" runat="server" 
        TypeName="ITWMaintenance.Library.Evaluations.Forms.EvalForm, ITWMaintenance.Library" 
        TypeSupportsPaging="False" TypeSupportsSorting="False" TypeAssemblyName="">
    </csla:CslaDataSource> 
    <asp:ObjectDataSource ID="FieldTypeDataSource" runat="server" SelectMethod="GetEvalFormFieldTypes"
        TypeName="ITWMaintenance.Library.Evaluations.Forms.EvalFormFieldTypes" />
    <asp:ObjectDataSource ID="eClassDataSource" runat="server" SelectMethod="GetEvalFormEClassValues"
        TypeName="ITWMaintenance.Library.Evaluations.Forms.EvalFormEClassValues" />
    <asp:ObjectDataSource ID="FieldValidationDataSource" runat="server" SelectMethod="GetValidationCodes"
        TypeName="ITWMaintenance.Library.Lookup.FormValidationList" />
    <%--<asp:ObjectDataSource ID="ReportTemplatesDataSource" runat="server" SelectMethod="GetReportTemplates"
        TypeName="ITWMaintenance.Library.Evaluations.Forms.EvalFormReportTemplates" />--%>
</asp:Content>