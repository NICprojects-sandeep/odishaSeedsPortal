﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Approve_Page.aspx.cs" Inherits="Approve_Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="CSS/panel_nav/prettify.css" rel="stylesheet">

     <link href="font-awesome-4.2.0/css/font-awesome.css" rel="stylesheet">
      <link href="font-awesome-4.2.0/css/font-awesome.min.css" rel="stylesheet">

     <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css" />
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap-theme.min.css" />
    <script type="text/jscript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <script type="text/jscript" src="http://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js"></script>
    <script type="text/jscript" src="CSS/panel_nav/jquery-latest.js"></script>
    <script type="text/jscript" src="CSS/panel_nav/bootstrap.js"></script>
    <script type="text/jscript" src="CSS/panel_nav/jquery.js"></script>
    <script type="text/jscript" src="CSS/panel_nav/prettify.js"></script>
    <script type="text/jscript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.3.1/js/bootstrap-datepicker.js"></script>
    <style type="text/css">
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        .modalPopup
        {
          
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            padding-right: 10px;
            padding-bottom: 10px;
            width: 800px;
        }
        
        
        .Grid
        {
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
            font-family: Calibri;
            color: #474747;
        }
        .Grid td
        {
            padding: 2px;
            border: solid 1px #c1c1c1;
        }
        .Grid th
        {
            padding: 4px 2px;
            color: #fff;
            background: #363670 url(Images/grid-header.png) repeat-x top;
            border-left: solid 1px #525252;
            font-size: 0.9em;
        }
        .Grid .alt
        {
            background: #fcfcfc url(Images/grid-alt.png) repeat-x top;
        }
        .Grid .pgr
        {
            background: #363670 url(Images/grid-pgr.png) repeat-x top;
        }
        .Grid .pgr table
        {
            margin: 3px 0;
        }
        .Grid .pgr td
        {
            border-width: 0;
            padding: 0 6px;
            border-left: solid 1px #666;
            font-weight: bold;
            color: #fff;
            line-height: 12px;
        }
        .Grid .pgr a
        {
            color: Gray;
            text-decoration: none;
        }
        .Grid .pgr a:hover
        {
            color: #000;
            text-decoration: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="alert alert-info">
        
                           <strong>Complains</strong> 
                         </div>
    <div>
    <asp:GridView ID="grdAdminComplain" runat="server" AutoGenerateColumns="False" Width="1000px" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found!" 
         AlternatingRowStyle-CssClass="alt" CssClass="Grid" AllowPaging="True"  PageSize="15" onpageindexchanging="grdAdminComplain_PageIndexChanging"
                      PagerStyle-CssClass="pgr">
        
<AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
        
        <Columns>
            <asp:TemplateField HeaderText="Complain No">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"/>
                <ItemTemplate>
                    <asp:Label ID="lblcomplaintno" runat="server" Text='<%# Bind("Complain_No") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderTemplate>
                        Complain Date</HeaderTemplate>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                <ItemTemplate>
                    <asp:Label ID="lblcomplaindate" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("Complain_date","{0:dd-MM-yyyy}").ToString()) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderTemplate>
                        AAO Circle</HeaderTemplate>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                <ItemTemplate>
                    <asp:Label ID="ascocircle" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
         <%--   <asp:TemplateField>
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderTemplate>
                        SCO Circle</HeaderTemplate>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                <ItemTemplate>
                    <asp:Label ID="scocircle" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField>
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderTemplate>
                        Problem</HeaderTemplate>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                <ItemTemplate>
                    <asp:Label ID="Lblproblem" runat="server" Text='<%# Bind("Problem") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
            <HeaderTemplate>
                        Download</HeaderTemplate>
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                <ItemTemplate>
                    <asp:Button ID="btndownload" runat="server" Text="Download" CommandName="EFG" CommandArgument='<%# Eval("Complain_No") %>'
                        CssClass="btn btn-success" OnClick="btndownload_Click"  />
                </ItemTemplate>
            </asp:TemplateField>

            
             <asp:TemplateField>
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderTemplate>
                        Reject</HeaderTemplate>
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />                
                <ItemTemplate>
                    <asp:Button ID="Btn_Reject" runat="server" Text="Reject" CommandName="ABC" CommandArgument='<%# Eval("Complain_No") %>'
                      OnClick="Btn_Reject_Click"   CssClass="btn btn-success"/>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderTemplate>
                        Action</HeaderTemplate>
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />                
                <ItemTemplate>
                    <asp:Button ID="Btn_Resolve" runat="server" Text="Resolved" CommandName="DEF" CommandArgument='<%# Eval("Complain_No") %>'
                      OnClick="Btn_Resolve_Click"   CssClass="btn btn-success"/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

<PagerStyle CssClass="pgr"></PagerStyle>
    </asp:GridView>
    </div>
     <cc1:ModalPopupExtender ID="mpe1" runat="server" BehaviorID="popup" TargetControlID="Button1" DynamicServicePath="" Enabled="True"
            PopupControlID="poppanel" CancelControlID="btn_close" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none" />
        
              <div class="col-lg-1 col-offset-6 centered" >
            <asp:Panel ID="poppanel" runat="server"  CssClass="modalPopup" align="center" Style="width: 800px; display:none;
                background-color: #F0FFF0" ScrollBars="Auto" >
                  <table border="0" cellspacing="0" align="center" width="100%" style="font-family: cambria;
                    color: Black; font-size: 15px; background-color: #F0FFF0; font-weight: bold;
                    font-style: normal;">
                    <tr>
                    <td colspan="2">
                    <div class="alert alert-info">
        
                           <strong>Reject</strong> 
                         </div
                    </td>
                    </tr>
                    <tr>
                    <td><strong>Reason :</strong> </td>
                    <td> <asp:TextBox ID="Txt_Reject" class="form-control" runat="server" TextMode="MultiLine" Width="400px"></asp:TextBox></td>
                    </tr>
                    <tr>
                    <td colspan="2">
                       <asp:Button ID="btn_RJCT" runat="server" class="btn btn-primary"  Text="Reject" onclick="btn_RJCT_Click" />
                     
                       <asp:Button ID="btn_close" runat="server" class="btn btn-primary"  Text="GoBack" />
                    
                    </td></tr>
                    </table>
               </asp:Panel>
                </div>
    </div>
    </form>
</body>
</html>