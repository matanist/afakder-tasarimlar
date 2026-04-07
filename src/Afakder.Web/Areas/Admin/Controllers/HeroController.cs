using Afakder.Web.Data;
using Afakder.Web.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Afakder.Web.Areas.Admin.Controllers;

[Area("Admin"), Authorize(Roles = "Admin,Editor")]
public class HeroController : Controller
{
    private readonly AfakderDbContext _db;

    public HeroController(AfakderDbContext db)
    {
        _db = db;
    }

    // GET /admin/hero/edit
    public async Task<IActionResult> Edit()
    {
        var section = await _db.HeroSections.FirstOrDefaultAsync();
        var stats = await _db.HeroStats.OrderBy(s => s.SortOrder).ToListAsync();
        ViewBag.Stats = stats;
        return View(section);
    }

    // POST /admin/hero/edit
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(HeroSection model)
    {
        var section = await _db.HeroSections.FirstOrDefaultAsync();
        if (section != null)
        {
            section.TitleLine1 = model.TitleLine1;
            section.TitleHighlight = model.TitleHighlight;
            section.Subtitle = model.Subtitle;
            section.PrimaryCtaText = model.PrimaryCtaText;
            section.PrimaryCtaUrl = model.PrimaryCtaUrl;
            section.SecondaryCtaText = model.SecondaryCtaText;
            section.SecondaryCtaUrl = model.SecondaryCtaUrl;
            await _db.SaveChangesAsync();
        }
        TempData["Success"] = "Değişiklikler kaydedildi.";
        return RedirectToAction("Edit");
    }

    // POST /admin/hero/createstat
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateStat(HeroStat model)
    {
        _db.HeroStats.Add(model);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Değişiklikler kaydedildi.";
        return RedirectToAction("Edit");
    }

    // POST /admin/hero/deletestat/5
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteStat(int id)
    {
        var stat = await _db.HeroStats.FindAsync(id);
        if (stat != null)
        {
            _db.HeroStats.Remove(stat);
            await _db.SaveChangesAsync();
        }
        TempData["Success"] = "Değişiklikler kaydedildi.";
        return RedirectToAction("Edit");
    }
}
