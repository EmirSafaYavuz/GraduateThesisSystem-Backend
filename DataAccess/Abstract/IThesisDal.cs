using Core.DataAccess;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;

namespace DataAccess.Abstract;

public interface IThesisDal : IEntityRepository<Thesis>
{
    IEnumerable<ThesisDetailDto> GetAllDetailDto();
}