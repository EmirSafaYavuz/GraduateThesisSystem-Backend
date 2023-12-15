using Core.Entities;

namespace Entities.Concrete;

public class University : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int LocationId { get; set; }
}