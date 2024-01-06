using Core.DataAccess;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;
using DataAccess.Entities.Enums;

namespace DataAccess.Abstract;

public interface ISearchDal
{
    IEnumerable<ThesisLookupDto> SearchThesisTitle(string query, ThesisType? thesisType);
    IEnumerable<ThesisLookupDto> SearchThesisAbstract(string query, ThesisType? thesisType);
    IEnumerable<ThesisLookupDto> SearchThesisNo(string query, ThesisType? thesisType);
    IEnumerable<Author> SearchAuthor(string query);
    IEnumerable<Institute> SearchInstitute(string query);
    IEnumerable<Supervisor> SearchSupervisor(string query);
    IEnumerable<SubjectTopic> SearchSubjectTopic(string query);
    IEnumerable<Keyword> SearchKeyword(string query);
}