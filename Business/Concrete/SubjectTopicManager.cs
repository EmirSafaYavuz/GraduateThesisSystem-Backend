using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;

namespace Business.Concrete;

public class SubjectTopicManager : ISubjectTopicService
{
    private readonly ISubjectTopicDal _subjectTopicDal;

    public SubjectTopicManager(ISubjectTopicDal subjectTopicDal)
    {
        _subjectTopicDal = subjectTopicDal;
    }

    public IDataResult<IEnumerable<SubjectTopic>> GetAll()
    {
        return new SuccessDataResult<IEnumerable<SubjectTopic>>(_subjectTopicDal.GetAll());
    }

    public IDataResult<SubjectTopic> Add(SubjectTopic subjectTopic)
    {
        var addedSubjectTopic = _subjectTopicDal.Add(subjectTopic);
        return new SuccessDataResult<SubjectTopic>(addedSubjectTopic);
    }

    public IResult Delete(int id)
    {
        var subjectTopic = _subjectTopicDal.GetById(id);
        if (subjectTopic is null)
        {
            return new ErrorResult("SubjectTopic not found");
        }

        _subjectTopicDal.Delete(subjectTopic);
        return new SuccessResult();
    }

    public IDataResult<long> GetCount()
    {
        var count = _subjectTopicDal.GetCount();
        return new SuccessDataResult<long>(count);
    }
}