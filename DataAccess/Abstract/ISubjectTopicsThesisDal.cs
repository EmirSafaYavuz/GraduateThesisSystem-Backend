using Core.DataAccess;
using DataAccess.Entities;

namespace DataAccess.Abstract;

public interface ISubjectTopicsThesisDal : IEntityRepository<SubjectTopicsThesis>
{
    IEnumerable<SubjectTopic> GetSubjectTopicsByThesisId(int id);
}