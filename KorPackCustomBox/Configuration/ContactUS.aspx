<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ContactUS.aspx.cs" Inherits="Configuration_AboutBlumenSoft" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
       
    </style>
    <script type="text/javascript">
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="row">
            <div class="col-md-9">
                <div class="bs-callout bs-callout-info">
                    <h4>Interested in partnering with us, have any questions, or simply want to say hello? Contact us today! You can reach us during our business hours of 7AM – 6PM Monday through Friday by phone, or fill out the form below, and we will get back to you as soon as possible. We look forward to hearing from you.</h4>
                </div>
            </div>
            <div class="col-md-7">

                <div class="row">
                    <div class="col-md-7 text-right">
                        <h5>Your Name:</h5>
                    </div>
                    <div class="col-md-5 amountText">
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control input-sm" required></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-7 text-right">
                        <h5>Company:</h5>
                    </div>
                    <div class="col-md-5 amountText">
                        <asp:TextBox ID="txtCompany" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-7 text-right">
                        <h5>Address 2:</h5>
                    </div>
                    <div class="col-md-5 amountText">
                        <asp:TextBox ID="txtStreet" runat="server" CssClass="form-control input-sm" placeholder="Street"></asp:TextBox>
                        <asp:TextBox ID="txtCity" runat="server" CssClass="form-control input-sm" placeholder="City"></asp:TextBox>
                        <asp:TextBox ID="txtState" runat="server" CssClass="form-control input-sm" placeholder="State"></asp:TextBox>
                        <asp:TextBox ID="txtZipCode" runat="server" CssClass="form-control input-sm" placeholder="Zip Code"></asp:TextBox>
                    </div>
                </div>
                
                <div class="row">
                    <div class="col-md-7 text-right">
                        <h5>Phone:</h5>
                    </div>
                    <div class="col-md-5 amountText">
                        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-7 text-right">
                        <h5>Email:</h5>
                    </div>
                    <div class="col-md-5 amountText">
                        <asp:TextBox ID="txtEmail" type="email" runat="server" CssClass="form-control input-sm" required></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-7 text-right">
                        <h5>Subject:</h5>
                    </div>
                    <div class="col-md-5 amountText">
                        <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-7 text-right">
                        <h5>Your Message:</h5>
                    </div>
                    <div class="col-md-5 amountText">
                        <textarea id="txtMessage" runat="server" class="form-control" rows="3" required></textarea>
                    </div>
                </div>

            </div>
            <div class="col-md-7">
                <div class="row">
                    <div class="col-md-7 text-right">
                    </div>
                    <div class="col-md-5 amountText">
                        <asp:Button ID="Button1" runat="server" Text="SEND" OnClick="btnProcess_Click"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
