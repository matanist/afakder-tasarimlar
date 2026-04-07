namespace Afakder.Web.Models.Entities;

public class HeroStat
{
    public int Id { get; set; }
    public string Value { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}
