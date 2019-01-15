<%@ Page Title="ITW Maintenance - Users - User List" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="UserList.aspx.vb" Inherits="Users_UserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function SearchIfEnter(keyStroke) {
            isNetscape = (document.layers);
            keyCode = (isNetscape) ? keyStroke.which : event.keyCode;
            if (keyCode == 13)
                Search();
        }

        function Search() {
            event.cancelBubble = true;
            event.returnValue = false;
            // Submit the form to cause a post-back
            document.forms[0].submit();
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <div style="float: left; padding-bottom: 0px;">
        <h3 class="PageHeading" style="margin-bottom: 15px;">User Maintenance</h3>
        <asp:Button ID="AddUserButton" runat="server" Text="Add User" />
    </div>
    <div style="float: right; text-align: right;">
        Search: <asp:TextBox ID="SearchTextBox" runat="server" style="width: 200px;" onkeypress="SearchIfEnter();" /> 
        <input type="button" id="SearchButton" value="Search" onclick="Search();" />
    </div>
    <div style="float: right; clear: right; padding: 10px 0px 5px 0px;">
        <asp:CheckBox id="ActiveOnlyCheckBox" runat="server" Text=" Active Only" TextAlign="Right" 
            AutoPostBack="True" Checked="True" />
    </div>
    <div style="clear: both;">
        <asp:GridView ID="UserListGridView" runat="server" 
            DataSourceID="UserListDataSource" AutoGenerateColumns="False" Width="100%" 
            SkinID="ClickableGrid">
            <EmptyDataTemplate>
                No Users Found
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="EditItem" CommandArgument='<%# Eval("ID") %>' />
                    </ItemTemplate>
                    <ItemStyle Width="5%" />
                </asp:TemplateField>
                <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" 
                    SortExpression="ID">
                <ItemStyle Width="5%" />
                </asp:BoundField>
                <asp:BoundField DataField="FullName" HeaderText="Full Name" ReadOnly="True" 
                    SortExpression="FullName" />
                <asp:BoundField DataField="UserName" HeaderText="User Name" ReadOnly="True" 
                    SortExpression="UserName" />
                <asp:BoundField DataField="Discipline" HeaderText="Discipline" ReadOnly="True" 
                    SortExpression="Discipline" />
                <asp:TemplateField HeaderText="Inactive" SortExpression="Inactive">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("Inactive") %>' 
                            Enabled="false" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="UserListDataSource" runat="server" SelectMethod="GetUserList"
            TypeName="ITWMaintenance.Library.Users.ReadOnlyUserList">
            <SelectParameters>
                <asp:ControlParameter ControlID="SearchTextBox" Name="SearchText" 
                    PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="ActiveOnlyCheckBox" DefaultValue="True" 
                    Name="ActiveOnly" PropertyName="Checked" Type="Boolean" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>

