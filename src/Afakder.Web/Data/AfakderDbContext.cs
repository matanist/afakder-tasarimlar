using Afakder.Web.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Afakder.Web.Data;

public class AfakderDbContext : IdentityDbContext<ApplicationUser>
{
    public AfakderDbContext(DbContextOptions<AfakderDbContext> options) : base(options) { }

    // Site
    public DbSet<SiteSetting> SiteSettings => Set<SiteSetting>();
    public DbSet<NavLink> NavLinks => Set<NavLink>();
    public DbSet<SocialLink> SocialLinks => Set<SocialLink>();
    public DbSet<FooterLink> FooterLinks => Set<FooterLink>();

    // Sections
    public DbSet<HeroSection> HeroSections => Set<HeroSection>();
    public DbSet<HeroStat> HeroStats => Set<HeroStat>();
    public DbSet<HikayeSection> HikayeSections => Set<HikayeSection>();
    public DbSet<TimelineCard> TimelineCards => Set<TimelineCard>();
    public DbSet<EtkiSection> EtkiSections => Set<EtkiSection>();
    public DbSet<StoryCard> StoryCards => Set<StoryCard>();
    public DbSet<NumberBox> NumberBoxes => Set<NumberBox>();
    public DbSet<FaaliyetlerSection> FaaliyetlerSections => Set<FaaliyetlerSection>();
    public DbSet<ActivityCard> ActivityCards => Set<ActivityCard>();
    public DbSet<BagisSection> BagisSections => Set<BagisSection>();
    public DbSet<ImpactCard> ImpactCards => Set<ImpactCard>();
    public DbSet<GonulluSection> GonulluSections => Set<GonulluSection>();
    public DbSet<VolunteerBenefit> VolunteerBenefits => Set<VolunteerBenefit>();
    public DbSet<IletisimSection> IletisimSections => Set<IletisimSection>();
    public DbSet<ContactDetail> ContactDetails => Set<ContactDetail>();

    // Forms
    public DbSet<VolunteerApplication> VolunteerApplications => Set<VolunteerApplication>();
    public DbSet<ContactMessage> ContactMessages => Set<ContactMessage>();

    // Blog
    public DbSet<BlogPost> BlogPosts => Set<BlogPost>();
    public DbSet<BlogCategory> BlogCategories => Set<BlogCategory>();
    public DbSet<BlogTag> BlogTags => Set<BlogTag>();
    public DbSet<BlogPostTag> BlogPostTags => Set<BlogPostTag>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // BlogPostTag composite key
        builder.Entity<BlogPostTag>()
            .HasKey(pt => new { pt.BlogPostId, pt.BlogTagId });

        builder.Entity<BlogPostTag>()
            .HasOne(pt => pt.BlogPost)
            .WithMany(p => p.PostTags)
            .HasForeignKey(pt => pt.BlogPostId);

        builder.Entity<BlogPostTag>()
            .HasOne(pt => pt.BlogTag)
            .WithMany(t => t.PostTags)
            .HasForeignKey(pt => pt.BlogTagId);

        // Unique indexes
        builder.Entity<BlogPost>()
            .HasIndex(p => p.Slug)
            .IsUnique();

        builder.Entity<BlogCategory>()
            .HasIndex(c => c.Slug)
            .IsUnique();

        builder.Entity<BlogTag>()
            .HasIndex(t => t.Slug)
            .IsUnique();

        // BlogPost -> Author
        builder.Entity<BlogPost>()
            .HasOne(p => p.Author)
            .WithMany(u => u.BlogPosts)
            .HasForeignKey(p => p.AuthorId);

        // BlogPost -> Category
        builder.Entity<BlogPost>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Posts)
            .HasForeignKey(p => p.CategoryId);
    }
}
