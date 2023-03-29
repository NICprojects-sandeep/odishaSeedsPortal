// JScript File
//======================Declaration of patterns for different Regular Expression================================
var PatternsDict = new Object()
//--------------------------------------------------------------------------------------------------------------

// mathes USA telephone no.
PatternsDict.telpat  = /^(\d{10}|(\d{3}-\d{3}-\d{4}))?$/
// example:-325-672-6433

// mathes telephone Indian no.
PatternsDict.telpatIND  = /^((\+){1}[1-9]{1}[0-9]{0,1}[0-9]{0,1}(\s){1}[\(]{1}[1-9]{1}[0-9]{1,5}[\)]{1}[\s]{1})[1-9]{1}[0-9]{4,9}$/
// example:+91 (22) 24440444

//PatternsDict.telpatUNI  = /^((\+){1}[1-9]{1}[0-9]{0,1}[0-9]{0,1}[\-]{1}[1-9]{1}[0-9]{1,5}[\-]{1})[1-9]{1}[0-9]{5,6}$/
PatternsDict.telpatUNI  = /^([1-9]{1}[0-9]{0,1}[0-9]{0,1}[\-]{1}[1-9]{1}[0-9]{1,5}[\-]{1})[1-9]{1}[0-9]{5,6}$/
// example:+91-674-2495452

// matches numeric
PatternsDict.numericpat  = "^\d*$" // Any number is allowed, but are optional

// matches white space
PatternsDict.whitespacepat = /\s+/

// matches zip code
PatternsDict.zippat = /^(\d{5}|\d{9}|(\d{5}-\d{4}))?$/
//example:-78731
//PatternsDict.zippat = "^(\d{5}|\d{9}|(\d{5}-\d{4}))?$"F

// matches IP address
PatternsDict.IPpat =/^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$/

// matches hex number
PatternsDict.hexpat = "^([a-fA-F0-9]+)?$"

// matches any alphanumeric character,hyphen(-) or an underscore(_)
// including white space
PatternsDict.validpat = "^[a-zA-Z0-9-_]+$"

// matches required field
PatternsDict.requiredpat = "^((/\s+)|'')?$"

// matches character
 PatternsDict.charpat = /^[a-zA-Z]+$/
 
 //matches Date
// PatternsDict.datepat=/^([1-9]|0[1-9]|[12][1-9]|3[01])\D([1-9]|0[1-9]|1[012])\D(19[0-9][0-9]|20[0-9][0-9])$/
PatternsDict.datepat=/^([1-9]|0[1-9]|[12][0-9]|3[01])\D([1-9]|0[1-9]|1[012])\D(19[0-9][0-9]|20[0-9][0-9])$/
 
//PatternsDict.urlpat="(?<protocol>http(s)?|ftp)://(?<server>([A-Za-z0-9-]+\.)*(?<basedomain>[A-Za-z0-9-]+\.[A-Za-z0-9]+))+((/?)(?<path>(?<dir>[A-Za-z0-9\._\-]+)(/){0,1}[A-Za-z0-9.-/]*)){0,1}"
//PatternsDict.urlpat="http(s)?://([\w-]+\.)+[\w-]+(/[\w-].[/?%&=]*?.[/?%&=]*)"
//PatternsDict.urlpat="/http(s)?://([\w-]+\.)+([\w-]+\.)/"
//PatternsDict.urlpatwww  = /^[www\.]+[A-Za-z0-9\-\.]+\.+[com]$/
//PatternsDict.urlpatwww="([\w-]+\.)+[\w-]+(/[\w-].[/?%&=]*)?"
//PatternsDict.urlpatwww="([\w-]+\.)+[\w-]+[\w-]+(/[\w-].[/?%&=]*)?"
PatternsDict.urlpat="http(s)?://([\w-]+\.)+[\w-]+(/[\w-].[/?%&=]*)?"
PatternsDict.wwwurlpat=/^[w]+[w]+[w]+\.+[A-Za-z0-9\-\.]+\.+[A-Za-z]{2,10}$/
PatternsDict.httpdomnurlpat=/^[h]+[t]+[t]+[p]+[:]+[/]+[/]+[A-Za-z0-9\-]+\.+[A-Za-z\-\.]{2,10}$/
 
