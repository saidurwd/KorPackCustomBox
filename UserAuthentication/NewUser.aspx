<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewUser.aspx.cs" MasterPageFile="~/MasterPageDummy.master"
    Inherits="StringEncodeDecode.UserAuthentication_NewUser"%>
    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="CreateUser.js" type="text/javascript"></script>
    <script type="text/javascript">
        $().ready(function () {
            // validate signup form on keyup and submit
            $("#aspnetForm").validate({
                rules: {
                    ctl00$ContentPlaceHolder1$Login: {
                        required: true,
                        minlength: 3
                    },
                    ctl00$ContentPlaceHolder1$txtPassword: {
                        required: true,
                        minlength: 5
                    },
                    ctl00$ContentPlaceHolder1$txtRetypePassword: {
                        required: true,
                        minlength: 5,
                        equalTo: "#ctl00_ContentPlaceHolder1_txtPassword"
                    },
                    ctl00$ContentPlaceHolder1$txtEmail: {
                        required: true,
                        minlength: 5,
                        email: true,
                        equalTo: "#ctl00_ContentPlaceHolder1_txtEmail"
                    }
                },

                messages: {
                    ctl00$ContentPlaceHolder1$Login: {
                        required: "Please provide a login name",
                        minlength: "Login name must be at least 3 characters"
                    },
                    ctl00$ContentPlaceHolder1$txtPassword: {
                        required: "Please provide a password",
                        minlength: "Password must be at least 5 characters"
                    },
                    ctl00$ContentPlaceHolder1$txtRetypePassword: {
                        required: "Please provide a password",
                        minlength: "Password must be at least 5 characters",
                        equalTo: "Please enter the same password as above"
                    },
                    ctl00$ContentPlaceHolder1$txtEmail: {
                        required: "Please provide a Email id",
                        minlength: "Email id must be at least 5 characters"
                    }
                }
            });

        });

    </script>
    <script type="text/javascript">
        tb_remove();
    </script>
    <div id="loading">
        <div class="loading-indicator">
            Page Loading...
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="tabsMain" style="height:400px">
        <ul>
            <li><a href="#tabs-1">Create New User</a></li>
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
                            Name of User:
                        </td>
                        <td class="style2">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="ReqFieldCSS"
                                ControlToValidate="NameofUser" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="valSaveDB">*</asp:RequiredFieldValidator>
                            <asp:TextBox ID="NameofUser" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Email ID:
                        </td>
                        <td class="style2">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="ReqFieldCSS"
                                ControlToValidate="txtEmail" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="valSaveDB">*</asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Login ID:
                        </td>
                        <td class="style2">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="ReqFieldCSS"
                                ControlToValidate="Login" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="valSaveDB">*</asp:RequiredFieldValidator>
                            <asp:TextBox ID="Login" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Password
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" TabIndex="17"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Confirm Password
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtRetypePassword" runat="server" TextMode="Password" TabIndex="18"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            User Role:
                        </td>
                        <td class="style2">
                            <asp:DropDownList ID="cboLoginRole" runat="server" DataSourceID="SqlDataSource1"
                                DataTextField="RoleDesc" DataValueField="RoleID">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            For Customer:
                        </td>
                        <td class="style2">
                            <asp:DropDownList ID="ddlCustomer" runat="server" DataSourceID="SqlDataSource2"
                                DataTextField="CustomersCId" DataValueField="CustomersId">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="bottomRow">
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                        </td>
                        <td class="style2">
                            <input type="button" value="Close" onclick="self.parent.tb_remove(); self.parent.location.href = 'PrmUserInfo.aspx';" />
                            <asp:Button ID="btn_update" runat="server" Text="Save" ValidationGroup="valSaveDB"
                                OnClick="btn_update_Click" />

                                <%--<!-- Code for Main Window link -->
<a href="#" onclick="tb_open_new('window_one.html?TB_iframe=true&height=400&width=400&modal=true')">Open Thickbox without href attribute</a>

<!-- Code of Window One buttons-->
<input type="button" value="Close & Open - Window Two" onclick="self.parent.tb_open_new('window_two.html?TB_iframe=true&height=400&width=500&modal=true')" />
<input type="button" value="Close" onclick="self.parent.tb_remove();" />

<!-- Code of Window Two buttons -->
<input type="button" value="Close & Open - Window One" onclick="self.parent.tb_open_new('window_one.html?TB_iframe=true&height=400&width=400&modal=true')" />
<input type="button" value="Close & Call Parent/Main Window function" onclick="self.parent.tb_remove('show_code();');" />--%>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div id="allHiddenField">
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Cnn %>"
            SelectCommand="SELECT RoleID, RoleDesc FROM (SELECT RoleID, RoleDesc,company_id FROM TB_PrmUserRole) AS TBL ORDER BY RoleDesc">
            <SelectParameters>
                <asp:SessionParameter Name="COMPANY_ID" SessionField="CompanyID" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Cnn %>"
            SelectCommand="SELECT 0 CustomersId, '' CustomersCId 
            union all
            SELECT CustomersId, CustomersCId FROM CB_Customers ORDER BY CustomersCId">
            
        </asp:SqlDataSource>
        <asp:HiddenField ID="txtEmployeeID" runat="server" Value="0" />
        <asp:HiddenField ID="txtUserID0" runat="server" Value="0" />
        <asp:HiddenField ID="txtSelectedEmpID" runat="server" Value="0" />
        <asp:HiddenField ID="hidMenuID" runat="server" />
    </div>
</asp:Content>
