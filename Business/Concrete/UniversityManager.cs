using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;

namespace Business.Concrete;

public class UniversityManager : IUniversityService
{
    private readonly IUniversityDal _universityDal;

    public UniversityManager(IUniversityDal universityDal)
    {
        _universityDal = universityDal;
    }

    public IDataResult<IEnumerable<UniversityDetailDto>> GetAll()
    {
        return new SuccessDataResult<IEnumerable<UniversityDetailDto>>(_universityDal.GetAllDetailDto());
    }

    public IDataResult<UniversityDetailDto> GetById(int id)
    {
        var university = _universityDal.GetDetailDto();
        if (university is null)
        {
            return new ErrorDataResult<UniversityDetailDto>("University not found");
        }
        
        return new SuccessDataResult<UniversityDetailDto>(university);
    }

    public IDataResult<IEnumerable<InstituteDetailDto>> GetInstitutesByUniversityId(int id)
    {
        var institutes = _universityDal.GetInstitutesByUniversityId(id);
        if (institutes is null)
        {
            return new ErrorDataResult<IEnumerable<InstituteDetailDto>>("Institutes not found");
        }
        
        return new SuccessDataResult<IEnumerable<InstituteDetailDto>>(institutes);
    }

    public IDataResult<University> Add(University university)
    {
        var addedUniversity = _universityDal.Add(university);
        return new SuccessDataResult<University>(addedUniversity);
    }

    public IResult Delete(int id)
    {
        var university = _universityDal.GetById(id);
        if (university is null)
        {
            return new ErrorResult("University not found");
        }

        _universityDal.Delete(university);
        return new SuccessResult();
    }

    public IDataResult<long> GetCount()
    {
        var count = _universityDal.GetCount();
        return new SuccessDataResult<long>(count);
    }
}