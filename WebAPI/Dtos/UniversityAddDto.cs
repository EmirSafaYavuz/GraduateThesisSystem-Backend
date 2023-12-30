using Core.Entities;

namespace WebAPI.Dtos;

public class UniversityAddDto : IDto
{
    public string Name { get; set; } = null!;

    public int LocationId { get; set; }
}