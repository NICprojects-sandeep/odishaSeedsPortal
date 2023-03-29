<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Dist.master" AutoEventWireup="true" CodeFile="Dda_DistwiseFarmerDeatils.aspx.cs" Inherits="Reports_Dda_DistwiseFarmerDeatils" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
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
        
    .modalBackground
    {
        background-color: Black;
        filter: alpha(opacity=90);
        opacity: 0.8;
    }
    .modalPopup
    {
        background-color: #FFFFFF;
        border-width: 3px;
        border-style: solid;
        border-color: black;
        padding-top: 10px;
        padding-left: 10px;
        width: 1503px;
        height: 400px;
    }
    </style>
      <script>
         function Valid() {

            var BlockName = document.getElementById("<%=drpBlock.ClientID %>");
             if (BlockName.value.trim() == "---SELECT---") {
                alert('Please Select a Block!');
                BlockName.focus();
                return false;
            }
          }
          function checkAll(objRef) {

              var GridView = objRef.parentNode.parentNode.parentNode;

              var inputList = GridView.getElementsByTagName("input");

              for (var i = 0; i < inputList.length; i++) {

                  var row = inputList[i].parentNode.parentNode;

                  if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                      if (objRef.checked) {

                          inputList[i].checked = true;
                      }

                      else {

                          if (row.rowIndex % 2 == 0) {

                              row.style.backgroundColor = "#C2D69B"
                           }

                          else {

                              row.style.backgroundColor = "white";
                           }

                          inputList[i].checked = false;

                      }

                  }

              }

          }
    </script>
    <center>  
        <fieldset class="tbl">
            <legend style="margin-left: 10px;" class="leg">Select any type</legend>

            <table style="width: 90%; padding: 15px;">
                <tr>
                    <td class="normaltd">Dist</td>
                    <td>:</td>
                     <td>
                        <asp:Literal ID="litDistName" runat="server"></asp:Literal>
                     </td>
                </tr>
                <tr>
                    <td class="normaltd">Block</td>
                    <td>:</td>
                    <td>
                        <asp:DropDownList ID="drpBlock" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="drpBlock_SelectedIndexChanged">
                        </asp:DropDownList></td>
                    <td class="normaltd">Gram Panchayat</td>
                    <td>:</td>
                    <td>
                        <asp:DropDownList ID="drpGp" runat="server" Width="150px" AutoPostBack="True"></asp:DropDownList></td>
                     <td rowspan="2">
                        <asp:Button ID="btnSearch" runat="server" Text="Search"  OnClientClick="return Valid();" OnClick="btnSearch_Click" />
                        <asp:Button ID="btnview" runat="server" Text="View" Visible="false" OnClick="btnview_Click"  OnClientClick="aspnetForm.target ='_self';"/>
                         <asp:Button ID="Button1" runat="server" Text="View" style="display:none;"/>
                    </td>
                </tr>
            </table>
        </fieldset>
        <!-- ModalPopupExtender -->
          <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="Button1" CancelControlID="btnClose" BackgroundCssClass="modalBackground">
          </cc1:ModalPopupExtender>
           <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" style = "display:none" align="center" ScrollBars="Horizontal" Width="985px" Height="700px" >
              <asp:Literal ID="litReport" runat="server"></asp:Literal>
               <br>
               </br>
               <asp:Button ID="btnexportexcel" runat="server" OnClick="btnexportexcel_Click" Text="Export to Excel" />
               <asp:Button ID="btnClose" runat="server" Text="Close" />
             
           </asp:Panel>

        <asp:GridView ID="grd_dda_FarmerList" runat="server" BackColor="#B2C629"
            AutoGenerateColumns="False" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
            CellPadding="4" Width="90%" ForeColor="Black" AllowPaging="false" PageSize="10" >
            <RowStyle BackColor="White" CssClass="tbl" />
         
            <Columns>
                    <asp:TemplateField>
                     <HeaderTemplate>
                        <asp:CheckBox ID="checkAll" runat="server" onclick = "checkAll(this);" />
                      </HeaderTemplate>
                     <ItemTemplate>
                     <asp:CheckBox id="CheckBox" runat="server" Checked='false' />
                    </ItemTemplate>
                     </asp:TemplateField>
                <asp:TemplateField HeaderText="Farmer Id" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbl_NICFARMERID" runat="server" Text='<%#Eval("NICFARMERID")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Village Name" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Village_Name" runat="server" Text='<%#Eval("vch_VillageName")%>'></asp:Label>
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
                <asp:TemplateField HeaderText="Aadhar No." HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbl_VCHAADHARNO" runat="server" Text='<%#Eval("VCHAADHARNO")%>'></asp:Label>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#B2C629" />
            <PagerStyle HorizontalAlign="Left" CssClass="pagination" />
            <EmptyDataTemplate>
                <asp:Label ID="Label1" runat="server" Text=""
                    Font-Bold="true" ForeColor="red"></asp:Label>
            </EmptyDataTemplate>
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#E6EBED" Font-Bold="True" ForeColor="black" />
        </asp:GridView>
    </center>
</asp:Content>

