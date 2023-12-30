using Core.Utilities.Results;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;

namespace Business.Abstract;

public interface IAuthorService
{
    IDataResult<IEnumerable<Author>> GetAll();
    IDataResult<Author> GetById(int id);
    IDataResult<IEnumerable<ThesisDetailDto>> GetThesesByAuthorId(int id);
    IDataResult<Author> Add(Author author);
    IResult Delete(int id);
    IDataResult<int> GetCount();
}