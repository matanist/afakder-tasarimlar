namespace Afakder.Web.Models.Entities;

public class SocialLink
{
    public int Id { get; set; }
    public string Platform { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string SvgIcon { get; set; } = string.Empty;
    public string AriaLabel { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public bool IsActive { get; set; } = true;
}
