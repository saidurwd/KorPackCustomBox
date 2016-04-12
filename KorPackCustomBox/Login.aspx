<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="StringEncodeDecode._Login" ResponseEncoding="Unicode" Title="Korpack: Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="<%=ResolveUrl("~/Style/EnterPriseBlue/SkinFile.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/Style/EnterPriseBlue/jquery-ui-1.8.custom.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/Style/EnterPriseBlue/thickbox.css") %>" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" type="image/x-icon" href="/Images/favicon.ico">
    <style type="text/css">
        .ui-dialog-titlebar-close {
            /* display: none !important;
            visibility: hidden !important;*/
        }

        #login_dialogbox {
            padding-left: 667px;
            padding-top: 159px;
            width: 300px;
            display: none;
        }

            #login_dialogbox .ui-dialog {
                width: 300px;
                height: 176px;
            }

        .cssTableTwo {
            /* height:125px;*/
            margin-top: 20px;
        }

        .loginSelectCompany a {
            text-decoration: none;
        }

            .loginSelectCompany a:hover {
                text-decoration: underline;
            }

        #imgLogin {
            left: 500px;
            position: relative;
            top: 268px;
        }

        .lnkChooseCompany {
            color: white;
            font-weight: bold;
            left: 925px;
            position: relative;
            top: 187px;
        }

        #form2 {
            position: absolute;
            top: 200px;
            right: 100px;
        }
    </style>
    <script type="text/javascript">
        function fromPopUp(valofCompref) {
            //alert(valofCompref);
            $('#<%=HidFromPopUp.ClientID %>').val(decodeURIComponent(valofCompref));
            document.getElementById("form1").submit();

        }

    </script>
    <script src="<%=ResolveUrl("~/Scripts/jquery-1.4.2.min.js") %>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/Scripts/thickbox-compressed.js")%>" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function openWindow() {
            var strFeatures = "dependent=no,resizable=yes,top=340,left=370,Width=525,Height=200,help=no,maximize=no;minimize=yes,scrollbars=no";
            window.open('SelectCompanyLogin.aspx', 'ReportPopUp', strFeatures);
        }
    </script>
</head>
<body class="loginPage">
    <form id="form1" runat="server" defaultbutton="btnLogin">
        <div class="loginBody">
            <asp:Image ID="imgLogin" runat="server" ImageUrl="~/Style/EnterPriseBlue/images/loginbtn.gif"
                Visible="false" />
            <div class="loginTableLogo">
                &nbsp
            </div>
            <div class="loginTable">
                <p>
                    <span style="font-size: 12px; font-weight: bold">User ID: </span>
                    <asp:TextBox ID="txtLoginID" runat="server" Width="200px"></asp:TextBox>
                </p>
                <p>
                    <span style="font-size: 12px; font-weight: bold">Password:</span>
                    <asp:TextBox ID="txtPassword" runat="server" Width="200px" Font-Names="Verdana" Font-Size="9pt"
                        EnableTheming="False" TextMode="Password"></asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="lblLoginFail" runat="server" Text="User Name or Password incorrect "
                        Font-Bold="False" Font-Size="8pt" ForeColor="Orange" Visible="False"></asp:Label>
                </p>
                <p style="text-align: right; padding-right: 35px;">
                    <asp:Button ID="btnLogin" OnClick="btnLogin_Click" runat="server" Text="Login" Visible="false"></asp:Button>
                    <asp:ImageButton runat="server" ID="imgButtom" OnClick="btnLogin_Click" ImageUrl="~/Style/EnterPriseBlue/images/loginscreen_06.png" AlternateText="Login" />
                </p>

            </div>
            <div class="hiddenFields">
                <asp:DropDownList ID="cboSchool" runat="server" Width="100px" Visible="False">
                </asp:DropDownList>
            </div>
            <div class="clearDiv">
            </div>
            <asp:HiddenField ID="HidFromPopUp" runat="server" Value="01" />
            <div style="vertical-align: bottom;text-align:right;padding-right: 35px;">
                <a id="A2" style="color:white" href="RetrievePassword.aspx">Forgot Password?</a>
            </div>
        </div>
        <script src="<%=ResolveUrl("~/Scripts/jquery-ui-1.8.custom.min.js") %>" type="text/javascript"></script>
        <script language="javascript" type="text/javascript">
            $(function () {
                $('input:submit').button();

                $('#<%=imgLogin.ClientID %>').click(function () {
                $('#login_dialogbox').fadeIn('slow');
                $('#txtLoginID').focus();
            });
        });

        </script>
    </form>
</body>
</html>
