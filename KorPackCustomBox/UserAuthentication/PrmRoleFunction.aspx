<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrmRoleFunction.aspx.cs"
    MasterPageFile="~/MasterPage.master" Inherits="UserAuthentication_PrmUserFunction"
    Title="Assign User Authentication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css" title="currentStyle">
        @import "../media/css/demo_page.css";
        @import "../media/css/demo_table_jui.css";
        @import "../examples_support/themes/smoothness/jquery-ui-1.8.4.custom.css";
    </style>
    <script type="text/javascript" language="javascript" src="../media/js/jquery.dataTables.js"></script>
    <style type="text/css">
        .treeDefault
        {
            background-color: #F2EEEE;
        }
        .treeDefault a
        {
            padding-left: 5px;
        }
        .leftPanle
        {
            float: left;
            width: 30%;
        }
        .rightPanel
        {
            float: right;
            width: 98%;
        }
        #quicksearch
        {
            font-weight: bold;
            left: 236px;
            margin-bottom: 5px;
            margin-top: 5px;
            position: absolute;
            top: 305px;
            width: 500px;
            z-index: 2;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function fnSave() {
            if (document.getElementById("<%=cboUserDesc.ClientID %>").value > 0) {
                return true;
            }
            else {
                alert("Pleast select an user first from dropdown");
                return false;
            }
        }
        function SelectAllCheckboxes(chk) {
            $('#<%=grdUserFunction.ClientID %>').find("input:checkbox").each(function () {
                if (this != chk) {
                    this.checked = chk.checked;
                }
            });

        }
    </script>
    <script language="javascript" type="text/javascript">
        function SelectAllUsers(chk) {
            $('#<%=grdUserFunction.ClientID%>').find("input:checkbox").each(function () {
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
                    $('#<%=grdUserFunction.ClientID %> tbody tr').quicksearch({
                        loaderText: '...',
                        labelText: 'Filter: '
                    });
                    $('#quicksearch').hide();
                }
            }
            $('#quicksearch').hide();
            $('#<%=grdUserFunction.ClientID %>').dataTable({
                //                "iDisplayLength": 20,
                //                "aLengthMenu": [[10, 25, 50, 100, -1], [10, 25, 50, 100, "All"]],
                //                //                 "sPaginationType": "full_numbers"
                "bJQueryUI": true,
                //                "sPaginationType": "full_numbers",
                //"bStateSave": true,

                "sScrollY": "385px",
                "bPaginate": false,
                "bScrollCollapse": true,
                "aoColumnDefs": [{ "bSortable": false, "aTargets": [3, 4, 5]}]
            });



            //To Fix header
            //             "sScrollY": "200px",
            //        "bPaginate": false,
            //        "bScrollCollapse": true
        }
    </script>
    <style type="text/css">
        .nopad
        {
            padding-left: 2px;
            padding-right: 10px !important;
        }
         input[type="text"], input[type="password"]
        {
            width: 100px;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="tabsMain">
        <ul>
            <li><a href="#tabs-1">Menu Level Security by Role</a></li>
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
                            <asp:DropDownList ID="cboUserDesc" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="cboUserDesc_SelectedIndexChanged"
                                AutoPostBack="true" DataSourceID="SDSUserInfo" DataTextField="RoleDesc" DataValueField="RoleID">
                                <asp:ListItem Value="0">&lt;Select Role&gt;</asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SDSUserInfo" runat="server" ConnectionString="<%$ AppSettings:Cnn %>"
                                SelectCommand="SELECT [RoleID], [RoleDesc] FROM [TB_PrmUserRole] Where Company_ID = @CompanyID">
                                <SelectParameters>
                                    <asp:SessionParameter DbType="String" DefaultValue="0" SessionField="CompanyID" Name="CompanyID" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Module:
                        </td>
                        <td class="style2">
                            <asp:DropDownList ID="ddlModule" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlModule_SelectedIndexChanged"
                                AutoPostBack="true" DataSourceID="sdsModule" DataTextField="Module" DataValueField="funcid">
                                <asp:ListItem Value="0">&lt;Select Module&gt;</asp:ListItem>
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
                            <asp:SqlDataSource ID="sdsModule" runat="server" ConnectionString="<%$ AppSettings:Cnn %>"
                                SelectCommand="SELECT  distinct (select funcid from TB_PrmFunction PF2 where PF2.FuncDesc=PF.Module) funcid, 
Module FROM TB_PrmFunction PF WHERE COMPANY_ID=@CompanyID and MenuEnableDisable=1 and MenuInUse=1 and Module is not null order by Module">
                                <SelectParameters>
                                    <asp:SessionParameter DbType="String" DefaultValue="0" SessionField="CompanyID" Name="CompanyID" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="ui-state-default ui-corner-all action-button"
                                EnableTheming="false" OnClick="mloadPre" Visible="false"><span class="ui-icon ui-icon-search"></span>Search</asp:LinkButton>
                            <asp:Button ID="btn_update" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="javascript:return fnSave();" />
                            <%--<asp:LinkButton ID="btnSave" runat="server" CssClass="ui-state-default ui-corner-all action-button"
                                OnClick="btnSave_Click" OnClientClick="javascript:return fnSave();" EnableTheming="false"
                                Style="float: left;">
                                        Save
                            </asp:LinkButton>--%>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="cleardiv">
            </div>
            <div class="dataGridStyle">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="grdUserFunction" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="grdUserFunction_PageIndexChanging"
                            CssClass="gridview  display" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Function Description">
                                    <ItemStyle HorizontalAlign="Left" Width="40%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="40%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblFuncID" runat="server" Text='<%# Bind("FuncID") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblParentID" runat="server" Text='<%# Bind("ParentID") %>' Visible="False"></asp:Label>
                                        <asp:Label ID="lblFuncDesc" runat="server" Text='<%# Bind("FuncDesc") %>' Visible="False"></asp:Label>
                                        <%# Eval("FuncDesc") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Function Of">
                                    <ItemStyle HorizontalAlign="Left" Width="25%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="25%" />
                                    <ItemTemplate>
                                        <%# Eval("ParentDesc") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Module">
                                    <ItemStyle HorizontalAlign="Left" Width="20%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                    <ItemTemplate>
                                        <%# Eval("Module")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Add">
                                   <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkRowAdd" runat="server" Checked='<%# Bind("PerAdd") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkRowEdit" runat="server" Checked='<%# Bind("PerEdit") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkRowDelete" runat="server" Checked='<%# Bind("PerDelete") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <span class="gridviewEmptyMsg">
                                    <%= Resources.Resource.GridViewEmptyDataMsg %>
                                </span>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cboUserDesc" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="btn_update" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                <div style="float: right; font-size: 10px">
                    <asp:LinkButton ID="btnCheck" runat="server" CssClass="ui-state-default ui-corner-all action-button"
                        OnClick="btnCheck_Click" EnableTheming="false" Visible="false">Select/Unselect All</asp:LinkButton>
                    <asp:CheckBox ID="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server"
                        ToolTip="Select/Deselect all" />
                    Select/Deselect all
                </div>
            </div>
        </div>
        <div class="cleardiv">
        </div>
        <asp:HiddenField ID="hidMenuID" runat="server" />
    </div>
</asp:Content>
