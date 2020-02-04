using System.Collections.Generic;

namespace SW.DeeBee
{
    public partial class StoredProcedureInfo
    {
        public string Name { get; set; }
        public IEnumerable<StoredProcedureParameter> Parameters { get; set; }

        public StoredProcedureInfo()
        {
        }

        public StoredProcedureInfo(string Name, IEnumerable<StoredProcedureParameter> Parameters)
        {
            this.Name = Name;
            this.Parameters = Parameters;
        }
    }
}



