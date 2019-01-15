<%@ Page Title="ITW Maintenance - Evals - Level 1 Folders" Language="VB" MasterPageFile="~/Evals/EvalMainMaster.master" AutoEventWireup="false" CodeFile="Level1Folders.aspx.vb" Inherits="Evals_Level1Folders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <div style="float: left; padding-bottom: 0px;">
        <h2 class="PageHeading">Level 1 Folders</h2>
        <asp:Button ID="btnAddNew" runat="server" Text="Add New" />
    </div>
    <div style="float: right; clear: right; padding: 10px 0px 5px 0px;">
        <asp:CheckBox id="ActiveOnlyCheckBox" runat="server" Text=" Active Only" TextAlign="Right" 
            AutoPostBack="True" Checked="True" />
    </div>
    <div style="clear: both;">
        <asp:GridView ID="Level1FoldersGrid" Runat="server" AutoGenerateColumns="False" SkinID="ClickableGrid" 
            DataSourceID="Level1FoldersSource" BorderStyle="None" GridLines="None" Width="100%">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="EditItem" CommandArgument='<%# Eval("EvalID") %>' />
                    </ItemTemplate>
                    <ItemStyle Width="5%" />
                </asp:TemplateField>
                <asp:BoundField DataField="EvalID" HeaderText="EvalID" ReadOnly="True" SortExpression="EvalID">
                </asp:BoundField>
                <asp:BoundField DataField="FolderName" HeaderText="Folder Name" ReadOnly="True"
                    SortExpression="FolderName">
                </asp:BoundField>
                <asp:BoundField DataField="Discipline" HeaderText="Discipline" ReadOnly="True"
                    SortExpression="Discipline">
                </asp:BoundField>
                <asp:BoundField DataField="DisplayOrder" HeaderText="Order" ReadOnly="True"
                    SortExpression="DisplayOrder">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Inactive">
                    <ItemTemplate>
                        <asp:CheckBox ID="InactiveCheckBox" runat="server" Enabled="False" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="Level1FoldersSource" runat="server" SelectMethod="GetEvalLevel1FolderList"
            TypeName="ITWMaintenance.Library.Evaluations.ReadOnlyEvalLevel1FolderList">
            <SelectParameters>
                <asp:ControlParameter ControlID="ActiveOnlyCheckBox" DefaultValue="True" Name="ActiveOnly"
                    PropertyName="Checked" Type="Boolean" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>

