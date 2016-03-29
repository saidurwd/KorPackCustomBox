<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ManageRole.aspx.cs" Inherits="UA_ManageRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script language="javascript" type="text/javascript">
        function fromPopUp(val1, val2) {
            //            alert(val1);
            //            alert(val2);

            $('#<%=HidCID.ClientID %>').val(val1);
            $('#<%=HidCategoryName.ClientID %>').val(val2);
            $('#<%=txtDescription.ClientID %>').val(val2);
            tb_remove();
        }

        function refreshParent(val1, val2) {
            //alert("val of :" + val);
            //tb_remove();
            fromPopUp(val1, val2);
        }
        function ShowConfirmation() {

            var Returnval = false;
            var answer = confirm("Are you sure to delete?")
            if (answer) {
                Returnval = true;
            }
            else {
                Returnval = false;
            }
            return Returnval;
        }
    </script>
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="tabsMain" style="height: 400px">
        <ul>
            <li><a href="#tabs-1">Manage Role</a></li>
        </ul>
        <div id="tabs-1">
            <div class="configurationPage">
                <table class="cssTableTwo">
                    <tr>
                        <td colspan="2" class="topRow">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Role:
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="ReqFieldCSS"
                                ControlToValidate="txtDescription" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="valSaveDB">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            &nbsp;
                        </td>
                        <td class="style2">
                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="bottomRow">
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            &nbsp;
                        </td>
                        <td class="style2">
                            <asp:Button ID="btnUpdate" runat="server" Text="Save" OnClick="btnUpdate_Click" ValidationGroup="valSaveDB" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="bottomRow">
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Panel ID="Panel2" CssClass="gridPanelSalary" runat="server" Width="99%" Height="230px" ScrollBars="Auto">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="gridview"
                        AlternatingRowStyle-CssClass="gridviewaltrow" Width="100%" DataKeyNames="RoleID"
                        AllowSorting="true" DataSourceID="SqlDataSource1">
                        <Columns>
                            <asp:TemplateField HeaderText="Select" ControlStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkRow" />
                                    <asp:TextBox runat="server" ID="txtKeyField" Text='<%#Eval("RoleID") %>' Style="display: none" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name" SortExpression="RoleDesc">
                                <ItemTemplate>
                                    <a href="javaScript:refreshParent('<%#Eval("RoleID") %>','<%#Eval("RoleDesc") %>');">
                                        <%# Eval("RoleDesc")%>
                                    </a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="95%" />
                                <HeaderStyle HorizontalAlign="Left" Width="95%" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                <div style="padding-top: 10px; text-align: left;">
                    <div class="buttonSection" style="text-align: left; padding-left: 20px">
                        <asp:Button ID="Button2" runat="server" Text="Delete" OnClientClick="return ShowConfirmation();"
                            OnClick="cmdDelete_Click" />
                    </div>
                </div>
                <div class="hiddenFields">
                    <asp:HiddenField ID="HidCID" runat="server" />
                    <asp:HiddenField ID="HidCategoryName" runat="server" />
                    <asp:HiddenField ID="hidMenuID" runat="server" />
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Cnn %>"
                        SelectCommand="SELECT     RoleID, RoleDesc
                    FROM         [dbo].[TB_PrmUserRole]
                    where COMPANY_ID='01'
            order by RoleDesc">
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
