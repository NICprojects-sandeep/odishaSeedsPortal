<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TotalRegisterdfarmer.aspx.cs" Inherits="Reports_TotalRegisterdfarmer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
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
            padding:2px;
        }
        .tbl {
            font-family: Tahoma;
            font-size: 14px;
            margin-top: 80px;
            background-color: #E6EBED;
            width: 90%;
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
        
    </style>
<script>

        function Valid() {
            var FarmId = document.getElementById("<%=drpDist.ClientID %>");
            if (FarmId.value.trim() == "--- SELECT ---") {
                alert('Please Select District !');
                FarmId.focus();
                return false;
            }
        }      
    </script>
   
         <center>
            <fieldset class="tbl">

                <legend style="margin-left:10px;" class="leg">Select any type</legend>
           
             <table style="width: 90%; padding: 15px;" >
                            <tr>
                                <td style="width: 175px;" class="normaltd">Dist</td>
                                <td style="width: 5px;">:</td>
                                <td style="width: 250px;">
                                    <asp:DropDownList ID="drpDist" runat="server" Width="150px" OnSelectedIndexChanged="drpDist_SelectedIndexChanged" AutoPostBack="true">                                    
                                    </asp:DropDownList></td>
                                <td style="width: 115px;" class="normaltd">Block</td>
                                <td style="width: 5px;">:</td>
                                <td style="width: 315px;">                                    
                                    <asp:DropDownList ID="drpBlock" runat="server" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="drpBlock_SelectedIndexChanged">
                                  
                                    </asp:DropDownList></td>
                                <td rowspan="2"><asp:Button id="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="aspnetForm.target ='_self'; return Valid();"/></td>
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
                        <asp:LinkButton ID="lnkVCard" runat="server" Text='<%#Eval("VCHVOTERIDCARDNOV")%>' CausesValidation="false" OnClientClick="aspnetForm.target ='_blank';"></asp:LinkButton>
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
    </form>
</body>
</html>
