using System.ComponentModel.DataAnnotations;

namespace Afakder.Web.Models.ViewModels;

public class VolunteerFormViewModel
{
    [Required(ErrorMessage = "Ad alanı zorunludur")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-posta alanı zorunludur")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Telefon alanı zorunludur")]
    [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şehir alanı zorunludur")]
    public string City { get; set; } = string.Empty;

    public string? Message { get; set; }
}

public class ContactFormViewModel
{
    [Required(ErrorMessage = "Ad alanı zorunludur")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-posta alanı zorunludur")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Konu alanı zorunludur")]
    public string Subject { get; set; } = string.Empty;

    [Required(ErrorMessage = "Mesaj alanı zorunludur")]
    public string Message { get; set; } = string.Empty;
}
