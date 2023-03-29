<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AAO.master" AutoEventWireup="true" CodeFile="Gender_casteupdation.aspx.cs" Inherits="Gender_casteupdation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <style>
       .tbl th {
font: bold 11px "Trebuchet MS", Verdana, Arial, Helvetica, sans-serif;
color: #4f6b72;
border-right: 1px solid #C1DAD7;
border-bottom: 1px solid #C1DAD7;
border-top: 1px solid #C1DAD7;
letter-spacing: 2px;
text-transform: uppercase;
text-align: left;
padding: 6px 6px 6px 6px;
background: #D5EDEF;
}
  
.tbl td {
border-right: 1px solid #C1DAD7;
border-bottom: 1px solid #C1DAD7;
background: #fff;
padding: 6px 6px 6px 6px;
color: #4f6b72;
}
  
.tbl td.alt
{
background: #F5FAFA;
color: #797268;
}
  
.tbl td.boldtd
{
font: bold 13px "Trebuchet MS", Verdana, Arial, Helvetica, sans-serif;
background: #D5EDEF;
color: #797268;
}
    </style>
    <div style="width: 100%" class="auto-style1">
                    <fieldset class="tbl">

            <legend style="margin-left: 10px;" class="leg">Select any type</legend>

            <table style="width: 90%; padding: 15px;">
                <tr>
                   
                        <td class="normaltd">Farmer ID</td>
                    <td>
                        <asp:TextBox ID="txtfarid" runat="server"  CssClass="txtbox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*Required" ControlToValidate="txtfarid"></asp:RequiredFieldValidator>
                    </td>
                    <td><asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" >
                            <asp:ListItem Value="0">Caste</asp:ListItem>
                            <asp:ListItem Value="1">Gender</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Required" ControlToValidate="RadioButtonList1" ForeColor="#FF6600"></asp:RequiredFieldValidator>
                        </td>
                    
                    <td>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="Verify" OnClick="btnSearch_Click" OnClientClick="aspnetForm.target ='_self';" /></td>
                </tr>
                
            </table>
        </fieldset>
    </div>
      <asp:GridView ID="grd_FarmerList" runat="server" BackColor="#B2C629"
                AutoGenerateColumns="False" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                CellPadding="4" Width="90%" ForeColor="Black" CssClass="tbl">
                <RowStyle BackColor="White"  />
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

                            <asp:Label ID="lbl_NICFARMER" runat="server" Text='<%#Eval("VCHFARMERNAME")%>'></asp:Label>

                        </ItemTemplate>

                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

                        <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Farmer ID" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lbl_NICFARMERID" runat="server" Text='<%#Eval("NICFARMERID")%>'></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Gender" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <a id="popup1" style='<%# Eval("GEN_ST").ToString() == "0"?"":"display:none" %>' href='EditGender.aspx?id=<%# Eval("VCHFARMERCODE") %>' >Change</a>
                            
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Caste" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <a id="popup2" style='<%# Eval("CATE_ST").ToString() == "0"?"":"display:none" %>' href='EditCaste.aspx?id=<%# Eval("VCHFARMERCODE") %>' >Change</a>
                        </ItemTemplate>

                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#B2C629" />
                <PagerStyle HorizontalAlign="Left" CssClass="pagination" />
                <EmptyDataTemplate>
                    <asp:Label ID="Label1" runat="server" Text="No Records/You already updated before !!!"
                        Font-Bold="true" ForeColor="red"></asp:Label>
                </EmptyDataTemplate>
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#E6EBED" Font-Bold="True" ForeColor="black" />
            </asp:GridView>
          

    <asp:Button ID="btnRefresh" Text="refresh" runat="server"
            onclick="btnRefresh_Click" Style="display:none;" ClientIDMode="Static" />
    <script type="text/javascript" src="http://ajax.microsoft.com/ajax/jquery.ui/1.8.6/jquery-ui.min.js"></script>
    <link type="text/css" rel="Stylesheet" href="http://ajax.microsoft.com/ajax/jquery.ui/1.8.6/themes/smoothness/jquery-ui.css">
    <script type="text/javascript">
        $(document).ready(function () {
            // $('a#popup1').live('click', function (e) {
                 
            //     var page = $(this).attr("href")  //get url of link
               
            //     var $dialog = $('<div></div>')
            //     .html('<iframe style="border: 0px; " src="' + page + '" width="100%" height="100%"></iframe>')
            //     .dialog({
            //         autoOpen: false,
            //         modal: true,
            //         height: 470,
            //         width: 670,
            //         title: "Edit Gender",
            //         buttons: {
            //             "Close": function () { $dialog.dialog('close'); }
            //                     },
            //         close: function (event, ui) {                         
            //            $('#btnRefresh').click();
            //         }
            //     });
            //     $dialog.dialog('open');
            //     e.preventDefault();
            // });
             function ShowGender()
             {
            var title = "Gender Update"; 
            $("#MyPopup .modal-title").html(title);
            $("#MyPopup").modal("show");
             }
            // $('a#popup2').live('click', function (e) {
            //     var page = $(this).attr("href")  //get url of link
            //     var $dialog = $('<div></div>')
            //     .html('<iframe style="border: 0px; " src="' + page + '" width="100%" height="100%"></iframe>')
            //     .dialog({
            //         autoOpen: false,
            //         modal: true,
            //         height: 470,
            //         width: 670,
            //         title: "Edit Caste",
            //         buttons: {
            //             "Close": function () { $dialog.dialog('close'); }
            //         },
            //         close: function (event, ui) {
            //            $('#btnRefresh').click();
            //         }
            //     });
            //     $dialog.dialog('open');
            //     e.preventDefault();
            // });
        });
    </script>
</asp:Content>

