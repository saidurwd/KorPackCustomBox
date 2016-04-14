<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ManageCustomers.aspx.cs" Inherits="UA_ManageRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function fromPopUp(val1, val2, val3, val4, val5, val6) {
            //            alert(val1);
            //            alert(val2);

            $('#<%=HidCID.ClientID %>').val(val1);
            $('#<%=HidCategoryName.ClientID %>').val(val2);
            $('#<%=txtDescription.ClientID %>').val(val2);
            $('#<%=txtFee.ClientID %>').val(val3);
            $('#<%=ddlSalesPerson.ClientID %>').val(val4);
            $('#<%=ddlPriceClass.ClientID %>').val(val5);
            $('#<%=txtTerms.ClientID %>').val(val6);
            tb_remove();
        }

        function refreshParent(val1, val2, val3, val4, val5, val6) {
            //alert("val of :" + val);
            //tb_remove();
            fromPopUp(val1, val2, val3, val4, val5, val6);
        }
        function ShowConfirmation() {

            var Returnval = false;
            var answer = confirm("Are you sure to delete?")
            if (answer) {
                Returnval = true;
            }
            else {
                Returnval = false;
            }
            return Returnval;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Manage Customer</h3>
            </div>
            <div class="panel-body form-horizontal">
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2 control-label">Customer</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-sm-4">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="ReqFieldCSS"
                            ControlToValidate="txtDescription" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="valSaveDB">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2 control-label">Fee</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtFee" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-sm-4">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="ReqFieldCSS"
                            ControlToValidate="txtFee" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="valSaveDB">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2 control-label">Sales Person</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlSalesPerson" runat="server" DataSourceID="SqlDataSource2" CssClass="form-control"
                            DataTextField="SPName" DataValueField="SPAutoId">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2 control-label">Price Class</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlPriceClass" runat="server" DataSourceID="SqlDataSource3" CssClass="form-control"
                            DataTextField="PriceClassDesc" DataValueField="PCAutoId">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label for="inputEmail3" class="col-sm-2 control-label">Terms</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtTerms" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-4">
                        <asp:Button ID="btnUpdate" runat="server" Text="Save" OnClick="btnUpdate_Click" ValidationGroup="valSaveDB" CssClass="btn btn-default btn-sm" />
                    </div>
                </div>
                <asp:Panel ID="Panel2" CssClass="gridPanelSalary" runat="server" Width="99%" Height="350px"
                    ScrollBars="Auto">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="gridview table table-striped table-bordered table-responsive"
                        AlternatingRowStyle-CssClass="gridviewaltrow" Width="100%" DataKeyNames="CustomersId"
                        AllowSorting="true" DataSourceID="SqlDataSource1">
                        <Columns>
                            <asp:TemplateField HeaderText="Select" ControlStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkRow" />
                                    <asp:TextBox runat="server" ID="txtKeyField" Text='<%#Eval("CustomersId") %>' Style="display: none" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer" SortExpression="CustomersCId">
                                <ItemTemplate>
                                    <a href="javaScript:refreshParent('<%#Eval("CustomersId") %>','<%#Eval("CustomersCId") %>','<%#Eval("Amount") %>','<%#Eval("SPAutoId") %>','<%#Eval("PCAutoId") %>','<%#Eval("Terms") %>');">
                                        <%# Eval("CustomersCId")%>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fee" SortExpression="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sales Person" SortExpression="SPName">
                                <ItemTemplate>
                                    <asp:Label ID="lblSalesPerson" runat="server" Text='<%#Eval("SPName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Price Class" SortExpression="PriceClassDesc">
                                <ItemTemplate>
                                    <asp:Label ID="lblPriceClassDesc" runat="server" Text='<%#Eval("PriceClassDesc") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Terms" SortExpression="Terms">
                                <ItemTemplate>
                                    <asp:Label ID="lblTerms" runat="server" Text='<%#Eval("Terms") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                <div style="padding-top: 10px; text-align: left;">
                    <div class="buttonSection" style="text-align: left; padding-left: 20px">
                        <asp:Button ID="Button2" runat="server" Text="Delete" OnClientClick="return ShowConfirmation();" CssClass="btn btn-danger btn-xs"
                            OnClick="cmdDelete_Click" />
                    </div>
                </div>
                <div class="hiddenFields">
                    <asp:HiddenField ID="HidCID" runat="server" />
                    <asp:HiddenField ID="HidCategoryName" runat="server" />
                    <asp:HiddenField ID="hidMenuID" runat="server" />
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Cnn %>"
                        SelectCommand="SELECT     CustomersId, CustomersCId, Amount, C.SPAutoId, S.SPName, P.PCAutoId, P.PriceClassDesc, C.Terms
                    FROM         [dbo].[CB_Customers] C
                    left join CB_SalesPerson S on S.SPAutoId=C.SPAutoId
                    left join CB_PriceClass P on C.PCAutoId=P.PCAutoId
                    order by CustomersCId"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Cnn %>"
                        SelectCommand="
            SELECT        SPAutoId, SPName
            FROM            CB_SalesPerson
            ORDER BY SPName"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Cnn %>"
                        SelectCommand="
            SELECT        PCAutoId, PriceClassDesc
            FROM           [dbo].[CB_PriceClass]
            ORDER BY PriceClassDesc"></asp:SqlDataSource>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
