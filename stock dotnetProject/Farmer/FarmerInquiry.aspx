<%@ Page Title="" Language="C#" MasterPageFile="~/Farmer/MasterPage.master" AutoEventWireup="true" CodeFile="FarmerInquiry.aspx.cs" Inherits="Farmer_FarmerInquiry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            margin-top: 80px;
            background-color: #E6EBED;
            width: 65%;
            border: 1px solid;
            border-radius: 5px;
            border-collapse: collapse;
        }

        .Header {
            padding-top: 20px;
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
    </style>
    <center style="height:600px;">
        <div class="tbl">
            Please Enter Your Voter Id Number Correctly :
            <asp:TextBox ID="txtVoterId" runat="server" CssClass="txtbox"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Submit" CssClass="btn" OnClick="btnSearch_Click" />
        </div>

    </center>

</asp:Content>

