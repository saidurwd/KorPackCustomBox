/// <reference path="jquery-1.4.2-vsdoc.js" />
$(function () {
    function log(message) {
        $("<div/>").text(message).prependTo("#log");
        $("#log").attr("scrollTop", 0);
        alert(message);
        //fetchPartyInformation(message);
    }

    $(".partyName").autocomplete({
        // TODO doesn't work when loaded from /demos/#autocomplete|remote
        source: "../WebService/PartyInfo.ashx",
        minLength: 1,
        select: function (event, ui) {
            //log(ui.item ? ("Selected: " + ui.item.value + " aka " + ui.item.id) : "Nothing selected, input was " + this.value);
            dateParseandAdd(ui.item.id);
            //alert(ui.item.id);
        }
    });


    var cache = {};

    function bindOnKeyUp() {
        alert('DDFDFD');
        var storeid = $('#ctl00_ContentPlaceHolder1_ddlLocation').val();
        //alert(storeid);
        $("#ctl00_ContentPlaceHolder1_txtItemName").autocomplete(
        {
            source: "../WebService/SearchItemName.ashx?storeid=" + storeid,
            minLength: 1,
            focus: function (event, ui) {
                $('#ctl00_ContentPlaceHolder1_txtItemName').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                return false;
            },
            select: function (event, ui) {
                findItemID(ui.item.id);
                $('#ctl00_ContentPlaceHolder1_txtItemName').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                $('#ctl00_ContentPlaceHolder1_txtItemName').val(decodeURIComponent(ui.item.value.replace(/\+/g, " ")));
                return false;
                //alert(decodeURIComponent(ui.item.value.replace(/\+/g, " ")));
            }

        }).data("autocomplete")._renderItem = function (ul, item) {
            var splitString = item.id.split("-");
            return $("<li></li>")
				.data("item.autocomplete", item)
				.append("<span style='float:right;'>" + decodeURIComponent(splitString[2].replace(/\+/g, " ")) + "</span><a>" + decodeURIComponent(item.label.replace(/\+/g, " ")) + "</a>")
				.appendTo(ul);
        };

    }

    $('#ctl00_ContentPlaceHolder1_ddlSearchBy').change(function () {
        //alert('dfds');
        if ($(this).val() == 'Date') {
            $('#fromDate').show();
            $('#toDate').show();
            $('#searchRow').hide();
        }
        else {
            //$('#<%=txtSearchText.ClientID %>').show();
            $('#searchRow').show();
            $('#fromDate').hide();
            $('#toDate').hide();
        }
    });








});



function findItemID(data) {
    //alert("Item Data:" + data);
    var splitString = data.split("-");
    var quantity = $('#ctl00_ContentPlaceHolder1_txtQuantity').val();
    //alert("1st data :" + splitString[0]);
    //alert("Item quantity:" + quantity);
    if (splitString[0] != 'undefined') {
        $('#ctl00_ContentPlaceHolder1_hidItemID').val(splitString[0]);
    }
    //alert("2nd data :" + splitString[1]);
    if (splitString[1] != 'undefined') {
        $('#ctl00_ContentPlaceHolder1_lblPer').text(splitString[1]);
        $('#ctl00_ContentPlaceHolder1_HidPer').val(splitString[1]);
        $('#ctl00_ContentPlaceHolder1_txtUnit').val(splitString[1]);
        $('#ctl00_ContentPlaceHolder1_hidItemType').val(splitString[4]);

        var splitString2 = splitString[2].split("+");
        //alert(splitString[3]); - serial tracking
        //alert(splitString2[0]);
        $('#ctl00_ContentPlaceHolder1_hidBalance').val(splitString2[0]);
        if (splitString[3] == 1) {
            $('#ctl00_ContentPlaceHolder1_lblSerial').show();
        }
        else {
            $('#ctl00_ContentPlaceHolder1_lblSerial').hide();
        }


    }
    if ((quantity != 'undefined') && (quantity.length > 0)) {

        //alert(quantity.length);
        fetchRateAgainstQuantity('Add', '1');

    }
}

function dateParseandAdd(valdate) {
    //alert(valdate);

    var validateArray = valdate.split("-");
    if (validateArray[0].length > 0) {
        //alert(validateArray[0]);
        $('#ctl00_ContentPlaceHolder1_HidPartyID').val(validateArray[0]);
    }
    if (validateArray[1].length > 0) {
        $('#ctl00_ContentPlaceHolder1_txtDueDate').val(validateArray[1]);
    }
    else {
        var cDate = $('#ctl00_ContentPlaceHolder1_ txtDate').val();
        $('#ctl00_ContentPlaceHolder1_txtDueDate').val(cDate);
    }
    if (validateArray[2].length > 0) {
        $('#ctl00_ContentPlaceHolder1_HidPriceLevelName').val(validateArray[2]);
        //alert('price level'+ validateArray[2]);
    }

}

function fetchPartyInformation(partyID) {
    alert(partyID);
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../WebService/SalesOrder.asmx/PartyInfoDetails",
        data: "{ PartyID: '" + partyID + "'}",
        dataType: "json",
        success: function (data) {
            //$("#CustomerDetails").html(data.d);
            alert(data);
        }
    });
}
function ShowSerialTracking(ctrl1) {

    var itemName = $('#ctl00_ContentPlaceHolder1_txtItemName').val();
    var itemID = $('#ctl00_ContentPlaceHolder1_hidItemID').val();
    var itemQty = $('#ctl00_ContentPlaceHolder1_txtQuantity').val();
    var orderID = $('#ctl00_ContentPlaceHolder1_txtSalesOrderNo').val();
    var _InOut = $('#ctl00_ContentPlaceHolder1_hidInOut').val();

    if (_InOut == 1) {
        tb_show('Item Serial', '../Sales/InvItemSerialOut.aspx?itemname=' + encodeURIComponent(itemName) + '&Opening=' + encodeURIComponent(itemQty) + '&hidItemId=' + encodeURIComponent(itemID) + 'TB_iframe=true&height=550&width=800', null);
    }
    else {
        tb_show('Item Serial', '../Sales/InvItemSerialIn.aspx?itemname=' + encodeURIComponent(itemName) + '&Opening=' + encodeURIComponent(itemQty) + '&hidItemId=' + encodeURIComponent(itemID) + 'TB_iframe=true&height=550&width=800', null);
    }
}
function fnCheckItemUnique() {
    var itemID = $('#ctl00_ContentPlaceHolder1_txtName').val();
    var companyID = $('#ctl00_hidCompanyID').val();
    var STOCKITEM_SERIAL = $('#ctl00_ContentPlaceHolder1_HidSTOCKITEM_SERIAL').val();
    var val = false;

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../WebService/SalesOrder.asmx/fnCheckUniqueItem",
        data: "{ itemID: '" + itemID + "',companyID: '" + companyID + "',STOCKITEM_SERIAL: '" + STOCKITEM_SERIAL + "'}",
        //dataType: "json",
        dataType: "html",
        success: function (data) {

            if (data.toString() != "") {
                $('#ctl00_ContentPlaceHolder1_btnUpdate').attr("disabled", true);
                val = false;
                showError(data.toString());
                return false;
            }
            else {
                val = true;
                $('#ctl00_ContentPlaceHolder1_btnUpdate').attr("disabled", false);
            }
        }
    });
    return val;
}

