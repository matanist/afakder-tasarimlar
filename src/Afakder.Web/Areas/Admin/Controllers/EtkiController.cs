using Afakder.Web.Data;
using Afakder.Web.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Afakder.Web.Areas.Admin.Controllers;

[Area("Admin"), Authorize(Roles = "Admin,Editor")]
public class EtkiController : Controller
{
    private readonly AfakderDbContext _db;

    public EtkiController(AfakderDbContext db)
    {
        _db = db;
    }

    // GET /admin/etki/edit
    public async Task<IActionResult> Edit()
    {
        var section = await _db.EtkiSections.FirstOrDefaultAsync();
        var cards = await _db.StoryCards.OrderBy(c => c.SortOrder).ToListAsync();
        ViewBag.StoryCards = cards;
        return View(section);
    }

    // POST /admin/etki/edit
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EtkiSection model)
    {
        var section = await _db.EtkiSections.FirstOrDefaultAsync();
        if (section != null)
        {
            section.SectionTag = model.SectionTag;
            section.Title = model.Title;
            section.TitleHighlight = model.TitleHighlight;
            section.Description = model.Description;
            await _db.SaveChangesAsync();
        }
        TempData["Success"] = "Değişiklikler kaydedildi.";
        return RedirectToAction("Edit");
    }

    // POST /admin/etki/createstorycard
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateStoryCard(StoryCard model)
    {
        _db.StoryCards.Add(model);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Değişiklikler kaydedildi.";
        return RedirectToAction("Edit");
    }

    // POST /admin/etki/deletestorycard/5
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteStoryCard(int id)
    {
        var card = await _db.StoryCards.FindAsync(id);
        if (card != null)
        {
            _db.StoryCards.Remove(card);
            await _db.SaveChangesAsync();
        }
        TempData["Success"] = "Değişiklikler kaydedildi.";
        return RedirectToAction("Edit");
    }
}
