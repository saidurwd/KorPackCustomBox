<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManagePrice.aspx.cs" MasterPageFile="~/MasterPage.master"
    Inherits="StringEncodeDecode.UserAuthentication_PrmUserInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css" title="currentStyle">
        @import "../media/css/demo_page.css";
        @import "../media/css/demo_table_jui.css";
        @import "../examples_support/themes/smoothness/jquery-ui-1.8.4.custom.css";
    </style>
    <script type="text/javascript" language="javascript" src="../media/js/jquery.dataTables.js"></script>
    <script language="javascript" type="text/javascript">
        function SelectAllUsers(chk) {
            $('#<%=grdEmpInfo.ClientID%>').find("input:checkbox").each(function () {
                if (this != chk) {
                    this.checked = chk.checked;
                }
            });

        }

        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {

                //alert($('#quicksearch').html());
                var qhtml = $('#quicksearch').html();
                //alert($('#quicksearch').html());
                if (qhtml != null) {
                    //alert('remove');
                    $('#quicksearch').remove();
                    $('#<%=grdEmpInfo.ClientID %> tbody tr').quicksearch({
                        loaderText: '...',
                        labelText: 'Filter: '
                    });
                    $('#quicksearch').hide();
                }
            }
            $('#quicksearch').hide();
            $('#<%=grdEmpInfo.ClientID %>').dataTable({
                                "iDisplayLength": 20,
                                "aLengthMenu": [[10, 25, 50, 100, -1], [10, 25, 50, 100, "All"]],
                //                //                 "sPaginationType": "full_numbers"
                "bJQueryUI": true,
                                "sPaginationType": "full_numbers",
                //"bStateSave": true,

                "sScrollY": "350px",
                "bPaginate": true,
                "bScrollCollapse": true,
                "aoColumnDefs": [{ "bSortable": false, "aTargets": [2] }]
            });

        }
    </script>
    <style type="text/css">
        select {
            width: 60px;
        }

        input[type="text"], input[type="password"] {
            width: 100px;
        }

        .nopad {
            padding-left: 2px;
            padding-right: 10px !important;
        }
    </style>
    <script src="CreateUser.js" type="text/javascript"></script>

    <style type="text/css">
        myDiv {
            border: 2px solid #0094ff;
            -webkit-border-top-left-radius: 6px;
            -webkit-border-top-right-radius: 6px;
            -moz-border-radius-topleft: 6px;
            -moz-border-radius-topright: 6px;
            border-top-left-radius: 6px;
            border-top-right-radius: 6px;
            width: 300px;
            font-size: 12pt; /* or whatever */
        }

        .myDiv h2 {
            padding: 4px;
            color: #fff;
            margin: 0;
            background-color: #0094ff;
            font-size: 12pt; /* or whatever */
        }

        .myDiv p {
            padding: 4px;
        }
    </style>
    <div id="loading">
        <div class="loading-indicator">
            Page Loading...
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="tabsMain" style="height: 350px; width:1200px">
        <ul>
            <li><a href="#tabs-1">Contact US History</a></li>
        </ul>
        <div id="tabs-1">
            <div class="configurationPage">
                <div class="dataGridStyle">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grdEmpInfo" runat="server" AutoGenerateColumns="False" EnableViewState="True"
                                CssClass="gridview  display" AlternatingRowStyle-CssClass="gridviewaltrow" DataKeyNames="CUAutoId"
                                AllowSorting="true" DataSourceID="SqlDataSource1" Font-Size="12px"
                                Width="100%">
                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="Board">
                                        <ItemTemplate>
                                            <asp:Label ID="grdlblLastName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Footage">
                                        <ItemTemplate>
                                            <asp:Label ID="grdlblDepartment" runat="server" Text='<%# Bind("Company") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Price">
                                        <ItemTemplate>
                                            <asp:Label ID="grdlblRole" runat="server" Text='<%# Bind("insertdate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                </Columns>
                                <EmptyDataTemplate>
                                    <span class="gridviewEmptyMsg">
                                        <%= Resources.Resource.GridViewEmptyDataMsg %>
                                    </span>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <div id="allHiddenField">
        <asp:HiddenField ID="hidMenuID" runat="server" />
    </div>
</asp:Content>
