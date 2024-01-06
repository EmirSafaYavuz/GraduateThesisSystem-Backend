using Core.DataAccess;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;

namespace DataAccess.Abstract;

public interface IInstituteDal : IEntityRepository<Institute>
{
    IEnumerable<InstituteDetailDto> GetAllDetailDto();
    IEnumerable<InstituteDetailDto> GetAllDetailDtoByUniversityId(int universityId);
    InstituteDetailDto GetDetailDtoById(int id);
}