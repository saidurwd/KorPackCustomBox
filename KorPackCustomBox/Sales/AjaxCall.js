//Class :   Javascript file for Exam Screen
//**************************************************************************************************************************************************

//Method Name       :   MakeAjaxCall();
//Description       :   Generic Method for sending an ajax request
//Parameters        :   parameter :- method name and callback methodname
//                  :   datatype  :- datatype of the result data
//                  :   datatype  :- Result data (response)
//Created By        :   Harpreet Singh
//Created Date      :   13 August 2012
function MakeAjaxCall(parameter, datatype, data) {
    jQuery.ajax({
        type: 'POST',
        url: "AjaxCallHandler.ashx" + parameter,
        data: data,
        dataType: datatype,
        success: function (data, textStatus) {

            try {
                //var jsonData = (new Function("return " + data))()//commented temporaliy
                if (data.IsSucess) {
                    eval(data.CallBack + '(data.ResponseData, data.Message)');
                }
                else {
                    alert(data.Message + data.IsSucess);
                }
            }
            catch (err) {
            }
        },
        error: function () {
            alert("Error");
        }
    });
}












