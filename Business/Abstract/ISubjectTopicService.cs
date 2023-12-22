using Core.Utilities.Results;
using DataAccess.Entities;

namespace Business.Abstract;

public interface ISubjectTopicService
{
    IDataResult<IEnumerable<SubjectTopic>> GetAll();
    IDataResult<SubjectTopic> Add(SubjectTopic subjectTopic);
    IResult Delete(int id);
}