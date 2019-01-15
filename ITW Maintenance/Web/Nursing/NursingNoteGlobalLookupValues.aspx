<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="NursingNoteGlobalLookupValues.aspx.vb" Inherits="Nursing_NursingNoteGlobalLookupValues" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <asp:Panel id="ErrorPanel" runat="server" Visible="false" cssclass="divError" style="clear: both;"><asp:Label ID="ErrorLabel" runat="server" /></asp:Panel>
    
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="ListView" runat="server">
            <asp:Panel ID="NewPanel" runat="server" Visible="false" style="width: 100px; float: left; margin-bottom: 10px;">
                <asp:Button ID="NewButton" runat="server" Text="Add New" />
            </asp:Panel>
            <div style="float: right; margin-bottom: 10px;">
                Search: 
                <asp:TextBox ID="SearchTextBox" runat="server"></asp:TextBox> 
                <asp:DropDownList ID="SearchFieldDropDownList" runat="server" />
                <asp:Button ID="SearchButton" runat="server" Text="Search" />
            </div>
            <asp:GridView ID="NursingNoteGlobalLookupListGridView" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="ID" DataSourceID="NursingNoteGlobalLookupListDataSource" CellPadding="5" style="clear: both;">
                <EmptyDataTemplate>
                    No Nursing Note Global Lookup Values Found
                </EmptyDataTemplate>
                <Columns>
                    <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                    <asp:TemplateField HeaderText="Description" SortExpression="Description">
                        <EditItemTemplate>
                            <asp:DropDownList ID="DescriptionDropDown" runat="server" 
                                SelectedValue='<%# Bind("Description") %>' DataSourceID="DescriptionDataSource" 
                                DataTextField="Key" DataValueField="Value" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Form" SortExpression="FormNumber">
                        <EditItemTemplate>
                            <asp:TextBox ID="FormNumberTextBox" runat="server" Text='<%# Bind("FormNumber") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="FormNumberLabel" runat="server" Text='<%# Bind("FormNumber") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Field" SortExpression="FormField">
                        <EditItemTemplate>
                            <asp:DropDownList ID="FormFieldDropDownList" runat="server" SelectedValue='<%# Bind("FormField") %>'>
                                <asp:ListItem Value="1" Text="1" />
                                <asp:ListItem Value="2" Text="2" />
                                <asp:ListItem Value="3" Text="3" />
                                <asp:ListItem Value="4" Text="4" />
                                <asp:ListItem Value="5" Text="5" />
                                <asp:ListItem Value="6" Text="6" />
                                <asp:ListItem Value="7" Text="7" />
                                <asp:ListItem Value="8" Text="8" />
                                <asp:ListItem Value="9" Text="9" />
                                <asp:ListItem Value="10" Text="10" />
                                <asp:ListItem Value="11" Text="11" />
                                <asp:ListItem Value="12" Text="12" />
                                <asp:ListItem Value="13" Text="13" />
                                <asp:ListItem Value="14" Text="14" />
                                <asp:ListItem Value="15" Text="15" />
                                <asp:ListItem Value="16" Text="16" />
                                <asp:ListItem Value="17" Text="17" />
                                <asp:ListItem Value="18" Text="18" />
                                <asp:ListItem Value="19" Text="19" />
                                <asp:ListItem Value="20" Text="20" />
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="FormFieldLabel" runat="server" Text='<%# Bind("FormField") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:View>
        <asp:View ID="InsertView" runat="server">
            <h3>New Nursing Note Global Lookup Value</h3>
            <asp:DetailsView ID="InsertDetailsView" runat="server" 
                AutoGenerateRows="False" DataKeyNames="ID" SkinID="InsertDetails" 
                DataSourceID="NursingNoteGlobalLookupListDataSource" CellPadding="3">
                <Fields>
                    <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                        ReadOnly="True" SortExpression="ID" />
                    <asp:TemplateField HeaderText="Description" SortExpression="Description">
                        <InsertItemTemplate>
                            <asp:DropDownList ID="DescriptionDropDown" runat="server" 
                                SelectedValue='<%# Bind("Description") %>' DataSourceID="DescriptionDataSource" 
                                DataTextField="Key" DataValueField="Value" />
                            or add new: <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>' />
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Form Number" SortExpression="FormNumber">
                        <InsertItemTemplate>
                            <asp:TextBox ID="FormNumberTextBox" runat="server" Text='<%# Bind("FormNumber") %>' Width="75px" />
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Form Field" SortExpression="FormField">
                        <InsertItemTemplate>
                            <asp:DropDownList ID="FormFieldDropDownList" runat="server" SelectedValue='<%# Bind("FormField") %>'>
                                <asp:ListItem Value="1" Text="1" />
                                <asp:ListItem Value="2" Text="2" />
                                <asp:ListItem Value="3" Text="3" />
                                <asp:ListItem Value="4" Text="4" />
                                <asp:ListItem Value="5" Text="5" />
                                <asp:ListItem Value="6" Text="6" />
                                <asp:ListItem Value="7" Text="7" />
                                <asp:ListItem Value="8" Text="8" />
                                <asp:ListItem Value="9" Text="9" />
                                <asp:ListItem Value="10" Text="10" />
                                <asp:ListItem Value="11" Text="11" />
                                <asp:ListItem Value="12" Text="12" />
                                <asp:ListItem Value="13" Text="13" />
                                <asp:ListItem Value="14" Text="14" />
                                <asp:ListItem Value="15" Text="15" />
                                <asp:ListItem Value="16" Text="16" />
                                <asp:ListItem Value="17" Text="17" />
                                <asp:ListItem Value="18" Text="18" />
                                <asp:ListItem Value="19" Text="19" />
                                <asp:ListItem Value="20" Text="20" />
                            </asp:DropDownList>
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <InsertItemTemplate>
                            <br />
                            <asp:Button ID="Button1" runat="server" CausesValidation="True" 
                                CommandName="Insert" Text="Add Lookup" />
                            &nbsp;&nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" 
                                CommandName="Cancel" Text="Cancel" />
                        </InsertItemTemplate>
                    </asp:TemplateField>
                </Fields>
            </asp:DetailsView>
        </asp:View>
    </asp:MultiView>
    <csla:CslaDataSource ID="NursingNoteGlobalLookupListDataSource" runat="server" 
        TypeAssemblyName="" TypeName="ITWMaintenance.Library.Nursing.GlobalLookup.NursingNoteGlobalLookupList, ITWMaintenance.Library" 
        TypeSupportsPaging="False" TypeSupportsSorting="False">
    </csla:CslaDataSource>
    <asp:ObjectDataSource ID="DescriptionDataSource" runat="server" 
        SelectMethod="GetDescriptionList" 
        TypeName="ITWMaintenance.Library.Nursing.GlobalLookup.NursingNoteGlobalLookupDescriptionList">
    </asp:ObjectDataSource>
</asp:Content>