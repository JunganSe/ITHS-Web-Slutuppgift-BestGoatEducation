using System.ComponentModel.DataAnnotations;

namespace WestcoastEducationApi.Models;

public class Address
{
    public int Id { get; set; }
    
    public Street? Street { get; set; }
    public int StreetId { get; set; }

    [Required]
    public string? StreetNumber { get; set; }
    
    public PostalCode? PostalCode { get; set; }
    public int PostalCodeId { get; set; }
    
    public City? City { get; set; }
    public int CityId { get; set; }
    
    public Country? Country { get; set; }
    public int CountryId { get; set; }
    
    
    
    public ICollection<Student> Students { get; set; } = new List<Student>();
    public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}