
namespace SW.DeeBee
{
    public partial class StoredProcedureParameter
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public StoredProcedureParameterDirectionEnum Direction { get; set; }

        public StoredProcedureParameter()
        {
        }

        public StoredProcedureParameter(string Name, object Value, StoredProcedureParameterDirectionEnum Direction)
        {
            this.Name = Name;
            this.Value = Value;
            this.Direction = Direction;
        }
    }

    public enum StoredProcedureParameterDirectionEnum
    {
        In = 1,
        Out = 2
    }
}






