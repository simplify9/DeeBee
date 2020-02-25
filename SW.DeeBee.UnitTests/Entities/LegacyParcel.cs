
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.DeeBee.UnitTests.Entities
{
    [Table("GLN_HAWB")]
    class LegacyParcel
    {
        public int ID { get; set; }
        public string HAWB { get; set; }
        public string OWNER { get; set; }
        public string Origin_Entity_ID { get; set; }
        public string CUS_Account { get; set; }
        public string CUS_Reference { get; set; }
        public string SND_Reference1 { get; set; }
        public string SND_Company { get; set; }
        public string SND_ContactName { get; set; }
        public string SND_Street { get; set; }
        public string SND_AddressLine2 { get; set; }
        public string SND_City { get; set; }
        public string SND_State { get; set; }
        public string SND_Zipcode { get; set; }
        public string SND_CountryID { get; set; }
        public string SND_Telephone1 { get; set; }
        public string SND_Telephone2 { get; set; }
        public string SND_Email { get; set; }
        public string RCV_Reference { get; set; }
        public string RCV_Company { get; set; }
        public string RCV_ContactName { get; set; }
        public string RCV_Street { get; set; }
        public string RCV_AddressLine2 { get; set; }
        public string RCV_City { get; set; }
        public string RCV_Zipcode { get; set; }
        public string RCV_State { get; set; }
        public string RCV_CountryID { get; set; }
        public string RCV_Telephone1 { get; set; }
        public string RCV_Telephone2 { get; set; }
        public string RCV_Email { get; set; }
        public string RCV_Remark { get; set; }
        public bool GOOD_Dutiable { get; set; }
        public string GOOD_Description { get; set; }
        public string GOOD_OriginCountryID { get; set; }
        public string GOOD_HarmonizedID { get; set; }
        public int Pieces { get; set; }
        public float Weight_Gross { get; set; }
        public float Weight_Volume { get; set; }
        public string Weight_Unit { get; set; }
        public int Dim_L { get; set; }
        public int Dim_W { get; set; }
        public int Dim_H { get; set; }
        public float Value { get; set; }
        public string Currency { get; set; }
        public int Weight_Factor { get; set; }
        public string Service_Type { get; set; }
        public string AgentCode { get; set; }
        public bool Service_PKP { get; set; }
        public bool Service_SAT { get; set; }
        public bool Service_IDCheck { get; set; }
        public bool Service_Insurance { get; set; }
        public float Service_COD { get; set; }
        public bool mailConsignee { get; set; }
        public string notifySMS { get; set; }
        public DateTime Pickup_Date { get; set; }
        public TimeSpan Pickup_Time { get; set; }
        public DateTime Alert_Date { get; set; }
        public string MAWB { get; set; }
        public int Manifest_EoD { get; set; }
        public string Manifest_Scan { get; set; }
        public string Manifest_Agent { get; set; }
        public string LASTSTATUS_Code { get; set; }
        public DateTime LASTSTATUS_Date { get; set; }
        public TimeSpan LASTSTATUS_Time { get; set; }
        public bool Final { get; set; }
        public DateTime FINAL_Entry_Date { get; set; }
        public DateTime FINAL_Action_Date { get; set; }
        public TimeSpan FINAL_Action_Time { get; set; }
        public string FINAL_TRK_Code { get; set; }
        public string DLVD_Signatory { get; set; }
        public string Agent { get; set; }
        public string CostCard { get; set; }
        public string SellingCard { get; set; }
        public bool CHARGED { get; set; }
        public bool COSTED { get; set; }
        public string PartnerINVOICE { get; set; }
        public string UserID { get; set; }
        public string Source { get; set; }
        public string BatchID { get; set; }
        public int BatchSequence { get; set; }
        public bool PRINTED { get; set; }
        public bool Void { get; set; }

    }
}
