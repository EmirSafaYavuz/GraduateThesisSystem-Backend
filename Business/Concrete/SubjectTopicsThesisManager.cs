using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;

namespace Business.Concrete;

public class SubjectTopicsThesisManager : ISubjectTopicsThesisService
{
    private readonly ISubjectTopicsThesisDal _subjectTopicsThesisDal;

    public SubjectTopicsThesisManager(ISubjectTopicsThesisDal subjectTopicsThesisDal)
    {
        _subjectTopicsThesisDal = subjectTopicsThesisDal;
    }

    public IDataResult<IEnumerable<SubjectTopicsThesis>> GetAll()
    {
        return new SuccessDataResult<IEnumerable<SubjectTopicsThesis>>(_subjectTopicsThesisDal.GetAll());
    }

    public IDataResult<SubjectTopicsThesis> Add(SubjectTopicsThesis subjectTopicsThesis)
    {
        var addedSubjectTopicsThesis = _subjectTopicsThesisDal.Add(subjectTopicsThesis);
        return new SuccessDataResult<SubjectTopicsThesis>(addedSubjectTopicsThesis);
    }

    public IResult Delete(int id)
    {
        var subjectTopicsThesis = _subjectTopicsThesisDal.GetById(id);
        if (subjectTopicsThesis is null)
        {
            return new ErrorResult("SubjectTopicsThesis not found");
        }

        _subjectTopicsThesisDal.Delete(subjectTopicsThesis);
        return new SuccessResult(); 
    }
}