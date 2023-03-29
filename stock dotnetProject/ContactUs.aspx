<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContactUs.aspx.cs" Inherits="ContactUs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>::: DBT :::</title>
    <link rel="Shortcut Icon" type="image/x-icon" style="width: 50px;" href="images/thumb2.ico" />
    <link rel="stylesheet" href="css/otb-admin.css" media="all" type="text/css" />
    <script src="Scripts/pngfix.js" type="text/javascript" lang="javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.js"></script>
    <script type="text/javascript" src="Scripts/popup.js"></script>
    <script type="text/javascript" src="Scripts/md5.js"></script>
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

        .auto-style1 {
            color: #FFFFFF;
            font-weight: bold;
        }

        .auto-style2 {
            color: #000000;
        }

        .icon-info-containorcontact {
            width: 35%;
            height: 96px;
            float: right;
            background-color: #121212;
        }
    </style>
</head>
<body style="background-color: #ECECEC;">

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
                    <div class="icon-info-containorcontact">
                        <table border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="float: right; padding-top: 10px;">
                                    <form id="Form2" action="#" method="post" runat="server" autocomplete="off">
                                        <div class="title1" runat="server">
                                            <asp:ImageButton ID="imgHome" runat="server" ImageUrl="images/BackContact.png" Height="50px" ToolTip="Seed Portal" OnClick="imgHome_Click" />
                                            &nbsp;
                                         <asp:ImageButton ID="imgLogin" runat="server" ImageUrl="images/LoginContact.png" Height="50px" ToolTip="DBT Login" OnClick="imgLogin_Click" />

                                        </div>
                                    </form>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td style="background-image: url(images/contactusbanner.jpg); background-repeat: no-repeat;" height="100%" width="100%">
                <div style="padding-left: 63%; padding-top: 130px;">
                    <p class="auto-style1"><span style="color: red; font-size: 18px;">For Further Assitance/Technical Support/Any Querries</span></p>

                    <p><i><span class="auto-style2"><strong>Mail To :</strong><strong><span style="color: #9A0000; font-size: 18px;"> dbt.odisha@gov.in</span></strong></span></i></p>
                    <p>
                        <i><span class="auto-style2"><strong>Help Line No : </strong><strong><span style="color: #9A0000;">+91-7894077717</span>&nbsp;(10AM - 6 PM)</strong></span></i>
                    </p>
                    <p class="auto-style2">&nbsp;</p>
                </div>
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
