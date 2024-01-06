using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;

namespace Business.Concrete;

public class KeywordManager : IKeywordService
{
    private readonly IKeywordDal _keywordDal;

    public KeywordManager(IKeywordDal keywordDal)
    {
        _keywordDal = keywordDal;
    }

    public IDataResult<IEnumerable<Keyword>> GetAll()
    {
        return new SuccessDataResult<IEnumerable<Keyword>>(_keywordDal.GetAll());
    }

    public IDataResult<Keyword> Add(Keyword keyword)
    {
        var addedKeyword = _keywordDal.Add(keyword);
        return new SuccessDataResult<Keyword>(addedKeyword);
    }

    public IResult Delete(int id)
    {
        var keyword = _keywordDal.GetById(id);
        if (keyword is null)
        {
            return new ErrorResult("Keyword not found");
        }

        _keywordDal.Delete(keyword);
        return new SuccessResult();
    }

    public IDataResult<long> GetCount()
    {
        return new SuccessDataResult<long>(_keywordDal.GetCount());
    }

    public IDataResult<IEnumerable<ThesisLookupDto>> GetThesesByKeywordId(int id)
    {
        var result = _keywordDal.GetThesesByKeywordId(id);
        return new SuccessDataResult<IEnumerable<ThesisLookupDto>>(result);
    }
}