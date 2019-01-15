<%@ Page Title="ITW Maintenance - Interventions - Search Results" Language="VB" MasterPageFile="~/IntVnts/IntVntMainMaster.master" AutoEventWireup="false" CodeFile="SearchResults.aspx.vb" Inherits="IntVnts_SearchResults" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <h3>Search Results</h3>
    <strong>Searching for:</strong> <asp:TextBox ID="SearchTextTextBox" runat="server" Width="200px" />&nbsp;
    Sort by: <asp:DropDownList ID="SortByDropDownList" runat="server" />&nbsp;
    <asp:Button ID="SearchButton" runat="server" text="Search" /><br /><br />
    <asp:GridView ID="SearchResultsGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="intVntID,Level2" DataSourceID="SearchResultDataSource">
        <EmptyDataTemplate>
            No Records Found.
        </EmptyDataTemplate>
        <Columns>
            <asp:CommandField ButtonType="Button" SelectText="Load" ShowSelectButton="True" />
            <asp:BoundField DataField="intVntID" HeaderText="ID" InsertVisible="False" ReadOnly="True"
                SortExpression="intVntID" />
            <asp:TemplateField HeaderText="Name" SortExpression="iName">
                <ItemTemplate>
                    <asp:Image ID="IntVntIconImage" runat="server" />
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("iName") %>' />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Image ID="IntVntIconImage" runat="server" />
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("iName") %>'></asp:Label>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="iPath" HeaderText="Path" SortExpression="iPath" />
            <asp:CheckBoxField DataField="Inactive" HeaderText="Inactive" ReadOnly="True" SortExpression="Inactive" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="SearchResultDataSource" runat="server" SelectMethod="Search"
        TypeName="ITWMaintenance.Library.Interventions.ReadOnlyIntVntSearchResultList">
    </asp:ObjectDataSource>
</asp:Content>

