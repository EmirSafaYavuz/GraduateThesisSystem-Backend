using Core.Utilities.Results;
using DataAccess.Entities;

namespace Business.Abstract;

public interface ISupervisorsThesisService
{
    IDataResult<IEnumerable<SupervisorsThesis>> GetAll();
    IDataResult<SupervisorsThesis> Add(SupervisorsThesis supervisorsThesis);
    IResult Delete(int id);
}