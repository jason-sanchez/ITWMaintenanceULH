<%@ Page Title="ITW Maintenance - Nursing - Diagnoses" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="Diagnosis_List.aspx.vb" Inherits="Nursing_Diagnosis_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <div style="float: left; padding-bottom: 10px;">
        <h3 class="PageHeading" style="margin-bottom: 15px;">Nursing Diagnoses</h3>
        <asp:Button ID="btnAddNew" runat="server" Text="Add New" />
    </div>
    <div style="float: right; clear: right; padding: 10px 0px 5px 0px;">
        <asp:CheckBox id="ActiveOnlyCheckBox" runat="server" Text=" Active Only" TextAlign="Right" 
            AutoPostBack="True" Checked="True" />
    </div>
    <div style="clear: both;">
        <asp:GridView ID="DiagnosesGrid" Runat="server" AutoGenerateColumns="False" SkinID="ClickableGrid" 
            DataSourceID="DiagnosesSource" BorderStyle="None" GridLines="None" Width="100%">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="EditItem" CommandArgument='<%# Eval("ID") %>' />
                    </ItemTemplate>
                    <ItemStyle Width="5%" />
                </asp:TemplateField>
                <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID">
                </asp:BoundField>
                <asp:TemplateField HeaderText="Description" SortExpression="Description" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Inactive">
                    <ItemTemplate>
                        <asp:CheckBox ID="InactiveCheckBox" runat="server" Enabled="False" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="DiagnosesSource" runat="server" SelectMethod="GetDiagnosisList"
            TypeName="ITWMaintenance.Library.Nursing.Diagnoses.ReadOnlyDiagnosisList">
            <SelectParameters>
                <asp:ControlParameter ControlID="ActiveOnlyCheckBox" DefaultValue="True" Name="ActiveOnly"
                    PropertyName="Checked" Type="Boolean" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>

