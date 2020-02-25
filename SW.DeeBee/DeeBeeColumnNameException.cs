using System;

namespace SW.DeeBee
{
    public class DeeBeeColumnNameException: Exception
    {
        public DeeBeeColumnNameException(string columnName) : base($"invalid column name {columnName}")
        {

        }
    }
}
