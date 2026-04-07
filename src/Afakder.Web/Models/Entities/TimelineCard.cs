namespace Afakder.Web.Models.Entities;

public class TimelineCard
{
    public int Id { get; set; }
    public string HourRange { get; set; } = string.Empty;
    public string HourLabel { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string SvgIcon { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}
