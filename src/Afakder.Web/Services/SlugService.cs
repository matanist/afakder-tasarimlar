using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Afakder.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace Afakder.Web.Services;

public partial class SlugService : ISlugService
{
    private readonly AfakderDbContext _db;

    public SlugService(AfakderDbContext db)
    {
        _db = db;
    }

    public string GenerateSlug(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;

        // Turkish character mapping
        var slug = text.ToLowerInvariant();
        slug = slug.Replace('ç', 'c').Replace('ğ', 'g').Replace('ı', 'i')
                   .Replace('ö', 'o').Replace('ş', 's').Replace('ü', 'u')
                   .Replace('Ç', 'c').Replace('Ğ', 'g').Replace('İ', 'i')
                   .Replace('Ö', 'o').Replace('Ş', 's').Replace('Ü', 'u');

        // Remove diacritics
        var normalized = slug.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();
        foreach (var c in normalized)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                sb.Append(c);
        }
        slug = sb.ToString().Normalize(NormalizationForm.FormC);

        // Replace non-alphanumeric with hyphens
        slug = NonAlphanumericRegex().Replace(slug, "-");
        slug = MultipleHyphensRegex().Replace(slug, "-");
        slug = slug.Trim('-');

        return slug;
    }

    public async Task<string> GenerateUniquePostSlugAsync(string title, int? excludeId = null)
    {
        var baseSlug = GenerateSlug(title);
        var slug = baseSlug;
        var counter = 1;

        while (await _db.BlogPosts.AnyAsync(p => p.Slug == slug && (excludeId == null || p.Id != excludeId)))
        {
            counter++;
            slug = $"{baseSlug}-{counter}";
        }

        return slug;
    }

    public async Task<string> GenerateUniqueCategorySlugAsync(string name, int? excludeId = null)
    {
        var baseSlug = GenerateSlug(name);
        var slug = baseSlug;
        var counter = 1;

        while (await _db.BlogCategories.AnyAsync(c => c.Slug == slug && (excludeId == null || c.Id != excludeId)))
        {
            counter++;
            slug = $"{baseSlug}-{counter}";
        }

        return slug;
    }

    public async Task<string> GenerateUniqueTagSlugAsync(string name, int? excludeId = null)
    {
        var baseSlug = GenerateSlug(name);
        var slug = baseSlug;
        var counter = 1;

        while (await _db.BlogTags.AnyAsync(t => t.Slug == slug && (excludeId == null || t.Id != excludeId)))
        {
            counter++;
            slug = $"{baseSlug}-{counter}";
        }

        return slug;
    }

    [GeneratedRegex("[^a-z0-9]")]
    private static partial Regex NonAlphanumericRegex();

    [GeneratedRegex("-{2,}")]
    private static partial Regex MultipleHyphensRegex();
}
