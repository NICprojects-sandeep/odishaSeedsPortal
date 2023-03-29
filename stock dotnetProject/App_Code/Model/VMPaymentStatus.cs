using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VMPaymentStatus
/// </summary>
public class VMPaymentStatus
{
    public string transNo { get; set; }
    public List<VMPaymentStatusProp> lstPaymentStatus { get; set; }
}