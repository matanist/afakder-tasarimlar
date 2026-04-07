using Afakder.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Afakder.Web.Controllers;

public class SitemapController : Controller
{
    private readonly AfakderDbContext _db;

    public SitemapController(AfakderDbContext db)
    {
        _db = db;
    }

    [Route("sitemap.xml")]
    [ResponseCache(Duration = 3600)]
    public async Task<IActionResult> Index()
    {
        var baseUrl = $"{Request.Scheme}://{Request.Host}";

        var urls = new List<string>
        {
            $"<url><loc>{baseUrl}/</loc><changefreq>weekly</changefreq><priority>1.0</priority></url>",
            $"<url><loc>{baseUrl}/blog</loc><changefreq>daily</changefreq><priority>0.8</priority></url>"
        };

        // Blog posts
        var posts = await _db.BlogPosts
            .Where(p => p.IsPublished)
            .OrderByDescending(p => p.PublishedAt)
            .Select(p => new { p.Slug, p.UpdatedAt })
            .ToListAsync();

        foreach (var post in posts)
        {
            urls.Add($"<url><loc>{baseUrl}/blog/{post.Slug}</loc><lastmod>{post.UpdatedAt:yyyy-MM-dd}</lastmod><changefreq>monthly</changefreq><priority>0.6</priority></url>");
        }

        // Blog categories
        var categories = await _db.BlogCategories.Select(c => c.Slug).ToListAsync();
        foreach (var slug in categories)
        {
            urls.Add($"<url><loc>{baseUrl}/blog/kategori/{slug}</loc><changefreq>weekly</changefreq><priority>0.5</priority></url>");
        }

        var xml = $@"<?xml version=""1.0"" encoding=""UTF-8""?>
<urlset xmlns=""http://www.sitemaps.org/schemas/sitemap/0.9"">
{string.Join("\n", urls)}
</urlset>";

        return Content(xml, "application/xml");
    }
}
