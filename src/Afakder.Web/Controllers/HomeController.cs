using Afakder.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Afakder.Web.Controllers;

public class HomeController : Controller
{
    private readonly IContentService _content;

    public HomeController(IContentService content)
    {
        _content = content;
    }

    public async Task<IActionResult> Index()
    {
        var model = await _content.GetHomePageViewModelAsync();

        // Pass shared data to layout via ViewData
        ViewData["SiteSettings"] = model.SiteSettings;
        ViewData["NavLinks"] = model.NavLinks;
        ViewData["SocialLinks"] = model.SocialLinks;
        ViewData["FooterLinks"] = model.FooterLinks;

        return View(model);
    }
}
