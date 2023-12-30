using Core.Utilities.Results;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;

namespace Business.Abstract;

public interface IUniversityService
{
    IDataResult<IEnumerable<UniversityDetailDto>> GetAll();
    IDataResult<UniversityDetailDto> GetById(int id);
    IDataResult<IEnumerable<InstituteDetailDto>> GetInstitutesByUniversityId(int id);
    IDataResult<University> Add(University university);
    IResult Delete(int id);
    IDataResult<int> GetCount();
}