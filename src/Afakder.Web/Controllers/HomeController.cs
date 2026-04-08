using Microsoft.AspNetCore.Mvc;

namespace Afakder.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}
