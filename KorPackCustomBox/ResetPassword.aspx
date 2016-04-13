<%@ Page Title="Korpack: Reset Password" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ResetPassword.aspx.cs" Inherits="Sales_DirGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        $().ready(function () {
            // validate signup form on keyup and submit
            $("#aspnetForm").validate({
                rules: {
                    ctl00$ContentPlaceHolder1$txtPassword: {
                        required: true,
                        minlength: 5
                    },
                    ctl00$ContentPlaceHolder1$txtRetypePassword: {
                        required: true,
                        minlength: 5,
                        equalTo: "#ctl00_ContentPlaceHolder1_txtPassword"
                    }
                },

                messages: {
                    ctl00$ContentPlaceHolder1$txtPassword: {
                        required: "Please provide a password",
                        minlength: "Password must be at least 5 characters"
                    },
                    ctl00$ContentPlaceHolder1$txtRetypePassword: {
                        required: "Please provide a password",
                        minlength: "Password must be at least 5 characters",
                        equalTo: "Please enter the same password as above"
                    }
                }
            });

        });
        function fun_ShowPassChangeMessage() {
            alert('Set new password successful. Please click \'ok\' and log on with your new password');
            window.top.location.href = "Logout.aspx";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="row">
            <div class="col-md-9">
                <div class="bs-callout bs-callout-info">
                    <h4>Please enter you email address</h4>
                    <div class="row">
                        <div class="col-md-4" style="text-align: center">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <h4>Please enter new password</h4>
                    <div class="row">
                        <div class="col-md-4" style="text-align: center">
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control input-sm" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <h4>Confirm password</h4>
                    <div class="row">
                        <div class="col-md-4" style="text-align: center">
                            <asp:TextBox ID="txtRetypePassword" runat="server" CssClass="form-control input-sm" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-2" style="text-align: center">
                            <asp:Button ID="btnLogin" runat="server" Text="Set Password" OnClick="btnLogin_Click" />
                        </div>
                    </div>
                </div>
                <br />

            </div>




        </div>


    </div>
    <div style="padding-top: 10px; text-align: left;">
        <div class="buttonSection" style="text-align: left; padding-left: 20px">
        </div>
    </div>
    <br />
    <br />

</asp:Content>
