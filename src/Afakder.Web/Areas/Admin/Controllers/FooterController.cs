using Afakder.Web.Data;
using Afakder.Web.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Afakder.Web.Areas.Admin.Controllers;

[Area("Admin"), Authorize(Roles = "Admin,Editor")]
public class FooterController : Controller
{
    private readonly AfakderDbContext _db;

    public FooterController(AfakderDbContext db)
    {
        _db = db;
    }

    // GET /admin/footer/edit
    public async Task<IActionResult> Edit()
    {
        var footerLinks = await _db.FooterLinks.OrderBy(l => l.SortOrder).ToListAsync();
        var socialLinks = await _db.SocialLinks.OrderBy(l => l.SortOrder).ToListAsync();
        ViewBag.FooterLinks = footerLinks;
        ViewBag.SocialLinks = socialLinks;
        return View();
    }

    // POST /admin/footer/createfooterlink
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateFooterLink(FooterLink model)
    {
        _db.FooterLinks.Add(model);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Değişiklikler kaydedildi.";
        return RedirectToAction("Edit");
    }

    // POST /admin/footer/deletefooterlink/5
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteFooterLink(int id)
    {
        var link = await _db.FooterLinks.FindAsync(id);
        if (link != null)
        {
            _db.FooterLinks.Remove(link);
            await _db.SaveChangesAsync();
        }
        TempData["Success"] = "Değişiklikler kaydedildi.";
        return RedirectToAction("Edit");
    }

    // POST /admin/footer/createsociallink
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateSocialLink(SocialLink model)
    {
        _db.SocialLinks.Add(model);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Değişiklikler kaydedildi.";
        return RedirectToAction("Edit");
    }

    // POST /admin/footer/deletesociallink/5
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteSocialLink(int id)
    {
        var link = await _db.SocialLinks.FindAsync(id);
        if (link != null)
        {
            _db.SocialLinks.Remove(link);
            await _db.SaveChangesAsync();
        }
        TempData["Success"] = "Değişiklikler kaydedildi.";
        return RedirectToAction("Edit");
    }
}
