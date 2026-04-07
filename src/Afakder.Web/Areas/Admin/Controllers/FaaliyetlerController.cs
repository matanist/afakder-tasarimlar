using Afakder.Web.Data;
using Afakder.Web.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Afakder.Web.Areas.Admin.Controllers;

[Area("Admin"), Authorize(Roles = "Admin,Editor")]
public class FaaliyetlerController : Controller
{
    private readonly AfakderDbContext _db;

    public FaaliyetlerController(AfakderDbContext db)
    {
        _db = db;
    }

    // GET /admin/faaliyetler/edit
    public async Task<IActionResult> Edit()
    {
        var section = await _db.FaaliyetlerSections.FirstOrDefaultAsync();
        var cards = await _db.ActivityCards.OrderBy(c => c.SortOrder).ToListAsync();
        ViewBag.ActivityCards = cards;
        return View(section);
    }

    // POST /admin/faaliyetler/edit
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(FaaliyetlerSection model)
    {
        var section = await _db.FaaliyetlerSections.FirstOrDefaultAsync();
        if (section != null)
        {
            section.SectionTag = model.SectionTag;
            section.Title = model.Title;
            section.Description = model.Description;
            await _db.SaveChangesAsync();
        }
        TempData["Success"] = "Değişiklikler kaydedildi.";
        return RedirectToAction("Edit");
    }

    // POST /admin/faaliyetler/createactivitycard
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateActivityCard(ActivityCard model)
    {
        _db.ActivityCards.Add(model);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Değişiklikler kaydedildi.";
        return RedirectToAction("Edit");
    }

    // POST /admin/faaliyetler/deleteactivitycard/5
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteActivityCard(int id)
    {
        var card = await _db.ActivityCards.FindAsync(id);
        if (card != null)
        {
            _db.ActivityCards.Remove(card);
            await _db.SaveChangesAsync();
        }
        TempData["Success"] = "Değişiklikler kaydedildi.";
        return RedirectToAction("Edit");
    }
}
