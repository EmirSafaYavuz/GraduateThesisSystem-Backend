using Core.Utilities.Results;
using DataAccess.Entities;

namespace Business.Abstract;

public interface IKeywordsThesisService
{
    IDataResult<IEnumerable<KeywordsThesis>> GetAll();
    IDataResult<KeywordsThesis> Add(KeywordsThesis keywordsThesis);
    IResult Delete(int id);
}