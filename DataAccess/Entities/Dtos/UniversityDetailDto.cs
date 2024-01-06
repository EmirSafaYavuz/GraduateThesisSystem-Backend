using Core.Entities;

namespace DataAccess.Entities.Dtos;

public class UniversityDetailDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public int LocationId { get; set; }
    public string City { get; set; } = null!;

    public string Country { get; set; } = null!;
    public IEnumerable<Institute> Institutes { get; set; } = null!;

}