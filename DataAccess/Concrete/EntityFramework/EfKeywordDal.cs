using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;
using DataAccess.Entities.Enums;

namespace DataAccess.Concrete.EntityFramework;

public class EfKeywordDal : EfEntityRepositoryBase<Keyword, MyDbContext>, IKeywordDal
{
    private readonly MyDbContext _context;

    public EfKeywordDal(MyDbContext context)
    {
        _context = context;
    }

    public IEnumerable<ThesisDetailDto> GetThesesByKeywordId(int id)
    {
        var result = from keyword in _context.Keywords
            join keywordsTheses in _context.KeywordsTheses on keyword.Id equals keywordsTheses.KeywordId
            join thesis in _context.Theses on keywordsTheses.ThesisId equals thesis.Id
            join author in _context.Authors on thesis.AuthorId equals author.Id
            join language in _context.Languages on thesis.LanguageId equals language.Id
            join institute in _context.Institutes on thesis.InstituteId equals institute.Id
            where keyword.Id == id
            select new ThesisDetailDto
            {
                Id = thesis.Id,
                ThesisNo = thesis.ThesisNo,
                Title = thesis.Title,
                Abstract = thesis.Abstract,
                Year = thesis.Year,
                NumOfPages = thesis.NumOfPages,
                SubmissionDate = thesis.SubmissionDate,
                AuthorId = thesis.AuthorId,
                AuthorName = author.Name,
                LanguageId = thesis.LanguageId,
                LanguageName = language.Name,
                InstituteId = thesis.InstituteId,
                InstituteName = institute.Name,
                SupervisorId = thesis.SupervisorId,
                SupervisorName = thesis.Supervisor.Name,
                ThesisType = thesis.ThesisType.GetThesisTypeAsString()
            };

        return result;
    }
}