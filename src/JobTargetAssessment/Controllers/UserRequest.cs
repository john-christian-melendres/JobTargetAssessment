using System.ComponentModel.DataAnnotations;

using JobTargetAssessment.Domain;

namespace JobTargetAssessment.Presentation;

public class UserRequest
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? UserName { get; set; }
    [Required]
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Website { get; set; }

    public User ToDomain()
    {
        return new User()
        {
            Id = Id,
            Name = Name ?? string.Empty,
            UserName = UserName ?? string.Empty,
            Email = Email ?? string.Empty,
            Phone = Phone ?? string.Empty,
            Website = Website ?? string.Empty,
            Address = new Domain.Address()
            {
                City = string.Empty,
                Street= string.Empty,
                Suite= string.Empty,
                Geo = new Domain.Geo()
                {
                    Lat = string.Empty,
                    Lng = string.Empty
                },
            },
            Company = new Domain.Company()
            {
                Name = string.Empty,
                CatchPhrase = string.Empty,
                BS = string.Empty
            }
        };
    }
}

public class Address
{
    public string? Street { get; set; }
    public string? Suite { get; set; }
    public string? City { get; set; }
    public Geo? Geo { get; set; }

}

public class Geo
{
    public string? Lat { get; set; }
    public string? Lng { get; set; }
}

public class Company
{
    public string? Name { get; set; }
    public string? CatchPhrase { get; set; }
    public string? BS { get; set; }
}


