//Author: Kapil 
//Date: 20-Aug-2012

jQuery.keynavigation = function (table) {
    var rowColorStyle = "BACKGROUND-COLOR: white; COLOR: #003399";
    var rowHighlightColor = "#aaa";
    //var rowHighlightColor = "Aqua";     

    $(table).find("input[type='text'], select").keydown(
    function (event) {
        //For navigating using right key        
        if ((event.keyCode == 39) || (event.keyCode == 9 && event.shiftKey == false)) {
            var inputs = $(this).parents("tr").find("input[type='text'], select");
            var idx = inputs.index(this);
            if (idx == inputs.length - 1) {
                inputs[0].select();
            } else {
                $(".grvInventoryDisplay").find("tr").not(':first').each(function () {
                    $(this).attr("style", rowColorStyle);
                });
                inputs[idx + 1].parentNode.parentNode.style.backgroundColor = rowHighlightColor;
                inputs[idx + 1].focus();
            }
            event.preventDefault();
        }

        //For navigating using left key
        if ((event.keyCode == 37) || (event.keyCode == 9 && event.shiftKey == true)) {
            var inputs = $(this).parents("tr").find("input[type='text'], select");
            var idx = inputs.index(this);
            if (idx > 0) {
                $(".grvInventoryDisplay").find("tr").not(':first').each(function () {
                    $(this).attr("style", rowColorStyle);
                });
                inputs[idx - 1].parentNode.parentNode.style.backgroundColor = rowHighlightColor;

                inputs[idx - 1].focus();
                inputs[idx].value = inputs[idx].value;
                if (inputs[idx - 1].type != "select-one")
                { inputs[idx - 1].select(); }
            }
            event.preventDefault();
        }
    });

    //For navigating using up and down arrow of the keyboard
    table.find("input[type='text']").keydown(
    function (event) {
        if ((event.keyCode == 40)) {
            if ($(this).parents("tr").next() != null) {
                var nextTr = $(this).parents("tr").next();
                var inputs = $(this).parents("tr").find("input[type='text']");
                var idx = inputs.index(this);
                nextTrinputs = nextTr.find("input[type='text']");
                if (nextTrinputs[idx] != null) {
                    $(".grvInventoryDisplay").find("tr").not(':first').each(function () {
                        $(this).attr("style", rowColorStyle).find('input[type="text"]').val('0');

                    });
                    nextTrinputs[idx].parentNode.parentNode.style.backgroundColor = rowHighlightColor;
                    nextTrinputs[idx].focus();
                }
            }
            else {
                $(this).focus();
            }
            event.preventDefault();
        }

        if ((event.keyCode == 38)) {
            if ($(this).parents("tr").prev() != null) {
                var nextTr = $(this).parents("tr").prev();
                var inputs = $(this).parents("tr").find("input[type='text']");
                var idx = inputs.index(this);
                nextTrinputs = nextTr.find("input[type='text']");
                if (nextTrinputs[idx] != null) {
                    $(".grvInventoryDisplay").find("tr").not(':first').each(function () {

                        $(this).attr("style", rowColorStyle).find('input[type="text"]').val('0');
                    });
                    nextTrinputs[idx].parentNode.parentNode.style.backgroundColor = rowHighlightColor;
                    nextTrinputs[idx].focus();
                }
                return false;
            }
            else {
                $(this).focus();
            }
            event.preventDefault();
        }
    });

    //Navigate to the next same column in the next row when the enter key is clicked.
    table.find("input[type='text']").keypress(
    function (event) {
        //When enter key clicked
        if (event.keyCode == 13) {
            if (parseInt($('input[name$="hdfOrderID"]').val()) <= 0) {
                alert('Please create sale order.');
                return false;
            }
            var inputs = $(this).parents("tr").find("input[type='text']");
            //$(inputs).eq(1).removeAttr('disabled');
            var idx = inputs.index(this);
            if ($(inputs).eq(0).val() <= 0) {
                alert('Please fill quantity');
                $(inputs).eq(0).focus();
                return false;
            }
            if ($(inputs).eq(1).val() <= 0) {
                alert('Please fill price');
                $(inputs).eq(1).focus();
                return false;
            }
            if (idx == inputs.length - 1) {
                //Clear Popup Span Control
                if (parseInt((inputs).eq(0).val()) < parseInt($(this).parents("tr").find("span[id$='lblQuantityAvail']").text())) {
                    $("span[id$='lblRequired']").text('');
                    $("input[name$='hdfQuantity']").val('');
                    $("span[id$='lblStem']").text('');
                    $("input[name$='hdfStemPrice']").val('');
                    $("input[name$='hdfInvID']").val('');
                    $("input[name$='hdfVarietyID']").val('');
                    // Pick value from grid row and fill popup
                    var Inv_ID = $(this).parents("tr").find("input[name$='hdfInventoryID']").val();
                    Inv_ID = Inv_ID + ',' + $(this).parents("tr").index();
                    var Quantity = $(this).parents("tr").find("input[name$='txtQantity']").val();
                    var Price = $(this).parents("tr").find("input[name$='txtPrice']").val();
                    var VarietyID = $(this).parents("tr").find("input[id$='hdfVarietyAutoID']").val();
                    $("span[id$='lblRequired']").text(Quantity);
                    $("input[name$='hdfQuantity']").val(Quantity);
                    $("span[id$='lblStem']").text(Price);
                    $("input[name$='hdfStemPrice']").val(Price);
                    $("input[name$='hdfInvID']").val(Inv_ID);
                    $("input[name$='hdfVarietyID']").val(VarietyID);
                    //centering with css                        
                    centerPopup("#DivPopup");
                    //load popup
                    loadPopup("#DivPopup");
                    $('input[id$="btnSaveOrder"]').focus();
                }
                else {
                    alert('Required quantity is more than available.');
                }
            }
            event.preventDefault();
        }
    });

}
