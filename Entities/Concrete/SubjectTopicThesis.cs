using Core.Entities;

namespace Entities.Concrete;

public class SubjectTopicThesis : IEntity
{
    public int Id { get; set; }
    public int SubjectTopicId { get; set; }
    public int ThesisId { get; set; }
}