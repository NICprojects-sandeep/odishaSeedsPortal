<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SeedAdmin.master" AutoEventWireup="true" CodeFile="SeedAdminComplaint.aspx.cs" Inherits="Masters_SeedAdminComplaint" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
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


        .Grid {
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
            font-family: Calibri;
            color: #474747;
        }

            .Grid td {
                padding: 2px;
                border: solid 1px #c1c1c1;
            }

            .Grid th {
                padding: 4px 2px;
                color: #fff;
                background: #363670 url(Images/grid-header.png) repeat-x top;
                border-left: solid 1px #525252;
                font-size: 0.9em;
            }

            .Grid .alt {
                background: #fcfcfc url(Images/grid-alt.png) repeat-x top;
            }

            .Grid .pgr {
                background: #363670 url(Images/grid-pgr.png) repeat-x top;
            }

                .Grid .pgr table {
                    margin: 3px 0;
                }

                .Grid .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                }

                .Grid .pgr a {
                    color: Gray;
                    text-decoration: none;
                }

                    .Grid .pgr a:hover {
                        color: #000;
                        text-decoration: none;
                    }
    </style>
    <center>
        <div class="form-group">

            <label for="inputEmail" class="control-label col-xs-2">Complain Type :</label>

            <div class="col-xs-5">

                <asp:DropDownList ID="Ddl_Complain" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Ddl_Complain_SelectedIndexChanged">
                    <asp:ListItem Value="0">--Select-- </asp:ListItem>
                    <asp:ListItem Value="1">Pending</asp:ListItem>
                    <asp:ListItem Value="2">Approoved </asp:ListItem>
                    <asp:ListItem Value="4">Rejected By Admin</asp:ListItem>


                </asp:DropDownList>

            </div>

        </div>
        <div class="form-group">
            <div class="col-xs-12">
                <asp:GridView ID="grdAdminComplain" runat="server" AutoGenerateColumns="False" Width="1000px" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found!"
                    AlternatingRowStyle-CssClass="alt" CssClass="Grid" AllowPaging="True" PageSize="15" OnPageIndexChanging="grdAdminComplain_PageIndexChanging"
                    PagerStyle-CssClass="pgr" OnRowDataBound="grdAdminComplain_RowDataBound">

                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>

                    <Columns>
                        <asp:TemplateField HeaderText="Complain No">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:Label ID="lblcomplaintno" runat="server" Text='<%# Bind("Complain_No") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <HeaderTemplate>
                                Complain Date
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:Label ID="lblcomplaindate" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("Complain_date","{0:dd-MM-yyyy}").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <HeaderTemplate>
                                AAO Circle
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:Label ID="ascocircle" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <HeaderTemplate>
                                Problem
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:Label ID="Lblproblem" runat="server" Text='<%# Bind("Problem") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <HeaderTemplate>
                                Download
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:Button ID="btndownload" runat="server" Text="Download" CommandName="EFG" CommandArgument='<%# Eval("Complain_No") %>'
                                    CssClass="btn btn-success" OnClick="btndownload_Click" />
                                <asp:HiddenField ID="hfDownload" runat="server" Value='<%# Eval("Problem_file") %>' />


                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <HeaderTemplate>
                                Approve
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:Button ID="btnapprove" runat="server" Text="Forward to NIC" CommandName="ABC" CommandArgument='<%# Eval("Complain_No") %>'
                                    OnClick="btnapprove_Click" CssClass="btn btn-success" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <HeaderTemplate>
                                Reject
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:Button ID="Btn_Reject" runat="server" Text="Reject" CommandName="ABC" CommandArgument='<%# Eval("Complain_No") %>'
                                    OnClick="Btn_Reject_Click" CssClass="btn btn-success" />


                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <HeaderTemplate>
                                Return
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:Button ID="Btn_Return" runat="server" Text="Return" CommandName="ABC" CommandArgument='<%# Eval("Complain_No") %>'
                                    OnClick="Btn_Return_Click" CssClass="btn btn-success" />


                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                    <PagerStyle CssClass="pgr"></PagerStyle>
                </asp:GridView>
            </div>
        </div>
        <div class="form-group">
            <div class="col-xs-12">

                <asp:GridView ID="grdAdminApproveComplaint" runat="server" AutoGenerateColumns="False" Width="1000px" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found!"
                    AlternatingRowStyle-CssClass="alt" CssClass="Grid" AllowPaging="True" PageSize="15" OnPageIndexChanging="grdAdminApproveComplaint_PageIndexChanging"
                    PagerStyle-CssClass="pgr">

                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>

                    <Columns>
                        <asp:TemplateField HeaderText="Complain No">
                            <ItemTemplate>
                                <asp:Label ID="lblcomplainno" runat="server" Text='<%# Bind("Complain_No") %>'></asp:Label>
                                <asp:Label ID="Lbl_compA" runat="server" Visible="false" Text='<%# Bind("Complain_Aproove") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sco Circle">
                            <ItemTemplate>
                                <asp:Label ID="lblscocircle" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Asco Circle">
                            <ItemTemplate>
                                <asp:Label ID="lblascocircle" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Problem" HeaderText="Problem" />


                        <asp:TemplateField HeaderText="Complain Date">
                            <ItemTemplate>
                                <asp:Label ID="lbl_compdate" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("Complain_date","{0:dd-MM-yyyy}").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Complain Approve">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Apr" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="File_Name" HeaderText="File Name" />
                        <asp:TemplateField HeaderText="Approve Date">
                            <ItemTemplate>
                                <asp:Label ID="lblcompdate" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("Approve_DateTime","{0:dd-MM-yyyy}").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remark">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Remark" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                    <PagerStyle CssClass="pgr"></PagerStyle>
                </asp:GridView>
            </div>
        </div>
        <cc1:ModalPopupExtender ID="mpe1" runat="server" BehaviorID="popup" TargetControlID="Button1" DynamicServicePath="" Enabled="True"
            PopupControlID="poppanel" CancelControlID="btn_close" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none" />

        <div class="col-lg-1 col-offset-6 centered">
            <asp:Panel ID="poppanel" runat="server" CssClass="modalPopup" align="center" Style="width: 800px; display: none; background-color: #F0FFF0"
                ScrollBars="Auto">
                <table border="0" cellspacing="0" align="center" width="100%" style="font-family: cambria; color: Black; font-size: 15px; background-color: #F0FFF0; font-weight: bold; font-style: normal;">
                    <tr>
                        <td colspan="2">
                            <div class="alert alert-info">

                                <strong>Reject</strong>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td><strong>Reason of Rejection :</strong> </td>
                        <td>
                            <asp:TextBox ID="Txt_Reject" class="form-control" runat="server" TextMode="MultiLine" Width="400px" Height="100px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btn_RJCT" runat="server" class="btn btn-primary" Text="Reject" OnClick="btn_RJCT_Click" />

                            <asp:Button ID="btn_close" runat="server" class="btn btn-primary" Text="GoBack" />

                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>



       <%-- ----------------------%>

        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BehaviorID="popup1" TargetControlID="Button2" DynamicServicePath="" Enabled="True"
            PopupControlID="Panel1" CancelControlID="Button4" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Button ID="Button2" runat="server" Text="Button" Style="display: none" />

        <div class="col-lg-1 col-offset-6 centered">
            <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="width: 800px; display: none; background-color: #F0FFF0"
                ScrollBars="Auto">
                <table border="0" cellspacing="0" align="center" width="100%" style="font-family: cambria; color: Black; font-size: 15px; background-color: #F0FFF0; font-weight: bold; font-style: normal;">
                    <tr>
                        <td colspan="2">
                            <div class="alert alert-info">

                                <strong>Return</strong>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td><strong>Reason of Return :</strong> </td>
                        <td>
                            <asp:TextBox ID="txtReturn" class="form-control" runat="server" TextMode="MultiLine" Width="400px" Height="100px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btn_Return" runat="server" class="btn btn-primary" Text="Return" OnClick="btn_Return_Click" />

                            <asp:Button ID="Button4" runat="server" class="btn btn-primary" Text="GoBack" />

                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>

        <%-----------------------%>
        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BehaviorID="popup2" TargetControlID="Button3" DynamicServicePath="" Enabled="True"
            PopupControlID="Panel2" CancelControlID="Button4" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none" />

        <div class="col-lg-1 col-offset-6 centered">
            <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" align="center" Style="width: 800px; display: none; background-color: #F0FFF0"
                ScrollBars="Auto">
                <table border="0" cellspacing="0" align="center" width="100%" style="font-family: cambria; color: Black; font-size: 15px; background-color: #F0FFF0; font-weight: bold; font-style: normal;">
                    <tr>
                        <td colspan="2">
                            <div class="alert alert-info">

                                <strong>Approval</strong>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td><strong>Approve :</strong> </td>
                        <td>
                            <asp:TextBox ID="Text_approve" class="form-control" runat="server" TextMode="MultiLine" Width="400px" Height="100px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btn_Approve" runat="server" class="btn btn-primary" Text="Approve" OnClick="btn_Approve_Click" />

                            <asp:Button ID="Button6" runat="server" class="btn btn-primary" Text="GoBack" />

                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>


    </center>
</asp:Content>

