using Core.Utilities.Results;
using DataAccess.Entities;

namespace Business.Abstract;

public interface ISupervisorService
{
    IDataResult<IEnumerable<Supervisor>> GetAll();
    IDataResult<Supervisor> Add(Supervisor supervisor);
    IResult Delete(int id);
}