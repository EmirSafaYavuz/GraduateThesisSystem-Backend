using DataAccess.Abstract;
using DataAccess.Entities;

namespace DataAccess.Concrete.AdoNet;

public class AnLocationDal : ILocationDal
{
    public Location GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IList<Location> GetAll()
    {
        throw new NotImplementedException();
    }

    public Location Add(Location entity)
    {
        throw new NotImplementedException();
    }

    public Location Update(Location entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Location entity)
    {
        throw new NotImplementedException();
    }

    public long GetCount()
    {
        throw new NotImplementedException();
    }
}