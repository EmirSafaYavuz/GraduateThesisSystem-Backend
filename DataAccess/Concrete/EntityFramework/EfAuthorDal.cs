using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.Entities;

namespace DataAccess.Concrete.EntityFramework;

public class EfAuthorDal : EfEntityRepositoryBase<Author, MyDbContext>, IAuthorDal
{
    public EfAuthorDal(MyDbContext context) : base(context)
    {
    }
}