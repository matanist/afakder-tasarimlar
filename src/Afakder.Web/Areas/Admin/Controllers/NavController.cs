using Afakder.Web.Data;
using Afakder.Web.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Afakder.Web.Areas.Admin.Controllers;

[Area("Admin"), Authorize(Roles = "Admin,Editor")]
public class NavController : Controller
{
    private readonly AfakderDbContext _db;

    public NavController(AfakderDbContext db)
    {
        _db = db;
    }

    // GET /admin/nav/edit
    public async Task<IActionResult> Edit()
    {
        var links = await _db.NavLinks.OrderBy(l => l.SortOrder).ToListAsync();
        return View(links);
    }

    // POST /admin/nav/create
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(NavLink model)
    {
        _db.NavLinks.Add(model);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Değişiklikler kaydedildi.";
        return RedirectToAction("Edit");
    }

    // POST /admin/nav/delete/5
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var link = await _db.NavLinks.FindAsync(id);
        if (link != null)
        {
            _db.NavLinks.Remove(link);
            await _db.SaveChangesAsync();
        }
        TempData["Success"] = "Değişiklikler kaydedildi.";
        return RedirectToAction("Edit");
    }
}
