using Afakder.Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Afakder.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,Editor")]
public class DashboardController : Controller
{
    private readonly AfakderDbContext _db;

    public DashboardController(AfakderDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Dashboard";

        ViewData["UnreadVolunteerApps"] = await _db.VolunteerApplications.CountAsync(v => !v.IsRead);
        ViewData["UnreadContactMessages"] = await _db.ContactMessages.CountAsync(m => !m.IsRead);
        ViewData["PublishedPosts"] = await _db.BlogPosts.CountAsync(p => p.IsPublished);
        ViewData["DraftPosts"] = await _db.BlogPosts.CountAsync(p => !p.IsPublished);

        return View();
    }
}
