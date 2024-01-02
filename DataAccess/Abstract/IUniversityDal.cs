using System.Linq.Expressions;
using Core.DataAccess;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;

namespace DataAccess.Abstract;

public interface IUniversityDal : IEntityRepository<University>
{
    IEnumerable<UniversityDetailDto> GetAllDetailDto();
    UniversityDetailDto GetDetailDto();
    IEnumerable<InstituteDetailDto> GetInstitutesByUniversityId(int id);
}