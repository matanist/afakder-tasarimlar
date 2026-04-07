using Afakder.Web.Data;
using Afakder.Web.Data.Seed;
using Afakder.Web.Models.Entities;
using Afakder.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<AfakderDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<AfakderDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/admin/giris";
    options.AccessDeniedPath = "/admin/erisim-engellendi";
    options.Cookie.Name = "Afakder.Auth";
});

// Services
builder.Services.AddScoped<IContentService, ContentService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<ISlugService, SlugService>();

// MVC
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

var app = builder.Build();

// Seed data on startup
using (var scope = app.Services.CreateScope())
{
    var seeder = new DataSeeder(scope.ServiceProvider);
    await seeder.SeedAsync();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Routes
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "blog-detail",
    pattern: "blog/{slug}",
    defaults: new { controller = "Blog", action = "Detail" });

app.MapControllerRoute(
    name: "blog-category",
    pattern: "blog/kategori/{kategori}",
    defaults: new { controller = "Blog", action = "Index" });

app.MapControllerRoute(
    name: "blog-tag",
    pattern: "blog/etiket/{etiket}",
    defaults: new { controller = "Blog", action = "Index" });

app.MapControllerRoute(
    name: "blog",
    pattern: "blog",
    defaults: new { controller = "Blog", action = "Index" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
