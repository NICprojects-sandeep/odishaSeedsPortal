﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class FARMERDBEntities : DbContext
    {
        public FARMERDBEntities()
            : base("name=FARMERDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<M_FARMER_REGISTRATION> M_FARMER_REGISTRATION { get; set; }
        public virtual DbSet<Request_Payment_tblHeader> Request_Payment_tblHeader { get; set; }
        public virtual DbSet<Request_Payment_tblTransaction> Request_Payment_tblTransaction { get; set; }
        public virtual DbSet<Request_tbl_Beneficiary_List> Request_tbl_Beneficiary_List { get; set; }
        public virtual DbSet<Request_tbl_Beneficiary_Message> Request_tbl_Beneficiary_Message { get; set; }
        public virtual DbSet<Request_tbl_Payment_List> Request_tbl_Payment_List { get; set; }
        public virtual DbSet<Request_tbl_Payment_Message> Request_tbl_Payment_Message { get; set; }
        public virtual DbSet<Response_Payment_tblHeader> Response_Payment_tblHeader { get; set; }
        public virtual DbSet<Response_Payment_tblTransaction> Response_Payment_tblTransaction { get; set; }
        public virtual DbSet<Response_tbl_Beneficiary_Ack_Message> Response_tbl_Beneficiary_Ack_Message { get; set; }
        public virtual DbSet<Response_tbl_Message_Ack_List> Response_tbl_Message_Ack_List { get; set; }
        public virtual DbSet<Response_tbl_Paymemt_Ack_Message> Response_tbl_Paymemt_Ack_Message { get; set; }
        public virtual DbSet<Response_tbl_Payment_Ack_List> Response_tbl_Payment_Ack_List { get; set; }
        public virtual DbSet<Response_tbl_Payment_Authorization_List> Response_tbl_Payment_Authorization_List { get; set; }
        public virtual DbSet<Response_tbl_Payment_Authorization_Message> Response_tbl_Payment_Authorization_Message { get; set; }
        public virtual DbSet<Tbl_FarmerApprove> Tbl_FarmerApprove { get; set; }
        public virtual DbSet<Tbl_Dist> Tbl_Dist { get; set; }
        public virtual DbSet<Response_tbl_Beneficiary_Ack_Message_D2> Response_tbl_Beneficiary_Ack_Message_D2 { get; set; }
        public virtual DbSet<Response_tbl_Paymemt_Ack_Message_D> Response_tbl_Paymemt_Ack_Message_D { get; set; }
        public virtual DbSet<Response_tbl_Beneficiary_Ack_Message_D> Response_tbl_Beneficiary_Ack_Message_D { get; set; }
        public virtual DbSet<Tbl_Bank> Tbl_Bank { get; set; }
        public virtual DbSet<Tbl_Branch> Tbl_Branch { get; set; }
        public virtual DbSet<block> blocks { get; set; }
        public virtual DbSet<Village> Villages { get; set; }
        public virtual DbSet<Tbl_GP> Tbl_GP { get; set; }
        public virtual DbSet<Response_CCB1> Response_CCB1 { get; set; }
        public virtual DbSet<PDS_BLOCKMASTER> PDS_BLOCKMASTER { get; set; }
        public virtual DbSet<NIC_farmer_updated_by_aao> NIC_farmer_updated_by_aao { get; set; }
        public virtual DbSet<View_BenificiaryAccpResponse> View_BenificiaryAccpResponse { get; set; }
    
        public virtual ObjectResult<REPORT_STATUSDETAILS_Result> REPORT_STATUSDETAILS(string fID)
        {
            var fIDParameter = fID != null ?
                new ObjectParameter("FID", fID) :
                new ObjectParameter("FID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<REPORT_STATUSDETAILS_Result>("REPORT_STATUSDETAILS", fIDParameter);
        }
    }
}
