<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TopRightHeader.aspx.cs" Inherits="TopRightHeader" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"
        ScriptMode="Release">
        <Scripts>
            <asp:ScriptReference Path="~/Scripts/RequestHandlerAjax.js" />
        </Scripts>
    </asp:ScriptManager>
    <div style="text-align: right; width: 100%; height: 50px; padding: 0px; margin: 0px; border: 1px solid red;">
        
            <div id="dock" class="">
                <ul>
                    <li></li>
                    <li>
                        <asp:DropDownList AutoPostBack="true" ID="ddlCulture" runat="server" OnSelectedIndexChanged="ddlCulture_SelectedIndexChanged"
                            Width="200px" Visible="false">
                            <asp:ListItem Value="en-US" Text="English"></asp:ListItem>
                            <asp:ListItem Value="de-AT" Text="German"></asp:ListItem>
                            <asp:ListItem Value="fr-Be" Text="French"></asp:ListItem>
                            <asp:ListItem Value="hi-IN" Text="Hindi"></asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li class="asd">
                        <asp:HyperLink ID="hlLogout" NavigateUrl="#" runat="server">Hi <%= logoutusername()%>!!!</asp:HyperLink>
                    </li>
                    <li>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <%--<asp:Label ID="lblInbox" runat="server" ForeColor="#0033CC"></asp:Label>--%>
                                <asp:HyperLink ID="hlnkInbox" NavigateUrl="Alerts.aspx" runat="server"></asp:HyperLink>
                                <asp:HiddenField ID="hidTimerValue" Value="0" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:Timer ID="Timer1" runat="server" Interval="60000">
                        </asp:Timer>
                        <%--<asp:HyperLink ID="hlHome" NavigateUrl="~/Index2.aspx" runat="server">Inbox</asp:HyperLink>--%>
                    </li>
                    <li>
                        <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Index2.aspx" CssClass="menuItemSettings"
                            runat="server">Settings</asp:HyperLink>
                        <%= SmallSubMenuSetting() %>
                    </li>
                    <li>
                        <asp:HyperLink ID="lnkHelp" NavigateUrl="~/Logout.aspx" runat="server">Logout</asp:HyperLink>
                    </li>
                </ul>
            </div>
        
    </div>
    </form>
</body>
</html>
