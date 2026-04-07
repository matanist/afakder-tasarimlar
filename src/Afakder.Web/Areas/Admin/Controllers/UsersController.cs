using System.Security.Claims;
using Afakder.Web.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Afakder.Web.Areas.Admin.Controllers;

[Area("Admin"), Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    // GET /admin/users
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Kullanıcılar";

        var users = await _userManager.Users.OrderBy(u => u.FullName).ToListAsync();
        var userRoles = new Dictionary<string, IList<string>>();

        foreach (var user in users)
        {
            userRoles[user.Id] = await _userManager.GetRolesAsync(user);
        }

        ViewBag.UserRoles = userRoles;
        return View(users);
    }

    // GET /admin/users/create
    public async Task<IActionResult> Create()
    {
        ViewData["Title"] = "Yeni Kullanıcı";

        var roles = await _roleManager.Roles.OrderBy(r => r.Name).ToListAsync();
        ViewBag.Roles = new SelectList(roles, "Name", "Name");

        return View();
    }

    // POST /admin/users/create
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(string fullName, string email, string password, string role)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FullName = fullName,
            CreatedAt = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            var roles = await _roleManager.Roles.OrderBy(r => r.Name).ToListAsync();
            ViewBag.Roles = new SelectList(roles, "Name", "Name");
            ViewData["Title"] = "Yeni Kullanıcı";
            TempData["Error"] = string.Join(" ", result.Errors.Select(e => e.Description));
            return View();
        }

        if (!string.IsNullOrEmpty(role))
        {
            await _userManager.AddToRoleAsync(user, role);
        }

        TempData["Success"] = "Kullanıcı başarıyla oluşturuldu.";
        return RedirectToAction("Index");
    }

    // POST /admin/users/delete/abc123
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string id)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (id == currentUserId)
        {
            TempData["Error"] = "Kendi hesabınızı silemezsiniz.";
            return RedirectToAction("Index");
        }

        var user = await _userManager.FindByIdAsync(id);
        if (user != null)
        {
            await _userManager.DeleteAsync(user);
        }

        TempData["Success"] = "Kullanıcı silindi.";
        return RedirectToAction("Index");
    }
}
