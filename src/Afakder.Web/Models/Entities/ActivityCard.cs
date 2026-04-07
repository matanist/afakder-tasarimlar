namespace Afakder.Web.Models.Entities;

public class ActivityCard
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string SvgIcon { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}
