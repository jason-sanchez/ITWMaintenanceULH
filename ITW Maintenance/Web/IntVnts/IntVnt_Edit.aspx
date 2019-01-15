<%@ Page Title="ITW Maintenance - IntVnts - IntVnt Edit" Language="VB" MasterPageFile="~/IntVnts/IntVntSubMaster.master" AutoEventWireup="false" CodeFile="IntVnt_Edit.aspx.vb" Inherits="IntVnts_IntVnt_Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <h3><asp:Literal ID="lblHeader" runat="server" /></h3>
    <asp:panel id="pnlError" runat="server" Visible="false" cssclass="divError"><asp:Label ID="lblError" runat="server" /></asp:panel>
    <asp:FormView ID="frmEdit" runat="server" DataKeyNames="intVntID" DataSourceID="IntVntDataSource" DefaultMode="Edit">
        <EditItemTemplate>
            <table border="0" cellpadding="5" cellspacing="0">
                <tr>
                    <td>Intervention ID:</td>
                    <td><asp:Label ID="intVntIDLabel" runat="server" Text='<%# Eval("intVntID") %>' /></td>
                </tr>
                <tr>
                    <td>Parent Folder:</td>
                    <td><asp:Label ID="ParentFolderLabel" runat="server" /></td>
                </tr>
                <tr>
                    <td>Intervention Name:</td>
                    <td><asp:TextBox ID="IntVntNameTextBox" runat="server" Text='<%# Bind("IntVntName") %>' 
                        MaxLength="200" Width="300" />&nbsp;<asp:RequiredFieldValidator runat="server" 
                        id="RequiredFieldValidator1" ControlToValidate="IntVntNameTextBox" Text="(required)" /></td>
                </tr>
                <tr>
                    <td>Display Order:</td>
                    <td><asp:TextBox ID="DisplayOrderTextBox" runat="server" Text='<%# Bind("DisplayOrder") %>' 
                        Width="50" />&nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ErrorMessage="(invalid)" ControlToValidate="DisplayOrderTextBox" Operator="DataTypeCheck" Type="Integer">
                        </asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="Attention">Inactive:</td>
                    <td><asp:CheckBox ID="InactiveCheckBox" runat="server" Checked='<%# Bind("Inactive") %>' />
                    </td>
                </tr>
                <tr>
                    <td>One-Time:</td>
                    <td><asp:CheckBox ID="OneTimeCheckBox" runat="server" Checked='<%# Bind("OneTime") %>' />
                    </td>
                </tr>
                <tr>
                    <td>Billing:</td>
                    <td><asp:CheckBox ID="BillingCheckBox" runat="server" Checked='<%# Bind("Billing") %>' />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2"><asp:Button ID="UpdateButton" runat="server" CausesValidation="True" 
                        CommandName="Update" Text="Save Changes" />&nbsp;&nbsp;&nbsp;<asp:Button ID="UpdateCancelButton" 
                        runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" /></td>
                </tr>
            </table>
            <br />
            <asp:Panel ID="ChildFunctionsPanel" runat="server" Visible="false">
                <table border="0" cellpadding="5" cellspacing="0" width="100%">
                    <tr>
                        <td><asp:Button ID="NewSubFormButton" runat="server" CausesValidation="false" Text="Add New Form" OnClick="NewSubFormButton_Click" />&nbsp;&nbsp;
                            <asp:Button ID="NewSubFolderButton" runat="server" CausesValidation="false" Text="Add New Folder" OnClick="NewSubFolderButton_Click" /></td>
                        <td align="right"><asp:Button ID="PasteFormButton" runat="server" CausesValidation="false" Text="Paste Copied Form" OnClick="PasteFormButton_Click" Visible="false" /></td>
                    </tr>
                </table>
            <br /><br />
            </asp:Panel>
            <asp:Panel ID="RebuildPanel" runat="server" Visible="false">
                <table border="0" cellpadding="5" cellspacing="0">
                <tr>
                    <td colspan="2"><strong>Rebuild for this intervention...</strong></td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBoxList ID="RebuildCheckBoxList" runat="server">
                            <asp:ListItem Text=" Short Names" Value="ShortNames" />
                            <asp:ListItem Text=" iPaths" Value="iPaths" />
                            <%--<asp:ListItem Text=" iGroups" Value="iGroups" />--%>
                        </asp:CheckBoxList>
                    </td>
                    <td align="center"><asp:Label ID="RebuildResultLabel" runat="server" Text="" CssClass="Attention" /></td>
                </tr>
                <tr>
                    <td colspan="2"><asp:Button ID="RebuildButton" runat="server" CausesValidation="false" Text="Rebuild" OnClick="RebuildButton_Click" /></td>
                </tr>
                </table>
            </asp:Panel>
        </EditItemTemplate>
        <InsertItemTemplate>
            <table border="0" cellpadding="5" cellspacing="0">
                <tr>
                    <td>Parent Folder:</td>
                    <td><asp:Label ID="ParentFolderLabel" runat="server" /></td>
                </tr>
                <tr>
                    <td>Intervention Name:</td>
                    <td><asp:TextBox ID="IntVntNameTextBox" runat="server" Text='<%# Bind("IntVntName") %>' 
                        MaxLength="200" Width="300" />&nbsp;<asp:RequiredFieldValidator runat="server" 
                        id="RequiredFieldValidator1" ControlToValidate="IntVntNameTextBox" Text="(required)" /></td>
                </tr>
                <tr>
                    <td>Display Order:</td>
                    <td><asp:TextBox ID="DisplayOrderTextBox" runat="server" Text='<%# Bind("DisplayOrder") %>' 
                        Width="50" />&nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ErrorMessage="(invalid)" ControlToValidate="DisplayOrderTextBox" Operator="DataTypeCheck" Type="Integer">
                        </asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="Attention">Inactive:</td>
                    <td><asp:CheckBox ID="InactiveCheckBox" runat="server" Checked='<%# Bind("Inactive") %>' />
                    </td>
                </tr>
                <tr>
                    <td>One-Time:</td>
                    <td><asp:CheckBox ID="OneTimeCheckBox" runat="server" Checked='<%# Bind("OneTime") %>' />
                    </td>
                </tr>
                <tr>
                    <td>Billing:</td>
                    <td><asp:CheckBox ID="BillingCheckBox" runat="server" Checked='<%# Bind("Billing") %>' />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2"><asp:Button ID="InsertButton" runat="server" 
                        CausesValidation="True" CommandName="Insert" Text="Add Intervention" />
                        &nbsp;&nbsp;&nbsp;<asp:Button ID="InsertCancelButton" runat="server" 
                        CausesValidation="False" CommandName="Cancel" Text="Cancel" /></td>
                </tr>
            </table>
        </InsertItemTemplate>
		<ItemTemplate>
			<table border="0" cellpadding="5" cellspacing="0">
                <tr>
                    <td>Intervention ID:</td>
                    <td><asp:Label ID="intVntIDLabel" runat="server" Text='<%# Eval("intVntID") %>' /></td>
                </tr>
                <tr>
                    <td>Parent Folder:</td>
                    <td><asp:Label ID="ParentFolderLabel" runat="server" /></td>
                </tr>
                <tr>
                    <td>Intervention Name:</td>
                    <td><asp:TextBox ID="IntVntNameTextBox" runat="server" Text='<%# Bind("IntVntName") %>' 
                        MaxLength="200" Width="300" Enabled="false" /></td>
                </tr>
                <tr>
                    <td>Display Order:</td>
                    <td><asp:TextBox ID="DisplayOrderTextBox" runat="server" Text='<%# Bind("DisplayOrder") %>' 
                        Width="50" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td class="Attention">Inactive:</td>
                    <td><asp:CheckBox ID="InactiveCheckBox" runat="server" Checked='<%# Bind("Inactive") %>' Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td>One-Time:</td>
                    <td><asp:CheckBox ID="OneTimeCheckBox" runat="server" Checked='<%# Bind("OneTime") %>' Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td>Billing:</td>
                    <td><asp:CheckBox ID="BillingCheckBox" runat="server" Checked='<%# Bind("Billing") %>' Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2"><asp:Button ID="BackButton" runat="server" CausesValidation="False" 
						CommandName="Cancel" Text="Back" /></td>
                </tr>
            </table>
		</ItemTemplate>
    </asp:FormView>
    <csla:CslaDataSource ID="IntVntDataSource" runat="server" 
        TypeName="ITWMaintenance.Library.Intervention, ITWMaintenance.Library" 
        TypeSupportsPaging="False" TypeSupportsSorting="False">
    </csla:CslaDataSource>
</asp:Content>

