using Core.Entities;

namespace Entities.Concrete;

public class KeywordThesis : IEntity
{
    public int Id { get; set; }
    public int KeywordId { get; set; }
    public int ThesisId { get; set; }
}