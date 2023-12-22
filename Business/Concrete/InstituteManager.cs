using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;

namespace Business.Concrete;

public class InstituteManager : IInstituteService
{
    private readonly IInstituteDal _instituteDal;

    public InstituteManager(IInstituteDal instituteDal)
    {
        _instituteDal = instituteDal;
    }

    public IDataResult<IEnumerable<Institute>> GetAll()
    {
        return new SuccessDataResult<IEnumerable<Institute>>(_instituteDal.GetList());
    }

    public IDataResult<Institute> Add(Institute institute)
    {
        var addedInstitute = _instituteDal.Add(institute);
        _instituteDal.SaveChanges();
        return new SuccessDataResult<Institute>(addedInstitute);
    }

    public IResult Delete(int id)
    {
        var institute = _instituteDal.Get(i => i.Id == id);
        if (institute is null)
        {
            return new ErrorResult("Institute not found");
        }

        _instituteDal.Delete(institute);
        _instituteDal.SaveChanges();
        return new SuccessResult();
    }
}