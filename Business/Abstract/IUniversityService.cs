using Core.Utilities.Results;
using DataAccess.Entities;

namespace Business.Abstract;

public interface IUniversityService
{
    IDataResult<IEnumerable<University>> GetAll();
    IDataResult<University> Add(University university);
    IResult Delete(int id);
}