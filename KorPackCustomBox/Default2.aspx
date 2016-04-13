<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" %>

<script runat="server">

</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="infoMsg" class="ui-state-highlight ui-corner-all" style="margin-top: 0px;
        padding: 0 .7em;">
        <p>
            <span class="ui-icon-close"></span>
            <span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span>
            <strong><span id="actInfo"></span></strong>
        </p>
    </div>
</asp:Content>
