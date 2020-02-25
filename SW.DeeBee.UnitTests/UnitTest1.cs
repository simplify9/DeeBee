using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using SW.DeeBee.UnitTests.Entities;
using SW.PrimitiveTypes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SW.DeeBee.UnitTests
{
    [TestClass]
    public class UnitTest1
    {




        [TestMethod]
        async public Task TestMethod1()
        {

            var req = new SearchyRequest()
            {
                Conditions = new List<SearchyCondition>() {
                    new SearchyCondition() {
                        Filters = new List<SearchyFilter>() {
                            new SearchyFilter() {
                                Field = "Number",
                                Rule = SearchyRule.Contains,
                                ValueString = "65456456"
                            }
                        }
                    }
                },
                PageIndex = 0,
                PageSize = 12,
                Sorts = new List<SearchySort>() {
                    new SearchySort() {
                        Field = "ID", Sort = SearchySortOrder.DEC
                    }
                }
            };

            var invalidReq = new SearchyRequest()
            {
                Conditions = new List<SearchyCondition>() {
                    new SearchyCondition() {
                        Filters = new List<SearchyFilter>() {
                            new SearchyFilter() {
                                Field = "NOTAVALIDFIELD",
                                Rule = SearchyRule.Contains,
                                ValueString = "65456456"
                            }
                        }
                    }
                },
                PageIndex = 0,
                PageSize = 12,
                Sorts = new List<SearchySort>() {
                    new SearchySort() {
                        Field = "ID", Sort = SearchySortOrder.DEC
                    }
                }
            };

            var bag = new Bag
            {
                Number = "65456456",
                Description = "test bag",
                Entity = "XYZ",
                SampleDate = DateTime.UtcNow
            };

            //using (var  connectionHost = new ConnectionHost(new DeeBeeOptions()
            //{
            //    Provider = typeof(MySqlConnection),
            //    ConntectionString = "Server=mysql-s9-do-user-6997732-0.db.ondigitalocean.com;Port=25060;Database=mailboxdb;User=doadmin;Password=pwpxz6xcmxxq9tlv;sslmode=none;"
            //    //ConnectionFactory = () => new MySqlConnection("Server=mysql-s9-do-user-6997732-0.db.ondigitalocean.com;Port=25060;Database=mailboxdb;User=doadmin;Password=pwpxz6xcmxxq9tlv;sslmode=none;")
            //}))
            //{

            //};


            using (var connection = new MySqlConnection("Server=mysql-s9-do-user-6997732-0.db.ondigitalocean.com;Port=25060;Database=dee_bee_tests;User=doadmin;Password=pwpxz6xcmxxq9tlv;sslmode=none;convert zero datetime=True;"))
            {
                await connection.OpenAsync();
                await connection.Add(bag);

                //bag.Description = "ttt";

                //await connection.Update(bag);
                var bags = await connection.All<Bag>(req.Conditions, req.Sorts, req.PageSize, req.PageIndex);
                //  var bag1 = await connection.One<LegacyParcel>(20);

                //var condition = new SearchyCondition();

                //condition.Filters.Add(new SearchyFilter("Id", SearchyRule.EqualsTo, 1));
                //condition.Filters.Add(new SearchyFilter("Description", SearchyRule.StartsWith, "f"));


                //var selectedBags = await connection.All<Bag>(condition);

                var exceptionCatched = false;
                try
                {
                    var data = await connection.All<Bag>(invalidReq.Conditions, invalidReq.Sorts, invalidReq.PageSize, invalidReq.PageIndex);
                }
                catch (DeeBeeColumnNameException )
                {
                    exceptionCatched = true;


                }

                Assert.IsTrue(exceptionCatched);

                
            }
        }
    }
}
