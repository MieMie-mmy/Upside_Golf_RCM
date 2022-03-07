
var windowObjectReference;
function openRequestedPopup() {
    windowObjectReference = window.open("../Item/Item_Preview_Form.aspx",
              "DescriptiveWindowName",
              "menubar=yes,location=yes,resizable=yes,scrollbars=yes,status=yes");
}

function ShowCatagoryList() {
    //declare a string variable
    var retval = "";
    //show modal dialog box and collect its return value
    retval = window.showModalDialog
		('../Item/PopupCatagoryList.aspx', window,
		 'dialogHeight:500px; dialogWidth:1000px; dialogLeft:50px; dialogRight :50px; dialogTop:50px; help:no; unadorned:no; resizable:no; status:no; scroll:yes; minimize:no; maximize:yes;modal=yes;center=yes;');
    //check if user closed the dialog 
    //without selecting any value
    if (retval == undefined)
        retval = window.returnValue;
}

function ShowOption(SourceID) {
    var hidSourceID = document.getElementById("<%=CustomHiddenField.ClientID%>");
    hidSourceID.value = SourceID;
    //declare a string variable
    var retval = "";
    //show modal dialog box and collect its return value
    retval = window.showModalDialog
	  ('../Item/Item_Option_Select1.aspx', window, 'dialogHeight:1000px; dialogWidth:1000px; dialogLeft:200px; dialogRight :200px; dialogTop:50px; help:no; unadorned:no; resizable:no; status:no; scroll:yes; minimize:no; maximize:yes;modal=yes;center=yes;');
}

function __doPostBack(eventTarget, eventArgument) {
    if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
        theForm.__EVENTTARGET.value = eventTarget;
        theForm.__EVENTARGUMENT.value = eventArgument;
        theForm.submit();
    }
}

function ShowDialog(imagetype) {
    var hidSourceID = document.getElementById("<%=CustomHiddenField.ClientID%>");
    hidSourceID.value = document.getElementById("<%=lnkAddPhoto.ClientID%>");
    //declare a string variable
    var retval = "";
    //show modal dialog box and collect its return value
    retval = window.showModalDialog
			('../Item/FileUpload_Dialog.aspx?Image_Type=' + imagetype, window, 'dialogHeight:650px; dialogWidth:900px; dialogLeft:50px; dialogRight :50px; dialogTop:50px; help:no; unadorned:no; resizable:no; status:no; scroll:yes; minimize:no; maximize:yes;modal=yes;center=yes;');
    //check if user closed the dialog 
    //without selecting any value
    if (retval == undefined)
        retval = window.returnValue;
    if (retval != "" && retval != null) {
        //fill the textbox with selected value
        var Name = '<%= hfValue.ClientID %>';
        document.getElementById(Name).value = retval;
    }
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if ((charCode >= 48 && charCode <= 57) || charCode == 8 || charCode == 46)
        return true;
    else return false;
}

function ShowRelatedItem() {
    //declare a string variable
    var retval = "";
    //show modal dialog box and collect its return value
    retval = window.showModalDialog
		('../Item/Item_Choice.aspx', window,
		 'dialogHeight:500px; dialogWidth:1000px; dialogLeft:50px; dialogRight :50px; dialogTop:50px; help:no; unadorned:no; resizable:no; status:no; scroll:yes; minimize:no; maximize:yes;modal=yes;center=yes;');
    //check if user closed the dialog 
    //without selecting any value
    if (retval == undefined)
        retval = window.returnValue;
}

function RadioCheck(rb) {
    var dl = document.getElementById("<%=dlPhoto.ClientID%>");
    var rbs = dl.getElementsByTagName("input");
    var row = rb.parentNode.parentNode;
    for (var i = 0; i < rbs.length; i++) {
        if (rbs[i].type == "radio") {
            if (rbs[i].checked && rbs[i] != rb) {
                rbs[i].checked = false;
                break;
            }
        }
    }
}

function ShowMallCategory(mallID) {
    //declare a string variable
    var retval = "";
    //show modal dialog box and collect its return value
    retval = window.showModalDialog
			('../Item/Mall_Category_Choice.aspx?Mall_ID=' + mallID, window, 'dialogHeight:650px; dialogWidth:900px; dialogLeft:50px; dialogRight :50px; dialogTop:50px; help:no; unadorned:no; resizable:no; status:no; scroll:yes; minimize:no; maximize:yes;modal=yes;center=yes;');
    //check if user closed the dialog 
    //without selecting any value
}

function ShowYahooSpecValue() {
    //declare a string variable
    var retval = "";
    var hidderValue = document.getElementById("<%= txtYahoo_CategoryID.ClientID %>").value;
    //var h = document.getElementById("<%= hdfYahoo.ClientID %>");
    //show modal dialog box and collect its return value
    retval = window.showModalDialog
		('../Item/Item_YahooSpecificValue.aspx?YahooMallCategoryID=' + hidderValue, window,
		 'dialogHeight:500px; dialogWidth:1000px; dialogLeft:50px; dialogRight :50px; dialogTop:50px; help:no; unadorned:no; resizable:no; status:no; scroll:yes; minimize:no; maximize:yes;modal=yes;center=yes;');
    //check if user closed the dialog 
    //without selecting any value
    if (retval == undefined)
        retval = window.returnValue;
}

