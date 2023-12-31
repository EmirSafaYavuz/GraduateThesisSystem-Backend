using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;

namespace Business.Concrete;

public class SupervisorsThesisManager : ISupervisorsThesisService
{
    private readonly ISupervisorsThesisDal _supervisorsThesisDal;

    public SupervisorsThesisManager(ISupervisorsThesisDal supervisorsThesisDal)
    {
        _supervisorsThesisDal = supervisorsThesisDal;
    }

    public IDataResult<IEnumerable<SupervisorsThesis>> GetAll()
    {
        return new SuccessDataResult<IEnumerable<SupervisorsThesis>>(_supervisorsThesisDal.GetAll());
    }

    public IDataResult<SupervisorsThesis> Add(SupervisorsThesis supervisorsThesis)
    {
        var addedSupervisorsThesis = _supervisorsThesisDal.Add(supervisorsThesis);
        return new SuccessDataResult<SupervisorsThesis>(addedSupervisorsThesis);
    }

    public IResult Delete(int id)
    {
        var supervisorsThesis = _supervisorsThesisDal.GetById(id);
        if (supervisorsThesis is null)
        {
            return new ErrorResult("SupervisorsThesis not found");
        }

        _supervisorsThesisDal.Delete(supervisorsThesis);
        return new SuccessResult();
    }
}