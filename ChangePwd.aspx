<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePwd.aspx.cs" Inherits="StringEncodeDecode._ChangePwd"
    MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function fun_ShowPassChangeMessage() {
            alert('Password changed successfully. Please click \'ok\' and log on with your new password');
            window.top.location.href = "Logout.aspx";
        }
        function ChkPassword() {
            if ((document.getElementByID("<%=txtNewPassword.ClientID %>").value != "") && (document.getElementByID("<%=txtConfNewPassword.ClientID %>").value != "")) {
                if (document.getElementByID("<%=txtNewPassword.ClientID %>").value == document.getElementByID("<%=txtConfNewPassword.ClientID %>").value) {
                    return true;
                }
                else {
                    alert("New password and confirm password are not same");
                    return false;
                }
            }
            else {
                alert("New password or confirm password can not blank");
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        $().ready(function () {
            // validate signup form on keyup and submit
            $("#aspnetForm").validate({
                rules: {

                    ctl00$ContentPlaceHolder1$txtNewPassword: {
                        required: true,
                        minlength: 5
                    },
                    ctl00$ContentPlaceHolder1$txtConfNewPassword: {
                        required: true,
                        minlength: 5,
                        equalTo: "#ctl00_ContentPlaceHolder1_txtNewPassword"
                    }
                },

                messages: {
                    ctl00$ContentPlaceHolder1$txtNewPassword: {
                        required: "Please provide a password",
                        minlength: "Password must be at least 5 characters"
                    },
                    ctl00$ContentPlaceHolder1$txtConfNewPassword: {
                        required: "Please provide a password",
                        minlength: "Password must be at least 5 characters",
                        equalTo: "Please enter the same password as above"
                    }
                }
            });

        });

    </script>
    <style type="text/css">
        .bodyContent
        {
            margin: 0 auto;
            width: 500px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="changePassword">
        <div id="tabsMain">
            <ul>
                <li><a href="#tabs-1">Change Password</a></li>
            </ul>
            <div id="tabs-1">
                <div class="bodyContent">
                    <div style="float: left; width: 25%;">
                        <asp:Image ID="Image1" Style="padding-top: 20px" runat="server" ImageUrl="~/App_Themes/SkinFile/images/login.jpg" />
                    </div>
                    <div style="float: right; width: 74%;">
                        <table class="cssTableFour">
                            <tr>
                                <td colspan="2" class="topRow">
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    <asp:Label ID="lblPartCode" runat="server" Text="Login ID"></asp:Label>
                                </td>
                                <td class="style2">
                                    <asp:TextBox ID="txtLoginID" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    <asp:Label ID="lblPassword" runat="server" Text="Old Password"></asp:Label>
                                </td>
                                <td class="style2">
                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    <asp:Label ID="Label1" runat="server" Text="New Password"></asp:Label>
                                </td>
                                <td class="style2">
                                    <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    <asp:Label ID="Label2" runat="server" Text="Conf Password"></asp:Label>
                                </td>
                                <td class="style2">
                                    <asp:TextBox ID="txtConfNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr runat="server" visible="false" id="msgrow">
                                <td class="style1">
                                    &nbsp;
                                </td>
                                <td class="style2">
                                    <asp:Label ID="NoUser" runat="server" ForeColor="Red" Text="User does not exist"
                                        Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    <asp:HiddenField ID="txtHidLoginID" runat="server" />
                                </td>
                                <td class="style2">
                                    <asp:Button ID="btnLogin" runat="server" Text="Update" OnClick="btnLogin_Click" OnClientClick="javascript:return ChkPassword()" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="bottomRow">
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="clearDiv">
                    </div>
                    <asp:HiddenField ID="hidMenuID" runat="server" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
