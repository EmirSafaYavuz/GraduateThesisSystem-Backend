using Core.Entities;

namespace Entities.Concrete;

public class SupervisorThesis : IEntity
{
    public int Id { get; set; }
    public int SupervisorId { get; set; }
    public int ThesisId { get; set; }
}