<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ADAPT.master" AutoEventWireup="true" CodeFile="JAL_ADAPRpt.aspx.cs" Inherits="Reports_JAL_ADAPRpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <link href="css/style.css" rel="stylesheet" />
    <link href="css/bootstrap.css" rel="stylesheet" />
    <div class="container">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12">

                        <div class="panel panel-success" >

                            <div class="row">&nbsp;</div>
                            <div class="row">
                                <div class="col-md-2 col-sm-12"></div>
                                <div style="margin-top: 4px; FONT-WEIGHT: bold" class="col-md-2 col-sm-12 text-right Text"><a>Financial Year</a>:</div>
                                <div class="col-md-4 col-sm-12 text-left">
                                    <asp:DropDownList ID="ddlfyr" runat="server" CssClass="DropdownCss" AutoPostBack="True" OnSelectedIndexChanged="ddlfyr_SelectedIndexChanged">
                                        <asp:ListItem Text="--Select--" Value="0" />
                                        <asp:ListItem Text="2017-18" Value="2017-18" />
                                        <asp:ListItem Text="2018-19" Value="2018-19" />
                                        <asp:ListItem Text="2019-20" Value="2019-20" />
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4 col-sm-12 text-left">
                                </div>
                            </div>
                            <div class="row">&nbsp;</div>
                            <div class="row">&nbsp;</div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12">

                        <div class="panel panel-success">
                             <asp:Button ID="btnEperttoexcel" CssClass="btn btn-success btn-group-sm active" Text="Expert To Excel" runat="server" OnClick="btnEperttoexcel_Click" /><br />
                            <asp:Literal ID="litReport" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
            </div>
</asp:Content>

