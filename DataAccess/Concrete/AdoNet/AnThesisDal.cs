using DataAccess.Abstract;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;

namespace DataAccess.Concrete.AdoNet;

public class AnThesisDal : IThesisDal
{
    public Thesis GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IList<Thesis> GetAll()
    {
        throw new NotImplementedException();
    }

    public Thesis Add(Thesis entity)
    {
        throw new NotImplementedException();
    }

    public Thesis Update(Thesis entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Thesis entity)
    {
        throw new NotImplementedException();
    }

    public long GetCount()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ThesisDetailDto> GetListDetailDto()
    {
        throw new NotImplementedException();
    }
}