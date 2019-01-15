<%@ Page Title="Edit Order - ITW Maintenance" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="EditOrder.aspx.vb" Inherits="Orders_EditOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <h3><asp:Literal ID="HeaderLabel" runat="server" /></h3>
    <asp:panel id="ErrorPanel" runat="server" Visible="false" cssclass="divError"><asp:Label ID="ErrorLabel" runat="server" /></asp:panel>

    <asp:FormView ID="EditFormView" runat="server" DataKeyNames="ID" 
        DataSourceID="OrderDataSource" DefaultMode="Edit" EnableModelValidation="True">
        <EditItemTemplate>
            <table cellpadding="3" cellspacing="0" border="0">
                <tr>
                    <td>ID:</td>
                    <td><asp:Label ID="IDLabel1" runat="server" Text='<%# Eval("ID") %>' /></td>
                </tr>
                <tr>
                    <td>Alias:</td>
                    <td>
                        <asp:TextBox ID="AliasTextBox" runat="server" Text='<%# Bind("Alias") %>' />
                        <asp:RequiredFieldValidator ID="AliasRequired" runat="server" ErrorMessage="(required)" ControlToValidate="AliasTextBox" Display="Dynamic" ValidationGroup="EditOrder" />
                    </td>
                </tr>
                <tr>
                    <td>Description:</td>
                    <td>
                        <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>' Width="300" />
                        <asp:RequiredFieldValidator ID="DescriptionRequired" runat="server" ErrorMessage="(required)" ControlToValidate="DescriptionTextBox" Display="Dynamic" ValidationGroup="EditOrder" />
                    </td>
                </tr>
                <tr>
                    <td>Order Group:</td>
                    <td>
                        <asp:DropDownList ID="OrderGroupDropDownList" runat="server" DataTextField="Value" 
							DataValueField="Key" AppendDataBoundItems="true" DataSourceID="OrderGroupDataSource">
							<asp:ListItem Text="Select..." Value="" />
						</asp:DropDownList>
                        <%--<asp:RequiredFieldValidator ID="OrderGroupRequired" runat="server" ErrorMessage="(required)" ControlToValidate="OrderGroupTextBox" Display="Dynamic" ValidationGroup="EditOrder" />--%>
                    </td>
                </tr>
                <tr>
                    <td>Lab Collect:</td>
                    <td><asp:CheckBox ID="LabCollectCheckBox" runat="server" 
                            Checked='<%# Bind("LabCollect") %>' /></td>
                </tr>
                <tr>
                    <td>Auto Expand Details:</td>
                    <td><asp:CheckBox ID="AutoExpandDetailsCheckBox" runat="server" 
                            Checked='<%# Bind("AutoExpandDetails") %>' /></td>
                </tr>
            </table>

            <div style="margin-top: 15px;">
                <h3>Order Facility Actions</h3>

                <asp:MultiView ID="FacilityActionMultiView" runat="server" ActiveViewIndex="0">
                    <asp:View ID="ListView" runat="server">
                        <asp:GridView ID="OrderFacilityActionGridView" runat="server" 
                            AutoGenerateColumns="False" DataKeyNames="ID" EmptyDataText="No Facility Actions exist for this order." 
                            DataSourceID="FacilityActionsDataSource" EnableModelValidation="True" OnRowDataBound="OrderFacilityActionGridView_RowDataBound" OnRowDeleting="OrderFacilityActionGridView_RowDeleting">
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                                    ReadOnly="True" SortExpression="ID" Visible="False" />
                                <asp:TemplateField HeaderText="Intake Facility" SortExpression="IntakeFacility">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="FacilityActionIntakeFacilityDropDownList" runat="server" SelectedValue='<%# Bind("IntakeFacility") %>'>
                                            <asp:ListItem Text="Frazier" Value="200" />
                                            <asp:ListItem Text="SIRH" Value="300" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Discipline" SortExpression="Discipline">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="FacilityActionDisciplineDropDownList" runat="server" 
                                            DataTextField="Value" DataValueField="Key" AppendDataBoundItems="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action Code" SortExpression="ActionCode">
                                    <ItemTemplate>
                                        <asp:TextBox ID="ActionCodeTextBox" runat="server" Text='<%# Bind("ActionCode") %>' Width="50" />
                                        <asp:CompareValidator ID="ActionCodeValidator" runat="server" ErrorMessage="(invalid)" Display="Dynamic" 
                                            ControlToValidate="ActionCodeTextBox" Operator="DataTypeCheck" Type="Integer" ValidationGroup="EditOrder" />
                                        <asp:RequiredFieldValidator ID="ActionCodeRequired" runat="server" ErrorMessage="(required)" 
                                            ControlToValidate="ActionCodeTextBox" Display="Dynamic" ValidationGroup="EditOrder" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />
                            </Columns>
                        </asp:GridView>
                        <div style="margin-top: 10px; margin-bottom: 20px;">
                            <asp:LinkButton ID="AddNewFacilityAction" runat="server" 
                                onclick="AddNewFacilityAction_Click">Add New Facility Action...</asp:LinkButton>
                        </div>
                    </asp:View>
                    <asp:View ID="InsertView" runat="server">
                        <asp:DetailsView ID="NewFacilityActionDetailsView" runat="server" 
                            SkinID="InsertDetails" AutoGenerateRows="False" 
                            DataKeyNames="ID" DataSourceID="FacilityActionsDataSource" DefaultMode="Insert" 
                            EnableModelValidation="True" OnDataBound="NewFacilityActionDetailsView_DataBound" 
                            OnItemInserting="NewFacilityActionDetailsView_ItemInserting">
                            <Fields>
                                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                                    ReadOnly="True" SortExpression="ID" />
                                <asp:TemplateField HeaderText="Intake Facility" SortExpression="IntakeFacility">
                                    <InsertItemTemplate>
                                        <asp:DropDownList ID="NewFacilityActionIntakeFacilityDropDownList" runat="server" SelectedValue='<%# Bind("IntakeFacility") %>'>
                                            <asp:ListItem Text="Frazier" Value="200" />
                                            <asp:ListItem Text="SIRH" Value="300" />
                                        </asp:DropDownList>
                                    </InsertItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Discipline" SortExpression="Discipline">
                                    <InsertItemTemplate>
                                        <asp:DropDownList ID="NewFacilityActionDisciplineDropDownList" runat="server" 
                                            DataTextField="Value" DataValueField="Key" AppendDataBoundItems="true" />
                                    </InsertItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action Code" SortExpression="ActionCode">
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="NewFacilityActionActionCodeTextBox" runat="server" Text='<%# Bind("ActionCode") %>' Width="50" />
                                        <asp:CompareValidator ID="ActionCodeValidator" runat="server" ErrorMessage="(invalid)" Display="Dynamic" 
                                            ControlToValidate="NewFacilityActionActionCodeTextBox" Operator="DataTypeCheck" Type="Integer" ValidationGroup="NewFacilityAction" />
                                        <asp:RequiredFieldValidator ID="ActionCodeRequired" runat="server" ErrorMessage="(required)" 
                                            ControlToValidate="NewFacilityActionActionCodeTextBox" Display="Dynamic" ValidationGroup="NewFacilityAction" />
                                    </InsertItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                            CommandName="New" Text="New"></asp:LinkButton>
                                    </ItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" 
                                            CommandName="Insert" Text="Add" ValidationGroup="NewFacilityAction" />
                                        &nbsp;&nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                                            CommandName="Cancel" Text="Cancel" />
                                    </InsertItemTemplate>
                                </asp:TemplateField>
                            </Fields>
                        </asp:DetailsView>
                    </asp:View>
                </asp:MultiView>
            </div>

            <div style="margin-top: 15px;">
                <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" 
                    CommandName="Update" Text="Update" ValidationGroup="EditOrder" />
                &nbsp;&nbsp;&nbsp;<asp:Button ID="UpdateCancelButton" runat="server" 
                    CausesValidation="False" CommandName="Cancel" Text="Cancel" />
            </div>
        </EditItemTemplate>
        <InsertItemTemplate>
            <table cellpadding="3" cellspacing="0" border="0">
                <tr>
                    <td>Alias:</td>
                    <td>
                        <asp:TextBox ID="AliasTextBox" runat="server" Text='<%# Bind("Alias") %>' />
                        <asp:RequiredFieldValidator ID="AliasRequired" runat="server" ErrorMessage="(required)" ControlToValidate="AliasTextBox" Display="Dynamic" ValidationGroup="NewOrder" />
                    </td>
                </tr>
                <tr>
                    <td>Description:</td>
                    <td>
                        <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>' Width="300" />
                        <asp:RequiredFieldValidator ID="DescriptionRequired" runat="server" ErrorMessage="(required)" ControlToValidate="DescriptionTextBox" Display="Dynamic" ValidationGroup="NewOrder" />
                    </td>
                </tr>
                <tr>
                    <td>Order Group:</td>
                    <td>
						<asp:DropDownList ID="OrderGroupDropDownList" runat="server" DataTextField="Value" 
							DataValueField="Key" AppendDataBoundItems="true" DataSourceID="OrderGroupDataSource">
							<asp:ListItem Text="Select..." Value="" />
						</asp:DropDownList> 
						Or add new:
                        <asp:TextBox ID="OrderGroupTextBox" runat="server" Text='<%# Bind("OrderGroup") %>' />
                        <%--<asp:RequiredFieldValidator ID="OrderGroupRequired" runat="server" ErrorMessage="(required)" 
							ControlToValidate="OrderGroupTextBox" Display="Dynamic" ValidationGroup="NewOrder" />--%>
                    </td>
                </tr>
                <tr>
                    <td>Lab Collect:</td>
                    <td><asp:CheckBox ID="LabCollectCheckBox" runat="server" 
                            Checked='<%# Bind("LabCollect") %>' /></td>
                </tr>
                <tr>
                    <td>Auto Expand Details:</td>
                    <td><asp:CheckBox ID="AutoExpandDetailsCheckBox" runat="server" 
                            Checked='<%# Bind("AutoExpandDetails") %>' /></td>
                </tr>
            </table>

            <div style="margin-top: 15px;">
                <h3>Order Facility Actions</h3>

                <asp:MultiView ID="FacilityActionMultiView" runat="server" ActiveViewIndex="0">
                    <asp:View ID="ListView" runat="server">
                        <asp:GridView ID="OrderFacilityActionGridView" runat="server" 
                            AutoGenerateColumns="False" DataKeyNames="ID" EmptyDataText="No Facility Actions exist for this order." 
                            DataSourceID="FacilityActionsDataSource" EnableModelValidation="True" OnRowDataBound="OrderFacilityActionGridView_RowDataBound" OnRowDeleting="OrderFacilityActionGridView_RowDeleting">
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                                    ReadOnly="True" SortExpression="ID" Visible="False" />
                                <asp:TemplateField HeaderText="Intake Facility" SortExpression="IntakeFacility">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="FacilityActionIntakeFacilityDropDownList" runat="server" SelectedValue='<%# Bind("IntakeFacility") %>'>
                                            <asp:ListItem Text="Frazier" Value="200" />
                                            <asp:ListItem Text="SIRH" Value="300" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Discipline" SortExpression="Discipline">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="FacilityActionDisciplineDropDownList" runat="server" 
                                            DataTextField="Value" DataValueField="Key" AppendDataBoundItems="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action Code" SortExpression="ActionCode">
                                    <ItemTemplate>
                                        <asp:TextBox ID="ActionCodeTextBox" runat="server" Text='<%# Bind("ActionCode") %>' Width="50" />
                                        <asp:CompareValidator ID="ActionCodeValidator" runat="server" ErrorMessage="(invalid)" Display="Dynamic" 
                                            ControlToValidate="ActionCodeTextBox" Operator="DataTypeCheck" Type="Integer" ValidationGroup="NewOrder" />
                                        <asp:RequiredFieldValidator ID="ActionCodeRequired" runat="server" ErrorMessage="(required)" 
                                            ControlToValidate="ActionCodeTextBox" Display="Dynamic" ValidationGroup="NewOrder" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />
                            </Columns>
                        </asp:GridView>
                        <div style="margin-top: 10px; margin-bottom: 20px;">
                            <asp:LinkButton ID="AddNewFacilityAction" runat="server" 
                                onclick="AddNewFacilityAction_Click">Add New Facility Action...</asp:LinkButton>
                        </div>
                    </asp:View>
                    <asp:View ID="InsertView" runat="server">
                        <asp:DetailsView ID="NewFacilityActionDetailsView" runat="server" 
                            SkinID="InsertDetails" AutoGenerateRows="False" 
                            DataKeyNames="ID" DataSourceID="FacilityActionsDataSource" DefaultMode="Insert" 
                            EnableModelValidation="True" OnDataBound="NewFacilityActionDetailsView_DataBound" 
                            OnItemInserting="NewFacilityActionDetailsView_ItemInserting">
                            <Fields>
                                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                                    ReadOnly="True" SortExpression="ID" />
                                <asp:TemplateField HeaderText="Intake Facility" SortExpression="IntakeFacility">
                                    <InsertItemTemplate>
                                        <asp:DropDownList ID="NewFacilityActionIntakeFacilityDropDownList" runat="server" SelectedValue='<%# Bind("IntakeFacility") %>'>
                                            <asp:ListItem Text="Frazier" Value="200" />
                                            <asp:ListItem Text="SIRH" Value="300" />
                                        </asp:DropDownList>
                                    </InsertItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Discipline" SortExpression="Discipline">
                                    <InsertItemTemplate>
                                        <asp:DropDownList ID="NewFacilityActionDisciplineDropDownList" runat="server" 
                                            DataTextField="Value" DataValueField="Key" AppendDataBoundItems="true" />
                                    </InsertItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action Code" SortExpression="ActionCode">
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="NewFacilityActionActionCodeTextBox" runat="server" Text='<%# Bind("ActionCode") %>' Width="50" />
                                        <asp:CompareValidator ID="ActionCodeValidator" runat="server" ErrorMessage="(invalid)" Display="Dynamic" 
                                            ControlToValidate="NewFacilityActionActionCodeTextBox" Operator="DataTypeCheck" Type="Integer" ValidationGroup="NewFacilityAction" />
                                        <asp:RequiredFieldValidator ID="ActionCodeRequired" runat="server" ErrorMessage="(required)" 
                                            ControlToValidate="NewFacilityActionActionCodeTextBox" Display="Dynamic" ValidationGroup="NewFacilityAction" />
                                    </InsertItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                            CommandName="New" Text="New"></asp:LinkButton>
                                    </ItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" 
                                            CommandName="Insert" Text="Add" ValidationGroup="NewFacilityAction" />
                                        &nbsp;&nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                                            CommandName="Cancel" Text="Cancel" />
                                    </InsertItemTemplate>
                                </asp:TemplateField>
                            </Fields>
                        </asp:DetailsView>
                    </asp:View>
                </asp:MultiView>
            </div>

            <div style="margin-top: 15px;">
                <asp:Button ID="InsertButton" runat="server" CausesValidation="True" 
                    CommandName="Insert" Text="Insert" ValidationGroup="NewOrder" />
                &nbsp;<asp:Button ID="InsertCancelButton" runat="server" 
                    CausesValidation="False" CommandName="Cancel" Text="Cancel" />
            </div>
        </InsertItemTemplate>
        <ItemTemplate>
            ID:
            <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
            <br />
            Alias:
            <asp:Label ID="AliasLabel" runat="server" Text='<%# Bind("Alias") %>' />
            <br />
            Description:
            <asp:Label ID="DescriptionLabel" runat="server" 
                Text='<%# Bind("Description") %>' />
            <br />
            OrderGroup:
            <asp:Label ID="OrderGroupLabel" runat="server" 
                Text='<%# Bind("OrderGroup") %>' />
            <br />
            LabCollect:
            <asp:CheckBox ID="LabCollectCheckBox" runat="server" 
                Checked='<%# Bind("LabCollect") %>' Enabled="false" />
            <br />
            AutoExpandDetails:
            <asp:CheckBox ID="AutoExpandDetailsCheckBox" runat="server" 
                Checked='<%# Bind("AutoExpandDetails") %>' Enabled="false" />
            <br />
            OrderFacilityActionItems:
            <asp:Label ID="OrderFacilityActionItemsLabel" runat="server" 
                Text='<%# Bind("OrderFacilityActionItems") %>' />
            <br />
            <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" 
                CommandName="Edit" Text="Edit" />
            &nbsp;<asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" 
                CommandName="Delete" Text="Delete" />
            &nbsp;<asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" 
                CommandName="New" Text="New" />
        </ItemTemplate>
    </asp:FormView>

    <csla:CslaDataSource ID="OrderDataSource" runat="server" TypeAssemblyName="" 
        TypeName="ITWMaintenance.Library.Orders.OrderCatalogItem, ITWMaintenance.Library" 
        TypeSupportsPaging="False" TypeSupportsSorting="False">
    </csla:CslaDataSource>
    <csla:CslaDataSource ID="FacilityActionsDataSource" runat="server" 
        TypeAssemblyName="" 
        TypeName="ITWMaintenance.Library.Orders.OrderFacilityActionItemList, ITWMaintenance.Library" 
        TypeSupportsPaging="False" TypeSupportsSorting="False">
    </csla:CslaDataSource>
    <asp:ObjectDataSource ID="DisciplineDataSource" runat="server" 
        SelectMethod="GetAllDisciplines" 
        TypeName="ITWMaintenance.Library.Lookup.DisciplineList"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OrderGroupDataSource" runat="server" 
        SelectMethod="GetOrderGroups" 
        TypeName="ITWMaintenance.Library.Orders.OrderGroups"></asp:ObjectDataSource>
</asp:Content>