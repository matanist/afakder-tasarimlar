namespace Afakder.Web.Models.Entities;

public class ImpactCard
{
    public int Id { get; set; }
    public int Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsFeatured { get; set; }
    public string FeaturedBadgeText { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}
