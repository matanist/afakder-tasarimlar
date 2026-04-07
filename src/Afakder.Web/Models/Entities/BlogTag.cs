namespace Afakder.Web.Models.Entities;

public class BlogTag
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public ICollection<BlogPostTag> PostTags { get; set; } = new List<BlogPostTag>();
}
