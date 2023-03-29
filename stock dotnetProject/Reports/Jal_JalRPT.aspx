<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ADAPT.master" CodeFile="Jal_JalRPT.aspx.cs" Inherits="Jalanidhi_Jal_JalRPT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <link href="../../css/bootstrap.css" rel="stylesheet" />
<%-- <link href="../../css/style.css" rel="stylesheet" />--%>
      <style type="text/css">
    .gvHeader th{
   padding:3px; 
    background-color:#DDEECC; 
   color:maroon; 
   border:1px solid #bbb;}
 .gvRow td{padding:3px; 
    background-color:#ffffff; 
    border:1px solid #bbb;}
.gvAltRow td{
    padding:3px; 
    background-color:#f1f1f1; 
    border:1px solid #bbb;}
 </style>
     <script type="text/javascript">

         function getPrint(print_area) {
             var divToPrint = document.getElementById(print_area);
             var printWin = window.open('', 'Mail In', 'left=0,top=0,width=1000,height=1000,status=0');
             printWin.document.write("<div align='center' style='width:1000px;margin:auto;padding-top:10px'>");
             printWin.document.write(divToPrint.outerHTML);
             printWin.document.write("</div>");
             printWin.document.close();
             printWin.focus();
             printWin.print();
         }
    </script>

    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <div class="container">
                <div class="row ">
                    <div class="col-lg-12 col-md-12 col-sm-12">
                       
                        <div class="panel panel-success" style="margin-top: 6px;">

                            <div class="row">&nbsp;</div>
                            <div class="row">
                                <div class="col-md-2 col-sm-12"></div>
                                <div style="margin-top: 4px; FONT-WEIGHT: bold" class="col-md-2 col-sm-12 text-right Text"><a>Financial Year</a>:</div>
                                <div class="col-md-4 col-sm-12 text-left">
                                    <asp:DropDownList ID="ddlfyr" runat="server" CssClass="form-control" AutoPostBack="True">
                                        <asp:ListItem Text="--Select--" Value="0" />
                                        <asp:ListItem Text="2017-18" Value="2017-18" />
                                        <asp:ListItem Text="2018-19" Value="2018-19" />
                                        <asp:ListItem Text="2019-20" Value="2019-20" />
                                    </asp:DropDownList>
                                    
                                    
                                </div>
                                <div class="col-md-4 col-sm-12 text-left">
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-2 col-sm-12"></div>
                                <div style="margin-top: 4px; FONT-WEIGHT: bold" class="col-md-2 col-sm-12 text-right Text"><a>Plip</a>:</div>
                                <div class="col-md-4 col-sm-12 text-left">
                                    <asp:DropDownList ID="ddlPlpi" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Ddldist_SelectedIndexChanged" >

                                         <asp:ListItem Text="--Select--" Value="0" />
                                        <asp:ListItem Text="Bore Well" Value="Bore Well" />
                                        <asp:ListItem Text="STW" Value="STW" />
                                    </asp:DropDownList>
                                                                        
                                </div>
                                <div class="col-md-4 col-sm-12 text-left">
                                </div>
                            </div>


                            <div class="row">&nbsp;</div>
                        </div>
                        
                       
                        <div id="divPrint" class="text-center" runat="server">
                            <asp:Literal ID="litReport" runat="server"></asp:Literal>
                        </div>
                        <div class="row">&nbsp;</div>
                         <div class="row" runat="server" id="divButton">
                        <div class="col-sm-12 col-md-3 col-lg-12 text-center"  >
                         <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-success btn-group-sm active" Text="Print" OnClientClick="getPrint('divPrint')" OnClick="btnPrint_Click"/>
                          <asp:Button ID="btnEperttoexcel" CssClass="btn btn-success btn-group-sm active" Text="Expert To Excel" runat="server" OnClick="btnEperttoexcel_Click" />
                        </div>
                             </div>
                        
                    </div>
                </div>
            </div>
        <%--</ContentTemplate>
               <Triggers>
                <asp:PostBackTrigger ControlID="btnEperttoexcel" />
                
            </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>
