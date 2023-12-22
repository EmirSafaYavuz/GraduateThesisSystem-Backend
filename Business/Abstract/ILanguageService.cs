using Core.Utilities.Results;
using DataAccess.Entities;

namespace Business.Abstract;

public interface ILanguageService
{
    IDataResult<IEnumerable<Language>> GetAll();
    IDataResult<Language> Add(Language language);
    IResult Delete(int id);
}