function fnCheckEmpIDIfExists() {
    var _txtEmpID = $('#ctl00_ContentPlaceHolder1_txtEmpCardNo').val();
    var companyID = $('#ctl00_hidCompanyID').val();

    //alert(_txtEmpID);
    //alert(companyID);

    var val = false;

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../WebService/SalesOrder.asmx/fnCheckEmpIDIfExists",
        data: "{ EmpID: '" + _txtEmpID + "',companyID: '" + companyID + "'}",
        //dataType: "json",
        dataType: "html",
        success: function (data) {

            if (data.toString() != "") {
                addMarkClassSaveButton()
                val = false;
                showError(data.toString());
                return false;
            }
            else {
                val = true;
                removeMarkClassSaveButton()
            }
        }
    });
    return val;
}
function fnCheckuUniueEmpID() {
    var _txtEmpID = $('#ctl00_ContentPlaceHolder1_txtEmpID').val();
    var companyID = $('#ctl00_hidCompanyID').val();
    var _hiddEmpAutoID = $('#ctl00_ContentPlaceHolder1_hiddEmpAutoID').val();
    //alert(_txtEmpID);
    //alert(companyID);
    //alert(_hiddEmpAutoID);
    var val = false;

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../WebService/SalesOrder.asmx/fnCheckuUniueEmpID",
        data: "{ EmpID: '" + _txtEmpID + "',companyID: '" + companyID + "',EmpAutoID: '" + _hiddEmpAutoID + "'}",
        //dataType: "json",
        dataType: "html",
        success: function (data) {

            if (data.toString() != "") {
                addMarkClassSaveButton()
                val = false;
                showError(data.toString());
                return false;
            }
            else {
                val = true;
                removeMarkClassSaveButton()
            }
        }
    });
    return val;
}
function fnCheckStoreUnique() {
    var itemID = $('#ctl00_ContentPlaceHolder1_txtName').val();
    var companyID = $('#ctl00_hidCompanyID').val();
    var GODOWNS_SERIAL = $('#ctl00_ContentPlaceHolder1_hidGodownsSl').val();
    var val = false;
    //alert(itemID);
    //alert(companyID);
    //alert(GODOWNS_SERIAL);

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../WebService/SalesOrder.asmx/fnCheckUniqueStore",
        data: "{ itemID: '" + itemID + "',branchID: '" + companyID + "',GODOWNS_SERIAL: '" + GODOWNS_SERIAL + "'}",
        dataType: "html",
        success: function (data) {

            if (data.toString() != "") {
                $('#ctl00_ContentPlaceHolder1_btnUpdate').attr("disabled", true);
                val = false;
                showError(data.toString());
                return false;
            }
            else {
                val = true;
                $('#ctl00_ContentPlaceHolder1_btnUpdate').attr("disabled", false);
            }
        }
    });
    return val;
}

function fnCheckStockCategoryUnique() {
    var itemID = $('#ctl00_ContentPlaceHolder1_txtCategoryName').val();
    var companyID = $('#ctl00_hidCompanyID').val();
    var StockCatSerial = $('#ctl00_ContentPlaceHolder1_HidStockCatSerial').val();
    var val = false;
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../WebService/SalesOrder.asmx/fnCheckStockCategoryUnique",
        data: "{ itemID: '" + itemID + "',companyID: '" + companyID + "',StockCatSerial: '" + StockCatSerial + "'}",
        dataType: "html",
        success: function (data) {
            if (data.toString() != "") {
                $('#ctl00_ContentPlaceHolder1_btnUpdate').attr("disabled", true);
                val = false;
                showError(data.toString());
                return false;
            }
            else {
                val = true;
                $('#ctl00_ContentPlaceHolder1_btnUpdate').attr("disabled", false);
            }
        }
    });
    return val;
}

function fnCheckCustomerNameUnique() {
    var itemID = $('#ctl00_ContentPlaceHolder1_txtFName').val();
    var companyID = $('#ctl00_hidCompanyID').val();
    var LEDGER_SERIAL = $('#ctl00_ContentPlaceHolder1_hidLedgerSl').val();
    var val = false;

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../WebService/SalesOrder.asmx/fnCheckCustomerNameUnique",
        data: "{ itemID: '" + itemID + "',companyID: '" + companyID + "',LEDGER_SERIAL: '" + LEDGER_SERIAL + "'}",
        dataType: "html",
        success: function (data) {
            if (data.toString() != "") {
                $('#ctl00_ContentPlaceHolder1_btnSave').attr("disabled", true);
                val = false;
                showError(data.toString());
                return false;
            }
            else {
                val = true;
                $('#ctl00_ContentPlaceHolder1_btnSave').attr("disabled", false);
            }
        }
    });
    return val;
}
function isVoucherNumerDuplicate(strVoucherType) {

    //alert("Type:" + strVoucherType);

    var newrefno = $('#ctl00_ContentPlaceHolder1_txtSalesOrderNo').val();
    var branch_id = $('#ctl00_ContentPlaceHolder1_ddlBranch').val();
    var VoucherSerial = $('#ctl00_ContentPlaceHolder1_hidVoucherSerial').val();

    //alert("Ref:" + newrefno);
    //alert("Branchid: " + branch_id);
    //alert("Edit Mode?" + VoucherSerial);
    var val = false;
    if (VoucherSerial == "0") {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../WebService/SalesOrder.asmx/isVoucherNumerDuplicate",
            data: "{ vouchertype: '" + strVoucherType + "',branchid: '" + branch_id + "',newrefno: '" + newrefno + "'}",
            dataType: "html",
            success: function (data) {
                if (data.toString() != "") {
                    $('#ctl00_ContentPlaceHolder1_btnSave').attr("disabled", true);
                    addMarkClassSaveButton();
                    val = false;
                    showError(data.toString());
                    return false;
                }
                else {
                    val = true;
                    $('#ctl00_ContentPlaceHolder1_btnSave').attr("disabled", false);
                    removeMarkClassSaveButton();
                }
            }
        });
    }
    return val;
}
function fnCheckGroupNameUnique() {
    var itemID = $('#ctl00_ContentPlaceHolder1_txtGroupName').val();
    var companyID = $('#ctl00_hidCompanyID').val();
    var SERIAL = $('#ctl00_ContentPlaceHolder1_HidGrpSerial').val();
    var val = false;

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../WebService/SalesOrder.asmx/fnCheckGroupNameUnique",
        data: "{ itemID: '" + itemID + "',companyID: '" + companyID + "',SERIAL: '" + SERIAL + "'}",
        dataType: "html",
        success: function (data) {
            if (data.toString() != "") {
                $('#ctl00_ContentPlaceHolder1_btnUpdate').attr("disabled", true);
                val = false;
                showError(data.toString());
                return false;
            }
            else {
                val = true;
                $('#ctl00_ContentPlaceHolder1_btnUpdate').attr("disabled", false);
            }
        }
    });
    return val;
}

function fnCheckLedgerNameUnique() {
    var itemID = $('#ctl00_ContentPlaceHolder1_txtName').val();
    var companyID = $('#ctl00_hidCompanyID').val();
    var LEDGER_SERIAL = $('#ctl00_ContentPlaceHolder1_HidGrpSerial').val();
    var val = false;

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../WebService/SalesOrder.asmx/fnCheckLedgerNameUnique",
        data: "{ itemID: '" + itemID + "',companyID: '" + companyID + "',LEDGER_SERIAL: '" + LEDGER_SERIAL + "'}",
        dataType: "html",
        success: function (data) {

            if (data.toString() != "") {
                $('#ctl00_ContentPlaceHolder1_btnUpdate').attr("disabled", true);
                val = false;
                showError(data.toString());
                return false;
            }
            else {
                val = true;
                $('#ctl00_ContentPlaceHolder1_btnUpdate').attr("disabled", false);
            }
        }
    });
    return val;
}

function fnCheckStockGroupUnique() {

    var companyID = $('#ctl00_hidCompanyID').val();
    var itemID = $('#ctl00_ContentPlaceHolder1_txtGroupName').val();
    var stockGroupSL = $('#ctl00_ContentPlaceHolder1_HidStockGrpSerial').val();
    var val = false;

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../WebService/SalesOrder.asmx/fnCheckStockGroupUnique",
        data: "{ itemID: '" + itemID + "',companyID: '" + companyID + "',stockGroupSL: '" + stockGroupSL + "'}",
        dataType: "html",
        success: function (data) {

            if (data.toString() != "") {
                $('#ctl00_ContentPlaceHolder1_btnUpdate').attr("disabled", true);
                val = false;
                showError(data.toString());
                return false;
            }
            else {
                val = true;
                $('#ctl00_ContentPlaceHolder1_btnUpdate').attr("disabled", false);
            }
        }
    });
    return val;
}

function fnCheckBranchUnique() {
    //alert('aaa');
    var BranchName = $('#ctl00_ContentPlaceHolder1_txtName').val();
    //alert(BranchName);
    var BranchSl = $('#ctl00_ContentPlaceHolder1_hidBranchsSl').val();
    var companyID = $('#ctl00_hidCompanyID').val();
    //alert(BranchSl);
    //alert(companyID);
    var val = false;
    //val = fnUserPermission('ctl00_ContentPlaceHolder1_hidBranchsSl');

    //if (val == false) {
    //  return val;          
    //alert(val);
    //        }
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../WebService/SalesOrder.asmx/fnCheckBranchUnique",
        data: "{ BranchName: '" + BranchName + "',BranchSl: '" + BranchSl + "',companyID: '" + companyID + "'}",
        //dataType: "json",
        dataType: "html",
        success: function (data) {
            //alert(data.toString());
            if (data.toString() != "") {
                $('#ctl00_ContentPlaceHolder1_btnUpdate').attr("disabled", true);
                val = false;
                showError(data.toString());
                return false;
            }
            else {
                val = true;
                $('#ctl00_ContentPlaceHolder1_btnUpdate').attr("disabled", false);
            }

        }
    });

    return val;
}

