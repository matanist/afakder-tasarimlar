namespace Afakder.Web.Models.Entities;

public class ContactDetail
{
    public int Id { get; set; }
    public string Label { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string SvgIcon { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}
