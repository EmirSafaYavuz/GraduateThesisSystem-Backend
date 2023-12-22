using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;

namespace Business.Concrete;

public class UniversityManager : IUniversityService
{
    private readonly IUniversityDal _universityDal;

    public UniversityManager(IUniversityDal universityDal)
    {
        _universityDal = universityDal;
    }

    public IDataResult<IEnumerable<University>> GetAll()
    {
        return new SuccessDataResult<IEnumerable<University>>(_universityDal.GetList());
    }

    public IDataResult<University> Add(University university)
    {
        var addedUniversity = _universityDal.Add(university);
        _universityDal.SaveChanges();
        return new SuccessDataResult<University>(addedUniversity);
    }

    public IResult Delete(int id)
    {
        var university = _universityDal.Get(i => i.Id == id);
        if (university is null)
        {
            return new ErrorResult("University not found");
        }

        _universityDal.Delete(university);
        _universityDal.SaveChanges();
        return new SuccessResult();
    }
}