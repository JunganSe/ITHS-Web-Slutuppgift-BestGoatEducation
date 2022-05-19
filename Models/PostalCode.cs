using System.ComponentModel.DataAnnotations;

namespace WestcoastEducationApi.Models;

public class PostalCode
{
    public int Id { get; set; }

    [Required]
    public string? Code { get; set; }
    
    

    public ICollection<Address> Addresses { get; set; } = new List<Address>();
}