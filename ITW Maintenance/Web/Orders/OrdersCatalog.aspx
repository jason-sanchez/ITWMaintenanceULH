<%@ Page Title="Orders Catalog - ITW Maintenance" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="OrdersCatalog.aspx.vb" Inherits="Orders_OrdersCatalog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function SearchIfEnter(keyStroke) {
            isNetscape = (document.layers);
            keyCode = (isNetscape) ? keyStroke.which : event.keyCode;
            if (keyCode == 13)
                Search();
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <div style="float: left; padding-bottom: 0px;">
        <h3 class="PageHeading" style="margin-bottom: 15px;">Orders Catalog Maintenance</h3>
        <asp:Button ID="AddOrderButton" runat="server" Text="Add Order" UseSubmitBehavior="false" />
    </div>
    <div style="float: right; text-align: right;">
        Search: <asp:TextBox ID="SearchTextBox" runat="server" style="width: 200px;" onkeypress="SearchIfEnter();" /> 
        <asp:Button ID="SearchButton" runat="server" Text="Search" OnClientClick="Search();" UseSubmitBehavior="true" />
    </div>
    <div style="clear: both; margin-top: 10px;">
        <asp:GridView ID="OrdersCatalogGridView" runat="server" 
            AutoGenerateColumns="False" DataKeyNames="ID" Width="100%" 
            DataSourceID="OrdersCatalogDataSource" EnableModelValidation="True" AllowPaging="True" PageSize="25">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="EditButton" runat="server" Text="Edit" CommandName="EditItem" CommandArgument='<%# Eval("ID") %>' UseSubmitBehavior="false" />
                    </ItemTemplate>
                    <ItemStyle Width="5%" />
                </asp:TemplateField>
                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                    ReadOnly="True" SortExpression="ID" Visible="False" />
                <asp:BoundField DataField="Alias" HeaderText="Alias" ReadOnly="True" 
                    SortExpression="Alias" />
                <asp:BoundField DataField="Description" HeaderText="Description" 
                    ReadOnly="True" SortExpression="Description" />
                <asp:BoundField DataField="OrderGroup" HeaderText="Order Group" ReadOnly="True" 
                    SortExpression="OrderGroup" />
                <asp:CheckBoxField DataField="LabCollect" HeaderText="Lab Collect" 
                    ReadOnly="True" SortExpression="LabCollect" />
                <asp:CheckBoxField DataField="AutoExpandDetails" HeaderText="Auto Expand Details" 
                    ReadOnly="True" SortExpression="AutoExpandDetails" />
            </Columns>
        </asp:GridView>
        <csla:CslaDataSource ID="OrdersCatalogDataSource" runat="server" 
            TypeAssemblyName="" 
            TypeName="ITWMaintenance.Library.Orders.ReadOnlyOrderCatalogItemList, ITWMaintenance.Library" 
            TypeSupportsPaging="False" TypeSupportsSorting="False">
        </csla:CslaDataSource>
    </div>
</asp:Content>