using System.ComponentModel.DataAnnotations;

namespace WestcoastEducationApi.Models;

public class City
{
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }
    
    
    
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
}