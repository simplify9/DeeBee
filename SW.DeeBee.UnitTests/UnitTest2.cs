using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using SW.DeeBee.UnitTests.Entities;
using SW.PrimitiveTypes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SW.DeeBee.UnitTests
{
    
    namespace SW.DeeBee.UnitTests
    {
        [TestClass]
        public class UnitTest3
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
                                Field = "HAWB",
                                Rule = SearchyRule.Contains,
                                ValueString = "2020"
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


                using (var connection = new MySqlConnection(ConnectionString.Value))
                {
                    await connection.OpenAsync();
                    //await connection.Add(bag);

                    //bag.Description = "ttt";

                    //await connection.Update(bag);
                    var bags = await connection.Count<LegacyParcel>(req.Conditions);
                    //  var bag1 = await connection.One<LegacyParcel>(20);

                    //var condition = new SearchyCondition();

                    //condition.Filters.Add(new SearchyFilter("Id", SearchyRule.EqualsTo, 1));
                    //condition.Filters.Add(new SearchyFilter("Description", SearchyRule.StartsWith, "f"));


                    //var selectedBags = await connection.All<Bag>(condition);
                }
            }
        }
    }
}