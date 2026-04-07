using Afakder.Web.Data;
using Afakder.Web.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Afakder.Web.Areas.Admin.Controllers;

[Area("Admin"), Authorize(Roles = "Admin,Editor")]
public class GonulluController : Controller
{
    private readonly AfakderDbContext _db;

    public GonulluController(AfakderDbContext db)
    {
        _db = db;
    }

    // GET /admin/gonullu/edit
    public async Task<IActionResult> Edit()
    {
        var section = await _db.GonulluSections.FirstOrDefaultAsync();
        var benefits = await _db.VolunteerBenefits.OrderBy(b => b.SortOrder).ToListAsync();
        ViewBag.Benefits = benefits;
        return View(section);
    }

    // POST /admin/gonullu/edit
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(GonulluSection model)
    {
        var section = await _db.GonulluSections.FirstOrDefaultAsync();
        if (section != null)
        {
            section.SectionTag = model.SectionTag;
            section.Title = model.Title;
            section.TitleHighlight = model.TitleHighlight;
            section.Description = model.Description;
            section.FormTitle = model.FormTitle;
            section.SubmitButtonText = model.SubmitButtonText;
            await _db.SaveChangesAsync();
        }
        TempData["Success"] = "Değişiklikler kaydedildi.";
        return RedirectToAction("Edit");
    }

    // POST /admin/gonullu/createbenefit
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateBenefit(VolunteerBenefit model)
    {
        _db.VolunteerBenefits.Add(model);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Değişiklikler kaydedildi.";
        return RedirectToAction("Edit");
    }

    // POST /admin/gonullu/deletebenefit/5
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteBenefit(int id)
    {
        var benefit = await _db.VolunteerBenefits.FindAsync(id);
        if (benefit != null)
        {
            _db.VolunteerBenefits.Remove(benefit);
            await _db.SaveChangesAsync();
        }
        TempData["Success"] = "Değişiklikler kaydedildi.";
        return RedirectToAction("Edit");
    }
}
