//Reference: http://stephenwalther.com/archive/2015/01/16/asp-net-5-and-angularjs-part-3-adding-client-routing


using HelloWorld_ASPNetMVC5_AJS.Models;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.Framework.Configuration.Json;
//using Microsoft.Framework.ConfigurationModel;

using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Data.Entity;


namespace HelloWorld_ASPNetMVC5_AJS
{
  public class Startup
  {
    public Startup(IHostingEnvironment env)
    {
      // Setup configuration sources.
      //Configuration = new Configuration()
      //    .AddJsonFile("config.json")
      //    .AddEnvironmentVariables();

      var builder = new ConfigurationBuilder()
    .AddJsonFile("config.json")
    .AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; set; }

    public void ConfigureServices(IServiceCollection services)
    {
      //var connection = @"Server=(localdb)\mssqllocaldb;Database=EFGetStarted.AspNet5.NewDb;Trusted_Connection=True;";
      var connection = @"Server=.\SQLEXPRESS;Database=EFGetStarted.AspNet5.NewDb;Trusted_Connection=True;";

      services.AddMvc();

      connection = Configuration.Get<string>("Data:DefaultConnection:ConnectionString");

      // Register Entity Framework
      services.AddEntityFramework()
          .AddSqlServer()
          .AddDbContext<MoviesAppContext>(options => options.UseSqlServer(connection));
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseStaticFiles();
      app.UseMvc();
    }


    // Entry point for the application.
    public static void Main(string[] args) => WebApplication.Run<Startup>(args);
  }
}
