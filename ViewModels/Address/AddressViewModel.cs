namespace WestcoastEducationApi.ViewModels.Address;

public class AddressViewModel
{
    public int Id { get; set; }
    public string? Street { get; set; }
    public string? StreetNumber { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
}