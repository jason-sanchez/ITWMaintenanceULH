<%@ Page Title="ITW Maintenance - Evals - Eval List" Language="VB" MasterPageFile="~/Evals/EvalMainMaster.master" AutoEventWireup="false" CodeFile="Evals.aspx.vb" Inherits="Evals_Evals" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function loadEval(EvalID) {
            window.location.href = "Eval_Landing.aspx?EvalID=" + EvalID + "&EvalActiveOnly=True";
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <div style="float: left; padding-bottom: 0px;">
        <h2 class="PageHeading"><asp:Literal ID="ParentNameLabel" runat="server" /> - Evaluations</h2>
        <asp:Button ID="btnGoBack" runat="server" Text="Go Back" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnAddNew" runat="server" Text="Add New" />
    </div>
    <div style="float: right; clear: right; padding: 10px 0px 0px 0px;">
        <asp:CheckBox id="ActiveOnlyCheckBox" runat="server" Text=" Active Only" TextAlign="Right" 
            AutoPostBack="True" Checked="True" />
    </div>
    <div style="clear: both;">
        <asp:GridView ID="EvaluationsGrid" Runat="server" AutoGenerateColumns="False" SkinID="ClickableGrid" 
            DataSourceID="EvaluationsSource" BorderStyle="None" GridLines="None" Width="100%">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="EditItem" CommandArgument='<%# Eval("EvalID") %>' />
                    </ItemTemplate>
                    <ItemStyle Width="5%" />
                </asp:TemplateField>
                <asp:BoundField DataField="EvalID" HeaderText="EvalID" ReadOnly="True" SortExpression="EvalID">
                </asp:BoundField>
                <asp:BoundField DataField="EvalName" HeaderText="Evaluation Name" ReadOnly="True"
                    SortExpression="EvalName">
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
                <asp:TemplateField HeaderText="One Time">
                    <ItemTemplate>
                        <asp:CheckBox ID="OneTimeCheckBox" runat="server" Enabled="False" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Post Discharge">
                    <ItemTemplate>
                        <asp:CheckBox ID="PostDischargeCheckBox" runat="server" Enabled="False" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="EvaluationsSource" runat="server" SelectMethod="GetEvaluationList"
            TypeName="ITWMaintenance.Library.Evaluations.ReadOnlyEvaluationList">
            <SelectParameters>
                <asp:QueryStringParameter Name="ParentID" QueryStringField="ParentID" Type="Int32" />
                <asp:ControlParameter ControlID="ActiveOnlyCheckBox" DefaultValue="True" Name="ActiveOnly"
                    PropertyName="Checked" Type="Boolean" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>

