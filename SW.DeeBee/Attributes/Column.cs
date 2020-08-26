using System;

namespace SW.DeeBee 
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Column : Attribute
    {
        public Column(string columnName)
        {
            ColumnName  = columnName;
        }

        public string ColumnName { get; set; }

        public string ColumnNameEscaped(string provider) => provider ==DbConnectionExtensions.MYSQL ? @$"`{ColumnName}`" :  @$"""{ColumnName}""";
    }
}



