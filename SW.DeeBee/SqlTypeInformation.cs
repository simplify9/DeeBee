using System;
using System.Collections.Generic;
using System.Text;

namespace SW.DeeBee
{
    public class SqlTypeInformation
    {
        private string sqlType;
        public string SqlType { get => sqlType; set { sqlType = value; } }
        public bool IsUnique { get; set; }
    }
}
