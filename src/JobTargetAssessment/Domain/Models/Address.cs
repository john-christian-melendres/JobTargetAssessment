namespace JobTargetAssessment.Domain;

public class Address
{
    public required string Street { get; set; }
    public required string Suite { get; set; }
    public required string City { get; set; }
    public required Geo Geo { get; set; }

}

public class Geo
{
    public required string Lat { get; set; }
    public required string Lng { get; set; }
}
