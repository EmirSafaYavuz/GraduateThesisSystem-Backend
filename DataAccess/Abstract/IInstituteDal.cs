using Core.DataAccess;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;

namespace DataAccess.Abstract;

public interface IInstituteDal : IEntityRepository<Institute>
{
    IEnumerable<InstituteDetailDto> GetAllDetailDto();
    InstituteDetailDto GetDetailDtoById(int id);
}