function fnUserPermission(strAction) {
    var companyID = $('#ctl00_hidCompanyID').val();
    var strUserID = $('#ctl00_hidUserID').val();
    var strMenuID = $('#ctl00_ContentPlaceHolder1_hidMenuID').val();
    //alert(strAction);
    strAction = "#" + strAction;
    strAction = $(strAction).val();
    //alert(strAction);
    if (strAction == "0") {
        strAction = 1;
    }
    else if (strAction == "") {
        strAction = 1;
    }
    else if (strAction != "0" && strAction != "") {
        strAction = 2;
    }

    //alert(companyID);
    //alert(strUserID);
    //alert(strMenuID);
    //alert(strAction);

    var found = true;
    var val = false;
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../WebService/SalesOrder.asmx/fnCheckUserPermission",
        data: "{ UserID: '" + strUserID + "',strMenuID: '" + strMenuID + "',strAction: '" + strAction + "',companyID: '" + companyID + "'}",
        dataType: "html",
        //dataType: "json",
        success: function (data) {
            alert(data.toString());
            val = data.toString();
            if (data.toString() == 'false') {
                found = false;
                showError("Insufficient Previlleges.");
                return false;
            }
            else {
                found = true;
                return true;
            }
        }

    });
    //val = false;
    //alert(found);
    return found;

}



function fnCheckItemUnique12() {
    var itemID = $('#ctl00_ContentPlaceHolder1_txtName').val();
    var companyID = $('#ctl00_ContentPlaceHolder1_hidCompanyID').val();

    if (itemID.length > 0) {
        //alert('called');
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../WebService/SalesOrder.asmx/fnCheckUniqueItem",
            data: "{ itemID: '" + itemID + "',companyID: '" + companyID + "'}",
            dataType: "html",
            success: function (data) {
                alert(data);
                //                var btnShowOrHide = data.split(',');
                //                if (btnShowOrHide[0] == '2') { // = Bill by Bill 
                //                    $('#lnkBillByBillShow').show();
                //                }
                //                else {
                //                    $('#lnkBillByBillShow').hide();
                //                }
                //                if (btnShowOrHide[1] == '2') { // = Cost Center
                //                    $('#lnkCostCenter').show();
                //                }
                //                else {
                //                    $('#lnkCostCenter').hide();
                //                }

            },
            failure: function (data) {
                alert(data);
            }
        });
    }
}



function calculateAmountFun(type) {

    var quantity, rate, amount, discount, Actdiscount, Commission;
    //alert(type);
    if (type == 'Add') {
        quantity = $('#ctl00_ContentPlaceHolder1_txtQuantity').val();
        rate = $('#ctl00_ContentPlaceHolder1_txtRate').val();
        Commission = $('#ctl00_ContentPlaceHolder1_txtCommission').val();
        discount = $('#ctl00_ContentPlaceHolder1_txtDiscount').val();
        //alert(Commission);
    }
    else {
        quantity = $('.txtQuantity').val();
        rate = $('.txtRate').val();
        Commission = $('.txtCommission').val();
        discount = $('.txtDiscount').val();
        //alert(quantity);
    }

    if (discount == undefined) {
        discount = 0;
        //discount = parseFloat(discount.toString());
    }
    var vperecnt = 0;
    var vdiscount = 0;

    if (discount.length != undefined) {
        vperecnt = discount.substring(discount.length - 1, discount.length);
        vdiscount = discount.substring(0, discount.length - 1);
        if (vperecnt == "%") {
            Actdiscount = (vdiscount * amount) / 100;
        }
        else {
            Actdiscount = discount;
        }
    }
    else {
        Actdiscount = discount;
    }

    //alert(Actdiscount);
    if (Commission == "")
    { Commission = 0; }
    if (Commission == undefined)
    { Commission = 0; }

    var calculateAmount = parseFloat(quantity) * parseFloat(rate);
    var calculateAmount2 = parseFloat(quantity) * (parseFloat(rate) + parseFloat(Commission));
    //alert(Commission);
    //alert(calculateAmount2);

    if (discount.length > 0) {
        var netamount = calculateAmount2 - parseFloat(Actdiscount.toString());
    }
    else {
        var netamount = calculateAmount2;
    }
    if (isNaN(calculateAmount) == false) {
        if (type == 'Add') {
            $('#ctl00_ContentPlaceHolder1_txtAmount').val(calculateAmount.toFixed(2));
            $('#ctl00_ContentPlaceHolder1_txtAmountwithCommission').val(calculateAmount2.toFixed(2));
            //$('#ctl00_ContentPlaceHolder1_txtNetAmount').val(calculateAmount2.toFixed(2));
            $('#ctl00_ContentPlaceHolder1_txtNetAmount').val(netamount.toFixed(2));
            $('#ctl00_ContentPlaceHolder1_txtDiscount').val(parseFloat(Actdiscount.toString()));
        }
        else {
            $('.txtAmount').val(calculateAmount.toFixed(2));
            $('.txtAmountwithCommission').val(calculateAmount2.toFixed(2));
            //$('.txtNetAmount').val(calculateAmount2.toFixed(2)); 
            $('.txtNetAmount').val(netamount.toFixed(2));
            $('.txtDiscount').val(parseFloat(Actdiscount.toString()));
        }
    }
}





function CheckForCheckedItem(chk) {
    var found = false;
    $('#ctl00_ContentPlaceHolder1_GridView2').find("input:checkbox").each(function () {
        if (this.checked) {
            found = true;
            //break;
            return;
        }
    });
    //alert(found);
    if (found == false) {
        showInfo('Select atleast 1 from list');
    }
    return found;
}


function bindOnFocusSubType() {
    var storeid = "¢";
    $("#ctl00_ContentPlaceHolder1_txtSubType").autocomplete(
        {
            source: "../WebService/SearchItemName.ashx",
            minLength: 1,
            focus: function (event, ui) {
                $('#ctl00_ContentPlaceHolder1_txtSubType').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                return false;
            },
            select: function (event, ui) {
                findItemID(ui.item.id);
                $('#ctl00_ContentPlaceHolder1_txtSubType').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                $('#ctl00_ContentPlaceHolder1_txtSubType').val(decodeURIComponent(ui.item.value.replace(/\+/g, " ")));
                return false;
            }
        }).data("autocomplete")._renderItem = function (ul, item) {
            var splitString = item.id.split("-");
            //alert(splitString);
            return $("<li></li>")
				.data("item.autocomplete", item)
				.append("<span style='float:right;'></span><a>" + decodeURIComponent(item.label.replace(/\+/g, " ")) + "</a>")
				.appendTo(ul);
        };

}
function bindOnFocusGenus() {
    var storeid = "¢";
    $("#ctl00_ContentPlaceHolder1_txtGenus").autocomplete(
        {
            source: "../WebService/SearchGenus.ashx",
            minLength: 1,
            focus: function (event, ui) {
                $('#ctl00_ContentPlaceHolder1_txtGenus').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                return false;
            },
            select: function (event, ui) {
                findItemID(ui.item.id);
                $('#ctl00_ContentPlaceHolder1_txtGenus').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                $('#ctl00_ContentPlaceHolder1_txtGenus').val(decodeURIComponent(ui.item.value.replace(/\+/g, " ")));
                return false;
            }
        }).data("autocomplete")._renderItem = function (ul, item) {
            var splitString = item.id.split("-");
            //alert(splitString);
            return $("<li></li>")
				.data("item.autocomplete", item)
				.append("<span style='float:right;'></span><a>" + decodeURIComponent(item.label.replace(/\+/g, " ")) + "</a>")
				.appendTo(ul);
        };

}
//function bindOnFocusVariety() {
//    $(".autofill").autocomplete(
//        {
//            source: "../WebService/SearchVariety.ashx",
//            minLength: 1,
//            focus: function (event, ui) {
//                $('.autofill').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
//                return false;
//            },
//            select: function (event, ui) {
//                findItemID(ui.item.id);
//                $('.autofill').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
//                $('.autofill').val(decodeURIComponent(ui.item.value.replace(/\+/g, " ")));
//                return false;
//            }
//        }).data("autocomplete")._renderItem = function (ul, item) {
//            var splitString = item.id.split("-");
//            //alert(splitString);
//            return $("<li></li>")
//				.data("item.autocomplete", item)
//				.append("<span style='float:right;'></span><a>" + decodeURIComponent(item.label.replace(/\+/g, " ")) + "</a>")
//				.appendTo(ul);
//        };

