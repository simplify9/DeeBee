using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using SW.DeeBee.UnitTests.Entities;
using SW.PrimitiveTypes;
using System;
using System.Threading.Tasks;

namespace SW.DeeBee.UnitTests
{
    [TestClass]
    public class UnitTest2
    {


        static TestServer server;


        [ClassInitialize]
        public static void ClassInitialize(TestContext tcontext)
        {
            server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseDefaultServiceProvider((context, options) => { options.ValidateScopes = true; })
                .UseEnvironment("UnitTesting")
                .UseStartup<TestStartup>());
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            server.Dispose();
        }


        [TestMethod]
        async public Task TestMethod1()
        {

            using (var scope = server.Host.Services.CreateScope())
            {
                var connHost = scope.ServiceProvider.GetService<ConnectionHost>();
                var bb = await connHost.All<Bag>();
            }


        }
    }
}
