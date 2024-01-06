using Core.Utilities.Results;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;

namespace Business.Abstract;

public interface IAuthorService
{
    IDataResult<IEnumerable<Author>> GetAll();
    IDataResult<Author> GetById(int id);
    IDataResult<IEnumerable<ThesisLookupDto>> GetThesesByAuthorId(int id);
    IDataResult<Author> Add(Author author);
    IDataResult<Author> Update(Author author);
    IResult Delete(int id);
    IDataResult<long> GetCount();
}