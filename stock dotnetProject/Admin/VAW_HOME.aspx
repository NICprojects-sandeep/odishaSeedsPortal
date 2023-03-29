<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/VAW.master" AutoEventWireup="true" CodeFile="VAW_HOME.aspx.cs" Inherits="Admin_VAW_HOME" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
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
        <div style="padding-top: 60px;" class="watermark">
            Wel-Come
            <br />
            to<br />
            <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
        </div>
    </div>
</asp:Content>

