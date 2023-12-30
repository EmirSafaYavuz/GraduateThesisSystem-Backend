using Core.Entities;

namespace DataAccess.Entities.Dtos;

public class SupervisorDetailDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
}