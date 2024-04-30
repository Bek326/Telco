namespace Telco.Models;

public class Address
{
    public int AddressId { get; init; }
    public int AbonentId { get; init; }
    public int StreetId { get; init; }
    public string? HouseNumber { get; init; }
    public Street? Street { get; init; }
}