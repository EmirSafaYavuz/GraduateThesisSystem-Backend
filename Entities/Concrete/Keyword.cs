using Core.Entities;

namespace Entities.Concrete;

public class Keyword : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
}