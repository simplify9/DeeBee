using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace SW.DeeBee
{
    public class DeeBeeOptions
    {
        public DeeBeeOptions()
        {
            IsolationLevel = IsolationLevel.ReadCommitted;
            CommandTimeout = 30;
        }

        //public Func<DbConnection> ConnectionFactory { get; set; }
        public Type Provider { get; set; }
        public string ConntectionString { get; set; }
        public bool Trasnaction { get; set; }
        public IsolationLevel IsolationLevel { get; set; }
        public int CommandTimeout { get; set; }
    }
}
