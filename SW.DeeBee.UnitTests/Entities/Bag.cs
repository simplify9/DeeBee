using System;
using System.Collections.Generic;
using System.Text;

namespace SW.DeeBee.UnitTests.Entities
{
    [Table("Bags")]
    class Bag
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public string Entity { get; set; }
        public bool  Closed { get; set; }
        public DateTime? SampleDate { get; set; }
    }
}
