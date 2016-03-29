<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="404.aspx.cs" Inherits="ErrorPages_404" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        h1.notFound
        {
            font-weight: normal;
            color: #D75A46;
            font-family: Trebuchet MS;
            font-size: 52px;
            margin: 100px 0 12px;
            text-align: center;
        }
        h2.notFoundDesc
        {
            font-weight: normal;
            color: #676767;
            font-family: Trebuchet MS;
            font-size: 32px;
            margin: 30px 0px 100px 0px;
            text-align: center;
        }
        h3.notFoundGoBack
        {
            font-weight: normal;
            color: #676767;
            font-family: Trebuchet MS;
            font-size: 32px;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="tabsMain">
        <ul>
            <li><a href="#tabs-1">Page Not Found</a></li>
        </ul>
        <div id="tabs-1">
            <h1 class="notFound">
                404 Error - Page not found.</h1>
            <h2 class="notFoundDesc">
                The page you're looking can't be found
            </h2>
            <h3 class="notFoundGoBack">
                <asp:HyperLink ID="lnkHome" runat="server" NavigateUrl="~/Index2.aspx">Return to the homepage</asp:HyperLink>
            </h3>
        </div>
    </div>
</asp:Content>
