<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrmUserInfo.aspx.cs" MasterPageFile="~/MasterPage.master"
    Inherits="StringEncodeDecode.UserAuthentication_PrmUserInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css" title="currentStyle">
        @import "../media/css/demo_page.css";
        @import "../media/css/demo_table_jui.css";
        @import "../examples_support/themes/smoothness/jquery-ui-1.8.4.custom.css";
    </style>
    <script type="text/javascript" language="javascript" src="../media/js/jquery.dataTables.js"></script>
    <script language="javascript" type="text/javascript">
        function SelectAllUsers(chk) {
            $('#<%=grdEmpInfo.ClientID%>').find("input:checkbox").each(function () {
                if (this != chk) {
                    this.checked = chk.checked;
                }
            });

        }

        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {

                //alert($('#quicksearch').html());
                var qhtml = $('#quicksearch').html();
                //alert($('#quicksearch').html());
                if (qhtml != null) {
                    //alert('remove');
                    $('#quicksearch').remove();
                    $('#<%=grdEmpInfo.ClientID %> tbody tr').quicksearch({
                        loaderText: '...',
                        labelText: 'Filter: '
                    });
                    $('#quicksearch').hide();
                }
            }
            $('#quicksearch').hide();
            $('#<%=grdEmpInfo.ClientID %>').dataTable({
                //                "iDisplayLength": 20,
                //                "aLengthMenu": [[10, 25, 50, 100, -1], [10, 25, 50, 100, "All"]],
                //                //                 "sPaginationType": "full_numbers"
                "bJQueryUI": true,
                //                "sPaginationType": "full_numbers",
                //"bStateSave": true,

                "sScrollY": "425px",
                "bPaginate": false,
                "bScrollCollapse": true,
                "aoColumnDefs": [{ "bSortable": false, "aTargets": [5]}]
            });

        }
    </script>
    <style type="text/css">
        select
        {
            width: 60px;
        }
        
        input[type="text"], input[type="password"]
        {
            width: 100px;
        }
        
        .nopad
        {
            padding-left: 2px;
            padding-right: 10px !important;
        }
    </style>
    <script src="CreateUser.js" type="text/javascript"></script>
    <script type="text/javascript">
        $().ready(function () {

            // validate signup form on keyup and submit
            $("#aspnetForm").validate({
                rules: {
                    ctl00$ContentPlaceHolder1$txtLoginName: {
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
                    ctl00$Content$txtEmail: {
                        required: true,
                        minlength: 5,
                        email: true,
                        equalTo: "#ctl00_Content_txtEmail"
                    }
                },

                messages: {
                    ctl00$ContentPlaceHolder1$txtLoginName: {
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
    <style type="text/css">
        myDiv
        {
            border: 2px solid #0094ff;
            -webkit-border-top-left-radius: 6px;
            -webkit-border-top-right-radius: 6px;
            -moz-border-radius-topleft: 6px;
            -moz-border-radius-topright: 6px;
            border-top-left-radius: 6px;
            border-top-right-radius: 6px;
            width: 300px;
            font-size: 12pt; /* or whatever */
        }
        
        .myDiv h2
        {
            padding: 4px;
            color: #fff;
            margin: 0;
            background-color: #0094ff;
            font-size: 12pt; /* or whatever */
        }
        
        .myDiv p
        {
            padding: 4px;
        }
    </style>
    <div id="loading">
        <div class="loading-indicator">
            Page Loading...
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="row">
            <div style="text-align: right; width: 100%">
                <a id="A1" class="thickbox specialLink" href="NewUser.aspx?TB_iframe=true&height=480&width=700&modal=true"
                    style="padding: 2px 5px 2px 5px; font-size: 12px;">Create New User</a>
            </div>
        </div>
    </div>
    <div class="container">
        <div class="configurationPage">
            <div class="myDiv" style="display: none">
                <h2>
                    Div Title</h2>
                <p>
                    Div content.
                </p>
            </div>
            <table class="cssTableTwo" style="display: none">
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
                    <td colspan="2" class="bottomRow">
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                    </td>
                    <td class="style2">
                        <asp:Button ID="btn_update" runat="server" Text="Save" ValidationGroup="valSaveDB"
                            OnClick="btn_update_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div class="cleardiv">
        </div>
        <div class="dataGridStyle">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grdEmpInfo" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="grdEmpInfo_RowCancelingEdit"
                        OnRowEditing="grdEmpInfo_RowEditing" OnRowUpdating="grdEmpInfo_RowUpdating" EnableViewState="True"
                        CssClass="gridview display" AlternatingRowStyle-CssClass="gridviewaltrow" OnRowDataBound="grdEmpInfo_RowDataBound"
                        Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Name of User">
                                <ItemTemplate>
                                    <asp:Label ID="grdlblLastName" runat="server" Text='<%# Bind("FullName") %>'></asp:Label>
                                    <asp:TextBox ID="txtLoginID" runat="server" Text='<%# Eval("LoginID") %>' Style="display: none"></asp:TextBox>
                                    <asp:TextBox ID="txtPassword" runat="server" Text='<%# Eval("Password") %>' Style="display: none"></asp:TextBox>
                                    <asp:Label ID="lblUserStatus" runat="server" Text='<%# Bind("UserStatus") %>' Style="display: none"></asp:Label>
                                    <asp:TextBox ID="txtUserID" runat="server" Text='<%# Eval("UserID") %>' Style="display: none"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="15%" HorizontalAlign="Left" />
                                <HeaderStyle Width="15%" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email">
                                <ItemTemplate>
                                    <asp:Label ID="grdlblDepartment" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="ReqFieldCSS"
                                        ControlToValidate="txtGrdEditEmail" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="valSaveDB">*</asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtGrdEditEmail" runat="server" Text='<%# Bind("Email") %>' Width="99%"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle Width="15%" HorizontalAlign="Left" />
                                <HeaderStyle Width="15%" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Role">
                                <ItemTemplate>
                                    <asp:Label ID="grdlblRole" runat="server" Text='<%# Bind("RoleDesc") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="cboGrdRole" runat="server" DataSourceID="SqlDataSource1" DataTextField="RoleDesc"
                                        DataValueField="RoleID" Width="98%" SelectedValue='<%# Bind("RoleID") %>'>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemStyle Width="15%" HorizontalAlign="Left" />
                                <HeaderStyle Width="15%" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="For Customer">
                                <ItemTemplate>
                                    <asp:Label ID="grdlblCustomer" runat="server" Text='<%# Bind("CustomersCId") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlGrdEditCustomer" runat="server" DataSourceID="SqlDataSource2"
                                        DataTextField="CustomersCId" DataValueField="CustomersId" Width="98%" SelectedValue='<%# Bind("CustomerId") %>'>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemStyle Width="15%" HorizontalAlign="Left" />
                                <HeaderStyle Width="15%" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblLoginID" runat="server" Text='<%# Bind("UserStatus") %>'></asp:Label>
                                    <asp:Label ID="lblRoleDesc" runat="server" Text='<%# Bind("RoleDesc") %>' Visible="False"></asp:Label>
                                    <asp:Label ID="lblRoleID" runat="server" Text='<%# Bind("RoleID") %>' Visible="False"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblPrvLoginID" runat="server" Text='<%# Bind("LoginID") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemStyle Width="10%" HorizontalAlign="Left" />
                                <HeaderStyle Width="10%" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Login Info">
                                <ItemTemplate>
                                    <asp:Label ID="lblCurrLoginID" runat="server" Text='<%# Bind("LoginID") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtGrdPassOLD" Visible="false" runat="server" Text='<%# Bind("Password") %>'
                                        Width="94%"></asp:TextBox>
                                    <asp:TextBox ID="txtGrdConfPassOLD" Visible="false" runat="server" Text='<%# Bind("Password") %>'
                                        Width="94%"></asp:TextBox>
                                    <table width="100%">
                                        <tr>
                                            <td width="30%" bgcolor="silver" align="left">
                                                Name of User
                                            </td>
                                            <td width="70%" align="left">
                                                <asp:TextBox ID="txtGridFullName" runat="server" Text='<%# Eval("FullName") %>' Width="94%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" bgcolor="silver" align="left">
                                                Login ID
                                            </td>
                                            <td width="70%" align="left">
                                                <asp:TextBox ID="txtGrdLoginID" runat="server" Text='<%# Bind("LoginID") %>' Width="94%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" bgcolor="silver" align="left">
                                                Password
                                            </td>
                                            <td width="70%" align="left">
                                                <asp:TextBox ID="txtGrdPass" runat="server" TextMode="Password" Text="..." Width="94%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="30%" bgcolor="silver" align="left">
                                                Conf. Password
                                            </td>
                                            <td width="70%" align="left">
                                                <asp:TextBox ID="txtGrdConfPass" runat="server" TextMode="Password" Width="94%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>
                                <ItemStyle Width="30%" HorizontalAlign="Left" />
                                <HeaderStyle Width="30%" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="cboUserActive" runat="server" SelectedValue='<%# Bind("UserActive") %>'>
                                        <asp:ListItem Selected="True" Value="True">Active</asp:ListItem>
                                        <asp:ListItem Value="False">Inactive</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblUserActive" runat="server" Visible="false" Text='<%# Bind("UserActive") %>'></asp:Label>
                                    <asp:Label ID="lblgrdActiveDeactive" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                            </asp:TemplateField>
                            <asp:CommandField ButtonType="Image" CancelImageUrl="~/App_Themes/SkinFile/images/notclose.gif"
                                EditImageUrl="~/App_Themes/SkinFile/images/edit.png" HeaderText="Action" ShowEditButton="True"
                                UpdateImageUrl="~/App_Themes/SkinFile/images/done.gif">
                                <HeaderStyle Font-Names="Verdana" Font-Size="X-Small" />
                            </asp:CommandField>
                        </Columns>
                        <EmptyDataTemplate>
                            <span class="gridviewEmptyMsg">
                                <%= Resources.Resource.GridViewEmptyDataMsg %>
                            </span>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="space-10"></div>
    </div>
    <div id="allHiddenField">
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Cnn %>"
            SelectCommand="SELECT RoleID, RoleDesc FROM (SELECT RoleID, RoleDesc,company_id FROM TB_PrmUserRole) AS TBL ORDER BY RoleDesc">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Cnn %>"
            SelectCommand="SELECT 0 CustomersId, '' CustomersCId 
            union all
            SELECT CustomersId, CustomersCId FROM CB_Customers ORDER BY CustomersCId"></asp:SqlDataSource>
        <asp:HiddenField ID="txtEmployeeID" runat="server" Value="0" />
        <asp:HiddenField ID="txtUserID0" runat="server" Value="0" />
        <asp:HiddenField ID="txtSelectedEmpID" runat="server" Value="0" />
        <asp:HiddenField ID="hidMenuID" runat="server" />
    </div>
</asp:Content>
