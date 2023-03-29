
    public class SchemeWisteDetailsList
    {
        private string schemeCode;

        public SchemeWisteDetailsList()
        {
        }

        public SchemeWisteDetailsList(string schemeCode)
        {
            this.schemeCode = schemeCode;
        }

        public string Accepted_Payment { get; internal set; }
        public string Accepted_Payment_Count { get; internal set; }
        public string Amount_To_Be_Paid { get; internal set; }
        public string Balance_Amount { get; internal set; }
        public string Opening_Balance { get; internal set; }
        public string Paid_By_Bank { get; internal set; }
        public string Paid_By_Bank_Count { get; internal set; }
        public string Requested_Payment { get; internal set; }
        public string Requested_Payment_Count { get; internal set; }
        public string Scheme_Code { get; internal set; }
        public string Scheme_Name { get; internal set; }
        public string Topup_Balance { get; internal set; }
        public string Total_Balance { get; internal set; }
    }
