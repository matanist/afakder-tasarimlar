using Afakder.Web.Data;
using Afakder.Web.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Afakder.Web.Areas.Admin.Controllers;

[Area("Admin"), Authorize(Roles = "Admin,Editor")]
public class IletisimController : Controller
{
    private readonly AfakderDbContext _db;

    public IletisimController(AfakderDbContext db)
    {
        _db = db;
    }

    // GET /admin/iletisim/edit
    public async Task<IActionResult> Edit()
    {
        var section = await _db.IletisimSections.FirstOrDefaultAsync();
        var details = await _db.ContactDetails.OrderBy(d => d.SortOrder).ToListAsync();
        ViewBag.ContactDetails = details;
        return View(section);
    }

    // POST /admin/iletisim/edit
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(IletisimSection model)
    {
        var section = await _db.IletisimSections.FirstOrDefaultAsync();
        if (section != null)
        {
            section.SectionTag = model.SectionTag;
            section.Title = model.Title;
            section.Description = model.Description;
            section.FormTitle = model.FormTitle;
            section.SubmitButtonText = model.SubmitButtonText;
            await _db.SaveChangesAsync();
        }
        TempData["Success"] = "Değişiklikler kaydedildi.";
        return RedirectToAction("Edit");
    }

    // POST /admin/iletisim/createcontactdetail
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateContactDetail(ContactDetail model)
    {
        _db.ContactDetails.Add(model);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Değişiklikler kaydedildi.";
        return RedirectToAction("Edit");
    }

    // POST /admin/iletisim/deletecontactdetail/5
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteContactDetail(int id)
    {
        var detail = await _db.ContactDetails.FindAsync(id);
        if (detail != null)
        {
            _db.ContactDetails.Remove(detail);
            await _db.SaveChangesAsync();
        }
        TempData["Success"] = "Değişiklikler kaydedildi.";
        return RedirectToAction("Edit");
    }
}
