using Core.DataAccess;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;

namespace DataAccess.Abstract;

public interface IAuthorDal : IEntityRepository<Author>
{ 
    IEnumerable<ThesisLookupDto> GetThesesByAuthorId(int id);
}