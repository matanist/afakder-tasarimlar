using Afakder.Web.Data;
using Afakder.Web.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Afakder.Web.Services;

public class ContentService : IContentService
{
    private readonly AfakderDbContext _db;

    public ContentService(AfakderDbContext db)
    {
        _db = db;
    }

    public async Task<HomePageViewModel> GetHomePageViewModelAsync()
    {
        var siteSettings = await _db.SiteSettings.FirstAsync();
        var navLinks = await _db.NavLinks.Where(n => n.IsActive).OrderBy(n => n.SortOrder).ToListAsync();
        var socialLinks = await _db.SocialLinks.Where(s => s.IsActive).OrderBy(s => s.SortOrder).ToListAsync();
        var footerLinks = await _db.FooterLinks.Where(f => f.IsActive).OrderBy(f => f.SortOrder).ToListAsync();

        return new HomePageViewModel
        {
            SiteSettings = siteSettings,
            NavLinks = navLinks,
            SocialLinks = socialLinks,
            FooterLinks = footerLinks,
            Hero = new HeroViewModel
            {
                Section = await _db.HeroSections.FirstAsync(),
                Stats = await _db.HeroStats.OrderBy(s => s.SortOrder).ToListAsync()
            },
            Hikaye = new HikayeViewModel
            {
                Section = await _db.HikayeSections.FirstAsync(),
                TimelineCards = await _db.TimelineCards.OrderBy(t => t.SortOrder).ToListAsync()
            },
            Etki = new EtkiViewModel
            {
                Section = await _db.EtkiSections.FirstAsync(),
                StoryCards = await _db.StoryCards.OrderBy(s => s.SortOrder).ToListAsync()
            },
            Rakamlar = await _db.NumberBoxes.OrderBy(n => n.SortOrder).ToListAsync(),
            Faaliyetler = new FaaliyetlerViewModel
            {
                Section = await _db.FaaliyetlerSections.FirstAsync(),
                ActivityCards = await _db.ActivityCards.OrderBy(a => a.SortOrder).ToListAsync()
            },
            Bagis = new BagisViewModel
            {
                Section = await _db.BagisSections.FirstAsync(),
                ImpactCards = await _db.ImpactCards.OrderBy(i => i.SortOrder).ToListAsync()
            },
            Gonullu = new GonulluViewModel
            {
                Section = await _db.GonulluSections.FirstAsync(),
                Benefits = await _db.VolunteerBenefits.OrderBy(b => b.SortOrder).ToListAsync()
            },
            Iletisim = new IletisimViewModel
            {
                Section = await _db.IletisimSections.FirstAsync(),
                ContactDetails = await _db.ContactDetails.OrderBy(c => c.SortOrder).ToListAsync()
            }
        };
    }
}
