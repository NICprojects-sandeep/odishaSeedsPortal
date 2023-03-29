<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ADAPT.master" AutoEventWireup="true" CodeFile="AdaptHome.aspx.cs" Inherits="Admin_AdaptHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <style type="text/css">
        .watermark {
            /*position: fixed;
            bottom: 5px;
            right: 5px;
            opacity: 0.5;
            z-index: 99;*/
            color: #CBBE94;
            font-size: 70px;
        }
    </style>
   <%-- <div>
        <marquee behavior="scroll" scrolldelay="150" style="color: red; background-color: yellow; font-size: 18px; font-weight: bold;">The MOBILE NO. and AADHAR NO. change option will be effective from 5:30 pm today</marquee>
    </div>--%>
    <div class="Hometxt">
        <asp:HiddenField ID="hfSession" runat="server" />
        <asp:HiddenField ID="hfMCode" runat="server" />
        <div style="padding-top: 60px;" class="watermark">
            Wel-Come
            <br />
            to<br />
            <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
        </div>
    </div>
</asp:Content>

