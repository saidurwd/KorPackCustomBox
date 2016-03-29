<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ManageSalesPerson.aspx.cs" Inherits="UA_ManageRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function fromPopUp(val1, val2, val3, val4, val5) {
            $('#<%=HidCID.ClientID %>').val(val1);
            $('#<%=HidCategoryName.ClientID %>').val(val2);
            $('#<%=txtName.ClientID %>').val(val2);
            $('#<%=txtPosition.ClientID %>').val(val3);
            $('#<%=txtEmail.ClientID %>').val(val4);
            $('#<%=txtPhone.ClientID %>').val(val5);
            tb_remove();
        }

        function refreshParent(val1, val2, val3, val4, val5) {
            //alert("val of :" + val);
            //tb_remove();
            fromPopUp(val1, val2, val3, val4, val5);
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
    <div class="container">
        <div id="tabsMain" style="height: 600px">
            <ul>
                <li><a href="#tabs-1">Manage Sales Person</a></li>
            </ul>
            <div id="tabs-1">
                <div class="configurationPage">
                    <table class="cssTableTwo">
                        <tr>
                            <td class="style1">
                                Name:
                            </td>
                            <td class="style2" style="padding-bottom: 2px;">
                                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="ReqFieldCSS"
                                    ControlToValidate="txtName" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="valSaveDB">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                Position:
                            </td>
                            <td class="style2" style="padding-bottom: 2px;">
                                <asp:TextBox ID="txtPosition" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                Email:
                            </td>
                            <td class="style2" style="padding-bottom: 2px;">
                                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="ReqFieldCSS"
                                    ControlToValidate="txtEmail" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="valSaveDB">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                Phone:
                            </td>
                            <td class="style2" style="padding-bottom: 2px;">
                                <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
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
                    <asp:Panel ID="Panel2" CssClass="gridPanelSalary" runat="server" Width="99%" Height="150px"
                        ScrollBars="Auto">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="gridview"
                            AlternatingRowStyle-CssClass="gridviewaltrow" Width="100%" DataKeyNames="SPAutoId"
                            AllowSorting="true" DataSourceID="SqlDataSource1">
                            <Columns>
                                <asp:TemplateField HeaderText="Select" ControlStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkRow" />
                                        <asp:TextBox runat="server" ID="txtKeyField" Text='<%#Eval("SPAutoId") %>' Style="display: none" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name" SortExpression="SPName">
                                    <ItemTemplate>
                                        <a href="javaScript:refreshParent('<%#Eval("SPAutoId") %>','<%#Eval("SPName") %>','<%#Eval("SPPosition") %>','<%#Eval("SPEmail") %>','<%#Eval("SPPhoneNo") %>');">
                                            <%# Eval("SPName")%>
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Position" SortExpression="SPPosition">
                                    <ItemTemplate>
                                        <%# Eval("SPPosition")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email" SortExpression="SPEmail">
                                    <ItemTemplate>
                                        <%# Eval("SPEmail")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Phone" SortExpression="SPPhoneNo">
                                    <ItemTemplate>
                                        <%# Eval("SPPhoneNo")%>
                                    </ItemTemplate>
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
                            SelectCommand="SELECT       SPAutoId, SPName, SPAddress, SPPosition, SPEmail, SPFaxNo, SPPhoneNo, Comments
                        FROM            CB_SalesPerson
                        order by SPName"></asp:SqlDataSource>
                    </div>
                </div>
            </div>
        </div>
        <div class="container">
</asp:Content>
