namespace Afakder.Web.Models.Entities;

public class SiteSetting
{
    public int Id { get; set; }

    // Branding
    public string LogoText { get; set; } = string.Empty;
    public string LogoSvg { get; set; } = string.Empty;
    public string FooterTagline { get; set; } = string.Empty;
    public string FooterMotto { get; set; } = string.Empty;
    public string PreloaderText { get; set; } = string.Empty;
    public string EmergencyNumber { get; set; } = string.Empty;
    public string EmergencyText { get; set; } = string.Empty;
    public string Copyright { get; set; } = string.Empty;

    // SEO defaults
    public string DefaultPageTitle { get; set; } = string.Empty;
    public string DefaultMetaDescription { get; set; } = string.Empty;

    // Nav CTA
    public string NavCtaText { get; set; } = string.Empty;
    public string NavCtaUrl { get; set; } = string.Empty;
}
