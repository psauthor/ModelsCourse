namespace JurisTempus.Data.Entities
{
  public class Address
  {
    public required string Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? Address3 { get; set; }
    public required string CityTown { get; set; }
    public required string StateProvince { get; set; }
    public required string PostalCode { get; set; }
    public string? Country { get; set; }
  }
}