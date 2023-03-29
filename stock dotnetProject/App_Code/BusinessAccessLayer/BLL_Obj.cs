using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
/// <summary>
/// Summary description for BLL_App
/// </summary>
public class BLL_Obj
{
    public BLL_Obj()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string dbmsg { get; set; }
    private string qry;
    private string param;
    private string paramobj;
    private string[] paramlist;
    private object[] paramobjlist;
    private string alies;
    private string mob; 
    private string msg;
    private string dname;
    private string imp;
    private int serialno;
    private string FinMob;
    private string smsmode;
    private string prno;
    private string Finalcialyear;
    public string id { get; set; }
    public string mnt { get; set; }
    public string yrr { get; set; }
    public string Query

    {
        get
        {
            return qry;
        }

        set
        {
            qry = value;
        }

    }
    public string SMSMode
    {
        get
        {
            return smsmode;
        }

        set
        {
            smsmode = value;
        }

    }
    public string Prno
    {
        get
        {
            return prno;
        }

        set
        {
            prno = value;
        }

    }
    public string Fyr
    {
        get
        {
            return Finalcialyear;
        }

        set
        {
            Finalcialyear = value;
        }

    }
    public string finMob
    {
        get
        {
            return FinMob;
        }

        set
        {
            FinMob = value;
        }

    }
    public string Imp
    { 
        get
        {
            return imp;
        
        }
        
        
        set
        {
            imp = value;

        }    
    }
    public int slno 
    {
        get
        {
            return serialno;

        }


        set
        {
            serialno = value;

        }
    }
    public string Dname
    {
        get
        {
            return dname;

        }


        set
        {
            dname = value;

        }
    }
     public string Param
    {

        get
        {           
                return param;
                      
        }
        set
        {
            param = value;
        }
    }

     public string Paramobj
     {

         get
         {
             return paramobj;

         }
         set
         {
             paramobj = value;
         }
     }
     public string[] ParamList
     {

         get
         {
             return paramlist;

         }
         set
         {
             paramlist = (string[])value;
         }
     }

     public object[] ParamobjList
     {

         get
         {
             return paramobjlist;

         }
         set
         {
             paramobjlist = (object[])value;
         }
     }
     public string Alies
     {
         get
         {
             return alies;
         }

         set
         {
             alies = value;
         }

     }
     public string Mob
     {

         get
         {
             return mob;

         }
         set
         {
             mob = value;
         }
     }
     public string Msg
     {

         get
         {
             return msg;

         }
         set
         {
             msg = value;
         }
     }
     public void BindDropdownList_NIC(DropDownList dropdownlistname, string datavaluefield, string datatextfield, dynamic DatasourceName)
     {

         try
         {

             dropdownlistname.DataSource = DatasourceName;
             dropdownlistname.DataTextField = datatextfield;
             dropdownlistname.DataValueField = datavaluefield;
             dropdownlistname.DataBind();
             dropdownlistname.Items.Insert(0, "--Select--");
         }
         catch
         {


         }
     }
}