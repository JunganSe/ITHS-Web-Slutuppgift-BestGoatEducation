namespace WestcoastEducationApi.Models;

public class PostalCode
{
    public int Id { get; set; }
    public string? Code { get; set; }

    public ICollection<Address> Addresses { get; set; } = new List<Address>();
}