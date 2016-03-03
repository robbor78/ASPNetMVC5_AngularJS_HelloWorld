using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorld_ASPNetMVC5_AJS.Models
{
  public class MoviesAppContext : DbContext
  {

    public DbSet<Movie> Movies { get; set; }

  }
}
