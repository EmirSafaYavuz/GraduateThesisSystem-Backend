using Core.Utilities.Results;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;

namespace Business.Abstract;

public interface IKeywordService
{
    IDataResult<IEnumerable<Keyword>> GetAll();
    IDataResult<Keyword> Add(Keyword keyword);
    IResult Delete(int id);
    IDataResult<long> GetCount();
    IDataResult<IEnumerable<ThesisLookupDto>> GetThesesByKeywordId(int id);
}