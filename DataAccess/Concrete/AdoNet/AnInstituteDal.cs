using DataAccess.Abstract;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;

namespace DataAccess.Concrete.AdoNet;

public class AnInstituteDal : IInstituteDal
{
    public Institute GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IList<Institute> GetAll()
    {
        throw new NotImplementedException();
    }

    public Institute Add(Institute entity)
    {
        throw new NotImplementedException();
    }

    public Institute Update(Institute entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Institute entity)
    {
        throw new NotImplementedException();
    }

    public long GetCount()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<InstituteDetailDto> GetListDetailDto()
    {
        throw new NotImplementedException();
    }
}