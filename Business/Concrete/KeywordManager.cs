using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;

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
        return new SuccessDataResult<IEnumerable<Keyword>>(_keywordDal.GetList());
    }

    public IDataResult<Keyword> Add(Keyword keyword)
    {
        var addedKeyword = _keywordDal.Add(keyword);
        _keywordDal.SaveChanges();
        return new SuccessDataResult<Keyword>(addedKeyword);
    }

    public IResult Delete(int id)
    {
        var keyword = _keywordDal.Get(i => i.Id == id);
        if (keyword is null)
        {
            return new ErrorResult("Keyword not found");
        }

        _keywordDal.Delete(keyword);
        _keywordDal.SaveChanges();
        return new SuccessResult();
    }
}