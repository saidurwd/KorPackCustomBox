<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="BoxSamples.aspx.cs" Inherits="Configuration_AboutBlumenSoft" %>

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
                    Box Styles</h3>
                <h5>
                    Here are the most common box styles used to ship and display products:
                </h5>
            </div>
        </div>
        <div class="space-10">
        </div>
        <div class="media">
            <div class="media-left">
                <img src="<%=ResolveUrl("~/Images/BoxStyles/rsc.jpg") %>" class="media-object" alt="Regular Slotted Container (RSC)"
                    title="Regular Slotted Container (RSC)" />
            </div>
            <div class="media-body text-padding-top-50">
                <h4 class="media-heading">
                    Regular Slotted Container (RSC)</h4>
                This is the most common box style, guaranteed when you think of a box, this is the
                one of the designs you see in your mind. You can see it everywhere you look. They
                can be made without a die most of the time, however there are larger boxes that
                require Dies. With our Quickor System, you never need a die! The Quickor RSC is
                perfect for custom sizes large or small and a quick turn-around. of waste.
            </div>
        </div>
        <div class="space-10">
        </div>
        <div class="media">
            <div class="media-left">
                <img src="<%=ResolveUrl("~/Images/BoxStyles/fol.jpg") %>" class="media-object" alt="Full Overlap Container (FOL)"
                    title="Full Overlap Container (FOL)" />
            </div>
            <div class="media-body text-padding-top-50">
                <h4 class="media-heading">
                    Full Overlap Container (FOL)
                </h4>
                The FOL type box, or side loading FOL is great for many reasons. Perfect for picture
                frames, televisions, large skinny items, heavy items, and much more. The overlapping
                flaps will provide extra cushion, and extra strength to tops and bottoms. An RSC
                Bottom is only as strong as the tape and seams, with an FOL you have two extra layers
                that can increase cushioning and strength to the tops and bottoms. They can be laid
                down on their side, or stood on their ends.
            </div>
        </div>
        <div class="space-10">
        </div>
        <div class="media">
            <div class="media-left">
                <img src="<%=ResolveUrl("~/Images/BoxStyles/opf.jpg") %>" class="media-object" alt="One Panel Folder (OPF)"
                    title="One Panel Folder (OPF)" />
            </div>
            <div class="media-body text-padding-top-50">
                <h4 class="media-heading">
                    One Panel Folder (OPF)</h4>
                Also known as a Bookfold or an easy fold mailer, it is perfect for shipping flat
                items where the box can gain its strength from the contents. This is perfect for
                books, paper, posters, and other items that may be too small for an RSC.
            </div>
        </div>
        <div class="space-10">
        </div>
        <div class="media">
            <div class="media-left">
                <img src="<%=ResolveUrl("~/Images/BoxStyles/tele_tray.jpg") %>" class="media-object"
                    alt="Tele-Tray" title="Tele-Tray" />
            </div>
            <div class="media-body text-padding-top-50">
                <h4 class="media-heading">
                    Tele-Tray</h4>
                This type of set allows you to use as a tray to carry items, or as a top and bottom
                piece for large thin items that need to be protected while transporting.
            </div>
        </div>
        <div class="space-10">
        </div>
        <div class="media">
            <div class="media-left">
                <img src="<%=ResolveUrl("~/Images/BoxStyles/hsc.jpg") %>" class="media-object" alt="Half Slotted Container (HSC)"
                    title="Half Slotted Container (HSC)" />
            </div>
            <div class="media-body text-padding-top-50">
                <h4 class="media-heading">
                    Half Slotted Container (HSC)</h4>
                The HSC is an RSC without the top flaps, perfect for moving items around the office
                or home, a separate tray can be created if a cover is needed and will be taken off
                consistently. An HSC is great for every day usage.
            </div>
        </div>
        <div class="space-10">
        </div>
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12">
                <h3>
                    Box Strength</h3>
                <p>
                    <strong>Regular Strength: </strong>Single-wall, 32 ECT / 200#
                </p>
                <p>
                    <strong>Heavy Duty strength: </strong>Single-wall, 44 ECT / 275# - *40% stronger
                    than Regular shipping containers*</p>
                <p>
                    <strong>Extra Heavy Duty strength: </strong>Double-wall, 48 ECT / 275# - *Provides
                    greater protection and stacking strength*</p>
            </div>
        </div>
        <div class="space-10">
        </div>
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12">
                <img src="<%=ResolveUrl("~/Images/BoxStyles/SingleWall.png") %>" class="img-responsive"
                    alt="Single Wall (left) vs. Double Wall (right) Box Strengths" title="Single Wall (left) vs. Double Wall (right) Box Strengths" />
                <p>
                    Single Wall (left) vs. Double Wall (right) Box Strengths</p>
            </div>
        </div>
        <div class="space-10">
        </div>
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12">
                <h3>
                    Box Colors</h3>
                <p>
                    Our online box configurator offers the two most common box colors:
                </p>
                <p>
                    <strong>Kraft/Kraft </strong>is the standard brown box you see, both inside and
                    out.</p>
                <p>
                    <strong>White/Kraft </strong>is white on the outside and Kraft on the inside. *For
                    more color options, please contact us at <a href="mailto:quotes@korpack.com">quotes@korpack.com</a>
                    or 630-213-3600*</p>
            </div>
        </div>
        <div class="space-10">
        </div>
    </div>
</asp:Content>
