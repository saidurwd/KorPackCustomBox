<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContactUSHistory.aspx.cs" MasterPageFile="~/MasterPage.master"
    Inherits="StringEncodeDecode.UserAuthentication_PrmUserInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">    
    <%--<script type="text/javascript" language="javascript" src="../media/js/jquery.dataTables.js"></script> --%>
    <script type="text/javascript" charset="utf8" src="//cdn.datatables.net/1.10.2/js/jquery.dataTables.js"></script>
    <script type="text/javascript" charset="utf8" src="//cdn.datatables.net/1.10.11/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" charset="utf8" src="//cdn.datatables.net/1.10.11/js/dataTables.bootstrap.min.js"></script>
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.11/css/dataTables.bootstrap.min.css">
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
            $('#<%=grdEmpInfo.ClientID %>').DataTable({
                "scrollX": true
            });
        }
    </script>
    <script src="CreateUser.js" type="text/javascript"></script>
    <div id="loading">
        <div class="loading-indicator">
            Page Loading...
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Contact US History</h3>
            </div>
            <div class="panel-body">
                <div class="dataGridStyle">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grdEmpInfo" runat="server" AutoGenerateColumns="False" EnableViewState="True"
                                CssClass="gridview  display table table-striped table-bordered" AlternatingRowStyle-CssClass="gridviewaltrow" DataKeyNames="CUAutoId"
                                AllowSorting="true" DataSourceID="SqlDataSource1" Font-Size="12px"
                                Width="100%">
                                <Columns>

                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="grdlblLastName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company">
                                        <ItemTemplate>
                                            <asp:Label ID="grdlblDepartment" runat="server" Text='<%# Bind("Company") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Time">
                                        <ItemTemplate>
                                            <asp:Label ID="grdlblRole" runat="server" Text='<%# Bind("insertdate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Street">
                                        <ItemTemplate>
                                            <asp:Label ID="grdlblCustomer" runat="server" Text='<%# Bind("Street") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="City">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLoginID" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="State">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgrdActiveDeactive" runat="server" Text='<%# Bind("State") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ZipCode">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFinalEachPrice" runat="server" Text='<%# Bind("ZipCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Phone">
                                        <ItemTemplate>

                                            <asp:Label ID="lblTotalForThisQty" runat="server" Text='<%# Bind("Phone") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuoteRequested" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Subject">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubject" runat="server" Text='<%# Bind("Subject") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Message">
                                        <ItemTemplate>
                                            <%# Eval("Message")%>
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
            SelectCommand="SELECT       CUAutoId, Name, Company, Street, City, State, ZipCode, Phone, Email, Subject, Message, insertdate
                    FROM            CB_ContactUS
                where insertdate >=  DATEADD(day,-7,GETDATE()) order by insertdate desc">
        </asp:SqlDataSource>
        <asp:HiddenField ID="txtEmployeeID" runat="server" Value="0" />
        <asp:HiddenField ID="txtUserID0" runat="server" Value="0" />
        <asp:HiddenField ID="txtSelectedEmpID" runat="server" Value="0" />
        <asp:HiddenField ID="hidMenuID" runat="server" />
    </div>
</asp:Content>
