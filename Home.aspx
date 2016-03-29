<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Home.aspx.cs" Inherits="Index2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .text-padding-top-50
        {
            padding-top: 50px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12">
                <h3>
                    QuicKor</h3>
                <h5>
                    Our on demand Quickor machine provides you with flexibility that before was only
                    a dream. We can now create Boxes in a matter of seconds to your specific dimensions.
                    Don't worry about minimums, tooling or lead time. 90% of our Quickor orders are
                    able to be delivered the next business day if not the same day!
                    <br />
                    <br />
                    Should you need a quote however for a large run, don't worry, click <a href="mailto:quotes@korpack.com?Subject=Send%20Quote"
                        target="_top">here</a> and send us a quote, because we excel in that area too.
                </h5>
            </div>
        </div>
        <div class="space-10">
        </div>
    </div>
</asp:Content>
