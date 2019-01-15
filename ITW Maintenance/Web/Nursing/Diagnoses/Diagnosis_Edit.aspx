<%@ Page Title="ITW Maintenance - Nursing - Diagnosis Edit" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="Diagnosis_Edit.aspx.vb" Inherits="Nursing_Diagnosis_Edit" %>
<%@ Register src="~/Nursing/NursingFormPicker.ascx" tagname="NursingFormPicker" tagprefix="Systemax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <h2><asp:Literal ID="HeaderLabel" runat="server" /></h2>
    <asp:panel id="ErrorPanel" runat="server" Visible="false" cssclass="divError"><asp:Label ID="ErrorLabel" runat="server" /></asp:panel>
    <asp:FormView ID="EditForm" runat="server" DataKeyNames="ID" DataSourceID="DiagnosisDataSource" DefaultMode="Edit">
        <EditItemTemplate>
            <table border="0" cellpadding="5" cellspacing="0" class="form">
                <tr>
                    <td>Diagnosis ID:</td>
                    <td><asp:Label ID="IDLabel" runat="server" Text='<%# Bind("ID") %>' /></td>
                </tr>
                <tr>
                    <td>Description:</td>
                    <td><asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>' 
                        Width="250" />&nbsp;<asp:RequiredFieldValidator runat="server" 
                        id="RequiredFieldValidator1" ControlToValidate="DescriptionTextBox" Text="(required)" /></td>
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
                    <td colspan="2">
                        <div style="float: left; width: 58%;"><h4 style="margin-bottom: 5px;">Linked Forms</h4></div>
                        <asp:Panel ID="LinkNewFormPanel" runat="server" style="text-align: right; float: right; width: 40%;">
                            <asp:LinkButton ID="LinkNewFormButton" runat="server" Text="Link Form..."
                                OnClick="LinkNewFormButton_Click" SkinID="ItalicsLinkButton" style="padding-right: 10px;" />
                        </asp:Panel>
                        <div style="clear: both;"></div>
                        <asp:MultiView ID="LinkFormMultiView" runat="server" ActiveViewIndex="0">
                            <asp:View ID="ListView" runat="server">
                                <asp:GridView ID="LinkedFormsGridView" runat="server" 
                                    AutoGenerateColumns="False" DataKeyNames="FormID" 
                                    DataSourceID="LinkedFormsDataSource" Width="100%" 
                                    onRowDeleting="LinkedFormsGridView_RowDeleting">
                                    <Columns>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                                    CommandName="Delete" Text="Remove" OnClientClick="if (!confirm('Are you sure you wish to remove this Nursing Note Form link?')) return false;" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="FormID" HeaderText="ID" ReadOnly="True" 
                                            SortExpression="FormID" />
                                        <asp:BoundField DataField="FormName" HeaderText="Name" ReadOnly="True" 
                                            SortExpression="FormName" />
                                    </Columns>
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="LinkView" runat="server">
                                <Systemax:NursingFormPicker ID="NursingFormPicker1" runat="server" OnFormSelect="NursingFormPicker1_FormSelect" />
                            </asp:View>
                        </asp:MultiView>
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
                    <td>Description:</td>
                    <td><asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>' 
                        Width="250" />&nbsp;<asp:RequiredFieldValidator runat="server" 
                        id="RequiredFieldValidator1" ControlToValidate="DescriptionTextBox" Text="(required)" /></td>
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
                    <td colspan="2">
                        <div style="float: left; width: 58%;"><h4 style="margin-bottom: 5px;">Linked Forms</h4></div>
                        <asp:Panel ID="LinkNewFormPanel" runat="server" style="text-align: right; float: right; width: 40%;">
                            <asp:LinkButton ID="LinkNewFormButton" runat="server" Text="Link Form..."
                                OnClick="LinkNewFormButton_Click" SkinID="ItalicsLinkButton" style="padding-right: 10px;" />
                        </asp:Panel>
                        <div style="clear: both;"></div>
                        <asp:MultiView ID="LinkFormMultiView" runat="server" ActiveViewIndex="0">
                            <asp:View ID="ListView" runat="server">
                                <asp:GridView ID="LinkedFormsGridView" runat="server" 
                                    AutoGenerateColumns="False" DataKeyNames="FormID" 
                                    DataSourceID="LinkedFormsDataSource" Width="100%" 
                                    onRowDeleting="LinkedFormsGridView_RowDeleting">
                                    <Columns>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                                    CommandName="Delete" Text="Remove" OnClientClick="if (!confirm('Are you sure you wish to remove this Nursing Note Form link?')) return false;" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="FormID" HeaderText="ID" ReadOnly="True" 
                                            SortExpression="FormID" />
                                        <asp:BoundField DataField="FormName" HeaderText="Name" ReadOnly="True" 
                                            SortExpression="FormName" />
                                    </Columns>
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="LinkView" runat="server">
                                <Systemax:NursingFormPicker ID="NursingFormPicker1" runat="server" OnFormSelect="NursingFormPicker1_FormSelect" />
                            </asp:View>
                        </asp:MultiView>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2"><asp:Button ID="InsertButton" runat="server" 
                        CausesValidation="True" CommandName="Insert" Text="Add Diagnosis" />
                        &nbsp;&nbsp;&nbsp;<asp:Button ID="InsertCancelButton" runat="server" 
                        CausesValidation="False" CommandName="Cancel" Text="Cancel" /></td>
                </tr>
            </table>
        </InsertItemTemplate>
    </asp:FormView>
    <csla:CslaDataSource ID="DiagnosisDataSource" runat="server" 
        TypeName="ITWMaintenance.Library.Nursing.Diagnoses.Diagnosis, ITWMaintenance.Library" 
        TypeSupportsPaging="False" TypeSupportsSorting="False">
    </csla:CslaDataSource>
    <csla:CslaDataSource ID="LinkedFormsDataSource" runat="server" 
        TypeName="ITWMaintenance.Library.Nursing.Diagnoses.DiagnosisFormList, ITWMaintenance.Library" 
        TypeSupportsPaging="False" TypeSupportsSorting="False">
    </csla:CslaDataSource>
</asp:Content>