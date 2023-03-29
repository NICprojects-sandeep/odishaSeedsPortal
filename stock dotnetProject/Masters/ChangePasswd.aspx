<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/MasterPages/STOCK.master" AutoEventWireup="true" CodeFile="~/Masters/ChangePasswd.aspx.cs" Inherits="Manufacture_ChangePasswd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script type="text/javascript" src="../Scripts/sha512.min.js"></script>
    <script type="text/javascript" language="javascript">
        function PasswdCheck() {
            var pwdE = document.getElementById("<%=txtPwdE.ClientID%>").value;
            var pwd = document.getElementById("<%=txtPwd.ClientID%>").value;
            var pwd2 = document.getElementById("<%=txtCnfPwd.ClientID%>").value;
            var pattern = /^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$/;

            if (pwdE == null || pwdE == "") {
                alert('Please Enter Existing Password!');
                document.getElementById("<%=txtPwdE.ClientID%>").value = "";
                document.getElementById("<%=txtPwdE.ClientID%>").focus();
                return false;
            }
            if (pwd == null || pwd == "" || pwd2 == null || pwd2 == "") {
                alert("Please Enter New Password!");
                document.getElementById("<%=txtPwdE.ClientID%>").value = "";
                document.getElementById("<%=txtPwd.ClientID%>").value = "";
                document.getElementById("<%=txtCnfPwd.ClientID%>").value = "";
                document.getElementById("<%=txtPwdE.ClientID%>").focus();
                return false;
            }
            if (pwd.length < 8 || pwd2.length < 8) {
                alert('Password Must Be 8-13 Characters Long!');
                document.getElementById("<%=txtPwdE.ClientID%>").value = "";
                document.getElementById("<%=txtPwd.ClientID%>").value = "";
                document.getElementById("<%=txtCnfPwd.ClientID%>").value = "";
                document.getElementById("<%=txtPwdE.ClientID%>").focus();
                return false;
            }
            if (pwd.search(pattern) == -1) {
                alert('Password does not confirm to Password Policy!');
                document.getElementById("<%=txtPwdE.ClientID%>").value = "";
                document.getElementById("<%=txtPwd.ClientID%>").value = "";
                document.getElementById("<%=txtCnfPwd.ClientID%>").value = "";
                document.getElementById("<%=txtPwdE.ClientID%>").focus();
                return false;
            }
            if (pwd != pwd2) {
                alert('Password And Confirm Password Must Be Same!');
                document.getElementById("<%=txtPwdE.ClientID%>").value = "";
                document.getElementById("<%=txtPwd.ClientID%>").value = "";
                document.getElementById("<%=txtCnfPwd.ClientID%>").value = "";
                document.getElementById("<%=txtPwdE.ClientID%>").focus();
                return false;
            }

            // GET RANDOM NUMBER FROM HIDDEN FIELD WHICH WILL WORK AS SALT
            var salt = document.getElementById("<%=hfRandNo.ClientID%>").value;
            // APPLY MD5 ENCRYPTION TO EXISTING PASSWORD 
            var psswdE = sha512(document.getElementById("<%=txtPwdE.ClientID%>").value);
            // ADD RANDOM NUMBER WITH MD5 ENCRYPTED PASSWORD
            var saltPsswd = salt + psswdE;
            // APPLY MD5 ENCRYPTION TO ABOVE
            var saltPwdHash = sha512(saltPsswd);//old

            // APPLY MD5 ENCRYPTION TO PASSWORD (USER INPUT)        
            var psswd = sha512(document.getElementById("<%=txtPwd.ClientID%>").value);
            // STORE THE ENCRYPTED PASSWORD IN HIDDEN FIELD 
            document.getElementById("<%=hfPasswdE.ClientID%>").value = saltPwdHash;//old
            document.getElementById("<%=hfPasswd.ClientID%>").value = psswd;//new
            // CHANGE PASSWORD VALUE IN FORM 
            document.getElementById("<%=txtPwdE.ClientID%>").value = "##############";
            document.getElementById("<%=txtPwd.ClientID%>").value = "##############";
            document.getElementById("<%=txtCnfPwd.ClientID%>").value = "##############";
            document.getElementById("<%=hfRandNo.ClientID%>").value = "";
            return true;
        }
    </script>
    <asp:UpdatePanel ID="upChngPwd" runat="server">
        <ContentTemplate>
            <div class="bedcromb">
                <asp:Label ID="lblTitle" runat="server" Text="Change Password"></asp:Label>

            </div>

            <div style="padding-top: 15px; padding-left: 30px;">
                <asp:HiddenField ID="hfSession" runat="server" />
                <asp:HiddenField ID="hfPasswdE" runat="server" />
                <asp:HiddenField ID="hfPasswd" runat="server" />
                <asp:Panel ID="Pnl2" runat="server">
                    <div style="width: 900px; background-color: #666; padding: 2px; margin: 0 auto;">
                        <div style="background-color: #FFF; padding: 10px;">
                            <table width="100%" cellpadding="3" cellspacing="3">
                                <tr>
                                    <td align="center" colspan="4">

                                        <asp:Label ID="lblMesg" runat="server" Style="text-decoration: blink;" Visible="false" ForeColor="red" Font-Bold="true" Font-Size="medium"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="140" class="normaltd"></td>
                                    <td width="5" class="normaltd"></td>
                                    <td align="right">
                                        <span class="mandatory">"*" Marked fields are mandatory&nbsp;&nbsp;&nbsp;&nbsp;</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="140" class="normaltd">&nbsp;User Name</td>
                                    <td width="5" class="normaltd">:</td>
                                    <td>
                                        <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr>

                                    <td class="normaltd">&nbsp;Existing Password</td>
                                    <td width="5" class="normaltd">:</td>
                                    <td class="normaltd">
                                        <asp:TextBox ID="txtPwdE" runat="server" TextMode="password" AutoComplete="off" Width="150"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqPwdE" runat="server" ControlToValidate="txtPwdE" ErrorMessage="Enter Existing Password" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>

                                    <td class="normaltd">&nbsp;New Password<span class="mandatory">&nbsp;*</span></td>
                                    <td width="5" class="normaltd">:</td>
                                    <td class="normaltd">
                                        <asp:TextBox ID="txtPwd" runat="server" TextMode="password" AutoComplete="off" Width="150" MaxLength="13"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqPwd" runat="server" ControlToValidate="txtPwd" ErrorMessage="Enter New Password" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                        <asp:HiddenField ID="txtpasswd" Value="" runat="server" />
                                        <br />

                                    </td>
                                </tr>
                                <tr>

                                    <td class="normaltd">&nbsp;Confirm New Password<span class="mandatory">&nbsp;*</span></td>
                                    <td width="5" class="normaltd">:</td>
                                    <td class="normaltd">
                                        <asp:TextBox ID="txtCnfPwd" runat="server" TextMode="password" AutoComplete="off" Width="150" MaxLength="13"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqCnfPwd" runat="server" ControlToValidate="txtCnfPwd" ErrorMessage="Enter Confirm Password" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="cmpPwd" runat="server" ControlToCompare="txtPwd" ControlToValidate="txtCnfPwd" Display="dynamic" ErrorMessage="Password & Confirm Password Should Match" Font-Italic="true"></asp:CompareValidator>

                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" class="normaltd">
                                        <asp:HiddenField ID="SesTicket" runat="server" />
                                        <asp:HiddenField ID="hfRandNo" runat="server" />
                                    </td>
                                </tr>
                                <tr>

                                    <td colspan="3" align="center">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return PasswdCheck();" OnClick="btnSubmit_Click" /></td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="center" valign="middle">&nbsp;</td>
                                </tr>
                                <tr>

                                    <td class="normaltd" style="width: 30%; color: Red;">&nbsp;Note :-</td>
                                    <td class="normaltd" style="width: 65%; color: Red;" colspan="2">Password must be a combination of the following :
                            <br />
                                        1. Atleast one upper case character (A - Z)
                            <br />
                                        2. Atleast one lower case character (a - z)
                            <br />
                                        3. Atleast one numeric digit (0 - 9)
                            <br />
                                        4. Atleast one non-alphanumeric symbol (e.g. @#$%^&+=)
                            <br />
                                        5. Must not contain blank space(s)
                            <br />
                                        6. Password length must be minimum 8 to maximum 13 characters
                            <br />
                                        7. New Password must be different from previous 3 passwords used
                                    </td>
                                </tr>
                            </table>

                        </div>
                    </div>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<asp:UpdateProgress runat="server" ID="Uprogress" AssociatedUpdatePanelID="upChngPwd"
        DisplayAfter="0">
        <ProgressTemplate>
            <div style="text-align: center; vertical-align: middle; position: absolute; top: 0px; left: 0px; z-index: 99; width: 100%; height: 850px; background-color: #ededed; -ms-filter: 'progid:DXImageTransform.Microsoft.Alpha(Opacity=60)'; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=60); -moz-opacity: 0.8; opacity: 0.8;"
                visible="true">
                <div style="width: 48px; height: 48px; margin: 0 auto; margin-top: 275px;">
                    <asp:Image ImageUrl="~/images/loading.gif" Width="48px" Height="48px" runat="server"
                        ID="TaskLoadingImage" />
                </div>
                <div style="font-family: Trebuchet MS; font-size: 12px; color: Red; text-align: center;">
                    Please Wait ...
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
</asp:Content>

