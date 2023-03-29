<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AAO.master" AutoEventWireup="true" CodeFile="AaoComplainAproove.aspx.cs" Inherits="Reports_AaoComplainAproove" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <style type="text/css">
        .Grid {
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
            font-family: Calibri;
            color: #474747;
        }

            .Grid td {
                border: solid 1px #c1c1c1;
            }

            .Grid th {
                padding: 4px 2px;
                color: #fff;
                background: #363670 url(images/grid-header.png) repeat-x top;
                border-left: solid 1px #525252;
                font-size: 0.9em;
            }

            .Grid .alt {
                background: #fcfcfc url(images/grid-alt.png) repeat-x top;
            }

            .Grid .pgr {
                background: #363670 url(images/grid-pgr.png) repeat-x top;
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


        .btnl {
            background: #228B22;
            background-image: -webkit-linear-gradient(top, #0b3754, #5da11e);
            background-image: -moz-linear-gradient(top, #0b3754, #5da11e);
            background-image: -ms-linear-gradient(top, #0b3754, #5da11e);
            background-image: -o-linear-gradient(top, #0b3754, #5da11e);
            background-image: linear-gradient(to bottom, #0b3754, #5da11e);
            -webkit-border-radius: 28;
            -moz-border-radius: 28;
            border-radius: 28px;
            font-family: Arial;
            color: #ffffff;
            font-size: 13px;
            padding: 0px 40px 0px 40px;
            border: solid #1f628d 2px;
            text-decoration: none;
            width: 85px;
            height: 28px;
        }

            .btnl:hover {
                background: #3c46fc;
                background-image: -webkit-linear-gradient(top, #3c46fc, #3498db);
                background-image: -moz-linear-gradient(top, #3c46fc, #3498db);
                background-image: -ms-linear-gradient(top, #3c46fc, #3498db);
                background-image: -o-linear-gradient(top, #3c46fc, #3498db);
                background-image: linear-gradient(to bottom, #3c46fc, #3498db);
                text-decoration: none;
            }
    </style>

    <label for="inputEmail" class="control-label col-xs-2">Complain Type :</label>

    <div class="col-xs-5">

        <asp:DropDownList ID="Ddl_Complain" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Ddl_Complain_SelectedIndexChanged">
            <asp:ListItem Value="0">--Select-- </asp:ListItem>
            <asp:ListItem Value="1">Registered</asp:ListItem>
            <asp:ListItem Value="2">Approoved </asp:ListItem>
            <asp:ListItem Value="3">Resolved</asp:ListItem>
            <asp:ListItem Value="4">Rejected By Admin</asp:ListItem>




        </asp:DropDownList>

    </div>

    <div class="form-group">
        <div class="col-xs-12">
            <asp:GridView ID="Grid_status" runat="server" CssClass="Grid" PagerStyle-CssClass="pgr" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found!"
                Width="850px" AutoGenerateColumns="false" AllowPaging="True" PageSize="15" OnPageIndexChanging="Grid_status_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="Complain No">
                        <ItemTemplate>
                            <asp:Label ID="lblcomplainno" runat="server" Text='<%# Bind("Complain_No") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                        <HeaderTemplate>
                            Complain Date
                        </HeaderTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px" />
                        <ItemTemplate>
                            <asp:Label ID="lblcomplaindate" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("Complain_date","{0:dd-MM-yyyy}").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Problem" HeaderText="Problem" />
                    <asp:TemplateField HeaderText="Complain Status">
                        <ItemTemplate>
                            <asp:Label ID="lblAproov" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Approve Date">
                        <ItemTemplate>
                            <asp:Label ID="lblAprooveDate" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remark">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Remark" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>

