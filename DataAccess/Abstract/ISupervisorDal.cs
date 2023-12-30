using Core.DataAccess;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;

namespace DataAccess.Abstract;

public interface ISupervisorDal : IEntityRepository<Supervisor>
{
    IEnumerable<ThesisDetailDto> GetThesesBySupervisorId(int id);
}