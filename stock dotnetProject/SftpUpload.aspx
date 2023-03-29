<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SftpUpload.aspx.cs" Inherits="SftpUpload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="jumbotron">
            <h1>ASP.NET</h1>
            <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
            <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
        </div>
        <div>
            <table>
                <tr>
                    <td>URL</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="txt_url" runat="server"></asp:TextBox></td>
                    <td>User Name</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="txt_user" runat="server"></asp:TextBox></td>
                    <td>Password</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="txt_pass" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>To Folder</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="txt_to" runat="server"></asp:TextBox></td>
                    <td>From Folder</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="txt_from" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:Button ID="btn_show" runat="server" Text="Show" OnClick="btn_show_Click" /></td>

                    <td></td>
                    <td></td>
                </tr>
            </table>
        </div>
        <div class="row">
            <div class="col-md-4">
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="grid_sftp" runat="server">
                                <Columns>
                                </Columns>
                            </asp:GridView>
                        </td>
                        <td style="width: 32%"></td>
                        <td>
                            <asp:GridView ID="GridView1" runat="server">
                                <Columns>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
