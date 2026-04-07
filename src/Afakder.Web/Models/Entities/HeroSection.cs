namespace Afakder.Web.Models.Entities;

public class HeroSection
{
    public int Id { get; set; }
    public string TitleLine1 { get; set; } = string.Empty;
    public string TitleHighlight { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string PrimaryCtaText { get; set; } = string.Empty;
    public string PrimaryCtaUrl { get; set; } = string.Empty;
    public string SecondaryCtaText { get; set; } = string.Empty;
    public string SecondaryCtaUrl { get; set; } = string.Empty;
}
