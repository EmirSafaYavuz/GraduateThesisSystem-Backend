using Core.Utilities.Results;
using DataAccess.Entities;

namespace Business.Abstract;

public interface IInstituteService
{
    IDataResult<IEnumerable<Institute>> GetAll();
    IDataResult<Institute> Add(Institute institute);
    IResult Delete(int id);
}