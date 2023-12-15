using Core.Entities;

namespace Entities.Concrete;

public class Institute : IEntity
{
    public int Id { get; set; }
    public int UniversityId { get; set; }
    public string Name { get; set; }
}