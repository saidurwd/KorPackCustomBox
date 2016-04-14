<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePwd.aspx.cs" Inherits="StringEncodeDecode._ChangePwd"
    MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function fun_ShowPassChangeMessage() {
            alert('Password changed successfully. Please click \'ok\' and log on with your new password');
            window.top.location.href = "../Logout.aspx";
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
        .bodyContent {
            margin: 0 auto;
            width: 500px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="panel panel-primary" id="changePassword">
            <div class="panel-heading">
                <h3 class="panel-title">Change Password</h3>
            </div>
            <div class="panel-body form-horizontal">
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2 control-label">
                        <asp:Label ID="lblPartCode" runat="server" Text="Login ID"></asp:Label></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtLoginID" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-sm-4">
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2 control-label">
                        <asp:Label ID="lblPassword" runat="server" Text="Old Password"></asp:Label></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-sm-4">
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2 control-label">
                        <asp:Label ID="Label1" runat="server" Text="New Password"></asp:Label></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-sm-4">
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2 control-label">
                        <asp:Label ID="Label2" runat="server" Text="Conf Password"></asp:Label></label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtConfNewPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-sm-4">
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2 control-label"></label>
                    <div class="col-sm-4">
                        <asp:Label ID="NoUser" runat="server" ForeColor="Red" Text="User does not exist"
                            Visible="False"></asp:Label>
                    </div>
                    <div class="col-sm-4">
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-4">                        
                        <asp:Button ID="btnLogin" runat="server" Text="Update" OnClick="btnLogin_Click" OnClientClick="javascript:return ChkPassword()" CssClass="btn btn-default btn-sm" />
                    </div>
                </div>
                <asp:HiddenField ID="txtHidLoginID" runat="server" />
                <asp:HiddenField ID="hidMenuID" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
