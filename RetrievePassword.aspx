<%@ Page Title="Korpack: Retrieve Password" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="RetrievePassword.aspx.cs" Inherits="Sales_DirGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="row">
            <div class="col-md-9">
                <div class="bs-callout bs-callout-info">
                    <h4>Please enter you email address</h4>
                    <div class="row">
                        <div class="col-md-4" style="text-align: center">
                           <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-2" style="text-align: center">
                           <asp:Button ID="btnLogin" runat="server" Text="Reset Password" OnClick="btnLogin_Click"/>
                        </div>
                    </div>
                </div>
                <br />
            </div>        
        </div>


    </div>
    <div style="padding-top: 10px; text-align: left;">
        <div class="buttonSection" style="text-align: left; padding-left: 20px">
        </div>
    </div>
    <br />
    <br />

</asp:Content>
