using Core.DataAccess;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;

namespace DataAccess.Abstract;

public interface ISupervisorDal : IEntityRepository<Supervisor>
{
    IEnumerable<ThesisLookupDto> GetThesesBySupervisorId(int id);
}