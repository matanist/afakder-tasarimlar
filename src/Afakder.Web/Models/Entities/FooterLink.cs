namespace Afakder.Web.Models.Entities;

public class FooterLink
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public bool IsActive { get; set; } = true;
}
