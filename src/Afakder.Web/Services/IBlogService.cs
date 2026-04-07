using Afakder.Web.Models.Entities;
using Afakder.Web.Models.ViewModels;

namespace Afakder.Web.Services;

public interface IBlogService
{
    Task<BlogListViewModel> GetPostsAsync(int page, int pageSize, string? categorySlug = null, string? tagSlug = null);
    Task<BlogPost?> GetPostBySlugAsync(string slug);
    Task<List<BlogCategory>> GetCategoriesAsync();
    Task<List<BlogTag>> GetPopularTagsAsync(int count = 10);
}
