<%@ Page Title="ITW Maintenance - Export" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="Export_List.aspx.vb" Inherits="Export_List" %>
 <%@Register Assembly="Csla" Namespace="Csla.Web" TagPrefix="csla" %>

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
<%--    <div style="float: left; padding-bottom: 0px;">
        <h3 class="PageHeading" style="margin-bottom: 15px;">Export List</h3>
        <asp:Button ID="AddButton" runat="server" Text="Add Export" />
    </div>--%>
<%--    <div style="float: right; text-align: right;">
        Search: <asp:TextBox ID="SearchTextBox" runat="server" style="width: 200px;" onclick="SearchIfEnter();" /> 
        <input type="button" id="SearchButton" value="Search" onclick="Search();" />
    </div>--%>
<%--    <div style="float: right; clear: right; padding: 10px 0px 5px 0px;">
        Facility Type: <asp:DropDownList ID="FacilityTypeDropDownList" runat="server" AutoPostBack="true" /> 
        <asp:CheckBox id="ActiveOnlyCheckBox" runat="server" Text=" Active Only" TextAlign="Right" 
            AutoPostBack="True" Checked="True" />
    </div>--%>
    <div style="clear: both;">
        <asp:GridView ID="ExportListGridView" runat="server" 
            DataSourceID="ExportDataSource" AutoGenerateColumns="False" Width="100%" 
            SkinID="ClickableGrid" AllowPaging="False" EnableModelValidation="True">
            <EmptyDataTemplate>
                No Facilities Found
            </EmptyDataTemplate>
            <Columns>
<%--                <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" 
                    SortExpression="ID">
                <ItemStyle Width="5%" />
                </asp:BoundField>--%>
               
                <asp:TemplateField>                    
                    <ItemTemplate>
                        <asp:Button ID="ExportButton" runat="server" Text="Export" UseSubmitBehavior="false" />
                    </ItemTemplate>
                    <ItemStyle Width="5%" />
                </asp:TemplateField>

                <asp:BoundField DataField="CollectionName" HeaderText="Export Name" ReadOnly="True" 
                    SortExpression="CollectionName" />
            </Columns>

        </asp:GridView>
<%--        <csla:CslaDataSource ID="ExportDataSource" runat="server" 
            TypeAssemblyName="" TypeName="ITWMaintenance.Library.Exports.ExportList, ITWMaintenance.Library" 
            TypeSupportsPaging="False" TypeSupportsSorting="False" EnableViewState="False">
        </csla:CslaDataSource>--%>
        <csla:CslaDataSource ID ="ExportDataSource" runat="server" 
            TypeAssemblyName="" TypeName="ITWMaintenance.Library.Exports.ExportList, ITWMaintenance.Library"
            TypeSupportsPaging="False" TypeSupportsSorting="False" EnableViewState ="False">
        </csla:CslaDataSource>

    </div>

</asp:Content>