using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Model;

    public class PaymentStatusBal
    {
        //Retrives all the Inactive Request list
        BASEntities orm = new BASEntities();
        public List<Response_Payment_Authorization_List> getPaymentStatus(string UmID)
        {
            var StatusUM = UmID.Replace("REQ","STS");
            orm.Configuration.ProxyCreationEnabled = false;
            return orm.Response_Payment_Authorization_List.Where(s => s.Unique_Message_Id.Contains(StatusUM+"_")).ToList();
        }

        public List<Response_Payment_Authorization_Message> getAllResponseList()
        {
            var k = orm.Response_Payment_Authorization_Message.ToList();
            return k;
        }

        public List<Response_Payment_Authorization_Message> getPaymentStatusDetails(string UmID)
        {
            var StatusUM = UmID.Replace("REQ", "STS");
            orm.Configuration.ProxyCreationEnabled = false;
            return orm.Response_Payment_Authorization_Message.Where(s => s.Unique_Message_Id.Contains(StatusUM+"_")).ToList();
        }

        public List<Response_Payment_Authorization_Message> getAllResponseETE(string ete)
        {
            var k = orm.Response_Payment_Authorization_Message.Where(s=>s.Unique_Credit_Transaction_Id == ete).ToList();
            return k;
        }
    }
