<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Form_Preview.aspx.vb" Inherits="IntVnts_Form_Preview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ITW Maintenance - Interventions - Form Preview</title>
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
</head>
<body style="background: none #FFFFFF;">
    <form id="form1" runat="server">
    <div>
        <asp:FormView ID="FormView1" runat="server" DataKeyNames="intVntID" DataSourceID="IntVntFormDataSource" Width="100%">
            <ItemTemplate>
                <table cellpadding="3" cellspacing="0" border="0">
					<tr>
						<td colspan="5"><input type="button" id="CloseButton" value="Close" onclick="javascript:window.close();" />&nbsp;&nbsp;
							<input type="button" id="PrintButton" value="Print Form" onclick="javascript:window.print();" /></td>
					</tr>
					<tr>
						<td colspan="5"><h2><asp:Label ID="ParentDisciplineLabel" Text='' runat="server" /></h2></td>
					</tr>
					<tr>
						<td colspan="5">IntVntID: <asp:Label ID="IntVntIDLabel" runat="server" Text='<%# Eval("IntVntID") %>' /></td>
					</tr>
                    <tr>
                        <td colspan="5"><h2><asp:Literal ID="iPathLabel" runat="server" Text='<%# Eval("iPath") %>' /></h2></td>
                    </tr>
                    <%--<tr>
                        <td colspan="5">Report Description: <asp:Label ID="Label1" runat="server" Text='<%# Bind("ReportDescription") %>' /></td>
                    </tr>--%>
                    <tr>
                        <td>Custom:</td>
                        <td><asp:CheckBox ID="CustomCheckBox" runat="server" Checked='<%# Bind("Custom") %>' Enabled="false" />&nbsp;
                            <%--<asp:Label ID="CalculationLabel" runat="server" Text='<%# Bind("Calculation") %>' /></td>--%>
                    </tr>
                    <tr>
                        <td>Education:</td>
                        <td><asp:CheckBox ID="EducationCheckBox" runat="server" Checked='<%# Bind("Education") %>' Enabled="false" /></td>
                        <td style="width: 20px;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="Attention">Inactive:</td>
                        <td><asp:CheckBox ID="InactiveCheckBox" runat="server" Checked='<%# Bind("Inactive") %>' Enabled="false" /></td>
                        <td>&nbsp;</td>
                        <%--<td>Functional Objective Finding:</td>
                        <td><asp:CheckBox ID="FunctionalObjectiveFindingCheckBox" runat="server" Checked='<%# Bind("FunctionalObjectiveFinding") %>' Enabled="false" /></td>--%>
                    </tr>
                    <%--<tr>
                        <td>Problem:</td>
                        <td><asp:CheckBox ID="ProblemCheckBox" runat="server" Checked='<%# Bind("Problem") %>' Enabled="false" /></td>
                        <td>&nbsp;</td>
                        <td>Required for Pediatrics:</td>
                        <td><asp:CheckBox ID="RequiredForPediatricsCheckBox" runat="server" Checked='<%# Bind("RequiredForPediatrics") %>' Enabled="false" /></td>
                    </tr>--%>
                    <%--<tr>
                        <td>Patient Goal:</td>
                        <td><asp:CheckBox ID="PatientGoalCheckBox" runat="server" Checked='<%# Bind("PatientGoal") %>' Enabled="false" /></td>
                        <td>&nbsp;</td>
                        <td>Subjective:</td>
                        <td><asp:CheckBox ID="SubjectiveCheckBox" runat="server" Checked='<%# Bind("Subjective") %>' Enabled="false" /></td>
                    </tr>--%>
                    <tr>
                        <td>Billing:</td>
                        <td><asp:CheckBox ID="BillingCheckBox" runat="server" Checked='<%# Bind("Billing") %>' Enabled="false" /></td>
                        <td>&nbsp;</td>
                        <%--<td>Test:</td>
                        <td><asp:CheckBox ID="TestCheckBox" runat="server" Checked='<%# Bind("Test") %>' Enabled="false" /></td>--%>
                    </tr>
                </table>
                <br />
				<asp:Table ID="FormTable" runat="server" CellPadding="3" CellSpacing="0" Width="100%" />
            </ItemTemplate>
        </asp:FormView>
        <asp:ObjectDataSource ID="IntVntFormDataSource" runat="server" SelectMethod="GetIntVntFormInfo"
            TypeName="ITWMaintenance.Library.Interventions.Forms.ReadOnlyIntVntForm">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="0" Name="intVntID" QueryStringField="intVntID"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>

