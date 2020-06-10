

using Microsoft.Data.Sqlite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using SW.DeeBee.UnitTests.Entities;
using SW.PrimitiveTypes;
using System;
using System.Collections.Generic;
using System.Linq;
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
                Number = "1a",
                Description = "test bag 1",
                Entity = "XYZ",
                TS = DateTime.UtcNow,
            };
            //using (var connection = new MySqlConnection("Server=mysql-s9-do-user-6997732-0.db.ondigitalocean.com;Port=25060;Database=glnetclo;User=doadmin;Password=pwpxz6xcmxxq9tlv;sslmode=none;convert zero datetime=True;"))
            //{
            //    await connection.OpenAsync();
            //    var a = new Pq
            //    {
            //        Comment = "",
            //        HAWB = "1Z47Y19F6890057094",
            //        HAWB_ID = 274826,
            //        Update_Time = DateTime.Now,
            //        PQ_Code = "",
            //        UserID = ""
            //    };

            //    await connection.Add(a);
            //}
            using (var connection = new SqliteConnection("Data Source=./Data/TestDb.db"))
            {
                await connection.OpenAsync();
                await connection.Delete<Bag>("Bags");
                await connection.Add(bag);
                bag.Number = "2";
                await connection.Add(bag);
                bag.Number = "3";
                await connection.Add(bag);

                var bags = await connection.All<Bag>();

                Assert.AreEqual(3, bags.Count());

                var bagsEither1Or2 = await connection.All<Bag>(new List<SearchyCondition>()
                {
                    new SearchyCondition(nameof(Bag.Number), SearchyRule.EqualsTo, "1"),
                    new SearchyCondition(nameof(Bag.Number), SearchyRule.EqualsTo, "2")
                });
                Assert.AreEqual(2, bagsEither1Or2.Count());

                Assert.IsTrue(bagsEither1Or2.Where(x => x.Number == "3").FirstOrDefault() == null);


                bag.Description = "some update";

                await connection.Update(bag);


                var exceptionCatched = false;
                try
                {
                    var data = await connection.All<Bag>(invalidReq.Conditions, invalidReq.Sorts, invalidReq.PageSize, invalidReq.PageIndex);
                }
                catch (DeeBeeColumnNameException)
                {
                    exceptionCatched = true;


                }

                Assert.IsTrue(exceptionCatched);


            }
        }
    }
}












