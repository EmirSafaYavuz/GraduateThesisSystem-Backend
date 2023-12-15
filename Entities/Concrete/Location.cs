using Core.Entities;

namespace Entities.Concrete;

public class Location : IEntity
{
    public int Id { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
}