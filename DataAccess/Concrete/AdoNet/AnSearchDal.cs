using DataAccess.Abstract;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;
using DataAccess.Entities.Enums;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Concrete.AdoNet;

public class AnSearchDal : ISearchDal
{
    public IEnumerable<ThesisDetailDto> SearchThesisTitle(string query, ThesisType? thesisType)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ThesisDetailDto> SearchThesisAbstract(string query, ThesisType? thesisType)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ThesisDetailDto> SearchThesisNo(string query, ThesisType? thesisType)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Author> SearchAuthor(string query)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Institute> SearchInstitute(string query)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<SupervisorDetailDto>> SearchSupervisor(string query)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<SubjectTopic> SearchSubjectTopic(string query)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Keyword> SearchKeyword(string query)
    {
        throw new NotImplementedException();
    }
}