using Afakder.Web.Models.Entities;

namespace Afakder.Web.Models.ViewModels;

public class HomePageViewModel
{
    public SiteSetting SiteSettings { get; set; } = null!;
    public HeroViewModel Hero { get; set; } = null!;
    public HikayeViewModel Hikaye { get; set; } = null!;
    public EtkiViewModel Etki { get; set; } = null!;
    public List<NumberBox> Rakamlar { get; set; } = new();
    public FaaliyetlerViewModel Faaliyetler { get; set; } = null!;
    public BagisViewModel Bagis { get; set; } = null!;
    public GonulluViewModel Gonullu { get; set; } = null!;
    public IletisimViewModel Iletisim { get; set; } = null!;
    public List<NavLink> NavLinks { get; set; } = new();
    public List<SocialLink> SocialLinks { get; set; } = new();
    public List<FooterLink> FooterLinks { get; set; } = new();
}

public class HeroViewModel
{
    public HeroSection Section { get; set; } = null!;
    public List<HeroStat> Stats { get; set; } = new();
}

public class HikayeViewModel
{
    public HikayeSection Section { get; set; } = null!;
    public List<TimelineCard> TimelineCards { get; set; } = new();
}

public class EtkiViewModel
{
    public EtkiSection Section { get; set; } = null!;
    public List<StoryCard> StoryCards { get; set; } = new();
}

public class FaaliyetlerViewModel
{
    public FaaliyetlerSection Section { get; set; } = null!;
    public List<ActivityCard> ActivityCards { get; set; } = new();
}

public class BagisViewModel
{
    public BagisSection Section { get; set; } = null!;
    public List<ImpactCard> ImpactCards { get; set; } = new();
}

public class GonulluViewModel
{
    public GonulluSection Section { get; set; } = null!;
    public List<VolunteerBenefit> Benefits { get; set; } = new();
}

public class IletisimViewModel
{
    public IletisimSection Section { get; set; } = null!;
    public List<ContactDetail> ContactDetails { get; set; } = new();
}
