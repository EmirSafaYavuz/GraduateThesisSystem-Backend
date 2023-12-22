using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;

namespace Business.Concrete;

public class KeywordsThesisManager : IKeywordsThesisService
{
    private readonly IKeywordsThesisDal _keywordsThesisDal;

    public KeywordsThesisManager(IKeywordsThesisDal keywordsThesisDal)
    {
        _keywordsThesisDal = keywordsThesisDal;
    }

    public IDataResult<IEnumerable<KeywordsThesis>> GetAll()
    {
        return new SuccessDataResult<IEnumerable<KeywordsThesis>>(_keywordsThesisDal.GetList());
    }

    public IDataResult<KeywordsThesis> Add(KeywordsThesis keywordsThesis)
    {
        var addedKeywordsThesis = _keywordsThesisDal.Add(keywordsThesis);
        _keywordsThesisDal.SaveChanges();
        return new SuccessDataResult<KeywordsThesis>(addedKeywordsThesis);
    }

    public IResult Delete(int id)
    {
        var keywordsThesis = _keywordsThesisDal.Get(i => i.Id == id);
        if (keywordsThesis is null)
        {
            return new ErrorResult("KeywordsThesis not found");
        }

        _keywordsThesisDal.Delete(keywordsThesis);
        _keywordsThesisDal.SaveChanges();
        return new SuccessResult();
    }
}