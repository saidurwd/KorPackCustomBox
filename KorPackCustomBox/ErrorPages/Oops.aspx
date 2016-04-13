<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Oops.aspx.cs" Inherits="ErrorPages_Oops" %>

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
            <li><a href="#tabs-1">An Error Has Occurred</a></li>
        </ul>
        <div id="tabs-1">
            <h1 class="notFound">
                404 Error - An Error Has Occurred.</h1>
            <h2 class="notFoundDesc">
                An unexpected error occurred. The Administrator has been
                notified.
            </h2>
            <h3 class="notFoundGoBack">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Index2.aspx">Return to the homepage</asp:HyperLink>
            </h3>
        </div>
    </div>
</asp:Content>
