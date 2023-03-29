using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BLL_DropDown
/// </summary>
public class BLL_DropDown
{
    private string _DistCode;
    private string _UserType;
    private string _Godown_ID;
    private string _CROPCATG_ID;
    private string _CROP_ID;
    private string _LOT_NO;
    private string _Crop_Verid;
    private string _CROPVARIETY_NAME;
    private string _CropClass;
    private string _Receive_Unitcd;
    private string _Bag_Size_In_kg;
    private string _Recv_No_Of_Bags;
    private string _OB_date;
    private string _OB_Quantity;
    private string _UserID;
    private string _UserIP;
    private string _AgenciesID;
    private string _Value1;
    private string _Value2;
    private string _SaleTo;
    private string _Sale_Date;
    private string _Challan_No;
    private string _CashMemo_No;
    private string _Outagency_Waybillnum_Farm;
    private DateTime _Receive_Date;    
    private string _Receive_Qty;
    private string _mGodown_ID;
    private string _LIC_NO;
    private string _XML_Value;
    private string _SUPPLY_TYPE;
    private string _CREDIT_BILL_NO;
    private string _mDATE;
    private string _DEPT_TYPE;
    private string _SALE_DATE;
    private string _SALE_TO;
    private string _DD_NUMBER;
    private decimal _AMOUNT;
    private char _CONFIRM_STATUS;
    private string _UPDATED_BY;
    private string _SESSION;
    private string _REF_NO;
    private string _NAME;
    private string _FIN_YR;
    private string _MOU_REFNO;
    private string _FROM_DATE;
    private string _TO_DATE;
    private string _STATUS;
    private string _SEASON;
    private string _AGENCYSECTER;
    private string _CURGODOWNID;
    private bool _ISPACS;
    private string _MOBNO;
    private string _MOBSTATUS;
    private string _TRANSACTIONID;
    private string _FARMERID;
    private string _SALETRANSID;
    private string _RECVTRANSID;    
    private string _TESTING_DATE;
    private string _EXPIRY_DATE;

