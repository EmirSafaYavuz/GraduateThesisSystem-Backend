using System.Linq.Expressions;
using Core.DataAccess;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;

namespace DataAccess.Abstract;

public interface IUniversityDal : IEntityRepository<University>
{
    IEnumerable<UniversityDetailDto> GetListDetailDto(Expression<Func<UniversityDetailDto, bool>> expression = null);
    UniversityDetailDto GetDetailDto(Expression<Func<UniversityDetailDto, bool>> expression);
    IEnumerable<InstituteDetailDto> GetInstitutesByUniversityId(int id);
}