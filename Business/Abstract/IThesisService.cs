using Core.Utilities.Results;
using DataAccess.Entities;

namespace Business.Abstract;

public interface IThesisService
{
    IDataResult<IEnumerable<Thesis>> GetAll();
    IDataResult<Thesis> Add(Thesis thesis);
    IResult Delete(int id);
}