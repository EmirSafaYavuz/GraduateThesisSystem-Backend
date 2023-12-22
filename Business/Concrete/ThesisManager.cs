using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;

namespace Business.Concrete;

public class ThesisManager : IThesisService
{
    private readonly IThesisDal _thesisDal;

    public ThesisManager(IThesisDal thesisDal)
    {
        _thesisDal = thesisDal;
    }

    public IDataResult<IEnumerable<Thesis>> GetAll()
    {
        return new SuccessDataResult<IEnumerable<Thesis>>(_thesisDal.GetList());
    }

    public IDataResult<Thesis> Add(Thesis thesis)
    {
        var addedThesis = _thesisDal.Add(thesis);
        _thesisDal.SaveChanges();
        return new SuccessDataResult<Thesis>(addedThesis);
    }

    public IResult Delete(int id)
    {
        var thesis = _thesisDal.Get(i => i.Id == id);
        if (thesis is null)
        {
            return new ErrorResult("Thesis not found");
        }

        _thesisDal.Delete(thesis);
        _thesisDal.SaveChanges();
        return new SuccessResult();
    }
}