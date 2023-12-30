using Core.Utilities.Results;
using DataAccess.Entities;

namespace Business.Abstract;

public interface ILocationService
{
    IDataResult<IEnumerable<Location>> GetAll();
    IDataResult<Location> Add(Location location);
    IResult Delete(int id);
    IDataResult<int> GetCount();
}