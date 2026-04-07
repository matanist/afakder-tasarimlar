using Afakder.Web.Models.Entities;

namespace Afakder.Web.Models.ViewModels;

public class BlogListViewModel
{
    public List<BlogPost> Posts { get; set; } = new();
    public List<BlogCategory> Categories { get; set; } = new();
    public List<BlogTag> PopularTags { get; set; } = new();
    public string? CurrentCategorySlug { get; set; }
    public string? CurrentTagSlug { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
}
