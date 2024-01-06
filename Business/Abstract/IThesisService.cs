using Core.Utilities.Results;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;

namespace Business.Abstract;

public interface IThesisService
{
    IDataResult<IEnumerable<ThesisLookupDto>> GetAll();
    IDataResult<ThesisDetailDto> GetById(int id);
    IDataResult<Thesis> Add(ThesisAddDto thesis);
    IResult Delete(int id);
    IDataResult<long> GetCount();
}