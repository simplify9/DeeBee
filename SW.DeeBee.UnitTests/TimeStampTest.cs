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

    namespace SW.DeeBee.UnitTests
    {
        [TestClass]
        public class TimeStampTest
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
                                Field = nameof(LegacyParcel.Alert_Date),
                                Rule = SearchyRule.GreaterThanOrEquals,
                                ValueDateTime = DateTime.Now
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

                    var request = new SearchyRequest() { PageIndex = 0, PageSize = 10 };

                    request.Conditions.Add(new SearchyCondition());

                
                        request.Conditions.FirstOrDefault().Filters.Add(new SearchyFilter
                            (nameof(GlnLog.Date), SearchyRule.GreaterThanOrEquals, new DateTime(2020, 4, 7,21,0,0)));
                        request.Conditions.FirstOrDefault().Filters.Add(new SearchyFilter
                            (nameof(GlnLog.Date), SearchyRule.LessThanOrEquals, new DateTime(2020,4,8, 21, 0, 0)));
                    
                    //await connection.Update(bag);
                    var bags = await connection.All<GlnLog>(request.Conditions);
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