using Afakder.Web.Data;
using Afakder.Web.Models.Entities;
using Afakder.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Afakder.Web.Areas.Admin.Controllers;

[Area("Admin"), Authorize(Roles = "Admin,Editor")]
public class BlogCategoriesController : Controller
{
    private readonly AfakderDbContext _db;
    private readonly ISlugService _slugService;

    public BlogCategoriesController(AfakderDbContext db, ISlugService slugService)
    {
        _db = db;
        _slugService = slugService;
    }

    // GET /admin/blogcategories/edit
    public async Task<IActionResult> Edit()
    {
        ViewData["Title"] = "Blog Kategorileri";
        var categories = await _db.BlogCategories.OrderBy(c => c.SortOrder).ToListAsync();
        return View(categories);
    }

    // POST /admin/blogcategories/create
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BlogCategory model)
    {
        model.Slug = await _slugService.GenerateUniqueCategorySlugAsync(model.Name);
        _db.BlogCategories.Add(model);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Kategori oluşturuldu.";
        return RedirectToAction("Edit");
    }

    // POST /admin/blogcategories/update/5
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, BlogCategory model)
    {
        var category = await _db.BlogCategories.FindAsync(id);
        if (category != null)
        {
            category.Name = model.Name;
            category.Slug = await _slugService.GenerateUniqueCategorySlugAsync(model.Name, id);
            category.Description = model.Description;
            category.SortOrder = model.SortOrder;
            await _db.SaveChangesAsync();
        }
        TempData["Success"] = "Kategori güncellendi.";
        return RedirectToAction("Edit");
    }

    // POST /admin/blogcategories/delete/5
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _db.BlogCategories.FindAsync(id);
        if (category != null)
        {
            var hasPost = await _db.BlogPosts.AnyAsync(p => p.CategoryId == id);
            if (hasPost)
            {
                TempData["Error"] = "Bu kategoride yazılar var, önce yazıları silin veya başka kategoriye taşıyınız.";
                return RedirectToAction("Edit");
            }
            _db.BlogCategories.Remove(category);
            await _db.SaveChangesAsync();
        }
        TempData["Success"] = "Kategori silindi.";
        return RedirectToAction("Edit");
    }
}
