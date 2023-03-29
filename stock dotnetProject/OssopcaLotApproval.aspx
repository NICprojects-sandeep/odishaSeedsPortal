<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OssopcaLotApproval.aspx.cs" Inherits="OssopcaLotApproval" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="bootstrap/css/bootstrap-grid.css" rel="stylesheet" />
    <link href="bootstrap/css/bootstrap-grid.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <br />
        The Following Lot numbers have been converted from Foundation to Certified.
    <asp:Literal ID="litReport" runat="server"></asp:Literal>    
    </div>
    </form>
</body>
</html>
