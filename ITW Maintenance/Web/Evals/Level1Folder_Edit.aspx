<%@ Page Title="ITW Maintenance - Evals - Level 1 Folder Edit" Language="VB" MasterPageFile="~/Evals/EvalMainMaster.master" AutoEventWireup="false" CodeFile="Level1Folder_Edit.aspx.vb" Inherits="Evals_Level1Folder_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <h2><asp:Literal ID="lblHeader" runat="server" /></h2>
    <asp:panel id="pnlError" runat="server" Visible="false" cssclass="divError"><asp:Label ID="lblError" runat="server" /></asp:panel>
    <asp:FormView ID="frmEdit" runat="server" DataKeyNames="EvalID" DataSourceID="EvalLevel1FolderDataSource" DefaultMode="Edit">
        <EditItemTemplate>
            <table border="0" cellpadding="5" cellspacing="0" class="form">
                <tr>
                    <td>Eval ID:</td>
                    <td><asp:Label ID="EvalIDLabel" runat="server" Text='<%# Bind("EvalID") %>' /></td>
                </tr>
                <tr>
                    <td>Folder Name:</td>
                    <td><asp:TextBox ID="FolderNameTextBox" runat="server" Text='<%# Bind("FolderName") %>' 
                        MaxLength="200" Width="300" />&nbsp;<asp:RequiredFieldValidator runat="server" 
                        id="RequiredFieldValidator1" ControlToValidate="FolderNameTextBox" Text="(required)" /></td>
                </tr>
                <tr>
                    <td>Discipline:</td>
                    <td><asp:DropDownList ID="DisciplineDropDown" runat="server" 
                        DataSourceID="DisciplineDataSource" DataTextField="Value" DataValueField="Key" 
                        SelectedValue='<%# Bind("Discipline") %>' />
                        <asp:ObjectDataSource ID="DisciplineDataSource" runat="server" 
                            SelectMethod="GetAllDisciplines" 
                            TypeName="ITWMaintenance.Library.Lookup.DisciplineList">
                        </asp:ObjectDataSource>
                    </td>
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
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2"><asp:Button ID="UpdateButton" runat="server" CausesValidation="True" 
                        CommandName="Update" Text="Update" />&nbsp;&nbsp;&nbsp;<asp:Button ID="UpdateCancelButton" 
                        runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" /></td>
                </tr>
            </table>
        </EditItemTemplate>
        <InsertItemTemplate>
            <table border="0" cellpadding="5" cellspacing="0" class="form">
                <tr>
                    <td>Folder Name:</td>
                    <td><asp:TextBox ID="FolderNameTextBox" runat="server" Text='<%# Bind("FolderName") %>' 
                        MaxLength="200" Width="300" />&nbsp;<asp:RequiredFieldValidator runat="server" 
                        id="RequiredFieldValidator1" ControlToValidate="FolderNameTextBox" Text="(required)" />
                    </td>
                </tr>
                <tr>
                    <td>Discipline:</td>
                    <td><asp:DropDownList ID="DisciplineDropDown" runat="server" 
                        DataSourceID="DisciplineDataSource" DataTextField="Value" DataValueField="Key" 
                        SelectedValue='<%# Bind("Discipline") %>' />
                        <asp:ObjectDataSource ID="DisciplineDataSource" runat="server" 
                            SelectMethod="GetAllDisciplines" 
                            TypeName="ITWMaintenance.Library.Lookup.DisciplineList">
                        </asp:ObjectDataSource>
                    </td>
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
            <table border="0" cellpadding="5" cellspacing="0" class="form">
                <tr>
                    <td>Eval ID:</td>
                    <td><asp:Label ID="EvalIDLabel" runat="server" Text='<%# Bind("EvalID") %>' /></td>
                </tr>
                <tr>
                    <td>Folder Name:</td>
                    <td>
						<asp:TextBox ID="FolderNameTextBox" runat="server" Text='<%# Bind("FolderName") %>' 
                        MaxLength="200" Width="300" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td>Discipline:</td>
                    <td>
						<asp:DropDownList ID="DisciplineDropDown" runat="server" 
                        DataSourceID="DisciplineDataSource" DataTextField="Value" DataValueField="Key" 
                        SelectedValue='<%# Bind("Discipline") %>' Enabled="false" />
                        <asp:ObjectDataSource ID="DisciplineDataSource" runat="server" 
                            SelectMethod="GetAllDisciplines" 
                            TypeName="ITWMaintenance.Library.Lookup.DisciplineList">
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td>Display Order:</td>
                    <td>
						<asp:TextBox ID="DisplayOrderTextBox" runat="server" Text='<%# Bind("DisplayOrder") %>' 
                        Width="50" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td class="Attention">Inactive:</td>
                    <td>
						<asp:CheckBox ID="InactiveCheckBox" runat="server" Checked='<%# Bind("Inactive") %>' Enabled="false" />
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
    <csla:CslaDataSource ID="EvalLevel1FolderDataSource" runat="server" 
        TypeName="ITWMaintenance.Library.Evaluations.EvalLevel1Folder, ITWMaintenance.Library" 
        TypeSupportsPaging="False" TypeSupportsSorting="False">
    </csla:CslaDataSource> 
</asp:Content>

