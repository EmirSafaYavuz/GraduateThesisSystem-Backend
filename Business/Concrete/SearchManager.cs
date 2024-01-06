using Business.Abstract;
using Core.Aspects.Autofac.Transaction;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;
using DataAccess.Entities.Enums;

namespace Business.Concrete;

public class SearchManager : ISearchService
{
    private readonly ISearchDal _searchDal;

    public SearchManager(ISearchDal searchDal)
    {
        _searchDal = searchDal;
    }

    public IDataResult<IEnumerable<ThesisLookupDto>> SearchThesisTitle(string query, ThesisType? thesisType)
    {
        return new SuccessDataResult<IEnumerable<ThesisLookupDto>>(_searchDal.SearchThesisTitle(query, thesisType));
    }

    public IDataResult<IEnumerable<ThesisLookupDto>> SearchThesisAbstract(string query, ThesisType? thesisType)
    {
        return new SuccessDataResult<IEnumerable<ThesisLookupDto>>(_searchDal.SearchThesisAbstract(query, thesisType));
    }

    public IDataResult<IEnumerable<ThesisLookupDto>> SearchThesisNo(string query, ThesisType? thesisType)
    {
        return new SuccessDataResult<IEnumerable<ThesisLookupDto>>(_searchDal.SearchThesisNo(query, thesisType));
    }

    public IDataResult<IEnumerable<Author>> SearchAuthor(string query)
    {
        return new SuccessDataResult<IEnumerable<Author>>(_searchDal.SearchAuthor(query));
    }

    public IDataResult<IEnumerable<Institute>> SearchInstitute(string query)
    {
        return new SuccessDataResult<IEnumerable<Institute>>(_searchDal.SearchInstitute(query));
    }

    public IDataResult<IEnumerable<Supervisor>> SearchSupervisor(string query)
    {
        var result = _searchDal.SearchSupervisor(query);
        return new SuccessDataResult<IEnumerable<Supervisor>>(result);
    }

    public IDataResult<IEnumerable<SubjectTopic>> SearchSubjectTopic(string query)
    {
        return new SuccessDataResult<IEnumerable<SubjectTopic>>(_searchDal.SearchSubjectTopic(query));
    }

    public IDataResult<IEnumerable<Keyword>> SearchKeyword(string query)
    {
        return new SuccessDataResult<IEnumerable<Keyword>>(_searchDal.SearchKeyword(query));
    }
}