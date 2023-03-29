<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>::: DBT :::</title>
    <link rel="Shortcut Icon" type="image/x-icon" style="width: 50px;" href="images/thumb2.ico" />
    <link rel="stylesheet" href="css/otb-admin.css" media="all" type="text/css" />
    <script src="Scripts/pngfix.js" type="text/javascript" lang="javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.js"></script>
    <script type="text/javascript" src="Scripts/popup.js"></script>
    <script type="text/javascript" src="Scripts/sha512.min.js"></script>
    <script type="text/javascript" src="Scripts/InputValidation.js"></script>
    <meta http-equiv="imagetoolbar" content="no" />
    <%--  <link rel="stylesheet" href="Scripts/layout.css" type="text/css" />--%>
    <link href="css/style_New.css" rel="stylesheet" />
    <script type="text/javascript" lang="javascript">
        $(document).ready(function () {
            var id = '#dialog';
            //Get the screen height and width
            var maskHeight = $(document).height();
            var maskWidth = $(window).width();
            //Set heigth and width to mask to fill up the whole screen
            $('#mask').css({ 'width': maskWidth, 'height': maskHeight });
            //transition effect		
            $('#mask').fadeIn(1000);
            $('#mask').fadeTo("slow", 0.8);
            //Get the window height and width
            var winH = $(window).height();
            var winW = $(window).width();
            //Set the popup window to center
            $(id).css('top', winH / 2 - $(id).height() / 2);
            $(id).css('left', winW / 2 - $(id).width() / 2);
            //transition effect
            $(id).fadeIn(2000);
            //if close button is clicked
            $('.window .close').click(function (e) {
                //Cancel the link behavior
                e.preventDefault();
                $('#mask').hide();
                $('.window').hide();
            });
        });

        function LoginCheck() {
            var uid = document.getElementById("<%=txtUserId.ClientID%>").value;
            var pwd = document.getElementById("<%=txtPwd.ClientID%>").value;
            var pwdSmall = /[a-z]/; //SMALL LETTER
            var pwdCap = /[A-Z]/; //CAPITAL LETTER
            var pwdSpl = /[\W_]/; //SPECIAL CHARECTER
            var RegExUID = /^([a-zA-Z_@])$/;


            if (uid == null || uid == "") {
                alert('Please Enter User ID!');
                document.getElementById("<%=txtUserId.ClientID%>").focus();
                document.getElementById("<%=txtPwd.ClientID%>").value = "";
                return false;
            }
            if (pwd == null || pwd == "") {
                alert("Please Enter Password!");
                document.getElementById("<%=txtPwd.ClientID%>").value = "";
                document.getElementById("<%=txtPwd.ClientID%>").focus();
                return false;
            }
            if (pwd.length < 6) {
                alert('Minimum Six Characters are Required!');
                document.getElementById("<%=txtPwd.ClientID%>").value = "";
                document.getElementById("<%=txtPwd.ClientID%>").focus();
                return false;
            }

            // GET RANDOM NUMBER FROM HIDDEN FIELD WHICH WILL WORK AS SALT
            var Pas = document.getElementById("<%=txtPwd.ClientID%>").value;
            var salt = document.getElementById("<%=hfRandNo.ClientID%>").value;
            // APPLY MD5 ENCRYPTION TO PASSWORD (USER INPUT)        
            var psswd = sha512(document.getElementById("<%=txtPwd.ClientID%>").value);
            // ADD RANDOM NUMBER WITH MD5 ENCRYPTED PASSWORD
            var saltPsswd = salt + psswd;
            // APPLY MD5 ENCRYPTION TO ABOVE
            var saltPwdHash = sha512(saltPsswd);
            // STORE THE VALUE IN HIDDEN FIELD WHICH WILL BE COMPARED WITH PASSWORD
            document.getElementById("<%=hfPasswd.ClientID%>").value = saltPwdHash;
            var pwd1 = SHA512(SHA512(Pas) + '123456789');
            document.getElementById("<%=hfPas.ClientID%>").value = pwd1;
            // REMOVE RANDOM NUMBER AND PASSWORD FROM FORM FIELD OR HIDDEN FIELD SO THAT THOSE CAN NOT TRAVERSE FROM CLIENT
            document.getElementById("<%=hfRandNo.ClientID%>").value = "";
            document.getElementById("<%=txtPwd.ClientID%>").value = "##############";
            return true;
        }

        function CrearValues() {
            document.getElementById("<%=txtPwd.ClientID%>").value = "";
        }
    </script>
    <script lang="javascript" type="text/javascript">
        function disableRightClick() {
            alert("Sorry, right click is not allowed !!");
            return false;
        }
        function DisableCtrlKey(e) {
            var code = (document.all) ? event.keyCode : e.which;
            var message = "Ctrl key functionality is disabled!";
            var message1 = "F12 key functionality is disabled!";
            //look for CTRL key press
            if (parseInt(code) == 17) {
                alert(message);
                window.event.returnValue = false;
            }

            if (parseInt(code) == 123) {
                alert(message1);
                window.event.returnValue = false;
            }
        }
    </script>
    <script type="text/javascript" lang="javascript">

        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }
    </script>
    <style>
        .btn {
            background-color: #37484A;
            padding: 5px;
            color: white;
            font-family: Tahoma;
            font-size: 12px;
        }

            .btn:hover {
                background-color: #88B64A;
                padding: 5px;
                color: white;
                font-family: Tahoma;
                font-size: 12px;
            }
    </style>
