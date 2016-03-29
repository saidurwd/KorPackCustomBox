<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CalculatePrice.aspx.cs" Inherits="Sales_DirGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function resetValues() {
            $('#ctl00_ContentPlaceHolder1_txtSquareFootEach').val(0);
            $('#ctl00_ContentPlaceHolder1_txtTotalFootEach').val(0);
            $('#ctl00_ContentPlaceHolder1_txtCWidth').val(0);
            $('#ctl00_ContentPlaceHolder1_txtCLength').val(0);

            $('#ctl00_ContentPlaceHolder1_HowmanyOut').text(0);
            $('#ctl00_ContentPlaceHolder1_stdglue').text(0);
            $('#ctl00_ContentPlaceHolder1_glueupcharge').text(0);
            $('#ctl00_ContentPlaceHolder1_totalGlue').text(0);

            $('#ctl00_ContentPlaceHolder1_EstimatedGlueTime').text(0);
            $('#ctl00_ContentPlaceHolder1_A125').text(0);
            $('#ctl00_ContentPlaceHolder1_upcharge').text(0);
            $('#ctl00_ContentPlaceHolder1_EstimatedCutTime').text(0);

            $('#ctl00_ContentPlaceHolder1_EstimatedGlueTimeHours').text(0);
            $('#ctl00_ContentPlaceHolder1_EstimatedCutTimeHours').text(0);
            $('#ctl00_ContentPlaceHolder1_ChargableSqft').text(0);
            $('#ctl00_ContentPlaceHolder1_ChargableTotalSqft').text(0);

            $('#ctl00_ContentPlaceHolder1_TotalFreight').text(0);
            $('#ctl00_ContentPlaceHolder1_MSFUsed').text(0);
            $('#ctl00_ContentPlaceHolder1_FreightPerea').text(0);
            $('#ctl00_ContentPlaceHolder1_MaterialCharge').text(0);

            $('#ctl00_ContentPlaceHolder1_LaborCharge').text(0);
            $('#ctl00_ContentPlaceHolder1_SetupPerBox').text(0);
            $('#ctl00_ContentPlaceHolder1_FinalEachPrice').text(0);
            $('#ctl00_ContentPlaceHolder1_TotalForThisQty').text(0);
        }
        function bindOnKeyUp1() {
            resetValues();
            var _Style = $('#ctl00_ContentPlaceHolder1_lblStyle').text(); //$('#ctl00_ContentPlaceHolder1_ddlStyle').val();

            if (_Style == "2") {
                $('#divBkf').show();
            }
            else {
                $('#divBkf').hide();
            }
            if (_Style == "3") {
                $('#lblH').hide();
                $('#divX').hide();
                $('#ctl00_ContentPlaceHolder1_txtheight').hide();
                $('#llbL').text('W Corr Dir.');
                $('#lblW').text('L');
            }
            else {
                $('#lblH').show();
                $('#divX').show();
                $('#ctl00_ContentPlaceHolder1_txtheight').show();
                $('#llbL').text('L');
                $('#lblW').text('W');
            }
            return false;
        }

        function bindOnKeyUp() {
            resetValues();
            var _UserID = '<% =Session["UserID"] %>';

            if (_UserID.length == 0)
            { alert('Please Sign In to calculate price. Thank you.'); return false; }
            else {

                var _Qty = $('#ctl00_ContentPlaceHolder1_txtQty').val();
                if ((_Qty.length == 0) || (_Qty == "0")) {
                    return;
                }
                var _Length = $('#ctl00_ContentPlaceHolder1_txtLength').val();
                if ((_Length.length == 0) || (_Length == "0")) {
                    return;
                }
                var _Width = $('#ctl00_ContentPlaceHolder1_txtWidth').val();
                if ((_Width.length == 0) || (_Width == "0")) {
                    return;
                }
                var _Height = $('#ctl00_ContentPlaceHolder1_txtheight').val();
                if ((_Height.length == 0) || (_Height == "0")) {
                    _Height = 0;
                }
                var _Style = $('#ctl00_ContentPlaceHolder1_lblStyle').text(); //$('#ctl00_ContentPlaceHolder1_ddlStyle').val();
                //var _Style = reply_click(this.id);
                var _BroadGrade = $('#ctl00_ContentPlaceHolder1_ddlBroadGrade').val();
                var _Flip = $('#ctl00_ContentPlaceHolder1_ddlFlip').val();
                //var _Customer = $('#ctl00_ContentPlaceHolder1_ddlCustomer').val();
                var _Customer = $('#ctl00_ContentPlaceHolder1_lblCustomer').text();

                var _Truck = $('#ctl00_ContentPlaceHolder1_txtTruck').val();
                if ((_Truck.length == 0) || (_Truck == "0")) {
                    _Truck = 0;
                }
                var _OveLap = $('#ctl00_ContentPlaceHolder1_txtOveLap').val();
                if ((_OveLap.length == 0) || (_OveLap == "0")) {
                    _OveLap = 0;
                }
                var _Perforated = $('#ctl00_ContentPlaceHolder1_ddlPerforated').val();
                if (_Style == "2") {
                    $('#divBkf').show();
                }
                else {
                    $('#divBkf').hide();
                }
                var _Image = $('#ctl00_ContentPlaceHolder1_Image').val();

                if (_Style == "3") {
                    $('#lblH').hide();
                    $('#divX').hide();
                    $('#ctl00_ContentPlaceHolder1_txtheight').hide();
                    $('#llbL').text('W Corr Dir.');
                    $('#lblW').text('L');
                }
                else {
                    $('#lblH').show();
                    $('#divX').show();
                    $('#ctl00_ContentPlaceHolder1_txtheight').show();
                    $('#llbL').text('L');
                    $('#lblW').text('W');
                }
                var _RoleID = '<% =Session["RoleID"] %>';
                //alert(_RoleID);
                var _callFrom = "0";
                var _PCAutoId = "0";
                if ((_RoleID == "18") || (_RoleID == "4")) {
                    _callFrom = "1";
                    _PCAutoId = $('#ctl00_ContentPlaceHolder1_ddlPriceClass').val();
                }
                else {
                    _callFrom = "0";
                    _PCAutoId = "0";
                }
                //alert(_PCAutoId);
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "../WebService/WebService.asmx/GetCalculations",
                    data: "{ Qty: '" + _Qty + "', Length: '" + _Length + "', Width: '" + _Width + "',Height: '" + _Height + "',StyleId: '" + _Style + "',Strength: '" + _BroadGrade + "',FlipCorrDir: '" + _Flip + "',CustomersId: '" + _Customer + "',OverLap: '" + _OveLap + "',Truck: '" + _Truck + "',Perforated:'" + _Perforated + "',callFrom: '" + _callFrom + "',PCAutoId:'" + _PCAutoId + "'}",
                    dataType: "json",
                    success: function (data) {
                        $('#ctl00_ContentPlaceHolder1_FinalEachPrice').text(data.FinalEachPrice);
                        $('#ctl00_ContentPlaceHolder1_TotalForThisQty').text(data.TotalForThisQty);
                        $('#ctl00_ContentPlaceHolder1_lblCBHAutoId').text(data.CBHAutoId);

                        //$('#ctl00_ContentPlaceHolder1_FinalEachPrice').text(data[22]);
                        //$('#ctl00_ContentPlaceHolder1_TotalForThisQty').text(data[23]);
                        //$('#ctl00_ContentPlaceHolder1_lblCBHAutoId').text(data[24]);
                    },
                    failure: function (data) {
                        alert(data);
                    }
                });
                return false;
            }
        }
        function reply_click(clicked_id, stylename) {
            //lblStyleSelected
            $('#defaultImg').hide();
            $('#imgLoad').html("");
            $('#imgLoad').append("<img src='../Images/box/" + clicked_id + "-L.png' height='250px' width='250px' />");
            $('#ctl00_ContentPlaceHolder1_lblStyle').text(clicked_id);
            $('#ctl00_ContentPlaceHolder1_lblStyleSelected').text(stylename);
            bindOnKeyUp1();
            //return clicked_id;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                <div class="well well-lg" style="height: 445px;">
                    Our on demand Quickor machine provides you with flexibility that before was only
                    a dream. We can now create Boxes in a matter of seconds to your specific dimensions.
                    Don’t worry about minimums, tooling or lead time. 90% of our Quickor orders are
                    able to be delivered the next business day if not the same day!
                    <br />
                    <br />
                    Should you need a quote however for a large run, don’t worry, click <a href="mailto:quotes@korpack.com">
                        here</a> and send us a quote, because we excel in that area too.
                </div>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="bs-callout bs-callout-info">
                    <h4>
                        Choose Your Box Style</h4>
                    <div class="row">
                        <div class="col-md-12" style="text-align: center">
                            <img src="../Images/box/1.png" id="1" alt="RSC" width="100" onclick="reply_click(this.id, this.title)"
                                title="RSC" />
                            <img src="../Images/box/2.png" id="2" alt="BKF" width="100" onclick="reply_click(this.id, this.title)"
                                title="Bookfold" />
                            <img src="../Images/box/4.png" id="4" alt="FOL" width="100" onclick="reply_click(this.id, this.title)"
                                title="FOL" />
                            <img src="../Images/box/5.png" id="5" alt="HSC" width="100" onclick="reply_click(this.id, this.title)"
                                title="HSC" />
                            <img src="../Images/box/3.png" id="3" alt="Pad" width="100" onclick="reply_click(this.id, this.title)"
                                title="Pad/Scored Pad" />
                            <img src="../Images/box/10.png" id="10" alt="5PF" width="100" onclick="reply_click(this.id, this.title)"
                                title="5 Panel Folder" />
                            <img src="../Images/box/7.png" id="7" alt="Tele Top" width="100" onclick="reply_click(this.id, this.title)"
                                title="Tele Tray Set" />
                            <%--<img src="../Images/box/6.jpg" id="6" alt="Tele Btm" width="68" onclick="reply_click(this.id, this.title)" title="Tray Bottom" />--%>
                            <%--<img src="../Images/box/8.jpg" id="8" alt="Tube" width="68" onclick="reply_click(this.id, this.title)" title="Tube" />
                            <img src="../Images/box/9.jpg" id="9" alt="HSC FOL BTM" width="68" onclick="reply_click(this.id, this.title)" title="HSC" />
                            <img src="../Images/box/11.jpg" id="11" alt="STD 5PF" width="68" onclick="reply_click(this.id, this.title)" title="STD 5PF" />--%>
                        </div>
                    </div>
                </div>
                <br />
                <div class="bs-callout bs-callout-danger">
                    <h4>
                        Inside Dimensions</h4>
                    <div class="row" runat="server" id="rwPriceClass">
                        <div class="col-md-2 text-right">
                            Price Class:
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlPriceClass" runat="server" CssClass="form-control input-sm"
                                onchange="javaScript:bindOnKeyUp1()">
                                <asp:ListItem Text="0" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 text-right">
                            Quantity:
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtQty" runat="server" CssClass="form-control input-sm" onkeyup="javaScript:bindOnKeyUp1();"></asp:TextBox>
                        </div>
                        <div class="col-md-1 text-right" style="display: none">
                            Style:
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlStyle" runat="server" CssClass="form-control input-sm" onchange="javaScript:bindOnKeyUp1()"
                                Style="display: none">
                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 text-right labelMargin">
                            Size:
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label for="Quantity" id="llbL" class="text-center">
                                    L</label>
                                <asp:TextBox ID="txtLength" runat="server" CssClass="form-control input-sm" onkeyup="javaScript:bindOnKeyUp1();"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1 text-center labelMargin">
                            X</div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label for="width" id="lblW" class="text-center">
                                    W</label>
                                <asp:TextBox ID="txtWidth" runat="server" CssClass="form-control input-sm" onkeyup="javaScript:bindOnKeyUp1();"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1 text-center labelMargin" id="divX">
                            X</div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label for="Height" id="lblH" class="text-center">
                                    H</label>
                                <asp:TextBox ID="txtheight" runat="server" CssClass="form-control input-sm" onkeyup="javaScript:bindOnKeyUp1();"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 text-right">
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label for="BoardGrade">
                                    Board Grade</label>
                                <asp:DropDownList ID="ddlBroadGrade" runat="server" CssClass="form-control input-sm"
                                    onchange="javaScript:bindOnKeyUp1()">
                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 text-right">
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label for="BoardGrade">
                                    Flip Corr Dir.</label>
                                <asp:DropDownList ID="ddlFlip" runat="server" CssClass="form-control input-sm" onchange="javaScript:bindOnKeyUp1()">
                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row" id="divBkf" style="display: none">
                        <div class="col-md-1 text-right">
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label for="BoardGrade">
                                    Perforated?</label>
                                <asp:DropDownList ID="ddlPerforated" runat="server" CssClass="form-control input-sm"
                                    onchange="javaScript:bindOnKeyUp1()">
                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 text-right">
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label for="BoardGrade">
                                    Tuck</label>
                                <asp:TextBox ID="txtTruck" runat="server" CssClass="form-control input-sm" onkeyup="javaScript:bindOnKeyUp1();"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1 text-right">
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label for="BoardGrade">
                                    OverLap</label>
                                <asp:TextBox ID="txtOveLap" runat="server" CssClass="form-control input-sm" onkeyup="javaScript:bindOnKeyUp1();"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1 text-right">
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <asp:Label ID="lblStyleSelected" runat="server" Text="RSC" CssClass=""></asp:Label></h3>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center" id="imgLoad" style="height: 250px;">
                            <img src="../Images/box/1.png" alt="Box" id="defaultImg" height="250px" width="250px" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-7 text-right">
                            <h5>
                                Final Each Price:</h5>
                        </div>
                        <div class="col-md-5 amountText">
                            <asp:Label ID="FinalEachPrice" runat="server" Text="0.00" CssClass=""></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-7 text-right">
                            <h5>
                                Total For This Qty.:</h5>
                        </div>
                        <div class="col-md-5 amountText">
                            <asp:Label ID="TotalForThisQty" runat="server" Text="0.00" CssClass=""></asp:Label>
                            <asp:Label ID="lblCBHAutoId" runat="server" Text="0.00" CssClass="" Style="display: none"></asp:Label>
                        </div>
                    </div>
                    <div class="row" style="display: none;">
                        <asp:Label ID="lblCustomer" runat="server" Text="0" Style="color: yellow; font-size: 16px;
                            display: none"></asp:Label>&nbsp;
                        <asp:Label ID="lblStyle" runat="server" Text="1" Style="color: yellow; font-size: 16px;
                            display: none"></asp:Label>&nbsp;
                        <div class="col-md-12">
                            <div id="columns">
                                <ul id="column1" class="column" runat="server">
                                    <asp:GridView ID="GridView1" runat="server" ShowHeader="true" AutoGenerateColumns="true"
                                        CssClass="gridview display" AlternatingRowStyle-CssClass="gridviewaltrow" Width="100%">
                                    </asp:GridView>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-7">
                        <asp:Button ID="Button3" runat="server" Text="Calculate" Style="color: red" OnClientClick="javaScript: return bindOnKeyUp();" />
                    </div>
                    <div class="col-md-5">
                        <asp:Button ID="Button4" runat="server" Text="Email My Quote" OnClick="btnProcessPO_Click" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-5" style="display: none">
                        <asp:Button ID="Button1" runat="server" Text="Calculate" OnClick="btnProcessPO_Click" />
                        <asp:Button ID="Button2" runat="server" Text="Email My Quote" OnClick="btnProcessPO_Click" />
                    </div>
                </div>
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