//}





function bindOnFocusItemType() {

    $("#ctl00_ContentPlaceHolder1_txtItemType").autocomplete(
        {
            source: "../WebService/SearchItemType.ashx",
            minLength: 1,
            focus: function (event, ui) {
                $('#ctl00_ContentPlaceHolder1_txtItemType').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                return false;
            },
            select: function (event, ui) {
                findItemID(ui.item.id);
                $('#ctl00_ContentPlaceHolder1_txtItemType').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                $('#ctl00_ContentPlaceHolder1_txtItemType').val(decodeURIComponent(ui.item.value.replace(/\+/g, " ")));
                return false;
            }
        }).data("autocomplete")._renderItem = function (ul, item) {
            var splitString = item.id.split("-");
            //alert(splitString);
            return $("<li></li>")
				.data("item.autocomplete", item)
				.append("<span style='float:right;'></span><a>" + decodeURIComponent(item.label.replace(/\+/g, " ")) + "</a>")
				.appendTo(ul);
        };

}
function bindOnFocusStemSize() {

    $("#ctl00_ContentPlaceHolder1_txtStemSize").autocomplete(
        {
            source: "../WebService/SearchStemSize.ashx",
            minLength: 1,
            focus: function (event, ui) {
                $('#ctl00_ContentPlaceHolder1_txtStemSize').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                return false;
            },
            select: function (event, ui) {
                findItemID(ui.item.id);
                $('#ctl00_ContentPlaceHolder1_txtStemSize').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                $('#ctl00_ContentPlaceHolder1_txtStemSize').val(decodeURIComponent(ui.item.value.replace(/\+/g, " ")));
                return false;
            }
        }).data("autocomplete")._renderItem = function (ul, item) {
            var splitString = item.id.split("-");
            //alert(splitString);
            return $("<li></li>")
				.data("item.autocomplete", item)
				.append("<span style='float:right;'></span><a>" + decodeURIComponent(item.label.replace(/\+/g, " ")) + "</a>")
				.appendTo(ul);
        };

}
function bindOnFocusUnitType() {

    $("#ctl00_ContentPlaceHolder1_txtUnitType").autocomplete(
        {
            source: "../WebService/SearchUnitType.ashx",
            minLength: 1,
            focus: function (event, ui) {
                $('#ctl00_ContentPlaceHolder1_txtUnitType').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                return false;
            },
            select: function (event, ui) {
                findItemID(ui.item.id);
                $('#ctl00_ContentPlaceHolder1_txtUnitType').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                $('#ctl00_ContentPlaceHolder1_txtUnitType').val(decodeURIComponent(ui.item.value.replace(/\+/g, " ")));
                return false;
            }
        }).data("autocomplete")._renderItem = function (ul, item) {
            var splitString = item.id.split("-");
            //alert(splitString);
            return $("<li></li>")
				.data("item.autocomplete", item)
				.append("<span style='float:right;'></span><a>" + decodeURIComponent(item.label.replace(/\+/g, " ")) + "</a>")
				.appendTo(ul);
        };

}
function bindOnFocusQuality() {

    $("#ctl00_ContentPlaceHolder1_txtQuality").autocomplete(
        {
            source: "../WebService/SearchQuality.ashx",
            minLength: 1,
            focus: function (event, ui) {
                $('#ctl00_ContentPlaceHolder1_txtQuality').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                return false;
            },
            select: function (event, ui) {
                findItemID(ui.item.id);
                $('#ctl00_ContentPlaceHolder1_txtQuality').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                $('#ctl00_ContentPlaceHolder1_txtQuality').val(decodeURIComponent(ui.item.value.replace(/\+/g, " ")));
                return false;
            }
        }).data("autocomplete")._renderItem = function (ul, item) {
            var splitString = item.id.split("-");
            //alert(splitString);
            return $("<li></li>")
				.data("item.autocomplete", item)
				.append("<span style='float:right;'></span><a>" + decodeURIComponent(item.label.replace(/\+/g, " ")) + "</a>")
				.appendTo(ul);
        };

}
function bindOnFocusUofM() {

    $("#ctl00_ContentPlaceHolder1_txtUofM").autocomplete(
        {
            source: "../WebService/SearchUOM.ashx",
            minLength: 1,
            focus: function (event, ui) {
                $('#ctl00_ContentPlaceHolder1_txtUofM').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                return false;
            },
            select: function (event, ui) {
                findItemID(ui.item.id);
                $('#ctl00_ContentPlaceHolder1_txtUofM').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                $('#ctl00_ContentPlaceHolder1_txtUofM').val(decodeURIComponent(ui.item.value.replace(/\+/g, " ")));
                return false;
            }
        }).data("autocomplete")._renderItem = function (ul, item) {
            var splitString = item.id.split("-");
            //alert(splitString);
            return $("<li></li>")
				.data("item.autocomplete", item)
				.append("<span style='float:right;'></span><a>" + decodeURIComponent(item.label.replace(/\+/g, " ")) + "</a>")
				.appendTo(ul);
        };

}
function bindOnFocusBrand() {

    $("#ctl00_ContentPlaceHolder1_txtBrand").autocomplete(
        {
            source: "../WebService/SearchBrand.ashx",
            minLength: 1,
            focus: function (event, ui) {
                $('#ctl00_ContentPlaceHolder1_txtBrand').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                return false;
            },
            select: function (event, ui) {
                findItemID(ui.item.id);
                $('#ctl00_ContentPlaceHolder1_txtBrand').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                $('#ctl00_ContentPlaceHolder1_txtBrand').val(decodeURIComponent(ui.item.value.replace(/\+/g, " ")));
                return false;
            }
        }).data("autocomplete")._renderItem = function (ul, item) {
            var splitString = item.id.split("-");
            //alert(splitString);
            return $("<li></li>")
				.data("item.autocomplete", item)
				.append("<span style='float:right;'></span><a>" + decodeURIComponent(item.label.replace(/\+/g, " ")) + "</a>")
				.appendTo(ul);
        };

}

function fetchRateAgainstQuantity(type, InOut) {
    var itemname, effictivedate, pricelevel, quantity, hidItemID, itemtype;
    // alert(type);
    if (type == 'Add') {
        itemname = $('#ctl00_ContentPlaceHolder1_txtItemName').val();
        hidItemID = $('#ctl00_ContentPlaceHolder1_hidItemID').val();

        effictivedate = $('#ctl00_ContentPlaceHolder1_txtDate').val();
        pricelevel = $('#ctl00_ContentPlaceHolder1_HidPriceLevelName').val();
        quantity = parseFloat($('#ctl00_ContentPlaceHolder1_txtQuantity').val());
        quantity2 = parseFloat($('#ctl00_ContentPlaceHolder1_hidBalance').val());
        itemtype = parseFloat($('#ctl00_ContentPlaceHolder1_hidItemType').val());

        //alert(itemtype);
        //alert(itemname);
        //alert(effictivedate);
        //alert(pricelevel);
        //alert(quantity);
        //alert(InOut);
        //if (InOut == undefined) {
        //  quantity2 = parseFloat($('#ctl00_ContentPlaceHolder1_hidInOut').val());
        //}
        //alert(InOut);
        //alert(quantity2);
        if (itemtype == '0') {
            if (InOut == '1') {
                if (quantity > quantity2) {
                    $("#duplicateShow").html('Insufficient Stock Balance. Please Check.');
                    $('#duplicateShow').fadeIn("slow").delay(2000).fadeOut(2000);
                    $('#ctl00_ContentPlaceHolder1_btnAdd').attr("disabled", true);
                }
                else {
                    $('#ctl00_ContentPlaceHolder1_btnAdd').attr("disabled", false);
                }
            }
        }
    }
    else {

        hidItemID = $('.txtItemSerial').html();
        itemname = $('.txtItemName').html();
        effictivedate = $('#ctl00_ContentPlaceHolder1_txtDate').val();
        pricelevel = $('#ctl00_ContentPlaceHolder1_HidPriceLevelName').val();
        quantity = $('.txtQuantity').val();
        //alert(InOut);
        if (InOut == undefined) {
            InOut = $('#ctl00_ContentPlaceHolder1_hidInOut').val();
        }
        //alert(InOut);
    }
    //    alert(hidItemID);
    //    alert(effictivedate);
    //    alert(pricelevel);
    //    alert(quantity);
    //    alert(InOut);

    vIsThisInventory = $('#ctl00_ContentPlaceHolder1_hidIsThisInventory').val();
    if (vIsThisInventory == "1") {
        InOut = 0;
    }
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../WebService/SalesOrder.asmx/RateAgainstQty",
        data: "{ itemname: '" + hidItemID + "', effictivedate: '" + effictivedate + "', pricelevel: '" + pricelevel + "',quantity: '" + quantity + "',InOut: '" + InOut + "'}",
        dataType: "json",
        success: function (data) {
            //alert(data);
            if (type == 'Add') {
                $('#ctl00_ContentPlaceHolder1_txtRate').val(data[0]);
                $('#ctl00_ContentPlaceHolder1_HidDiscount').val(data[1]);
                $('#ctl00_ContentPlaceHolder1_txtDiscount').val(data[1]);
            }
            else {
                //alert(data[0]);
                $('.txtRate').val(data[0]);
                $('.HidDiscount').val(data[1]);
            }


            //HidDiscount
            //alert("data :" + data[1]);
        },
        failure: function (data) {
            alert(data);
        }
    });
}





