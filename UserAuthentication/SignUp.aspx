<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignUp.aspx.cs" MasterPageFile="~/MasterPageSignUp.master"
    Inherits="StringEncodeDecode.UserAuthentication_NewUser"%>
    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="CreateUser.js" type="text/javascript"></script>
    <script type="text/javascript">
        $().ready(function () {
            // validate signup form on keyup and submit
            $("#aspnetForm").validate({
                rules: {
                    ctl00$ContentPlaceHolder1$txtCompanyName: {
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
                    ctl00$ContentPlaceHolder1$txtCompanyName: {
                        required: "Please provide your company name",
                        minlength: "Company name must be at least 3 characters"
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
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="tabsMain" style="height:270px">
        <ul>
            <li><a href="#tabs-1">Sign up</a></li>
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
                            Your Name:
                        </td>
                        <td class="style2">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="ReqFieldCSS"
                                ControlToValidate="NameofUser" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="valSaveDB">*</asp:RequiredFieldValidator>
                            <asp:TextBox ID="NameofUser" runat="server" TabIndex="1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Company Name:
                        </td>
                        <td class="style2">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="ReqFieldCSS"
                                ControlToValidate="txtCompanyName" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="valSaveDB">*</asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtCompanyName" runat="server" TabIndex="2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Email ID:
                        </td>
                        <td class="style2">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="ReqFieldCSS"
                                ControlToValidate="txtEmail" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="valSaveDB">*</asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtEmail" runat="server" TabIndex="3"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Password
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" TabIndex="4" style="font-size:16px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Confirm Password
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtRetypePassword" runat="server" TextMode="Password" TabIndex="5" style="font-size:16px"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td class="style1">
                            Verification code
                        </td>
                        <td class="style2">
                            <asp:Image ID="imgCaptcha" runat="server" Height="25px" Width="156px"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Please Verify
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtVerificationCode" runat="server" TabIndex="6"></asp:TextBox>
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
                            <input type="button" value="Close" onclick="self.parent.tb_remove();" />
                            <asp:Button ID="btn_update" runat="server" Text="Sign up" ValidationGroup="valSaveDB"
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
        <asp:HiddenField ID="txtEmployeeID" runat="server" Value="0" />
        <asp:HiddenField ID="txtUserID0" runat="server" Value="0" />
        <asp:HiddenField ID="txtSelectedEmpID" runat="server" Value="0" />
        <asp:HiddenField ID="hidMenuID" runat="server" />
    </div>
</asp:Content>
