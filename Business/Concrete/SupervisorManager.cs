using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;

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
        return new SuccessDataResult<IEnumerable<Supervisor>>(_supervisorDal.GetList());
    }

    public IDataResult<Supervisor> Add(Supervisor supervisor)
    {
        var addedSupervisor = _supervisorDal.Add(supervisor);
        _supervisorDal.SaveChanges();
        return new SuccessDataResult<Supervisor>(addedSupervisor);
    }

    public IResult Delete(int id)
    {
        var supervisor = _supervisorDal.Get(i => i.Id == id);
        if (supervisor is null)
        {
            return new ErrorResult("Supervisor not found");
        }

        _supervisorDal.Delete(supervisor);
        _supervisorDal.SaveChanges();
        return new SuccessResult();
    }
}