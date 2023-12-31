using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;

namespace Business.Concrete;

public class LocationManager : ILocationService
{
    private readonly ILocationDal _locationDal;

    public LocationManager(ILocationDal locationDal)
    {
        _locationDal = locationDal;
    }

    public IDataResult<IEnumerable<Location>> GetAll()
    {
        return new SuccessDataResult<IEnumerable<Location>>(_locationDal.GetAll());
    }

    public IDataResult<Location> Add(Location location)
    {
        var addedLocation = _locationDal.Add(location);
        return new SuccessDataResult<Location>(addedLocation);
    }

    public IResult Delete(int id)
    {
        var location = _locationDal.GetById(id);
        if (location is null)
        {
            return new ErrorResult("Location not found");
        }

        _locationDal.Delete(location);
        return new SuccessResult();
    }

    public IDataResult<long> GetCount()
    {
        var count = _locationDal.GetCount();
        return new SuccessDataResult<long>(count);
    }
}