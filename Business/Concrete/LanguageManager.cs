using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;

namespace Business.Concrete;

public class LanguageManager : ILanguageService
{
    private readonly ILanguageDal _languageDal;

    public LanguageManager(ILanguageDal languageDal)
    {
        _languageDal = languageDal;
    }

    public IDataResult<IEnumerable<Language>> GetAll()
    {
        return new SuccessDataResult<IEnumerable<Language>>(_languageDal.GetList());
    }

    public IDataResult<Language> Add(Language language)
    {
        var addedLanguage = _languageDal.Add(language);
        return new SuccessDataResult<Language>(addedLanguage);
    }

    public IResult Delete(int id)
    {
        var language = _languageDal.Get(i => i.Id == id);
        if (language is null)
        {
            return new ErrorResult("Language not found");
        }

        _languageDal.Delete(language);
        return new SuccessResult();
    }

    public IDataResult<int> GetCount()
    {
        var count = _languageDal.GetCount();
        return new SuccessDataResult<int>(count);
    }
}