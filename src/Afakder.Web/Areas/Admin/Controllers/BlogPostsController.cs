using System.Security.Claims;
using Afakder.Web.Data;
using Afakder.Web.Models.Entities;
using Afakder.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Afakder.Web.Areas.Admin.Controllers;

[Area("Admin"), Authorize(Roles = "Admin,Editor")]
public class BlogPostsController : Controller
{
    private readonly AfakderDbContext _db;
    private readonly ISlugService _slugService;
    private readonly UserManager<ApplicationUser> _userManager;

    public BlogPostsController(AfakderDbContext db, ISlugService slugService, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _slugService = slugService;
        _userManager = userManager;
    }

    // GET /admin/blogposts
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Blog Yazıları";

        var posts = await _db.BlogPosts
            .Include(p => p.Category)
            .Include(p => p.Author)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        return View(posts);
    }

    // GET /admin/blogposts/create
    public async Task<IActionResult> Create()
    {
        ViewData["Title"] = "Yeni Yazı";
        ViewBag.Categories = new SelectList(
            await _db.BlogCategories.OrderBy(c => c.SortOrder).ToListAsync(),
            "Id", "Name");

        return View(new BlogPost());
    }

    // POST /admin/blogposts/create
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BlogPost model)
    {
        model.AuthorId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        model.Slug = await _slugService.GenerateUniquePostSlugAsync(model.Title);
        model.CreatedAt = DateTime.UtcNow;
        model.UpdatedAt = DateTime.UtcNow;

        if (model.IsPublished)
            model.PublishedAt = DateTime.UtcNow;

        _db.BlogPosts.Add(model);
        await _db.SaveChangesAsync();

        TempData["Success"] = "Yazı başarıyla oluşturuldu.";
        return RedirectToAction("Index");
    }

    // GET /admin/blogposts/edit/5
    public async Task<IActionResult> Edit(int id)
    {
        ViewData["Title"] = "Yazıyı Düzenle";

        var post = await _db.BlogPosts.FindAsync(id);
        if (post == null)
        {
            TempData["Error"] = "Yazı bulunamadı.";
            return RedirectToAction("Index");
        }

        ViewBag.Categories = new SelectList(
            await _db.BlogCategories.OrderBy(c => c.SortOrder).ToListAsync(),
            "Id", "Name", post.CategoryId);

        return View(post);
    }

    // POST /admin/blogposts/edit/5
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, BlogPost model)
    {
        var post = await _db.BlogPosts.FindAsync(id);
        if (post == null)
        {
            TempData["Error"] = "Yazı bulunamadı.";
            return RedirectToAction("Index");
        }

        post.Title = model.Title;
        post.Slug = await _slugService.GenerateUniquePostSlugAsync(model.Title, id);
        post.Summary = model.Summary;
        post.Content = model.Content;
        post.FeaturedImageUrl = model.FeaturedImageUrl;
        post.MetaTitle = model.MetaTitle;
        post.MetaDescription = model.MetaDescription;
        post.CategoryId = model.CategoryId;
        post.UpdatedAt = DateTime.UtcNow;

        if (model.IsPublished && !post.IsPublished)
            post.PublishedAt = DateTime.UtcNow;

        post.IsPublished = model.IsPublished;

        await _db.SaveChangesAsync();

        TempData["Success"] = "Yazı başarıyla güncellendi.";
        return RedirectToAction("Index");
    }

    // POST /admin/blogposts/delete/5
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var post = await _db.BlogPosts.FindAsync(id);
        if (post != null)
        {
            _db.BlogPosts.Remove(post);
            await _db.SaveChangesAsync();
        }

        TempData["Success"] = "Yazı silindi.";
        return RedirectToAction("Index");
    }
}
