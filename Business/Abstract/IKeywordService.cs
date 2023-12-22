using Core.Utilities.Results;
using DataAccess.Entities;

namespace Business.Abstract;

public interface IKeywordService
{
    IDataResult<IEnumerable<Keyword>> GetAll();
    IDataResult<Keyword> Add(Keyword keyword);
    IResult Delete(int id);
}