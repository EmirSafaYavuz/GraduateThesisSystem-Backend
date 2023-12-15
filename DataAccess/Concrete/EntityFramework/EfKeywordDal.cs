using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.Entities;

namespace DataAccess.Concrete.EntityFramework;

public class EfKeywordDal : EfEntityRepositoryBase<Keyword, MyDbContext>, IKeywordDal
{
    public EfKeywordDal(MyDbContext context) : base(context)
    {
    }
}