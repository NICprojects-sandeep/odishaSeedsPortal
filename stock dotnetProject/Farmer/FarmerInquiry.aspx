<%@ Page Title="" Language="C#" MasterPageFile="~/Farmer/MasterPage.master" AutoEventWireup="true" CodeFile="FarmerInquiry.aspx.cs" Inherits="Farmer_FarmerInquiry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .Border {
            border: dashed black 2px;
            width: 900px;
            padding-bottom: 30px;
            clear: both;
        }

        .tbl {
            font-family: Tahoma;
            font-size: 18px;
            padding: 10px;
            margin-top: 80px;
            background-color: #E6EBED;
            width: 65%;
            border: 1px solid;
            border-radius: 5px;
            border-collapse: collapse;
        }

        .Header {
            padding-top: 20px;
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
    </style>
       <center style="height:600px;">
        <div class="tbl">
            Please Enter Reference Id/ Voter Id/ Aadhar Number/Mobile Number/Account Number Correctly :
            <asp:TextBox ID="txtVoterId" runat="server" CssClass="txtbox"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Submit" CssClass="btn" OnClick="btnSearch_Click" />
        </div>
        <div><asp:Literal ID="litMsg" runat="server"></asp:Literal></div>
        <div style="padding-top:20px;">  <asp:GridView ID="grdInactive" runat="server" AutoGenerateColumns="False" Width="800px" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                    <Columns>
                        <asp:TemplateField HeaderText="SlNo" ItemStyle-Width="10px">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>

<ItemStyle Width="10px"></ItemStyle>
                        </asp:TemplateField>
                    
                         <asp:BoundField HeaderText="Farmer Name" DataField="VCHFARMERNAME" ItemStyle-Width="75px" >
                             
<ItemStyle Width="75px"></ItemStyle>
                        </asp:BoundField>
                             
                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="100px">
                                                    <ItemTemplate>
                                                   <asp:Button ID="btnView" Text="View" runat="server" OnClick="btnView_Click" />
                                           <asp:HiddenField id="hfVid" runat="server"  value='<%#Eval("VCHVOTERIDCARDNO").ToString().Trim()%>'></asp:HiddenField>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:TemplateField>
                                          
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView> </div>
    </center>


</asp:Content>

