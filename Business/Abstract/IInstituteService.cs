using Core.Utilities.Results;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;

namespace Business.Abstract;

public interface IInstituteService
{
    IDataResult<IEnumerable<InstituteDetailDto>> GetAll();
    IDataResult<Institute> Add(Institute institute);
    IResult Delete(int id);
    IDataResult<long> GetCount();
}