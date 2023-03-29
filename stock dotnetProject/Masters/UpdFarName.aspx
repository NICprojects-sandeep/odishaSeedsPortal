<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Dist.master" AutoEventWireup="true" CodeFile="UpdFarName.aspx.cs" Inherits="Masters_UpdFarName" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <style>
        .Border {
            border: dashed black 2px;
            width: 900px;
            padding-bottom: 30px;
            clear: both;
        }

        .leg {
            font-family: Tahoma;
            font-size: 14px;
            background-color: #E6EBED;
            border: 1px solid;
            border-radius: 5px;
            border-collapse: collapse;
            padding: 2px;
        }

        .tbl {
            font-family: Tahoma;
            font-size: 14px;
            margin-top: 80px;
            background-color: #E6EBED;
            width: 100%;
            border: 1px solid;
            border-radius: 5px;
            border-collapse: collapse;
            padding: 5px;
            margin-bottom: 30px;
        }

        .Header {
            padding-top: 10px;
            padding-bottom: 10px;
            font-size: 24px;
            text-decoration: underline;
            text-align: center;
        }

        .nb {
            font-size: 14px;
            font-family: Comic Sans MS;
            text-align: left;
            width: 100%;
            padding-left: 20px;
            padding-top: 20px;
        }

        .btn {
            background-color: #E6EBED;
            padding: 5px;
            color: black;
            font-family: Tahoma;
            font-size: 12px;
            width: 100px;
            font-weight: bold;
            border-radius: 5px;
            border-collapse: collapse;
        }

            .btn:hover {
                background-color: #CED6D9;
                padding: 5px;
                color: black;
                font-family: Tahoma;
                font-size: 12px;
                width: 100px;
                font-weight: bold;
                border-radius: 5px;
                border-collapse: collapse;
            }

        .txtbox {
            font-family: Tahoma, Geneva, sans-serif;
            font-size: 12px;
            color: #000000;
            width: 280px;
            padding: 5px;
        }

        .tabletxt {
            font-family: Tahoma, Geneva, sans-serif;
            color: #000;
            font-size: 12px;
            padding: 5px;
        }

        .normaltd {
            padding: 6px;
        }

        .auto-style1 {
            padding: 6px;
            width: 82px;
        }

        .auto-style2 {
            width: 202px;
        }

        .auto-style3 {
            width: 196px;
        }
    </style>
    <%-- <table >
        <tr>
            <td>
                Enter Farmer ID :
            </td>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>/<asp:TextBox ID="txtfarmerid" runat="server"></asp:TextBox>
            </td>
        </tr>
        <td colspan="2" align="center">
            <asp:Button ID="btnsearch" runat="server" Text="Search" />
        </td>
         </table>--%>


    <script type="text/javascript">

        function validate(key) {

            var keycode = (key.which) ? key.which : key.keyCode;
            var phn = document.getElementById('txtmobno');

            if (!(keycode == 8 || keycode == 46) && (keycode < 48 || keycode > 57)) {
                return false;
            }
        }

    </script>

    <br />

    <table runat="server" width="100%" cellpadding="3" cellspacing="3">
        <tr>
            <td>
                <asp:HiddenField ID="hfSession" runat="server" Visible="false" />
                <asp:HiddenField ID="hfDistCode" runat="server" Visible="false" />
                <asp:GridView ID="grd_updfarnm" runat="server" AutoGenerateColumns="false"
                    BackColor="#B2C629" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                    CellPadding="4" Width="100%" ForeColor="Black"
                    DataKeyNames="VCHFARMERCODE" OnRowEditing="grd_updfarnm_RowEditing" OnRowUpdating="grd_updfarnm_RowUpdating" OnRowCancelingEdit="grd_updfarnm_RowCancelingEdit">

                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="15" FirstPageText="First" LastPageText="Last" />

                    <RowStyle BackColor="White" CssClass="tbl" />
                    <Columns>
                        <asp:TemplateField HeaderText="Dist ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblDistCode" runat="server" Text='<%#Eval("VCHDISTID")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Farmer Code">
                            <ItemTemplate>
                                <asp:Label ID="lblfarmercode" runat="server" Text='<%# Eval("VCHFARMERCODE") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Farmer Name">
                            <ItemTemplate>
                                <asp:Label ID="lblfarmername" runat="server" Text='<%# Eval("VCHFARMERNAME") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtfarmername" runat="server" Text='<%# Eval("VCHFARMERNAME") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Father Name">
                            <ItemTemplate>
                                <asp:Label ID="lblfarmerfathrnm" runat="server" Text='<%# Eval("VCHFATHERNAME") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtfarmerfathrnm" runat="server" Text='<%# Eval("VCHFATHERNAME") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Account Holder Name">
                            <ItemTemplate>
                                <asp:Label ID="lblaccntholdrnm" runat="server" Text='<%# Eval("VCHACCHOLDERNM") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAccHoldname" runat="server" Text='<%# Eval("VCHACCHOLDERNM") %>'></asp:TextBox>
                                <%--onkeypress="return blockNonNumbers(this, event, true, false);"--%>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mobile No.">
                            <ItemTemplate>
                                <asp:Label ID="lblmob" runat="server" Text='<%# Eval("VCHMOBILENO") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtmobno" runat="server" Text='<%# Eval("VCHMOBILENO") %>' onkeypress="return validate(event)" MaxLength="10"></asp:TextBox>
                                <%--onkeypress="return blockNonNumbers(this, event, true, false);"--%>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbtnedit" runat="server" CommandName="EDIT">EDIT</asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="lnkbtnupd" runat="server" CommandName="UPDATE">UPDATE</asp:LinkButton>
                                <asp:LinkButton ID="lnkbtncan" runat="server" CommandName="CANCEL">CANCEL</asp:LinkButton>
                            </EditItemTemplate>


                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>


    </table>


</asp:Content>

