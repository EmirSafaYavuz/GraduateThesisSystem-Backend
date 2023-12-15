using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.Entities;

namespace DataAccess.Concrete.EntityFramework;

public class EfThesisDal : EfEntityRepositoryBase<Thesis, MyDbContext>, IThesisDal
{
    public EfThesisDal(MyDbContext context) : base(context)
    {
    }
}