</head>
<body>
    <%--  oncontextmenu="return disableRightClick();" onkeydown="return DisableCtrlKey(event)"--%>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="outertbl">
        <tr>
            <td class="topbarbg">
                <div class="logobg1">
                    <!-- Logo Section-->
                    <div id="logo">
                        <img src="images/logo.gif" width="100px" height="96px">
                    </div>
                    <!-- Logo Section End-->
                    <!-- Title Section-->
                    <div id="titlecontainor">
                        <!-- Title 1-->
                        <div class="title1">
                            INVENTORY OF SEED
                        </div>
                        <div class="title2">
                            Directorate of Agriculture & Food Production,
                        </div>
                        <!-- Title 2-->

                        <!-- Slogan-->
                        <div class="title3">
                            Govt. of Odisha
                        </div>
                    </div>
                    <!-- Title Section End-->
                    <!-- Icon and Info Section-->
                    <div class="icon-info-containor">
                        <table border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="padding-right: 10px; padding-top: 0px;">
                                    <div class="title1">
                                        <span style="color: red; font-size: 18px;">For Further Assitance/Technical Support/Any Querries.</span>
                                    </div>
                                    <div class="title2">
                                        Mail To : <span style="color: yellow; font-size: 18px;">dbt.odisha@gov.in/egovcell.agri.od@nic.in</span>
                                    </div>
                                    <div class="title3">
                                        Help Line No :
                                        <span style="color: yellow;margin-left:20px;">+91-7008542438</span> (10AM - 6 PM)
                                    </div>
                                    <div class="title3">
                                        Farmer Registration & Payment : 
                                        <span style="color: yellow;margin-left:20px;">+91-8093939067</span> (10AM - 6 PM)
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!-- Icon and Info Section End-->
                </div>
            </td>
        </tr>
        <%--<tr>
    <td>

    </td>
