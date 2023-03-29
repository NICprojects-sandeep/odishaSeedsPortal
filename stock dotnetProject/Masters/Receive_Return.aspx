<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/STOCK.master" AutoEventWireup="true" CodeFile="Receive_Return.aspx.cs" Inherits="Masters_Receive_Return" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <script type="text/javascript">
        function Validate() {
            var ddl_godown = document.getElementById("ddl_godown");
            var txtfrmdt = document.getElementById("txtfrmdt");
            var txttodt = document.getElementById("txttodt");
            if (ddlfinyr.selectedIndex == 0) {
                alert("Please select Godown!");
                return false;
            }
            else if (txtfrmdt.value == "") {
                alert("Please select Date !!");
                return false;
            }
            else if (txttodt.value == "") {
                alert("Please select Date!");
                return false;
            }
            return true;
        }
    </script>
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
        }
        /*------Dropdownlist CSS---------*/
        select.DropdownCss {
            border: 1px solid #7bc1f7;
            width: 200px;
            height: 25px;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            color: #00213d;
            padding: 4px 4px 4px 4px;
            border-radius: 4px;
            -moz-border-radius: 4px;
            -webkit-border-radius: 4px;
            box-shadow: 0px 0px 8px #7bc1f7;
            -moz-box-shadow: 0px 0px 8px #7bc1f7;
            -webkit-box-shadow: 0px 0px 8px #7bc1f7;
        }

            select.DropdownCss:focus {
                outline: none;
                border: 1px solid #135e8a;
                box-shadow: 0px 0px 8px #CF0A4C;
                -moz-box-shadow: 0px 0px 8px #CF0A4C;
                -webkit-box-shadow: 0px 0px 8px #CF0A4C;
            }
        /*------TextBox CSS---------*/
        input.TextboxCss {
            border: 1px solid #7bc1f7;
            width: 200px;
            height: auto;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            color: #00213d;
            padding: 4px 4px 4px 4px;
            border-radius: 4px;
            -moz-border-radius: 4px;
            -webkit-border-radius: 4px;
            box-shadow: 0px 0px 2px #7bc1f7;
            -moz-box-shadow: 0px 0px 2px #7bc1f7;
            -webkit-box-shadow: 0px 0px 2px #7bc1f7;
        }

            input.TextboxCss:focus {
                outline: none;
                border: 1px solid #135e8a;
                box-shadow: 0px 0px 2px #D42367;
                -moz-box-shadow: 0px 0px 2px #D42367;
                -webkit-box-shadow: 0px 0px 2px #D42367;
            }

        /*------TextArea CSS---------*/
        /*------Button CSS---------*/

        .ButtonCss {
            border: 1px solid #fff;
            width: 100px;
            height: 28px;
            font-size: 13px;
            padding: 4px 4px 4px 4px;
            border-radius: 14px;
            -moz-border-radius: 14px;
            -webkit-border-radius: 14px;
            box-shadow: 0px 0px 8px #7bc1f7;
            -moz-box-shadow: 0px 0px 8px #7bc1f7;
            -webkit-box-shadow: 0px 0px 8px #7bc1f7;
            display: inline-block;
            background-color: #050742;
            color: #ffffff;
            font-family: Arial;
            font-size: 15px;
            font-weight: bold;
            padding: 5px 15px;
            text-decoration: none;
            text-align: center; /*text-shadow:0px 4px 0px #b23e35;*/
        }

            .ButtonCss:hover {
                outline: none;
                border: 1px solid #fff;
                box-shadow: 0px 0px 8px #008b8b;
                -moz-box-shadow: 0px 0px 8px #008b8b;
                -webkit-box-shadow: 0px 0px 8px #008b8b;
                background-color: #008b8b;
            }

            .ButtonCss:active {
                position: relative;
                top: 5px;
            }

        .tbltxtbox {
            font-family: Tahoma, Geneva, sans-serif;
            font-size: 11px;
            color: #000;
            width: 250px;
            border: 1px solid #999;
        }

        .datalisttopbar {
            font-family: Tahoma, Geneva, sans-serif;
            font-size: 12px;
            font-weight: bold;
            color: #FFF;
            background-color: #070849;
        }

        .datalistalternaterow {
            background-color: #F7F6F3;
            color: #000;
        }

        .datalistdefaultrow {
            font-family: Tahoma, Geneva, sans-serif;
            font-size: 12px;
            background-color: #FFF;
            color: #000;
        }

            .datalistdefaultrow a:link {
                font-family: Tahoma, Geneva, sans-serif;
                font-size: 12px;
                color: #000;
                text-decoration: underline;
            }

            .datalistdefaultrow a:visited {
                font-family: Tahoma, Geneva, sans-serif;
                font-size: 12px;
                color: #000;
                text-decoration: none;
            }

            .datalistdefaultrow a:hover {
                font-family: Tahoma, Geneva, sans-serif;
                font-size: 12px;
                color: #63C;
                text-decoration: none;
            }

        .searchtb {
            font-family: Tahoma, Geneva, sans-serif;
            font-size: 11px;
            color: #000;
            width: 120px;
            border: 1px solid #333;
        }

        .tblsmalltxtbox {
            font-family: Tahoma, Geneva, sans-serif;
            font-size: 11px;
            color: #000;
            width: 100px;
            border: 1px solid #999;
        }

        .booktxtbox {
            font-family: Tahoma, Geneva, sans-serif;
            font-size: 11px;
            color: #000;
            width: 350px;
            border: 1px solid #999;
        }

        .textAlignText {
            text-align: right;
        }

        input.redtxt {
            border: 1px solid #7bc1f7;
            width: 200px;
            height: auto;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            color: White;
            background-color: Gray;
            padding: 4px 4px 4px 4px;
            border-radius: 4px;
            -moz-border-radius: 4px;
            -webkit-border-radius: 4px;
            box-shadow: 0px 0px 2px #7bc1f7;
            -moz-box-shadow: 0px 0px 2px #7bc1f7;
            -webkit-box-shadow: 0px 0px 2px #7bc1f7;
        }
    </style>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div>
                <center>
                    <asp:Panel ID="Panel1" runat="server" Width="60%">
                        <fieldset>
                            <table width="100%">
                                <tr>
                                    <td>Select GoDown :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_godown" runat="server" AutoPostBack="true" CssClass="DropdownCss"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>From Date
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtfrmdt" runat="server" CssClass="TextboxCss" OnTextChanged="txtfrmdt_TextChanged"></asp:TextBox>

                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter Date"
                                            ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
                                            EnableClientScript="False" ControlToValidate="txtfrmdt" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtfrmdt" Format="dd/MM/yyyy" />
                                    </td>
                                    <td>To Date
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txttodt" runat="server" CssClass="TextboxCss" AutoPostBack="true" OnTextChanged="txttodt_TextChanged"></asp:TextBox>

                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Enter Date"
                                            ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
                                            EnableClientScript="False" ControlToValidate="txttodt" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txttodt" Format="dd/MM/yyyy" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click" CssClass="ButtonCss" OnClientClick="return Validate();" />
                                        &nbsp; &nbsp;
                                        <asp:Button ID="btncan" runat="server" Text="Cancel" OnClick="btncan_Click" CssClass="ButtonCss" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </asp:Panel>
                    <br />
                    <br />
                    <asp:Panel ID="gvRcvDtls" runat="server" Width="1200px" ScrollBars="Auto">
                        <asp:GridView ID="grd_RecvDetails" runat="server" AutoGenerateColumns="false"
                            BackColor="#B2C629" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                            CellPadding="4" Width="100%" ForeColor="Black" DataKeyNames="RECVTRANSID" AllowPaging="true"
                            PageSize="6" OnPageIndexChanging="grd_RecvDetails_PageIndexChanging" OnRowDataBound="grd_RecvDetails_RowDataBound">
                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="6" FirstPageText="First" LastPageText="Last" />
                            <RowStyle BackColor="White" CssClass="tbl" />
                            <Columns>
                                <asp:TemplateField HeaderText="ReceiveTransactionID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblrecivetransid" runat="server" Text='<%#Eval("RECVTRANSID")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Agency_Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblagentcyname" runat="server" Text='<%#Eval("AgenciesName")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ReferenceNo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblmourefno" runat="server" Text='<%# Eval("MOU_REFNO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Challan No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblchallan" runat="server" Text='<%# Eval("Challan_No") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vareity">
                                    <ItemTemplate>
                                        <%-- <asp:Label ID="lblcropvariety" runat="server" Text='<%# Eval("Crop_Verid") %>' />--%>
                                        <asp:Label ID="lblcropvariety" runat="server" Text='<%# Eval("Variety_Name") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lot No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lbllotnumber" runat="server" Text='<%# Eval("Lot_No") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Recv.No.Of.Bags">
                                    <ItemTemplate>
                                        <asp:Label ID="lblrecvnoofbags" runat="server" Text='<%# Eval("Recv_No_Of_Bags") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Avl.No.Of.Bags">
                                    <ItemTemplate>
                                        <asp:Label ID="lblavlnoofbags" runat="server" Text='<%# Eval("AVL_NO_OF_BAGS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bag Size">
                                    <ItemTemplate>
                                        <asp:Label ID="lblbagsize" runat="server" Text='<%# Eval("Bag_Size_In_Kg") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Recv.Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblrecvquan" runat="server" Text='<%# Eval("Recv_Quantity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Avl.Stock.Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblavlstquan" runat="server" Text='<%# Eval("Avl_Quantity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Season">
                                    <ItemTemplate>
                                        <asp:Label ID="lblseason" runat="server" Text='<%# Eval("SEASSION_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Recv_Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbtransactiondate" runat="server" Text='<%# Eval("Recv_Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblusertyp" runat="server" Text='<%# Eval("User_Type") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action ?">
                                    <ItemTemplate>
                                        <asp:Button ID="btndel" runat="server" Text="Delete" OnClick="btndel_Click" OnClientClick="return confirm('Are you sure you want to delete this TransID?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                    </td>
                        </tr>
                    </table>
                </center>
            </div>
            <%--<asp:UpdateProgress runat="server" ID="Uprogress" AssociatedUpdatePanelID="UpdatePanel1"
                DisplayAfter="0">
                <ProgressTemplate>
                    <div style="text-align: center; vertical-align: middle; position: absolute; top: 0px; left: 0px; z-index: 99; width: 100%; height: 1000px; background-color: #ededed; -ms-filter: 'progid:DXImageTransform.Microsoft.Alpha(Opacity=60)'; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=60); -moz-opacity: 0.8; opacity: 0.8;"
                        visible="true">
                        <div style="width: 48px; height: 48px; margin: 0 auto; margin-top: 275px;">
                            <asp:Image ImageUrl="~/images/loading.gif" Width="48px" Height="48px" runat="server"
                                ID="TaskLoadingImage" />
                        </div>
                        <div style="font-family: Trebuchet MS; font-size: 12px; color: Red; text-align: center;">
                            Please Wait ...
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>--%>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

