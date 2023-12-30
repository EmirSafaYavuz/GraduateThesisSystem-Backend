using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;
using DataAccess.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework;

public class EfAuthorDal : EfEntityRepositoryBase<Author, MyDbContext>, IAuthorDal
{
    private readonly MyDbContext _context;

    public EfAuthorDal(MyDbContext context)
    {
        _context = context;
    }

    public IEnumerable<ThesisDetailDto> GetThesesByAuthorId(int id)
    {
        var result = _context.Theses
            .Include(t => t.Author)
            .Include(t => t.Language)
            .Include(t => t.CoSupervisor)
            .Include(t => t.Institute)
            .Include(t => t.Supervisor)
            .Where(t => t.AuthorId == id)
            .Select(t => new ThesisDetailDto
            {
                Id = t.Id,
                ThesisNo = t.ThesisNo,
                Title = t.Title,
                Abstract = t.Abstract,
                Year = t.Year,
                NumOfPages = t.NumOfPages,
                SubmissionDate = t.SubmissionDate,
                AuthorId = t.AuthorId,
                AuthorName = t.Author.Name,
                LanguageId = t.LanguageId,
                LanguageName = t.Language.Name,
                CoSupervisorId = t.CoSupervisorId,
                CoSupervisorName = t.CoSupervisor.Name,
                InstituteId = t.InstituteId,
                InstituteName = t.Institute.Name,
                SupervisorId = t.SupervisorId,
                SupervisorName = t.Supervisor.Name,
                ThesisType = t.ThesisType.GetThesisTypeAsString()
            });

        return result;
    }
}