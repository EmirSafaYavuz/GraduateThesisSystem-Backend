using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.Entities;

namespace DataAccess.Concrete.EntityFramework;

public class EfInstituteDal : EfEntityRepositoryBase<Institute, MyDbContext>, IInstituteDal
{
    public EfInstituteDal(MyDbContext context) : base(context)
    {
    }
}