	public BLL_DropDown()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string DistCode
    {
        get
        {
            return _DistCode;
        }
        set
        {
            _DistCode = value;
        }
    }
    public string UserType
    {
        get
        {
            return _UserType;
        }
        set
        {
            _UserType = value;
        }
    }
    public string Godown_ID
    {
        get
        {
            return _Godown_ID;
        }
        set
        {
            _Godown_ID = value;
        }
    }
    public string CROPCATG_ID
    {
        get
        {
            return _CROPCATG_ID;
        }
        set
        {
            _CROPCATG_ID = value;
        }
    }
    public string CROP_ID
    {
        get
        {
            return _CROP_ID;
        }
        set
        {
            _CROP_ID = value;
        }
    }    
    public string LOT_NO
    {
        get
        {
            return _LOT_NO;
        }
        set
        {
            _LOT_NO = value;
        }
    }
    public string Crop_Verid
    {
        get
        {
            return _Crop_Verid;
        }
        set
        {
            _Crop_Verid = value;
        }
    }
    public string CROPVARIETY_NAME
    {
        get
        {
            return _CROPVARIETY_NAME;
        }
        set
        {
            _CROPVARIETY_NAME = value;
        }
    }
    public string CropClass
    {
        get
        {
            return _CropClass;
        }
        set
        {
            _CropClass = value;
        }
    }
    public string Receive_Unitcd
    {
        get
        {
            return _Receive_Unitcd;
        }
        set
        {
            _Receive_Unitcd = value;
        }
    }
    public string Bag_Size_In_kg
    {
        get
        {
            return _Bag_Size_In_kg;
        }
        set
        {
            _Bag_Size_In_kg = value;
        }
    }
    public string Recv_No_Of_Bags
    {
        get
        {
            return _Recv_No_Of_Bags;
        }
        set
        {
            _Recv_No_Of_Bags = value;
        }
    }
    public string OB_date
    {
        get
        {
            return _OB_date;
        }
        set
        {
            _OB_date = value;
        }
    }
    public string OB_Quantity
    {
        get
        {
            return _OB_Quantity;
        }
        set
        {
            _OB_Quantity = value;
        }
    }
    public string UserID
    {
        get
        {
            return _UserID;
        }
        set
        {
            _UserID = value;
        }
    }
    public string UserIP
    {
        get
        {
            return _UserIP;
        }
        set
        {
            _UserIP = value;
        }
    }
    public string AgenciesID
    {
        get
        {
            return _AgenciesID;
        }
        set
        {
            _AgenciesID = value;
        }
    }
    public string Value1
    {
        get
        {
            return _Value1;
        }
        set
        {
            _Value1 = value;
        }
    }
    public string Value2
    {
        get
        {
            return _Value2;
        }
        set
        {
            _Value2 = value;
        }
    }
    public string SaleTo
    {
        get
        {
            return _SaleTo;
        }
        set
        {
            _SaleTo = value;
        }
    }
    public string Sale_Date
    {
        get
        {
            return _Sale_Date;
        }
        set
        {
            _Sale_Date = value;
        }
    }
    public string Challan_No
    {
        get
        {
            return _Challan_No;
        }
        set
        {
            _Challan_No = value;
        }
    }
    public string CashMemo_No
    {
        get
        {
            return _CashMemo_No;
        }
        set
        {
            _CashMemo_No = value;
        }
    }
    public string Outagency_Waybillnum_Farm
    {
        get
        {
            return _Outagency_Waybillnum_Farm;
        }
        set
        {
            _Outagency_Waybillnum_Farm = value;
        }
    }
    public DateTime Receive_Date
    {
        get
        {
            return _Receive_Date;
        }
        set
        {
            _Receive_Date = value;
        }
    }
    public string mReceive_Date { get; set; }
    public string Receive_Qty
    {
        get
        {
            return _Receive_Qty;
        }
        set
        {
            _Receive_Qty = value;
        }
    }
    public string mGodown_ID
    {
        get
        {
            return _mGodown_ID;
        }
        set
        {
            _mGodown_ID = value;
        }
    }
    public string LIC_NO
    {
        get
        {
            return _LIC_NO;
        }
        set
        {
            _LIC_NO = value;
        }
    }
    public string XML_Value
    {
        get
        {
            return _XML_Value;
        }
        set
        {
            _XML_Value = value;
        }
    }
    public string SUPPLY_TYPE
    {
        get
        {
            return _SUPPLY_TYPE;
        }
        set
        {
            _SUPPLY_TYPE = value;
        }
    }
    public string CREDIT_BILL_NO
    {
        get
        {
            return _CREDIT_BILL_NO;
        }
        set
        {
            _CREDIT_BILL_NO = value;
        }
    }
    public string mDATE
    {
        get
        {
            return _mDATE;
        }
        set
        {
            _mDATE = value;
        }
    }
    public string DEPT_TYPE
    {
        get
        {
            return _DEPT_TYPE;
        }
        set
        {
            _DEPT_TYPE = value;
        }
    }
    public string SALE_DATE
    {
        get
        {
            return _SALE_DATE;
        }
        set
        {
            _SALE_DATE = value;
        }
    }
    public string SALE_TO
    {
        get
        {
            return _SALE_TO;
        }
        set
        {
            _SALE_TO = value;
        }
    }
    public string DD_NUMBER
    {
        get
        {
            return _DD_NUMBER;
        }
        set
        {
            _DD_NUMBER = value;
        }
    }
    public decimal AMOUNT
    {
        get
        {
            return _AMOUNT;
        }
        set
        {
            _AMOUNT = value;
        }
    }
    public char CONFIRM_STATUS
    {
        get
        {
            return _CONFIRM_STATUS;
        }
        set
        {
            _CONFIRM_STATUS = value;
        }
    }
    public string UPDATED_BY
    {
        get
        {
            return _UPDATED_BY;
        }
        set
        {
            _UPDATED_BY = value;
        }
    }
    public string SESSION
    {
        get
        {
            return _SESSION;
        }
        set
        {
            _SESSION = value;
        }
    }
    public string REF_NO
    {
        get
        {
            return _REF_NO;
        }
        set
        {
            _REF_NO = value;
        }
    }
    public string NAME
    {
        get
        {
            return _NAME;
        }
        set
        {
            _NAME = value;
        }
    }
    public string FIN_YR
    {
        get
        {
            return _FIN_YR;
        }
        set
        {
            _FIN_YR = value;
        }
    }
    public string MOU_REFNO
    {
        get
        {
            return _MOU_REFNO;
        }
        set
        {
            _MOU_REFNO = value;
        }
    }
    public string FROM_DATE
    {
        get
        {
            return _FROM_DATE;
        }
        set
        {
            _FROM_DATE = value;
        }
    }
    public string TO_DATE
    {
        get
        {
            return _TO_DATE;
        }
        set
        {
            _TO_DATE = value;
        }
    }
    public string STATUS
    {
        get
        {
            return _STATUS;
        }
        set
        {
            _STATUS = value;
        }
    }
    public string SEASON
    {
        get
        {
            return _SEASON;
        }
        set
        {
            _SEASON = value;
        }
    }
    public string AGENCYSECTER
    {
        get
        {
            return _AGENCYSECTER;
        }
        set
        {
            _AGENCYSECTER = value;
        }
    }
    public string CURGODOWNID
    {
        get
        {
            return _CURGODOWNID;
        }
        set
        {
            _CURGODOWNID = value;
        }
    }
    public bool ISPACS
    {
        get
        {
            return _ISPACS;
        }
        set
        {
            _ISPACS = value;
        }
    }
    public string MOBNO
    {
        get
        {
            return _MOBNO;
        }
        set
        {
            _MOBNO = value;
        }
    }
    public string MOBSTATUS
    {
        get
        {
            return _MOBSTATUS;
        }
        set
        {
            _MOBSTATUS = value;
        }
    }

