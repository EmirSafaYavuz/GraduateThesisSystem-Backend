using DataAccess.Abstract;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;

namespace DataAccess.Concrete.AdoNet;

public class AnSupervisorDal : ISupervisorDal
{
    public Supervisor GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IList<Supervisor> GetAll()
    {
        throw new NotImplementedException();
    }

    public Supervisor Add(Supervisor entity)
    {
        throw new NotImplementedException();
    }

    public Supervisor Update(Supervisor entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Supervisor entity)
    {
        throw new NotImplementedException();
    }

    public long GetCount()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ThesisDetailDto> GetThesesBySupervisorId(int id)
    {
        throw new NotImplementedException();
    }
}