</tr>--%>
        <tr style="height: 25px;background-color:yellow;" valign="middle">
            <td valign="middle" >
                <asp:Literal ID="litNews" runat="server"></asp:Literal>
            </td>
        </tr>
        
        <%--<tr style="height: 40px;">
            <td>
                <marquee behavior="scroll" scrolldelay="150" style="color: red; background-color: yellow; font-size: 22px; font-weight: bold;">Services will not be available from 4.30 pm to 7.00 pm on 29th june 2016 due to server maintenance.</marquee>
            </td>
        </tr>--%>
        <%--<tr style="height: 25px;">
            <td style="color: red; background-color: yellow; font-size: 16px; font-weight: bold; text-align: center;">Online seed sale stopped until further notice. </td>
        </tr>--%>
        <%--<tr style="height: 25px;">
            <td style="color: red; background-color: yellow; font-size: 16px; font-weight: bold; text-align: center;">Online seed transactions for Kharif 2017 will be closed at 12 PM on 31st October 2017      </td>
            <td style="color: red; background-color: yellow; font-size: 16px; font-weight: bold; text-align: center;">Online seed transactions for Rabi 2017-18 has been started from 8th November 2017      </td>
        </tr>--%>
        <%--<tr style="height: 25px;">            
            <td style="color: red; background-color: yellow; font-size: 16px; font-weight: bold; text-align: center;">Online seed sale for Rabi 2016-17 will be re-opened from 15/03/2018 10:30 AM to 16/03/2018 4:30 PM.</td>
        </tr>--%>
        <%--<tr style="height: 25px;">            
            <td style="color: red; background-color: yellow; font-size: 16px; font-weight: bold; text-align: center;">Online seed transactions for Rabi 2017-18 is going on.</td>
        </tr>--%>
        <tr>
            <td align="center" valign="middle" class="contentbgLogin">
                <table width="431" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td colspan="3" valign="middle" class="logintopbar">
                            <div style="margin-bottom: 8px;">
                                Login
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#FFFFFF">&nbsp;
                        </td>
                        <td valign="top" bgcolor="#FFFFFF" class="logcentr">
                            <form id="Form2" action="#" method="post" runat="server" autocomplete="off">
                                <div style="float: left; width: 100%; display: block;">
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; background-color: White;"
                                        rules="none">
                                        <tr>
                                            <td colspan="3" align="center">&nbsp;
                                            </td>
                                        </tr>
                                        <tr id="row_errmsg" runat="server">
                                            <td colspan="3" align="center">
                                                <asp:Label ID="lbl_errmsg" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="logintd">User ID
                                            </td>
                                            <td class="logintd" style="width: 5px; text-align: center;">:&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtUserId" runat="server"
                                                    oncut="return false" CssClass="textbox" Width="175" MaxLength="50" autocomplete="off"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="uidReq" runat="server" ControlToValidate="txtUserId"
                                                    ErrorMessage="Please Provide User ID" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="logintd">Password
                                            </td>
                                            <td class="logintd" style="width: 5px; text-align: center;">:&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPwd" runat="server" CssClass="textbox" Width="175" TextMode="Password"
                                                    MaxLength="20" autocomplete="off" onkeypress="return validate_Input_Passwd(event);"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="pwdReq" runat="server" ControlToValidate="txtPwd"
                                                    ErrorMessage="Please Provide Password"></asp:RequiredFieldValidator>
                                                <asp:HiddenField ID="hfRandNo" runat="server" />
                                                <asp:HiddenField ID="hfPasswd" runat="server" />
                                                <asp:HiddenField ID="hfPas" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="logintd">Verification Code</td>
                                            <td class="logintd" style="width: 5px; text-align: center;">:&nbsp;&nbsp;
                                            </td>
                                            <td>

                                                <asp:Image ID="captcha" runat="server" Style="border: solid 1px gray;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td rowspan="4"></td>
                                            <td>&nbsp;</td>
                                            <td style="font-size: 12px; font-family: Tahoma; color: #9E0500;">Enter code as shown above
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:TextBox ID="txtCaptcha" runat="server" Width="100" MaxLength="6" Style="text-transform: uppercase;"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>

                                            <td colspan="2">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:Button ID="btnLogin" runat="server" Width="100PX" Text="Login" OnClientClick="return LoginCheck();" CssClass="btn"
                                                    OnClick="btnLogin_Click" />&nbsp;
                                <asp:Button ID="btnClear" runat="server" Width="100PX" Text="Clear" OnClientClick="return CrearValues();" CssClass="btn"
                                    OnClick="btnClear_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" align="center">&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </form>
                        </td>
                        <td bgcolor="#FFFFFF">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="12" colspan="3" valign="bottom" class="loginbottombar">
                            <img src="images/mask.gif" width="10" height="10">
                        </td>
                    </tr>
                    <tr>
                        <td height="12" colspan="3" valign="bottom" class="log-refl">&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="Loginfooter123">
                <!-- Footer Section-->
                <div>
                    Designed by NIC,Odisha.
                </div>
            </td>
        </tr>
    </table>
    <%--</form>--%>
</body>
</html>
