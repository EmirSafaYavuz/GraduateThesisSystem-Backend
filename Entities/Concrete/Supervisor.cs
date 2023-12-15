using Core.Entities;

namespace Entities.Concrete;

public class Supervisor : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
}