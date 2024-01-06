using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;

namespace Business.Concrete;

public class SupervisorManager : ISupervisorService
{
    private readonly ISupervisorDal _supervisorDal;

    public SupervisorManager(ISupervisorDal supervisorDal)
    {
        _supervisorDal = supervisorDal;
    }

    public IDataResult<IEnumerable<Supervisor>> GetAll()
    {
        return new SuccessDataResult<IEnumerable<Supervisor>>(_supervisorDal.GetAll());
    }

    public IDataResult<Supervisor> GetById(int id)
    {
        var supervisor = _supervisorDal.GetById(id);
        if (supervisor is null)
        {
            return new ErrorDataResult<Supervisor>("Supervisor not found");
        }

        return new SuccessDataResult<Supervisor>(supervisor);
    }

    public IDataResult<Supervisor> Add(Supervisor supervisor)
    {
        var addedSupervisor = _supervisorDal.Add(supervisor);
        return new SuccessDataResult<Supervisor>(addedSupervisor);
    }

    public IResult Delete(int id)
    {
        var supervisor = _supervisorDal.GetById(id);
        if (supervisor is null)
        {
            return new ErrorResult("Supervisor not found");
        }

        _supervisorDal.Delete(supervisor);
        return new SuccessResult();
    }

    public IDataResult<long> GetCount()
    {
        var count = _supervisorDal.GetCount();
        return new SuccessDataResult<long>(count);
    }

    public IDataResult<IEnumerable<ThesisLookupDto>> GetThesesBySupervisorId(int id)
    {
        var result = _supervisorDal.GetThesesBySupervisorId(id);
        return new SuccessDataResult<IEnumerable<ThesisLookupDto>>(result);
    }
}