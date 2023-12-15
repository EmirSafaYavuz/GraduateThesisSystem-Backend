using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.Entities;

namespace DataAccess.Concrete.EntityFramework;

public class EfLanguageDal : EfEntityRepositoryBase<Language, MyDbContext>, ILanguageDal
{
    public EfLanguageDal(MyDbContext context) : base(context)
    {
    }
}