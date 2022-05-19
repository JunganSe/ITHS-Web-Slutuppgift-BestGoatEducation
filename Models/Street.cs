namespace WestcoastEducationApi.Models;

public class Street
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public ICollection<Address> Addresses { get; set; } = new List<Address>();
}