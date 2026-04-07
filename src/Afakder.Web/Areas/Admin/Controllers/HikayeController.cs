using Afakder.Web.Data;
using Afakder.Web.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Afakder.Web.Areas.Admin.Controllers;

[Area("Admin"), Authorize(Roles = "Admin,Editor")]
public class HikayeController : Controller
{
    private readonly AfakderDbContext _db;

    public HikayeController(AfakderDbContext db)
    {
        _db = db;
    }

    // GET /admin/hikaye/edit
    public async Task<IActionResult> Edit()
    {
        var section = await _db.HikayeSections.FirstOrDefaultAsync();
        var cards = await _db.TimelineCards.OrderBy(c => c.SortOrder).ToListAsync();
        ViewBag.TimelineCards = cards;
        return View(section);
    }

    // POST /admin/hikaye/edit
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(HikayeSection model)
    {
        var section = await _db.HikayeSections.FirstOrDefaultAsync();
        if (section != null)
        {
            section.SectionTag = model.SectionTag;
            section.MegaNumber = model.MegaNumber;
            section.MegaUnit = model.MegaUnit;
            section.Subtitle = model.Subtitle;
            await _db.SaveChangesAsync();
        }
        TempData["Success"] = "Değişiklikler kaydedildi.";
        return RedirectToAction("Edit");
    }

    // POST /admin/hikaye/createtimelinecard
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateTimelineCard(TimelineCard model)
    {
        _db.TimelineCards.Add(model);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Değişiklikler kaydedildi.";
        return RedirectToAction("Edit");
    }

    // POST /admin/hikaye/deletetimelinecard/5
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteTimelineCard(int id)
    {
        var card = await _db.TimelineCards.FindAsync(id);
        if (card != null)
        {
            _db.TimelineCards.Remove(card);
            await _db.SaveChangesAsync();
        }
        TempData["Success"] = "Değişiklikler kaydedildi.";
        return RedirectToAction("Edit");
    }
}
