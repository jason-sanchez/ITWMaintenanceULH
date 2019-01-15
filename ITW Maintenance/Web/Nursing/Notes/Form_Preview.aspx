<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Form_Preview.aspx.vb" Inherits="Nursing_Notes_Form_Preview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ITW Maintenance - Nursing Notes - Form Preview</title>
</head>
<body style="background: none #FFFFFF;">
    <form id="form1" runat="server">
    <div>
        <asp:FormView ID="FormView1" runat="server" DataKeyNames="FormID" DataSourceID="NursingNoteFormDataSource" Width="100%">
            <ItemTemplate>
                <table cellpadding="3" cellspacing="0" border="0">
                    <tr>
                        <td colspan="5"><input type="button" id="CloseButton" value="Close" onclick="javascript:window.close();" />&nbsp;&nbsp;
                            <input type="button" id="PrintButton" value="Print Form" onclick="javascript:window.print();" /></td>
                    </tr>
                    <tr>
                        <td colspan="5">Form ID: <asp:Label ID="FormIDLabel" runat="server" Text='<%# Eval("FormID") %>' /></td>
                    </tr>
                    <tr>
                        <td colspan="5"><h2><asp:Literal ID="FormNameLabel" runat="server" Text='<%# Eval("FormName") %>' /></h2></td>
                    </tr>
                    <tr>
                        <td style="width: 25%;">Required:</td>
                        <td style="width: 12%;"><asp:CheckBox ID="RequiredCheckBox" runat="server" Checked='<%# Bind("Required") %>' 
                            Enabled="false" /></td>
                        <td style="width: 5%;">&nbsp;</td>
                        <td style="width: 30%;" class="Attention">Inactive:</td>
                        <td style="width: 12%;"><asp:CheckBox ID="InactiveCheckBox" runat="server" Checked='<%# Bind("Inactive") %>' 
                            Enabled="false" /></td>
                        <td style="width: 16%;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Education:</td>
                        <td><asp:CheckBox ID="EducationCheckBox" runat="server" Checked='<%# Bind("Education") %>' 
                            Enabled="false" /></td>
                        <td>&nbsp;</td>
                        <td>PCar Only:</td>
                        <td><asp:CheckBox ID="PCarOnlyCheckBox" runat="server" Checked='<%# Bind("PCarOnly") %>' 
                            Enabled="false" /></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Nursing Note Only:</td>
                        <td><asp:CheckBox ID="NursingNoteOnlyCheckBox" runat="server" Checked='<%# Bind("NursingNoteOnly") %>' 
                            Enabled="false" /></td>
                        <td>&nbsp;</td>
                        <td>Include Graphics:</td>
                        <td><asp:CheckBox ID="IncludeGraphicsCheckBox" runat="server" Checked='<%# Bind("IncludeGraphics") %>' 
                            Enabled="false" /></td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                <br />
                <table cellpadding="3" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td style="vertical-align: top;">
                            <strong>1. <asp:Label ID="Field01LabelLabel" runat="server" Text='<%# Bind("Field01Label") %>' /></strong>
                            &nbsp;-&nbsp;<asp:Label ID="Field01TypeLabel" runat="server" Text='<%# GetFieldTypeValue(Eval("Field01Type")) %>' />
                            <asp:Table ID="Field01OptionsTable" runat="server" Width="100%" CellPadding="3" CellSpacing="0" Visible="false">
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="top"><asp:Label ID="Field01OptionsLabel" runat="server" Text='<%# Bind("Field01Options") %>' /></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <strong>2. <asp:Label ID="Field02LabelLabel" runat="server" Text='<%# Bind("Field02Label") %>' /></strong>
                            &nbsp;-&nbsp;<asp:Label ID="Field02TypeLabel" runat="server" Text='<%# GetFieldTypeValue(Eval("Field02Type")) %>' />
                            <asp:Table ID="Field02OptionsTable" runat="server" Width="100%" CellPadding="3" CellSpacing="0" Visible="false">
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="top"><asp:Label ID="Field02OptionsLabel" runat="server" Text='<%# Bind("Field02Options") %>' /></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <strong>3. <asp:Label ID="Field03LabelLabel" runat="server" Text='<%# Bind("Field03Label") %>' /></strong>
                            &nbsp;-&nbsp;<asp:Label ID="Field03TypeLabel" runat="server" Text='<%# GetFieldTypeValue(Eval("Field03Type")) %>' />
                            <asp:Table ID="Field03OptionsTable" runat="server" Width="100%" CellPadding="3" CellSpacing="0" Visible="false">
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="top"><asp:Label ID="Field03OptionsLabel" runat="server" Text='<%# Bind("Field03Options") %>' /></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <strong>4. <asp:Label ID="Field04LabelLabel" runat="server" Text='<%# Bind("Field04Label") %>' /></strong>
                            &nbsp;-&nbsp;<asp:Label ID="Field04TypeLabel" runat="server" Text='<%# GetFieldTypeValue(Eval("Field04Type")) %>' />
                            <asp:Table ID="Field04OptionsTable" runat="server" Width="100%" CellPadding="3" CellSpacing="0" Visible="false">
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="top"><asp:Label ID="Field04OptionsLabel" runat="server" Text='<%# Bind("Field04Options") %>' /></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <strong>5. <asp:Label ID="Field05LabelLabel" runat="server" Text='<%# Bind("Field05Label") %>' /></strong>
                            &nbsp;-&nbsp;<asp:Label ID="Field05TypeLabel" runat="server" Text='<%# GetFieldTypeValue(Eval("Field05Type")) %>' />
                            <asp:Table ID="Field05OptionsTable" runat="server" Width="100%" CellPadding="3" CellSpacing="0" Visible="false">
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="top"><asp:Label ID="Field05OptionsLabel" runat="server" Text='<%# Bind("Field05Options") %>' /></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <strong>6. <asp:Label ID="Field06LabelLabel" runat="server" Text='<%# Bind("Field06Label") %>' /></strong>
                            &nbsp;-&nbsp;<asp:Label ID="Field06TypeLabel" runat="server" Text='<%# GetFieldTypeValue(Eval("Field06Type")) %>' />
                            <asp:Table ID="Field06OptionsTable" runat="server" Width="100%" CellPadding="3" CellSpacing="0" Visible="false">
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="top"><asp:Label ID="Field06OptionsLabel" runat="server" Text='<%# Bind("Field06Options") %>' /></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <strong>7. <asp:Label ID="Field07LabelLabel" runat="server" Text='<%# Bind("Field07Label") %>' /></strong>
                            &nbsp;-&nbsp;<asp:Label ID="Field07TypeLabel" runat="server" Text='<%# GetFieldTypeValue(Eval("Field07Type")) %>' />
                            <asp:Table ID="Field07OptionsTable" runat="server" Width="100%" CellPadding="3" CellSpacing="0" Visible="false">
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="top"><asp:Label ID="Field07OptionsLabel" runat="server" Text='<%# Bind("Field07Options") %>' /></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <strong>8. <asp:Label ID="Field08LabelLabel" runat="server" Text='<%# Bind("Field08Label") %>' /></strong>
                            &nbsp;-&nbsp;<asp:Label ID="Field08TypeLabel" runat="server" Text='<%# GetFieldTypeValue(Eval("Field08Type")) %>' />
                            <asp:Table ID="Field08OptionsTable" runat="server" Width="100%" CellPadding="3" CellSpacing="0" Visible="false">
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="top"><asp:Label ID="Field08OptionsLabel" runat="server" Text='<%# Bind("Field08Options") %>' /></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <strong>9. <asp:Label ID="Field09LabelLabel" runat="server" Text='<%# Bind("Field09Label") %>' /></strong>
                            &nbsp;-&nbsp;<asp:Label ID="Field09TypeLabel" runat="server" Text='<%# GetFieldTypeValue(Eval("Field09Type")) %>' />
                            <asp:Table ID="Field09OptionsTable" runat="server" Width="100%" CellPadding="3" CellSpacing="0" Visible="false">
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="top"><asp:Label ID="Field09OptionsLabel" runat="server" Text='<%# Bind("Field09Options") %>' /></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <strong>10. <asp:Label ID="Field10LabelLabel" runat="server" Text='<%# Bind("Field10Label") %>' /></strong>
                            &nbsp;-&nbsp;<asp:Label ID="Field10TypeLabel" runat="server" Text='<%# GetFieldTypeValue(Eval("Field10Type")) %>' />
                            <asp:Table ID="Field10OptionsTable" runat="server" Width="100%" CellPadding="3" CellSpacing="0" Visible="false">
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="top"><asp:Label ID="Field10OptionsLabel" runat="server" Text='<%# Bind("Field10Options") %>' /></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <strong>11. <asp:Label ID="Field11LabelLabel" runat="server" Text='<%# Bind("Field11Label") %>' /></strong>
                            &nbsp;-&nbsp;<asp:Label ID="Field11TypeLabel" runat="server" Text='<%# GetFieldTypeValue(Eval("Field11Type")) %>' />
                            <asp:Table ID="Field11OptionsTable" runat="server" Width="100%" CellPadding="3" CellSpacing="0" Visible="false">
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="top"><asp:Label ID="Field11OptionsLabel" runat="server" Text='<%# Bind("Field11Options") %>' /></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <strong>12. <asp:Label ID="Field12LabelLabel" runat="server" Text='<%# Bind("Field12Label") %>' /></strong>
                            &nbsp;-&nbsp;<asp:Label ID="Field12TypeLabel" runat="server" Text='<%# GetFieldTypeValue(Eval("Field12Type")) %>' />
                            <asp:Table ID="Field12OptionsTable" runat="server" Width="100%" CellPadding="3" CellSpacing="0" Visible="false">
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="top"><asp:Label ID="Field12OptionsLabel" runat="server" Text='<%# Bind("Field12Options") %>' /></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <strong>13. <asp:Label ID="Field13LabelLabel" runat="server" Text='<%# Bind("Field13Label") %>' /></strong>
                            &nbsp;-&nbsp;<asp:Label ID="Field13TypeLabel" runat="server" Text='<%# GetFieldTypeValue(Eval("Field13Type")) %>' />
                            <asp:Table ID="Field13OptionsTable" runat="server" Width="100%" CellPadding="3" CellSpacing="0" Visible="false">
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="top"><asp:Label ID="Field13OptionsLabel" runat="server" Text='<%# Bind("Field13Options") %>' /></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <strong>14. <asp:Label ID="Field14LabelLabel" runat="server" Text='<%# Bind("Field14Label") %>' /></strong>
                            &nbsp;-&nbsp;<asp:Label ID="Field14TypeLabel" runat="server" Text='<%# GetFieldTypeValue(Eval("Field14Type")) %>' />
                            <asp:Table ID="Field14OptionsTable" runat="server" Width="100%" CellPadding="3" CellSpacing="0" Visible="false">
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="top"><asp:Label ID="Field14OptionsLabel" runat="server" Text='<%# Bind("Field14Options") %>' /></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <strong>15. <asp:Label ID="Field15LabelLabel" runat="server" Text='<%# Bind("Field15Label") %>' /></strong>
                            &nbsp;-&nbsp;<asp:Label ID="Field15TypeLabel" runat="server" Text='<%# GetFieldTypeValue(Eval("Field15Type")) %>' />
                            <asp:Table ID="Field15OptionsTable" runat="server" Width="100%" CellPadding="3" CellSpacing="0" Visible="false">
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="top"><asp:Label ID="Field15OptionsLabel" runat="server" Text='<%# Bind("Field15Options") %>' /></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <strong>16. <asp:Label ID="Field16LabelLabel" runat="server" Text='<%# Bind("Field16Label") %>' /></strong>
                            &nbsp;-&nbsp;<asp:Label ID="Field16TypeLabel" runat="server" Text='<%# GetFieldTypeValue(Eval("Field16Type")) %>' />
                            <asp:Table ID="Field16OptionsTable" runat="server" Width="100%" CellPadding="3" CellSpacing="0" Visible="false">
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="top"><asp:Label ID="Field16OptionsLabel" runat="server" Text='<%# Bind("Field16Options") %>' /></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <strong>17. <asp:Label ID="Field17LabelLabel" runat="server" Text='<%# Bind("Field17Label") %>' /></strong>
                            &nbsp;-&nbsp;<asp:Label ID="Field17TypeLabel" runat="server" Text='<%# GetFieldTypeValue(Eval("Field17Type")) %>' />
                            <asp:Table ID="Field17OptionsTable" runat="server" Width="100%" CellPadding="3" CellSpacing="0" Visible="false">
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="top"><asp:Label ID="Field17OptionsLabel" runat="server" Text='<%# Bind("Field17Options") %>' /></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <strong>18. <asp:Label ID="Field18LabelLabel" runat="server" Text='<%# Bind("Field18Label") %>' /></strong>
                            &nbsp;-&nbsp;<asp:Label ID="Field18TypeLabel" runat="server" Text='<%# GetFieldTypeValue(Eval("Field18Type")) %>' />
                            <asp:Table ID="Field18OptionsTable" runat="server" Width="100%" CellPadding="3" CellSpacing="0" Visible="false">
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="top"><asp:Label ID="Field18OptionsLabel" runat="server" Text='<%# Bind("Field18Options") %>' /></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <strong>19. <asp:Label ID="Field19LabelLabel" runat="server" Text='<%# Bind("Field19Label") %>' /></strong>
                            &nbsp;-&nbsp;<asp:Label ID="Field19TypeLabel" runat="server" Text='<%# GetFieldTypeValue(Eval("Field19Type")) %>' />
                            <asp:Table ID="Field19OptionsTable" runat="server" Width="100%" CellPadding="3" CellSpacing="0" Visible="false">
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="top"><asp:Label ID="Field19OptionsLabel" runat="server" Text='<%# Bind("Field19Options") %>' /></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <strong>20. <asp:Label ID="Field20LabelLabel" runat="server" Text='<%# Bind("Field20Label") %>' /></strong>
                            &nbsp;-&nbsp;<asp:Label ID="Field20TypeLabel" runat="server" Text='<%# GetFieldTypeValue(Eval("Field20Type")) %>' />
                            <asp:Table ID="Field20OptionsTable" runat="server" Width="100%" CellPadding="3" CellSpacing="0" Visible="false">
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="top"><asp:Label ID="Field20OptionsLabel" runat="server" Text='<%# Bind("Field20Options") %>' /></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="5"><input type="button" id="Button1" value="Close" onclick="javascript:window.close();" />&nbsp;&nbsp;
                            <input type="button" id="Button2" value="Print Form" onclick="javascript:window.print();" /></td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:FormView>
        <asp:ObjectDataSource ID="NursingNoteFormDataSource" runat="server" SelectMethod="GetFormInfo"
            TypeName="ITWMaintenance.Library.Nursing.Notes.ReadOnlyNursingNoteForm">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="0" Name="FormID" QueryStringField="FormID"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
