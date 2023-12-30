using Core.DataAccess;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;
using DataAccess.Entities.Enums;

namespace DataAccess.Abstract;

public interface ISearchDal
{
    IEnumerable<ThesisDetailDto> SearchThesisTitle(string query, ThesisType? thesisType);
    IEnumerable<ThesisDetailDto> SearchThesisAbstract(string query, ThesisType? thesisType);
    IEnumerable<ThesisDetailDto> SearchThesisNo(string query, ThesisType? thesisType);
    IEnumerable<Author> SearchAuthor(string query);
    IEnumerable<Institute> SearchInstitute(string query);
    Task<IEnumerable<SupervisorDetailDto>> SearchSupervisor(string query);
    IEnumerable<SubjectTopic> SearchSubjectTopic(string query);
    IEnumerable<Keyword> SearchKeyword(string query);
}