using Afakder.Web.Data;
using Afakder.Web.Models.Entities;
using Afakder.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Afakder.Web.Controllers;

public class FormController : Controller
{
    private readonly AfakderDbContext _db;

    public FormController(AfakderDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Gonullu(VolunteerFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }

        _db.VolunteerApplications.Add(new VolunteerApplication
        {
            Name = model.Name,
            Email = model.Email,
            Phone = model.Phone,
            City = model.City,
            Message = model.Message
        });

        await _db.SaveChangesAsync();

        return Json(new { success = true, title = "Başvurunuz Alındı!", message = "Gönüllü başvurunuz başarıyla alınmıştır. En kısa sürede sizinle iletişime geçeceğiz. Birlikte daha güçlüyüz!" });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Iletisim(ContactFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }

        _db.ContactMessages.Add(new ContactMessage
        {
            Name = model.Name,
            Email = model.Email,
            Subject = model.Subject,
            Message = model.Message
        });

        await _db.SaveChangesAsync();

        return Json(new { success = true, title = "Mesajınız Gönderildi!", message = "Mesajınızı aldık, en kısa sürede size dönüş yapacağız. İlginiz için teşekkür ederiz." });
    }
}
