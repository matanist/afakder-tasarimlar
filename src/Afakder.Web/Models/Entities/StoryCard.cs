namespace Afakder.Web.Models.Entities;

public class StoryCard
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Quote { get; set; } = string.Empty;
    public string AvatarCssClass { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}
