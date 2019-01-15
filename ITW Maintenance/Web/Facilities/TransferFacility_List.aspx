<%@ Page Title="ITW Maintenance - Facility Maintenance" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="TransferFacility_List.aspx.vb" Inherits="Facilities_TransferFacility_List" %>

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
        <h3 class="PageHeading" style="margin-bottom: 15px;">Facility Maintenance</h3>
        <asp:Button ID="AddButton" runat="server" Text="Add Facility" />
    </div>
    <div style="float: right; text-align: right;">
        Search: <asp:TextBox ID="SearchTextBox" runat="server" style="width: 200px;" onkeypress="SearchIfEnter();" /> 
        <input type="button" id="SearchButton" value="Search" onclick="Search();" />
    </div>
    <div style="float: right; clear: right; padding: 10px 0px 5px 0px;">
        Facility Type: <asp:DropDownList ID="FacilityTypeDropDownList" runat="server" AutoPostBack="true" /> 
        <asp:CheckBox id="ActiveOnlyCheckBox" runat="server" Text=" Active Only" TextAlign="Right" 
            AutoPostBack="True" Checked="True" />
    </div>
    <div style="clear: both;">
        <asp:GridView ID="FacilityListGridView" runat="server" 
            DataSourceID="TransferFacilityListDataSource" AutoGenerateColumns="False" Width="100%" 
            SkinID="ClickableGrid">
            <EmptyDataTemplate>
                No Facilities Found
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="EditButton" runat="server" Text="Edit" UseSubmitBehavior="false" />
                    </ItemTemplate>
                    <ItemStyle Width="5%" />
                </asp:TemplateField>
                <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" 
                    SortExpression="ID">
                <ItemStyle Width="5%" />
                </asp:BoundField>
                <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" 
                    SortExpression="Name" />
                <asp:BoundField DataField="ContactName" HeaderText="Contact" ReadOnly="True" 
                    SortExpression="ContactName" />
                <asp:BoundField DataField="Phone" HeaderText="Phone" ReadOnly="True" 
                    SortExpression="Phone" />
                <asp:TemplateField HeaderText="Transfer Facility" SortExpression="TransferFacility">
                    <ItemTemplate>
                        <asp:CheckBox ID="TransferFacilityCheckBox" runat="server" Checked='<%# Bind("TransferFacility") %>' 
                            Enabled="false" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Outpatient Therapy" SortExpression="OutpatientTherapy">
                    <ItemTemplate>
                        <asp:CheckBox ID="OutpatientTherapyCheckBox" runat="server" Checked='<%# Bind("OutpatientTherapy") %>' 
                            Enabled="false" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Acute Facility" SortExpression="AcuteFacility">
                    <ItemTemplate>
                        <asp:CheckBox ID="AcuteFacilityCheckBox" runat="server" Checked='<%# Bind("AcuteFacility") %>' 
                            Enabled="false" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Inactive" SortExpression="Inactive">
                    <ItemTemplate>
                        <asp:CheckBox ID="InactiveCheckBox" runat="server" Checked='<%# Bind("Inactive") %>' 
                            Enabled="false" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <csla:CslaDataSource ID="TransferFacilityListDataSource" runat="server" 
            TypeAssemblyName="" TypeName="ITWMaintenance.Library.Facilities.ReadOnlyTransferFacilityList, ITWMaintenance.Library" 
            TypeSupportsPaging="False" TypeSupportsSorting="False">
        </csla:CslaDataSource>
    </div>
</asp:Content>