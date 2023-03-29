<%@ Page Language="C#" MasterPageFile="~/MasterPages/SeedAdmin.master" AutoEventWireup="true" CodeFile="Sms.aspx.cs" Inherits="FMNEW_Sms" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

   <table cellpadding="0" cellspacing="5" border="5" width="100%" id="tblSuper" runat="Server">
        <tr>
            <td width="49%"></td>
            <td width="1%"></td>
            <td width="50%"></td>
        </tr>
        <tr>
            <td colspan="3">
                <table align="center" width="80%" cellpadding="0" cellspacing="0" border="0"> 
                    <tr>
                        <td width="49%"></td>
                       <td width="1%"></td>
                       <td width="50%"></td>
                    </tr>
                     <tr>
                <td colspan="3" align="Center"><br />
                    <strong><span style="text-decoration: underline; color: #000099">SEND SMS TO SELECTED FARMER</span> </strong>
                   
                </td>
            </tr>
                   
                  
                    <tr>
                        <td colspan="3" width="100%" runat="server" id="tdMfcDetails">
                            <table align="center" width="100%" cellpadding="0" cellspacing="2" border="0" id="tblMst" runat="Server">
                               
            <tr>
                        <td width="39%"></td>
                       <td width="1%"></td>
                       <td width="60%"></td>
                    </tr>
          
           
            <tr>
                <td></td>
                <td></td>
                <td></td>
            </tr>
           
            <tr>
                <td><span class="auto-style2" style="color: #800000"><strong>SEND SMS</strong></span><br /><img src="../images/ul.jpg" /></td>
                <td></td>
                <td></td>
               
            </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
            
              <tr>
                <td align="right"><strong>Mobile Number&nbsp; </strong></td>
                <td><strong>:</strong></td>
                <td id="Td1" runat="server" >
                    <strong><span style="font-size: 12pt">(+91)&nbsp; </span></strong><asp:TextBox ID="txtMob" runat="Server" BackColor="#FFCC99" MaxLength="10" ToolTip="Mobile No"></asp:TextBox></td>
            </tr>
            
               <tr runat="server" id="trtxtRej">
                <td align="right"><strong>Message </strong> &nbsp;</td>
                <td><strong>:</strong></td>
                <td align="left">
                   &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;
                    <asp:TextBox ID="txtMSG" runat="server" BackColor="#FFCC99" TextMode="MultiLine" Width="213px" ToolTip="Type Message"></asp:TextBox>
                 </td>
            </tr>
          
           
            <tr>
                <td></td>
                <td></td>
                <td></td>
            </tr>
             <tr>
                <td colspan="3" align="center" style="height: 12px">
                    &nbsp;</td>
            </tr>
             <tr>
                <td style="height: 20px"></td>
                <td style="height: 20px"></td>
                <td style="height: 20px"></td>
            </tr>
           
          
           
             <tr>
                <td></td>
                <td></td>
                <td>
                    <asp:Button ID="btnApprove" runat="server" BackColor="White" BorderColor="#660033" style="font-weight: 700" Text="Submit" OnClick="btnApprove_Click" Font-Italic="True"  />
                    </td>
            </tr>
            
             <tr>
                <td><br /></td>
                <td></td>
                <td></td>
            </tr>
        </table>
                        </td>
                    </tr>
                      <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    
        <tr>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
        </tr>
    </table>


</asp:Content>

