<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AAO.master" AutoEventWireup="true" CodeFile="FarmerList_Block.aspx.cs" Inherits="Reports_FarmerList_Block" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
        <style type="text/css">
        .auto-style1 {
            font-size: x-large;
        }
    </style>
      <div style="width:100%" class="auto-style1"><strong><em><asp:Literal ID="Literal1" runat="server"></asp:Literal></em></strong></div>
            <fieldset class="tbl">

                <legend style="margin-left:10px;" class="leg">Select any one</legend>
           
             <table style="width: 90%; padding: 15px;" >
                            <tr>
                                <td style="width: 175px;" class="normaltd">Dist</td>
                                <td style="width: 5px;">:</td>
                                <td style="width: 250px;">
                                    <asp:Label ID="lbl_dst" runat="server" Text="DIst" style="font-weight: 700; font-style: italic"></asp:Label></td>
                                <td style="width: 115px;" class="normaltd">Block</td>
                                <td style="width: 5px;">:</td>
                                <td style="width: 315px;">                                    
                                   <asp:Label ID="lbl_blck" runat="server" Text="Block" style="font-style: italic"></asp:Label> </td>
                                <td rowspan="2"><asp:Button id="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="form1.target ='_self';"/>
                                    <asp:Button id="btnback" runat="server" Text="Back"  OnClientClick="form1.target ='_self';" OnClick="btnback_Click"/>
                                </td>
                            </tr>
                            <tr>
                                <td class="normaltd">Gram Panchayat</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="drpGp" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="drpGp_SelectedIndexChanged" >
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
            CellPadding="4" Width="90%" ForeColor="Black">
            <RowStyle BackColor="White" CssClass="tbl" />
            <Columns>

<asp:TemplateField HeaderText="Sl No." HeaderStyle-HorizontalAlign="Left"> <ItemTemplate> <%#Container.DataItemIndex+1 %> </ItemTemplate> </asp:TemplateField>
                <asp:TemplateField HeaderText="Voter Id" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>

                        <asp:HiddenField ID="hfVCard" runat="server" Value='<%#Eval("VCHVOTERIDCARDNO")%>' />
                        
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Farmer Id" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbl_NICFARMERID" runat="server" Text='<%#Eval("NICFARMERID")%>'></asp:Label>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Farmer Name" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbl_VCHFARMERNAME" runat="server" Text='<%#Eval("VCHFARMERNAME")%>'></asp:Label>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Father's Name" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbl_VCHFATHERNAME" runat="server" Text='<%#Eval("VCHFATHERNAME")%>'></asp:Label>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Category" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Category_Value" runat="server" Text='<%#Eval("Category_Value")%>'></asp:Label>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Gender" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Gender_Value" runat="server" Text='<%#Eval("Gender_Value")%>'></asp:Label>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Mobile" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbl_VCHMOBILENO" runat="server" Text='<%#Eval("VCHMOBILENOV")%>'></asp:Label>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:TemplateField>

            </Columns>
            <FooterStyle BackColor="#B2C629" />
            <PagerStyle HorizontalAlign="Left" CssClass="pagination" />
            <EmptyDataTemplate>
                <asp:Label ID="Label1" runat="server" Text="Data not found for this Variety,Select another Variety"
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
            var chby = 'ashok';
            $.ajax({
                type: "POST",
                url: "AAO_REPORT.aspx/UpdateCustomer",
                data: '{fcode: "' + fcode + '",accno: "' + accno + '",bank: "' + bank + '",branch: "' + branch + '",holder: "' + holder + '",chby: "' + chby + '"}',
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

