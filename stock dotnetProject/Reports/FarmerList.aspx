<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="FarmerList.aspx.cs" Inherits="Farmer_FarmerList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
        <div class="titleHeader">
        &nbsp;
            Farmer List
    </div>
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
            width: 90%;
            border: 1px solid;
            border-radius: 5px;
            border-collapse: collapse;
            padding: 5px;
            margin-bottom: 30px;
        }

        .Header {
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

        .modalPopup {
            background-color: #FFFFFF;
            border: 3px solid #0DA9D0;
            border-radius: 12px;
            padding: 0;
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup .body {
            min-height: 100px;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
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

            <legend style="margin-left: 10px;" class="leg">Select any type</legend>

            <table style="width: 90%; padding: 5px;">
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
                    <td rowspan="2">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="aspnetForm.target ='_self'; return Valid();" /></td>
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
        <asp:UpdatePanel ID="upFarmerDtls" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="grd_FarmerList" runat="server" BackColor="#B2C629"
                    AutoGenerateColumns="False" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                    CellPadding="4" Width="90%" ForeColor="Black" >
                    <RowStyle BackColor="White" CssClass="tbl" />
                    <Columns>

                        <asp:TemplateField HeaderText="Sl No." HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate><%#Container.DataItemIndex+1 %> </ItemTemplate>
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
                        <asp:TemplateField HeaderText="Father's/Husband's Name" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lbl_VCHFATHERNAME" runat="server" Text='<%#Eval("VCHFATHERNAME")%>'></asp:Label>
                            </ItemTemplate>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Voter Id" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>

                                <asp:HiddenField ID="hfVCard" runat="server" Value='<%#Eval("VCHVOTERIDCARDNO")%>' />
                                <asp:LinkButton ID="lnkVCard" runat="server" Text='<%#Eval("VCHVOTERIDCARDNOV")%>' CausesValidation="false" OnClientClick="aspnetForm.target ='_blank';" OnClick="lnkVCard_Click"></asp:LinkButton>
                            </ItemTemplate>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Category_Value" runat="server" Text='<%#Eval("Category_Value")%>'></asp:Label>
                            </ItemTemplate>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Bank" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Bank_Name" runat="server" Text='<%#Eval("Bank_Name")%>'></asp:Label>
                            </ItemTemplate>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Account No." HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Acc_No" runat="server" Text='<%#Eval("VCHACCOUNTNO")%>'></asp:Label>
                            </ItemTemplate>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Acc.Holder Name" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lbl_AccHolder_Name" runat="server" Text='<%#Eval("VCHACCHOLDERNM")%>'></asp:Label>
                            </ItemTemplate>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Mobile" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lbl_VCHMOBILENO" runat="server" Text='<%#Eval("VCHMOBILENOV")%>'></asp:Label>
                            </ItemTemplate>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:TemplateField>

<%--                        <asp:TemplateField HeaderText="AadharNo." HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lbl_VCHAADHARNO" runat="server" Text='<%#Eval("VCHAADHARNO")%>'></asp:Label>
                            </ItemTemplate>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        </asp:TemplateField>--%>

                        <asp:TemplateField HeaderText="Family Details" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImgbtnShowDtls" runat="server" ImageUrl="~/images/binoculars.png" CommandName="FamilyDtls" CommandArgument='<%#Eval("VCHFARMERCODE")%>' CausesValidation="False" ClientIDMode="Static" OnClick="ImgbtnShowDtls_Click"/>
                                <asp:HiddenField ID="hfNICFARMERID" runat="server" Value='<%#Eval("NICFARMERID")%>' />
                                <asp:HiddenField ID="hfFARMERCODE" runat="server" Value='<%#Eval("VCHFARMERCODE")%>' />
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

                <table width="100%">
                    <tr>
                        <td>
                            <asp:Button ID="btnEditPopUp" runat="server" Style="display: none" /> <%--onokscript="onOk()" oncancelscript="onCancel()" --%>
                            <cc1:modalpopupextender id="popupEdit" runat="server" targetcontrolid="btnEditPopUp"
                                popupcontrolid="pnlEdit" backgroundcssclass="modalBackground">
                        </cc1:modalpopupextender>
                            <asp:Panel ID="pnlEdit" runat="server" CssClass="modalPopup" Style="display: block">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvFarmerFamilyDtls" runat="server" AutoGenerateColumns="False" CellPadding="4" CellSpacing="4" ForeColor="#333333" GridLines="None">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                             <asp:Label ID="lblText" Text="Sl.No." runat="server" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="VCHNAME" HeaderText="Name" />
                                                    <asp:BoundField DataField="VCHORIYAREL" HeaderText="Relation" />
                                                    <asp:BoundField DataField="INTAGE" HeaderText="Age" />
                                                </Columns>
                                                <EditRowStyle BackColor="#7C6F57" />
                                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#E3EAEB" />
                                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                                <SortedAscendingHeaderStyle BackColor="#246B61" />
                                                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                                <SortedDescendingHeaderStyle BackColor="#15524A" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                             <asp:Button ID="btnClose" runat="server" Text="Close" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>

                </table>
                <asp:Literal ID="litReport" runat="server"></asp:Literal>
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
     <asp:UpdateProgress runat="server" ID="Uprogress" AssociatedUpdatePanelID="upFarmerDtls" DisplayAfter="0">
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
    </asp:UpdateProgress>
</asp:Content>

