using Afakder.Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Afakder.Web.Areas.Admin.Controllers;

[Area("Admin"), Authorize(Roles = "Admin,Editor")]
public class VolunteerAppsController : Controller
{
    private readonly AfakderDbContext _db;

    public VolunteerAppsController(AfakderDbContext db)
    {
        _db = db;
    }

    // GET /admin/volunteerapps
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Gönüllü Başvuruları";

        var apps = await _db.VolunteerApplications
            .OrderByDescending(v => v.SubmittedAt)
            .ToListAsync();

        return View(apps);
    }

    // GET /admin/volunteerapps/detail/5
    public async Task<IActionResult> Detail(int id)
    {
        ViewData["Title"] = "Başvuru Detayı";

        var app = await _db.VolunteerApplications.FindAsync(id);
        if (app == null)
        {
            TempData["Error"] = "Başvuru bulunamadı.";
            return RedirectToAction("Index");
        }

        if (!app.IsRead)
        {
            app.IsRead = true;
            await _db.SaveChangesAsync();
        }

        return View(app);
    }

    // POST /admin/volunteerapps/delete/5
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var app = await _db.VolunteerApplications.FindAsync(id);
        if (app != null)
        {
            _db.VolunteerApplications.Remove(app);
            await _db.SaveChangesAsync();
        }

        TempData["Success"] = "Başvuru silindi.";
        return RedirectToAction("Index");
    }
}
