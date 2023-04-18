<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FarmerIdentification.aspx.cs"
    Inherits="FarmerIdentification" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <style>
        .Border {
            border: dashed black 2px;
            width: 900px;
            padding-bottom: 30px;
            clear: both;
        }

        .tbl {
            font-family: Tahoma;
            font-size: 18px;
            padding: 10px;
        }

        .Header {
            padding-bottom: 10px;
            font-size: 18px;
            text-decoration: underline;
            text-align: center;
        }

        .Header1 {
            padding-top: 20px;
            padding-bottom: 10px;
            font-size: 21px;
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
    </style>
    <style>
        @media print {
            table {
                page-break-inside: avoid;
            }
        }
    </style>
    <script language="javascript" type="text/javascript">
        function Print() {
            window.print();
            // window.close();
            window.onfocus = function () { window.close(); }
        }

    </script>
</head>
<body>
    <center>
        <div class="Border">
            <table width="100%" class="tbl" cellspacing="10px">
                <tr>
                    <td colspan="2" class="Header1">Department of Agriculture and Farmers' Empowerment
                        <br />
                        Govt. of Odisha
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="Header">Acknowledgement Slip
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div style="float: left; width: 250px; padding-left: 20px;">
                            <img src="images/ogl.png" width="120px" height="120px" />
                        </div>
                        <div style="float: right; width: 150px;" id="td_QrCode" runat="server">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;" colspan="2">
                        <b>Farmer Identification No. - <u>
                            <asp:Literal ID="litFarmerId" runat="server"></asp:Literal></u></b>
                    </td>
                </tr>
                <tr>
                    <td>Farmer's Name -
                        <asp:Literal ID="litFarmerName" runat="server"></asp:Literal>
                    </td>
                    <td>
                        <asp:Label ID="lblFH" runat="server"></asp:Label>
                        -
                        <asp:Literal ID="litFather" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>Gender -
                        <asp:Literal ID="litGender" runat="server"></asp:Literal>
                    </td>
                    <td>Category -
                        <asp:Literal ID="litCategory" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>Dist -
                        <asp:Literal ID="litDist" runat="server"></asp:Literal>
                    </td>
                    <td>Block -
                        <asp:Literal ID="litBlock" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>Grampanchayat -
                        <asp:Literal ID="litGp" runat="server"></asp:Literal>
                    </td>
                    <td>Village -
                        <asp:Literal ID="litVillage" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
            <div class="nb">
                <span style="color: red;">*</span> NB : Farmer Identification No. may be used for Direct Benefit Transfer (DBT) subject to verification of details. 
                 <br />
                &nbsp;&nbsp;&nbsp;&nbsp;  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;This is a computer generated document.
            </div>
        </div>
        <div style="height: 30px;">
            &nbsp;
        </div>
        <div class="Border">
            <table width="100%" class="tbl" cellspacing="10px">
                <tr>
                    <td colspan="2" class="Header1">Department of Agriculture and Farmers' Empowerment
                        <div style="float: right; padding-left: 20px; font-size: 14px; font-weight: bold;"><u>Duplicate</u></div>
                        <br />
                        Govt. of Odisha
                       
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="Header">Acknowledgement Slip
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div style="float: left; width: 250px; padding-left: 20px;">
                            <img src="images/ogl.png" width="120px" height="120px" />
                        </div>
                        <div style="float: right; width: 150px;" id="td_QrCode2" runat="server">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;" colspan="2">
                        <b>Farmer Identification No. - <u>
                            <asp:Literal ID="litFarmerId1" runat="server"></asp:Literal></u></b>
                    </td>
                </tr>
                <tr>
                    <td>Farmer's Name -
                        <asp:Literal ID="litFarmerName1" runat="server"></asp:Literal>
                    </td>
                    <td><asp:Label ID="Label1" runat="server"></asp:Label> -  
                        <asp:Literal ID="litFather1" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>Gender - 
                        <asp:Literal ID="litGender1" runat="server"></asp:Literal>
                    </td>
                    <td>Category -  
                        <asp:Literal ID="litCategory1" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>Dist -  
                        <asp:Literal ID="litDist1" runat="server"></asp:Literal>
                    </td>
                    <td>Block -  
                        <asp:Literal ID="litBlock1" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>Grampanchayat -  
                        <asp:Literal ID="litGp1" runat="server"></asp:Literal>
                    </td>
                    <td>Village -  
                        <asp:Literal ID="litVillage1" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
            <div class="nb">
                <span style="color: red;">*</span> NB : Farmer Identification No. may be used for Direct Benefit Transfer (DBT) subject to verification of details. 
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;This is a computer generated document.
            </div>
        </div>
    </center>
</body>
</html>
