using System.ComponentModel.DataAnnotations;

namespace WestcoastEducationApi.Models;

public class Address
{
    public int Id { get; set; }
    
    [Required]
    public string? Street { get; set; }

    [Required]
    public string? StreetNumber { get; set; }

    [Required]
    public string? PostalCode { get; set; }

    [Required]
    public string? City { get; set; }

    [Required]
    public string? Country { get; set; }
    
    
    
    public ICollection<AppUser> AppUsers { get; set; } = new List<AppUser>();
}