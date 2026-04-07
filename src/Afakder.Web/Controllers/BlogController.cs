using Afakder.Web.Data;
using Afakder.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Afakder.Web.Controllers;

public class BlogController : Controller
{
    private readonly IBlogService _blog;
    private readonly AfakderDbContext _db;

    public BlogController(IBlogService blog, AfakderDbContext db)
    {
        _blog = blog;
        _db = db;
    }

    public async Task<IActionResult> Index(int page = 1, string? kategori = null, string? etiket = null)
    {
        var settings = await _db.SiteSettings.FirstAsync();
        ViewData["SiteSettings"] = settings;
        ViewData["NavLinks"] = await _db.NavLinks.Where(n => n.IsActive).OrderBy(n => n.SortOrder).ToListAsync();
        ViewData["SocialLinks"] = await _db.SocialLinks.Where(s => s.IsActive).OrderBy(s => s.SortOrder).ToListAsync();
        ViewData["FooterLinks"] = await _db.FooterLinks.Where(f => f.IsActive).OrderBy(f => f.SortOrder).ToListAsync();
        ViewData["Title"] = "Blog — " + settings.DefaultPageTitle;

        var model = await _blog.GetPostsAsync(page, 9, kategori, etiket);
        return View(model);
    }

    public async Task<IActionResult> Detail(string slug)
    {
        var post = await _blog.GetPostBySlugAsync(slug);
        if (post == null) return NotFound();

        var settings = await _db.SiteSettings.FirstAsync();
        ViewData["SiteSettings"] = settings;
        ViewData["NavLinks"] = await _db.NavLinks.Where(n => n.IsActive).OrderBy(n => n.SortOrder).ToListAsync();
        ViewData["SocialLinks"] = await _db.SocialLinks.Where(s => s.IsActive).OrderBy(s => s.SortOrder).ToListAsync();
        ViewData["FooterLinks"] = await _db.FooterLinks.Where(f => f.IsActive).OrderBy(f => f.SortOrder).ToListAsync();
        ViewData["Title"] = post.MetaTitle ?? post.Title;
        ViewData["MetaDescription"] = post.MetaDescription ?? post.Summary;

        return View(post);
    }
}
