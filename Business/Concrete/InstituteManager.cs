using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;

namespace Business.Concrete;

public class InstituteManager : IInstituteService
{
    private readonly IInstituteDal _instituteDal;

    public InstituteManager(IInstituteDal instituteDal)
    {
        _instituteDal = instituteDal;
    }

    public IDataResult<IEnumerable<InstituteDetailDto>> GetAll()
    {
        return new SuccessDataResult<IEnumerable<InstituteDetailDto>>(_instituteDal.GetAllDetailDto());
    }

    public IDataResult<InstituteDetailDto> GetById(int id)
    {
        var institute = _instituteDal.GetDetailDtoById(id);
        if (institute is null)
        {
            return new ErrorDataResult<InstituteDetailDto>("Institute not found");
        }

        return new SuccessDataResult<InstituteDetailDto>(institute);
    }

    public IDataResult<Institute> Add(Institute institute)
    {
        var addedInstitute = _instituteDal.Add(institute);
        return new SuccessDataResult<Institute>(addedInstitute);
    }

    public IResult Delete(int id)
    {
        var institute = _instituteDal.GetById(id);
        if (institute is null)
        {
            return new ErrorResult("Institute not found");
        }

        _instituteDal.Delete(institute);
        return new SuccessResult();
    }

    public IDataResult<long> GetCount()
    {
        var count = _instituteDal.GetCount();
        return new SuccessDataResult<long>(count);
    }
}