function changeRateByDemand(type) {
    var quantity, rate, amount;
    if (type == 'Add') {
        quantity = $('#ctl00_ContentPlaceHolder1_txtQuantity').val();
        rate = $('#ctl00_ContentPlaceHolder1_txtRate').val();
        amount = $('#ctl00_ContentPlaceHolder1_txtAmount').val();
    }
    else {
        quantity = $('.txtQuantity').val();
        rate = $('.txtRate').val();
        amount = $('.txtAmount').val();

    }
    if (quantity * rate != amount) {
        rate = amount / quantity;
        //alert('rate :' + rate);
        if (type == 'Add') {
            $('#ctl00_ContentPlaceHolder1_txtRate').val(rate.toFixed(2));
        }
        else {
            $('.txtRate').val(rate.toFixed(2));

        }

    }
}

function changeDiscountByDemand(type) {

    var quantity, rate, amount, discount, Actdiscount, Commission;
    if (type == 'Add') {
        quantity = $('#ctl00_ContentPlaceHolder1_txtQuantity').val();
        rate = $('#ctl00_ContentPlaceHolder1_txtRate').val();
        amount = $('#ctl00_ContentPlaceHolder1_txtAmount').val();
        discount = $('#ctl00_ContentPlaceHolder1_txtDiscount').val();
        Commission = $('#ctl00_ContentPlaceHolder1_txtCommission').val();
    }
    else {
        quantity = $('.txtQuantity').val();
        rate = $('.txtRate').val();
        amount = $('.txtAmount').val();
        discount = $('.txtDiscount').val();
        Commission = $('.txtCommission').val();
    }
    var vperecnt = discount.substring(discount.length - 1, discount.length);
    var vdiscount = discount.substring(0, discount.length - 1);
    if (vperecnt == "%") {
        Actdiscount = (vdiscount * amount) / 100;
    }
    else {
        Actdiscount = discount;
    }
    //alert(Actdiscount);
    if (Commission == "")
    { Commission = 0; }
    if (Commission == undefined)
    { Commission = 0; }
    var calculateAmount2 = parseFloat(quantity) * (parseFloat(rate) + parseFloat(Commission));
    //alert(amount.length);
    //alert(parseFloat(discount.length));
    if ((amount.length > 0) && (discount.length > 0)) {
        //rate = amount / quantity;
        //var netamount = amount - discount;

        var netamount = calculateAmount2 - parseFloat(Actdiscount.toString());

        if (type == 'Add') {
            $('#ctl00_ContentPlaceHolder1_txtNetAmount').val(parseFloat(netamount.toString()));
            $('#ctl00_ContentPlaceHolder1_txtDiscount').val(parseFloat(Actdiscount.toString()));
        }
        else {
            $('.txtNetAmount').val(parseFloat(netamount.toString()));
            $('.txtDiscount').val(parseFloat(Actdiscount.toString()));
        }
    }
}

function changeDiscountByDemandTotal(type) { //008

    var amount, discount;
    if (type == 'Add') {
        amount = $('#ctl00_ContentPlaceHolder1_txtNetTotalAmount').val();
        discount = $('#ctl00_ContentPlaceHolder1_txtTotalDiscount').val();
    }
    else {
        amount = $('.txtNetTotalAmount').val();
        discount = $('.txtTotalDiscount').val();
    }
    if ((amount.length > 0) && (discount.length > 0)) {
        var netamount = amount - discount;
        if (type == 'Add') {
            $('#ctl00_ContentPlaceHolder1_txtPayableAmount').val(parseFloat(netamount.toString()));
        }
        else {
            $('.txtPayableAmount').val(parseFloat(netamount.toString()));
        }
    }

    if (type == 'Add') {
        var value1 = $('#ctl00_ContentPlaceHolder1_txtAdd').val();
        var value2 = $('#ctl00_ContentPlaceHolder1_txtLess').val();

        if (value1.length == 0) {
            value1 = 0;
        }
        if (isNaN(value1)) {
            value1 = 0;
        }
        if (value2.length == 0) {
            value2 = 0;
        }
        if (isNaN(value2)) {
            value2 = 0;
        }

        var netTotal = (parseFloat(netamount) + parseFloat(value1)) - parseFloat(value2);
        $('#ctl00_ContentPlaceHolder1_txtNetPayable').val(netTotal);
        $('#ctl00_ContentPlaceHolder1_hidTotal').val(netTotal);
    }
    else {
        var value1 = $('.txtAdd').val();
        var value2 = $('.txtLess').val();
        if (value1.length == 0) {
            value1 = 0;
        }
        if (isNaN(value1)) {
            value1 = 0;
        }
        if (value2.length == 0) {
            value2 = 0;
        }
        if (isNaN(value2)) {
            value2 = 0;
        }
        var netTotal = (parseFloat(netamount) + parseFloat(value1)) - parseFloat(value2);
        $('.txtNetPayable').val(parseFloat(netTotal.toString()));
        $('.hidTotal').val(parseFloat(netTotal.toString()));

    }
}

function changeTotalPayableAmount(type) { //008

    var payable, payment;
    if (type == 'Add') {
        payable = $('#ctl00_ContentPlaceHolder1_txtPayableAmount').val();
        payment = $('#ctl00_ContentPlaceHolder1_txtPaymentAmount').val();
    }
    else {
        payable = $('.txtPayableAmount').val();
        payment = $('.txtPaymentAmount').val();

    }
    if ((payable.length > 0) && (payment.length > 0)) {
        var netamount = payable - payment;
        if (type == 'Add') {
            $('#ctl00_ContentPlaceHolder1_txtTotalPayableAmount').val(parseFloat(netamount.toString()));
        }
        else {
            $('.txtTotalPayableAmount').val(parseFloat(netamount.toString()));
        }

    }
    if (type == 'Add') {
        var value1 = $('#ctl00_ContentPlaceHolder1_txtAdd').val();
        var value2 = $('#ctl00_ContentPlaceHolder1_txtLess').val();
        if (value1.length == 0) {
            value1 = 0;
        }
        if (isNaN(value1)) {
            value1 = 0;
        }
        if (value2.length == 0) {
            value2 = 0;
        }
        if (isNaN(value2)) {
            value2 = 0;
        }
        var netTotal = (parseFloat(payable) + parseFloat(value1)) - parseFloat(value2);
        $('#ctl00_ContentPlaceHolder1_txtNetPayable').val(netTotal);
        $('#ctl00_ContentPlaceHolder1_hidTotal').val(netTotal);
    }
    else {
        var value1 = $('.txtAdd').val();
        var value2 = $('.txtLess').val();
        if (value1.length == 0) {
            value1 = 0;
        }
        if (isNaN(value1)) {
            value1 = 0;
        }
        if (value2.length == 0) {
            value2 = 0;
        }
        if (isNaN(value2)) {
            value2 = 0;
        }
        var netTotal = (parseFloat(payable) + parseFloat(value1)) - parseFloat(value2);
        $('.txtNetPayable').val(parseFloat(netTotal.toString()));
        $('.hidTotal').val(parseFloat(netTotal.toString()));

    }
}

