using Afakder.Web.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Afakder.Web.Data.Seed;

public class DataSeeder
{
    private readonly IServiceProvider _services;

    public DataSeeder(IServiceProvider services)
    {
        _services = services;
    }

    public async Task SeedAsync()
    {
        var db = _services.GetRequiredService<AfakderDbContext>();
        await db.Database.MigrateAsync();

        if (await db.SiteSettings.AnyAsync())
            return; // Already seeded

        await SeedRolesAndAdminAsync();
        await SeedSiteSettingsAsync(db);
        await SeedNavLinksAsync(db);
        await SeedHeroAsync(db);
        await SeedHikayeAsync(db);
        await SeedEtkiAsync(db);
        await SeedRakamlarAsync(db);
        await SeedFaaliyetlerAsync(db);
        await SeedBagisAsync(db);
        await SeedGonulluAsync(db);
        await SeedIletisimAsync(db);
        await SeedFooterAsync(db);
        await SeedSocialLinksAsync(db);
        await SeedBlogCategoriesAsync(db);

        await db.SaveChangesAsync();
    }

    private async Task SeedRolesAndAdminAsync()
    {
        var roleManager = _services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = _services.GetRequiredService<UserManager<ApplicationUser>>();

        string[] roles = ["Admin", "Editor"];
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        if (await userManager.FindByEmailAsync("admin@afakder.org") == null)
        {
            var admin = new ApplicationUser
            {
                UserName = "admin@afakder.org",
                Email = "admin@afakder.org",
                FullName = "Admin",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(admin, "Afakder2024!");
            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }

    private static async Task SeedSiteSettingsAsync(AfakderDbContext db)
    {
        db.SiteSettings.Add(new SiteSetting
        {
            LogoText = "AFAKDER",
            LogoSvg = @"<svg viewBox=""0 0 40 40"" fill=""none"" xmlns=""http://www.w3.org/2000/svg""><circle cx=""20"" cy=""20"" r=""18"" stroke=""currentColor"" stroke-width=""2""/><path d=""M20 8 L20 32 M12 16 L20 8 L28 16"" stroke=""currentColor"" stroke-width=""2.5"" stroke-linecap=""round"" stroke-linejoin=""round""/><path d=""M10 28 Q20 22 30 28"" stroke=""currentColor"" stroke-width=""2"" stroke-linecap=""round""/></svg>",
            FooterTagline = "Afet Arama Kurtarma Derneği",
            FooterMotto = "Umut hiç bitmez.",
            PreloaderText = "umut yükleniyor...",
            EmergencyNumber = "112",
            EmergencyText = "Acil Durum Hattı",
            Copyright = "© 2026 AFAKDER — Afet Arama Kurtarma Derneği. Tüm hakları saklıdır.",
            DefaultPageTitle = "AFAKDER — Afet Arama Kurtarma Derneği",
            DefaultMetaDescription = "AFAKDER - Afet Arama Kurtarma Derneği. Karanlığın içinde bir ışık oluyoruz. Her afette umut, her enkaz altında hayat.",
            NavCtaText = "Destek Ol",
            NavCtaUrl = "#bagis"
        });
    }

    private static async Task SeedNavLinksAsync(AfakderDbContext db)
    {
        db.NavLinks.AddRange(
            new NavLink { Text = "Hikayemiz", Url = "#hikaye", SortOrder = 1 },
            new NavLink { Text = "Etkimiz", Url = "#etki", SortOrder = 2 },
            new NavLink { Text = "Ne Yapıyoruz", Url = "#faaliyetler", SortOrder = 3 },
            new NavLink { Text = "Bağış", Url = "#bagis", SortOrder = 4 },
            new NavLink { Text = "Gönüllü Ol", Url = "#gonullu", SortOrder = 5 },
            new NavLink { Text = "İletişim", Url = "#iletisim", SortOrder = 6 }
        );
    }

    private static async Task SeedHeroAsync(AfakderDbContext db)
    {
        db.HeroSections.Add(new HeroSection
        {
            TitleLine1 = "Karanlığın İçinde",
            TitleHighlight = "Bir Işık",
            Subtitle = "Her afet bir karanlık getirir.<br>Biz o karanlıkta <em>umut</em> oluyoruz.",
            PrimaryCtaText = "Hayat Kurtar",
            PrimaryCtaUrl = "#bagis",
            SecondaryCtaText = "Hikayemizi Oku",
            SecondaryCtaUrl = "#hikaye"
        });

        db.HeroStats.AddRange(
            new HeroStat { Value = "2.847", Label = "Kurtarılan Can", SortOrder = 1 },
            new HeroStat { Value = "43", Label = "Operasyon", SortOrder = 2 },
            new HeroStat { Value = "18", Label = "İlde Faaliyet", SortOrder = 3 },
            new HeroStat { Value = "156", Label = "Gönüllü", SortOrder = 4 }
        );
    }

    private static async Task SeedHikayeAsync(AfakderDbContext db)
    {
        db.HikayeSections.Add(new HikayeSection
        {
            SectionTag = "Her Saniye Hayat",
            MegaNumber = "72",
            MegaUnit = "saat",
            Subtitle = "Altın saatler. Her saniye bir hayat. Afet sonrası ilk 72 saat, hayatta kalma şansının en yüksek olduğu zamandır."
        });

        db.TimelineCards.AddRange(
            new TimelineCard
            {
                HourRange = "0-24", HourLabel = "saat", Title = "Kritik Müdahale",
                Description = "Ekipler sahaya ulaşır. İlk temas, ilk umut. Enkazın altından ilk sesler duyulur. Her saniye altın değerinde.",
                SvgIcon = @"<svg viewBox=""0 0 24 24"" fill=""none"" stroke=""currentColor"" stroke-width=""1.5""><path d=""M21 16V8a2 2 0 0 0-1-1.73l-7-4a2 2 0 0 0-2 0l-7 4A2 2 0 0 0 3 8v8a2 2 0 0 0 1 1.73l7 4a2 2 0 0 0 2 0l7-4A2 2 0 0 0 21 16z""/><polyline points=""3.27 6.96 12 12.01 20.73 6.96""/><line x1=""12"" y1=""22.08"" x2=""12"" y2=""12""/></svg>",
                SortOrder = 1
            },
            new TimelineCard
            {
                HourRange = "24-48", HourLabel = "saat", Title = "Yaşam Mücadelesi",
                Description = "Kurtarma operasyonları yoğunlaşır. Enkaz altındaki insanlara su ve hava ulaştırılır. Umut azalmaz, çaba artar.",
                SvgIcon = @"<svg viewBox=""0 0 24 24"" fill=""none"" stroke=""currentColor"" stroke-width=""1.5""><path d=""M20.84 4.61a5.5 5.5 0 0 0-7.78 0L12 5.67l-1.06-1.06a5.5 5.5 0 0 0-7.78 7.78l1.06 1.06L12 21.23l7.78-7.78 1.06-1.06a5.5 5.5 0 0 0 0-7.78z""/></svg>",
                SortOrder = 2
            },
            new TimelineCard
            {
                HourRange = "48-72", HourLabel = "saat", Title = "Son Şans",
                Description = "En kritik eşik. İnsan bedeni sınırlarına ulaşır ama mucizeler bu saatlerde gerçekleşir. Vazgeçmek yok.",
                SvgIcon = @"<svg viewBox=""0 0 24 24"" fill=""none"" stroke=""currentColor"" stroke-width=""1.5""><circle cx=""12"" cy=""12"" r=""10""/><polyline points=""12 6 12 12 16 14""/></svg>",
                SortOrder = 3
            }
        );
    }

    private static async Task SeedEtkiAsync(AfakderDbContext db)
    {
        db.EtkiSections.Add(new EtkiSection
        {
            SectionTag = "Gerçek Hikayeler",
            Title = "Her Rakamın Arkasında",
            TitleHighlight = "Bir İnsan",
            Description = "Bu isimler istatistik değil. Her biri bir anne, bir baba, bir çocuk. Her biri bizim için dünyaya değer."
        });

        db.StoryCards.AddRange(
            new StoryCard
            {
                Name = "Ayşe, 8 yaşında", Location = "Hatay",
                Quote = "14 saat sonra enkazdan sağ çıkarıldı. İlk sözü <em>'Annem nerede?'</em> oldu. Bugün annesiyle birlikte yeni hayatına devam ediyor.",
                AvatarCssClass = "avatar-1", SortOrder = 1
            },
            new StoryCard
            {
                Name = "Mehmet Amca, 67 yaşında", Location = "Adıyaman",
                Quote = "Komşularıyla birlikte kurtarıldı. <em>'Beni bırakın, önce çocukları çıkarın'</em> diyordu. Hepsini kurtardık. Hepsini.",
                AvatarCssClass = "avatar-2", SortOrder = 2
            },
            new StoryCard
            {
                Name = "Elif bebek, 11 aylık", Location = "Kahramanmaraş",
                Quote = "Mucize kurtuluş. 26 saat sonra annesinin kollarında bulundu. Ekibimiz o gün <em>sessizce ağladı</em>. Mutluluktan.",
                AvatarCssClass = "avatar-3", SortOrder = 3
            }
        );
    }

    private static async Task SeedRakamlarAsync(AfakderDbContext db)
    {
        db.NumberBoxes.AddRange(
            new NumberBox
            {
                TargetValue = 2847, Label = "Kurtarılan Can",
                SvgIcon = @"<svg viewBox=""0 0 24 24"" fill=""none"" stroke=""currentColor"" stroke-width=""1.5""><path d=""M20.84 4.61a5.5 5.5 0 0 0-7.78 0L12 5.67l-1.06-1.06a5.5 5.5 0 0 0-7.78 7.78l1.06 1.06L12 21.23l7.78-7.78 1.06-1.06a5.5 5.5 0 0 0 0-7.78z""/></svg>",
                SortOrder = 1
            },
            new NumberBox
            {
                TargetValue = 156, Label = "Gönüllü",
                SvgIcon = @"<svg viewBox=""0 0 24 24"" fill=""none"" stroke=""currentColor"" stroke-width=""1.5""><path d=""M17 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2""/><circle cx=""9"" cy=""7"" r=""4""/><path d=""M23 21v-2a4 4 0 0 0-3-3.87""/><path d=""M16 3.13a4 4 0 0 1 0 7.75""/></svg>",
                SortOrder = 2
            },
            new NumberBox
            {
                TargetValue = 43, Label = "Operasyon",
                SvgIcon = @"<svg viewBox=""0 0 24 24"" fill=""none"" stroke=""currentColor"" stroke-width=""1.5""><polygon points=""13 2 3 14 12 14 11 22 21 10 12 10 13 2""/></svg>",
                SortOrder = 3
            },
            new NumberBox
            {
                TargetValue = 18, Label = "İlde Faaliyet",
                SvgIcon = @"<svg viewBox=""0 0 24 24"" fill=""none"" stroke=""currentColor"" stroke-width=""1.5""><path d=""M21 10c0 7-9 13-9 13s-9-6-9-13a9 9 0 0 1 18 0z""/><circle cx=""12"" cy=""10"" r=""3""/></svg>",
                SortOrder = 4
            }
        );
    }

    private static async Task SeedFaaliyetlerAsync(AfakderDbContext db)
    {
        db.FaaliyetlerSections.Add(new FaaliyetlerSection
        {
            SectionTag = "Uzmanlık Alanlarımız",
            Title = "Ne Yapıyoruz?",
            Description = "Her afetin farklı ihtiyaçları var. Biz her birine hazırız."
        });

        db.ActivityCards.AddRange(
            new ActivityCard
            {
                Title = "Arama Kurtarma",
                Description = "Profesyonel eğitimli ekiplerimizle afet bölgelerinde hayat kurtarıyoruz. 7/24 hazırız, her an her yere.",
                SvgIcon = @"<svg viewBox=""0 0 24 24"" fill=""none"" stroke=""currentColor"" stroke-width=""1.5""><path d=""M16 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2""/><circle cx=""8.5"" cy=""7"" r=""4""/><line x1=""20"" y1=""8"" x2=""20"" y2=""14""/><line x1=""23"" y1=""11"" x2=""17"" y2=""11""/></svg>",
                SortOrder = 1
            },
            new ActivityCard
            {
                Title = "Eğitim",
                Description = "Afet bilinci ve ilk yardım eğitimleri veriyoruz. Toplumu afetlere hazırlıyoruz. Bilgi hayat kurtarır.",
                SvgIcon = @"<svg viewBox=""0 0 24 24"" fill=""none"" stroke=""currentColor"" stroke-width=""1.5""><path d=""M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z""/><polyline points=""14 2 14 8 20 8""/><line x1=""16"" y1=""13"" x2=""8"" y2=""13""/><line x1=""16"" y1=""17"" x2=""8"" y2=""17""/></svg>",
                SortOrder = 2
            },
            new ActivityCard
            {
                Title = "Lojistik",
                Description = "Yardım malzemelerini hızla ve etkin şekilde ihtiyaç sahiplerine ulaştırıyoruz. Koordinasyon hayat kurtarır.",
                SvgIcon = @"<svg viewBox=""0 0 24 24"" fill=""none"" stroke=""currentColor"" stroke-width=""1.5""><rect x=""1"" y=""3"" width=""15"" height=""13""/><polygon points=""16 8 20 8 23 11 23 16 16 16 16 8""/><circle cx=""5.5"" cy=""18.5"" r=""2.5""/><circle cx=""18.5"" cy=""18.5"" r=""2.5""/></svg>",
                SortOrder = 3
            },
            new ActivityCard
            {
                Title = "Psikososyal Destek",
                Description = "Afetin görünmeyen yaralarını sarıyoruz. Travma sonrası destek ile kalpleri iyileştiriyoruz.",
                SvgIcon = @"<svg viewBox=""0 0 24 24"" fill=""none"" stroke=""currentColor"" stroke-width=""1.5""><path d=""M20.84 4.61a5.5 5.5 0 0 0-7.78 0L12 5.67l-1.06-1.06a5.5 5.5 0 0 0-7.78 7.78l1.06 1.06L12 21.23l7.78-7.78 1.06-1.06a5.5 5.5 0 0 0 0-7.78z""/><path d=""M16 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2""/></svg>",
                SortOrder = 4
            }
        );
    }

    private static async Task SeedBagisAsync(AfakderDbContext db)
    {
        db.BagisSections.Add(new BagisSection
        {
            SectionTag = "Senin Gücün",
            Title = "Sen Olmasaydın,",
            TitleHighlight = "Bu Hikayeler Yazılamazdı",
            Description = "Her bağışın bir hayata dokunuyor. Küçük ya da büyük, fark etmez. Önemli olan senin de bu hikayenin bir parçası olman.",
            FooterNote = "Her kuruş hayat kurtarır. Bağışlarınız vergiden düşülebilir.",
            CampaignTitle = "Kış Kampanyası",
            CampaignTarget = "Hedef: 100.000₺",
            CampaignProgress = 73,
            CampaignAmount = "73.000₺ toplandı",
            CampaignNote = "Hedefe ulaşmamıza çok az kaldı. Sen de katkıda bulun!"
        });

        db.ImpactCards.AddRange(
            new ImpactCard { Amount = 25, Description = "Bir kurtarma köpeğinin<br><strong>bir günlük maması</strong>", SortOrder = 1 },
            new ImpactCard { Amount = 50, Description = "Bir aileye<br><strong>sıcak battaniye</strong>", SortOrder = 2 },
            new ImpactCard { Amount = 100, Description = "Bir gönüllünün<br><strong>sahaya ulaşım masrafı</strong>", SortOrder = 3 },
            new ImpactCard { Amount = 250, Description = "Bir aileye<br><strong>geçici barınak çadırı</strong>", IsFeatured = true, FeaturedBadgeText = "En Çok Tercih Edilen", SortOrder = 4 },
            new ImpactCard { Amount = 500, Description = "Bir haftalık<br><strong>gıda paketi</strong>", SortOrder = 5 },
            new ImpactCard { Amount = 1000, Description = "Profesyonel<br><strong>arama kurtarma ekipmanı</strong>", SortOrder = 6 }
        );
    }

    private static async Task SeedGonulluAsync(AfakderDbContext db)
    {
        db.GonulluSections.Add(new GonulluSection
        {
            SectionTag = "Ekibimize Katıl",
            Title = "Birlikte",
            TitleHighlight = "Daha Güçlüyüz",
            Description = "Hayat kurtarmak için süper kahraman olmana gerek yok. Sadece iyi bir insan olman yeterli.",
            FormTitle = "Gönüllü Başvurusu",
            SubmitButtonText = "Başvurumu Gönder"
        });

        db.VolunteerBenefits.AddRange(
            new VolunteerBenefit { Text = "Profesyonel arama kurtarma eğitimi", SortOrder = 1 },
            new VolunteerBenefit { Text = "İlk yardım ve afet bilinci sertifikası", SortOrder = 2 },
            new VolunteerBenefit { Text = "Hayat kurtaran bir topluluğun parçası ol", SortOrder = 3 },
            new VolunteerBenefit { Text = "Kişisel gelişim ve liderlik deneyimi", SortOrder = 4 }
        );
    }

    private static async Task SeedIletisimAsync(AfakderDbContext db)
    {
        db.IletisimSections.Add(new IletisimSection
        {
            SectionTag = "Bize Ulaşın",
            Title = "İletişim",
            Description = "Sorularınız, önerileriniz veya işbirliği talepleriniz için bize yazın. Her mesajınızı okuyoruz.",
            FormTitle = "Mesaj Gönderin",
            SubmitButtonText = "Gönder"
        });

        db.ContactDetails.AddRange(
            new ContactDetail
            {
                Label = "Adres", Value = "Atatürk Cad. No:42/A, Çankaya, Ankara",
                SvgIcon = @"<svg viewBox=""0 0 24 24"" fill=""none"" stroke=""currentColor"" stroke-width=""1.5""><path d=""M21 10c0 7-9 13-9 13s-9-6-9-13a9 9 0 0 1 18 0z""/><circle cx=""12"" cy=""10"" r=""3""/></svg>",
                SortOrder = 1
            },
            new ContactDetail
            {
                Label = "Telefon", Value = "+90 (312) 555 42 42",
                SvgIcon = @"<svg viewBox=""0 0 24 24"" fill=""none"" stroke=""currentColor"" stroke-width=""1.5""><path d=""M22 16.92v3a2 2 0 0 1-2.18 2 19.79 19.79 0 0 1-8.63-3.07 19.5 19.5 0 0 1-6-6 19.79 19.79 0 0 1-3.07-8.67A2 2 0 0 1 4.11 2h3a2 2 0 0 1 2 1.72 12.84 12.84 0 0 0 .7 2.81 2 2 0 0 1-.45 2.11L8.09 9.91a16 16 0 0 0 6 6l1.27-1.27a2 2 0 0 1 2.11-.45 12.84 12.84 0 0 0 2.81.7A2 2 0 0 1 22 16.92z""/></svg>",
                SortOrder = 2
            },
            new ContactDetail
            {
                Label = "E-posta", Value = "info@afakder.org",
                SvgIcon = @"<svg viewBox=""0 0 24 24"" fill=""none"" stroke=""currentColor"" stroke-width=""1.5""><path d=""M4 4h16c1.1 0 2 .9 2 2v12c0 1.1-.9 2-2 2H4c-1.1 0-2-.9-2-2V6c0-1.1.9-2 2-2z""/><polyline points=""22,6 12,13 2,6""/></svg>",
                SortOrder = 3
            }
        );
    }

    private static async Task SeedFooterAsync(AfakderDbContext db)
    {
        db.FooterLinks.AddRange(
            new FooterLink { Text = "Hikayemiz", Url = "#hikaye", SortOrder = 1 },
            new FooterLink { Text = "Etkimiz", Url = "#etki", SortOrder = 2 },
            new FooterLink { Text = "Ne Yapıyoruz", Url = "#faaliyetler", SortOrder = 3 },
            new FooterLink { Text = "Bağış Yap", Url = "#bagis", SortOrder = 4 },
            new FooterLink { Text = "Gönüllü Ol", Url = "#gonullu", SortOrder = 5 }
        );
    }

    private static async Task SeedSocialLinksAsync(AfakderDbContext db)
    {
        db.SocialLinks.AddRange(
            new SocialLink
            {
                Platform = "Instagram", Url = "#", AriaLabel = "Instagram",
                SvgIcon = @"<svg viewBox=""0 0 24 24"" fill=""none"" stroke=""currentColor"" stroke-width=""1.5""><rect x=""2"" y=""2"" width=""20"" height=""20"" rx=""5"" ry=""5""/><path d=""M16 11.37A4 4 0 1 1 12.63 8 4 4 0 0 1 16 11.37z""/><line x1=""17.5"" y1=""6.5"" x2=""17.51"" y2=""6.5""/></svg>",
                SortOrder = 1
            },
            new SocialLink
            {
                Platform = "Twitter", Url = "#", AriaLabel = "Twitter / X",
                SvgIcon = @"<svg viewBox=""0 0 24 24"" fill=""none"" stroke=""currentColor"" stroke-width=""1.5""><path d=""M18 2h3l-8 9.5L22 22h-7l-5-6.5L4 22H1l8.5-10L2 2h7l4.5 6z""/></svg>",
                SortOrder = 2
            },
            new SocialLink
            {
                Platform = "Facebook", Url = "#", AriaLabel = "Facebook",
                SvgIcon = @"<svg viewBox=""0 0 24 24"" fill=""none"" stroke=""currentColor"" stroke-width=""1.5""><path d=""M18 2h-3a5 5 0 0 0-5 5v3H7v4h3v8h4v-8h3l1-4h-4V7a1 1 0 0 1 1-1h3z""/></svg>",
                SortOrder = 3
            },
            new SocialLink
            {
                Platform = "YouTube", Url = "#", AriaLabel = "YouTube",
                SvgIcon = @"<svg viewBox=""0 0 24 24"" fill=""none"" stroke=""currentColor"" stroke-width=""1.5""><path d=""M22.54 6.42a2.78 2.78 0 0 0-1.94-2C18.88 4 12 4 12 4s-6.88 0-8.6.46a2.78 2.78 0 0 0-1.94 2A29 29 0 0 0 1 11.75a29 29 0 0 0 .46 5.33A2.78 2.78 0 0 0 3.4 19.13C5.12 19.56 12 19.56 12 19.56s6.88 0 8.6-.46a2.78 2.78 0 0 0 1.94-2 29 29 0 0 0 .46-5.25 29 29 0 0 0-.46-5.33z""/><polygon points=""9.75 15.02 15.5 11.75 9.75 8.48 9.75 15.02""/></svg>",
                SortOrder = 4
            }
        );
    }

    private static async Task SeedBlogCategoriesAsync(AfakderDbContext db)
    {
        db.BlogCategories.AddRange(
            new BlogCategory { Name = "Haberler", Slug = "haberler", SortOrder = 1 },
            new BlogCategory { Name = "Operasyonlar", Slug = "operasyonlar", SortOrder = 2 },
            new BlogCategory { Name = "Eğitimler", Slug = "egitimler", SortOrder = 3 },
            new BlogCategory { Name = "Duyurular", Slug = "duyurular", SortOrder = 4 }
        );
    }
}
