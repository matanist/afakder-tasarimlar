namespace Afakder.Web.Services;

public interface ISlugService
{
    string GenerateSlug(string text);
    Task<string> GenerateUniquePostSlugAsync(string title, int? excludeId = null);
    Task<string> GenerateUniqueCategorySlugAsync(string name, int? excludeId = null);
    Task<string> GenerateUniqueTagSlugAsync(string name, int? excludeId = null);
}