function getTotalPayableAmount(type) { //008

    var netamount;
    if (type == 'Get') {
        netamount = $('#ctl00_ContentPlaceHolder1_txtNetTotalAmount').val();
    }
    else {
        netamount = $('.txtNetTotalAmount').val();

    }
    /*
    if (type == 'Get') {
    $('#ctl00_ContentPlaceHolder1_txtTotalPayableAmount').val(parseFloat(netamount.toString()));
    }
    else {
    $('.txtTotalPayableAmount').val(parseFloat(netamount.toString()));
    }
    */
    if (type == 'Get') {
        $('#ctl00_ContentPlaceHolder1_txtPayableAmount').val(parseFloat(netamount.toString()));
    }
    else {
        $('.txtPayableAmount').val(parseFloat(netamount.toString()));
    }
    if (type == 'Get') {
        var value1 = $('#ctl00_ContentPlaceHolder1_txtAdd').val();
        var value2 = $('#ctl00_ContentPlaceHolder1_txtLess').val();
        if (value1.length == 0) {
            value1 = 0;
        }
        if (isNaN(value1)) {
            value1 = 0;
        }
        if (value2.length == 0) {
            value2 = 0;
        }
        if (isNaN(value2)) {
            value2 = 0;
        }
        var netTotal = (parseFloat(netamount) + parseFloat(value1)) - parseFloat(value2);
        $('#ctl00_ContentPlaceHolder1_txtNetPayable').val(netTotal);
        $('#ctl00_ContentPlaceHolder1_hidTotal').val(netTotal);
    }
    else {
        var value1 = $('.txtAdd').val();
        var value2 = $('.txtLess').val();
        if (value1.length == 0) {
            value1 = 0;
        }
        if (isNaN(value1)) {
            value1 = 0;
        }
        if (value2.length == 0) {
            value2 = 0;
        }
        if (isNaN(value2)) {
            value2 = 0;
        }
        var netTotal = (parseFloat(netamount) + parseFloat(value1)) - parseFloat(value2);
        $('.txtNetPayable').val(parseFloat(netTotal.toString()));
        $('.hidTotal').val(parseFloat(netTotal.toString()));


    }
}

function changeWithNetAmount(type) {

    var quantity = 0, rate = 0, amount = 0, discount = 0, netamount = 0;
    if (type == 'Add') {
        quantity = $('#ctl00_ContentPlaceHolder1_txtQuantity').val();
        rate = $('#ctl00_ContentPlaceHolder1_txtRate').val();
        amount = $('#ctl00_ContentPlaceHolder1_txtAmount').val();
        discount = $('#ctl00_ContentPlaceHolder1_txtDiscount').val();
        netamount = $('#ctl00_ContentPlaceHolder1_txtNetAmount').val();
    }
    else {
        quantity = $('.txtQuantity').val();
        rate = $('.txtRate').val();
        amount = $('.txtAmount').val();
        discount = $('.txtDiscount').val();
        netamount = $('.txtNetAmount').val();

    }
    if ((amount.length > 0) && (discount.length > 0)) {
        //rate = amount / quantity;
        var c_amount = parseFloat(netamount) + parseFloat(discount);
        var c_rate = parseFloat(c_amount) / parseFloat(quantity);
        if (type == 'Add') {
            $('#ctl00_ContentPlaceHolder1_txtAmount').val(parseFloat(c_amount.toString()).toFixed(2));
            $('#ctl00_ContentPlaceHolder1_txtRate').val(parseFloat(c_rate.toString()).toFixed(2));
        }
        else {
            $('.txtAmount').val(parseFloat(c_amount.toString()).toFixed(2));
            $('.txtRate').val(parseFloat(c_rate.toString()).toFixed(2));
        }
    }
}



function fromPopUp(valofCompref) {
    $('#ctl00_ContentPlaceHolder1_HidFromPopUp').val(decodeURIComponent(valofCompref));
    document.getElementById("aspnetForm").submit();

}





$('.txtQuantity').live('blur', function () {
    fetchRateAgainstQuantity('Edit')
});

$('.txtRate').live('blur', function () {
    calculateAmountFun('Edit')
});

$('.txtAmount').live('blur', function () {
    changeRateByDemand('Edit')
});

$('.txtDiscount').live('blur', function () {
    changeDiscountByDemand('Edit')
});

$('.txtNetAmount').live('blur', function () {
    changeWithNetAmount('Edit')
});


function CheckDBSave() {
    var found = false;

    $('.gridview').find("td").each(function () {
        //alert('123');
        found = true;
        //break;
        return;

    });

    //alert(found);
    if (found == false) {
        showError('Please check your data before submit');
    }
    return found;
}

function CheckDBSaveForTransfer() {
    var found = false;

    var storeid1 = $('#ctl00_ContentPlaceHolder1_ddlStoreFrom').val();
    var storeid2 = $('#ctl00_ContentPlaceHolder1_ddlStoreTo').val();
    if (storeid1 == storeid2) {
        found = false;
        showError('Both store cannt be same. Please check.');
        return found;
    }
    $('.gridview').find("td").each(function () {
        //alert('123');
        found = true;
        //break;
        return;

    });

    //alert(found);
    if (found == false) {
        showError('Please check your data before submit');
    }

    return found;
}

function showAdvance() {
    $('.searchSectionAd').show();
    $('#ctl00_ContentPlaceHolder1_Panel1').removeClass('afterAdd');
    $('#ctl00_ContentPlaceHolder1_Panel1').addClass('afterSearch');
}

function hideAdvance() {
    $('.searchSectionAd').hide();
}

function bringFront() {
    $('#ctl00_ContentPlaceHolder1_Panel1').removeClass('afterAdd');
    $('#ctl00_ContentPlaceHolder1_Panel1').addClass('afterSearch');
}

function setBack() {
    $('#ctl00_ContentPlaceHolder1_Panel1').removeClass('afterSearch');
    $('#ctl00_ContentPlaceHolder1_Panel1').addClass('afterAdd');

}


function isNumeric(strObj, strmsgobj) {

    strObj = "#" + strObj;
    var n = $(strObj).val();
    //alert(n);
    if (n == '') {
        n = 0;
    }
    var n2 = n;
    n = parseFloat(n);
    var val = (n != 'NaN' && n2 == n);
    if (val.toString() == "false") {
        showError('Only numeric value is acceepted for ' + strmsgobj + '. Please check.');
        addMarkClass();
        $(strObj).val('');
    } else { removeMarkClass(); }
    return (n != 'NaN' && n2 == n);

}

function addMarkClass() {
    $('.backDateCheck').addClass('mark');
    $('#ctl00_ContentPlaceHolder1_btnUpdate').addClass('ui-button-disabled');
    $('#ctl00_ContentPlaceHolder1_btnUpdate').addClass('ui-state-disabled');
    $('#ctl00_ContentPlaceHolder1_btnUpdate').attr('disabled', 'disabled');

}


function removeMarkClass() {
    $('.backDateCheck').removeClass('mark');
    $('#ctl00_ContentPlaceHolder1_btnUpdate').removeClass('ui-button-disabled');
    $('#ctl00_ContentPlaceHolder1_btnUpdate').removeClass('ui-state-disabled');
    $('#ctl00_ContentPlaceHolder1_btnUpdate').removeAttr('disabled');

}

function addMarkClassSaveButton() {

    $('#ctl00_ContentPlaceHolder1_btnSave').addClass('ui-button-disabled');
    $('#ctl00_ContentPlaceHolder1_btnSave').addClass('ui-state-disabled');
    $('#ctl00_ContentPlaceHolder1_btnSave').attr('disabled', 'disabled');

}


function removeMarkClassSaveButton() {

    $('#ctl00_ContentPlaceHolder1_btnSave').removeClass('ui-button-disabled');
    $('#ctl00_ContentPlaceHolder1_btnSave').removeClass('ui-state-disabled');
    $('#ctl00_ContentPlaceHolder1_btnSave').removeAttr('disabled');

}

function disableEnterKey(e) {
    var key;
    if (window.event)
        key = window.event.keyCode; //IE
    else
        key = e.which; //firefox      

    return (key != 13);
}


