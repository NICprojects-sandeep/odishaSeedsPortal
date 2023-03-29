using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Report : System.Web.UI.Page
{
    BASEntities orm = new BASEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        populateList();
    }

    public void populateList()
    {
        bal obj = new bal();
        var itemList = obj.getSystemwiseBankList();


        foreach (var item in itemList)
        {
            ClientScript.RegisterStartupScript(GetType(), item.System_ID, "$('#systemselect').append('<option value=" + item.System_ID + ">" + item.System_ID + "</option>');", true);
        }
    }

    public void loadLedger(object sender, EventArgs e) {
        DateTime frm = Convert.ToDateTime(from.Value.ToString());
        DateTime todt = Convert.ToDateTime(todate.Value.ToString());
        var sid = Request.Form["systemselect"].ToString();
        var res = (from c in orm.Bank_Code_AccountNo_V
                   where c.System_ID == sid &&
                   c.Date >= frm && c.Date <= todt
                   select c).AsEnumerable();
        var resre = (from d in orm.Request_PaymentList_Header where d.File_Status == "3" && d.Message_Date_Time >= frm && d.Message_Date_Time <= todt select d);
        foreach (var resl in res)
        {
            //list.Add(new CPViewModel { date = Convert.ToDateTime(resl.Date), crAmt = Convert.ToDecimal(resl.Topup_amount), drAmt = 0 });
            ClientScript.RegisterStartupScript(GetType(), resl.Date.ToString()+resl.sl_no, "$('#myList').append('<tr><td>" + Convert.ToDateTime(resl.Date) + "</td><td>" + Convert.ToDecimal(resl.Topup_amount) + "</td><td>0</td></tr>');", true);

        }
        foreach (var reslr in resre)
        {
            //list.Add(new CPViewModel { date = Convert.ToDateTime(reslr.Message_Date_Time), crAmt = 0, drAmt = Convert.ToDecimal(reslr.Control_Sum) });
            ClientScript.RegisterStartupScript(GetType(), reslr.Message_Date_Time.ToString()+reslr.Unique_Message_Id, "$('#myList').append('<tr><td>" + Convert.ToDateTime(reslr.Message_Date_Time) + "</td><td>0</td><td>"+ Convert.ToDecimal(reslr.Control_Sum) + "</td></tr>');", true);
        }
    }
}