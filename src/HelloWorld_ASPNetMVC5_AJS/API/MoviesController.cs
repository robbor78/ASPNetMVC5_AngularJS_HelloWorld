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
    [HttpGet]
    public IEnumerable<Movie> Get()
    {
      return new List<Movie> {
                new Movie {Id=1, Title="Star Wars", Director="Lucas"},
                new Movie {Id=2, Title="King Kong", Director="Jackson"},
                new Movie {Id=3, Title="Memento", Director="Nolan"}
            };
    }

    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
      return new ObjectResult(new Movie
      {
        Id = 1,
        Title = "Star Wars",
        Director = "Lucas"
      });
    }

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
