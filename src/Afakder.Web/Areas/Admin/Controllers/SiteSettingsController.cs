using Afakder.Web.Data;
using Afakder.Web.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Afakder.Web.Areas.Admin.Controllers;

[Area("Admin"), Authorize(Roles = "Admin,Editor")]
public class SiteSettingsController : Controller
{
    private readonly AfakderDbContext _db;

    public SiteSettingsController(AfakderDbContext db)
    {
        _db = db;
    }

    // GET /admin/sitesettings/edit
    public async Task<IActionResult> Edit()
    {
        var settings = await _db.SiteSettings.FirstOrDefaultAsync();
        return View(settings);
    }

    // POST /admin/sitesettings/edit
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(SiteSetting model)
    {
        var settings = await _db.SiteSettings.FirstOrDefaultAsync();
        if (settings != null)
        {
            settings.LogoText = model.LogoText;
            settings.LogoSvg = model.LogoSvg;
            settings.FooterTagline = model.FooterTagline;
            settings.FooterMotto = model.FooterMotto;
            settings.PreloaderText = model.PreloaderText;
            settings.EmergencyNumber = model.EmergencyNumber;
            settings.EmergencyText = model.EmergencyText;
            settings.Copyright = model.Copyright;
            settings.DefaultPageTitle = model.DefaultPageTitle;
            settings.DefaultMetaDescription = model.DefaultMetaDescription;
            settings.NavCtaText = model.NavCtaText;
            settings.NavCtaUrl = model.NavCtaUrl;
            await _db.SaveChangesAsync();
        }
        TempData["Success"] = "Değişiklikler kaydedildi.";
        return RedirectToAction("Edit");
    }
}
