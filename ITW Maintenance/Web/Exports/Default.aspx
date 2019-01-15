<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContentPlaceHolder">
    
    <div>
        <label>Please select which report you would like to run:</label>
        <asp:DropDownList 
            ID="collectionDrop" 
            runat="server" 
            AutoPostBack ="True"
            DataTextField="collectionName" 
            DataValueField ="collectionName" 
            AppendDataBoundItems="True" 
            DataSourceID="SqlDataSource1">          
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ITW %>" SelectCommand="SELECT [collectionName] FROM [105infoCollection]"></asp:SqlDataSource>

        <br />
        <br />
        <!--label1 and textbox1-->
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <%If TextBox1.Enabled = True Then%> 
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*This is a required field!" ControlToValidate="TextBox1"></asp:RequiredFieldValidator>
        <%End If%>           
       
        <asp:RegularExpressionValidator ID="DateValidator1" runat="server" ErrorMessage="The Date must be in MM/DD/YYYY format!" ControlToValidate="TextBox1" ValidationExpression="^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$"></asp:RegularExpressionValidator>                   

        <br />

        <!--label2 and textbox2-->
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <%If TextBox2.Enabled = True Then%> 
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*This is a required field!" ControlToValidate="TextBox2"></asp:RequiredFieldValidator>
        <%End If%>


        <asp:RegularExpressionValidator ID="DateValidator2" runat="server" ErrorMessage="The Date must be in MM/DD/YYYY format!" ControlToValidate="TextBox2" ValidationExpression="^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$"></asp:RegularExpressionValidator>

        <br />

        <!--label3 and textbox3-->
        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <%If TextBox3.Enabled = True Then%> 
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*This is a required field!" ControlToValidate="TextBox3"></asp:RequiredFieldValidator>
        <%End If%>
        <br />

        <br />
        <asp:Button ID="Button1" runat="server" Text="Run Export" onClientClick="daterange();"/>

        <asp:CompareValidator 
            ID="CompareValidator1" 
            runat="server" 
            ErrorMessage="From date must be less than to date!" 
            ControltoValidate="TextBox1" 
            ControlToCompare="TextBox2" 
            Operator="LessThan" 
            Type="Date">
        </asp:CompareValidator>

    </div>

</asp:Content>


