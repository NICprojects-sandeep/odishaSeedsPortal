<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="OssopcaLotApproval.aspx.cs" Inherits="Reports_OssopcaLotApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="../bootstrap/css/bootstrap-grid.css" rel="stylesheet" />
    <link href="../bootstrap/css/bootstrap-grid.min.css" rel="stylesheet" />

    <asp:Panel ID="pnl" runat="server" Visible="false">
        <div class="row">
            <div class="col-md-2">Year</div>
            <div class="col-md-2">
                <asp:DropDownList ID="ddlYear" class="form-control" runat="server"></asp:DropDownList>
            </div>
            <div class="col-md-2">Seassion</div>
            <div class="col-md-2">
                <asp:DropDownList ID="ddlSeassion" class="form-control" runat="server">
                    <asp:ListItem Value="0">--All--</asp:ListItem>
                    <asp:ListItem Value="R">Rabi</asp:ListItem>
                    <asp:ListItem Value="K">Kharif</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-2"></div>
        </div>
        <div class="row">
            <div class="col-md-2">Lot Number</div>
            <div class="col-md-2">
                <asp:TextBox ID="txtLotNo" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-2">Qty</div>
            <div class="col-md-2">
                <asp:TextBox ID="txtQty" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-2"></div>
        </div>
        <div class="row">
            <div class="col-md-2">Source</div>
            <div class="col-md-2">
                <asp:TextBox ID="txtSource" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-2">Reason</div>
            <div class="col-md-2">
                <asp:TextBox ID="txtReason" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnAdd" runat="server" Text="Add" class="btn btn-success" OnClick="btnAdd_Click" />
            </div>
        </div>
    </asp:Panel>
    
    <br />
    <asp:GridView ID="gvLots" runat="server" BackColor="#B2C629"
        CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#999999"
        AllowPaging="True" AutoGenerateColumns="False"
        ForeColor="Black" OnPageIndexChanging="gvLots_PageIndexChanging" OnRowDataBound="gvLots_RowDataBound">
        <RowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Sl No.">
                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                <ItemStyle HorizontalAlign="Left" Width="5%" />
                <ItemTemplate>
                    <asp:Label ID="lblSl" runat="server" Text='<%# Container.DataItemIndex + 1 %> '></asp:Label>
                    <asp:HiddenField ID="hfSlno" runat="server" Value='<%#Eval("SLNO")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="CROP">
                <ItemTemplate>
                    <asp:Label ID="lbl_Cropnm" runat="server" Text='<%#Eval("Crop_Name") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="30%" />
                <ItemStyle HorizontalAlign="Left" Width="30%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="VARIETY">
                <ItemTemplate>
                    <asp:Label ID="lbl_Varietynm" runat="server" Text='<%#Eval("Variety_Name") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="30%" />
                <ItemStyle HorizontalAlign="Left" Width="30%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="LOT NUMBER">
                <ItemTemplate>
                    <asp:Label ID="lbl_Lotnm" runat="server" Text='<%#Eval("LOT_NO") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="30%" />
                <ItemStyle HorizontalAlign="Left" Width="30%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="QTY">
                <ItemTemplate>
                    <asp:Label ID="lbl_Qtynm" runat="server" Text='<%#Eval("QTY") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="30%" />
                <ItemStyle HorizontalAlign="Left" Width="30%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="SOURCE">
                <ItemTemplate>
                    <asp:Label ID="lbl_Sourcenm" runat="server" Text='<%#Eval("SOURCE") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="30%" />
                <ItemStyle HorizontalAlign="Left" Width="30%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="STATUS">
                <ItemTemplate>
                    <asp:Label ID="lbl_Ossopca" runat="server" Text='<%#Eval("IS_OSSOPCA") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="30%" />
                <ItemStyle HorizontalAlign="Left" Width="30%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ACTION">
                <ItemTemplate>                    
                    <asp:DropDownList ID="ddl_Action" runat="server" Width="200px" CssClass="DropdownCss" 
                        OnSelectedIndexChanged="ddl_Action_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:HiddenField ID="hfTestingdate" runat="server" />
                    <asp:HiddenField ID="hfExpiredate" runat="server" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                <ItemStyle HorizontalAlign="Center" Width="15%" />
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <div class="mandiatory" style="width: 850px; text-align: center;">
                No Record
            </div>
        </EmptyDataTemplate>
        <FooterStyle BackColor="#B2C629"></FooterStyle>
        <PagerStyle HorizontalAlign="Left" CssClass="pagination" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="#B2C629"></HeaderStyle>
    </asp:GridView>
    <%--<asp:Literal ID="litReport" runat="server"></asp:Literal>--%>
    
</asp:Content>

