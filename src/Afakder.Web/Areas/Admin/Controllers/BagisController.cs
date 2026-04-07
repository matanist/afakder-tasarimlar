using Afakder.Web.Data;
using Afakder.Web.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Afakder.Web.Areas.Admin.Controllers;

[Area("Admin"), Authorize(Roles = "Admin,Editor")]
public class BagisController : Controller
{
    private readonly AfakderDbContext _db;

    public BagisController(AfakderDbContext db)
    {
        _db = db;
    }

    // GET /admin/bagis/edit
    public async Task<IActionResult> Edit()
    {
        var section = await _db.BagisSections.FirstOrDefaultAsync();
        var cards = await _db.ImpactCards.OrderBy(c => c.SortOrder).ToListAsync();
        ViewBag.ImpactCards = cards;
        return View(section);
    }

    // POST /admin/bagis/edit
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(BagisSection model)
    {
        var section = await _db.BagisSections.FirstOrDefaultAsync();
        if (section != null)
        {
            section.SectionTag = model.SectionTag;
            section.Title = model.Title;
            section.TitleHighlight = model.TitleHighlight;
            section.Description = model.Description;
            section.FooterNote = model.FooterNote;
            section.CampaignTitle = model.CampaignTitle;
            section.CampaignTarget = model.CampaignTarget;
            section.CampaignProgress = model.CampaignProgress;
            section.CampaignAmount = model.CampaignAmount;
            section.CampaignNote = model.CampaignNote;
            await _db.SaveChangesAsync();
        }
        TempData["Success"] = "Değişiklikler kaydedildi.";
        return RedirectToAction("Edit");
    }

    // POST /admin/bagis/createimpactcard
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateImpactCard(ImpactCard model)
    {
        _db.ImpactCards.Add(model);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Değişiklikler kaydedildi.";
        return RedirectToAction("Edit");
    }

    // POST /admin/bagis/deleteimpactcard/5
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteImpactCard(int id)
    {
        var card = await _db.ImpactCards.FindAsync(id);
        if (card != null)
        {
            _db.ImpactCards.Remove(card);
            await _db.SaveChangesAsync();
        }
        TempData["Success"] = "Değişiklikler kaydedildi.";
        return RedirectToAction("Edit");
    }
}
