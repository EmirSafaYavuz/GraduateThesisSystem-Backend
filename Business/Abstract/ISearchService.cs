using System.Collections;
using Core.Utilities.Results;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;
using DataAccess.Entities.Enums;

namespace Business.Abstract;

public interface ISearchService
{
    IDataResult<IEnumerable<ThesisDetailDto>> SearchThesisTitle(string query, ThesisType? thesisType);
    IDataResult<IEnumerable<ThesisDetailDto>> SearchThesisAbstract(string query, ThesisType? thesisType);
    IDataResult<IEnumerable<ThesisDetailDto>> SearchThesisNo(string query, ThesisType? thesisType);
    IDataResult<IEnumerable<Author>> SearchAuthor(string query);
    IDataResult<IEnumerable<Institute>> SearchInstitute(string query);
    Task<IDataResult<IEnumerable<SupervisorDetailDto>>> SearchSupervisor(string query);
    IDataResult<IEnumerable<SubjectTopic>> SearchSubjectTopic(string query);
    IDataResult<IEnumerable<Keyword>> SearchKeyword(string query);
    
}