    public string TRANSACTIONID
    {
        get
        {
            return _TRANSACTIONID;
        }
        set
        {
            _TRANSACTIONID = value;
        }
    }
    public string FARMERID
    {
        get
        {
            return _FARMERID;
        }
        set
        {
            _FARMERID = value;
        }
    }
    public string SALETRANSID
    {
        get
        {
            return _SALETRANSID;
        }
        set
        {
            _SALETRANSID = value;
        }
    }
    public string RECVTRANSID
    {
        get
        {
            return _RECVTRANSID;
        }
        set
        {
            _RECVTRANSID= value;
        }
    }

    public string TESTING_DATE
    {
        get
        {
            return _TESTING_DATE;
        }
        set
        {
            _TESTING_DATE = value;
        }
    }
    public string EXPIRY_DATE
    {
        get
        {
            return _EXPIRY_DATE;
        }
        set
        {
            _EXPIRY_DATE = value;
        }
    }


    public string UID { get; set; }
    public string URL { get; set; }
    public string DEVICETYPE { get; set; }
    public string MODEL { get; set; }
    public string MANUFACTURE { get; set; }
    public string OS { get; set; }
    public string BWSER { get; set; }
    public string SCREENSIZE { get; set; }
    public string ABOVE10YEAR { get; set; }
    public string RECEIVE_UNITCD { get; set; }
    public decimal loadingcharges { get; set; }
    public decimal Transportation { get; set; }
    public decimal Dealercommsiosn { get; set; }
    public decimal Pacscommsiosn { get; set; }
    public decimal allincostprice { get; set; }
    public decimal goisubsidy { get; set; }
    public decimal spsubsidy { get; set; }
    public string PACSRebate { get; set; }
    public string LICENCENO { get; set; }
    public string AADHAAR_NO { get; set; }
    public string ACC_HOLDERNAME { get; set; }
    public string ACC_NO { get; set; }
    public string BANK_ID { get; set; }
    public string BRANCH_ID { get; set; }
    public string IFSC_CODE { get; set; }
    public string CPAID { get; set; }
    public string scheme_code { get; set; }
    public string Month { get; set; }

    public string UserPass { get; set; }
    public int TypeId { get; set; }
    public int slno { get; set; }
    public int ApprovaTypeID { get; set; }
    public int AGE { get; set; }
    public string SOURCE { get; set; }
    public string LICENCENO1 { get; set; }
    public DateTime APR_UPTO { get; set; }

}