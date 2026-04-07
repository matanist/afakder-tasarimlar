namespace Afakder.Web.Models.Entities;

public class GonulluSection
{
    public int Id { get; set; }
    public string SectionTag { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string TitleHighlight { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string FormTitle { get; set; } = string.Empty;
    public string SubmitButtonText { get; set; } = string.Empty;
}
