using Afakder.Web.Data;
using Afakder.Web.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Afakder.Web.Areas.Admin.Controllers;

[Area("Admin"), Authorize(Roles = "Admin,Editor")]
public class RakamlarController : Controller
{
    private readonly AfakderDbContext _db;

    public RakamlarController(AfakderDbContext db)
    {
        _db = db;
    }

    // GET /admin/rakamlar/edit
    public async Task<IActionResult> Edit()
    {
        var items = await _db.NumberBoxes.OrderBy(n => n.SortOrder).ToListAsync();
        return View(items);
    }

    // POST /admin/rakamlar/create
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(NumberBox model)
    {
        _db.NumberBoxes.Add(model);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Değişiklikler kaydedildi.";
        return RedirectToAction("Edit");
    }

    // POST /admin/rakamlar/delete/5
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.NumberBoxes.FindAsync(id);
        if (item != null)
        {
            _db.NumberBoxes.Remove(item);
            await _db.SaveChangesAsync();
        }
        TempData["Success"] = "Değişiklikler kaydedildi.";
        return RedirectToAction("Edit");
    }
}
