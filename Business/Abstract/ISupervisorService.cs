using Core.Utilities.Results;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;

namespace Business.Abstract;

public interface ISupervisorService
{
    IDataResult<IEnumerable<Supervisor>> GetAll();
    IDataResult<Supervisor> GetById(int id);
    IDataResult<Supervisor> Add(Supervisor supervisor);
    IResult Delete(int id);
    IDataResult<long> GetCount();
    IDataResult<IEnumerable<ThesisLookupDto>> GetThesesBySupervisorId(int id);
}