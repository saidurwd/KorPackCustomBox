<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CalculationHistrory.aspx.cs" MasterPageFile="~/MasterPage.master"
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
                "bScrollCollapse": true
                //"aoColumnDefs": [{ "bSortable": false, "aTargets": [2] }]
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
    <div id="tabsMain" style="height: 350px">
        <ul>
            <li><a href="#tabs-1">Browse History</a></li>
        </ul>
        <div id="tabs-1">
            <div class="configurationPage">
                <div class="dataGridStyle">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grdEmpInfo" runat="server" AutoGenerateColumns="False" EnableViewState="True"
                                CssClass="gridview  display" AlternatingRowStyle-CssClass="gridviewaltrow" DataKeyNames="CBHAutoId"
                                AllowSorting="true" DataSourceID="SqlDataSource1"
                                Width="100%" Font-Size="11px">
                                <Columns>
                                    <asp:TemplateField HeaderText="User">
                                        <ItemTemplate>
                                            <asp:Label ID="grdlblLastName" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer">
                                        <ItemTemplate>
                                            <asp:Label ID="grdlblDepartment" runat="server" Text='<%# Bind("CustomersCId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Time">
                                        <ItemTemplate>
                                            <asp:Label ID="grdlblRole" runat="server" Text='<%# Bind("insertdate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Length">
                                        <ItemTemplate>
                                            <asp:Label ID="grdlblLength" runat="server" Text='<%# Bind("Length") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Width">
                                        <ItemTemplate>
                                            <asp:Label ID="grdlblWidth" runat="server" Text='<%# Bind("Width") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Height">
                                        <ItemTemplate>
                                            <asp:Label ID="grdlblHeight" runat="server" Text='<%# Bind("Height") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Style">
                                        <ItemTemplate>
                                            <asp:Label ID="grdlblCustomer" runat="server" Text='<%# Bind("StyleCId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Board">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLoginID" runat="server" Text='<%# Bind("StrengthCId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                                                        
                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgrdActiveDeactive" runat="server" Text='<%# Bind("Qty") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="FlipCorrDir">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFlipCorrDir" runat="server" Text='<%# Bind("FlipCorrDir") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Price(EA)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFinalEachPrice" runat="server" Text='<%# Bind("FinalEachPrice") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalForThisQty" runat="server" Text='<%# Bind("TotalForThisQty") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quote">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuoteRequested" runat="server" Text='<%# Bind("QuoteRequested") %>'></asp:Label>
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
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Cnn %>"
            SelectCommand="SELECT CBHAutoId, Qty, convert(numeric(6,2), Length) Length, convert(numeric(6,2), Width) Width, convert(numeric(6,2), Height) Height, S.StyleCId, B.StrengthCId,
                 case when FlipCorrDir=1 then 'Yes'
	                else 'No' end FlipCorrDir,
                 CC.CustomersCId,  OverLap, Truck, 
                 case when Perforated='1' then 'Yes' else 'No' end Perforated,
                 U.UserName , FinalEachPrice, TotalForThisQty, dateadd(hour,-6,insertdate) insertdate, 
				 case when isnull(QuoteSent,0)=1 then 'Quote Requested'
					else '' end QuoteRequested
                FROM          CalculateBoxPrice_History C
                inner join [dbo].[CB_Style] S on S.StyleId=C.StyleId
                inner join [dbo].[CB_Strength] B on B.StrengthId=C.Strength
                left join [dbo].[CB_Customers] CC on CC.CustomersId=C.CustomersId
                inner join [dbo].[TB_PrmUserInfo] U on U.UserID=C.UserID
                where insertdate >=  DATEADD(day,-7,GETDATE()) order by insertdate desc">
        </asp:SqlDataSource>
        <asp:HiddenField ID="txtEmployeeID" runat="server" Value="0" />
        <asp:HiddenField ID="txtUserID0" runat="server" Value="0" />
        <asp:HiddenField ID="txtSelectedEmpID" runat="server" Value="0" />
        <asp:HiddenField ID="hidMenuID" runat="server" />
    </div>
</asp:Content>
