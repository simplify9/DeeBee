using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using SW.DeeBee.Extensions;

namespace SW.DeeBee.UnitTests
{
    public class TestStartup
    {
        public TestStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDeeBee(configure => 
            {
                configure.Provider = typeof(MySqlConnection);
                configure.ConntectionString = "Server=mysql-s9-do-user-6997732-0.db.ondigitalocean.com;Port=25060;Database=mailboxdb;User=doadmin;Password=pwpxz6xcmxxq9tlv;sslmode=none;";
            }); 

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseRouting();
            //app.UseAuthorization();
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
        }
    }
}
