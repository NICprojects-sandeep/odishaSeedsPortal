<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AAO.master" AutoEventWireup="true" CodeFile="AAO_REPORT.aspx.cs" Inherits="Reports_AAO_REPORT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <style type="text/css">
        .auto-style1 {
            font-size: x-large;
        }
    </style>
     <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <center>
        <div style="width: 100%" class="auto-style1"><strong><em>
            <asp:Literal ID="Literal1" runat="server"></asp:Literal></em></strong></div>
        <fieldset class="tbl">
            <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="HiddenField2" runat="server"></asp:HiddenField>
            <legend style="margin-left: 10px;" class="leg">Select any one</legend>

            <table style="width: 90%; padding: 15px;">
                <tr>
                    <td style="width: 175px;" class="normaltd">Dist</td>
                    <td style="width: 5px;">:</td>
                    <td style="width: 250px;">
                        <asp:Label ID="lbl_dst" runat="server" Text="DIst" Style="font-weight: 700; font-style: italic"></asp:Label></td>
                    <td style="width: 115px;" class="normaltd">Block</td>
                    <td style="width: 5px;">:</td>
                    <td style="width: 315px;">
                        <asp:Label ID="lbl_blck" runat="server" Text="Block" Style="font-style: italic"></asp:Label>
                    </td>
                    <td rowspan="2">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="form1.target ='_self';" />
                        <asp:Button ID="btnback" runat="server" Text="Back" OnClientClick="form1.target ='_self';" OnClick="btnback_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="normaltd">Gram Panchayat</td>
                    <td>:</td>
                    <td>
                        <asp:DropDownList ID="drpGp" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="drpGp_SelectedIndexChanged">
                        </asp:DropDownList></td>
                    <td class="normaltd">Village</td>
                    <td>:</td>
                    <td>
                        <asp:DropDownList ID="drpVillage" runat="server" Width="150px"></asp:DropDownList></td>
                </tr>
            </table>
        </fieldset>

        <asp:GridView ID="grd_FarmerList" runat="server" BackColor="#B2C629"
            AutoGenerateColumns="False" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
            CellPadding="4" Width="90%" ForeColor="Black" OnRowDataBound="grd_FarmerList_RowDataBound">
            <RowStyle BackColor="White" CssClass="tbl" />
            <Columns>
                <asp:TemplateField HeaderText="Sl.No" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%# Container.DataItemIndex+1 %>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:HiddenField ID="fcode" runat="server" Value='<%#Eval("VCHFARMERCODE") %>'></asp:HiddenField>
                        <%#Eval("VCHFARMERNAME")%>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Father Name" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbl_NICFARMERID" runat="server" Text='<%#Eval("VCHFATHERNAME")%>'></asp:Label>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Account No" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>

                        <asp:TextBox ID="TextBox2" runat="server" Text='<%#Eval("VCHACCOUNTNO") %>'></asp:TextBox>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Acc. Info" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        Bank:<asp:DropDownList ID="ddl_bank" runat="server" AutoPostBack="true" Width="180px" OnSelectedIndexChanged="ddl_bank_SelectedIndexChanged"></asp:DropDownList>
                        <asp:HiddenField ID="hidbank" runat="server" Value='<%#Eval("INTBANKID") %>'></asp:HiddenField>
                        <br />
                        Branch:<asp:DropDownList ID="ddl_branch" runat="server" Width="180px"></asp:DropDownList>
                        <asp:HiddenField ID="hidbranch" runat="server" Value='<%#Eval("INTBRANCHID") %>'></asp:HiddenField>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="AccountHold. Name" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%#Eval("VCHACCHOLDERNM") %>'></asp:TextBox>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RR" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#Eval("status") %>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Update" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <button id="upd" class="Edit" runat="server" visible='<%# Eval("upst").ToString()=="0"?true:false %>'>Update</button>
                        <asp:Label ID="Label2" runat="server" Text="Updated" ForeColor="#33CC33" Font-Size="Medium" Visible='<%# Eval("upst").ToString()=="0"?false:true %>'></asp:Label>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:TemplateField>

            </Columns>
            <FooterStyle BackColor="#B2C629" />
            <PagerStyle HorizontalAlign="Left" CssClass="pagination" />
            <EmptyDataTemplate>
                <asp:Label ID="Label1" runat="server" Text="Data not found for this VILLAGE,Select another VILLAGE OR GP"
                    Font-Bold="true" ForeColor="red"></asp:Label>
            </EmptyDataTemplate>
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#E6EBED" Font-Bold="True" ForeColor="black" />
        </asp:GridView>
        <asp:Literal ID="litReport" runat="server"></asp:Literal>
    </center>

    <script type="text/javascript">

        $("body").on("click", "[id*=grd_FarmerList] .Edit", function () {
            var row = $(this).closest("tr");
            var fcode = row.find("[id*=fcode]").val();
            var accno = row.find("[id*=TextBox2]").val();
            var bank = row.find("[id*=ddl_bank]").val();
            var branch = row.find("[id*=ddl_branch]").val();
            var holder = row.find("[id*=TextBox1]").val();
            var types = $("[id *= HiddenField1]").val();
            var chby = $("[id *= HiddenField2]").val();
            $.ajax({
                type: "POST",
                url: "AAO_REPORT.aspx/UpdateCustomer",
                data: '{fcode: "' + fcode + '",accno: "' + accno + '",bank: "' + bank + '",branch: "' + branch + '",holder: "' + holder + '",chby: "' + chby + '",types: "' + types + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    row.find(".Edit").hide();
                },
                error: function (response) {
                    alert(response.error);
                }
            });

            //alert(fcode+"/"+accno+"/"+bank+"/"+branch+"/"+holder);
            row.find(".Edit").hide();

            $(this).hide();
            return false;
        });
    </script>
</asp:Content>

