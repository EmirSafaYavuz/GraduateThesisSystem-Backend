using System.Linq.Expressions;
using DataAccess.Abstract;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;

namespace DataAccess.Concrete.AdoNet;

public class AnUniversityDal : IUniversityDal
{
    public University GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IList<University> GetAll()
    {
        throw new NotImplementedException();
    }

    public University Add(University entity)
    {
        throw new NotImplementedException();
    }

    public University Update(University entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(University entity)
    {
        throw new NotImplementedException();
    }

    public long GetCount()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<UniversityDetailDto> GetListDetailDto(Expression<Func<UniversityDetailDto, bool>> expression = null)
    {
        throw new NotImplementedException();
    }

    public UniversityDetailDto GetDetailDto(Expression<Func<UniversityDetailDto, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<InstituteDetailDto> GetInstitutesByUniversityId(int id)
    {
        throw new NotImplementedException();
    }
}