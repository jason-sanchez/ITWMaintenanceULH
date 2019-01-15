<%@ Page Title="ITW Maintenance - Facility Edit" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="TransferFacility_Edit.aspx.vb" Inherits="Facilities_TransferFacility_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <h3><asp:Literal ID="HeaderLabel" runat="server" /></h3>
    <asp:panel id="ErrorPanel" runat="server" Visible="false" cssclass="divError"><asp:Label ID="ErrorLabel" runat="server" /></asp:panel>
    <asp:FormView ID="EditFormView" runat="server" DataSourceID="FacilityDataSource" 
        DefaultMode="Edit" DataKeyNames="ID" CssClass="form">
        <EditItemTemplate>
            <div style="float: left; padding: 5px 20px 5px 5px;">
                <table width="100%" border="0" cellpadding="3" cellspacing="0">
                    <tr>
                        <td>ID:&nbsp;</td>
                        <td><asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' /></td>
                    </tr>
                    <tr>
                        <td>Name:&nbsp;</td>
                        <td><asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' 
                                TabIndex="1" Width="300px" /></td>
                    </tr>
                    <tr>
                        <td>Contact Name:&nbsp;</td>
                        <td><asp:TextBox ID="ContactNameTextBox" runat="server" 
                                Text='<%# Bind("ContactName") %>' TabIndex="2" Width="200px" /></td>
                    </tr>
                    <tr>
                        <td>Address:&nbsp;</td>
                        <td><asp:TextBox ID="AddressLine1TextBox" runat="server" 
                                Text='<%# Bind("AddressLine1") %>' TabIndex="3" Width="300px" /></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td><asp:TextBox ID="AddressLine2TextBox" runat="server" 
                                Text='<%# Bind("AddressLine2") %>' TabIndex="4" Width="300px" /></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:TextBox ID="CityTextBox" runat="server" Text='<%# Bind("City") %>' 
                                TabIndex="5" Width="156px" />, 
                            <asp:TextBox ID="StateTextBox" runat="server" Text='<%# Bind("State") %>' 
                                TabIndex="6" Width="30px" /> 
                            <asp:TextBox ID="ZipTextBox" runat="server" Text='<%# Bind("Zip") %>' 
                                TabIndex="7" Width="90px" />
                        </td>
                    </tr>
                    <tr>
                        <td>Phone:&nbsp;</td>
                        <td><asp:TextBox ID="PhoneTextBox" runat="server" Text='<%# Bind("Phone") %>' 
                                TabIndex="8" Width="150px" /></td>
                    </tr>
                    <tr>
                        <td>Fax:&nbsp;</td>
                        <td><asp:TextBox ID="FaxTextBox" runat="server" Text='<%# Bind("Fax") %>' 
                                TabIndex="9" Width="150px" /></td>
                    </tr>
                    <tr>
                        <td>Facility Type:&nbsp;</td>
                        <td><asp:TextBox ID="FacilityTypeTextBox" runat="server" 
                                Text='<%# Bind("FacilityType") %>' TabIndex="10" Width="200px" /></td>
                    </tr>
                    <tr>
                        <td>SMS ID:&nbsp;</td>
                        <td><asp:TextBox ID="SMSIDTextBox" runat="server" Text='<%# Bind("SMSID") %>' 
                                TabIndex="11" Width="100px" /></td>
                    </tr>
                </table>
            </div>
            
            <div style="float: left; padding: 5px;">
                <table width="100%" border="0" cellpadding="3" cellspacing="0">
                    <tr>
                        <td>Last Modified:&nbsp;</td>
                        <td><asp:Label ID="LastModifiedLabel" runat="server" 
                                Text='<%# Bind("LastModified") %>' /></td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Transfer Facility:&nbsp;</td>
                        <td><asp:CheckBox ID="TransferFacilityCheckBox" runat="server" Checked='<%# Bind("TransferFacility") %>'
                                TabIndex="12" /></td>
                    </tr>
                    <tr>
                        <td>Outpatient Therapy:&nbsp;</td>
                        <td><asp:CheckBox ID="OutpatientTherapyCheckBox" runat="server" Checked='<%# Bind("OutpatientTherapy") %>'
                                TabIndex="13" /></td>
                    </tr>
                    <tr>
                        <td>Acute Facility:&nbsp;</td>
                        <td><asp:CheckBox ID="AcuteFacilityCheckBox" runat="server" Checked='<%# Bind("AcuteFacility") %>'
                                TabIndex="14" /></td>
                    </tr>
                    <tr>
                        <td>Inactive:&nbsp;</td>
                        <td><asp:CheckBox ID="InactiveCheckBox" runat="server" Checked='<%# Bind("Inactive") %>'
                                TabIndex="15" /></td>
                    </tr>
                </table>
            </div>
            
            <div style="clear: both; text-align: center; margin: 15px;">
                <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" 
                    CommandName="Update" Text="Update" TabIndex="20" />&nbsp;&nbsp;
                <asp:Button ID="UpdateCancelButton" runat="server" 
                    CausesValidation="False" CommandName="Cancel" Text="Cancel" TabIndex="21" />
            </div>
        </EditItemTemplate>
        <InsertItemTemplate>
            <div style="float: left; padding: 5px 20px 5px 5px;">
                <table width="100%" border="0" cellpadding="3" cellspacing="0">
                    <tr>
                        <td>Name:&nbsp;</td>
                        <td><asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' 
                                TabIndex="1" Width="300px" /></td>
                    </tr>
                    <tr>
                        <td>Contact Name:&nbsp;</td>
                        <td><asp:TextBox ID="ContactNameTextBox" runat="server" 
                                Text='<%# Bind("ContactName") %>' TabIndex="2" Width="200px" /></td>
                    </tr>
                    <tr>
                        <td>Address:&nbsp;</td>
                        <td><asp:TextBox ID="AddressLine1TextBox" runat="server" 
                                Text='<%# Bind("AddressLine1") %>' TabIndex="3" Width="300px" /></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td><asp:TextBox ID="AddressLine2TextBox" runat="server" 
                                Text='<%# Bind("AddressLine2") %>' TabIndex="4" Width="300px" /></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:TextBox ID="CityTextBox" runat="server" Text='<%# Bind("City") %>' 
                                TabIndex="5" Width="156px" />, 
                            <asp:TextBox ID="StateTextBox" runat="server" Text='<%# Bind("State") %>' 
                                TabIndex="6" Width="30px" /> 
                            <asp:TextBox ID="ZipTextBox" runat="server" Text='<%# Bind("Zip") %>' 
                                TabIndex="7" Width="90px" />
                        </td>
                    </tr>
                    <tr>
                        <td>Phone:&nbsp;</td>
                        <td><asp:TextBox ID="PhoneTextBox" runat="server" Text='<%# Bind("Phone") %>' 
                                TabIndex="8" Width="150px" /></td>
                    </tr>
                    <tr>
                        <td>Fax:&nbsp;</td>
                        <td><asp:TextBox ID="FaxTextBox" runat="server" Text='<%# Bind("Fax") %>' 
                                TabIndex="9" Width="150px" /></td>
                    </tr>
                    <tr>
                        <td>Facility Type:&nbsp;</td>
                        <td><asp:TextBox ID="FacilityTypeTextBox" runat="server" 
                                Text='<%# Bind("FacilityType") %>' TabIndex="10" Width="200px" /></td>
                    </tr>
                    <tr>
                        <td>SMS ID:&nbsp;</td>
                        <td><asp:TextBox ID="SMSIDTextBox" runat="server" Text='<%# Bind("SMSID") %>' 
                                TabIndex="11" Width="100px" /></td>
                    </tr>
                </table>
            </div>
            
            <div style="float: left; padding: 5px;">
                <table width="100%" border="0" cellpadding="3" cellspacing="0">
                    <tr>
                        <td>Transfer Facility:&nbsp;</td>
                        <td><asp:CheckBox ID="TransferFacilityCheckBox" runat="server" Checked='<%# Bind("TransferFacility") %>'
                                TabIndex="12" /></td>
                    </tr>
                    <tr>
                        <td>Outpatient Therapy:&nbsp;</td>
                        <td><asp:CheckBox ID="OutpatientTherapyCheckBox" runat="server" Checked='<%# Bind("OutpatientTherapy") %>'
                                TabIndex="13" /></td>
                    </tr>
                    <tr>
                        <td>Acute Facility:&nbsp;</td>
                        <td><asp:CheckBox ID="AcuteFacilityCheckBox" runat="server" Checked='<%# Bind("AcuteFacility") %>'
                                TabIndex="14" /></td>
                    </tr>
                    <tr>
                        <td>Inactive:&nbsp;</td>
                        <td><asp:CheckBox ID="InactiveCheckBox" runat="server" Checked='<%# Bind("Inactive") %>'
                                TabIndex="15" /></td>
                    </tr>
                </table>
            </div>
            
            <div style="clear: both; text-align: center; margin: 15px;">
                <asp:Button ID="InsertButton" runat="server" CausesValidation="True" 
                    CommandName="Insert" Text="Insert" TabIndex="20" />&nbsp;&nbsp;
                <asp:Button ID="InsertCancelButton" runat="server" 
                    CausesValidation="False" CommandName="Cancel" Text="Cancel" TabIndex="21" />
            </div>
        </InsertItemTemplate>
    </asp:FormView>
    <csla:CslaDataSource ID="FacilityDataSource" runat="server" 
        TypeName="ITWMaintenance.Library.Facilities.TransferFacility, ITWMaintenance.Library" 
        TypeSupportsPaging="False" TypeSupportsSorting="False" TypeAssemblyName="">
    </csla:CslaDataSource>
</asp:Content>