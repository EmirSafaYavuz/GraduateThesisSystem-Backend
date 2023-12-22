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
        return new SuccessDataResult<IEnumerable<SubjectTopic>>(_subjectTopicDal.GetList());
    }

    public IDataResult<SubjectTopic> Add(SubjectTopic subjectTopic)
    {
        var addedSubjectTopic = _subjectTopicDal.Add(subjectTopic);
        _subjectTopicDal.SaveChanges();
        return new SuccessDataResult<SubjectTopic>(addedSubjectTopic);
    }

    public IResult Delete(int id)
    {
        var subjectTopic = _subjectTopicDal.Get(i => i.Id == id);
        if (subjectTopic is null)
        {
            return new ErrorResult("SubjectTopic not found");
        }

        _subjectTopicDal.Delete(subjectTopic);
        _subjectTopicDal.SaveChanges();
        return new SuccessResult();
    }
}