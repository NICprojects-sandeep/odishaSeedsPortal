<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AAO.master" AutoEventWireup="true" CodeFile="Aao_Adhar_Updation.aspx.cs" Inherits="Reports_Aao_Adhar_Updation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <style>
        .Border {
            border: dashed black 2px;
            width: 900px;
            padding-bottom: 30px;
            clear: both;
        }

        .leg {
            font-family: Tahoma;
            font-size: 14px;
            background-color: #E6EBED;
            border: 1px solid;
            border-radius: 5px;
            border-collapse: collapse;
            padding: 2px;
        }

        .tbl {
            font-family: Tahoma;
            font-size: 14px;
            margin-top: 80px;
            background-color: #E6EBED;
            width: 90%;
            border: 1px solid;
            border-radius: 5px;
            border-collapse: collapse;
            padding: 5px;
            margin-bottom: 30px;
        }

        .Header {
            padding-top: 10px;
            padding-bottom: 10px;
            font-size: 24px;
            text-decoration: underline;
            text-align: center;
        }

        .nb {
            font-size: 14px;
            font-family: Comic Sans MS;
            text-align: left;
            width: 100%;
            padding-left: 20px;
            padding-top: 20px;
        }

        .btn {
            background-color: #E6EBED;
            padding: 5px;
            color: black;
            font-family: Tahoma;
            font-size: 12px;
            width: 100px;
            font-weight: bold;
            border-radius: 5px;
            border-collapse: collapse;
        }

            .btn:hover {
                background-color: #CED6D9;
                padding: 5px;
                color: black;
                font-family: Tahoma;
                font-size: 12px;
                width: 100px;
                font-weight: bold;
                border-radius: 5px;
                border-collapse: collapse;
            }

        .txtbox {
            font-family: Tahoma, Geneva, sans-serif;
            font-size: 12px;
            color: #000000;
            width: 280px;
            padding: 5px;
        }

        .tabletxt {
            font-family: Tahoma, Geneva, sans-serif;
            color: #000;
            font-size: 12px;
            padding: 5px;
        }

        .normaltd {
            padding: 6px;
        }

        button:link {
            color: red;
        }

        /* visited link */
        button:visited {
            color: green;
        }

        /* mouse over link */
        button:hover {
            color: hotpink;
        }

        /* selected link */
        button:active {
            color: blue;
        }
    </style>
    <script>
        function Valid() {
            var Aadharno = document.getElementById("<%=txtadharno.ClientID %>");
            if (Aadharno.value.trim() == "") {
                alert('Please Enter the Aadhar No.!');
                Aadharno.focus();
                return false;
            }
        }
        function Valid1() {
            var FarmId = document.getElementById("<%=txtfarmerid.ClientID %>");
             if (FarmId.value.trim() == "") {
                 alert('Please Enter the Farmer Id!');
                 FarmId.focus();
                 return false;
             }
         }
    </script>
    <div>
        <div align="center">
            <h2><u>AAO FOR AADHAAR NO UPDATION</u></h2>
        </div>
        <div align="center">
            <b>Farmer Id : </b>
            <asp:TextBox ID="txtfarmerid" CssClass="txtbox" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
            <asp:Button ID="btnsearch" CssClass="btn" runat="server" Text="Search" OnClientClick="return Valid1();" OnClick="btnsearch_Click" />
        </div>

        <fieldset class="tbl">
            <legend style="margin-left: 10px;" class="leg">Updation By AAO
            </legend>
            <div style="margin-left: 80px">
                <table style="width: 90%; padding: 15px;">
                    <tr>
                        <td style="width: 150px;" class="normaltd">Farmer Name</td>
                        <td style="width: 5px;">:</td>
                        <td style="width: 300px;">
                            <asp:TextBox ID="txtfarmername" runat="server" CssClass="txtbox" placeholder="Farmer Name" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px;" class="normaltd">Father's Name</td>
                        <td style="width: 5px;">:</td>
                        <td style="width: 300px;">
                            <asp:TextBox ID="txtfathername" runat="server" CssClass="txtbox" placeholder="Father's Name" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <%-- <tr>
                      <td style="width: 150px;" class="normaltd">Date of Birth</td>
                      <td style="width: 5px;">:</td>
                       <td style="width: 300px;">
                           <asp:TextBox ID="txtdbo" runat="server" placeholder="Date of Birth" ReadOnly="True"></asp:TextBox>
                     </tr>--%>
                    <tr>
                        <td style="width: 150px;" class="normaltd">Aadhaar No</td>
                        <td style="width: 5px;">:</td>
                        <td style="width: 300px;">
                            <asp:TextBox ID="txtadharno" runat="server" CssClass="txtbox" TextMode="Number" placeholder="Aadhaar No"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 150px;" class="normaltd"></td>
                        <td style="width: 5px;">:</td>
                        <td style="width: 300px;">
                            <asp:Button ID="btnupdate" CssClass="btn" runat="server" Text="Update" OnClientClick="return Valid();" OnClick="btnupdate_Click" />&nbsp;&nbsp;
                       <asp:Button ID="btnClear1" CssClass="btn" runat="server" Text="Clear" OnClick="btnClear1_Click" />
                        </td>
                    </tr>
                </table>

            </div>
        </fieldset>
    </div>
</asp:Content>