function bindOnFocusCustomerGroup() {

    $("#ctl00_ContentPlaceHolder1_txtCustomerGroup").autocomplete(
        {
            source: "../WebService/SearchCustomerGroup.ashx",
            minLength: 1,
            focus: function (event, ui) {
                $('#ctl00_ContentPlaceHolder1_txtCustomerGroup').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                return false;
            },
            select: function (event, ui) {
                findItemID(ui.item.id);
                $('#ctl00_ContentPlaceHolder1_txtCustomerGroup').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                $('#ctl00_ContentPlaceHolder1_txtCustomerGroup').val(decodeURIComponent(ui.item.value.replace(/\+/g, " ")));
                return false;
            }
        }).data("autocomplete")._renderItem = function (ul, item) {
            var splitString = item.id.split("^");
            //alert(splitString);
            return $("<li></li>")
				.data("item.autocomplete", item)
				.append("<span style='float:right;'></span><a>" + decodeURIComponent(item.label.replace(/\+/g, " ")) + "</a>")
				.appendTo(ul);
        };

}

function bindOnFocusCountry() {

    $("#ctl00_ContentPlaceHolder1_txtCountry").autocomplete(
        {
            source: "../WebService/SearchCountry.ashx",
            minLength: 1,
            focus: function (event, ui) {
                $('#ctl00_ContentPlaceHolder1_txtCountry').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                return false;
            },
            select: function (event, ui) {
                findItemID(ui.item.id);
                $('#ctl00_ContentPlaceHolder1_txtCountry').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                $('#ctl00_ContentPlaceHolder1_txtCountry').val(decodeURIComponent(ui.item.value.replace(/\+/g, " ")));
                return false;
            }
        }).data("autocomplete")._renderItem = function (ul, item) {
            var splitString = item.id.split("^");
            //alert(splitString);
            return $("<li></li>")
				.data("item.autocomplete", item)
				.append("<span style='float:right;'></span><a>" + decodeURIComponent(item.label.replace(/\+/g, " ")) + "</a>")
				.appendTo(ul);
        };

}
function bindOnFocusCountrybyID(btn) {
    var btnID = btn.id
    $('#' + btnID + '').autocomplete(
        {
            source: "../WebService/SearchCountry.ashx",
            minLength: 1,
            focus: function (event, ui) {
                $('#' + btnID + '').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                return false;
            },
            select: function (event, ui) {
                findItemID(ui.item.id);
                $('#' + btnID + '').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                $('#' + btnID + '').val(decodeURIComponent(ui.item.value.replace(/\+/g, " ")));
                return false;
            }
        }).data("autocomplete")._renderItem = function (ul, item) {
            var splitString = item.id.split("^");
            //alert(splitString);
            return $("<li></li>")
				.data("item.autocomplete", item)
				.append("<span style='float:right;'></span><a>" + decodeURIComponent(item.label.replace(/\+/g, " ")) + "</a>")
				.appendTo(ul);
        };
}
function bindOnFocusUserId() {

}
function bindOnFocusCity(btn) {
    var btnID = btn.id
    $('#' + btnID + '').autocomplete(
        {
            source: "../WebService/SearchCity.ashx",
            minLength: 1,
            focus: function (event, ui) {
                $('#' + btnID + '').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                return false;
            },
            select: function (event, ui) {
                findItemID(ui.item.id);
                $('#' + btnID + '').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                $('#' + btnID + '').val(decodeURIComponent(ui.item.value.replace(/\+/g, " ")));
                return false;
            }
        }).data("autocomplete")._renderItem = function (ul, item) {
            var splitString = item.id.split("^");
            //alert(splitString);
            return $("<li></li>")
				.data("item.autocomplete", item)
				.append("<span style='float:right;'></span><a>" + decodeURIComponent(item.label.replace(/\+/g, " ")) + "</a>")
				.appendTo(ul);
        };

}
function bindOnFocusBillCountry() {

    $("#ctl00_ContentPlaceHolder1_txtBillCountry").autocomplete(
        {
            source: "../WebService/SearchCountry.ashx",
            minLength: 1,
            focus: function (event, ui) {
                $('#ctl00_ContentPlaceHolder1_txtBillCountry').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                return false;
            },
            select: function (event, ui) {
                findItemID(ui.item.id);
                $('#ctl00_ContentPlaceHolder1_txtBillCountry').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                $('#ctl00_ContentPlaceHolder1_txtBillCountry').val(decodeURIComponent(ui.item.value.replace(/\+/g, " ")));
                return false;
            }
        }).data("autocomplete")._renderItem = function (ul, item) {
            var splitString = item.id.split("^");
            //alert(splitString);
            return $("<li></li>")
				.data("item.autocomplete", item)
				.append("<span style='float:right;'></span><a>" + decodeURIComponent(item.label.replace(/\+/g, " ")) + "</a>")
				.appendTo(ul);
        };

}

function bindOnFocusUserId() {

    $("#ctl00_ContentPlaceHolder1_txtUserID").autocomplete(
        {
            source: "../WebService/SearchUser.ashx",
            minLength: 1,
            focus: function (event, ui) {
                $('#ctl00_ContentPlaceHolder1_txtUserID').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                return false;
            },
            select: function (event, ui) {
                findItemID(ui.item.id);
                $('#ctl00_ContentPlaceHolder1_txtUserID').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                $('#ctl00_ContentPlaceHolder1_txtUserID').val(decodeURIComponent(ui.item.value.replace(/\+/g, " ")));
                return false;
            }
        }).data("autocomplete")._renderItem = function (ul, item) {
            var splitString = item.id.split("^");
            //alert(splitString);
            return $("<li></li>")
				.data("item.autocomplete", item)
				.append("<span style='float:right;'></span><a>" + decodeURIComponent(item.label.replace(/\+/g, " ")) + "</a>")
				.appendTo(ul);
        };

}


function bindOnFocusVariety() {

    $("#ctl00_ContentPlaceHolder1_txtVariety1").autocomplete(
        {
            source: "../WebService/SearchVariety.ashx",
            minLength: 1,
            focus: function (event, ui) {
                $('#TextBox1').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                return false;
            },
            select: function (event, ui) {
                findItemID(ui.item.id);
                $('#ctl00_ContentPlaceHolder1_TextBox1').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                $('#ctl00_ContentPlaceHolder1_TextBox1').val(decodeURIComponent(ui.item.value.replace(/\+/g, " ")));
                return false;
            }
        }).data("autocomplete")._renderItem = function (ul, item) {
            var splitString = item.id.split("^");
            //alert(splitString);
            return $("<li></li>")
				.data("item.autocomplete", item)
				.append("<span style='float:right;'></span><a>" + decodeURIComponent(item.label.replace(/\+/g, " ")) + "</a>")
				.appendTo(ul);
        };

}

//Methods created by Harpreet singh
jQuery.fn.ForceNumericOnly =
    function () {
        return this.each(function () {
            $(this).keydown(function (e) {
                //                if (event.shiftKey) //stopping the shift key so that user cannot enter the special characters
                //                {
                //                    event.preventDefault();
                //                }

                var key = e.charCode || e.keyCode || 0;
                // allow backspace, tab, delete, arrows, numbers and keypad numbers ONLY
                return (
                    key == 8 ||
                    key == 9 ||
                    key == 46 ||
                //(key >= 37 && key <= 40) ||
                    (key >= 48 && key <= 57) ||
                    (key >= 96 && key <= 105));
            });
        });
    };


function BindVariety(control) {

    var controlID = control.id
    $('#' + controlID + '').autocomplete(
        {
            source: "../WebService/SearchVariety.ashx",
            minLength: 1,
            focus: function (event, ui) {
                $('#' + controlID + '').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                return false;
            },
            select: function (event, ui) {
                findVarietyID(ui.item.id, controlID);
                $('#' + controlID + '').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                $('#' + controlID + '').val(decodeURIComponent(ui.item.value.replace(/\+/g, " ")));
                return false;
            }
        }).data("autocomplete")._renderItem = function (ul, item) {
            var splitString = item.id.split("-");
            //alert(splitString);
            return $("<li></li>")
				.data("item.autocomplete", item)
				.append("<span style='float:right;'></span><a>" + decodeURIComponent(item.label.replace(/\+/g, " ")) + "</a>")
				.appendTo(ul);
        };

}


