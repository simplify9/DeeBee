using System;
using System.Collections.Generic;
using System.Text;

namespace SW.DeeBee.UnitTests.Entities
{
    [Table("GLN_LOG", nameof(GlnLog.ID))]
    public class GlnLog
    {
        public int ID { get; set; }
        public string SERVICE { get; set; }
        public string Agent { get; set; }
        public string Folder { get; set; }
        public string File { get; set; }
        public DateTime Date { get; set; }
        public string User { get; set; }
        public int Records { get; set; }
        public int RECProcessed { get; set; }
        public int RECFailed { get; set; }

    }
}
