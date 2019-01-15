<%@ Page Title="ITW Maintenance - Diagnosis Classifications" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="DiagnosisClassifications.aspx.vb" Inherits="Classifications_DiagnosisClassifications" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <asp:Panel id="ErrorPanel" runat="server" Visible="false" cssclass="divError" style="clear: both;"><asp:Label ID="ErrorLabel" runat="server" /></asp:panel>
    
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="ListView" runat="server">
            <asp:Panel ID="NewPanel" runat="server" Visible="false" style="float: left; margin-top: 5px; margin-bottom: 10px;">
                <asp:Button ID="NewButton" runat="server" Text="Add New" />
            </asp:Panel>
            <div style="float: right; clear: right; padding: 5px 0px 5px 0px;">
                <asp:CheckBox id="ActiveOnlyCheckBox" runat="server" Text=" Active Only" TextAlign="Right" 
                    AutoPostBack="True" Checked="True" />
            </div>
            
            <div style="clear: both;">
                <asp:GridView ID="DiagnosisClassificationListGridView" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="ID" DataSourceID="DiagnosisClassificationListDataSource" Width="100%">
                    <EmptyDataTemplate>
                        No Diagnosis Classifications Found
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:CommandField ButtonType="Button" ShowEditButton="True" >
                        <ItemStyle Width="5%" />
                        </asp:CommandField>
                        <asp:TemplateField HeaderText="Category" SortExpression="Category">
                            <EditItemTemplate>
                            <asp:DropDownList ID="CategoryDropDownList" runat="server" 
                                AppendDataBoundItems="True" DataSourceID="CategoryDataSource" 
                                DataTextField="Key" DataValueField="Value" 
                                SelectedValue='<%# Bind("Category") %>'>
                                <asp:ListItem Text="Select..." Value="" />
                            </asp:DropDownList>&nbsp;&nbsp;OR Add New:&nbsp;&nbsp;
                            <asp:TextBox ID="CategoryTextBox" runat="server" Text='' />&nbsp;
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Category") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description" SortExpression="Description">
                            <EditItemTemplate>
                                <asp:TextBox ID="DescriptionTextBox" runat="server" 
                                    Text='<%# Bind("Description") %>' Width="350"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Inactive" SortExpression="Inactive">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <EditItemTemplate>
                                <asp:CheckBox ID="InactiveCheckBox" runat="server" Checked='<%# Bind("Inactive") %>' />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="InactiveCheckBox" runat="server" Enabled="false" Checked='<%# Bind("Inactive") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </asp:View>
        <asp:View ID="InsertView" runat="server">
            <h1>New Diagnosis Classification</h1>
            <asp:DetailsView ID="InsertDetailsView" runat="server" 
                AutoGenerateRows="False" DataKeyNames="ID" SkinID="InsertDetails" 
                DataSourceID="DiagnosisClassificationListDataSource" CellPadding="3" 
                DefaultMode="Insert">
                <Fields>
                    <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                        ReadOnly="True" SortExpression="ID" />
                    <asp:TemplateField HeaderText="Category" SortExpression="Category">
                        <InsertItemTemplate>
                            <asp:DropDownList ID="CategoryDropDownList" runat="server" 
                                AppendDataBoundItems="True" DataSourceID="CategoryDataSource" 
                                DataTextField="Key" DataValueField="Value" 
                                SelectedValue='<%# Bind("Category") %>'>
                                <asp:ListItem Text="Select..." Value="" />
                            </asp:DropDownList>&nbsp;&nbsp;OR Add New:&nbsp;&nbsp;
                            <asp:TextBox ID="CategoryTextBox" runat="server" Text='<%# Bind("Category") %>' />&nbsp;
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description" SortExpression="Description">
                        <InsertItemTemplate>
                            <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>' />&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="required"
                                ControlToValidate="DescriptionTextBox" />
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Inactive" SortExpression="Inactive">
                        <InsertItemTemplate>
                            <asp:CheckBox ID="InactiveCheckBox" runat="server" Checked='<%# Bind("Inactive") %>' />
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <InsertItemTemplate>
                            <br />
                            <asp:Button ID="Button1" runat="server" CausesValidation="True" 
                                CommandName="Insert" Text="Add Classification" />
                            &nbsp;&nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" 
                                CommandName="Cancel" Text="Cancel" />
                        </InsertItemTemplate>
                    </asp:TemplateField>
                </Fields>
            </asp:DetailsView>
        </asp:View>
    </asp:MultiView>
    <csla:CslaDataSource ID="DiagnosisClassificationListDataSource" runat="server" 
        TypeAssemblyName="" TypeName="ITWMaintenance.Library.Classifications.DiagnosisClassificationList, ITWMaintenance.Library" 
        TypeSupportsPaging="False" TypeSupportsSorting="False">
    </csla:CslaDataSource>
    <csla:CslaDataSource ID="CategoryDataSource" runat="server" 
        TypeAssemblyName="" TypeName="ITWMaintenance.Library.Classifications.DiagnosisClassificationCategoryList, ITWMaintenance.Library" 
        TypeSupportsPaging="False" TypeSupportsSorting="False">
    </csla:CslaDataSource>
</asp:Content>