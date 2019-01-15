﻿<%@ Page Title="ITW Maintenance - Interventions - Folder Edit" Language="VB" MasterPageFile="~/IntVnts/IntVntSubMaster.master" AutoEventWireup="false" CodeFile="Folder_Edit.aspx.vb" Inherits="IntVnts_Folder_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <h3><asp:Literal ID="lblHeader" runat="server" /></h3>
    <asp:panel id="pnlError" runat="server" Visible="false" cssclass="divError"><asp:Label ID="lblError" runat="server" /></asp:panel>
    <asp:FormView ID="frmEdit" runat="server" DataSourceID="IntVntFolderDataSource" DefaultMode="Edit">
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
                    <td>Full Path:</td>
                    <td><asp:Label ID="iPathLabel" runat="server" Text='<%# Eval("iPath") %>' /></td>
                </tr>
                <tr>
                    <td>Folder Name:</td>
                    <td><asp:TextBox ID="FolderNameTextBox" runat="server" Text='<%# Bind("FolderName") %>' 
                        MaxLength="200" Width="400" />&nbsp;<asp:RequiredFieldValidator runat="server" 
                        id="RequiredFieldValidator1" ControlToValidate="FolderNameTextBox" Text="(required)" /></td>
                </tr>
                <tr>
                    <td>Short Name:</td>
                    <td><asp:TextBox ID="ShortNameTextBox" runat="server" Text='<%# Bind("ShortName") %>' 
                        MaxLength="200" Width="400" /></td>
                </tr>
                <%--<tr>
                    <td>iGroup Order:</td>
                    <td><asp:TextBox ID="iGroupOrderTextBox" runat="server" Text='<%# Bind("iGroupOrder") %>' 
                        Width="50" />&nbsp;<asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="iGroupOrderTextBox"
                            ErrorMessage="(invalid)" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator></td>
                </tr>
                <tr>
                    <td>iGroup:</td>
                    <td><asp:TextBox ID="iGroupTextBox" runat="server" Text='<%# Bind("iGroup") %>'
                        width="400" MaxLength="200" /></td>
                </tr>--%>
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
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2"><asp:Button ID="UpdateButton" runat="server" CausesValidation="True" 
                        CommandName="Update" Text="Save Changes" />&nbsp;&nbsp;&nbsp;<asp:Button ID="UpdateCancelButton" 
                        runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" /></td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div style="width: 50%; float: left;">
                            <asp:Button ID="NewSubFormButton" runat="server" CausesValidation="false" Text="Add New Form" OnClick="NewSubFormButton_Click" />&nbsp;&nbsp;
                            <asp:Button ID="NewSubFolderButton" runat="server" CausesValidation="false" Text="Add New Folder" OnClick="NewSubFolderButton_Click" />
                        </div>
                        <div style="width: 50%; float: left; text-align: right;">
                            <asp:Button ID="PasteFormButton" runat="server" CausesValidation="false" Text="Paste Copied Form" OnClick="PasteFormButton_Click" Visible="false" />
                        </div>
                    </td>
                </tr>
            </table>
        </EditItemTemplate>
        <InsertItemTemplate>
            <table border="0" cellpadding="5" cellspacing="0">
                <tr>
                    <td>Parent Folder:</td>
                    <td><asp:Label ID="ParentFolderLabel" runat="server" /></td>
                </tr>
                <tr>
                    <td>Folder Name:</td>
                    <td><asp:TextBox ID="FolderNameTextBox" runat="server" Text='<%# Bind("FolderName") %>' 
                        MaxLength="200" Width="300" />&nbsp;<asp:RequiredFieldValidator runat="server" 
                        id="RequiredFieldValidator1" ControlToValidate="FolderNameTextBox" Text="(required)" /></td>
                </tr>
                <!--<tr>
                    <td>Short Name:</td>
                    <td><asp:TextBox ID="ShortNameTextBox" runat="server" Text='<%# Bind("ShortName") %>' 
                        MaxLength="200" Width="300" /></td>
                </tr>-->
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
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2"><asp:Button ID="InsertButton" runat="server" 
                        CausesValidation="True" CommandName="Insert" Text="Add Folder" />
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
                    <td>Full Path:</td>
                    <td><asp:Label ID="iPathLabel" runat="server" Text='<%# Eval("iPath") %>' /></td>
                </tr>
                <tr>
                    <td>Folder Name:</td>
                    <td><asp:TextBox ID="FolderNameTextBox" runat="server" Text='<%# Bind("FolderName") %>' 
                        MaxLength="200" Width="400" Enabled="false" /></td>
                </tr>
                <tr>
                    <td>Short Name:</td>
                    <td><asp:TextBox ID="ShortNameTextBox" runat="server" Text='<%# Bind("ShortName") %>' 
                        MaxLength="200" Width="400" Enabled="false" /></td>
                </tr>
                <%--<tr>
                    <td>iGroup Order:</td>
                    <td><asp:TextBox ID="iGroupOrderTextBox" runat="server" Text='<%# Bind("iGroupOrder") %>' 
                        Width="50" Enabled="false" /></td>
                </tr>
                <tr>
                    <td>iGroup:</td>
                    <td><asp:TextBox ID="iGroupTextBox" runat="server" Text='<%# Bind("iGroup") %>'
                        width="400" MaxLength="200" Enabled="false" /></td>
                </tr>--%>
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
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2"><asp:Button ID="BackButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Back" /></td>
                </tr>
            </table>
		</ItemTemplate>
    </asp:FormView>
    <csla:CslaDataSource ID="IntVntFolderDataSource" runat="server" 
        TypeName="ITWMaintenance.Library.Interventions.Folders.IntVntFolder, ITWMaintenance.Library" 
        TypeSupportsPaging="False" TypeSupportsSorting="False" TypeAssemblyName="">
    </csla:CslaDataSource> 
</asp:Content>

