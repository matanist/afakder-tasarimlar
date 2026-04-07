namespace Afakder.Web.Models.Entities;

public class BagisSection
{
    public int Id { get; set; }
    public string SectionTag { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string TitleHighlight { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string FooterNote { get; set; } = string.Empty;

    // Campaign Progress
    public string CampaignTitle { get; set; } = string.Empty;
    public string CampaignTarget { get; set; } = string.Empty;
    public int CampaignProgress { get; set; }
    public string CampaignAmount { get; set; } = string.Empty;
    public string CampaignNote { get; set; } = string.Empty;
}
