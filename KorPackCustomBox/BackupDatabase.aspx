<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="BackupDatabase.aspx.cs" Inherits="BackupDatabase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        #processAnimation
        {
            display: none;
            width: 20px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function processing() {

            $('#processAnimation').show();
            $('#<%=btnbackUP.ClientID%>').addClass('ui-button-disabled');
            $('#<%=btnbackUP.ClientID%>').addClass('ui-state-disabled');
            setTimeout('true', 2000);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="tabsMain">
        <ul>
            <li><a href="#tabs-1">Backup Database</a></li>
        </ul>
        <div id="tabs-1">
            <div style="margin: 0 auto; text-align: center;">
                <p>
                    Your DataBase will be backed up on the following path of server:
                    <asp:Label runat="server" ID="dbPath" /></p>
                <p>
                    Database backup can take minutes, depend on size. Please wait, while backup.
                </p>
                <span id="processAnimation">
                    <asp:Image ImageUrl="~/Style/EnterPriseBlue/images/ui-anim_basic_16x16.gif" runat="server" />
                </span>
                <asp:Button Text="Backup" runat="server" OnClientClick="processing();" ID="btnbackUP"
                    OnClick="btnbackUP_Click" /><br />
                <asp:Label runat="server" ID="lblMsg" Visible="false" CssClass="error" />
            </div>
            <div class="cleardiv">
                <asp:HiddenField ID="hidMenuID" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
