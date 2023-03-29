<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/SeedAdmin.master" CodeFile="CCBdetails.aspx.cs" Inherits="CCBdetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>    

     

        
        <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
         <%--<style>
       
        .Grid {background-color: #fff; margin: 5px 0 10px 0; border: solid 1px #525252; border-collapse:collapse; font-family:Calibri; color: #474747; width: 100%;}

.Grid td {

      

      border: solid 1px #c1c1c1; }

.Grid th  {

      padding : 4px 2px;

      color: #fff;

      background: #363670 url(images/grid-header.png) repeat-x top;

      border-left: solid 1px #525252;

      font-size: 0.9em; }

.Grid .alt {

      background: #fcfcfc url(images/grid-alt.png) repeat-x top; }

.Grid .pgr {background: #363670 url(images/grid-pgr.png) repeat-x top; }

.Grid .pgr table { margin: 3px 0; }

.Grid .pgr td { border-width: 0; padding: 0 6px; border-left: solid 1px #666; font-weight: bold; color: #fff; line-height: 12px; }  

.Grid .pgr a { color: Gray; text-decoration: none; }

.Grid .pgr a:hover { color: #000; text-decoration: none; }
.ddl
        {
            border: 2px solid GREEN;
            color: Purple;
            border-radius: 5px;
            -webkit-appearance: none;
            background: url(/images/downArrow.png);
            background-position: right;
            background-repeat: no-repeat;
            font-family: 'Comic Sans MS';
            font-size: medium;
            text-align: left;
            height: 25px;
        }

        </style>--%>
        <style type="text/css">
            .modal
        {
            position: fixed;
            z-index: 9;
          height:100%;
          width:55%;
            top: 0;
            background-color: #f3ecec;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }
        .center
        {
            z-index: 10;
            margin: 300px auto;
            padding: 10px;
            width: 130px;
            background-color: White;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }
        .center img
        {
            height: 128px;
            width: 128px;
        }
        </style>
       



            <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>--%>
        <%--<title>CCB DETAILS</title>--%>



<br />

<div id="printdiv">
<div class="panel panel-success">
<div class="panel-heading">
<strong><b>PFMS TRANSACTION DETAILS FOR 2018-19 AND ONWARDS</b></strong>
</div>
    <br />
<div class="panel-body ol_pay">
<DIV class="row" align="center">
    <table style="width:100%;">
        <tr>
            <td style="width:20%;">

            </td>
            <td style="width:60%;">
                <b>Enter CPAID :</b><asp:TextBox ID="txtRef" runat="server" CssClass="form-control" AutoPostBack="True" ontextchanged="ddl_TextChanged"></asp:TextBox>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRef"
                            ErrorMessage="*" Font-Bold="True" Font-Size="X-Small" SetFocusOnError="True"
                            ValidationGroup="a"></asp:RequiredFieldValidator>
            </td>
            <td style="width:20%;">

            </td>
        </tr>
        <tr>
            <td style="width:20%;">

            </td>
            <td style="width:60%;">
                <b>Select Dealer :</b><asp:DropDownList ID="drpDealer" runat="server" Width="250px">  
        </asp:DropDownList>   
            </td>
            <td style="width:20%;">

            </td>
        </tr>
    </table>

<%--<div class="col-lg-3 col-md-3 col-sm-3" style="text-align:right">
                        
                    </div>
         <div class="col-lg-3 col-md-3 col-sm-3">
                        
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3" style="text-align:right">
                        
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3">
                    
           
            </div>--%>
</DIV>
<div>
<div class="div" align="center">
    <br />
       <asp:Button ID="Button1" class="btn btn-success" Text="Search" runat="server" onclick="btn_search_Click"/>  
    </div>
</div>

    <asp:GridView ID="Grd_optsumm" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found!"
        AlternatingRowStyle-CssClass="alt"   CssClass="Grid" ShowFooter="true" >
    <Columns>
    <asp:TemplateField HeaderText="SL NO"  HeaderStyle-Width="10px"  HeaderStyle-HorizontalAlign="Left"  >
                  <HeaderStyle HorizontalAlign="Left" Font-Size="small" />
              <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Size="medium" />
                   <ItemTemplate>
                    <%# ((GridViewRow)Container).RowIndex + 1%>
                </ItemTemplate>
                </asp:TemplateField>
    
    
    <asp:BoundField HeaderText="TRANSACTION DATE AND TIME" HtmlEncode="true" DataField="UPDATED_ON" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-Font-Size="Small" HeaderStyle-Font-Size="Small" HeaderStyle-VerticalAlign="Middle"
                    ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="70" />
    <asp:BoundField HeaderText="TRANSACTION ID" HtmlEncode="true" DataField="TRANSACTION_ID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-Font-Size="Small" HeaderStyle-Font-Size="Small" HeaderStyle-VerticalAlign="Middle"
                    ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="70" />
    <asp:BoundField HeaderText="FARMER ID" HtmlEncode="true" DataField="FARMER_ID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-Font-Size="Small" HeaderStyle-Font-Size="Small" HeaderStyle-VerticalAlign="Middle"
                    ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="70" />
    <asp:BoundField HeaderText="FARMER NAME" HtmlEncode="true" DataField="Beneficiary_Name" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-Font-Size="Small" HeaderStyle-Font-Size="Small" HeaderStyle-VerticalAlign="Middle"
                    ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="70" />
    <asp:BoundField HeaderText="CROP NAME" HtmlEncode="true" DataField="Crop_Name" ItemStyle-Font-Size="Smaller"
                    HeaderStyle-Font-Size="Small" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle"
                    ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="90" />    
    <asp:BoundField HeaderText="VARIETY NAME" HtmlEncode="true" DataField="Variety_Name" ItemStyle-Font-Size="Smaller"
                    HeaderStyle-Font-Size="Small" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle"
                    ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="90" />  
    <asp:BoundField HeaderText="QUANTITY(In QTL)" HtmlEncode="true" DataField="TOT_QTL" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-Font-Size="Small" HeaderStyle-Font-Size="Small" HeaderStyle-VerticalAlign="Middle"
                    ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="70" DataFormatString="{0:N1}"/>    
    <asp:BoundField HeaderText="SUBSIDY TO BE PAID" HtmlEncode="true" DataField="Total_Payment_Amount" ItemStyle-Font-Size="Smaller"
                    HeaderStyle-Font-Size="Small" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle"
                    ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="90" DataFormatString="{0:N2}" />     
         
                
            </Columns>
    </asp:GridView>                                 


   
    </div>
    </div>
    </div>
    </asp:Content>