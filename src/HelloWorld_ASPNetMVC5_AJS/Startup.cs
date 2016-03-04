//Reference: http://stephenwalther.com/archive/2015/01/12/asp-net-5-and-angularjs-part-1-configuring-grunt-uglify-and-angularjs
//Reference: http://stephenwalther.com/archive/2015/01/16/asp-net-5-and-angularjs-part-3-adding-client-routing
//Reference: http://stephenwalther.com/archive/2015/01/17/asp-net-5-and-angularjs-part-4-using-entity-framework-7

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
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

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

      services.AddIdentity<ApplicationUser, IdentityRole>()
      .AddEntityFrameworkStores<MoviesAppContext>();
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseStaticFiles();
      app.UseIdentity();
      app.UseMvc();

      CreateSampleData(app.ApplicationServices).Wait();
    }

    private static async Task CreateSampleData(IServiceProvider applicationServices)
    {
      using (var dbContext = applicationServices.GetService<MoviesAppContext>())
      {
        var sqlServerDatabase = dbContext.Database;// as SqlServerDatabase;
        if (sqlServerDatabase != null)
        {
          // Create database in user root (c:\users\your name)
          if (await sqlServerDatabase.EnsureCreatedAsync())
          {
            // add some movies
            var movies = new List<Movie>
                {
                    new Movie {Title="Star Wars", Director="Lucas"},
                    new Movie {Title="King Kong", Director="Jackson"},
                    new Movie {Title="Memento", Director="Nolan"}
                };
            movies.ForEach(m => dbContext.Movies.Add(m));

            // add some users
            var userManager = applicationServices.GetService<UserManager<ApplicationUser>>();

            // add editor user
            var stephen = new ApplicationUser
            {
              UserName = "Stephen"
            };
            var result = await userManager.CreateAsync(stephen, "P@ssw0rd");
            await userManager.AddClaimAsync(stephen, new Claim("CanEdit", "true"));

            // add normal user
            var bob = new ApplicationUser
            {
              UserName = "Bob"
            };
            await userManager.CreateAsync(bob, "P@ssw0rd");
          }

        }
      }
    }

    // Entry point for the application.
    public static void Main(string[] args) => WebApplication.Run<Startup>(args);
  }
}
