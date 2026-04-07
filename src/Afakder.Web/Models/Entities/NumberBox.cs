namespace Afakder.Web.Models.Entities;

public class NumberBox
{
    public int Id { get; set; }
    public string SvgIcon { get; set; } = string.Empty;
    public int TargetValue { get; set; }
    public string Label { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}
