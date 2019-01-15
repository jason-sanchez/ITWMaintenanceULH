<%@ Control Language="VB" AutoEventWireup="false" CodeFile="NursingFormPicker.ascx.vb" Inherits="Nursing_NursingFormPicker" %>
<table cellpadding="3" cellspacing="0">
    <tr>
        <td>Search:&nbsp;<asp:TextBox ID="SearchTextBox" runat="server" Width="242px" /></td>
        <td>Sort:&nbsp;<asp:DropDownList ID="SortByDropDownList" runat="server" /></td>
        <td><asp:Button ID="SearchButton" runat="server" Text="Search" /></td>
    </tr>
</table>

<asp:MultiView ID="SearchMultiView" runat="server" ActiveViewIndex="0">
    <asp:View ID="TreeView" runat="server"><div id="FormTreeDiv" style="padding: 5px; margin-right: 7px;">
        <div>
            <div style="text-align: right;"><asp:CheckBox ID="ActiveOnlyCheckBox" runat="server" Text="Active Only" AutoPostBack="true" Checked="true" /></div>
            <asp:TreeView ID="FormTreeView" runat="server" DataSourceID="TreeXmlDataSource" 
                EnableViewState="True" ExpandDepth="0">
                <DataBindings>
                    <asp:TreeNodeBinding DataMember="TreeNode" TextField="Name" ValueField="ID" />
                </DataBindings>
            </asp:TreeView>
            <asp:XmlDataSource ID="TreeXmlDataSource" runat="server" EnableCaching="False" XPath="/*/*"></asp:XmlDataSource>
        </div>
    </div>
    </asp:View>
    <asp:View ID="SearchResultsView" runat="server">
        <asp:GridView ID="SearchResultsGridView" runat="server" 
            AutoGenerateColumns="False" DataKeyNames="ID" Width="100%" 
            DataSourceID="SearchResultDataSource">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                    ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" 
                    SortExpression="Name" />
                <asp:CheckBoxField DataField="Inactive" HeaderText="Inactive" ReadOnly="True" 
                    SortExpression="Inactive" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="SearchResultDataSource" runat="server" SelectMethod="Search"
            TypeName="ITWMaintenance.Library.Nursing.Notes.ReadOnlyNursingNoteSearchResultList">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="FormID" Type="Int32" />
                <asp:Parameter DefaultValue="" Name="SortBy" Type="Object" />
                <asp:Parameter DefaultValue="True" Name="OnlySearchForms" Type="Boolean" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </asp:View>
</asp:MultiView>