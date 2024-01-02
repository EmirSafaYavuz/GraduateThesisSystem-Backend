using Business.Abstract;
using Core.Aspects.Autofac.Transaction;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;

namespace Business.Concrete;

public class ThesisManager : IThesisService
{
    private readonly IThesisDal _thesisDal;

    public ThesisManager(IThesisDal thesisDal)
    {
        _thesisDal = thesisDal;
    }

    public IDataResult<IEnumerable<ThesisDetailDto>> GetAll()
    {
        return new SuccessDataResult<IEnumerable<ThesisDetailDto>>(_thesisDal.GetAllDetailDto());
    }

    public IDataResult<Thesis> Add(Thesis thesis)
    {
        var addedThesis = _thesisDal.Add(thesis);
        return new SuccessDataResult<Thesis>(addedThesis);
    }

    public IResult Delete(int id)
    {
        var thesis = _thesisDal.GetById(id);
        if (thesis is null)
        {
            return new ErrorResult("Thesis not found");
        }

        _thesisDal.Delete(thesis);
        return new SuccessResult();
    }

    public IDataResult<long> GetCount()
    {
        var count = _thesisDal.GetCount();
        return new SuccessDataResult<long>(count);
    }
}