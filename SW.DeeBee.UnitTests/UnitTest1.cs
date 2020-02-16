using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using SW.DeeBee.UnitTests.Entities;
using SW.PrimitiveTypes;
using System;
using System.Threading.Tasks;

namespace SW.DeeBee.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        async public Task TestMethod1()
        {
            //var bag = new Bag
            //{
            //    Number ="65456456",
            //    Description = "test bag",
            //    Entity = "XYZ",
            //    SampleDate = DateTime.UtcNow
            //};

            //using (var  connectionHost = new ConnectionHost(new DeeBeeOptions()
            //{
            //    Provider = typeof(MySqlConnection),
            //    ConntectionString = "Server=mysql-s9-do-user-6997732-0.db.ondigitalocean.com;Port=25060;Database=mailboxdb;User=doadmin;Password=pwpxz6xcmxxq9tlv;sslmode=none;"
            //    //ConnectionFactory = () => new MySqlConnection("Server=mysql-s9-do-user-6997732-0.db.ondigitalocean.com;Port=25060;Database=mailboxdb;User=doadmin;Password=pwpxz6xcmxxq9tlv;sslmode=none;")
            //}))
            //{

            //};


            using (var connection = new MySqlConnection("Server=mysql-s9-do-user-6997732-0.db.ondigitalocean.com;Port=25060;Database=mailboxdb;User=doadmin;Password=pwpxz6xcmxxq9tlv;sslmode=none;"))
            {
                await connection.OpenAsync();
                //await connection.Add(bag);

                //bag.Description = "ttt";

                //await connection.Update(bag);
                var bags = await connection.All<Bag>();
                var bag1 = await connection.One<Bag>(20);

                var condition = new SearchyCondition();

                condition.Filters.Add(new SearchyFilter("Id", SearchyRule.EqualsTo, 1));
                condition.Filters.Add(new SearchyFilter("Description", SearchyRule.StartsWith, "f"));


                var selectedBags = await connection.All<Bag>(condition);
            }
        }
    }
}
