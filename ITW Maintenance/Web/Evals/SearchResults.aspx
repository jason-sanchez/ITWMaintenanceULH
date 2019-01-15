<%@ Page Title="ITW Maintenance - Evals - Search Results" Language="VB" MasterPageFile="~/Evals/EvalMainMaster.master" AutoEventWireup="false" CodeFile="SearchResults.aspx.vb" Inherits="Evals_SearchResults" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <h3>Search Results</h3>
    <strong>Searching for:</strong> <asp:TextBox ID="SearchTextTextBox" runat="server" Width="200px" />&nbsp;
    Sort by: <asp:DropDownList ID="SortByDropDownList" runat="server" />&nbsp;
    <asp:Button ID="SearchButton" runat="server" text="Search" /><br /><br />
    <asp:GridView ID="SearchResultsGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="EvalID,Level2" DataSourceID="SearchResultDataSource">
        <EmptyDataTemplate>
            No Records Found.
        </EmptyDataTemplate>
        <Columns>
            <asp:CommandField ButtonType="Button" SelectText="Load" ShowSelectButton="True" />
            <asp:BoundField DataField="EvalID" HeaderText="ID" InsertVisible="False" ReadOnly="True"
                SortExpression="EvalID" />
            <asp:TemplateField HeaderText="Name" SortExpression="EName">
                <ItemTemplate>
                    <asp:Image ID="EvalIconImage" runat="server" />
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("EName") %>' />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Image ID="EvalIconImage" runat="server" />
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("EName") %>'></asp:Label>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="EPath" HeaderText="Path" SortExpression="EPath" />
            <asp:CheckBoxField DataField="Inactive" HeaderText="Inactive" ReadOnly="True" SortExpression="Inactive" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="SearchResultDataSource" runat="server" SelectMethod="Search"
        TypeName="ITWMaintenance.Library.Evaluations.ReadOnlyEvalSearchResultList">
    </asp:ObjectDataSource>
</asp:Content>

