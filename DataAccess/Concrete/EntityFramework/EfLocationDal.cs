using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.Entities;

namespace DataAccess.Concrete.EntityFramework;

public class EfLocationDal : EfEntityRepositoryBase<Location, MyDbContext>, ILocationDal
{
    public EfLocationDal(MyDbContext context) : base(context)
    {
    }
}