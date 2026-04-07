using Afakder.Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Afakder.Web.Areas.Admin.Controllers;

[Area("Admin"), Authorize(Roles = "Admin,Editor")]
public class ContactMessagesController : Controller
{
    private readonly AfakderDbContext _db;

    public ContactMessagesController(AfakderDbContext db)
    {
        _db = db;
    }

    // GET /admin/contactmessages
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "İletişim Mesajları";

        var messages = await _db.ContactMessages
            .OrderByDescending(m => m.SubmittedAt)
            .ToListAsync();

        return View(messages);
    }

    // GET /admin/contactmessages/detail/5
    public async Task<IActionResult> Detail(int id)
    {
        ViewData["Title"] = "Mesaj Detayı";

        var message = await _db.ContactMessages.FindAsync(id);
        if (message == null)
        {
            TempData["Error"] = "Mesaj bulunamadı.";
            return RedirectToAction("Index");
        }

        if (!message.IsRead)
        {
            message.IsRead = true;
            await _db.SaveChangesAsync();
        }

        return View(message);
    }

    // POST /admin/contactmessages/delete/5
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var message = await _db.ContactMessages.FindAsync(id);
        if (message != null)
        {
            _db.ContactMessages.Remove(message);
            await _db.SaveChangesAsync();
        }

        TempData["Success"] = "Mesaj silindi.";
        return RedirectToAction("Index");
    }
}
