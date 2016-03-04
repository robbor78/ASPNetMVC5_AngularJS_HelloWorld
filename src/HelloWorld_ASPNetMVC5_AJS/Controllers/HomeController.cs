using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HelloWorld_ASPNetMVC5_AJS.Controllers
{
  [Authorize]
  public class HomeController : Controller
  {

    public IActionResult Index()
    {
      var user = (ClaimsIdentity)User.Identity;
      ViewBag.Name = user.Name;
      ViewBag.CanEdit = user.FindFirst("CanEdit") != null ? "true" : "false";
      return View();
    }
  }
}
