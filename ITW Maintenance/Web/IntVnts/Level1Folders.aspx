<%@ Page Title="ITW Maintenance - IntVnts - Level 1 Folders" Language="VB" MasterPageFile="~/IntVnts/IntVntMainMaster.master" AutoEventWireup="false" CodeFile="Level1Folders.aspx.vb" Inherits="IntVnts_Level1Folders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <div style="float: left; padding-bottom: 0px;">
    <h3 class="PageHeading">Interventions</h3>
        <h2 class="PageHeading">Home<sub><font size='0' color='grey'> 0</font></sub></h2>
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
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="EditItem" CommandArgument='<%# Eval("intVntID") %>' />
                    </ItemTemplate>
                    <ItemStyle Width="5%" />
                </asp:TemplateField>
                <asp:BoundField DataField="intVntID" HeaderText="intVntID" ReadOnly="True" SortExpression="intVntID">
                    <ItemStyle Width="5%" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
						<asp:Image ID="Image2" runat="server" ImageUrl="..\Images\folderclosed.gif" /> 
                    </ItemTemplate> 
                    <ItemStyle Width="1%" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate> 
                        <span>Folder Name</span><sup><font size='0' color='grey'> 1</font></sup>
                    </HeaderTemplate>
					<ItemTemplate>
						<asp:Label runat="server" ID="FolderNameLabel" Text='<%# Eval("FolderName")%>' />
					</ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Discipline" HeaderText="Discipline" ReadOnly="True"
                    SortExpression="Discipline">
                </asp:BoundField>
                <asp:BoundField DataField="DisplayOrder" HeaderText="Display Order" ReadOnly="True"
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
        <asp:ObjectDataSource ID="Level1FoldersSource" runat="server" SelectMethod="GetIntVntLevel1FolderList"
            TypeName="ITWMaintenance.Library.Interventions.ReadOnlyIntVntLevel1FolderList">
            <SelectParameters>
                <asp:ControlParameter ControlID="ActiveOnlyCheckBox" DefaultValue="True" Name="ActiveOnly"
                    PropertyName="Checked" Type="Boolean" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>

