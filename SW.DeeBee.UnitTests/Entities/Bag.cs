using System;
using System.Collections.Generic;
using System.Text;

namespace SW.DeeBee.UnitTests.Entities
{
    [Table("GLN_PQ", null, false)]
    public class Pq
    {
        public int HAWB_ID { get; set; }
        public string HAWB { get; set; }
        public DateTime Update_Time { get; set; }
        public string PQ_Code { get; set; }
        public string Comment { get; set; }
        public string UserID { get; set; }

    }

    [Table("Bags")]
    class Bag
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public string Entity { get; set; }
        public bool  Closed { get; set; }
        public DateTime? TS { get; set; }
    }
}