// mathes email
var emailpat = /^[A-Za-z0-9\-_\.]+@+[A-Za-z0-9\-\.]+\.+[A-Za-z]{2,10}$/

// matches unsigned float
var ufloatpat = /^((\d+(\.\d*)?)|((\d*\.)?\d+))$/

// matches signed float
var sfloatpat = /^(((\+|\-)?\d+(\.\d*)?)|((\+|\-)?(\d*\.)?\d+))$/

// PatternsDict.datepat=/^([1-9]|0[1-9]|[12][1-9]|3[01])\D([1-9]|0[1-9]|1[012])\D(19[0-9][0-9]|20[0-9][0-9])$/

//var decipat="^(\\+|-)?[0-9][0-9]*(\\.[0-9]*)?$"
var decipat= /^\s*(\+|-)?((\d+(\.\d+)?)|(\.\d+))\s*$/

//----------------------------------------End of pattern declaration----------------------------------------
//======================================Create Finctions Using Pattern======================================



 function isRequired(Object,msg)
  {
    var strInput = trim(new String(Object.value))
    var objregExp  = new RegExp(PatternsDict.requiredpat)

   
   if(objregExp.test(strInput))

     {
       alert(msg)
       Object.focus()
       return false

     }

     return true

 }
//--------------------------- check dropdown is selected or not ------------------------------------
function isSelectDropDown(Object,msg)
  { 
  if( Object == false || Object.selectedIndex== 0 ) 
   {
     alert(msg)
     Object.focus()
     return false
    }
     return true
 }
 
//=======================END OF INPUT VALIDATION SERVICE==============================

// JScript File

var strMandatoryAlert2          = "<Field> can not be left blank!"
var strSelecteDropDown  = "Please select <Field>!"
//=========================================END OF  STANDRD ERROR ALERT==========================================
function InputValidator()
{
    
    this.isRequired		 		= isRequired;
	this.isSelectDropDown       = isSelectDropDown;   
    this.Replace		 		= Replace;
    this.trim			 		= trim;
}

function ErrorAlert()
{
	
	this.MandatoryAlert2			= strMandatoryAlert2;
	this.strSelecteDropDown         = strSelecteDropDown;
}
//====================================END OF TEST PAGE=======================================
//============1.Function checking blank field for textboxes/areas==============================
function blankFieldValidation(Controlname,Fieldname)
  {
    
    //alert(document.getElementById(Controlname));
  var objfrm=document.getElementById(Controlname);   	
	var objFieldname=Fieldname;
	var flag;
	flag=false;
	
	var objValidator = new InputValidator();
	var objError 	 = new ErrorAlert();
   if
    (
       objValidator.isRequired(objfrm, objValidator.Replace(objError.MandatoryAlert2,"<Field>",objFieldname))
  
    )
        {
	  	objError = null
		objValidator = null
		//alert ("Form has been validated successfully.")
		flag=true
        }

	objError = null
	objValidator = null
	return flag
  }
  //============27.Function checking select dropdown==============================
      
function DropDownValidation(Controlname,Fieldname)
  {
 
  	var objfrm=document.getElementById(Controlname);
  	
  	var objFieldname=Fieldname;
  	var flag;
	flag=false;
	
	var objValidator = new InputValidator();
	var objError 	 = new ErrorAlert();
   if
    (
        objValidator.isSelectDropDown(objfrm, objValidator.Replace(objError.strSelecteDropDown,"<Field>",objFieldname))
    )
        {
	  	objError = null
		objValidator = null
		//alert ("Form has been validated successfully.")
		flag=true
        }

	objError = null
	objValidator = null
	return flag
  }
  
//--------------------------Function to replace a string with another string ---------------------------
function Replace(str1, str2, str3)
 {
str1 = str1.replace(new RegExp(str2),str3);
return str1
 }
  
  //-------------------------------------Function to Trim a String--------------------------------------------

function trim(strString)
{
   var strCopy = new String(strString)
   strCopy = strCopy.replace(/^\s+/,"")
   strCopy = strCopy.replace(/\s+$/, "")
   return strCopy.toString()
}