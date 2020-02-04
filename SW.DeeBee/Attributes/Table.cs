using System;

namespace SW.DeeBee
{
    [AttributeUsage(AttributeTargets.Class)]
    public class Table : Attribute
    {
        public Table(string tableName, string identityColumn = "Id", bool serverSideIdentity = true)
        {
            TableName = tableName;
            IdentityColumn = identityColumn;
            ServerSideIdentity = serverSideIdentity;
        }

        public string TableName { get; set; }
        public string IdentityColumn { get; set; }
        public bool ServerSideIdentity { get; set; }



    }
}





