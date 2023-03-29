using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VMStatus
/// </summary>
public class VMStatus
{
    public string farmerID { get; set; }
    public string transactionCode { get; set; }
    public string accountNo { get; set; }
    public string reqMsgID { get; set; }
    public string respMsgID { get; set; }
    public string reqSent { get; set; }
    public string responseRecvStatus { get; set; }
    public string bulkStatus { get; set; }
    public string transDate { get; set; }
    public string BankPaid { get; set; }

}