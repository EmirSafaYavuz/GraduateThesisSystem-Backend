using Core.Utilities.Results;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;

namespace Business.Abstract;

public interface IThesisService
{
    IDataResult<IEnumerable<ThesisDetailDto>> GetAll();
    IDataResult<Thesis> Add(Thesis thesis);
    IResult Delete(int id);
    IDataResult<int> GetCount();
}