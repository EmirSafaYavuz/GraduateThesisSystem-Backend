using Core.Utilities.Results;
using DataAccess.Entities;

namespace Business.Abstract;

public interface ISubjectTopicsThesisService
{
    IDataResult<IEnumerable<SubjectTopicsThesis>> GetAll();
    IDataResult<SubjectTopicsThesis> Add(SubjectTopicsThesis subjectTopicsThesis);
    IResult Delete(int id);
}