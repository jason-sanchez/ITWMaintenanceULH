<%@ Page Title="ITW Maintenance - Nursing Notes - Search Results" Language="VB" MasterPageFile="~/Nursing/Notes/NursingNoteMainMaster.master" AutoEventWireup="false" CodeFile="SearchResults.aspx.vb" Inherits="Nursing_Notes_SearchResults" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <h3 style="clear: both;">Search Results</h3>
    <strong>Searching for:</strong> <asp:TextBox ID="SearchTextBox" runat="server" Width="200px" />&nbsp;
    Sort:&nbsp;<asp:DropDownList ID="SortByDropDownList" runat="server" />&nbsp;
    <asp:Button ID="SearchButton" runat="server" Text="Search" /><br /><br />
    
    <asp:GridView ID="SearchResultsGridView" runat="server" 
        AutoGenerateColumns="False" DataKeyNames="ID" Width="100%" 
        DataSourceID="SearchResultDataSource">
        <Columns>
            <asp:CommandField ButtonType="Button" SelectText="Load" ShowSelectButton="True" />
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                ReadOnly="True" SortExpression="ID" />
            <asp:TemplateField HeaderText="Name" SortExpression="Name">
                <ItemTemplate>
                    <asp:Image ID="IconImage" runat="server" />
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>' />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Image ID="IconImage" runat="server" />
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Discipline" HeaderText="Discipline" InsertVisible="False" 
                ReadOnly="True" SortExpression="Discipline" />
            <asp:CheckBoxField DataField="Inactive" HeaderText="Inactive" ReadOnly="True" 
                SortExpression="Inactive" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="SearchResultDataSource" runat="server" SelectMethod="Search"
        TypeName="ITWMaintenance.Library.Nursing.Notes.ReadOnlyNursingNoteSearchResultList">
        <SelectParameters>
            <asp:Parameter DefaultValue="-1" Name="FormID" Type="Int32" />
            <asp:Parameter DefaultValue="" Name="SortBy" Type="Object" />
            <asp:Parameter DefaultValue="False" Name="OnlySearchForms" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>