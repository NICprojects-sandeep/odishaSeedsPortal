// JScript File
//function validate_Input_Login(evt) {
//    var charCode = (evt.which) ? evt.which : event.keyCode;
//    if ((charCode >= 97 && charCode <= 122) || (charCode >= 65 && charCode <= 90) || charCode == 64 || charCode == 95 || charCode == 127 || charcode == 8)
//        return true;
//    else
//        return false;
//}

function validate_Input_Passwd(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if ((charCode >= 97 && charCode <= 122) || (charCode >= 65 && charCode <= 90) || (charCode >= 48 && charCode <= 57) || charCode == 64 || charCode == 95 || charCode == 127 || charcode == 8)
        return true;
    else
        return false;
}

function validate_Input_Passwd1(event, obj) {
    if ((document.getElementById("<%=txtPwd.ClientID%>").value == "") || (document.getElementById("<%=txtConPwd.ClientID%>").value == "")) {
        alert("Please Enter Password/Confirm Password!");
        document.getElementById("<%=txtPwd.ClientID%>").focus();
        return false;
    }
    else if (document.getElementById("<%=txtPwd.ClientID%>").value != document.getElementById("<%=txtConPwd.ClientID%>").value) {
        alert("Password and Confirm Password Not Matching!");
        document.getElementById("<%=txtPwd.ClientID%>").focus();
        return false;
    }
    else {
        alert(document.getElementById("<%=txtPwd.ClientID%>").value);
        var psswd = MD5(document.getElementById("<%=txtPwd.ClientID%>").value);
        document.getElementById("<%=txtpasswd.ClientID%>").value = psswd;
        alert(psswd);
        return true;
    }
}

function isNumericKey(evt)        //CHECK FOR INPUT OF NUMERIC KEYS (0 1 2 3 4 5 6 7 8 9, BACKSPACE)
{
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if ((charCode > 47 && charCode < 58) || charCode == 8)
        return true;
    else
        return false;
}

function CheckAlphabetKey(evt)        //CHECK FOR INPUT OF ALPHABET KEYS (A-Z, a-z, SPACE, BACKSPACE)
{
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if ((charCode >= 97 && charCode <= 122) || (charCode >= 65 && charCode <= 90) || charCode == 32 || charcode == 8)
        return true;
    else
        return false;
}

function CheckDecimal(event, obj)    // CHECK FOR INPUT OF NUMERIC KEYS (0-9, BACKSPACE) WITH 3 DECIMAL PLACES
{
    var code = (event.which) ? event.which : event.keyCode;
    if ((code >= 48 && code <= 57)) {
        if (obj.value.indexOf(".") != -1) {
            if (obj.value.length < obj.value.indexOf(".") + 4)      // Check for 3 digits after decimal
                return true;
            else
                return false;
        }
        else
            return true;
    }
    else if (code == 46) {
        if (obj.value.indexOf(".") < 0)                             // Check dot
            return true;
    }
    else if (code == 8)
        return true;                                                // Allow backspace
    else if (code >= 37 && code <= 40)
        return true;                                                // Allow directional arrows
    return false;
}

function CheckDecimal2(event, obj)    // CHECK FOR INPUT OF NUMERIC KEYS (0-9, BACKSPACE) WITH 3 DECIMAL PLACES
{
    var code = (event.which) ? event.which : event.keyCode;
    if ((code >= 48 && code <= 57)) {
        if (obj.value.indexOf(".") != -1) {
            if (obj.value.length < obj.value.indexOf(".") + 3)      // Check for 2 digits after decimal
                return true;
            else
                return false;
        }
        else
            return true;
    }
    else if (code == 46) {
        if (obj.value.indexOf(".") < 0)                             // Check dot
            return true;
    }
    else if (code == 8)
        return true;                                                // Allow backspace
    else if (code >= 37 && code <= 40)
        return true;                                                // Allow directional arrows
    return false;
}

function CheckDecimal5(event, obj)    // CHECK FOR INPUT OF NUMERIC KEYS (0-9, BACKSPACE) WITH 3 DECIMAL PLACES
{
    var code = (event.which) ? event.which : event.keyCode;
    if ((code >= 48 && code <= 57)) {
        if (obj.value.indexOf(".") != -1) {
            if (obj.value.length < obj.value.indexOf(".") + 6)      // Check for 5 digits after decimal
                return true;
            else
                return false;
        }
        else
            return true;
    }
    else if (code == 46) {
        if (obj.value.indexOf(".") < 0)                             // Check dot
            return true;
    }
    else if (code == 8)
        return true;                                                // Allow backspace
    else if (code >= 37 && code <= 40)
        return true;                                                // Allow directional arrows
    return false;
}

function checkDate(sender, args) {
    if (sender._selectedDate > new Date()) {
        alert("Invalid / Future Date Selected!");
        sender._selectedDate = new Date();                      // set date back to the current date
        sender._textbox.set_Value(sender._selectedDate.format(sender._format))
        alert(sender._textbox.set_Value(sender._selectedDate.format(sender._format)));
    }
}

function confirm_Print() {
    var printOk = document.getElementById('hfPrintOK');
    if (confirm("Did you Print the Cheque?\nIf you Click OK, Cheque Can not be Printed Again\nClick Cancel to Reprint Cheque") == true) {
        printOk.value = 'true';
        return true;
    }
    else {
        printOk.value = 'false';
        return false;
    }
}



function Confirm_RowClick() {
    if (confirm("You are going to change login ststus of the selected user.\nClick OK to Confirm") == true)
        return true;
    else
        return false;
}