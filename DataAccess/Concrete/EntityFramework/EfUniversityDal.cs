using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.Entities;

namespace DataAccess.Concrete.EntityFramework;

public class EfUniversityDal : EfEntityRepositoryBase<University, MyDbContext>, IUniversityDal
{
    public EfUniversityDal(MyDbContext context) : base(context)
    {
    }
}