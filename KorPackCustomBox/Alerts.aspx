<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageDummy.master" AutoEventWireup="true"
    CodeFile="Alerts.aspx.cs" Inherits="Index2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .cssPager td
        {
            padding-left: 2px;
            padding-right: 2px;
            font-family: Verdana;
            font-size: 10pt;
        }
        .cssPager
        {
            float: right;
        }
        .selected
        {
            color: Green;
        }
        .ui-tabs-important
        {
            background-color: Red;
        }
        .ddStyle
        {
            float: right;
            position: relative;
            top: -41px;
        }
        .InnerReportdiv
        {
            /*border: solid 1px red;*/
            float: left;
            position: relative;
            width: 24%;
        }
    </style>
    <script type="text/javascript">

        //jQuery.noConflict();
        $(function () {
            $('.cssPager').click(function () {

                $('.cssPager').css('selected');
            });
        });

        function SelectAllCheckboxes(chk) {
            jQuery('#<%=grdTaskInfo.ClientID %>').find("input:checkbox").each(function () {
                if (this != chk) {
                    this.checked = chk.checked;
                }
            });

        }
        function fnNewTask() {

            //var strFeatures = "titlebar=no,toolbar=no, dependent=yes, resizable=no,top=120,left=255,Width=550,Height=290,help=no,maximize=no;minimize=no,scrollbars=no";
            //window.open('HomePage/TaskAssign.aspx?TaskID=0&Task=&DueDate=', 'popwindow', strFeatures, "");

            tb_show('New Message', 'HomePage/TaskAssign.aspx?TaskID=0&Task=&DueDate=&TB_iframe=true&height=550&width=800', null);
            return false;

        }
        function fnArchive() {
            var c = confirm("Are you sure you want to archive task(s)");
            if (c == true)
                return true;
            else
                return false;
        }
        function fnDelete() {
            var c = confirm("Are you sure you want to delete task(s)");
            if (c == true)
                return true;
            else
                return false;
        }
        function fnDone() {
            var c = confirm("Are you sure you want to complete task(s)");
            if (c == true)
                return true;
            else
                return false;
        }

        function fnHomePageRefresh() {
            //alert('please');

            __doPostBack('ctl00$ContentPlaceHolder1$btnRefreshGrid', '')
        }
        function WriteDateinCookie(target) {

            alert("changed - value: " + target.value);

            window.top.location = 'http://localhost/bs/Index2.aspx';
            return false;
            //$.cookies.set('DDate', target.value);
            //document.cookie = DDate + "=" + target.value;
            //alert ("you have changed the textbox...")
        }

        
    </script>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div class="updatePanelPro">
                Loading Message...
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div>
        <asp:HiddenField ID="HidtxtWhereCondition" runat="server" />
        <div style="float: right">
            <asp:Label ID="lblPageVal" runat="server"></asp:Label>
        </div>
    </div>
    <div>
        <div id="tabsMain">
            <ul>
                <li><a href="#alerts">Alerts</a></li>
            </ul>
            <div id="Messages" style="display: none">
                <div class="ddStyle">
                    <asp:DropDownList ID="cboTaskStatus" runat="server" Font-Names="Verdana" Width="80px"
                        Font-Size="XX-Small" AutoPostBack="True">
                        <asp:ListItem>Archive</asp:ListItem>
                        <asp:ListItem Selected="True" Value="Active">Inbox</asp:ListItem>
                        <asp:ListItem Value="AssignByMe">Send Box</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <table width="100%" cellpadding="0" cellspacing="0" border="0" style="display: none">
                    <tr>
                        <td style="width: 96%" valign="top">
                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td valign="top" style="width: 100%;">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="width: 100%" valign="top">
                                                    <asp:Panel ID="Panel1" runat="server" BorderStyle="None" BorderWidth="1px" Height="420px"
                                                        ScrollBars="none" Width="100%">
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:GridView ID="grdTaskInfo" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                                                                    PageSize="10" CssClass="gridview" AlternatingRowStyle-CssClass="gridviewaltrow"
                                                                    ToolTip="Double click to get details information" Width="100%">
                                                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                        NextPageText="&gt;" Position="Bottom" PreviousPageText="&lt;" Visible="true" />
                                                                    <Columns>
                                                                        <asp:ButtonField CommandName="DoubleClick" Text="DoubleClick" Visible="False" />
                                                                        <asp:TemplateField HeaderText="Subject">
                                                                            <ItemStyle Width="45%" HorizontalAlign="Left" />
                                                                            <HeaderStyle Width="45%" HorizontalAlign="Left" />
                                                                            <ItemTemplate>
                                                                                <div style="width: 100%;">
                                                                                    <div style="width: 10%; float: left; text-align: left;">
                                                                                        <asp:Image ID="imgAlert" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/SkinFile/images/alert.gif"
                                                                                            BackColor="Transparent" />
                                                                                        <asp:Image ID="imgMsg" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/SkinFile/images/msg.gif" />
                                                                                    </div>
                                                                                    <div style="width: 88%; float: left; text-align: left;">
                                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Task") %>' ToolTip='<%# Eval("Task") %>'></asp:Label>
                                                                                        <asp:HyperLink ID="hypTask" runat="server" Font-Size="X-Small" NavigateUrl="~/HomePage/TaskAssign.aspx"
                                                                                            Target="popwindow" Text='<%# Bind("Task") %>' Visible="False"></asp:HyperLink>
                                                                                        <asp:Label ID="lblReminder" Visible="false" runat="server" Text='<%# Eval("Reminder") %>'></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Due Date">
                                                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDueDate" runat="server" Text='<%# Bind("DueDate") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Assigned By">
                                                                            <ItemStyle HorizontalAlign="Center" Width="25%" />
                                                                            <HeaderStyle HorizontalAlign="Center" Width="25%" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAssignedBy" runat="server" Text='<%# Bind("AssignedBy") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Status">
                                                                            <ItemStyle HorizontalAlign="Center" Width="20%" />
                                                                            <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>&nbsp;
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Select">
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <HeaderTemplate>
                                                                                <asp:CheckBox ID="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server" />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkTask" runat="server" TextAlign="Left" />
                                                                                <asp:TextBox ID="txtTaskID" runat="server" Text='<%# Bind("TaskID") %>' Visible="False"></asp:TextBox><asp:TextBox
                                                                                    ID="txtAutoID" runat="server" Text='<%# Bind("AutoID") %>' Visible="False"></asp:TextBox>
                                                                                <asp:TextBox ID="txtAssignedToID" runat="server" Height="0px" Text='<%# Bind("AssignedToID") %>'
                                                                                    Visible="False"></asp:TextBox>
                                                                                <asp:TextBox ID="txtAssignedByID" runat="server" Height="0px" Text='<%# Bind("AssignedByID") %>'
                                                                                    Visible="False"></asp:TextBox>
                                                                                <asp:TextBox ID="txtFullTask" runat="server" Height="0px" Text='<%# Bind("FullTask") %>'
                                                                                    Visible="False"></asp:TextBox><br />
                                                                                <asp:TextBox ID="txtPageURL" runat="server" Height="0px" Text='<%# Bind("PageURL") %>'
                                                                                    Visible="False"></asp:TextBox>
                                                                                <asp:TextBox ID="txtJobID" runat="server" Height="0px" Text='<%# Bind("JobID") %>'
                                                                                    Visible="False"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EmptyDataTemplate>
                                                                        <span class="gridviewEmptyMsg">
                                                                            <%= Resources.Resource.GridViewEmptyDataMsg %>
                                                                        </span>
                                                                    </EmptyDataTemplate>
                                                                    <PagerStyle HorizontalAlign="Right" Font-Names="verdana" Font-Size="8pt" />
                                                                </asp:GridView>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btnShowAll" EventName="Click" />
                                                                <asp:AsyncPostBackTrigger ControlID="btnArchieve" EventName="Click" />
                                                                <asp:AsyncPostBackTrigger ControlID="btnDelete" EventName="Click" />
                                                                <asp:AsyncPostBackTrigger ControlID="btnDone" EventName="Click" />
                                                                <asp:AsyncPostBackTrigger ControlID="btnRefreshGrid" EventName="Click" />
                                                                <asp:AsyncPostBackTrigger ControlID="cboTaskStatus" EventName="SelectedIndexChanged" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td align="left" height="5">
                                                            </td>
                                                            <td align="right">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Button ID="btnShowAll" runat="server" Text="Show" Visible="False" />
                                                            </td>
                                                            <td align="right">
                                                                <asp:LinkButton ID="cmdReport" runat="server" Text="Click & Sort" Visible="false"
                                                                    OnClientClick="return showReport();" />
                                                                <asp:LinkButton ID="btnRefreshGrid" runat="server" Visible="true" Text="Refresh"
                                                                    CssClass="specialLink" />
                                                                <asp:LinkButton ID="btnNewTask" runat="server" OnClientClick="javascript:return fnNewTask();"
                                                                    Text="New Message" CssClass="specialLink" />
                                                                <asp:LinkButton ID="btnArchieve" runat="server" OnClientClick="javascript: return fnArchive()"
                                                                    Text="Archive" CssClass="specialLink" />
                                                                <asp:LinkButton ID="btnDelete" runat="server" OnClientClick="javascript: return fnDelete()"
                                                                    Text="Delete" CssClass="specialLink" />
                                                                <asp:LinkButton ID="btnDone" runat="server" OnClientClick="javascript: return fnDone()"
                                                                    Text="Done" CssClass="specialLink" />
                                                                <asp:HiddenField ID="txtHidUserID" runat="server" />
                                                                <asp:HiddenField ID="txtHidTaskShowStatus" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                &nbsp;
                                                            </td>
                                                            <td align="right">
                                                                <asp:TextBox ID="TextBox1" runat="server" Visible="False" Width="80%"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="alerts" class="">
                <asp:Panel ID="Panel2" runat="server" BorderStyle="None" BorderWidth="1px" Height="420px"
                    ScrollBars="none" Width="100%">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="grdAlerts" runat="server" AllowPaging="true" PageSize="20" CssClass="gridview"
                                AlternatingRowStyle-CssClass="gridviewaltrow" ToolTip="Double click to get details information"
                                Width="100%" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="Task" HeaderText="Subject" HeaderStyle-HorizontalAlign="Left"
                                        HeaderStyle-Width="40%" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%"
                                        SortExpression="Subject" ReadOnly="True" />
                                    <asp:BoundField DataField="DueDate" HeaderText="Due Date" HeaderStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%"
                                        SortExpression="DueDate" ReadOnly="True" />
                                    <asp:BoundField DataField="AssignedBy" HeaderText="Assigned By" HeaderStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%"
                                        SortExpression="AssignedBy" ReadOnly="True" />
                                    <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%"
                                        SortExpression="Status" ReadOnly="True" />
                                </Columns>
                                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                    NextPageText="&gt;" Position="Bottom" PreviousPageText="&lt;" Visible="true" />
                                <EmptyDataTemplate>
                                    <span class="gridviewEmptyMsg">
                                        <%= Resources.Resource.GridViewEmptyDataMsg %>
                                    </span>
                                </EmptyDataTemplate>
                                <PagerStyle HorizontalAlign="Right" Font-Names="verdana" Font-Size="8pt" />
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
