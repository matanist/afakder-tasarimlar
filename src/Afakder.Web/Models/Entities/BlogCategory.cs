namespace Afakder.Web.Models.Entities;

public class BlogCategory
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int SortOrder { get; set; }
    public ICollection<BlogPost> Posts { get; set; } = new List<BlogPost>();
}