function bindOnFocusCustomerList(btn) {
    var btnID = btn.id
    $('#' + btnID + '').autocomplete(
        {
            source: "../WebService/SearchCustomer.ashx",
            minLength: 1,
            focus: function (event, ui) {
                $('#' + btnID + '').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                return false;
            },
            select: function (event, ui) {
                findItemID(ui.item.id);
                $('#' + btnID + '').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                $('#' + btnID + '').val(decodeURIComponent(ui.item.value.replace(/\+/g, " ")));
                return false;
            }
        }).data("autocomplete")._renderItem = function (ul, item) {
            var splitString = item.id.split("^");
            //alert(splitString);
            return $("<li></li>")
				.data("item.autocomplete", item)
				.append("<span style='float:right;'></span><a>" + decodeURIComponent(item.label.replace(/\+/g, " ")) + "</a>")
				.appendTo(ul);
        };
}
function bindOnFocusCustomerBillTo(btn) {
    var btnID = btn.id

    var DirName = $('#ctl00_ContentPlaceHolder1_txtDirName').val();
    //alert(DirName);
    $('#' + btnID + '').autocomplete(
        {
            //source: "../WebService/SearchCustomerBill.ashx",
            source: "../WebService/SearchCustomerBill.ashx?DirName=" + DirName,
            minLength: 1,
            focus: function (event, ui) {
                $('#' + btnID + '').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                return false;
            },
            select: function (event, ui) {
                findItemID(ui.item.id);
                $('#' + btnID + '').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                $('#' + btnID + '').val(decodeURIComponent(ui.item.value.replace(/\+/g, " ")));
                return false;
            }
        }).data("autocomplete")._renderItem = function (ul, item) {

            var splitString = item.id.split("-");
            //alert(splitString);
            return $("<li></li>")
				.data("item.autocomplete", item)
				.append("<span style='float:right;'>" + decodeURIComponent(splitString[2].replace(/\+/g, " ")) + "</span><a>" + decodeURIComponent(item.label.replace(/\+/g, " ")) + "</a>")
				.appendTo(ul);
        };
}
function bindOnFocusCustomerShipTo(btn) {
    var btnID = btn.id

    var DirName = $('#ctl00_ContentPlaceHolder1_txtDirName').val();
    //alert(DirName);
    $('#' + btnID + '').autocomplete(
        {
            //source: "../WebService/SearchCustomerBill.ashx",
            source: "../WebService/SearchCustomerShip.ashx?DirName=" + DirName,
            minLength: 1,
            focus: function (event, ui) {
                $('#' + btnID + '').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                return false;
            },
            select: function (event, ui) {
                findItemID(ui.item.id);
                $('#' + btnID + '').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                $('#' + btnID + '').val(decodeURIComponent(ui.item.value.replace(/\+/g, " ")));
                return false;
            }
        }).data("autocomplete")._renderItem = function (ul, item) {

            var splitString = item.id.split("-");
            //alert(splitString);
            return $("<li></li>")
				.data("item.autocomplete", item)
				.append("<span style='float:right;'>" + decodeURIComponent(splitString[2].replace(/\+/g, " ")) + "</span><a>" + decodeURIComponent(item.label.replace(/\+/g, " ")) + "</a>")
				.appendTo(ul);
        };
}
function bindOnFocusExecutiveGroup(btn) {
    var btnID = btn.id

    var DirName = $('#' + btnID + '').val();
    //alert(DirName);
    $('#' + btnID + '').autocomplete(
        {
            source: "../WebService/SearchExecutiveGroup.ashx",
            minLength: 1,
            focus: function (event, ui) {
                $('#' + btnID + '').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                return false;
            },
            select: function (event, ui) {
                findItemID(ui.item.id);
                $('#' + btnID + '').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                $('#' + btnID + '').val(decodeURIComponent(ui.item.value.replace(/\+/g, " ")));
                return false;
            }
        }).data("autocomplete")._renderItem = function (ul, item) {

            //var splitString = item.id.split("-");
            //alert(splitString);
            return $("<li></li>")
				.data("item.autocomplete", item)
				.append("<span style='float:right;'></span><a>" + decodeURIComponent(item.label.replace(/\+/g, " ")) + "</a>")
				.appendTo(ul);
        };
}
function bindOnFocusExecutive(btn) {
    var btnID = btn.id

    var DirName = $('#' + btnID + '').val();
    //alert(DirName);
    $('#' + btnID + '').autocomplete(
        {
            source: "../WebService/SearchSalesExecutive.ashx",
            minLength: 1,
            focus: function (event, ui) {
                $('#' + btnID + '').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                return false;
            },
            select: function (event, ui) {
                findItemID(ui.item.id);
                $('#' + btnID + '').val(decodeURIComponent(ui.item.label.replace(/\+/g, " ")));
                $('#' + btnID + '').val(decodeURIComponent(ui.item.value.replace(/\+/g, " ")));
                return false;
            }
        }).data("autocomplete")._renderItem = function (ul, item) {

            //var splitString = item.id.split("-");
            //alert(splitString);
            return $("<li></li>")
				.data("item.autocomplete", item)
				.append("<span style='float:right;'></span><a>" + decodeURIComponent(item.label.replace(/\+/g, " ")) + "</a>")
				.appendTo(ul);
        };
}

//============================================================Harpreet Singh Code [STARTS]================================================================

//Method Name       :   GetGenusDetails();
//Description       :   Fetches the Genus details according to the Variety selected
//Parameters        :   VarietyID 
//Created By        :   Harpreet Singh
//Created Date      :   13 August 2012   
function GetGenusDetails(VarietyID) {
    var value;
    value = "VarietyID = " + VarietyID;
    //MakeAjaxCall method is a generic method for sending the ajax request to the server
    //this function is written in AjaxCall.js javascript (inside sales folder)
    MakeAjaxCall("?method=getVarietyGenus&callbackmethod=ShowGenus&VarietyID=" + VarietyID, "Script", value);
}




var CurrentControlID = '';
function findVarietyID(data, cntrl) {
    //alert("Item Data:" + data);
    var splitString = data.split("-");
    //alert(splitString);
    var VarietyID = $("#" + cntrl + "").next().attr("id");
    //alert(VarietyID);
    $("#" + VarietyID + "").val(splitString);
    //alert($("#" + VarietyID + "").val());
    CurrentControlID = VarietyID;
    GetGenusDetails(splitString);
    GetVarietySubtypeDetails(splitString);

}


//Callback function for GetGenusDetails
//Used for showing genus
function ShowGenus(data, message) {
    var s = CurrentControlID; //Client id of the txtVarietyID
    var controlname = "txtVarietyID"; //ClientSide  txtVarietyID
    var CntrlPrefix = s.substring(0, (s.length - controlname.length));
    var txtGenusName = CntrlPrefix + "txtGenusName";
    var txtGenusID = CntrlPrefix + "txtGenusID";

    for (var i = 0; i < data.length; i++) {
        //alert(GenusNameControlID);
        $("#" + txtGenusName + "").val(data[i].GenusName);
        $("#" + txtGenusID + "").val(data[i].GenusID);
        //alert(data[i].GenusName);
        //alert(data[i].GenusID);
    }
}


//Method Name       :   GetVarietySubtype Details();
//Description       :   Fetches the Subtype details according to the Variety selected
//Parameters        :   VarietyID 
//Created By        :   Harpreet Singh
//Created Date      :   16 August 2012   
function GetVarietySubtypeDetails(VarietyID) {
    var value;
    value = "VarietyID = " + VarietyID;
    //MakeAjaxCall method is a generic method for sending the ajax request to the server
    //this function is written in AjaxCall.js javascript (inside sales folder)
    MakeAjaxCall("?method=getvarietysubtype&callbackmethod=ShowSubtype&VarietyID=" + VarietyID, "Script", value);
}
//Callback function for GetVarietySubtypeDetails
function ShowSubtype(data, message) {

    var s = CurrentControlID; //Client id of the txtVarietyID
    var controlname = "txtVarietyID"; //ClientSide  txtVarietyID
    var CntrlPrefix = s.substring(0, (s.length - controlname.length));
    var ddlSubtype = CntrlPrefix + "ddlVarietySubtype";
    var txtSubtypeID = CntrlPrefix + "txtSubtypeID";
    $("#" + ddlSubtype + "").val("");
    $("#" + ddlSubtype + "").html("");

    for (var i = 0; i < data.length; i++) {
        //$('#ddlSubtype').append($("<option></option>").val(this['ID']).html(this['CityName']));
        $("#" + ddlSubtype + "").append($('<option></option>').val("0").html("Select"));
        $("#" + ddlSubtype + "").append($('<option></option>').val(data[i].SubtypeID).html(data[i].SubTypeName));
        $("#" + txtSubtypeID + "").val(data[i].SubtypeID);
    }
}






//============================================================Harpreet Singh Code [ENDS]==================================================================

