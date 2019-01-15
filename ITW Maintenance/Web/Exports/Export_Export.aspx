<%@ Page Title="ITW Maintenance - Facility Edit" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="Export_Export.aspx.vb" Inherits="Export_Export" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

    <br />

    <h3>
        <asp:Label ID="HeaderParam" runat="server" Text="Please Enter Parameters for the "></asp:Label>
        <asp:Label ID="HeaderName" runat="server"><%# Eval("ExportName")%></asp:Label>
        <asp:Label ID="HeaderEx" runat="server" Text=" Export"></asp:Label>
    </h3>

    <br />
    <asp:panel id="ErrorPanel" runat="server" Visible="false" cssclass="divError" EnableViewState="true" >
            <asp:Label ID="ErrorLabel" runat="server" />
    </asp:panel>
    
    <div class="col-md-12" >
        <asp:FormView ID="EFormView" runat="server" DataSourceID="ExportDataSource" EnableModelValidation="True"  >
            <ItemTemplate>
                <table>
                    <tr>                  
                        <td>
                            <asp:Label ID ="Label1" runat="server" Text='<%# Bind("Param1")%>'  Display="Dynamic"/>&nbsp&nbsp   
                       </td>                                                
                        <td>
                            <asp:TextBox ID="param1Txt" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Required" ControlToValidate ="param1Txt" Display="Dynamic"></asp:RequiredFieldValidator>                           
                       </td>
                   </tr>
                    <tr>
                        <td>
                            <asp:Label ID ="Label2" runat="server" Text='<%# Bind("Param2")%>' Display="Dynamic" />&nbsp&nbsp   
                       </td>
                        <td>
                            <asp:TextBox ID="param2Txt" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*Required" ControlToValidate ="param2Txt" Display="Dynamic"></asp:RequiredFieldValidator>
                       </td>
                   </tr>
                    <tr>
                        <td>
                            <asp:Label ID ="Label3" runat="server" Text='<%# Bind("Param3")%>' Display="Dynamic" />&nbsp&nbsp                               
                       </td>
                        <td>
                            <asp:DropDownList ID="param3Drop" runat="server" DataSourceID="HospSvcDataSource" DataTextField="hospsvc" DataValueField="hospsvc" ></asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*Required" ControlToValidate ="param3Drop" InitialValue="--Select One --" Display="Dynamic"></asp:RequiredFieldValidator>
                       </td>
                   </tr>
                   <tr>
                        <td>
                            <asp:Label ID ="Label4" runat="server" Text='<%# Bind("Param4")%>' Display="Dynamic" />&nbsp&nbsp                               
                       </td>
                        <td>
                            <asp:DropDownList ID="param4Drop" runat="server" DataSourceID="DisciplineDataSource" DataTextField="DisName" DataValueField="DisName" ></asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*Required" ControlToValidate ="param4Drop" InitialValue="--Select One --" Display="Dynamic"></asp:RequiredFieldValidator>
                       </td>
                   </tr>
                </table>
            </ItemTemplate>
        </asp:FormView>

    </div> 
    <br />   
    
     
    

        <div >        
            &nbsp&nbsp&nbsp<asp:Button  ID="backBtn" runat="server" Text="Back" CausesValidation="false" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp

            <asp:Button ID="submitBtn" runat="server" Text="Run Export" OnClientClick="Click()" />
        </div>


        <csla:CslaDataSource ID ="ExportDataSource" runat="server" 
            TypeAssemblyName="" TypeName="ITWMaintenance.Library.Exports.ExportParam, ITWMaintenance.Library"
            TypeSupportsPaging="False" TypeSupportsSorting="False" >
        </csla:CslaDataSource>
    
            <csla:CslaDataSource ID ="HospSvcDataSource" runat="server" 
            TypeAssemblyName="" TypeName="ITWMaintenance.Library.Exports.ExportHospService, ITWMaintenance.Library"
            TypeSupportsPaging="False" TypeSupportsSorting="False" >
        </csla:CslaDataSource>

                <csla:CslaDataSource ID ="DisciplineDataSource" runat="server" 
            TypeAssemblyName="" TypeName="ITWMaintenance.Library.Exports.ExportDiscipline, ITWMaintenance.Library"
            TypeSupportsPaging="False" TypeSupportsSorting="False" >
        </csla:CslaDataSource>


        <script type="text/javascript">

            function Click() {
                if (document.getElementById('<%=ErrorPanel.ClientID%>') != null) 
                document.getElementById('<%=ErrorPanel.ClientID%>').style.visibility = false;
                }

        </script>

</asp:Content>


