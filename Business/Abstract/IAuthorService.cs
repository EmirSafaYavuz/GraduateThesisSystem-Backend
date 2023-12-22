using Core.Utilities.Results;
using DataAccess.Entities;

namespace Business.Abstract;

public interface IAuthorService
{
    IDataResult<IEnumerable<Author>> GetAll();
    IDataResult<Author> Add(Author author);
    Task<IResult> Delete(int id);
}