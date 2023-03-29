using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VMPaymentStatusProp
/// </summary>
public class VMPaymentStatusProp
{
    public string uniqtransID { get; set; }
    public string transDate { get; set; }
    public string farmerName { get; set; }
    public int bagSize { get; set; }
    public int noOfBags { get; set; }
    public decimal totQTL { get; set; }
    public decimal amount { get; set; }
    public string status { get; set; }
    public string paidStatus { get; set; }
}