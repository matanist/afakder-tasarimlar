using Microsoft.AspNetCore.Identity;

namespace Afakder.Web.Models.Entities;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
}
