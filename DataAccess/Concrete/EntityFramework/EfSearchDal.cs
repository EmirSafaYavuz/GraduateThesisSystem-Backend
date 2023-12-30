using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;
using DataAccess.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework;

public class EfSearchDal : ISearchDal
{

    public IEnumerable<ThesisDetailDto> SearchThesisTitle(string query, ThesisType? thesisType)
    {
        using (var context = new MyDbContext())
        {
            var result = context.Theses
                .Include(t => t.Author)
                .Include(t => t.Language)
                .Include(t => t.CoSupervisor)
                .Include(t => t.Institute)
                .Include(t => t.Supervisor)
                .Where(t => t.Title.ToLower().Contains(query.ToLower()) &&
                            (!thesisType.HasValue || t.ThesisType == thesisType.Value))
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
                }).ToList();

            return result;
        }
    }

    public IEnumerable<ThesisDetailDto> SearchThesisAbstract(string query, ThesisType? thesisType)
    {
        using (var context = new MyDbContext())
        {
            var result = context.Theses
                .Include(t => t.Author)
                .Include(t => t.Language)
                .Include(t => t.CoSupervisor)
                .Include(t => t.Institute)
                .Include(t => t.Supervisor)
                .Where(t => t.Abstract.ToLower().Contains(query.ToLower()) &&
                            (!thesisType.HasValue || t.ThesisType == thesisType.Value))
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
                }).ToList();

            return result;
        }

        
    }

    public IEnumerable<ThesisDetailDto> SearchThesisNo(string query, ThesisType? thesisType)
    {
        using (var context = new MyDbContext())
        {
            var result = context.Theses
                .Include(t => t.Author)
                .Include(t => t.Language)
                .Include(t => t.CoSupervisor)
                .Include(t => t.Institute)
                .Include(t => t.Supervisor)
                .Where(t => t.ThesisNo.ToString().ToLower().Contains(query.ToLower()) &&
                            (!thesisType.HasValue || t.ThesisType == thesisType.Value))
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
                }).ToList();

            return result;
        }
        
    }

    public IEnumerable<Author> SearchAuthor(string query)
    {
        using (var context = new MyDbContext())
        {
            var result = context.Authors
                .Where(a => a.Name.ToLower().Contains(query.ToLower())).ToList();

            return result;
        }
        
    }

    public IEnumerable<Institute> SearchInstitute(string query)
    {
        using (var context = new MyDbContext())
        {
            var result = context.Institutes
                .Where(i => i.Name.ToLower().Contains(query.ToLower())).ToList();

            return result;
        }
       
    }

    public async Task<IEnumerable<SupervisorDetailDto>> SearchSupervisor(string query)
    {
        using (var context = new MyDbContext())
        {
            var result = await context.Supervisors
                .Where(s => s.Name.ToLower().Contains(query.ToLower()))
                .Select(s => new SupervisorDetailDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    PhoneNumber = s.PhoneNumber
                }).ToListAsync();

            return result;
        }
        
    }

    public IEnumerable<SubjectTopic> SearchSubjectTopic(string query)
    {
        using (var context = new MyDbContext())
        {
            var result = context.SubjectTopics
                .Where(st => st.Name.ToLower().Contains(query.ToLower())).ToList();

            return result;
        }
        
    }

    public IEnumerable<Keyword> SearchKeyword(string query)
    {
        using (var context = new MyDbContext())
        {
            var result = context.Keywords
                .Where(k => k.Name.ToLower().Contains(query.ToLower())).ToList();

            return result;
        }
        
    }
}