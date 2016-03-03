using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using HelloWorld_ASPNetMVC5_AJS.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloWorld_ASPNetMVC5_AJS.API
{
  [Route("api/[controller]")]
  public class MoviesController : Controller
  {
    private readonly MoviesAppContext _dbContext;

    public MoviesController(MoviesAppContext dbContext)
    {
      _dbContext = dbContext;
    }

    [HttpGet]
    public IEnumerable<Movie> Get()
    {
      return _dbContext.Movies;
    }

    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
      var movie = _dbContext.Movies.FirstOrDefault(m => m.Id == id);
      if (movie == null)
      {
        return new HttpNotFoundResult();
      }
      else {
        return new ObjectResult(movie);
      }
    }

    [HttpPost]
    public IActionResult Post([FromBody]Movie movie)
    {
      if (movie.Id == 0)
      {
        _dbContext.Movies.Add(movie);
        _dbContext.SaveChanges();
        return new ObjectResult(movie);
      }
      else
      {
        var original = _dbContext.Movies.FirstOrDefault(m => m.Id == movie.Id);
        original.Title = movie.Title;
        original.Director = movie.Director;
        _dbContext.SaveChanges();
        return new ObjectResult(original);
      }
    }


    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
      var movie = _dbContext.Movies.FirstOrDefault(m => m.Id == id);
      _dbContext.Movies.Remove(movie);
      _dbContext.SaveChanges();
      return new HttpStatusCodeResult(200);
    }

    //[HttpGet]
    ////    public IEnumerable<Movie> Get()
    //public IActionResult Get()
    //{
    //  var movie = _dbContext.Movies.FirstOrDefault(m => m.Id == id);
    //  if (movie == null)
    //  {
    //    return new HttpNotFoundResult();
    //  }
    //  else {
    //    return new ObjectResult(movie);
    //  }
    //  //return new List<Movie> {
    //  //          new Movie {Id=1, Title="Star Wars", Director="Lucas"},
    //  //          new Movie {Id=2, Title="King Kong", Director="Jackson"},
    //  //          new Movie {Id=3, Title="Memento", Director="Nolan"}
    //  //      };
    //}

    //[HttpGet("{id:int}")]
    //public IActionResult Get(int id)
    //{
    //  return new ObjectResult(new Movie
    //  {
    //    Id = 1,
    //    Title = "Star Wars",
    //    Director = "Lucas"
    //  });
    //}

    //// GET: api/values
    //[HttpGet]
    //public IEnumerable<string> Get()
    //{
    //    return new string[] { "value1", "value2" };
    //}

    //// GET api/values/5
    //[HttpGet("{id}")]
    //public string Get(int id)
    //{
    //    return "value";
    //}

    //// POST api/values
    //[HttpPost]
    //public void Post([FromBody]string value)
    //{
    //}

    //// PUT api/values/5
    //[HttpPut("{id}")]
    //public void Put(int id, [FromBody]string value)
    //{
    //}

    //// DELETE api/values/5
    //[HttpDelete("{id}")]
    //public void Delete(int id)
    //{
    //}
  }
}
