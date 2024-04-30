namespace Telco.Models;

public class PhoneNumber
{
    public int PhoneNumberId { get; init; }
    public int AbonentId { get; init; }
    public string? NumberType { get; init; }
    public string? Number { get; init; }
}