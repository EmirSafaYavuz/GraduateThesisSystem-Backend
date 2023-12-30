using Core.DataAccess;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;

namespace DataAccess.Abstract;

public interface IKeywordDal : IEntityRepository<Keyword>
{
    IEnumerable<ThesisDetailDto> GetThesesByKeywordId(int id);
}