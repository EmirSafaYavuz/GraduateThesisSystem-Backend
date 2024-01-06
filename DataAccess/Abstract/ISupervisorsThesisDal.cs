using Core.DataAccess;
using DataAccess.Entities;

namespace DataAccess.Abstract;

public interface ISupervisorsThesisDal : IEntityRepository<SupervisorsThesis>
{
    IEnumerable<Supervisor> GetSupervisorsByThesisId(int id);
}