using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorld_ASPNetMVC5_AJS.Models
{
  public class MoviesAppContext : DbContext
  {
    public MoviesAppContext()//DbContextOptions options) : base(options)
    {

    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //  optionsBuilder.UseSqlServer("Filename=./blog.db");
    //}

    public DbSet<Movie> Movies { get; set; }

  }
}
