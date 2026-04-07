using Afakder.Web.Data;
using Afakder.Web.Models.Entities;
using Afakder.Web.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Afakder.Web.Services;

public class BlogService : IBlogService
{
    private readonly AfakderDbContext _db;

    public BlogService(AfakderDbContext db)
    {
        _db = db;
    }

    public async Task<BlogListViewModel> GetPostsAsync(int page, int pageSize, string? categorySlug = null, string? tagSlug = null)
    {
        var query = _db.BlogPosts
            .Include(p => p.Author)
            .Include(p => p.Category)
            .Include(p => p.PostTags).ThenInclude(pt => pt.BlogTag)
            .Where(p => p.IsPublished);

        if (!string.IsNullOrEmpty(categorySlug))
            query = query.Where(p => p.Category.Slug == categorySlug);

        if (!string.IsNullOrEmpty(tagSlug))
            query = query.Where(p => p.PostTags.Any(pt => pt.BlogTag.Slug == tagSlug));

        var totalCount = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        var posts = await query
            .OrderByDescending(p => p.PublishedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new BlogListViewModel
        {
            Posts = posts,
            Categories = await GetCategoriesAsync(),
            PopularTags = await GetPopularTagsAsync(),
            CurrentCategorySlug = categorySlug,
            CurrentTagSlug = tagSlug,
            CurrentPage = page,
            TotalPages = totalPages,
            TotalCount = totalCount
        };
    }

    public async Task<BlogPost?> GetPostBySlugAsync(string slug)
    {
        return await _db.BlogPosts
            .Include(p => p.Author)
            .Include(p => p.Category)
            .Include(p => p.PostTags).ThenInclude(pt => pt.BlogTag)
            .FirstOrDefaultAsync(p => p.Slug == slug && p.IsPublished);
    }

    public async Task<List<BlogCategory>> GetCategoriesAsync()
    {
        return await _db.BlogCategories
            .OrderBy(c => c.SortOrder)
            .ToListAsync();
    }

    public async Task<List<BlogTag>> GetPopularTagsAsync(int count = 10)
    {
        return await _db.BlogTags
            .OrderByDescending(t => t.PostTags.Count(pt => pt.BlogPost.IsPublished))
            .Take(count)
            .ToListAsync();
    }
}
