using Business.Abstract;
using Core.Aspects.Autofac.Transaction;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;

namespace Business.Concrete;

public class ThesisManager : IThesisService
{
    private readonly IThesisDal _thesisDal;
    private readonly ISupervisorsThesisDal _supervisorsThesisDal;
    private readonly IKeywordsThesisDal _keywordsThesisDal;
    private readonly ISubjectTopicsThesisDal _subjectTopicsThesisDal;
    private readonly IKeywordDal _keywordDal;

    public ThesisManager(IThesisDal thesisDal, ISupervisorsThesisDal supervisorsThesisDal, ISubjectTopicsThesisDal subjectTopicsThesisDal, IKeywordsThesisDal keywordsThesisDal, IKeywordDal keywordDal)
    {
        _thesisDal = thesisDal;
        _supervisorsThesisDal = supervisorsThesisDal;
        _subjectTopicsThesisDal = subjectTopicsThesisDal;
        _keywordsThesisDal = keywordsThesisDal;
        _keywordDal = keywordDal;
    }

    public IDataResult<IEnumerable<ThesisLookupDto>> GetAll()
    {
        var theses = _thesisDal.GetAllLookupDto();
        
        return new SuccessDataResult<IEnumerable<ThesisLookupDto>>(theses);
    }

    public IDataResult<ThesisDetailDto> GetById(int id)
    {
        var thesis = _thesisDal.GetDetailDtoById(id);
        if (thesis is null)
        {
            return new ErrorDataResult<ThesisDetailDto>("Thesis not found");
        }
        
        thesis.SubjectTopics = _subjectTopicsThesisDal.GetSubjectTopicsByThesisId(id);
        thesis.Keywords = _keywordsThesisDal.GetKeywordsByThesisId(id);
        thesis.Supervisors = _supervisorsThesisDal.GetSupervisorsByThesisId(id);

        return new SuccessDataResult<ThesisDetailDto>(thesis);
    }

    public IDataResult<Thesis> Add(ThesisAddDto thesis)
    {
        var newThesis = new Thesis
        {
            ThesisNo = thesis.ThesisNo,
            Title = thesis.Title,
            Abstract = thesis.Abstract,
            Year = thesis.Year,
            NumOfPages = thesis.NumOfPages,
            AuthorId = thesis.AuthorId,
            LanguageId = thesis.LanguageId,
            CoSupervisorId = thesis.CoSupervisorId,
            InstituteId = thesis.InstituteId,
            SupervisorId = thesis.SupervisorId,
            ThesisType = thesis.ThesisType,
            SubmissionDate = DateTime.Now,
        };
        
        var addedThesis = _thesisDal.Add(newThesis);
        
        if (thesis.SupervisorIdList.Count() != 0)
        {
            // Convert SupervisorIdList to a HashSet to ensure uniqueness
            var uniqueSupervisorIdList = new HashSet<int>(thesis.SupervisorIdList);

            foreach (var supervisorId in uniqueSupervisorIdList)
            {
                _supervisorsThesisDal.Add(new SupervisorsThesis
                {
                    ThesisId = addedThesis.Id,
                    SupervisorId = supervisorId
                });
            }
        }
        
        if (thesis.SubjectTopicIdList.Count() != 0)
        {
            // Convert SubjectTopicIdList to a HashSet to ensure uniqueness
            var uniqueSubjectTopicIdList = new HashSet<int>(thesis.SubjectTopicIdList);

            foreach (var subjectTopicId in uniqueSubjectTopicIdList)
            {
                _subjectTopicsThesisDal.Add(new SubjectTopicsThesis
                {
                    ThesisId = addedThesis.Id,
                    SubjectTopicId = subjectTopicId
                });
            }
        }
        
        if (thesis.Keywords.Count() != 0)
        {
            // Convert Keywords to a HashSet to ensure uniqueness
            var uniqueKeywords = new HashSet<string>(thesis.Keywords);

            foreach (var keyword in uniqueKeywords)
            {
                var addedKeyword = _keywordDal.Add(new Keyword
                {
                    Name = keyword
                });
                
                _keywordsThesisDal.Add(new KeywordsThesis
                {
                    ThesisId = addedThesis.Id,
                    KeywordId = addedKeyword.Id
                });
            }
        }
        
        return new SuccessDataResult<Thesis>(addedThesis);
    }

    public IResult Delete(int id)
    {
        var thesis = _thesisDal.GetById(id);
        if (thesis is null)
        {
            return new ErrorResult("Thesis not found");
        }

        _thesisDal.Delete(thesis);
        return new SuccessResult();
    }

    public IDataResult<long> GetCount()
    {
        var count = _thesisDal.GetCount();
        return new SuccessDataResult<long>(count);
    }
}