<%@ Page Title="ITW Maintenance - Users - User Edit" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="User_Edit.aspx.vb" Inherits="Users_User_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <h3><asp:Literal ID="HeaderLabel" runat="server" /></h3>
    <asp:panel id="ErrorPanel" runat="server" Visible="false" cssclass="divError"><asp:Label ID="ErrorLabel" runat="server" /></asp:panel>
    <asp:FormView ID="EditFormView" runat="server" DataSourceID="UserDataSource" 
        DefaultMode="Edit" DataKeyNames="ID" CssClass="form">
        <EditItemTemplate>
            <div style="width: 600px; float: left; padding: 5px;">
                <table width="100%" border="0" cellpadding="3" cellspacing="0">
                    <tr>
                        <td>ID:&nbsp;</td>
                        <td><asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' /></td>
                    </tr>
                    <tr>
                        <td>First Name:&nbsp;</td>
                        <td><asp:TextBox ID="FirstNameTextBox" runat="server" Text='<%# Bind("FirstName") %>' 
                                TabIndex="1" /></td>
                    </tr>
                    <tr>
                        <td>Last Name:&nbsp;</td>
                        <td><asp:TextBox ID="LastNameTextBox" runat="server" Text='<%# Bind("LastName") %>' 
                                TabIndex="2" /></td>
                    </tr>
                    <tr>
                        <td>Full Name:&nbsp;</td>
                        <td><asp:TextBox ID="FullNameTextBox" runat="server" Text='<%# Bind("FullName") %>' 
                                TabIndex="3" /></td>
                    </tr>
                    <tr>
                        <td>Initials:&nbsp;</td>
                        <td><asp:TextBox ID="InitialsTextBox" runat="server" Text='<%# Bind("Initials") %>' 
                                TabIndex="4" /></td>
                    </tr>
                    <tr>
                        <td>Phone:&nbsp;</td>
                        <td><asp:TextBox ID="PhoneTextBox" runat="server" 
                                Text='<%# Bind("Phone") %>' TabIndex="5" /></td>
                    </tr>
                    <tr>
                        <td>Pager:&nbsp;</td>
                        <td><asp:TextBox ID="PagerTextBox" runat="server" 
                                Text='<%# Bind("Pager") %>' TabIndex="6" /></td>
                    </tr>
                    <tr>
                        <td>Email:&nbsp;</td>
                        <td><asp:TextBox ID="EmailTextBox" runat="server" Width="280px" 
                                Text='<%# Bind("Email") %>' TabIndex="7" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2">Password Last Changed: <asp:Label ID="PasswordLastChangedLabel" runat="server" Text='<%# Eval("PasswordChangeDate") %>' /></td>
                    </tr>
                </table>
            </div>
            
            <div style="width: 400px; float: left; padding: 5px;">
                <table width="100%" border="0" cellpadding="3" cellspacing="0">
                    <tr>
                        <td>Inactive:&nbsp;</td>
                        <td><asp:CheckBox ID="InactiveCheckBox" runat="server" Checked='<%# Bind("Inactive") %>'
                                TabIndex="8" /></td>
                    </tr>
                    <tr>
                        <td>Administrator:&nbsp;</td>
                        <td><asp:CheckBox ID="AdministratorCheckBox" runat="server" Checked='<%# Bind("Administrator") %>'
                                TabIndex="9" /></td>
                    </tr>
                    <tr>
                        <td>UserName:&nbsp;</td>
                        <td><asp:TextBox ID="UserNameTextBox" runat="server" 
                                Text='<%# Bind("UserName") %>' TabIndex="10" /></td>
                    </tr>
                    <tr>
                        <td>Group:&nbsp;</td>
                        <td><asp:DropDownList ID="UserGroupDropDownList" runat="server" 
                                DataSourceID="UserGroupDataSource" DataTextField="Description" 
                                DataValueField="ID" SelectedValue='<%# Bind("GroupID") %>' TabIndex="11" 
                                AppendDataBoundItems="True">
                                <asp:ListItem Selected="True" Value="0">Select...</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>User Role:&nbsp;</td>
                        <td><asp:DropDownList ID="UserRoleDropDownList" runat="server" 
                                DataSourceID="UserRoleDataSource" DataTextField="value" 
                                DataValueField="key" SelectedValue='<%# Bind("UserRole") %>' TabIndex="12" 
                                AppendDataBoundItems="True">
                                <asp:ListItem Selected="True" Value="">Select...</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>Department:&nbsp;</td>
                        <td><asp:DropDownList ID="DepartmentDropDownList" runat="server" 
                                DataSourceID="DepartmentDataSource" DataTextField="value" 
                                DataValueField="key" SelectedValue='<%# Bind("Department") %>' TabIndex="13" 
                                AppendDataBoundItems="True">
                                <asp:ListItem Selected="True" Value="">Select...</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>Discipline:&nbsp;</td>
                        <td><asp:DropDownList ID="DisciplineDropDownList" runat="server" 
                                DataSourceID="DisciplineDataSource" DataTextField="value" 
                                DataValueField="key" SelectedValue='<%# Bind("Discipline") %>' TabIndex="14" 
                                AppendDataBoundItems="True">
                                <asp:ListItem Selected="True" Value="0">Select...</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>Export Security Level:&nbsp;</td>
                        <td><asp:DropDownList ID="ExportSecurityLevelDropDownList" runat="server"
                                TabIndex="15" SelectedValue='<%# Bind("ExportSecurityLevel") %>'>
                                <asp:ListItem Value="0">0</asp:ListItem>
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="20">20</asp:ListItem>
                                <asp:ListItem Value="30">30</asp:ListItem>
                                <asp:ListItem Value="40">40</asp:ListItem>
                                <asp:ListItem Value="50">50</asp:ListItem>
                                <asp:ListItem Value="60">60</asp:ListItem>
                                <asp:ListItem Value="70">70</asp:ListItem>
                                <asp:ListItem Value="80">80</asp:ListItem>
                                <asp:ListItem Value="80">90</asp:ListItem>
                                <asp:ListItem Value="100">100</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>USA Provider ID:&nbsp;</td>
                        <td><asp:TextBox ID="USAProviderIDTextBox" runat="server" 
                                Text='<%# Bind("USAProviderID") %>' TabIndex="16" /></td>
                    </tr>
                    <tr>
                        <td>Physician Number:&nbsp;</td>
                        <td><asp:TextBox ID="PhysicianNumberTextBox" runat="server" 
                                Text='<%# Bind("PhysicianNumber") %>' TabIndex="17" /></td>
                    </tr>
                    <tr>
                        <td>Physician Group:&nbsp;</td>
                        <td><asp:DropDownList ID="PhysicianGroupDropDownList" runat="server" 
                                DataSourceID="PhysicianGroupDataSource" DataTextField="value" 
                                DataValueField="key" SelectedValue='<%# Bind("PhysicianGroup") %>' TabIndex="18" 
                                AppendDataBoundItems="True">
                                <asp:ListItem Selected="True" Value="0">Select...</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>Medical Director Facility:&nbsp;</td>
                        <td><asp:DropDownList ID="MedicalDirectorFacilityDropDownList" runat="server" 
                                DataSourceID="MedicalDirectorFacilityDataSource" DataTextField="value" 
                                DataValueField="key" SelectedValue='<%# Bind("MedicalDirectorFacility") %>' TabIndex="19" 
                                AppendDataBoundItems="True">
                                <asp:ListItem Selected="True" Value="0">Select...</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                </table>
            </div>
            
            <div style="clear: both; text-align: center; margin: 15px;">
                <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" 
                    CommandName="Update" Text="Update" TabIndex="20" />&nbsp;&nbsp;
                <asp:Button ID="UpdateCancelButton" runat="server" 
                    CausesValidation="False" CommandName="Cancel" Text="Cancel" TabIndex="21" />&nbsp;&nbsp;
                <asp:Button ID="ResetPasswordButton" runat="server" 
                    Text="Reset Password" onclick="ResetPasswordButton_Click" TabIndex="22" />
            </div>
        </EditItemTemplate>
        <InsertItemTemplate>
            <div style="width: 400px; float: left; padding: 5px;">
                <table width="100%" border="0" cellpadding="3" cellspacing="0">
                    <tr>
                        <td>First Name:&nbsp;</td>
                        <td><asp:TextBox ID="FirstNameTextBox" runat="server" Text='<%# Bind("FirstName") %>' 
                                TabIndex="1" /></td>
                    </tr>
                    <tr>
                        <td>Last Name:&nbsp;</td>
                        <td><asp:TextBox ID="LastNameTextBox" runat="server" Text='<%# Bind("LastName") %>' 
                                TabIndex="2" /></td>
                    </tr>
                    <tr>
                        <td>Full Name:&nbsp;</td>
                        <td><asp:TextBox ID="FullNameTextBox" runat="server" Text='<%# Bind("FullName") %>' 
                                TabIndex="3" /></td>
                    </tr>
                    <tr>
                        <td>Initials:&nbsp;</td>
                        <td><asp:TextBox ID="InitialsTextBox" runat="server" Text='<%# Bind("Initials") %>' 
                                TabIndex="4" /></td>
                    </tr>
                    <tr>
                        <td>Phone:&nbsp;</td>
                        <td><asp:TextBox ID="PhoneTextBox" runat="server" 
                                Text='<%# Bind("Phone") %>' TabIndex="5" /></td>
                    </tr>
                    <tr>
                        <td>Pager:&nbsp;</td>
                        <td><asp:TextBox ID="PagerTextBox" runat="server" 
                                Text='<%# Bind("Pager") %>' TabIndex="6" /></td>
                    </tr>
                    <tr>
                        <td>Email:&nbsp;</td>
                        <td><asp:TextBox ID="EmailTextBox" runat="server" Width="280px" 
                                Text='<%# Bind("Email") %>' TabIndex="7" /></td>
                    </tr>
                </table>
            </div>
            
            <div style="width: 400px; float: left; padding: 5px;">
                <table width="100%" border="0" cellpadding="3" cellspacing="0">
                    <tr>
                        <td>Inactive:&nbsp;</td>
                        <td><asp:CheckBox ID="InactiveCheckBox" runat="server" Checked='<%# Bind("Inactive") %>'
                                TabIndex="8" /></td>
                    </tr>
                    <tr>
                        <td>Administrator:&nbsp;</td>
                        <td><asp:CheckBox ID="AdministratorCheckBox" runat="server" Checked='<%# Bind("Administrator") %>'
                                TabIndex="9" /></td>
                    </tr>
                    <tr>
                        <td>UserName:&nbsp;</td>
                        <td><asp:TextBox ID="UserNameTextBox" runat="server" 
                                Text='<%# Bind("UserName") %>' TabIndex="10" /></td>
                    </tr>
                    <tr>
                        <td>Group:&nbsp;</td>
                        <td><asp:DropDownList ID="UserGroupDropDownList" runat="server" 
                                DataSourceID="UserGroupDataSource" DataTextField="Description" 
                                DataValueField="ID" SelectedValue='<%# Bind("GroupID") %>' TabIndex="11" 
                                AppendDataBoundItems="True">
                                <asp:ListItem Selected="True" Value="0">Select...</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>User Role:&nbsp;</td>
                        <td><asp:DropDownList ID="UserRoleDropDownList" runat="server" 
                                DataSourceID="UserRoleDataSource" DataTextField="value" 
                                DataValueField="key" SelectedValue='<%# Bind("UserRole") %>' TabIndex="12" 
                                AppendDataBoundItems="True">
                                <asp:ListItem Selected="True" Value="">Select...</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>Department:&nbsp;</td>
                        <td><asp:DropDownList ID="DepartmentDropDownList" runat="server" 
                                DataSourceID="DepartmentDataSource" DataTextField="value" 
                                DataValueField="key" SelectedValue='<%# Bind("Department") %>' TabIndex="13" 
                                AppendDataBoundItems="True">
                                <asp:ListItem Selected="True" Value="">Select...</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>Discipline:&nbsp;</td>
                        <td><asp:DropDownList ID="DisciplineDropDownList" runat="server" 
                                DataSourceID="DisciplineDataSource" DataTextField="value" 
                                DataValueField="key" SelectedValue='<%# Bind("Discipline") %>' TabIndex="14" 
                                AppendDataBoundItems="True">
                                <asp:ListItem Selected="True" Value="0">Select...</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>Export Security Level:&nbsp;</td>
                        <td><asp:DropDownList ID="ExportSecurityLevelDropDownList" runat="server"
                                TabIndex="15" SelectedValue='<%# Bind("ExportSecurityLevel") %>'>
                                <asp:ListItem Value="0">0</asp:ListItem>
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="20">20</asp:ListItem>
                                <asp:ListItem Value="30">30</asp:ListItem>
                                <asp:ListItem Value="40">40</asp:ListItem>
                                <asp:ListItem Value="50">50</asp:ListItem>
                                <asp:ListItem Value="60">60</asp:ListItem>
                                <asp:ListItem Value="70">70</asp:ListItem>
                                <asp:ListItem Value="80">80</asp:ListItem>
                                <asp:ListItem Value="80">90</asp:ListItem>
                                <asp:ListItem Value="100">100</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>USA Provider ID:&nbsp;</td>
                        <td><asp:TextBox ID="USAProviderIDTextBox" runat="server" 
                                Text='<%# Bind("USAProviderID") %>' TabIndex="16" /></td>
                    </tr>
                    <tr>
                        <td>Physician Number:&nbsp;</td>
                        <td><asp:TextBox ID="PhysicianNumberTextBox" runat="server" 
                                Text='<%# Bind("PhysicianNumber") %>' TabIndex="17" /></td>
                    </tr>
                    <tr>
                        <td>Physician Group:&nbsp;</td>
                        <td><asp:DropDownList ID="PhysicianGroupDropDownList" runat="server" 
                                DataSourceID="PhysicianGroupDataSource" DataTextField="value" 
                                DataValueField="key" SelectedValue='<%# Bind("PhysicianGroup") %>' TabIndex="18" 
                                AppendDataBoundItems="True">
                                <asp:ListItem Selected="True" Value="0">Select...</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>Medical Director Facility:&nbsp;</td>
                        <td><asp:DropDownList ID="MedicalDirectorFacilityDropDownList" runat="server" 
                                DataSourceID="MedicalDirectorFacilityDataSource" DataTextField="value" 
                                DataValueField="key" SelectedValue='<%# Bind("MedicalDirectorFacility") %>' TabIndex="19" 
                                AppendDataBoundItems="True">
                                <asp:ListItem Selected="True" Value="0">Select...</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                </table>
            </div>
            
            <div style="clear: both; text-align: center; margin: 15px;">
                The user's password will initially be "password" and he/she will be prompted to change it upon first login.<br /><br />
                <asp:Button ID="InsertButton" runat="server" CausesValidation="True" 
                    CommandName="Insert" Text="Insert" TabIndex="20" />&nbsp;&nbsp;
                <asp:Button ID="InsertCancelButton" runat="server" 
                    CausesValidation="False" CommandName="Cancel" Text="Cancel" TabIndex="21" />
            </div>
        </InsertItemTemplate>
        <ItemTemplate>
            <div style="width: 400px; float: left; padding: 5px;">
                <table width="100%" border="0" cellpadding="3" cellspacing="0">
                    <tr>
                        <td>ID:&nbsp;</td>
                        <td><asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' /></td>
                    </tr>
                    <tr>
                        <td>First Name:&nbsp;</td>
                        <td><asp:Label id="Label1" runat="server" Text='<%# Eval("FirstName") %>' /></td>
                    </tr>
                    <tr>
                        <td>Last Name:&nbsp;</td>
                        <td><asp:Label ID="Label2" runat="server" Text='<%# Eval("LastName") %>' /></td>
                    </tr>
                    <tr>
                        <td>Full Name:&nbsp;</td>
                        <td><asp:Label ID="Label3" runat="server" Text='<%# Eval("FullName") %>' /></td>
                    </tr>
                    <tr>
                        <td>Initials:&nbsp;</td>
                        <td><asp:Label ID="Label4" runat="server" Text='<%# Eval("Initials") %>' /></td>
                    </tr>
                    <tr>
                        <td>Phone:&nbsp;</td>
                        <td><asp:Label ID="Label5" runat="server" Text='<%# Eval("Phone") %>' /></td>
                    </tr>
                    <tr>
                        <td>Pager:&nbsp;</td>
                        <td><asp:Label ID="Label6" runat="server" Text='<%# Eval("Pager") %>' /></td>
                    </tr>
                    <tr>
                        <td>Email:&nbsp;</td>
                        <td><asp:Label ID="Label7" runat="server" Text='<%# Eval("Email") %>' /></td>
                    </tr>
                </table>
            </div>
            
            <div style="width: 400px; float: left; padding: 5px;">
                <table width="100%" border="0" cellpadding="3" cellspacing="0">
                    <tr>
                        <td>Inactive:&nbsp;</td>
                        <td><asp:CheckBox ID="InactiveCheckBox" runat="server" Checked='<%# Bind("Inactive") %>'
                                Enabled="false" /></td>
                    </tr>
                    <tr>
                        <td>Administrator:&nbsp;</td>
                        <td><asp:CheckBox ID="AdministratorCheckBox" runat="server" Checked='<%# Bind("Administrator") %>'
                                Enabled="false" /></td>
                    </tr>
                    <tr>
                        <td>UserName:&nbsp;</td>
                        <td><asp:Label ID="Label8" runat="server" Text='<%# Eval("UserName") %>' /></td>
                    </tr>
                    <tr>
                        <td>Group:&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>User Role:&nbsp;</td>
                        <td><asp:Label ID="Label9" runat="server" Text='<%# Eval("UserRole") %>' /></td>
                    </tr>
                    <tr>
                        <td>Department:&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Discipline:&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Export Security Level:&nbsp;</td>
                        <td><asp:Label ID="Label10" runat="server" Text='<%# Eval("ExportSecurityLevel") %>' /></td>
                    </tr>
                    <tr>
                        <td>USA Provider ID:&nbsp;</td>
                        <td><asp:Label ID="Label11" runat="server" Text='<%# Eval("USAProviderID") %>' /></td>
                    </tr>
                    <tr>
                        <td>Physician Number:&nbsp;</td>
                        <td><asp:Label ID="Label12" runat="server" Text='<%# Eval("PhysicianNumber") %>' /></td>
                    </tr>
                    <tr>
                        <td>Physician Group:&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Medical Director Facility:&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </div>
            
            <div style="clear: both; text-align: center; margin: 15px;">
                <asp:Button ID="BackButton" runat="server" CausesValidation="False" 
                    CommandName="Cancel" Text="Cancel" TabIndex="10" />
            </div>
        </ItemTemplate>
    </asp:FormView>
    <csla:CslaDataSource ID="UserDataSource" runat="server" 
        TypeName="ITWMaintenance.Library.Users.User, ITWMaintenance.Library" 
        TypeSupportsPaging="False" TypeSupportsSorting="False" TypeAssemblyName="">
    </csla:CslaDataSource>
    <asp:ObjectDataSource ID="UserGroupDataSource" runat="server" 
        SelectMethod="GetUserGroupList" TypeName="ITWMaintenance.Library.Lookup.ReadOnlyUserGroupList">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="UserRoleDataSource" runat="server" 
        SelectMethod="GetUserRoles" 
        TypeName="ITWMaintenance.Library.Lookup.UserRoleList">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="DepartmentDataSource" runat="server" 
        SelectMethod="GetDepartments" 
        TypeName="ITWMaintenance.Library.Lookup.DepartmentList">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="IntakeFacility" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="DisciplineDataSource" runat="server" 
        SelectMethod="GetAllDisciplines" 
        TypeName="ITWMaintenance.Library.Lookup.DisciplineList">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="PhysicianGroupDataSource" runat="server" 
        SelectMethod="GetPhysicianGroups" 
        TypeName="ITWMaintenance.Library.Lookup.PhysicianGroupList">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="MedicalDirectorFacilityDataSource" runat="server" 
        SelectMethod="GetFacilities" 
        TypeName="ITWMaintenance.Library.Lookup.FacilityList">
    </asp:ObjectDataSource>
</asp:Content>