var lastFocusedControlId = "";

function focusHandler(e) 
{    
    document.activeElement = e.originalTarget;
}

function appInit() 
{    
    if (typeof(window.addEventListener) !== "undefined") 
    {        
        window.addEventListener("focus", focusHandler, true);    
    }    
    
    Sys.WebForms.PageRequestManager.getInstance().add_pageLoading(pageLoadingHandler);    
    Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoadedHandler);
}

function pageLoadingHandler(sender, args) 
{    
    lastFocusedControlId = typeof(document.activeElement) === "undefined"         ? "" : document.activeElement.id;
}

function focusControl(targetControl) 
{    
    if (Sys.Browser.agent === Sys.Browser.InternetExplorer) 
    {        
        var focusTarget = targetControl;        
        if (focusTarget && (typeof(focusTarget.contentEditable) !== "undefined")) 
        {            
            oldContentEditableSetting = focusTarget.contentEditable;            
            focusTarget.contentEditable = false;        
        }        
        else 
        {            
            focusTarget = null;        
        }
        try
        {
        targetControl.focus();        
        if (focusTarget) 
        {            
            focusTarget.contentEditable = oldContentEditableSetting;        
        }
        }
        catch(err){}    
    }    
    else 
    {        
        targetControl.focus();    
    }
}

function pageLoadedHandler(sender, args) 
{    
    if (typeof(lastFocusedControlId) !== "undefined" && lastFocusedControlId != "") 
    {        
        var newFocused = $get(lastFocusedControlId);        
        if (newFocused) 
        {            
            focusControl(newFocused);        
        }    
    }
}

Sys.Application.add_init(appInit); 


////////////////////////////// for email pattern matching

var emailPat = /^(\".*\"|[A-Za-z]\w*)@(\[\d{1,3}(\.\d{1,3}){3}]|[A-Za-z]\w*(\.[A-Za-z]\w*)+)$/
var EmailmatchArray = Email.match(emailPat);
            
if (EmailmatchArray == null) {
    alert("Your email address seems incorrect. Please try again.");
    return false;
}


////////////// for pin no pattern

var digits = "0123456789";
var temp;

for (var i = 0; i < document.getElementById("<%=txtPin.ClientID %>").value.length; i++) {
    temp = document.getElementById("<%=txtPin.ClientID%>").value.substring(i, i + 1);
    if (digits.indexOf(temp) == -1) {
        alert("Please enter correct pin code");
        document.getElementById("<%=txtPin.ClientID%>").focus();
        return false;
    }
}


///////// url pattern

var Url = "^[A-Za-z]+://[A-Za-z0-9-_]+\\.[A-Za-z0-9-_%&\?\/.=]+$";
var matchURL = WebUrl.match(Url);
if (matchURL == null) {
   alert("Web URL does not look valid");
   document.getElementById("<%=txtURL.ClientID %>").focus();
   return false;
}