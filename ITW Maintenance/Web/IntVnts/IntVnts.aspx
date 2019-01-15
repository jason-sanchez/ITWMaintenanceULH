<%@ Page Title="ITW Maintenance - IntVnts - Intervention List" Language="VB" MasterPageFile="~/IntVnts/IntVntMainMaster.master" AutoEventWireup="false" CodeFile="IntVnts.aspx.vb" Inherits="IntVnts_IntVnts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function loadIntVnt(intVntID) {
            window.location.href = "IntVnt_Landing.aspx?intVntID=" + intVntID + "&IntVntActiveOnly=True";
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <div style="float: left; padding-bottom: 0px;">
        <h3 class="PageHeading"><asp:Literal ID="ParentDisciplineLabel" runat="server" /></h3>
        <h2 class="PageHeading"><a href="Level1Folders.aspx">Home<sub><font size='0' color='grey'> 0</font></sub></a> - <asp:Literal ID="ParentNameLabel" runat="server" /></h2>
        <asp:Button ID="btnGoBack" runat="server" Text="Go Back" />&nbsp;&nbsp;&nbsp;
        <%--<asp:Button ID="btnAddNew" runat="server" Text="Add New" />--%>
        <asp:Button ID="NewSubFormButton" runat="server" CausesValidation="false" Text="Add New Form" OnClick="NewSubFormButton_Click" />&nbsp;&nbsp;
        <asp:Button ID="NewSubFolderButton" runat="server" CausesValidation="false" Text="Add New Folder" OnClick="NewSubFolderButton_Click" /></td>
    </div>
    <div style="float: right; clear: right; padding: 10px 0px 0px 0px;">
        <asp:CheckBox id="ActiveOnlyCheckBox" runat="server" Text=" Active Only" TextAlign="Right" 
            AutoPostBack="True" Checked="True" />
    </div>
    <div style="clear: both;">
        <asp:GridView ID="InterventionsGrid" Runat="server" AutoGenerateColumns="False" SkinID="ClickableGrid" 
            DataSourceID="InterventionsSource" BorderStyle="solid" GridLines="Both" Width="100%">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="EditItem" CommandArgument='<%# Eval("intVntID") %>' />
                    </ItemTemplate>
                    <ItemStyle Width="5%" />
                </asp:TemplateField>
                <asp:BoundField DataField="intVntID" HeaderText="IntVntID" ReadOnly="True" SortExpression="intVntID">
                    <ItemStyle Width="5%" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
						<asp:Image ID="Image1" runat="server" ImageUrl="..\Images\icodoc4.gif" Visible=<%# Eval("IsForm")%> /> 
						<asp:Image ID="Image2" runat="server" ImageUrl="..\Images\folderclosed.gif" Visible=<%# Not Eval("IsForm") %> /> 
                    </ItemTemplate> 
                    <ItemStyle Width="1%" />
                </asp:TemplateField>
                <asp:TemplateField SortExpression="IntVntName">
                    <HeaderTemplate> 
                         <span>Intervention Name</span><sup><font size='0' color='grey'> 2</font></sup></h2>
                    </HeaderTemplate>
					<ItemTemplate>
						<asp:Label runat="server" ID="IntVntNameLabel" Text='<%# Eval("IntVntName")%>' />
					</ItemTemplate>
                    <ItemStyle Width="69%" />
                </asp:TemplateField>
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
                <asp:TemplateField HeaderText="One Time">
                    <ItemTemplate>
                        <asp:CheckBox ID="OneTimeCheckBox" runat="server" Enabled="False" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Billing">
                    <ItemTemplate>
                        <asp:CheckBox ID="BillingCheckBox" runat="server" Enabled="False" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                

        <%--<asp:Image visible='<%# Convert.ToBoolean(Bind("IntVntForm")) %>'--%>


            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="InterventionsSource" runat="server" SelectMethod="GetInterventionList"
            TypeName="ITWMaintenance.Library.Interventions.ReadOnlyInterventionList">
            <SelectParameters>
                <asp:QueryStringParameter Name="ParentID" QueryStringField="ParentID" Type="Int32" />
                <asp:ControlParameter ControlID="ActiveOnlyCheckBox" DefaultValue="True" Name="ActiveOnly"
                    PropertyName="Checked" Type="Boolean" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>

