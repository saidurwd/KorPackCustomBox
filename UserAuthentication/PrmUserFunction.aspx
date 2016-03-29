<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrmUserFunction.aspx.cs"
    MasterPageFile="~/MasterPage.master" Inherits="UserAuthentication_PrmUserFunction"
    Title="Assign User Authentication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
            left: 206px;
            margin-bottom: 5px;
            margin-top: 5px;
            position: absolute;
            top: 285px;
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


        $(function () {
            $('#<%=grdUserFunction.ClientID %> tbody tr').quicksearch({
                loaderText: 'loading...',
                labelText: 'Filter: '
            });
            var qhtml = $('#quicksearch').html();
            //alert($('#quicksearch').html());
            //            if (qhtml != null) {
            //                $('#quicksearch').hide();
            //            }
        });

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
                    $('#quicksearch').show();
                }
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="tabsMain">
        <ul>
            <li><a href="#tabs-1">Menu Level Security</a></li>
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
                            User Name:
                        </td>
                        <td class="style2">
                            <asp:DropDownList ID="cboUserDesc" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="cboUserDesc_SelectedIndexChanged"
                                AutoPostBack="True" DataSourceID="SDSUserInfo" DataTextField="LoginID" DataValueField="UserID">
                                <asp:ListItem Value="0">&lt;Select User&gt;</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Module:
                        </td>
                        <td class="style2">
                            <asp:DropDownList ID="ddlModule" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlModule_SelectedIndexChanged"
                                AutoPostBack="True" DataSourceID="sdsModule" DataTextField="Module" DataValueField="Module">
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
                            <asp:SqlDataSource ID="SDSUserInfo" runat="server" ConnectionString="<%$ AppSettings:Cnn %>"
                                SelectCommand="SELECT [UserID], [LoginID] FROM [TB_PrmUserInfo] Where Company_ID = @CompanyID and LoginID<>'ecdsadmin' order by LoginID">
                                <SelectParameters>
                                    <asp:SessionParameter DbType="String" DefaultValue="0" SessionField="CompanyID" Name="CompanyID" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="sdsModule" runat="server" ConnectionString="<%$ AppSettings:Cnn %>"
                                SelectCommand="SELECT  distinct Module FROM TB_PrmFunction WHERE COMPANY_ID=@CompanyID and MenuEnableDisable=1 and MenuInUse=1 and Module is not null order by Module">
                                <SelectParameters>
                                    <asp:SessionParameter DbType="String" DefaultValue="0" SessionField="CompanyID" Name="CompanyID" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:Button ID="btn_update" runat="server" Text="Save" 
                                OnClick="btnSave_Click" OnClientClick="javascript:return fnSave();" />

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
                        <asp:Panel ID="Panel1" runat="server" CssClass="gridPanel" ScrollBars="Vertical"
                            Height="430px" BackColor="#EEEEEE" Style="margin-top: 20px; width: 100%;">
                            <asp:GridView ID="grdUserFunction" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="grdUserFunction_PageIndexChanging"
                                CssClass="gridview" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Function Description">
                                        <ItemStyle HorizontalAlign="Left" Width="50%" />
                                        <HeaderStyle HorizontalAlign="Left" Width="50%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblFuncID" runat="server" Text='<%# Bind("FuncID") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblParentID" runat="server" Text='<%# Bind("ParentID") %>' Visible="False"></asp:Label>
                                            <%# Eval("FuncDesc") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Function Of">
                                        <ItemStyle HorizontalAlign="Left" Width="30%" />
                                        <HeaderStyle HorizontalAlign="Left" Width="30%" />
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
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkRowAdd" runat="server" Checked='<%# Bind("PerAdd") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkRowEdit" runat="server" Checked='<%# Bind("PerEdit") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemStyle HorizontalAlign="Center" />
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
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cboUserDesc" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="btn_update" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:LinkButton ID="btnCheck" runat="server" CssClass="ui-state-default ui-corner-all action-button"
                    OnClick="btnCheck_Click" EnableTheming="false">Select/Unselect All</asp:LinkButton>
            </div>
        </div>
        <div class="cleardiv">
        </div>
        <asp:HiddenField ID="hidMenuID" runat="server" />
    </div>
    
</asp:Content>
