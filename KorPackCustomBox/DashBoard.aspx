<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DashBoard.aspx.cs" Inherits="DashBoard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="~/Style/EnterpriseBlue/reset-min.css" rel="stylesheet" type="text/css" />
    <link href="Style/EnterPriseBlue/inettuts.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="Scripts/inettuts/jquery-1.2.6.min.js"></script>
    <link href="Style/EnterPriseBlue/jquery.jqplot.css" rel="stylesheet" type="text/css" />
    <%--<link href="Scripts/inettuts/inettuts.css" rel="stylesheet" type="text/css" />--%>
    <!-- BEGIN: load jqplot -->
    <script language="javascript" type="text/javascript" src="Scripts/jqplot/jquery.jqplot.js"></script>
    <script language="javascript" type="text/javascript" src="Scripts/jqplot/plugins/jqplot.dateAxisRenderer.js"></script>
    <script language="javascript" type="text/javascript" src="Scripts/jqplot/plugins/jqplot.highlighter.js"></script>
    <script language="javascript" type="text/javascript" src="Scripts/jqplot/plugins/jqplot.cursor.js"></script>
    <script language="javascript" type="text/javascript" src="Scripts/jqplot/plugins/jqplot.categoryAxisRenderer.js"></script>
    <!-- END: load jqplot -->
    <%--
    <script language="javascript" type="text/javascript" src="<%= ResolveUrl("~/Scripts/jqplot/jquery.jqplot.js")  %>"></script>
    <script language="javascript" type="text/javascript" src="<%= ResolveUrl("~/Scripts/jqplot/plugins/jqplot.dateAxisRenderer.js")  %>"></script>
    <script language="javascript" type="text/javascript" src="<%= ResolveUrl("~/Scripts/jqplot/plugins/jqplot.highlighter.js")  %>"></script>
    <script language="javascript" type="text/javascript" src="<%= ResolveUrl("~/Scripts/jqplot/plugins/jqplot.cursor.js")  %>"></script>
    <script language="javascript" type="text/javascript" src="<%= ResolveUrl("~/Scripts/jqplot/plugins/jqplot.categoryAxisRenderer.js")  %>"></script>
    --%>
    <script type="text/javascript">
        $('<style type="text/css">.column{visibility:hidden;}</style>').appendTo('head');
        $('body').css({ background: '#fff url(Style/EnterPriseBlue/images/DashBoard/load.gif) no-repeat center' })
    </script>
    <style type="text/css">
        td
        {
            border: 0 !important;
        }
        table.gridviewDashBoard
        {
            width: 100%;
            margin: 10px 0;
        }
        .numericCol
        {
            text-align: right;
        }
        .gridviewDashBoard
        {
            font-size: 11px;
            font-family: Verdana,MS Sans Serif;
            border: 0px !important;
            border-collapse: separate !important;
        }
        .gridviewDashBoard tr
        {
            line-height: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%; height: 550px; padding: 0px; margin: 0px;">
        <style>
            .chart-blue
            {
                background: #148EA4;
                width: 98%;
                float: left;
                margin: 5px;
                height: 235px;
                border-bottom-left-radius: 4px 4px;
                border-bottom-right-radius: 4px 4px;
                border-top-left-radius: 4px 4px;
                border-top-right-radius: 4px 4px;
                padding: 3px;
            }
            .chart-blackish
            {
                width: 100%;
                float: left;
                height: 80px;
                padding-top: 12px;
                vertical-align: middle;
                text-align: center; /*
                margin: 1px;
                border-bottom-left-radius: 4px 4px;
                border-bottom-right-radius: 4px 4px;
                border-top-left-radius: 4px 4px;
                border-top-right-radius: 4px 4px;
                padding: 3px;*/
                background: transparent url('images/bg.png') repeat-x;
            }
            .bgshadow
            {
                width: 100%;
                float: left;
                height: 18px;
                vertical-align: middle;
                text-align: center; /*
                padding-top:20px;
                margin: 1px;
                border-bottom-left-radius: 4px 4px;
                border-bottom-right-radius: 4px 4px;
                border-top-left-radius: 4px 4px;
                border-top-right-radius: 4px 4px;
                padding: 3px;*/
                background: transparent url('images/bgshadow.png') repeat-x;
            }
            .Shortcut2
            {
                width: 700px;
                margin-left: auto;
                margin-right: auto;
                text-align: center !important;
                vertical-align: middle;
            }
            .chart-head
            {
                background: #148EA4;
                text-align: left;
                height: 20px;
                color: #fff;
            }
            .chart-head h3
            {
                margin: 9px 0 0 15px;
            }
            .chart-content
            {
                background: #000000;
            }
        </style>
        <div class="chart-blackish" runat="server" id="shortcut">
            <div id="Shortcut2" class="Shortcut2">
                <asp:GridView ID="GridView1" runat="server" Width="100%" DataSourceID="SqlDataSource1"
                    OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="false" ShowHeader="false"
                    BorderWidth="0">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <table style="text-align: center; vertical-align: middle;">
                                    <tr style="border: 0px none">
                                        <td>
                                            <asp:HyperLink ID="hlinkRefNo" runat="server" Text='<% #Eval("FuncDesc1") %>' NavigateUrl='<%#Eval("TargetPage1") %>'
                                                ImageUrl="~/Images/icon1.png" Target="_parent" Visible="false">
                                        
                                            </asp:HyperLink>
                                            &nbsp;
                                            <asp:ImageButton ID="imgbRefNo1" Style="height: 50px; width: auto;" runat="server"
                                                ImageUrl="~/Images/icon1.png" PostBackUrl='<%#Eval("TargetPage1") %>' Visible="false"/>
                                            <a href="<%#Eval("TargetPage1") %>" target="_parent">
                                                <img border="0" alt="logo" style="height: 50px; width: auto;" src="<%=ResolveUrl("~/Images/icon1.png") %>" />
                                            </a>
                                    </tr>
                                    <tr style="border: 0px none">
                                        <td>
                                            &nbsp;
                                            <asp:Label ID="lblRefNo" runat="server" Text='<%#Eval("FuncDesc1") %>' Style="color: White;
                                                font-weight: bold"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <table style="text-align: center; vertical-align: middle">
                                    <tr style="border: 0px none">
                                        <td>
                                            <asp:HyperLink ID="hlinkRefNo2" runat="server" Text='<% #Eval("FuncDesc2") %>' NavigateUrl='<%#Eval("TargetPage2") %>'
                                                ImageUrl="~/Images/icon2.png" Target="_parent" Visible="false">
                                            </asp:HyperLink>
                                            <asp:ImageButton ID="imgbRefNo2" Style="height: 50px; width: auto;" runat="server"
                                                ImageUrl="~/Images/icon2.png" PostBackUrl='<%#Eval("TargetPage2") %>' Visible="false"/>
                                            <a href="<%#Eval("TargetPage2") %>" target="_parent">
                                                <img border="0" alt="logo" style="height: 50px; width: auto;" src="<%=ResolveUrl("~/Images/icon2.png") %>" />
                                            </a>
                                    </tr>
                                    <tr style="border: 0px none">
                                        <td>
                                            <asp:Label ID="lblRefNo2" runat="server" Text='<%#Eval("FuncDesc2") %>' Style="color: White;
                                                font-weight: bold"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <table style="text-align: center; vertical-align: middle">
                                    <tr style="border: 0px none">
                                        <td>
                                            <asp:HyperLink ID="hlinkRefNo3" runat="server" Text='<% #Eval("FuncDesc3") %>' NavigateUrl='<%#Eval("TargetPage3") %>'
                                                ImageUrl="~/Images/icon3.png" Target="_parent" Visible="false">
                                            </asp:HyperLink>
                                            <asp:ImageButton ID="imgbRefNo3" Style="height: 50px; width: auto;" runat="server"
                                                ImageUrl="~/Images/icon3.png" PostBackUrl='<%#Eval("TargetPage3") %>' Visible="false"/>
                                            <a href="<%#Eval("TargetPage3") %>" target="_parent">
                                                <img border="0" alt="logo" style="height: 50px; width: auto;" src="<%=ResolveUrl("~/Images/icon3.png") %>" />
                                            </a>
                                    </tr>
                                    <tr style="border: 0px none">
                                        <td>
                                            <asp:Label ID="lblRefNo3" runat="server" Text='<%#Eval("FuncDesc3") %>' Style="color: White;
                                                font-weight: bold"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <table style="text-align: center; vertical-align: middle">
                                    <tr style="border: 0px none">
                                        <td>
                                            <asp:HyperLink ID="hlinkRefNo4" runat="server" Text='<% #Eval("FuncDesc4") %>' NavigateUrl='<%#Eval("TargetPage4") %>'
                                                ImageUrl="~/Images/icon4.png" Target="_parent" Visible="false">
                                            </asp:HyperLink>
                                            <asp:ImageButton ID="imgbRefNo4" Style="height: 50px; width: auto;" runat="server"
                                                ImageUrl="~/Images/icon4.png" PostBackUrl='<%#Eval("TargetPage4") %>' Visible="false" />
                                            <a href="<%#Eval("TargetPage4") %>" target="_parent">
                                                <img border="0" alt="logo" style="height: 50px; width: auto;" src="<%=ResolveUrl("~/Images/icon4.png") %>" />
                                            </a>
                                    </tr>
                                    <tr style="border: 0px none">
                                        <td>
                                            <asp:Label ID="lblRefNo4" runat="server" Text='<%#Eval("FuncDesc4") %>' Style="color: White;
                                                font-weight: bold"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <table style="text-align: center; vertical-align: middle">
                                    <tr style="border: 0px none">
                                        <td>
                                            <asp:HyperLink ID="hlinkRefNo5" runat="server" Text='<% #Eval("FuncDesc5") %>' NavigateUrl='<%#Eval("TargetPage5") %>'
                                                ImageUrl="~/Images/icon1.png" Target="_parent" Visible="false">
                                            </asp:HyperLink>
                                            <asp:ImageButton ID="imgbRefNo5" Style="height: 50px; width: auto;" runat="server"
                                                ImageUrl="~/Images/icon1.png" PostBackUrl='<%#Eval("TargetPage5") %>' Visible="false" />
                                            <a href="<%#Eval("TargetPage5") %>" target="_parent">
                                                <img border="0" alt="logo" style="height: 50px; width: auto;" src="<%=ResolveUrl("~/Images/icon1.png") %>" />
                                            </a>
                                    </tr>
                                    <tr style="border: 0px none">
                                        <td>
                                            <asp:Label ID="lblRefNo5" runat="server" Text='<%#Eval("FuncDesc5") %>' Style="color: White;
                                                font-weight: bold"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Cnn %>">
                </asp:SqlDataSource>
            </div>
        </div>
        <div class="bgshadow" runat="server" id="bgShadow">
        </div>
        <div class="chart-blue" runat="server" id="saleschart" style="display: none">
            <div class="chart-head">
                <h3>
                    Sales Reports</h3>
            </div>
            <div class="chart-content">
                <div id="chart1" class="plot" style="width: 95%; height: 200px;">
                </div>
                <asp:HiddenField ID="HidchartData" runat="server" Value="0" />
                <asp:HiddenField ID="HidTitle1" runat="server" Value="0" />
                <script type="text/javascript">
                    $.jqplot.config.enablePlugins = true;
                    //s1 = [['2010-01-01', 0.00], ['2010-02-01', 0.00], ['2010-03-01', 0.00], ['2010-04-01', 0.00], ['2010-05-01', 0.00], ['2010-06-01', 0.00], ['2010-07-01', 0.00], ['2010-10-01', 0.00], ['2010-12-01', 0.00], ['2010-11-01', 100.00], ['2010-09-01', 52800.00], ['2010-08-01', 285000.00]];
                    //s1 = [[1, 40.00], [2, 60.00], [3, 0.00], [4, 0.00]];
                    s2 = $('#<%=HidchartData.ClientID %>').val();
                    if (s2 != "0") {
                        //alert("s2" + s2); 
                        var titleText = $('#<%=HidTitle1.ClientID %>').val();
                        obj = eval("[" + s2 + "]");
                        plot2 = $.jqplot('chart1', obj,
            { title: titleText,
                axes: { xaxis: { renderer: $.jqplot.CategoryAxisRenderer} },
                series: [{ lineWidth: 4, markerOptions: { style: 'square'}}]
            });
                    } 
                </script>
            </div>
        </div>
        <div id="columns">
            <ul id="column1" class="column" runat="server">
                <li class="widget color-white" runat="server" id="intro">
                    <div class="widget-head">
                        <h3>
                            Today's Sales</h3>
                    </div>
                    <div class="widget-content">
                        <asp:GridView ID="grdDocControl" runat="server" AutoGenerateColumns="true" ShowHeader="false"
                            CssClass="gridviewDashBoard">
                        </asp:GridView>
                    </div>
                </li>
                <li class="widget color-white" runat="server" id="widget2">
                    <div class="widget-head">
                        <h3>
                            Top 5 Customers</h3>
                    </div>
                    <div class="widget-content">
                        <asp:GridView ID="grdSales" runat="server" AutoGenerateColumns="false" ShowHeader="false"
                            CssClass="gridviewDashBoard">
                            <Columns>
                                <asp:BoundField DataField="Caption" HeaderText="Caption" HeaderStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="60%" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50%"
                                    SortExpression="Caption" ReadOnly="True" />
                                <asp:BoundField DataField="No" HeaderText="No" HeaderStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%"
                                    ItemStyle-CssClass="numericCol" SortExpression="No" ReadOnly="True" />
                                <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%"
                                    ItemStyle-CssClass="numericCol" SortExpression="Caption" ReadOnly="True" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
            </ul>
            <ul id="column2" class="column">
                <li class="widget color-white" runat="server" id="widget3">
                    <div class="widget-head">
                        <h3>
                            Today's Purchase</h3>
                    </div>
                    <div class="widget-content">
                        <asp:GridView ID="grdProcurement" runat="server" AutoGenerateColumns="false" ShowHeader="false"
                            CssClass="gridviewDashBoard">
                            <Columns>
                                <asp:BoundField DataField="Caption" HeaderText="Caption" HeaderStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="60%" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50%"
                                    SortExpression="Caption" ReadOnly="True" />
                                <asp:BoundField DataField="No" HeaderText="No" HeaderStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%"
                                    ItemStyle-CssClass="numericCol" SortExpression="No" ReadOnly="True" />
                                <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%"
                                    ItemStyle-CssClass="numericCol" SortExpression="Caption" ReadOnly="True" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
                <li class="widget color-white" runat="server" id="widget4">
                    <div class="widget-head">
                        <h3>
                            5 First Moving Items</h3>
                    </div>
                    <div class="widget-content">
                        <asp:GridView ID="grdInventory" runat="server" AutoGenerateColumns="false" ShowHeader="false"
                            CssClass="gridviewDashBoard">
                            <Columns>
                                <asp:BoundField DataField="Caption" HeaderText="Caption" HeaderStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="60%" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50%"
                                    SortExpression="Caption" ReadOnly="True" />
                                <asp:BoundField DataField="No" HeaderText="No" HeaderStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%"
                                    ItemStyle-CssClass="numericCol" SortExpression="No" ReadOnly="True" />
                                <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%"
                                    ItemStyle-CssClass="numericCol" SortExpression="Caption" ReadOnly="True" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
            </ul>
            <ul id="column3" class="column">
                <li class="widget color-white" runat="server" id="widget5">
                    <div class="widget-head">
                        <h3>
                            Items not in stock</h3>
                    </div>
                    <div class="widget-content">
                        <asp:GridView ID="grdAccounts" runat="server" AutoGenerateColumns="false" ShowHeader="false"
                            CssClass="gridviewDashBoard">
                            <Columns>
                                <asp:BoundField DataField="Caption" HeaderText="Caption" HeaderStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="50%" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50%"
                                    SortExpression="Caption" ReadOnly="True" />
                                <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="50%" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50%"
                                    ItemStyle-CssClass="numericCol" SortExpression="Caption" ReadOnly="True" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
                <li class="widget color-white" runat="server" id="widget6">
                    <div class="widget-head">
                        <h3>
                            Receivables & Payables</h3>
                    </div>
                    <div class="widget-content">
                        <asp:GridView ID="grdHR" runat="server" AutoGenerateColumns="true" ShowHeader="false"
                            CssClass="gridviewDashBoard">
                        </asp:GridView>
                    </div>
                </li>
            </ul>
        </div>
        <script type="text/javascript" src="Scripts/inettuts/jquery-ui-personalized-1.6rc2.min.js"></script>
        <script src="Scripts/inettuts/cookie.jquery.js" type="text/javascript"></script>
        <script src="Scripts/inettuts/inettuts.js" type="text/javascript"></script>
    </div>
    </form>
</body>
</html>
