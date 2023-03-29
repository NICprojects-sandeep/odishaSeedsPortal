<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BankLogin.aspx.cs" Inherits="BankLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Login</title>
    <link href="css/login.css" rel="stylesheet" />
    
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
            margin: 10px;
            float: left;
        }

            .btn:hover {
                background-color: #88B64A;
                padding: 5px;
                color: white;
                font-family: Tahoma;
                font-size: 12px;
            }

        .Loginfooter123 {
            background: url(images/footerbg.jpg) repeat-x left top;
            width: 100%;
            text-align: center;
            color: #FFFFFF;
            padding: 10px;
            font-size: 12px;
          min-height:20px;
        }
    </style>
</head>
<body>
    <form id="Form2" action="#" method="post" runat="server" autocomplete="off">
        <div class="mainlog">
            <div class="loghead">
                <div class="logo">
                </div>
                <div class="logotext">
                    <p style="text-align: center;">
                        Bank Aggregator System
                    </p>
                </div>
                <div style="float: left; padding-left: 50px; padding-top: 20px;" title="Go to Home">
                </div>
            </div>
        </div>

        <main style="margin-bottom:290px;">
            <h1>sign in</h1>

            <div class="flp">
                 <div>  <asp:Label ID="lbl_errmsg" runat="server"></asp:Label></div>
                <div>
                      <asp:TextBox ID="fname" runat="server" autocomplete="off"> </asp:TextBox>
                    <label for="fname">User Name</label>
                      <asp:RequiredFieldValidator ID="uidReq" runat="server" ControlToValidate="fname"
                                                    ErrorMessage="Please Provide User ID" SetFocusOnError="true"></asp:RequiredFieldValidator>



                </div>
                <div>
                 <%--   <input type="password" id="email" name="pass" autocomplete="off"/>--%>

                    <asp:TextBox ID="email" runat="server" autocomplete="off" TextMode="Password"> </asp:TextBox>
                    <label for="email">Password</label>
                     <asp:RequiredFieldValidator ID="pwdReq" runat="server" ControlToValidate="email"
                                                    ErrorMessage="Please Provide Password"></asp:RequiredFieldValidator>
                </div>
                <div class="buttonsub">
                
                    <asp:Button ID="btnLogin" runat="server" Width="100" Text="Login" OnClientClick="return LoginCheck();" CssClass="btn"
                                                    OnClick="btnLogin_Click" />


                                <asp:Button ID="btnClear" runat="server" Width="100" Text="Clear" OnClientClick="return CrearValues();" CssClass="btn"
                                    OnClick="btnClear_Click" />
                  
                        
                </div>
                  <asp:HiddenField ID="hfRandNo" runat="server" />
                                                <asp:HiddenField ID="hfPasswd" runat="server" />
            </div>
          
        </main>
        <div class="Loginfooter123">
            <!-- Footer Section-->
            <div>
                Designed by NIC,Odisha.
            </div>
        </div>


        <!-- jQuery -->
        <script src="Scripts/jquery-1.11.3.min.js"></script>


        <script src="Scripts/sha512.min.js"></script>

        <!-- jQuery easing plugin for animation fun -->
        <script src="Scripts/easing.js"></script>
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
            var uid = document.getElementById("<%=fname.ClientID%>").value;
            var pwd = document.getElementById("<%=email.ClientID%>").value;
            var pwdSmall = /[a-z]/; //SMALL LETTER
            var pwdCap = /[A-Z]/; //CAPITAL LETTER
            var pwdSpl = /[\W_]/; //SPECIAL CHARECTER
            var RegExUID = /^([a-zA-Z_@])$/;


            if (uid == null || uid == "") {
                alert('Please Enter User ID!');
                document.getElementById("<%=fname.ClientID%>").focus();
                document.getElementById("<%=email.ClientID%>").value = "";
                return false;
            }
            if (pwd == null || pwd == "") {
                alert("Please Enter Password!");
                document.getElementById("<%=email.ClientID%>").value = "";
                document.getElementById("<%=email.ClientID%>").focus();
                return false;
            }
            if (pwd.length < 6) {
                alert('Minimum Six Characters are Required!');
                document.getElementById("<%=email.ClientID%>").value = "";
                document.getElementById("<%=email.ClientID%>").focus();
                return false;
            }
            // GET RANDOM NUMBER FROM HIDDEN FIELD WHICH WILL WORK AS SALT
            var salt = document.getElementById("<%=hfRandNo.ClientID%>").value;
            // APPLY MD5 ENCRYPTION TO PASSWORD (USER INPUT)        
            var psswd = sha512(document.getElementById("<%=email.ClientID%>").value);
            // ADD RANDOM NUMBER WITH MD5 ENCRYPTED PASSWORD
            var saltPsswd = salt + psswd;
            // APPLY MD5 ENCRYPTION TO ABOVE
            var saltPwdHash = sha512(saltPsswd);
            // STORE THE VALUE IN HIDDEN FIELD WHICH WILL BE COMPARED WITH PASSWORD
            document.getElementById("<%=hfPasswd.ClientID%>").value = saltPwdHash;
            // REMOVE RANDOM NUMBER AND PASSWORD FROM FORM FIELD OR HIDDEN FIELD SO THAT THOSE CAN NOT TRAVERSE FROM CLIENT
            document.getElementById("<%=hfRandNo.ClientID%>").value = "";
            document.getElementById("<%=email.ClientID%>").value = "##############";
            return true;
        }

        function CrearValues() {
            document.getElementById("<%=pwdReq.ClientID%>").value = "";
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
        <script type="text/javascript">

            //breakdown the labels into single character spans
            $(".flp label").each(function () {
                var sop = '<span class="ch">'; //span opening
                var scl = '</span>'; //span closing
                //split the label into single letters and inject span tags around them
                $(this).html(sop + $(this).html().split("").join(scl + sop) + scl);
                //to prevent space-only spans from collapsing
                $(".ch:contains(' ')").html("&nbsp;");
            })

            var d;
            //animation time
            $(".flp input").focus(function () {
                //calculate movement for .ch = half of input height
                var tm = $(this).outerHeight() / 2 * -1 + "px";
                //label = next sibling of input
                //to prevent multiple animation trigger by mistake we will use .stop() before animating any character and clear any animation queued by .delay()
                $(this).next().addClass("focussed").children().stop(true).each(function (i) {
                    d = i * 50; //delay
                    $(this).delay(d).animate({ top: tm }, 200, 'easeOutBack');
                })
            })
            $(".flp input").blur(function () {
                //animate the label down if content of the input is empty
                if ($(this).val() == "") {
                    $(this).next().removeClass("focussed").children().stop(true).each(function (i) {
                        d = i * 50;
                        $(this).delay(d).animate({ top: 0 }, 500, 'easeInOutBack');
                    })
                }
            })

        </script>

        <script type="text/javascript">

            $(document).ready(function () {
                $("input:text, form").attr("autocomplete", "off");
                var Finalhash = null;
                $('#btnLogin').click(function (e) {
                    // debugger;
                    var strVal = $('#email').val();

                    var getDataSlt = $('#hfRandNo').val();

                    Finalhash = sha512(getDataSlt + sha512(strVal));                    
                    $('#email').val(Finalhash);
                });

            });

        </script>
    </form>

</body>
</html>
