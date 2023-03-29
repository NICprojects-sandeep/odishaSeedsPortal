<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Unauthorised.aspx.cs" Inherits="Unauthorised" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Unauthorised Page</title>
    <script lang="javascript" type="text/javascript" src="Scripts/CloseWindow.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-left: 450px; margin-top: 300px">
            <asp:HyperLink ID="lnkLogin" runat="server" Font-Bold="true" Font-Size="Large" ForeColor="green" Text="Not Authorised to View the Requested Page. Click here to Login" NavigateUrl="~/Login.aspx"></asp:HyperLink>
        </div>
    </form>
</body>
</html>
