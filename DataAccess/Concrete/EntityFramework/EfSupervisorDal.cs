using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using DataAccess.Entities;

namespace DataAccess.Concrete.EntityFramework;

public class EfSupervisorDal : EfEntityRepositoryBase<Supervisor, MyDbContext>, ISupervisorDal
{
    public EfSupervisorDal(MyDbContext context) : base(context)
    {
    }
}