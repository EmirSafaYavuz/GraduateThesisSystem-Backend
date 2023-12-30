using Core.Entities;

namespace DataAccess.Entities.Dtos;

public class InstituteDetailDto : IDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int UniversityId { get; set; }
    public string UniversityName { get; set; } = null!;
}