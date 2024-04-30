namespace Telco.Models;

public class Abonent
{
    public int AbonentId { get; init; }
    public string? FullName { get; init; }
    public string? HomePhoneNumber { get; init; }
    public string? WorkPhoneNumber { get; init; }
    public string? MobilePhoneNumber { get; init; }
    public Address? Address { get; init; }
    public List<PhoneNumber> PhoneNumbers { get; set; }
}