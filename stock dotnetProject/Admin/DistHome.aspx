<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Dist.master" AutoEventWireup="true" CodeFile="DistHome.aspx.cs" Inherits="Admin_DistHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
      .
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

